using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class UserGUI : MonoBehaviour  
{  
	//与之前一样，UserAction代表用户的动作
	//接口在Interface中，实现在场记里
	private UserAction action;  
	bool isStart = false;  

	void Start () {  
		action = SSDirector.getInstance().currentSceneController as UserAction; 
	}  

	private void OnGUI()
	{  
		GUIStyle score_style = new GUIStyle {
			fontSize = 30,
			fontStyle = FontStyle.Bold
		};

		GUIStyle text_style = new GUIStyle {
			fontSize = 30,
			fontStyle = FontStyle.Bold
		};

//		GUIStyle button_style = new GUIStyle {
//			fontSize=30
//		};

//		Debug.Log (action.getMode ());
			
		if (action.getMode () == ActionMode.NOTSET) {  
			if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height / 2 - 300, 100, 50), "MOTION")) {  
				action.setMode (ActionMode.KINEMATIC);  
			}  
			if (GUI.Button (new Rect (Screen.width / 2 + 60, Screen.height / 2 - 300, 100, 50), "PHYSIC")) {  
				action.setMode (ActionMode.PHYSIC);  
			}  
		} else {
			//鼠标左键点击
			if (Input.GetButtonDown ("Fire1")) {
				Vector3 pos = Input.mousePosition;  
				action.hit (pos);  
			}  

			if (isStart && action.getGameState () == GameState.RUNNING)
				GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 300, 100, 50), "Round " + action.getGameRound (), text_style);

			if (action.getGameState () != GameState.END)
				GUI.Label (new Rect (Screen.width / 2 + 200, Screen.height / 2 - 150, 100, 50), "Score: " + action.GetScore ().ToString (), score_style);  

			if (!isStart && GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 300, 100, 50), "Start")) {  
				isStart = true;  
				action.setGameState (GameState.ROUND_START);  
			}  

			if (isStart && action.getGameState () == GameState.ROUND_FINISH && GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 300, 100, 50), "Next Round")) {  
				action.setGameState (GameState.ROUND_START);  
			}  
		}  
	}
}  