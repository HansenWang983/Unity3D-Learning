using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystick : MonoBehaviour {

	public float speedx = 10.0f;
	public float speedy = 10.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float translationY = Input.GetAxis ("Vertical") * speedy;
		float translationX = Input.GetAxis ("Horizontal") * speedx;
		translationX *= Time.deltaTime;
		translationY *= Time.deltaTime;
		transform.Translate (0, translationY, 0);
		transform.Translate (translationX, 0, 0);
		if (Input.GetButtonDown ("Fire1")) {
			Debug.Log ("Fired Pressed");
		}
	}
}
