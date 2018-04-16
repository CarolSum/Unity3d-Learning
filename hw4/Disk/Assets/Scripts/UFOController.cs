using CarolSum.com;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour {
    //预设文件
    public GameObject PlaneItem, LauncherItem, ExplosionItem;
    public Material greenMat, redMat, blueMat;

    private GameObject plane, launcher, explosion;
    private SceneController scene;

    //发射每一个飞碟的时间间隔
    private const float LAUNCH_GAP = 0.1f;

	// Use this for initialization
	void Start () {
        scene = SceneController.getInstance();
        scene.setUFOController(this);
        plane = Instantiate(PlaneItem);
        launcher = Instantiate(LauncherItem);
        explosion = Instantiate(ExplosionItem);
	}
	
	// Update is called once per frame
	void Update () {
        UFOFactory.getInstance().detectLandingUFOs();
	}

    public void launchUFO()
    {
        int trailNum = scene.getTrailNum() > 3 ? 3 : scene.getTrailNum();
        Debug.Log("发射！");
        if (!UFOFactory.getInstance().isLaunching())
        {
            StartCoroutine(launchUFOs(trailNum));
        }
    }

    IEnumerator launchUFOs(int roundNum)
    {
        for(int i = 0; i<roundNum; i++)
        {
            GameObject UFO = UFOFactory.getInstance().getUFO();
            UFO.transform.position = launcher.transform.position;
            UFO.GetComponent<MeshRenderer>().material = getMaterial(scene.getTrailNum());

            Vector3 force = getRandomForce();
            UFO.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

            //每隔LAUNCH_GAP时间发射一个UFO
            yield return new WaitForSeconds(LAUNCH_GAP);
        }
    }

    private Vector3 getRandomForce()
    {
        int x = UnityEngine.Random.Range(-30, 31);
        int y = UnityEngine.Random.Range(30, 41);
        int z = UnityEngine.Random.Range(20, 31);
        float t = 0.7f + scene.getTrailNum() / 20;
        return new Vector3(x, y, z) * t;
    }

    public void hitUFO(Vector3 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag.Equals("UFO"))
            {
                createExplosion(hit.collider.gameObject.transform.position);
                scene.addScore();
                UFOFactory.getInstance().RecyclingUFO(hit.collider.gameObject);
            }
        }
    }

    private void createExplosion(Vector3 position)
    {
        explosion.transform.position = position;
        explosion.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = getMaterial(scene.getTrailNum());
        explosion.GetComponent<ParticleSystem>().Play();
    }

    private Material getMaterial(int roundNum)
    {
        switch(roundNum % 3)
        {
            case 0:
                return redMat;
            case 1:
                return greenMat;
            case 2:
                return blueMat;
            default:
                return redMat; 
        }
    }

}
