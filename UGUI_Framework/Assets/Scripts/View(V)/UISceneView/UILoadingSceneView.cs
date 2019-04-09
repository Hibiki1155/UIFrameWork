using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 加载场景视图类
/// </summary>
public class UILoadingSceneView : UISceneViewBase
{
    /// <summary>
    /// 进度条
    /// </summary>
    public Slider m_sldProgress;

    /// <summary>
    /// 进度条文本
    /// </summary>
    public Text m_txtPercent;

    /// <summary>
    /// 设置进度条
    /// </summary>
    /// <param name="_value"></param>
    public void SetProgress(float _value)
    {
        if (m_sldProgress == null || m_txtPercent == null)
        {
            return;
        }

        m_sldProgress.value = _value;

        m_txtPercent.text = string.Format((int)(_value * 100) + "%");
    }
}
