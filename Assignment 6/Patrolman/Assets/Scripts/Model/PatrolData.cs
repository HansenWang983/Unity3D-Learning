using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class PatrolData : MonoBehaviour {  
    public bool follow_player = false;    //是否跟随玩家
    public GameObject player;             //玩家游戏对象
    public Vector3 start_position;        //当前巡逻兵初始位置   
}  