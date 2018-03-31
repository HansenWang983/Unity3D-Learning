using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {


	public Transform sun;
	public Transform earth;
	public Transform moon;

	// Use this for initialization
	void Start () {
		Debug.Log ("Start");
		sun.position = Vector3.zero;
		earth.position = new Vector3(6,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		earth.transform.RotateAround (Vector3.zero,Vector3.up,5*Time.deltaTime);
		earth.transform.Rotate (Vector3.up*30*Time.deltaTime);
	}
}
