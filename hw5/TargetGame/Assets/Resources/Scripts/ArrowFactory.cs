using carolsum.com;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory : System.Object
{
    private static ArrowFactory instance;
    private GameObject ArrowPrefabs;
    private List<GameObject> usingArrowList = new List<GameObject>();
    private List<GameObject> freeArrowList = new List<GameObject>();
    private Vector3 INITIAL_POS = new Vector3(0, 0, -19);

    public static ArrowFactory getInstance()
    {
        if (instance == null) instance = new ArrowFactory();
        return instance;
    }

    public void init(GameObject _arrowPrefabs)
    {
        ArrowPrefabs = _arrowPrefabs;
    }

    internal void detectArrowsReuse()
    {
       for(int i = 0; i < usingArrowList.Count; i++)
        {
            if(usingArrowList[i].transform.position.y <= -8)
            {
                usingArrowList[i].GetComponent<Rigidbody>().isKinematic = true;
                usingArrowList[i].SetActive(false);
                usingArrowList[i].transform.position = INITIAL_POS;
                freeArrowList.Add(usingArrowList[i]);
                usingArrowList.Remove(usingArrowList[i]);
                i--;
                SceneController.getInstance().changeWind();
            }
        } 
    }

    internal GameObject getArrow()
    {
        if(freeArrowList.Count == 0)
        {
            GameObject newArrow = Camera.Instantiate(ArrowPrefabs);
            usingArrowList.Add(newArrow);
            return newArrow;
        }
        else
        {
            GameObject oldArrow = freeArrowList[0];
            freeArrowList.RemoveAt(0);
            oldArrow.SetActive(true);
            usingArrowList.Add(oldArrow);
            return oldArrow;
        }
    }
}
