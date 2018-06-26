using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
    private float hp;

    public float getHp()
    {
        return hp;
    }

    public void setHp(float hp)
    {
        this.hp = hp;
    }
}
