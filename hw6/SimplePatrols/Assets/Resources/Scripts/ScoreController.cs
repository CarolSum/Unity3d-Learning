using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

    public SceneController sceneController;
    public int score = 0;
    public int crystal_num = 12;


    void AddScore()
    {
        score++;
    }

	// Use this for initialization
	void Start () {
        sceneController = (SceneController)SSDirector.getInstance().currentSceneController;
        sceneController.scoreController = this;
	}
}
