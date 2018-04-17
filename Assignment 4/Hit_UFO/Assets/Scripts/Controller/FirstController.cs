using System;  
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public enum GameState { ROUND_START, ROUND_FINISH, RUNNING, END, START};

public class FirstController : MonoBehaviour, ISceneController, UserAction {  


	public SceneActionManager actionManager { get; set; }  //动作管理器

	public ScoreRecorder scoreRecorder { get; set; }  //积分管理器

	public Queue<GameObject> diskQueue = new Queue<GameObject>();  //每回合抛出的飞碟

	private int disk_number;  //抛出的飞碟总数

	private int currentRound = -1;  //当前回合

	public int round = 3;  //回合总数

	private float time = 0;  //抛出时间间隔

	private GameState gameState = GameState.START;  //当前游戏状态

	void Awake () {  
		SSDirector director = SSDirector.getInstance();  
		director.currentSceneController = this;  
		disk_number = 10;  
		this.gameObject.AddComponent<ScoreRecorder>();  
		this.gameObject.AddComponent<DiskFactory>();  
		scoreRecorder = Singleton<ScoreRecorder>.Instance;  
		director.currentSceneController.LoadResources();  
	}  

	private void Update() {  
		//每回合结束
		if (actionManager.diskNumber == 0 && gameState == GameState.RUNNING) {
			if (currentRound == 2)
				gameState = GameState.END;
			else
				gameState = GameState.ROUND_FINISH;  
			diskQueue.Clear ();
		}

		//每回合开始
		if (actionManager.diskNumber == 0 && gameState == GameState.ROUND_START) {  
			currentRound = (currentRound + 1) % round;  //0,1,2
			NextRound();  
			actionManager.diskNumber = disk_number;  
			gameState = GameState.RUNNING;  
		}  

		//设置每次时间间隔为1s
		if (time > 0.5)  {  
			ThrowDisk();  
			time = 0;  
		}  
		else  
			time += Time.deltaTime;  
	}  

	//从工厂加载disk_number个飞碟，并入队
	private void NextRound()  {  
		DiskFactory df = Singleton<DiskFactory>.Instance;  
		for (int i = 0; i < disk_number; i++)  {  
			diskQueue.Enqueue(df.GetDisk(currentRound));  
		}  
//		actionManager.Fly(diskQueue);  
	}  

	//每隔0.5s抛出一个飞碟
	void ThrowDisk()  {  
		if (diskQueue.Count != 0) {  
			GameObject disk = diskQueue.Dequeue();  

			//飞碟出现的位置
			Vector3 position = new Vector3(0, 0, 0);  
			float y = UnityEngine.Random.Range(0f, 2f);  
			position = new Vector3(-disk.GetComponent<DiskData>().direction.x * 7, y, 0);  
			disk.transform.position = position;  

			actionManager.Fly(disk);  //抛出飞碟

			disk.SetActive(true);  
		}  

	}  
	//击中飞碟
	public void hit(Vector3 pos) {  
		Ray ray = Camera.main.ScreenPointToRay(pos);  

		RaycastHit[] hits;  
		hits = Physics.RaycastAll(ray);  
		for (int i = 0; i < hits.Length; i++)  
		{  
			RaycastHit hit = hits[i];  

			if (hit.collider.gameObject.GetComponent<DiskData>() != null)  
			{  
				scoreRecorder.Record(hit.collider.gameObject);  

				//飞碟被击中，使其落地，然后被回收
				hit.collider.gameObject.transform.position = new Vector3(0, -4, 0);  
			}  
		}  
	}  

	private void OnGUI(){
		if (gameState == GameState.END) {
			GameOver ();
		}

	}
		
	public void LoadResources()  {  
		Instantiate(Resources.Load<GameObject>("Prefabs/Ground"));  
	}  

	public void GameOver() {
		GUIStyle button_style = new GUIStyle {
			fontSize=30,
			fontStyle=FontStyle.Bold
		};
		button_style.normal.textColor = Color.red;
		GUIStyle score_style = new GUIStyle {
			fontSize=30,
			fontStyle=FontStyle.Bold
		};

		GUI.Label(new Rect(Screen.width / 2 -50, Screen.height / 2 - 200, 100, 50), "GAMEOVER",button_style); 

		GUI.Label(new Rect(Screen.width / 2 + 200, Screen.height / 2-150 , 100, 50), "Your final Score: "+this.GetScore().ToString(),score_style);  
	}  

	public int GetScore()  {  
		return scoreRecorder.score;  
	}  

	public GameState getGameState() {  
		return gameState;  
	}  

	public int getGameRound() {  
		return currentRound+1;  
	}  

	public void setGameState(GameState gs)  {  
		gameState = gs;  
	}  
}  