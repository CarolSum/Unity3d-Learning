using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalCollider : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
            Singleton<GameEventManager>.Instance.ReduceCrystalNum();
        }
    }
}
