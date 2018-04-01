using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
	private UserAction action;
	public int status = 0;
	GUIStyle style;
	GUIStyle buttonStyle;
	private string des="Cube represents Priest\n" +
		"Sphere represents Devil\n" +
		"One boat can load two characters at most\n";

	void Start() {
		action = SSDirector.getInstance ().currentSceneController as UserAction;

		style = new GUIStyle();
		style.fontSize = 40;
		style.alignment = TextAnchor.MiddleCenter;

		buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 30;


	}
	void OnGUI() {
		GUIStyle Box_Style = new GUIStyle {
			fontSize=30,
			fontStyle=FontStyle.Bold
		};
		//
		Box_Style.normal.textColor = Color.black;
		//
		GUI.Box(new Rect(Screen.width/2-100, Screen.height/2-150, 100, 50),"Priest and Devil",Box_Style);

		GUIStyle description = new GUIStyle {
			fontSize=15,
//			border = new RectOffset();
		};

		description.normal.textColor = Color.blue;


		GUI.Label (new Rect (Screen.width / 2 - 400, Screen.height / 2 - 120, 100, 50), des, description);

//		GUI.Button (new Rect (Screen.width / 2 -300, Screen.height /2 +100 , 100, 50), "On");
//
//		GUI.Button (new Rect (Screen.width / 2 +200 ,Screen.height /2 +100 , 100, 50), "On");
//


		if (status == 1) {
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-85, 100, 50), "Gameover!", style);
			if (GUI.Button(new Rect(Screen.width/2-70, Screen.height/2, 140, 70), "Restart", buttonStyle)) {
				status = 0;
				action.restart ();
			}
		} else if(status == 2) {
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-85, 100, 50), "You win!", style);
			if (GUI.Button(new Rect(Screen.width/2-70, Screen.height/2, 140, 70), "Restart", buttonStyle)) {
				status = 0;
				action.restart ();
			}
		}
	}

	CharacterController characterController;

	public void setController(CharacterController _characterCtrl) {
		characterController = _characterCtrl;
	}
		
	void OnMouseDown() {
		if (gameObject.name == "boat") {
			action.moveBoat ();
		} else {
			action.characterIsClicked (characterController);
		}
	}
}