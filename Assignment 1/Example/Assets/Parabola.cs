using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Parabola : MonoBehaviour {

	public float MoveSpeed ;
	public Vector3 vector =new Vector3(-1,0.9f,0);
//	public Vector3 vector2 =new Vector3(-1,-0.9f,0);

//	Vector3 Target = new Vector3(2, 2, 2);  
	public float time = 0;
	void start(){
//			gameObject.GetComponent<Rigidbody>().velocity = vector * MoveSpeed;  
		
	}

	void Update () {  
		if (time < 0.5) {
			gameObject.transform.localPosition = new Vector3 (
				Mathf.Lerp (transform.position.x, vector.x, MoveSpeed * Time.deltaTime),  
				Mathf.Lerp (transform.position.y, vector.y, MoveSpeed * Time.deltaTime),  
				Mathf.Lerp (transform.position.z, vector.z, MoveSpeed * Time.deltaTime));  
		}
		time += Time.deltaTime;
	}  
}
	