using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour {

    public GameObject target;
    // 相机跟随玩家的速度
    public float followSpeed = 5f;         
    Vector3 distance;

	// Use this for initialization
	void Start () {
        distance = transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + distance, followSpeed * Time.deltaTime);
	}
}
