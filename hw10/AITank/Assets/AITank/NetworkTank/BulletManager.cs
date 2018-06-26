using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletManager : MonoBehaviour {

    //private float explosionRadius = 3f;

    private void FixedUpdate()
    {
        if(this.transform.position.y <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        var hit = other.gameObject;
        var hitPlayer = hit.GetComponent<PlayerMove>();

        if (hitPlayer != gameObject)
        {
            // 根据击中坦克与爆炸中心的距离计算伤害值
            //float distance = Vector3.Distance(colliders[i].transform.position, transform.position);//被击中坦克与爆炸中心的距离
            //float hurt = 100f / distance;
            //float current = colliders[i].GetComponent<Tank>().getHp();
            //colliders[i].GetComponent<Tank>().setHp(current - hurt);
            var combat = hit.GetComponent<Combat>();
            combat.TakeDamage(100);
        }
        Destroy(gameObject);
    }
}
