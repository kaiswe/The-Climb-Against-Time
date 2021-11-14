using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

	/* GameManager(): Singleton class that handles all game data, when and how to access its relevant data */
	public class GameManager : MonoBehaviour 
	{
		private static GameManager gameSession;
		public UIManager uiManager;
		public string gameSceneName;
		public string menuSceneName;
		public string instructionsSceneName;
		public string saveFileName = "data.json";
		private GameData _gd;
		private bool playingGame;
		public float timeRemaining;
		public GameObject controller;
		public GameObject levelSpawn;


		/* GameManager(): Static method that allows access for any public variable or method within this class
		 * where accessing this class through this method returns any relevant data to this current game session */
		public static GameManager instance
		{
			get
			{
				/* returns the current game session and its relative variables/components stored in this class */
				return gameSession;
			}
		} 

		/* GameData(): Method that returns the current game data known about this game session */
		public GameData gameData
		{
			get
			{
				/* returns current game's data */
				return _gd;
			}
		}

		/* Awake(): Method instantiates on program start */
		void Awake()
		{
			/* If this current game session's game manager is not null
			 * meaning is the game running or not */
			if (gameSession == null)
			{
				/* current game session is this one */
				gameSession = this;
				/* Don't destroy this game manager on scene reloading or loading */
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				/* if the game session does not equal the current running session */
				if (gameSession != this)
				/* destroy the game manager */
					Destroy(gameObject);
			}
		}
		
		void Start()
		{
			/* load the main menu on game start */
			LoadMainMenu();
		}
		
		void Update () 
		{
			/* if user is currently playing the game */
			if (playingGame)
			{
				/* instantiate and set the game's ui with relative components and values */
				uiManager.gameUI.SetGameUI(_gd.playerHealth, timeRemaining, _gd.playerLives);
			}
			else
			{
			/* set the cursor to visible and lock it to the screen when the game is not being played and is on the main menu
			 * solves cursor issues that arrise during scene swapping and menu's during program runtime */
         	Cursor.visible = true;
         	Cursor.lockState = CursorLockMode.Confined;
			/* disable the in game menus when the main menu is the current scene, so the user cannot pause or cause other UI issues during program runtime */
			uiManager.gameMenu.SetActive(false);
			}
		}

		/* NewGame(): Method that creates a new game session on request */
		public void NewGame()
		{
			/* set all game data values to default */
			_gd = new GameData();
			/* load the game scene */
			LoadGameScene();
		}

		/* LoadMainMenu(): Load's the game's main menu */
		public void LoadMainMenu()
		{
			/* load the designated menu scene by it's preset name */
			SceneManager.LoadScene(menuSceneName);
			/* set only the relative UI component's be accessible and active if on the main menu */
			uiManager.OnMainMenu();
			playingGame = false;
		}

		/* LoadGameScene(): Load's the relevant game scene on user interaction */
		public void LoadGameScene()
		{
			playingGame = true;
			/* Load the designated game scene by its preset name */
			SceneManager.LoadScene(gameSceneName);
			/* set only the relative UI component's be accessible and active if in the game*/
			uiManager.OnGame();
		}

		/* LoadMainMenuInstructions(): Load the game's instructions scene if on the main menu */
		public void LoadMainMenuInstructions()
		{
			/* load the designated instructions scene by it's preset name */
			SceneManager.LoadScene(instructionsSceneName);
			/* set only the relative UI component's be accessible and active if on the instructions screen*/
			uiManager.OnMenuInstructions();

		}

		/* StartGameAt(): Method that instantiates the current game's data values based off the user's saved variables */
		void StartGameAt(GameData g)
		{
			/* Sets the loaded saved game's data to the current game sessions data */
			_gd = g; 
			LoadGameScene();
		}

		/* LoadGameData(): Method that handles the deserialization of relevant stored game data stored as strings within a file */
		public void LoadGameData()
		{
			/* Combine the identified strings into the file path to the stored game's data */
			string filePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
			Debug.Log(filePath);
			/* If the filepath exists within the identified location */
			if(File.Exists(filePath))
			{
				/* Read the stored game data as json and store into a string */
				string dataAsJson = File.ReadAllText(filePath); 
				/*Instantiate new game data object, pass the saved json data to JsonUtility and set the new data into this game data object */
				GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

				/* start the game with the relative saved data */
				StartGameAt(loadedData);
			}
			else
			{
				/* error handle incase the data cannot be found or loadad */
				Debug.LogError("Cannot load game data!");
			}
    
		}

		/* SaveGameData(): Serializes and saves the identified game data variables into a json file for later access */
		public void SaveGameData()
		{
			Debug.Log("Save");
			/* set current game's data into new game data object */
			GameData gd = _gd;
			/* Create new string, grab json data through JsonUtility and generate public fields of the game's data to store within the string */
			string dataAsJson = JsonUtility.ToJson(gd);
			/* Print the data being saved */
			Debug.Log(dataAsJson);
			/* Identify the file path to where the json file is stored*/
			string filePath = Application.streamingAssetsPath + "/" +  saveFileName;
			/* write all the saved string data onto the identified json file by first connecting to the file location, then writing it */
			File.WriteAllText (filePath, dataAsJson);
		}

	}

