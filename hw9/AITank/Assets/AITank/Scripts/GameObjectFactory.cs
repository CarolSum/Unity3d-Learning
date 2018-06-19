using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tankType : int { Player, Enemy }

public class GameObjectFactory : MonoBehaviour {
    // 玩家
    public GameObject player;
    // npc
    public GameObject tank;
    // 子弹
    public GameObject bullet;
    // 爆炸粒子系统
    public ParticleSystem ps;

    private Dictionary<int, GameObject> usingTanks;
    private Dictionary<int, GameObject> freeTanks;

    private Dictionary<int, GameObject> usingBullets;
    private Dictionary<int, GameObject> freeBullets;

    private List<ParticleSystem> psContainer;

    private void Awake()
    {
        usingTanks = new Dictionary<int, GameObject>();
        freeTanks = new Dictionary<int, GameObject>();
        usingBullets = new Dictionary<int, GameObject>();
        freeBullets = new Dictionary<int, GameObject>();
        psContainer = new List<ParticleSystem>();
    }

    // Use this for initialization
    void Start () {
        //回收坦克的委托事件
        AITank.recycleEvent += recycleTank;
    }

    public GameObject getPlayer()
    {
        return player;
    }

    public GameObject getTank()
    {
        if(freeTanks.Count == 0)
        {
            GameObject newTank = Instantiate<GameObject>(tank);
            usingTanks.Add(newTank.GetInstanceID(), newTank);
            //在一个随机范围内设置坦克位置
            newTank.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            return newTank;
        }
        foreach (KeyValuePair<int, GameObject> pair in freeTanks)
        {
            pair.Value.SetActive(true);
            freeTanks.Remove(pair.Key);
            usingTanks.Add(pair.Key, pair.Value);
            pair.Value.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            return pair.Value;
        }
        return null;
    }

    public GameObject getBullet(tankType type)
    {
        if (freeBullets.Count == 0)
        {
            GameObject newBullet = Instantiate(bullet);
            newBullet.GetComponent<Bullet>().setTankType(type);
            usingBullets.Add(newBullet.GetInstanceID(), newBullet);
            return newBullet;
        }
        foreach (KeyValuePair<int, GameObject> pair in freeBullets)
        {
            pair.Value.SetActive(true);
            pair.Value.GetComponent<Bullet>().setTankType(type);
            freeBullets.Remove(pair.Key);
            usingBullets.Add(pair.Key, pair.Value);
            return pair.Value;
        }
        return null;
    }

    public ParticleSystem getPs()
    {
        for(int i = 0; i < psContainer.Count; i++)
        {
            if (!psContainer[i].isPlaying) return psContainer[i];
        }
        ParticleSystem newPs = Instantiate<ParticleSystem>(ps);
        psContainer.Add(newPs);
        return newPs;
    }

    public void recycleTank(GameObject tank)
    {
        usingTanks.Remove(tank.GetInstanceID());
        freeTanks.Add(tank.GetInstanceID(), tank);
        tank.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        tank.SetActive(false);
    }

    public void recycleBullet(GameObject bullet)
    {
        usingBullets.Remove(bullet.GetInstanceID());
        freeBullets.Add(bullet.GetInstanceID(), bullet);
        bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        bullet.SetActive(false);
    }
}
