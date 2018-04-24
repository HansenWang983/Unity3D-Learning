	using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//删除SceneActionManager,直接在SSActionManager中添加简单动作的管理器，并且使用简单动作的工厂
public class SSActionManager : MonoBehaviour,IActionManager,ISSActionCallback {


	private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction> ();
	private List<SSAction> waitingAdd = new List<SSAction>();
	private List<int> waitingDelete = new List<int>();

	public FirstController sceneController;
	public int diskNumber = 0;

	protected void Start() {
		sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
		sceneController.actionManager = this;
	}

	protected void Update() {
		foreach (SSAction ac in waitingAdd) {
			actions[ac.GetInstanceID()] = ac;
		}
		waitingAdd.Clear();
		foreach (KeyValuePair<int, SSAction> kv in actions) {
			SSAction ac = kv.Value;
			if (ac.destory) {
				waitingDelete.Add(ac.GetInstanceID());
			} else if (ac.enable) {
				ac.Update();
			}
		}
		foreach(int key in waitingDelete) {
			SSAction ac = actions[key];
			actions.Remove(key);
			DestroyObject(ac);
		}
		waitingDelete.Clear();
	}

	public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager) {
		action.gameobject = gameobject;
		action.transform = gameobject.transform;
		action.callback = manager;
		waitingAdd.Add(action);
		action.Start();
	}


	//回调函数
	public void SSActionEvent(SSAction source){
		if (source is CCMoveToActions) {
			diskNumber--;
			source.gameobject.SetActive (false);
		}
	}

	public void StartThrow(GameObject disk)  {  
		CCMoveToActionFactory cf = Singleton<CCMoveToActionFactory>.Instance;  
		 RunAction(disk, cf.GetSSAction(), (ISSActionCallback)this);    
	}  

	public int getDiskNumber()  
	{  
		return diskNumber;  
	}  

	public void setDiskNumber(int num)  
	{  
		diskNumber = num;  
	} 

}
