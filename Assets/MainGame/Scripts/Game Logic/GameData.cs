using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* GameData class: Class that handles initialization of the game's data */
[System.Serializable]
public class GameData
{
	public int playerHealth;
	public float timeLeft;
	public int playerLives;
	
	/* Player's spawn point on load game */
	public Vector3 spawnPoint;

	/* GameData(): Initializes default values for players UI components and position */
	public GameData()
	{
		playerHealth = 3;
		timeLeft = 100.0f;
		playerLives = 5;
		/* Default level starting position */
		spawnPoint = new Vector3(493.75f, 7.56f, 446.57f);
	}

}
