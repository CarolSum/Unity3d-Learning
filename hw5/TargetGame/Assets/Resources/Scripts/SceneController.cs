using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace carolsum.com
{
    public interface IUserAction
    {
        void getArrow();
        void moveArrow(Vector3 mousePos);
        void shootArrow(Vector3 mousePos);
    }

    public interface ISceneController
    {
        void addScore(int point);
        void showTips(int point);
        void changeWind();
        int getWindDirection();
        int getWindStrength();
        bool ifReadyToShoot();
    }

    public class SceneController : System.Object, IUserAction, ISceneController
    {
        private static SceneController instance;
        private GameStateController gameStateController;
        private ArrowController arrowController;

        public static SceneController getInstance()
        {
            if (instance == null) instance = new SceneController();
            return instance;
        }

        public void setGameStatueController(GameStateController gsc)
        {
            gameStateController = gsc;
        }

        public void setArrowController(ArrowController _arrowController)
        {
            arrowController = _arrowController;
        }

        public void addScore(int point)
        {
            gameStateController.addScore(point);
        }

        public void changeWind()
        {
            gameStateController.changeWind();
        }

        public void getArrow()
        {
            arrowController.getArrow();
        }

        public int getWindDirection()
        {
            return gameStateController.getWindDirec();
        }

        public int getWindStrength()
        {
            return gameStateController.getWindStrength();
        }

        public bool ifReadyToShoot()
        {
            return arrowController.ifReadyToShoot();
        }

        public void moveArrow(Vector3 mousePos)
        {
            arrowController.moveArrow(mousePos);
        }

        public void shootArrow(Vector3 mousePos)
        {
            arrowController.shootArrow(mousePos);
        }

        public void showTips(int point)
        {
            gameStateController.showTips(point);
        }
    }

}