using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;

/// <summary>
/// 初始化场景的控制器
/// </summary>
public class UIInitSceneCtrl : MonoBehaviour
{
    /// <summary>
    /// 持有一个View的脚本
    /// </summary>
    private UIInitSceneView m_InitView;

    void Start()
    {
        //克隆InitViewScene
        GameObject initClone = UISceneCtrl.GetInstance.LoadScene(SceneType.Init);
        //从场景上的到View
        m_InitView = initClone.GetComponent<UIInitSceneView>();

        m_InitView.m_imgLog.transform.localPosition = Vector3.up * 1000;

        //Log图标的动画
        LogAnimation();
    }

    private void LogAnimation()
    {
        m_InitView.m_imgLog.transform.DOLocalMove(Vector3.zero, 2f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                m_InitView.m_imgLog.transform.DOScale(Vector3.one * 3, 1.0f)
                .OnComplete(
                    () =>
                    {
                        //加载到登录场景
                        ScenesLoadMgr.GetInstance.LoadLogOn();
                    }
                    );
            });
    }
}
