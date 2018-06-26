using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Tank {
    public delegate void destroy();
    public static event destroy destroyEvent;

	void Start () {
        // 初始血量
        setHp(500f);	
	}
	
	void Update () {
		if(getHp() <= 0)
        {
            this.gameObject.SetActive(false);
            if (destroyEvent != null)
                destroyEvent();
        }
	}
}
