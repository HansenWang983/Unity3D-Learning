using System;  
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class SceneActionManager : SSActionManager, ISSActionCallback {  

	public FirstController sceneController; 
	public List<CCMoveToActions> actions;  
	public int diskNumber = 0;  

	private List<SSAction> used = new List<SSAction>();  //used是用来保存正在使用的动作 
	private List<SSAction> free = new List<SSAction>();  //free是用来保存未使用的动作 

	//飞碟动作的缓存工厂模式
	SSAction GetSSAction() {  
		SSAction action = null;  
		if (free.Count > 0)  
		{  
			action = free[0];  
			free.Remove(free[0]);  
		}  
		else  
			action = ScriptableObject.Instantiate<CCMoveToActions>(actions[0]);  

		used.Add(action);  
		return action;  
	}  

	public void FreeSSAction(SSAction action){  
		SSAction tmp = null;  
		foreach (SSAction i in used) {  
			if (action.GetInstanceID() == i.GetInstanceID())  
				tmp = i;  
		}  
		if (tmp != null) {  
			tmp.reset();  
			free.Add(tmp);  
			used.Remove(tmp);  
		}  
	}  

	//场记的动作管理器为此场景
	protected new void Start() {  
		sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
		sceneController.actionManager = this;  
		actions.Add(CCMoveToActions.GetSSAction());  

	}  

	//执行完每次飞碟动作执行的回调函数
	//即飞碟落地后执行飞碟工厂的回收
	public new void SSActionEvent(SSAction source){  
		if (source is CCMoveToActions) {  
			diskNumber--;  
			DiskFactory df = Singleton<DiskFactory>.Instance;  
			df.FreeDisk(source.gameobject);  
			FreeSSAction(source);  
		}  
	}  

	//抛出一系列飞碟，执行简单动作
	public void Fly(GameObject disk){
		RunAction(disk, GetSSAction(), (ISSActionCallback)this);  
	}  
}