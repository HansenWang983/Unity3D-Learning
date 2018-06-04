using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 

public class UGUI2 : MonoBehaviour {

	public Slider HPStrip;    //添加血条Slider的引用
    public int HP;

	void Start () {
		HPStrip.value = HPStrip.maxValue = HP;    //
	}

	void OnGUI(){
		
		
		if (GUI.Button (new Rect (Screen.width / 2 - 140, Screen.height / 2 - 100, 70, 30), "Add")) {
			HP += 10;
			if (HP < 0) {
				HP = 0;
			}
			HPStrip.value = HP;    //
		}
		if (GUI.Button (new Rect (Screen.width / 2 + 70, Screen.height / 2 - 100, 70, 30), "Reduce")) {
			HP -= 10;
			if (HP > 200) {
				HP = 200;
			}
			HPStrip.value = HP;    //
		}
	}
}
