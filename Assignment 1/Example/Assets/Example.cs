using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		Debug.Log ("Start");

		//initialization of table and chair
		GameObject chair = GameObject.CreatePrimitive (PrimitiveType.Cube);
		chair.name = "chair1";
		chair.transform.position = new Vector3 (2, 0, 0);
		chair.transform.parent = this.transform;
		chair.transform.localScale = new Vector3 (1, 0.1f, 1);
		GameObject anotherchair1 = Instantiate (chair);
		anotherchair1.name = "chair2";
		anotherchair1.transform.position = new Vector3 (-2, 0, 0);
		anotherchair1.transform.parent = this.transform;
		GameObject anotherchair2= Instantiate (chair);
		anotherchair2.name = "chair3";
		anotherchair2.transform.position = new Vector3 (0, 0, 2);
		anotherchair2.transform.parent = this.transform;
		GameObject anotherchair3= Instantiate (chair);
		anotherchair3.name = "chair4";
		anotherchair3.transform.position = new Vector3 (0, 0, -2);
		anotherchair3.transform.parent = this.transform;


	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
