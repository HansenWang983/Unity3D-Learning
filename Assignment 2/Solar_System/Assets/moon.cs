using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moon : MonoBehaviour {
	public Transform earth;  // 地球
	public float radius;  // 地月距离
	public float ry, rz; 
	public float speed;
	private Vector3 axis;  // 法线

	private GameObject shadow;  // 地球的影子空对象

	void Start () {
		shadow = new GameObject ();
		shadow.transform.position = earth.position;
		transform.parent = shadow.transform;  // 月球是影子地球的子对象
		transform.localPosition = new Vector3 (radius, 0, 0);  // 设置月球相对于影子地球的距离

		axis = new Vector3 (0, ry, rz);
	}

	void Update () {
		shadow.transform.position = earth.position;  // 时刻保持影子对象与地球同步
		shadow.transform.Rotate (axis, speed*Time.deltaTime);  // 影子对象自转，月球就会跟着旋转。
	}
}