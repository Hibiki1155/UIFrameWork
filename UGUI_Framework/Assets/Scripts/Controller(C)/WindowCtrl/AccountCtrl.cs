using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 账号控制器(登录和注册两个窗口的逻辑)
/// </summary>
public class AccountCtrl : UISingleton<AccountCtrl>, IDisposable, ISystemCtrl
{
    /// <summary>
    /// 登录窗口
    /// </summary>
    private UILogOnWindowView m_LogOnView;

    /// <summary>
    /// 注册窗口
    /// </summary>
    private UIRegisterWindowView m_RegisterView;

    /// <summary>
    /// 构造函数(用来监听点击事件)
    /// </summary>
    public AccountCtrl()
    {
        //登录窗口监听
        UIDispatch.GetInstance.AddListener("UILogOnWindowView_Btn_LogOn", OnLogOnBtnClick);
        UIDispatch.GetInstance.AddListener("UILogOnWindowView_Btn_ToRegister", OnToRegBtnClick);

        //注册窗口监听
        UIDispatch.GetInstance.AddListener("UIRegisterWindowView_Btn_RetLogOn", OnRetLogOnBtnClick);
        UIDispatch.GetInstance.AddListener("UIRegisterWindowView_Btn_Register", OnRegisterBtnClick);
    }

    public void OpenView(WindowType _type)
    {
        switch (_type)
        {
            case WindowType.LogOn:
                OpenLogOnView();
                break;
            case WindowType.Register:
                OpenRegisterView();
                break;
        }
    }

    #region 两个打开窗口的功能函数
    /// <summary>
    /// 打开登录窗口
    /// </summary>
    public void OpenLogOnView()
    {

        m_LogOnView = UIWindowCtrl.GetInstance.LoadWindow(WindowType.LogOn).GetComponent<UILogOnWindowView>();

        //每次登陆窗口打开时，委托变量就绑定注册窗口的打开函数
        m_LogOnView.m_WinCloseCallBack = () =>
        {
            OpenRegisterView();
        };
    }

    /// <summary>
    /// 打开注册窗口
    /// </summary>
    public void OpenRegisterView()
    {
        m_RegisterView = UIWindowCtrl.GetInstance.LoadWindow(WindowType.Register).GetComponent<UIRegisterWindowView>();

        m_RegisterView.m_WinCloseCallBack = () =>
        {
            OpenLogOnView();
        };
    }
    #endregion

    #region 登录窗口 两个按钮点击事件
    /// <summary>
    /// 去注册按钮点击函数
    /// </summary>
    private void OnToRegBtnClick(params object[] args)
    {
        Close(WindowType.LogOn);
        m_LogOnView.m_NextWindowsType = WindowType.Register;
    }

    /// <summary>
    /// 登录按钮点击函数
    /// </summary>
    private void OnLogOnBtnClick(params object[] args)
    {
        Debug.Log("登录按钮被点击");

        if (string.IsNullOrEmpty(m_LogOnView.m_UserName.text))
        {
            Debug.Log("请输入账号");
            return;
        }

        if (string.IsNullOrEmpty(m_LogOnView.m_Password.text))
        {
            Debug.Log("请输入密码");
            return;
        }

        //得到缓存的账号和密码
        string username = PlayerPrefs.GetString("USERNAME");
        string password = PlayerPrefs.GetString("PASSWORD");

        if (username != m_LogOnView.m_UserName.text || password != m_LogOnView.m_Password.text)
        {
            Debug.Log("您输入账号或密码错误!");
            return;
        }

        ScenesLoadMgr.GetInstance.LoadMainCity();
    }
    #endregion

    #region 注册窗口 两个按钮点击事件
    /// <summary>
    /// 注册按钮点击函数
    /// </summary>
    private void OnRegisterBtnClick(params object[] args)
    {
        if (string.IsNullOrEmpty(m_RegisterView.m_userName.text))
        {
            Debug.Log("请输入账号");
            return;
        }

        if (string.IsNullOrEmpty(m_RegisterView.m_passWord.text))
        {
            Debug.Log("请输入密码");
            return;
        }

        //得到账号和密码输入框中的文本信息
        string username = m_RegisterView.m_userName.text;
        string password = m_RegisterView.m_passWord.text;

        //将账号密码发送到服务器 ---服务器查询是否可以注册

        //服务器匹配成功后，再将账号和密码存入本地注册表
        PlayerPrefs.SetString("USERNAME", username);
        PlayerPrefs.SetString("PASSWORD", password);

        Debug.Log("注册成功!");
    }

    /// <summary>
    /// 返回登录按钮点击函数
    /// </summary>
    private void OnRetLogOnBtnClick(params object[] args)
    {
        Close(WindowType.Register);
        m_RegisterView.m_NextWindowsType = WindowType.LogOn;
    }

    #endregion

    public void Dispose()
    {
        UIDispatch.GetInstance.RemoveListener("UILogOnWindowView_Btn_LogOn", OnLogOnBtnClick);
        UIDispatch.GetInstance.RemoveListener("UILogOnWindowView_Btn_ToRegister", OnToRegBtnClick);
    }
}
