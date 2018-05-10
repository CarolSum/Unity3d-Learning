using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolData : MonoBehaviour {
    // 巡逻兵所在区域号
    public int sign;
    // 是否跟随玩家
    public bool ifFollowPlayer = false;         
    // 当前玩家所在区域标志
    public int areaSign = -1;                  
    public GameObject player;
    // 巡逻兵的初始位置
    public Vector3 start_position;              
}
