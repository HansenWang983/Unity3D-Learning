using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class GameFactory : MonoBehaviour{  

	public GameObject patrolObj;  

	private List<GameObject> used = new List<GameObject>(); 

	private Vector3[] vec = new Vector3[9];  
		
	public List<GameObject> GetPatrols()  
	{  
		int[] pos_x = { -6, 4, 13 };
        int[] pos_z = { -4, 6, -13 };
        int index = 0;

        for(int i=0;i < 3;i++)
        {
            for(int j=0;j < 3;j++)
            {
                vec[index] = new Vector3(pos_x[i], 0, pos_z[j]);
                index++;
            }
        }
        for(int i=0; i < 9; i++)
        {		
        	patrolObj =Instantiate(Resources.Load<GameObject>("Prefabs/Patrol"), Vector3.zero, Quaternion.identity) as GameObject; 
            patrolObj.transform.position = vec[i];
            patrolObj.GetComponent<PatrolData>().start_position = vec[i];
            used.Add(patrolObj);
        }   
        return used;
	}  
}  