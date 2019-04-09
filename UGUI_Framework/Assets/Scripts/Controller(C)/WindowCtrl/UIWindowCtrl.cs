using UnityEngine;
using System.Collections.Generic;
using System;
using DG.Tweening; //DoTween

/// <summary>
/// 所有窗口的加载控制
/// </summary>
public class UIWindowCtrl : SingleTon<UIWindowCtrl>
{
    /// <summary>
    /// 窗口克隆体的缓冲区
    /// </summary>
    private Dictionary<WindowType, UIWindowViewBase> m_WindowCloneDic =
        new Dictionary<WindowType, UIWindowViewBase>();

    /// <summary>
    /// 窗口的挂点
    /// </summary>
    private Transform m_ContainerPos;

    /// <summary>
    /// 窗口资源加载
    /// </summary>
    /// <param name="_type">窗口类型</param>
    /// <returns>返回窗口资源</returns>
    public GameObject LoadWindow(WindowType _type)
    {
        if (_type == WindowType.None)
        {
            return null;
        }

        //局部变量
        GameObject windowClone = null;

        //缓冲区中没有该窗口
        if (!m_WindowCloneDic.ContainsKey(_type))
        {
            //从资源管理器中加载
            windowClone = ResourcesMgr.GetInstance.Load(
            ResourcesType.UIWindow,
            "pan_" + _type.ToString(),
            true);

            //窗口克隆为空
            if (windowClone == null) return null;

            //获取窗口父对象
            UIWindowViewBase windowBase = windowClone.GetComponent<UIWindowViewBase>();
            if (windowBase == null) return null;

            //存入缓冲区
            m_WindowCloneDic.Add(_type, windowBase);

            //每次加载的时候，告诉加载窗口自身的窗口类型
            windowBase.m_WindowsType = _type;

            //确认父对象
            switch (windowBase.m_ContainerType)
            {
                case ContainerType.Center:
                    m_ContainerPos = UISceneCtrl.GetInstance.m_CurtContainerPos;
                    break;
                case ContainerType.LeftTop:
                    break;
                case ContainerType.LeftBottom:
                    break;
                case ContainerType.RightTop:
                    break;
                case ContainerType.RightBottom:
                    break;
            }

            //设置层级关系
            windowClone.transform.SetParent(m_ContainerPos, false);

            //窗口隐藏
            //windowClone.gameObject.SetActive(false);

            //窗口动画
            StartWindowAnimation(windowBase, true);

        }
        else //缓存区中存在
        {
            //直接读取缓存区
            windowClone = m_WindowCloneDic[_type].gameObject;

            //窗口动画
            StartWindowAnimation(windowClone.GetComponent<UIWindowViewBase>(), true);
        }

        //返回克隆
        return windowClone;
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    public void CloseWindow(WindowType _type)
    {
        if (m_WindowCloneDic.ContainsKey(_type))
        {
            //反向播放
            StartWindowAnimation(m_WindowCloneDic[_type], false);
            //移除缓冲区中存储内容
            m_WindowCloneDic.Remove(_type);
        }
    }

    /// <summary>
    /// 窗口动画
    /// </summary>
    private void StartWindowAnimation(UIWindowViewBase _windowBase, bool _isOpen)
    {
        switch (_windowBase.m_AniType)
        {
            case AnimationType.Normal:
                break;
            case AnimationType.CenterToBig:
                CenterToBig(_windowBase, _isOpen);
                break;
            case AnimationType.FromTop:
                MoveToZero(_windowBase, _isOpen);
                break;
            case AnimationType.FromBottom:
                MoveToZero(_windowBase, _isOpen);
                break;
            case AnimationType.FromLeft:
                MoveToZero(_windowBase, _isOpen);
                break;
            case AnimationType.FromRight:
                MoveToZero(_windowBase, _isOpen);
                break;
        }
    }

    private void MoveToZero(UIWindowViewBase _windowBase, bool _isOpen)
    {
        _windowBase.gameObject.SetActive(true);
        Vector3 LocalPos = Vector3.zero;
        switch (_windowBase.m_AniType)
        {

            case AnimationType.FromTop:
                LocalPos = new Vector3(0, 650, 0);
                break;
            case AnimationType.FromBottom:
                LocalPos = -Vector3.up * 650;
                break;
            case AnimationType.FromLeft:
                LocalPos = -Vector3.right * 650;
                break;
            case AnimationType.FromRight:
                LocalPos = Vector3.right * 650;
                break;
            default:
                break;
        }
        _windowBase.transform.localPosition = LocalPos;
        _windowBase.transform.DOLocalMove(Vector3.zero, _windowBase.m_Duration).SetAutoKill(false)
            .Pause().OnRewind(
            () =>
            {
                GameObject.Destroy(_windowBase.gameObject);
            }


            ).SetEase(Ease.OutBack);
        if (_isOpen)
        {
            _windowBase.transform.DOPlayForward();
        }
        else
        {
            _windowBase.transform.DOPlayBackwards();
        }
    }

    /// <summary>
    /// 从中间变大
    /// </summary>
    /// <param name="_windowBase"></param>
    private void CenterToBig(UIWindowViewBase _windowBase, bool _isOpen)
    {
        //活跃设置true
        _windowBase.gameObject.SetActive(true);
        //缩放为0
        _windowBase.transform.localScale = Vector3.zero;

        _windowBase.transform.DOScale(Vector3.one, _windowBase.m_Duration)
            .SetAutoKill(false) //不杀死
            .Pause() //不执行
            .SetEase(Ease.OutBack) //回弹效果
            .OnRewind(() =>
            {
                GameObject.Destroy(_windowBase.gameObject);
            }) //反向播放动画才会调用
            ;

        if (!_isOpen)
        {
            _windowBase.transform.DOPlayBackwards(); //反向
        }
        else
        {
            _windowBase.transform.DOPlayForward(); //正向
        }
    }
}
