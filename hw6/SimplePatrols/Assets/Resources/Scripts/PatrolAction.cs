using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : SSAction {

	private enum Direction { EAST, NORTH, WEST, SOUTH };
    // 移动前的初始x和z坐标
    private float posX, posZ;
    // 移动的长度
    private float moveLength;
    // 移动速度
    private float speed = 1.2f;
    // 是否达到一个目标点
    private bool isArrivedAnEndPoint = true;
    // 移动的方向
    private Direction direction = Direction.EAST;
    // 侦察兵的数据
    private PatrolData data;

    private PatrolAction() { }

    public static PatrolAction getSSAction(Vector3 location)
    {
        PatrolAction action = CreateInstance<PatrolAction>();
        action.posX = location.x;
        action.posZ = location.z;
        action.moveLength = UnityEngine.Random.Range(4, 7);
        return action;
    }

    public override void Start()
    {
        this.gameObject.GetComponent<Animator>().SetBool("run", true);
        data = this.gameObject.GetComponent<PatrolData>();
    }

    public override void Update()
    {
        // 防止碰撞后旋转
        if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
        if(transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        goPatrol();

        // 如果侦察兵需要跟随玩家并且玩家就在侦察兵所在区域
        if(data.ifFollowPlayer && data.areaSign == data.sign)
        {
            this.destory = true;
            this.callback.SSActoinEvent(this, 0, this.gameObject);
        }
    }

    private void goPatrol()
    {
        // 到达一个点之后改变方向及设置下一个目标点
        if (isArrivedAnEndPoint)
        {
            switch (direction)
            {
                case Direction.EAST:
                    posX -= moveLength;
                    break;
                case Direction.NORTH:
                    posZ += moveLength;
                    break;
                case Direction.WEST:
                    posX += moveLength;
                    break;
                case Direction.SOUTH:
                    posZ -= moveLength;
                    break;
            }
            isArrivedAnEndPoint = false;
        }

        this.transform.LookAt(new Vector3(posX, 0, posZ));
        float distance = Vector3.Distance(transform.position, new Vector3(posX, 0, posZ));
        
        // 当前位置与目的坐标距离比较
        if(distance > 0.9)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(posX, 0, posZ), speed * Time.deltaTime);
        }
        else
        {
            direction += 1;
            if(direction > Direction.SOUTH)
            {
                direction = Direction.EAST;
            }
            isArrivedAnEndPoint = true;
        }
    }
}
