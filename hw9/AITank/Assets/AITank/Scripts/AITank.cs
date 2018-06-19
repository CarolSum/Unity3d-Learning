using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITank : Tank {

    public delegate void recycle(GameObject tank);
    public static event recycle recycleEvent;

    private Vector3 target;
    private bool gameover;

    // 巡逻点
    private static Vector3[] points = { new Vector3(37.6f,0,0), new Vector3(40.9f,0,39), new Vector3(13.4f, 0, 39),
        new Vector3(13.4f, 0, 21), new Vector3(0,0,0), new Vector3(-20,0,0.3f), new Vector3(-20, 0, 32.9f), 
        new Vector3(-37.5f, 0, 40.3f), new Vector3(-37.5f,0,10.4f), new Vector3(-40.9f, 0, -25.7f), new Vector3(-15.2f, 0, -37.6f),
        new Vector3(18.8f, 0, -37.6f), new Vector3(39.1f, 0, -18.1f)
    };
    private int destPoint = 0;
    private NavMeshAgent agent;
    private bool isPatrol = false;

    private void Awake()
    {
        destPoint = UnityEngine.Random.Range(0, 13);
    }

    // Use this for initialization
    void Start () {
        setHp(100f);
        StartCoroutine(shoot());
        agent = GetComponent<NavMeshAgent>();
    }

    private IEnumerator shoot()
    {
        while (!gameover)
        {
            for(float i = 1; i > 0; i -= Time.deltaTime)
            {
                yield return 0;
            }
            // 当敌军坦克距离玩家坦克不到20时开始射击
            if(Vector3.Distance(transform.position, target) < 20)
            {
                GameObjectFactory mf = Singleton<GameObjectFactory>.Instance;
                GameObject bullet = mf.getBullet(tankType.Enemy);
                bullet.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z) + transform.forward * 1.5f;
                bullet.transform.forward = transform.forward;
                
                // 发射子弹
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(bullet.transform.forward * 20, ForceMode.Impulse);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        gameover = GameDirector.getInstance().currentSceneController.isGameOver();
        if (!gameover)
        {
            target = GameDirector.getInstance().currentSceneController.getPlayerPos();
            if (getHp() <= 0 && recycleEvent != null)
            {//如果npc坦克被摧毁，则回收它
                recycleEvent(this.gameObject);
            }
            else
            {
                if(Vector3.Distance(transform.position, target) <= 30)
                {
                    isPatrol = false;
                    //否则向玩家坦克移动
                    agent.autoBraking = true;
                    agent.SetDestination(target);
                }
                else
                {
                    patrol();
                }
            }
        }
        else
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.velocity = Vector3.zero;
            agent.ResetPath();
        }
    }

    private void patrol()
    {
        if(isPatrol)
        {
            if(!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
        else
        {
            agent.autoBraking = false;
            GotoNextPoint();
        }
        isPatrol = true;
    }

    private void GotoNextPoint()
    {
        agent.SetDestination(points[destPoint]);
        destPoint = (destPoint + 1) % points.Length;
    }
}
