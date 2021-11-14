using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	/* UIManager class: Class that handles all player UI components and player interactibility */

	public class UIManager : MonoBehaviour 
	{
		public GameObject mainMenu;
		public GameObject gameMenu;
		public GameObject PauseScreen;
		public GameObject HowToPlayScreen;
		public GameObject instructionsMenu;
		public GameObject gameOverScreen;
		public GameObject winnerScreen;
		public GameUI gameUI;

		/* bool variable to determine if the game is paused or not
		 * depending on bool value and relative game conditions, pause or unpause */
		private bool isPaused;

		void Update () 
		{
			/* If player input is escape */
			if(Input.GetKeyDown(KeyCode.Escape) && !mainMenu.activeSelf && !winnerScreen.activeSelf)
			{
				/* if the game is not paused */
				if(!isPaused)
				{
					/* toggle the UI and pause the game */
					ToggleUI();
					Pause();
				}
				else
				{
					/* UnPause the game if previous conditions not met */
					UnPause();
				}
			}

		}

		/* ToggleUI(): Method that toggles the player UI on command */
		public void ToggleUI()
		{
			/* Set the current game menu active if it is not already active */
			gameMenu.SetActive(!gameMenu.activeSelf);
			
		}

		/* OnMainMenu(): First method that determine's what UI and game menu to display; depending on the current scene */
		public void OnMainMenu()
		{
			gameMenu.SetActive(false);
			instructionsMenu.SetActive(false);
			mainMenu.SetActive(true);
			/* Set the in-game UI to hidden if the game is on the main menu */
			gameUI.gameObject.SetActive(false);
		}

		public void OnMenuInstructions()
		{
			mainMenu.SetActive(false);
			gameMenu.SetActive(false);
			gameUI.gameObject.SetActive(false);
			instructionsMenu.SetActive(true);
		}

		/* OnGame(): Second method to determine what UI and game menu to display; depending on the current scene */
		public void OnGame()
		{
			gameMenu.SetActive(false);
			mainMenu.SetActive(false);
			/* Set the in-game UI to active if the game is within a game session */
			gameUI.gameObject.SetActive(true);
		}

		public void OnGameOver()
		{
			gameMenu.SetActive(true);
			gameOverScreen.SetActive(true);
			Pause();
		}

		public void YouWin()
		{
			gameMenu.SetActive(true);
			winnerScreen.SetActive(true);
			Pause();
		}

		/* Pause(): Method to pause the game depending on player input and relative conditions */
		public void Pause()
		{
			/* Freeze the game while the game is paused */
			Time.timeScale = 0;
			/* If the how to play screen is not active, set the pause screen to active
			 * this is done so the player cannot open the pause menu while the how to play screen is active
			 * this disables multiple screens from being active at once, and causing problems/confusion in-game */
			if(!HowToPlayScreen.activeSelf && !gameOverScreen.activeSelf && !winnerScreen.activeSelf)
			{
				PauseScreen.SetActive(true);
				/* set the paused boolean to true when it is paused */
				isPaused = true;
			}
			isPaused = true;

		}

		/* UnPause(): Method to unpause the game depending on player input and relative conditions */
		public void UnPause()
		{
			/* Return the game to normal when unpaused */
			Time.timeScale = 1;
			/* Set all relative in-game screens to hidden when the game is unpaused */
			PauseScreen.SetActive(false);
			HowToPlayScreen.SetActive(false);
			gameMenu.SetActive(false);
			gameOverScreen.SetActive(false);
			winnerScreen.SetActive(false);
			/* set the boolean to false */
			isPaused = false;
		}

		/* HowToPlayMenu(): Method to display in-game instructions screen for the player */
		public void HowToPlayMenu()
		{
			/* Set how to play screen to active on relative UI button click */
			HowToPlayScreen.SetActive(true);
			/* disable the pause screen */
			PauseScreen.SetActive(false);
			/* keep the game paused on screen change */
			Pause();
		}

	}
