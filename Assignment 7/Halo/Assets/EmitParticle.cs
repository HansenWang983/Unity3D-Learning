using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitParticle : MonoBehaviour {

	public ParticleSystem particleSystem;
	private ParticleSystem.Particle[] particleRing;//粒子工厂
	
	private float[] particleAngle;//每个粒子的角度
	private float[] particleR;//每个粒子的半径
	private float[] particleTime;//每个粒子的游离时间
	public Gradient gradient;  //每个粒子的透明度

	public int particleNum = 10000;//粒子数量
	public float minRadius = 5.0f;//粒子轨道最小半径
	public float maxRadius = 10.0f;//粒子轨道最大半径
	public int level = 3;//子部分的数量
	public float speed = 0.02f;
	public float size = 0.10f;
	public float pingPong = 0.02f;

	void Start () {
		particleRing = new ParticleSystem.Particle[particleNum];
		particleAngle = new float[particleNum];
		particleR = new float[particleNum];
		particleTime = new float[particleNum];

		particleSystem.startSize = size;          // 设置粒子大小  
		particleSystem.maxParticles = particleNum;
		particleSystem.Emit(particleNum);
		particleSystem.GetParticles(particleRing);

		 // 初始化梯度颜色控制器  
	    GradientAlphaKey[] alphaKeys = new GradientAlphaKey[5];  
	    alphaKeys[0].time = 0.0f; alphaKeys[0].alpha = 1.0f;  
	    alphaKeys[1].time = 0.4f; alphaKeys[1].alpha = 0.4f;  
	    alphaKeys[2].time = 0.6f; alphaKeys[2].alpha = 1.0f;  
	    alphaKeys[3].time = 0.9f; alphaKeys[3].alpha = 0.4f;  
	    alphaKeys[4].time = 1.0f; alphaKeys[4].alpha = 0.9f;  
	    GradientColorKey[] colorKeys = new GradientColorKey[2];  
	    colorKeys[0].time = 0.0f; colorKeys[0].color = Color.white;  
	    colorKeys[1].time = 1.0f; colorKeys[1].color = Color.white;  
	    gradient.SetKeys(colorKeys, alphaKeys);  

		for (int i = 0; i < particleNum; i++) {
			float midR = (maxRadius + minRadius)/2;
			float rate1 = Random.Range(1.0f, midR / minRadius); //最小半径随机扩大
			float rate2 = Random.Range(midR / maxRadius, 1.0f); //最大半径随机缩小
			float r = Random.Range(minRadius*rate1, maxRadius*rate2);

			float angle = Random.Range(0.0f, 360.0f);
			float time = Random.Range(0.0f, 360.0f);
			particleAngle[i] = angle;
			particleR[i] = r;
			particleTime[i] = time;

			float theta = angle / 180 * Mathf.PI;
			particleRing[i].position = new Vector3(r * Mathf.Cos(theta), 0.0f, r * Mathf.Sin(theta));
		}
		particleSystem.SetParticles(particleRing,particleNum);
	}


	void Update () {
		for(int i = 0;i < particleNum; i++) {
			//设置为level=5部分的粒子，能被2整除的部分逆时针旋转，否则顺时针
			if (i%2 == 0) {
				//逆时针旋转
				particleAngle[i] += (i % level) * speed;
			} else {
				//顺时针旋转
				particleAngle[i] -= (i % level) * speed;
			}
			//透明度变化
			particleRing[i].color = gradient.Evaluate(particleAngle[i] / 360.0f);  
			//半径变化
			particleTime[i] += Time.deltaTime;  
			particleR[i] += Mathf.PingPong(particleTime[i] / minRadius / maxRadius, pingPong) - pingPong / 2.0f;  
			//角度：0-359
			particleAngle[i] = particleAngle[i] % 360;
			float theta = particleAngle[i] / 180 * Mathf.PI;
			//根据正弦余弦公式设置粒子位置
			particleRing[i].position = new Vector3(particleR[i] * Mathf.Cos(theta),  0.0f,particleR[i] * Mathf.Sin(theta));
		}
		particleSystem.SetParticles(particleRing, particleNum);
	}

}