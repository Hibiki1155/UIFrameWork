using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// 登录窗口视图类
/// </summary>
public class UILogOnWindowView : UIWindowViewBase
{
    /// <summary>
    /// 登录窗口 账号输入框
    /// </summary>
    public InputField m_UserName;

    /// <summary>
    /// 登录窗口 密码输入框
    /// </summary>
    public InputField m_Password;
    
    protected override void OnAllBtnClick(GameObject go)
    {
        base.OnAllBtnClick(go);
        switch (go.name)
        {
            case "btn_LogOn":
                UIDispatch.GetInstance.Dispatch("UILogOnWindowView_Btn_LogOn");
                break;
            case "btn_ToRegister":
                UIDispatch.GetInstance.Dispatch("UILogOnWindowView_Btn_ToRegister");
                break;
        }
    }
}
