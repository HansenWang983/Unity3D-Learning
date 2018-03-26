using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

//	public Texture2D icon;

	enum Player{A,B,C};

	private Player[,] status = new Player[3,3];

	private int turn = 1;

	private int button_height = 100,buttion_width=100;

	private float height = Screen.height * 0.5f - 50,width = Screen.width * 0.5f - 150;
	
	private int count = 0;
	// Use this for initialization
	void Start () {
		Debug.Log ("Start");
		Reset ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Update");
	}

	void Reset(){
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				status [i,j] = Player.C;
			}
		}
		count = 0;
	}

	void OnGUI(){

		Debug.Log("OnGui");

		// Make a background box
		GUI.Box (new Rect (width-25,height-125,350,550),"Tic-Tac-Toe");

		Player result=Check();

		if (GUI.Button (new Rect (width + 50, height - 75, 100, 50), "RESET"))
			Reset ();

		if (result == Player.A)
			GUI.Label (new Rect (width + 200, height - 75, 100, 50), "X win!");

		if(result == Player.B)
			GUI.Label (new Rect (width + 200, height - 75, 100, 50), "O win!");

		else if(result == Player.C && count==9)
			GUI.Label (new Rect (width + 200, height - 75, 100, 50), "Withdraw");
		

		for (int x = 0; x < 3; x++) {
			for (int y = 0; y < 3; y++) {
				if (status [x, y] == Player.A)
					GUI.Button (new Rect (width + x * buttion_width, height + y * button_height, buttion_width, button_height), "X");
				if(status [x, y] == Player.B)
					GUI.Button (new Rect (width + x * buttion_width, height + y * button_height, buttion_width, button_height), "O");
				if (GUI.Button (new Rect (width + x * buttion_width, height + y * button_height, buttion_width, button_height), "")) {
					if (turn == 1)
						status [x, y] = Player.A;
					else
						status [x, y] = Player.B;
					turn = -turn;
					count++;
				}
			}
		}
	}
		
	private Player Check(){
		for (int i=0; i<3; i++) {  
			if (status[i,0]!=Player.C && status[i,0]==status[i,1] && status[i,1]==status[i,2]) {  
				return status[i,0];  
			}  
		}  
		for (int j=0; j<3; ++j) {  
			if (status[0,j]!=Player.C && status[0,j]==status[1,j] && status[1,j]==status[2,j]) {  
				return status[0,j];  
			}  
		}  
		if (status[1,1]!=Player.C &&  
			status[0,0]==status[1,1] && status[1,1]==status[2,2] ||  
			status[0,2]==status[1,1] && status[1,1]==status[2,0]) {  
			return status[1,1];  
		}  
		return Player.C;  
	}
}
