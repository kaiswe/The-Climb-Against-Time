using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	/* GameLogic class: Class that dictates how UI functions work
	 * and how the program stores and reaccesses UI components when saving and loading old game data */
	public class GameLogic : MonoBehaviour 
	{
		//public GameObject itemPrefab;
		private float _timeLeft;
		private static int playerHealth;
		private static int playerLives;
		private GameData _gd;
		private int spikeDamage = 1;
		private int gremlinDamage = 2;
		public Transform savePoint;
		public CharacterController controller;

		/* On program start, call init function which instantiates the gameData to use for this program runtime */
		void Start () 
		{
			SetGamesData(GameManager.instance.gameData);
		}
		
		void Update () 
		{
			/* Countdown the timer from specified preset time */
			_timeLeft  -= Time.deltaTime;
			
			/* set the _timeLeft variable to equal the preset game time length
			 * preset game time length accessed through the game manager's instance */
			 GameManager.instance.timeRemaining = _timeLeft;

			 /* Every frame update game's relevant data about the player */
			 _gd.playerHealth = playerHealth;
			 _gd.playerLives = playerLives;
			 _gd.timeLeft = _timeLeft;

			 /* set to below or equal to 0. Collisions can occur quite rapidly and skip 0 if fast enough, causing player invincibility */
			 if(_gd.playerHealth <= 0)
			 {
				 /* Disable the controller if the player's transform position needs to be updated */
				 controller.enabled = false;
				 Respawn();
				 /*Reset player's health to full on respawn to default */
				 playerHealth = 3;
				 /* Subtract one life on player health depletion */
				 playerLives -= 1;
				 return;
			 }
			 /* If the player's lives reach 0 or the timer reaches 0 */
			 if(_gd.playerLives == 0 || _timeLeft == 0.0f)
			 {
				 /* Instaniate the Game Over screen and sequence */
				 GameManager.instance.uiManager.OnGameOver();
			 }
			 /*re-enable the controller so the player can move again */
			 controller.enabled = true;
		}

		/* SetGamesData(): Method that instantiates on level start what the UI's components, their relative values are and the player's saved position within the level */
		public void SetGamesData(GameData g)
		{
			/* set the determined game data to this current program runtime's game data */
			_gd = g;
			/* set the saved or new UI components to this current game instance's UI variables */
			_timeLeft = g.timeLeft;
			playerHealth = g.playerHealth;
			playerLives = g.playerLives;
			savePoint.position = g.spawnPoint;
			transform.position = savePoint.position;
			/* Disable character controller on new runtime instantiation
			 * re-enables on update loop */
			controller.enabled = false;
		}

		/* SaveCheckPoint(): Method that save's the player's checkpoint location for later access */
		public void SaveCheckPoint(Vector3 checkpoint)
		{
			/* Set the checkpoint location to the save points position */
			savePoint.position = checkpoint;
			/* Set the save point of the player to the game's data */
			_gd.spawnPoint = savePoint.transform.position;
		}

		/* TakeDamage(): Method that reduces the player's health points depending on what the player collided with */
		public void TakeDamage(int damage)
    	{
         playerHealth -= damage;
    	}

		/* Respawn(): Respawns the player based on the last saved player position */
		void Respawn()
		{
			/* Set the player's current position to the position stored within the game's save point */
			transform.position = savePoint.position;
			/* Enable the character controller since it was disabled on player death
			 * done so the player doesnt get frozen on in game position change */
			controller.enabled = true;
			
		}

		/*  OnTriggerEnter(): Method that handles all player interactions with various assets within the level */
 		void OnTriggerEnter(Collider col)
    	{
        	if (col.tag == "Spike")
        	{
            	TakeSpikeDamage();
        	}
        	if (col.tag == "Gremlin")
        	{
            	TakeGremlinDamage();
        	}
			if(col.tag == "Trophy")
			{
				YouWin();
			}
			if(col.tag == "Respawn")
			{
				Debug.Log("Saved");
				/* Set the checkpoint to the collided objects position */
				SaveCheckPoint(col.transform.position);
				/* Destroy the object after collision */
				Destroy(col.gameObject);
			}
    	}

		/* TakeSpikeDamage(): Sets the amount of damage the player takes if colliding with a spike trap */
    	void TakeSpikeDamage()
    	{
        Debug.Log("Took Spike damage!");
        TakeDamage(spikeDamage);

    	}
		/* TakeGremlinDamage(): Sets the amount of damage the player takes if colliding with a gremlin */
    	void TakeGremlinDamage()
    	{
			Debug.Log("Took Gremlin damage!");
			TakeDamage(gremlinDamage);
    	}

		/* YouWin(): Method that sets the win screen on player completing the level */
		void YouWin()
		{
			/* Call the ui manager class through the game manager to set the win condition to fulfilled */
			GameManager.instance.uiManager.YouWin();
		}

	}

