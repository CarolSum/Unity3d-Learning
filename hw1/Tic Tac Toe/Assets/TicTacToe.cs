using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour {
    public Texture2D bgImage;

    private int turn = -1;
    private int[,] state = new int[3, 3];

	// Use this for initialization
	void Start () {
        Reset();
	}

    private void Reset()
    {
        turn = 1;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                state[i, j] = 0;
            }
        }
    }

    private void OnGUI()
    {
        //background image
        GUIStyle bgStyle = new GUIStyle();
        bgStyle.normal.background = bgImage;
        GUI.Label(new Rect(0, 0, 1280, 1024), "", bgStyle);

        GUIStyle msgStyle = new GUIStyle();
        msgStyle.fontSize = 30;
        msgStyle.normal.textColor = new Color(255, 255, 0);

        if (GUI.Button(new Rect(530, 380, 100, 50), "Reset")) Reset();
        int result = checkState();
        if(result == 1)
        {
            GUI.Label(new Rect(475, 140, 100, 50), "O wins the game!!", msgStyle);
        }else if(result == 2){
            GUI.Label(new Rect(475, 140, 100, 50), "X wins the game!!", msgStyle);
        }
        else
        {
            int flag = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (state[i, j] == 0) flag = 1;
            if (flag == 0) GUI.Label(new Rect(475, 140, 100, 50), "It ends in a draw!", msgStyle);
        }

        for (int i = 0; i < 3; i++)
        {
            for(int j = 0; j <3; j++)
            {
                if (state[i, j] == 1) GUI.Button(new Rect(500 + i * 50, 180 + j * 50, 50, 50), "O");
                if (state[i, j] == 2) GUI.Button(new Rect(500 + i * 50, 180 + j * 50, 50, 50), "X");
                if(GUI.Button(new Rect(500 + i * 50, 180 + j * 50, 50, 50), ""))
                {
                    if(result == 0)
                    {
                        if (turn == 1) state[i, j] = 1;
                        else state[i, j] = 2;
                        turn = -turn;
                    }
                }
            }
        }

    }

    private int checkState()
    {
        for (int i = 0; i < 3; i++) {
            if (state[i, 0] != 0 && state[i, 0] == state[i, 1] && state[i, 1] == state[i, 2])
                return state[i, 0];
        }
        for(int j = 0; j < 3; j++)
        {
            if (state[0, j] != 0 && state[0, j] == state[1, j] && state[1, j] == state[2, j])
                return state[0, j];
        }
        if (state[1, 1] != 0 && ((state[0, 0] == state[1, 1] && state[1, 1] == state[2, 2]) || (state[0, 2] == state[1, 1] && state[1, 1] == state[2, 0])))
            return state[1, 1];
        return 0;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
