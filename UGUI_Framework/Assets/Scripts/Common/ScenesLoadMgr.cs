using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// 不同场景的切换
/// </summary>
public class ScenesLoadMgr : SingleTon<ScenesLoadMgr>
{
    /// <summary>
    /// 当前的枚举类型
    /// </summary>
    public SceneType m_CurtType = SceneType.Init;

    /// <summary>
    /// 切换到LogOn场景
    /// </summary>
    public void LoadLogOn()
    {
        m_CurtType = SceneType.LogOn;
        SceneManager.LoadScene("02UIRoot_Loading");
    }

    /// <summary>
    /// 切换到MainCity场景
    /// </summary>
    public void LoadMainCity()
    {
        m_CurtType = SceneType.MainCity;
        SceneManager.LoadScene("02UIRoot_Loading");
    }

    /// <summary>
    /// 切换到FuBen场景
    /// </summary>
    public void LoadFuBen()
    {
        m_CurtType = SceneType.FuBen;
        SceneManager.LoadScene("02UIRoot_Loading");
    }

}
