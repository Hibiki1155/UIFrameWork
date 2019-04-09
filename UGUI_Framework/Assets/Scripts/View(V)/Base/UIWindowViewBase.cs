using UnityEngine;
using System.Collections;

/// <summary>
/// 所有窗口视图的基类
/// </summary>
public class UIWindowViewBase : UIViewBase
{
    /// <summary>
    /// 窗口挂点位置
    /// </summary>
    public ContainerType m_ContainerType = ContainerType.Center;

    /// <summary>
    /// 动画类型
    /// </summary>
    public AnimationType m_AniType = AnimationType.Normal;

    /// <summary>
    /// 动画持续时间
    /// </summary>
    public float m_Duration = 1.0f;

    /// <summary>
    /// 窗口类型
    /// </summary>
    public WindowType m_WindowsType = WindowType.None;

    /// <summary>
    /// 下个要打开的窗口
    /// </summary>
    public WindowType m_NextWindowsType = WindowType.None;

    /// <summary>
    /// 窗口关闭委托变量
    /// </summary>
    public System.Action m_WinCloseCallBack;

    /// <summary>
    /// 重写父类的点击
    /// </summary>
    /// <param name="go"></param>
    protected override void OnAllBtnClick(GameObject go)
    {
        //点击的是按钮
        if (go.name.Equals("btn_Close", System.StringComparison.CurrentCultureIgnoreCase))
        {
            UIWindowCtrl.GetInstance.CloseWindow(m_WindowsType);
        }
    }

    /// <summary>
    /// 销毁前调用
    /// </summary>
    protected override void BeforeDestroy()
    {
        if (m_NextWindowsType == WindowType.None) return;

        if (m_WinCloseCallBack != null)
        {
            m_WinCloseCallBack();
        }
    }
}
