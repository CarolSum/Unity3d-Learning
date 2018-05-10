using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCollider : MonoBehaviour {

    // 玩家进入巡逻兵的巡逻范围
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.gameObject.transform.parent.GetComponent<PatrolData>().ifFollowPlayer = true;
            this.gameObject.transform.parent.GetComponent<PatrolData>().player = other.gameObject;
        }
    }

    // 玩家逃离巡逻兵巡逻范围
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.gameObject.transform.parent.GetComponent<PatrolData>().ifFollowPlayer = false;
            this.gameObject.transform.parent.GetComponent<PatrolData>().player = null;
        }
    }

}
