using System;  
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class FirstController : MonoBehaviour, ISceneController, UserAction {  

	public PatrolActionController actionController { get; set; }  //动作管理器

	public ScoreRecorder scoreRecorder { get; set; }  //积分管理器

	public GameFactory gf;

	public GameObject playerObj;

	public List<GameObject> patrols;

	private bool isGameOver = false;

	private bool isWin = false;

	public float player_speed = 3;  

	public float rotate_speed = 120f;                             

	void OnEnable(){
		EventManager.OnScore += AddScore;
		EventManager.OnGameOver += GameOver;  
	}

	void OnDisable(){
		EventManager.OnScore -= AddScore; 
		EventManager.OnGameOver += GameOver; 
	}

	void Awake(){
		SSDirector director = SSDirector.getInstance();  
		director.currentSceneController = this;  

		scoreRecorder = this.gameObject.AddComponent<ScoreRecorder>();
		actionController = this.gameObject.AddComponent<PatrolActionController>();

		gf = Singleton<GameFactory>.Instance;
		
		director.currentSceneController.LoadResources(); 

	}


	void Update(){	
        if(!isGameOver&&playerObj.transform.position.x>=10&&playerObj.transform.position.z<=-13.3){
        	Win();
        }
	}

	public void movePlayer(float translationX, float translationZ)
	{
	    if(!isGameOver)
	    {
	        if (translationX != 0 || translationZ != 0)
	        {
	            playerObj.GetComponent<Animator>().SetBool("run", true);
	        }
	        else
	        {
	            playerObj.GetComponent<Animator>().SetBool("run", false);
	        }
	        //移动和旋转
	        playerObj.transform.Translate(0, 0, translationZ * player_speed * Time.deltaTime);
	        playerObj.transform.Rotate(0, translationX * rotate_speed * Time.deltaTime, 0);
	        //防止碰撞带来的移动
	        if (playerObj.transform.localEulerAngles.x != 0 || playerObj.transform.localEulerAngles.z != 0)
	        {
	            playerObj.transform.localEulerAngles = new Vector3(0, playerObj.transform.localEulerAngles.y, 0);
	        }
	        if (playerObj.transform.position.y != 0)
	        {
	            playerObj.transform.position = new Vector3(playerObj.transform.position.x, 0, playerObj.transform.position.z);
	        }     
	    }
	}

	public int getScore(){
		return scoreRecorder.getScore();
	}

	public bool getGameover(){
		return isGameOver;
	}

	void AddScore(){
		scoreRecorder.Record ();
	}

	void Win(){
		isWin = true;
		GameOver();
	}

	public bool getWin(){
		return isWin;
	}

	void GameOver(){
		isGameOver = true;
		actionController.DestroyAllAction();
		for (int i = 0; i < patrols.Count; i++)
        {
            patrols[i].GetComponent<Animator>().SetBool("run", false);
        }
	}	

	public void LoadResources()  {  
		Instantiate(Resources.Load<GameObject>("Prefabs/Plane"), Vector3.zero, Quaternion.identity); 
		playerObj = Instantiate(Resources.Load<GameObject>("Prefabs/Player"), new Vector3(-10,0,-10), Quaternion.identity) as GameObject; 
		playerObj.tag = "Player"; 
		Debug.Log (gf);
		patrols = gf.GetPatrols();
		for (int i = 0; i < patrols.Count; i++)
        {
            actionController.GoPatrol(patrols[i]);
        }
	}  
}  