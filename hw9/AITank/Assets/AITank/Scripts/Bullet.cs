using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    // 子弹伤害半径
    public float explosionRadius = 3f;
    private tankType type;

	public void setTankType(tankType type)
    {
        this.type = type;
    }

    private void Update()
    {
        if(this.transform.position.y < 0 && this.gameObject.activeSelf)
        {
            GameObjectFactory mf = Singleton<GameObjectFactory>.Instance;
            // 落地爆炸
            ParticleSystem explosion = mf.getPs();
            explosion.transform.position = transform.position;
            explosion.Play();
            mf.recycleBullet(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // 获得单实例工厂
        GameObjectFactory mf = Singleton<GameObjectFactory>.Instance;
        ParticleSystem explosion = mf.getPs();
        explosion.transform.position = transform.position;

        // 获取爆炸范围内的所有碰撞体
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].tag == "tankPlayer" && this.type == tankType.Enemy || colliders[i].tag == "tankEnemy" && this.type == tankType.Player)
            {
                // 根据击中坦克与爆炸中心的距离计算伤害值
                float distance = Vector3.Distance(colliders[i].transform.position, transform.position);//被击中坦克与爆炸中心的距离
                float hurt = 100f / distance;
                float current = colliders[i].GetComponent<Tank>().getHp();
                colliders[i].GetComponent<Tank>().setHp(current - hurt);
            }
        }

        explosion.Play();
        if (this.gameObject.activeSelf)
        {
            mf.recycleBullet(this.gameObject);
        }
    }
}
