using CarolSum.com;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour {
    private IUserAction action;

	// Use this for initialization
	void Start () {
        action = SceneController.getInstance() as IUserAction;
	}
	
	// Update is called once per frame
	void Update () {
        //检测鼠标点击
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            action.hitUFO(mousePos);
        }
        //检测用户按下空格
        if (Input.GetKeyDown(KeyCode.Space))
        {
            action.launchUFO();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            action.switchActionInNextRound();
        }
    }
}
