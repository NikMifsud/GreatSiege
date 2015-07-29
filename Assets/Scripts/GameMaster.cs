using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	
	public int gameState; // In this state, the code is waiting for : 0 = Piece selection, 1 = Piece animation, 2 = Player2/AI movement, 3 = Spawning Unit
	//private int activePlayer = 0; // 0 = Player1, 1 = Player2, 2 = AI, to be used later

	void Start(){

	}
	
	// Change the state of the game
	public void ChangeState(int _newState)
	{
		gameState = _newState;
	}
}