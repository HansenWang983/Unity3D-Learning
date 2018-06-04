using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGUI : MonoBehaviour {

	public RectTransform bgBar;
	public RectTransform bloodBar;
	public int reduceBlood = 0;
	void start(){
		bgBar.GetComponent<RectTransform>().Rotate(new Vector3 (0,180,0));
		bloodBar.GetComponent<RectTransform>().Rotate(new Vector3 (0,180,0));
	}

	void Update()
	{
		this.transform.LookAt (Camera.main.transform.position);
		bloodBar.GetComponent<RectTransform>().Right(reduceBlood);
	}

	private void OnGUI()
	{
		if (GUI.Button (new Rect (Screen.width/2-140, Screen.height/2-100, 70, 30), "Add")) {
			reduceBlood -= 10;
			if (reduceBlood < 0) {
				reduceBlood = 0;
			}
		}
		if (GUI.Button (new Rect (Screen.width/2+70, Screen.height/2-100,70, 30), "Reduce")) {
			reduceBlood += 10;
			if (reduceBlood > 200) {
				reduceBlood = 200;
			}
		}
	}
}
