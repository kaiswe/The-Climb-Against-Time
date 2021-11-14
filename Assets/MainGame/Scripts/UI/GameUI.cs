using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* GameUI class: Class that handles the displaying of UI components and their relative stored or new variables */
public class GameUI : MonoBehaviour 
{
	public Text healthText;
	public Text timeText;
	public Text livesText;

	void Update () 
	{
		
	}
	/* SetGameUI(): Method to set the components on the in game UI and their relative variables */
	public void SetGameUI(int health, float time, int lives)
	{
		healthText.text = "Health: " + health;
		timeText.text = "Timer: " + time;
		livesText.text = "Lives: " + lives;
	}
}
