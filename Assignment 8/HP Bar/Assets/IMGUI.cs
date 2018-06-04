using UnityEngine;
using System.Collections;

public class IMGUI : MonoBehaviour {

	//主摄像机对象
	private Camera camera;
	//主角对象
	public GameObject hero;
	//NPC模型高度
	float npcHeight;
	//红色血条贴图
	public Texture2D blood_red;
	//黑色血条贴图
	public Texture2D blood_black;
	//默认NPC血值
	private float HP = 100;

	void Start ()
	{
		
		hero = GameObject.FindGameObjectWithTag("Ethan");
	
		camera = Camera.main;
			
		npcHeight = 2;
		
	}

	void Update ()
	{
		transform.LookAt(hero.transform);

	}

	void OnGUI()
	{
		if (GUI.Button(new Rect (Screen.width/2-140, Screen.height/2-100, 70, 30), "Add"))
		{
			HP += 5f;
		}
		if (GUI.Button(new Rect(Screen.width/2+70, Screen.height/2-100,70, 30), "Reduce"))
		{
			HP -= 5f;
		}
		if (HP > 100f)
		{
			HP = 100f;
		}
		if (HP < 0.0f)
		{
			HP = 0.0f;
		}

		Vector3 worldPosition = new Vector3 (transform.position.x , transform.position.y + npcHeight,transform.position.z);

		Vector2 position = camera.WorldToScreenPoint (worldPosition);
	
		position = new Vector2 (position.x, Screen.height - position.y);


		Vector2 bloodSize = GUI.skin.label.CalcSize (new GUIContent(blood_red));

//		Debug.Log (bloodSize);
		float blood_width = blood_red.width * HP/100;

		GUI.DrawTexture(new Rect(position.x - (bloodSize.x/2),position.y - bloodSize.y ,bloodSize.x,bloodSize.y),blood_black);

		GUI.DrawTexture(new Rect(position.x - (bloodSize.x/2),position.y - bloodSize.y ,blood_width,bloodSize.y),blood_red);

	}
		
//	void OnMouseDown()
//	{
//		
//		if(HP >0)
//		{
//			HP -=5 ;
//		}
//
//	}
}