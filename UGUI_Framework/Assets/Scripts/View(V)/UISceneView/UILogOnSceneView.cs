using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 登录场景视图类
/// </summary>
public class UILogOnSceneView : UISceneViewBase
{
    protected override void OnStart()
    {
        base.OnStart();

        StartCoroutine(WaitScene());
    }

    private IEnumerator WaitScene()
    {
        yield return new WaitForSeconds(0.2f);

        //测试
        AccountCtrl.GetInstance.OpenLogOnView();
    }
}
