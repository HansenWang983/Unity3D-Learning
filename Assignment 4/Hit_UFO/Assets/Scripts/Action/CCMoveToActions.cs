﻿using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class CCMoveToActions : SSAction {  

	float gravity;  //重力加速度

	float horizonSpeed;  //水平速度

	Vector3 direction;  //初始飞行方向

	float time;  //飞行时间

	//重写基类
	public override void Start () {  
		enable = true;  
		gravity = 9.8f;  
		time = 0;  
		horizonSpeed = gameobject.GetComponent<DiskData>().speed;  
		direction = gameobject.GetComponent<DiskData>().direction;  
	}  

	public override void Update () {  
		if (gameobject.activeSelf)  
		{  
			time += Time.deltaTime;  

			transform.Translate(Vector3.down * gravity * time * Time.deltaTime);  //竖直位移

			transform.Translate(direction * horizonSpeed * Time.deltaTime);  //水平位移

			if (this.transform.position.y < -3) //落地并回收  
			{  
				this.destory = true;  
				this.enable = false;  
				this.callback.SSActionEvent(this);  
			}  
		}  

	}  

	public static CCMoveToActions GetSSAction()  
	{  
		CCMoveToActions action = ScriptableObject.CreateInstance<CCMoveToActions>();  
		return action;  
	}  
}  