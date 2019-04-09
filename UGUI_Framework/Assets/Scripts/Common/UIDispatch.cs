using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// UI事件派发类(观察者模式)
/// </summary>
public class UIDispatch : SingleTon<UIDispatch>
{
    /// <summary>
    /// 按钮点击事件委托原型
    /// </summary>
    public delegate void OnBtnClickHandler(params object[] args);

    /// <summary>
    /// 按钮名称对应多个点击事件
    /// </summary>
    private Dictionary<string, List<OnBtnClickHandler>> m_BtnClickDic 
        = new Dictionary<string, List<OnBtnClickHandler>>();

    /// <summary>
    /// 添加监听
    /// </summary>
    public void AddListener(string _btnName, OnBtnClickHandler _handler)
    {
        if (m_BtnClickDic.ContainsKey(_btnName))
        {
            //集合中不存在该点击事件时，再添加
            if (!m_BtnClickDic[_btnName].Contains(_handler))
            {
                m_BtnClickDic[_btnName].Add(_handler);
            }
        }
        else //第一次添加该按钮
        {
            List<OnBtnClickHandler> btnList = new List<OnBtnClickHandler>();
            btnList.Add(_handler);
            m_BtnClickDic.Add(_btnName, btnList);
        }
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    public void RemoveListener(string _btnName, OnBtnClickHandler _handler)
    {
        if (m_BtnClickDic.ContainsKey(_btnName))
        {
            //得到Key对应的集合
            List<OnBtnClickHandler> btnList = m_BtnClickDic[_btnName];
            //删除某个点击事件
            if (btnList.Contains(_handler))
            {
                btnList.Remove(_handler);
            }
            //该按钮没有对应的点击事件了
            if (btnList.Count == 0)
            {
                //字典一并删除
                m_BtnClickDic.Remove(_btnName);
            }
        }
    }

    /// <summary>
    /// 事件发生(事件派发)
    /// </summary>
    public void Dispatch(string _btnName, params object[] args)
    {
        if (m_BtnClickDic.ContainsKey(_btnName))
        {
            //先得到key对应的value
            List<OnBtnClickHandler> btnList = m_BtnClickDic[_btnName];

            //集合不为空，并且有成员
            if (btnList != null && btnList.Count > 0)
            {
                //遍历value集合
                for (int i = 0; i < btnList.Count; i++)
                {
                    //对应的委托变量不为空，执行
                    if (btnList[i] != null)
                    {
                        btnList[i](args);
                    }
                }
            }
        }
    }
}
