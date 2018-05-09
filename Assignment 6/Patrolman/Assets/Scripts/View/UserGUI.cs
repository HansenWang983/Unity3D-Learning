using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class UserGUI : MonoBehaviour 
{  
	private UserAction action;  

    private GUIStyle score_style = new GUIStyle();
    private GUIStyle text_style = new GUIStyle();
    private GUIStyle over_style = new GUIStyle();

	void Start () {  
		action = SSDirector.getInstance().currentSceneController as UserAction; 
		text_style.normal.textColor = new Color(0, 0, 0, 1);
        text_style.fontSize = 16;
        score_style.normal.textColor = new Color(1,0.92f,0.016f,1);
        score_style.fontSize = 16;
        over_style.fontSize = 25;
	}  

    void Update()
    {
        float translationX = Input.GetAxis("Horizontal");
        float translationZ = Input.GetAxis("Vertical");
        action.movePlayer(translationX, translationZ);
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 5, 200, 50), "分数:", text_style);
        GUI.Label(new Rect(55, 5, 200, 50), action.getScore().ToString(), score_style);
        if(action.getWin()){
			over_style.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 100, 100, 100), "恭喜胜利", over_style);
			over_style.normal.textColor = Color.yellow;
			GUI.Label(new Rect(Screen.width / 2 - 80, Screen.width / 2 - 70, 100, 100), "你的最终分数: "+action.getScore().ToString(), over_style);
        }
        if(!action.getWin()&&action.getGameover())
        {
			over_style.normal.textColor = Color.black;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 100, 100, 100), "游戏结束", over_style);
			over_style.normal.textColor = Color.yellow;
			GUI.Label(new Rect(Screen.width / 2 - 80, Screen.width / 2 - 70, 100, 100), "你的最终分数: "+action.getScore().ToString(), over_style);
        }
    }
}  