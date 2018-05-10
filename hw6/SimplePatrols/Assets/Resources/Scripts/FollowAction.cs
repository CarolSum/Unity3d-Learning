using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAction : SSAction {

    private float speed = 2f;
    private GameObject player;      // 玩家
    private PatrolData data;        // 侦察兵数据

    private FollowAction() { }

    public static FollowAction getSSAction(GameObject player)
    {
        FollowAction action = CreateInstance<FollowAction>();
        action.player = player;
        return action;
    }

    public override void Start()
    {
        data = this.gameObject.GetComponent<PatrolData>();
    }

    public override void Update()
    {
        if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        this.transform.LookAt(player.transform.position);

        if (!data.ifFollowPlayer || data.areaSign != data.sign)
        {
            this.destory = true;
            this.callback.SSActoinEvent(this, 1, this.gameObject);
        }
    }

}
