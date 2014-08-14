using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	
	/// <summary>
	/// Restart the level.
	/// </summary>
	public static void Restart(FlyAndMove player){
	
		if(player)
			player.Restart();
	}
	
	/// <summary>
	/// Closes the game.
	/// </summary>
	public void Update(){
		
		if(Input.GetKey(KeyCode.Escape))
			Application.Quit();
	}
}