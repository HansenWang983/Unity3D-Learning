using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	public delegate void ScoreEvent();
	public delegate void GameOverEvent();
	public static event ScoreEvent OnScore;
	public static event GameOverEvent OnGameOver;

	public void Escape()
	{
		if (OnScore != null)
		{
			OnScore();
		}
	}

	public void Over()
	{
		if(OnGameOver != null)
		{
			OnGameOver();
		}
	}
}
