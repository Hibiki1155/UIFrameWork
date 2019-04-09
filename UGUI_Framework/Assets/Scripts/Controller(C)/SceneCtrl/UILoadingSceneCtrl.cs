using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Text;

/// <summary>
/// 加载场景控制器
/// </summary>
public class UILoadingSceneCtrl : MonoBehaviour
{
    /// <summary>
    /// 加载场景的视图脚本对象
    /// </summary>
    private UILoadingSceneView m_LoadingView;

    /// <summary>
    /// 异步加载信息
    /// </summary>
    private AsyncOperation m_Async;

    /// <summary>
    /// 当前的进度条
    /// </summary>
    public int m_CurtProgress = 0;

    void Start()
    {
        GameObject loadingClone = UISceneCtrl.GetInstance.LoadScene(SceneType.Loading);
        m_LoadingView = loadingClone.GetComponent<UILoadingSceneView>();

        StartCoroutine(WaitAsync());
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitAsync()
    {
        //查看当前操控类型是否更新
        Debug.Log(ScenesLoadMgr.GetInstance.m_CurtType);

        StringBuilder builder = new StringBuilder();

        switch (ScenesLoadMgr.GetInstance.m_CurtType)
        {
            case SceneType.LogOn:
                builder.Append("03UIRoot_LogOn");
                break;
            case SceneType.MainCity:
                builder.Append("04MainCity");
                break;
            case SceneType.FuBen:
                builder.Append("05FuBen");
                break;

        }

        m_Async = SceneManager.LoadSceneAsync(builder.ToString());

        //是否允许场景立即切换，false不允许
        m_Async.allowSceneActivation = false;

        yield return m_Async;
    }

    void Update()
    {
        int toProgress = 0;

        //m_Async.progress 异步场景切换的进度信息(0,1)
        if (m_Async.progress < 0.9)
        {
            //Mathf.Clamp()限制
            toProgress = Mathf.Clamp((int)(m_Async.progress * 100), 1, 90);
        }
        else
        {
            toProgress = 100;
        }

        //
        if (m_CurtProgress < toProgress)
        {
            m_CurtProgress++;
        }
        else
        {
            m_CurtProgress = 100;
            m_Async.allowSceneActivation = true;
        }

        //更新UI
        m_LoadingView.SetProgress(m_CurtProgress * 0.01f);
    }
}
