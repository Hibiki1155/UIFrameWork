using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using LitJson;

/// <summary>
///注册窗口视图类 
/// </summary>
public class UIRegisterWindowView : UIWindowViewBase
{
    /// <summary>
    /// 用户名输入框
    /// </summary>
    public InputField m_userName;

    /// <summary>
    /// 密码输入框
    /// </summary>
    public InputField m_passWord;

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnAllBtnClick(GameObject go)
    {
        base.OnAllBtnClick(go);

        switch (go.name)
        {
            case "btn_RetLogOn":
                UIDispatch.GetInstance.Dispatch("UIRegisterWindowView_Btn_RetLogOn");
                break;
            case "btn_Register":
                UIDispatch.GetInstance.Dispatch("UIRegisterWindowView_Btn_Register");
                break;
        }
    }
}
