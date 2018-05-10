using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollider : MonoBehaviour {

    public int sign = 0;
    SceneController sceneController;

	// Use this for initialization
	void Start () {
        sceneController = SSDirector.getInstance().currentSceneController as SceneController;
	}

    private void Update()
    {
        sceneController = SSDirector.getInstance().currentSceneController as SceneController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            sceneController.areaSign = sign;
        }
    }

}
