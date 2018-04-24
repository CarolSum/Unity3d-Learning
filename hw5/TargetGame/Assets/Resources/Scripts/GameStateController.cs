using carolsum.com;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour {

    public GameObject canvasPrefabs, scoreTextPrefabs, tipsTextPrefabs, windTextPrefabs;
    private int score = 0, windDir = 0, windStrength = 0;

    private const float TIPS_SHOW_TIME = 0.5f;

    private GameObject canvas, scoreText, tipsText, windText;
    private SceneController scene;
    private string[] windDirectionArray;

    // Use this for initialization
    void Start () {
        scene = SceneController.getInstance();
        scene.setGameStatueController(this);
        canvas = Instantiate(canvasPrefabs);
        scoreText = Instantiate(scoreTextPrefabs, canvas.transform);
        tipsText = Instantiate(tipsTextPrefabs, canvas.transform);
        windText = Instantiate(windTextPrefabs, canvas.transform);

        //scoreText.transform.Translate(canvas.transform.position);
        //tipsText.transform.Translate(canvas.transform.position);
        //windText.transform.Translate(canvas.transform.position);

        scoreText.GetComponent<Text>().text = "Score: " + score;
        tipsText.SetActive(false);
        windDirectionArray = new string[8] { "↑", "↗", "→", "↘", "↓", "↙", "←", "↖" };
        changeWind();
    }

    public void changeWind()
    {
        windDir = UnityEngine.Random.Range(0,8);
        windStrength = UnityEngine.Random.Range(0, 8);
        windText.GetComponent<Text>().text = "Wind: " + windDirectionArray[windDir] + " x" + windStrength;
    }

    internal void addScore(int point)
    {
        score += point;
        scoreText.GetComponent<Text>().text = "Score: " + score;
        changeWind();
    }

    // Update is called once per frame
    void Update () {
		
	}

    internal int getWindDirec()
    {
        return windDir;
    }

    internal int getWindStrength()
    {
        return windStrength;
    }

    //提示命中环数
    internal void showTips(int point)
    {
        tipsText.GetComponent<Text>().text = point + " Points!";
        tipsText.SetActive(true);
        StartCoroutine(waitForTipsDisappear());
    }

    private IEnumerator waitForTipsDisappear()
    {
        yield return new WaitForSeconds(TIPS_SHOW_TIME);
        tipsText.SetActive(false);
    }
}
