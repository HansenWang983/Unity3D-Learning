using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class DiskFactory : MonoBehaviour{  

	public GameObject diskObj;  //保存飞碟游戏对象

	private Dictionary<int, DiskData> used = new Dictionary<int, DiskData>();  
	private List<DiskData> free = new List<DiskData>();  
	private List<int> wait = new List<int>();  	

	private void Awake()  {  
		//实例化预制
		diskObj =Instantiate(Resources.Load<GameObject>("Prefabs/Disk"), Vector3.zero, Quaternion.identity) as GameObject; 
		diskObj.SetActive(false);  	
	}  

	private void Update(){
		foreach (var tmp in used.Values) {  

			if (!tmp.gameObject.activeSelf)  
			{  
				wait.Add(tmp.GetInstanceID());  
			}  
		}  

		foreach (int tmp in wait)  {  
			FreeDisk(used[tmp].gameObject);  
		}  
		wait.Clear();  

	}

	//判断free有没有空余飞碟，有则使用，无则生成
	//然后进行
	public GameObject GetDisk(int round,ActionMode mode)  
	{  
		GameObject new_disk = null;  
		if (free.Count > 0)  
		{  
			new_disk = free[0].gameObject;  
			free.Remove(free[0]);  
		}  
		else  
		{  
			new_disk = Instantiate<GameObject>(diskObj, new Vector3(2,-4,0), Quaternion.identity);  
			new_disk.AddComponent<DiskData>();  
		}  

		/** 
         * 以下几句代码是用来随机生成飞碟的颜色的，并根据回合数来限制飞碟可用的颜色 
         * 第一回合智能生成黄色的飞碟，第二回合飞碟可以有黄色和红色，第三回合黄，红 
         * 黑三种颜色的飞碟都可以出现，
		*/
        
        //frequency表示每round出现飞碟的频率
        int frequency = 0;  
		if (round == 1) frequency = 100;  
		if (round == 2) frequency = 250;  

		//随机生成飞碟的颜色
		int selectedColor = Random.Range(frequency, round * 499);  

		if (selectedColor > 500)  //250-998
			round = 2;  
		else if (selectedColor > 300)  //100-499
			round = 1;  
		else  
			round = 0;  

		
        //根据回合数来生成相应的飞碟 

		switch (round){  		
			case 0:  
				{  
					new_disk.GetComponent<DiskData>().color = Color.yellow;  
					new_disk.GetComponent<DiskData>().speed = 8.0f;  
					float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -0.5f : 0.5f;  
					new_disk.GetComponent<DiskData>().direction = new Vector3(RanX, 1, 0);  
					new_disk.GetComponent<Renderer>().material.color = Color.yellow;  
					break;  
				}  
			case 1:  
				{  
					new_disk.GetComponent<DiskData>().color = Color.red;  
					new_disk.GetComponent<DiskData>().speed = 9.0f;  
					float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -0.7f : 0.7f;  
					new_disk.GetComponent<DiskData>().direction = new Vector3(RanX, 1, 0);  
					new_disk.GetComponent<Renderer>().material.color = Color.red;  
					break;  
				}  
			case 2:  
				{  
					new_disk.GetComponent<DiskData>().color = Color.black;  
					new_disk.GetComponent<DiskData>().speed = 10.0f;  
					float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ?-0.9f : 0.9f;  
					new_disk.GetComponent<DiskData>().direction = new Vector3(RanX, 1, 0);  
					new_disk.GetComponent<Renderer>().material.color = Color.black;  
					break;  
				}  
		}  

		//判断是否为物理模式决定添加刚体
		if (mode == ActionMode.PHYSIC) {
			new_disk.AddComponent<Rigidbody> ();
		}

		used.Add(new_disk.GetComponent<DiskData>().GetInstanceID(),new_disk.GetComponent<DiskData>());  
//		new_disk.SetActive(true);  
		new_disk.name = new_disk.GetInstanceID().ToString();  
		return new_disk;  
	}  

	public void FreeDisk(GameObject disk)  
	{  
		DiskData tmp = null;  
		foreach (DiskData i in used.Values)  
		{  
			if (disk.GetInstanceID() == i.gameObject.GetInstanceID())  
			{  
				tmp = i;  
			}  
		}  
		if (tmp != null) {  
			tmp.gameObject.SetActive(false);  
			free.Add(tmp);  
			used.Remove(tmp.GetInstanceID());  
		}  
	}  

}  