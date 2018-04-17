using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController {
	void LoadResources ();
}
	
public interface UserAction {  
	void hit(Vector3 pos);  
	void GameOver();  
	GameState getGameState();  
	void setGameState(GameState gs);  
	int GetScore();  
	int getGameRound();
}  