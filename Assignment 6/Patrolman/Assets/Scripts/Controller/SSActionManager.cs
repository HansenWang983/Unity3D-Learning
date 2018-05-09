using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour,IActionController,ISSActionCallback {

//	public FirstController sceneController;

	private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction> ();
	private List<SSAction> waitingAdd = new List<SSAction>();
	private List<int> waitingDelete = new List<int>();

//	protected void Start() {
//		sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
//		sceneController.actionController = this;
//	}

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
				ac.FixedUpdate();
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
	public void SSActionEvent(SSAction source,int intParam = 0, GameObject objectParam = null){
		if(intParam == 0)
        {
            //侦查兵跟随玩家
            PatrolFollowAction follow = PatrolFollowAction.GetSSAction(objectParam.gameObject.GetComponent<PatrolData>().player);
            this.RunAction(objectParam, follow, this);
        }
        else
        {
            //侦察兵按照初始位置开始继续巡逻
            PatrolAction move = PatrolAction.GetSSAction(objectParam.gameObject.GetComponent<PatrolData>().start_position);
			this.RunAction(objectParam, move, this);
			Singleton<EventManager>.Instance.Escape();
        }
	}

	public void DestroyAll()
    {
        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            ac.destory = true;
        }
    }
	
}
