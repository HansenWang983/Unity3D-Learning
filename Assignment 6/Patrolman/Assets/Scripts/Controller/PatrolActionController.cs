using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolActionController : SSActionManager {

	//Patrol Action Manager
//	public FirstController sceneController;

	private PatrolAction patrolAction;              

//	void Start(){
//		sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
//		sceneController.actionController = this;
//	}

    public void GoPatrol(GameObject patrolObj)
    {	
        patrolAction = PatrolAction.GetSSAction(patrolObj.transform.position);
        this.RunAction(patrolObj, patrolAction, this);
    }
    public void DestroyAllAction()
    {
        DestroyAll();
    }
}