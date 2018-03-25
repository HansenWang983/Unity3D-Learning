using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour {


	public GameObject table;
	// Use this for initialization
	void Start () {
		GameObject tableClone = Instantiate (table);
		tableClone.transform.position = new Vector3 (3, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
