using UnityEngine;
using System.Collections;

/// <summary>
/// 登录场景控制器
/// </summary>
public class UILogOnSceneCtrl : MonoBehaviour
{
    void Start()
    {
        UISceneCtrl.GetInstance.LoadScene(SceneType.LogOn);
    }

}
