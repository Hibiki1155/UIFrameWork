using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// 所有UI视图的基类
/// </summary>
public class UIViewBase : MonoBehaviour
{
    #region 生命周期和点击功能函数
    void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        //通过当前对象子对象获取所有Buuton组件
        Button[] buttonArr = GetComponentsInChildren<Button>(true);

        if (buttonArr.Length > 0)
        {
            //循环按钮数组，全部按钮绑定一个点击事件
            for (int i = 0; i < buttonArr.Length; i++)
            {
                EventTriggerListener.Get(buttonArr[i].gameObject).onClick += OnBtnClick;
            }
        }

        OnStart();
    }

    /// <summary>
    /// 按钮点击函数
    /// </summary>
    private void OnBtnClick(GameObject go)
    {
        OnAllBtnClick(go);
    }

    void Update()
    {
        OnUpdate();
    }

    void OnDestroy()
    {
        BeforeDestroy();
    }
    #endregion

    #region 子类重写功能函数
    protected virtual void OnAwake() { }

    protected virtual void OnStart() { }

    protected virtual void OnUpdate() { }

    protected virtual void BeforeDestroy() { }

    protected virtual void OnAllBtnClick(GameObject go) { }
    #endregion
}
