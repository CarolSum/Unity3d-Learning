using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarolSum.com
{
    public interface IUserAction
    {
        void launchUFO();
        void hitUFO(Vector3 mousePos);
        void switchActionInNextRound();
    }

    public interface IGameStatusOp
    {
        int getRoundNum();
        void addScore();
        void subScore();
    }

    public interface IActionManager
    {
        void addForceOnUFO(GameObject UFO);
    }
    
    public class SceneController : System.Object, IUserAction, IGameStatusOp
    {
        private static SceneController instance;    //单例模式
        private UFOController myUFOCtrl;          //管理发射飞碟及点击飞碟
        private StatusController myStatusCtrl;        //记分员
        private bool switchActionManager = false;

        public static SceneController getInstance()
        {
            if (instance == null) instance = new SceneController();
            return instance;
        }

        internal void setUFOController(UFOController ufoCtrl)
        {
            if (myUFOCtrl == null) myUFOCtrl = ufoCtrl;
        }

        internal int getTrailNum()
        {
            return myStatusCtrl.getTrialNum();
        }

        internal void setStatusController(StatusController gameStatus)
        {
            if (myStatusCtrl == null) myStatusCtrl = gameStatus;
;        }

        //记分员负责的工作
        public void addScore()
        {
            myStatusCtrl.addScore();
        }

        public int getRoundNum()
        {
            return myStatusCtrl.getRoundNum();
        }

        public void subScore()
        {
            myStatusCtrl.subScore();
        }

        //飞碟管家的任务
        public void launchUFO()
        {
            //每次发射之前清0分数
            if (switchActionManager)
            {
                switchActionManager = false;
                myUFOCtrl.switchActionManager();
            }
            myStatusCtrl.resetScore();
            myUFOCtrl.launchUFO();
        }

        public void hitUFO(Vector3 mousePos)
        {
            myUFOCtrl.hitUFO(mousePos);
        }

        public void switchActionInNextRound()
        {
            myStatusCtrl.showSwitchText();
            switchActionManager = true;
        }
    }

}

