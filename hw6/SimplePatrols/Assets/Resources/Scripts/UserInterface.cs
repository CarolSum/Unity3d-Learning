using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour {

    private IUserAction action;
    private GUIStyle scoreStyle = new GUIStyle();
    private GUIStyle textStyle = new GUIStyle();
    private GUIStyle overStyle = new GUIStyle();
    private int showTime = 8;

	// Use this for initialization
	void Start () {
        action = SSDirector.getInstance().currentSceneController as IUserAction;
        textStyle.normal.textColor = new Color(255,255,255,1);
        textStyle.fontSize = 16;
        scoreStyle.normal.textColor = new Color(1, 0.92f, 0.016f, 1);
        scoreStyle.fontSize = 16;
        overStyle.fontSize = 25;
        overStyle.normal.textColor = new Color(255, 255, 255, 1);
        StartCoroutine(showTip());
	}

    private IEnumerator showTip()
    {
        while(showTime >= 0)
        {
            yield return new WaitForSeconds(1);
            showTime--;
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            action.MovePlayer(Diretion.UP);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            action.MovePlayer(Diretion.DOWN);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            action.MovePlayer(Diretion.LEFT);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            action.MovePlayer(Diretion.RIGHT);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 5, 200, 50), "分数:", textStyle);
        GUI.Label(new Rect(55, 5, 200, 50), action.GetScore().ToString(), scoreStyle);
        GUI.Label(new Rect(Screen.width - 170, 5, 50, 50), "剩余水晶数", textStyle);
        GUI.Label(new Rect(Screen.width - 80, 5, 50, 50), action.GetCrystalNum().ToString(), scoreStyle);
        if(action.GetGameOver() && action.GetCrystalNum() != 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "Sorry!游戏结束!", overStyle);
            if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "重新开始"))
            {
                action.Restart();
                return;
            }
        }else if(action.GetCrystalNum() == 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "恭喜收集完全部水晶！", overStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "重新开始"))
            {
                action.Restart();
                return;
            }
        }
        if(showTime > 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 80, 10, 100, 100), "方向键控制玩家移动", textStyle);
            GUI.Label(new Rect(Screen.width / 2 - 87, 30, 100, 100), "躲避巡逻兵追捕加1分", textStyle);
            GUI.Label(new Rect(Screen.width / 2 - 90, 50, 100, 100), "寻找失落的水晶以通过游戏！", textStyle);
        }
    }
}
