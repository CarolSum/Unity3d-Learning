using CarolSum.com;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    //各种预设资源
    public GameObject canvasItem, roundTextItem, scoreTextItem, TipsTextItem;
    //回合数
    private int roundNum = 1;
    //trial数
    private int trialNum = 1;
    //得分
    private int score = 0;
    //每一个trial的得分上界
    private int scoreUpBound = 100;
    //Tips显示的时间
    private const float TIPS_TEXT_SHOW_TIME = 0.8f;

    private GameObject canvas, roundText, scoreText, TipsText;
    private SceneController scene;

    // Use this for initialization
    void Start()
    {
        scene = SceneController.getInstance();
        scene.setStatusController(this);

        canvas = Instantiate(canvasItem);
        roundText = Instantiate(roundTextItem, canvas.transform);
        roundText.GetComponent<Text>().text = "Round: " + roundNum + " Trial: " + trialNum;

        scoreText = Instantiate(scoreTextItem, canvas.transform);
        scoreText.GetComponent<Text>().text = "Score: " + score + " / " + (roundNum * 100);

        TipsText = Instantiate(TipsTextItem, canvas.transform);
        showTipsText();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int getRoundNum() { return roundNum; }

    void addRoundNum()
    {
        roundNum++;
        roundText.GetComponent<Text>().text = "Round: " + roundNum + " Trial: " + trialNum;
    }

    public int getScore()
    {
        return score;
    }

    public int getTrialNum() { return trialNum; }

    public void addScore()
    {
        score += 100;
        scoreText.GetComponent<Text>().text = "Score: " + score + " / " + scoreUpBound;

        Debug.Log(scoreUpBound);


        //当分数达到当前回合最大分数自动进入下一轮
        if (score >= scoreUpBound)
        {
            trialNum++;
            updateScoreUpBound();
            roundText.GetComponent<Text>().text = "Round: " + roundNum + " Trial: " + trialNum;
            resetScore();
            showTipsText();
        }
        if (trialNum > 10)
        {
            addRoundNum();
            trialNum = 1;
            roundText.GetComponent<Text>().text = "Round: " + roundNum + " Trial: " + trialNum;
            updateScoreUpBound();
            resetScore();
            showTipsText();
        }
    }

    private void updateScoreUpBound()
    {
        scoreUpBound = trialNum > 3 ? 300 : trialNum * 100;
    }

    private void showTipsText()
    {
        TipsText.GetComponent<Text>().text = "Round " + roundNum + ": Trial " + trialNum + "!";
        TipsText.SetActive(true);
        StartCoroutine(waitForSomeTimeAndDisappearTipsText());
    }

    IEnumerator waitForSomeTimeAndDisappearTipsText()
    {
        yield return new WaitForSeconds(TIPS_TEXT_SHOW_TIME);
        TipsText.SetActive(false);
    }

    public void resetScore()
    {
        score = 0;
        scoreText.GetComponent<Text>().text = "Score: " + score + " / " + scoreUpBound;
    }


    public void subScore()
    {
        score = score >= 100 ? score - 100 : 0;
        scoreText.GetComponent<Text>().text = "Score: " + score + " / " + scoreUpBound;
    }


}
