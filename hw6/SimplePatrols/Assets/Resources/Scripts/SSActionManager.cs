using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour, ISSActionCallback {

    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> waitingAdd = new List<SSAction>();
    private List<int> waitingDelete = new List<int>();


    public void SSActoinEvent(SSAction source, int param = 0, GameObject objParam = null)
    {
        if(param == 0)
        {
            // 侦察兵跟随玩家
            FollowAction followAction = FollowAction.getSSAction(objParam.gameObject.GetComponent<PatrolData>().player);
            this.RunAction(objParam, followAction, this);
        }
        else
        {
            PatrolAction patrolAction = PatrolAction.getSSAction(objParam.gameObject.GetComponent<PatrolData>().start_position);
            this.RunAction(objParam, patrolAction, this);
            Singleton<GameEventManager>.Instance.playerEscape();
        }
    }

    public void RunAction(GameObject objParam, SSAction action, ISSActionCallback manager)
    {
        action.gameObject = objParam;
        action.transform = objParam.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }

	// Update is called once per frame
	protected void Update ()
    {
	    foreach(SSAction action in waitingAdd)
        {
            actions[action.GetInstanceID()] = action;
        }
        waitingAdd.Clear();

        foreach(KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destory)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }else if (ac.enable)
            {
                ac.Update();
            }
        }

        foreach(int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        waitingDelete.Clear();
	}

    public void DestroyAll()
    {
        foreach(KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            ac.destory = true;
        }
    }
}
