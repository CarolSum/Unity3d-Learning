using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalFactory : MonoBehaviour {

    private GameObject crystal = null;
    private List<GameObject> usedCrystal = new List<GameObject>();
    private float range = 12;       // 水晶生成的坐标范围

    private static CrystalFactory instance = null;

    public List<GameObject> getCrystals()
    {
        for(int i = 0; i < 12; i++)
        {
            crystal = Instantiate(Resources.Load<GameObject>("Prefabs/Crystal"));
            float ranx = UnityEngine.Random.Range(-range, range);
            float ranz = UnityEngine.Random.Range(-range, range);
            crystal.transform.position = new Vector3(ranx, 0, ranz);
            usedCrystal.Add(crystal);
        }
        return usedCrystal;
    }

    public static CrystalFactory getInstance()
    {
        if (instance == null) instance = new CrystalFactory();
        return instance;
    }
}
