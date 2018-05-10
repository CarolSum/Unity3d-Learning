using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolActionManager : SSActionManager {

    private PatrolAction patrolAction;

	public void GoPatrol(GameObject patrol)
    {
        patrolAction = PatrolAction.getSSAction(patrol.transform.position);
        this.RunAction(patrol, patrolAction, this);
    }

}
