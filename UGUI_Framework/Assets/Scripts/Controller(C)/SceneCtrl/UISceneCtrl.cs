using UnityEngine;
using System.Collections;

/// <summary>
/// 所有UI场景资源的控制脚本
/// </summary>
public class UISceneCtrl : SingleTon<UISceneCtrl>
{

    /// <summary>
    /// 当前的挂点
    /// </summary>
    public Transform m_CurtContainerPos;

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <returns></returns>
    public GameObject LoadScene(SceneType _type)
    {
        GameObject sceneClone = ResourcesMgr.GetInstance.Load(ResourcesType.UIScene, "UIRoot_" + _type.ToString());
        m_CurtContainerPos = sceneClone.GetComponent<UISceneViewBase>().m_ContainerPos;

        return sceneClone;
    }

}
