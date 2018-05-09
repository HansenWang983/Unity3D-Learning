using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class ScoreRecorder : MonoBehaviour {  

    public FirstController sceneController;

	public int score = 0;  

	void Start () {  
		sceneController = (FirstController)SSDirector.getInstance().currentSceneController;
        sceneController.scoreRecorder = this;
	}  

	public void Record()  { 
		score++;
	}  

	public int getScore(){
		return score;
	}

	public void Reset() {   
		score = 0;  
	}  
}