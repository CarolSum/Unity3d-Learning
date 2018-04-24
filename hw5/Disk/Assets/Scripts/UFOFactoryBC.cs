using CarolSum.com;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFactory : System.Object {
    private static UFOFactory instance;                                        //单例工厂
    private List<GameObject> usedUFOList = new List<GameObject>();            //使用中的飞碟
    private List<GameObject> freeUFOList = new List<GameObject>();           //空闲的飞碟

    private GameObject UFOItem;        //飞碟预设

    public static UFOFactory getInstance()
    {
        if (instance == null) instance = new UFOFactory();
        return instance;
    }

    public GameObject getUFO()
    {
        if(freeUFOList.Count == 0)
        {
            GameObject newUFO = Camera.Instantiate(UFOItem);
            usedUFOList.Add(newUFO);
            return newUFO;
        }
        else
        {
            GameObject oldUFO = freeUFOList[0];
            freeUFOList.RemoveAt(0);
            oldUFO.SetActive(true);
            usedUFOList.Add(oldUFO);
            return oldUFO;
        }
    }

    //update()检测飞镖落地，回收。此方法由GameModel的update()方法触发;
    //飞碟未被击中，落地不扣分
    public void detectLandingUFOs()
    {
        for(int i = 0; i < usedUFOList.Count; i++)
        {
            if(usedUFOList[i].transform.position.y <= -8 || usedUFOList[i].transform.position.z <= -40 || usedUFOList[i].transform.position.z >= 60 || usedUFOList[i].transform.position.x <= -100 || usedUFOList[i].transform.position.x >= 100 || usedUFOList[i].transform.position.y >= 25)
            {
                usedUFOList[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                usedUFOList[i].SetActive(false);
                freeUFOList.Add(usedUFOList[i]);
                usedUFOList.Remove(usedUFOList[i]);
                i--;
                //SceneController.getInstance().subScore();
            }
        }
    }

    //飞镖被击中，回收 
    public void RecyclingUFO(GameObject UFOObject)
    {
        UFOObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        UFOObject.SetActive(false);
        freeUFOList.Add(UFOObject);
        usedUFOList.Remove(UFOObject);
    }


    //是否处于发射飞碟阶段，即空中是否有飞碟在飞
    public bool isLaunching()
    {
        return (usedUFOList.Count > 0);
    }

    public void initItems(GameObject ufoItem)
    {
        UFOItem = ufoItem;
    }
}

public class UFOFactoryBC: MonoBehaviour
{
    public GameObject ufoItem;

    private void Awake()
    {
        UFOFactory.getInstance().initItems(ufoItem);
    }
}
