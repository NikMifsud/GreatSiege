using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	//This script changes the state of the game, used mostly in the PlayerControl Script, the Spawning Scripts and the PowerUp scripts
	//gameState 0 = can select anything
	//gamestate 1 = moving a unit
	//game state 3 = spawning a unit/powerups


	public int gameState; 

	void Start(){

	}
	
	// Change the state of the game
	public void ChangeState(int _newState)
	{
		gameState = _newState;
	}
}