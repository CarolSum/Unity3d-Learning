using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFactory : MonoBehaviour {

    private List<GameObject> usedPatrols = new List<GameObject>(); 
    private Vector3[] vec = new Vector3[9];     // 保存每个巡逻兵的初始位置

    public List<GameObject> getPatrols()
    {
        int[] pos_x = { -6, 4, 13 };
        int[] pos_y = { -4, 6, -13 };
        int index = 0;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                vec[index] = new Vector3(pos_x[i], 0, pos_y[j]);
                index++;
            }
        }
        for(int i = 0; i < 9; i++)
        {
            GameObject patrol = Instantiate(Resources.Load<GameObject>("Prefabs/Patrol"));
            patrol.transform.position = vec[i];
            patrol.GetComponent<PatrolData>().sign = i + 1;
            patrol.GetComponent<PatrolData>().start_position = vec[i];
            usedPatrols.Add(patrol);
        }
        return usedPatrols;
    }

    public void stopPatrol()
    {
        for(int i = 0; i < usedPatrols.Count; i++)
        {
            usedPatrols[i].gameObject.GetComponent<Animator>().SetBool("run", false);
        }
    }
}
