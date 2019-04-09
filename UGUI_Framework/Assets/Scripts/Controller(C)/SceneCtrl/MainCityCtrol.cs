using UnityEngine;
using System.Collections;
using System;

public class MainCityCtrol : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitSceonds());
	}

    private IEnumerator WaitSceonds()
    {
        yield return new WaitForSeconds(2.0f);
        ScenesLoadMgr.GetInstance.LoadFuBen();
    }
}
