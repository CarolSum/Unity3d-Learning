using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarolSum.com
{
    public class UFOActionAdapter : IActionManager
    {
        private PhysisActionManager physisActionManager;
        private CCActionManager ccActionManager;
        int whichActionManager = 0; // 0 -> CCActionManager, 1 -> PhysisActionManager

        public UFOActionAdapter()
        {
            physisActionManager = PhysisActionManager.getInstance();
            ccActionManager = CCActionManager.getInstance();
        }

        public void switchActionMode()
        {
            whichActionManager = 1 - whichActionManager;
        }

        public void addForceOnUFO(GameObject UFO)
        {
            if(whichActionManager == 1)
            {
                physisActionManager.addForce(UFO);
            }
            else
            {
                ccActionManager.addForce(UFO);
            }
        }
    }

    public class PhysisActionManager
    {
        private SceneController scene;
        private static PhysisActionManager instance;

        private PhysisActionManager()
        {
            scene = SceneController.getInstance();
        }

        public static PhysisActionManager getInstance()
        {
            if (instance == null) instance = new PhysisActionManager();
            return instance;
        }

        public void addForce(GameObject UFO)
        {
            Vector3 force = getRandomForce();
            UFO.GetComponent<Rigidbody>().useGravity = true;
            UFO.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }

        private Vector3 getRandomForce()
        {
            int x = UnityEngine.Random.Range(-30, 31);
            int y = UnityEngine.Random.Range(30, 41);
            int z = UnityEngine.Random.Range(20, 31);
            float t = 0.7f + scene.getTrailNum() / 20;
            return new Vector3(x, y, z) * t;
        }
    }


    public class CCActionManager
    {
        private SceneController scene;
        private static CCActionManager instance;

        private CCActionManager()
        {
            scene = SceneController.getInstance();
        }

        public static CCActionManager getInstance()
        {
            if (instance == null) instance = new CCActionManager();
            return instance;
        }

        public void addForce(GameObject UFO)
        {
            // 控制飞碟向某个随机方向持续移动
            UFO.GetComponent<Rigidbody>().useGravity = false;
            UFO.GetComponent<Rigidbody>().velocity = new Vector3(5, 15, 5);

        }
    }
}