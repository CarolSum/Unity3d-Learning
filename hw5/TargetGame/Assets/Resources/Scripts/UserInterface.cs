using carolsum.com;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour {

    private IUserAction action;
    private ISceneController scene;

    // Use this for initialization
    void Start () {
        action = SceneController.getInstance() as IUserAction;
        scene = SceneController.getInstance() as ISceneController;
	}
	
	// Update is called once per frame
	void Update () {
        //用户按下空格，获取弓箭
        if (Input.GetKeyDown(KeyCode.Space)) action.getArrow();
        if (scene.ifReadyToShoot())
        {
            //按住鼠标左键，跟随移动
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
                action.moveArrow(mousePos);
            }
            //左键松开，射出弓箭
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mousePos = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
                action.shootArrow(mousePos);
            }
        }
	}
}
