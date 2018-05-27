using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitParticle : MonoBehaviour {

	public ParticleSystem particleSystem;
	private ParticleSystem.Particle[] particleRing;//粒子工厂
	private ParticlePosition[] particles;//粒子位置

	public int particleNum = 10000;//粒子数量
	public float size = 0.03f; //粒子大小
	public float minRadius = 5.0f;//粒子轨道最小半径
	public float maxRadius = 10.0f;//粒子轨道最大半径

	public int level = 5;//子部分的数量
	private bool collect = false;
	public float speed = 0.1f;
	public float pingPong = 0.02f;

	void Start () {
		particleRing = new ParticleSystem.Particle[particleNum];
		particles = new ParticlePosition[particleNum];

    	particleSystem.startSpeed = 0;            // 粒子位置由脚本控制，初始为0  
	    particleSystem.startSize = size;          // 设置粒子大小  
    	particleSystem.loop = false;  			  // 不循环
    	particleSystem.maxParticles = particleNum;      // 设置最大粒子量  
    	particleSystem.Emit(particleNum);               // 发射粒子  
    	particleSystem.GetParticles(particleRing);  //获得粒子数组


		for (int i = 0; i < particleNum; i++) {

			minRadius = (maxRadius + minRadius) / 2;  
        	float minRate = Random.Range(1.0f, minRadius / maxRadius);  //最小半径随机扩大
        	float maxRate = Random.Range(minRadius / maxRadius, 1.0f);  //最大半径随机缩小
        	float radius = Random.Range(minRadius * minRate, maxRadius * maxRate); 

        	// 随机每个粒子的角度
        	float angle = Random.Range(0.0f, 360.0f);  
        	float theta = angle / 180 * Mathf.PI;   
        	// 随机每个粒子的游离起始时间  
        	float time = Random.Range(0.0f, 360.0f);

        	//保存位置信息
        	particles[i] = new ParticlePosition(radius,angle,time);

        	//初始化位置
			particleRing[i].position = new Vector3(particles[i].radius * Mathf.Cos(theta), 0.0f, particles[i].radius * Mathf.Sin(theta));
		}

		particleSystem.SetParticles(particleRing,particleNum);
	}


	void Update () {
		for(int i = 0;i < particleNum; i++) {
			//设置为level=5部分的粒子，能被2整除的部分逆时针旋转，否则顺时针
			if (i%2 == 0) {
				//逆时针旋转
				particles[i].angle += (i % level) * speed;
			} else {
				//顺时针旋转
				particles[i].angle -= (i % level) * speed;
			}
			//角度：0-359
			particles[i].angle = particles[i].angle % 360;
			float theta = particles[i].angle / 180 * Mathf.PI;
			//根据正弦余弦公式设置粒子位置
			particleRing[i].position = new Vector3(particles[i].radius * Mathf.Cos(theta), 0.0f, particles[i].radius * Mathf.Sin(theta));
		}
		particleSystem.SetParticles(particleRing, particleNum);
	}

	// public void Collect() {
	// 	collect = true;
	// }
	// public void outCollect() {
	// 	collect = false;
	// }
}