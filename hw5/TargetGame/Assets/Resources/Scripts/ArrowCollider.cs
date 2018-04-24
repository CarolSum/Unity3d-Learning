using carolsum.com;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollider : MonoBehaviour {

    private ISceneController scene;

    // Use this for initialization
    void Start () {
        scene = SceneController.getInstance() as ISceneController;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "target")
        {
            gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.SetActive(false);
            int points = other.gameObject.name[other.gameObject.name.Length - 1] - '0';
            scene.showTips(points);
            scene.addScore(points);
        }
    }
}
