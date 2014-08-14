using UnityEngine;
using System.Collections;

public class Debug : MonoBehaviour {
	
	public GUISkin mainSkin;
	public static bool showDebug;
	
	public void OnGUI(){
		
		// turn on and off
		if(!showDebug)
			if(GUI.Button(new Rect(Screen.width - 100f, 10f, 100f, 20f), "Hide")){
				print("Hide debugging messages");
				showDebug = true;
			}
		
		if(showDebug)
			if(GUI.Button(new Rect(Screen.width - 100f, 10f, 100f, 20f), "Show")){
				print("Show debugging messages");
				showDebug = false;
			}	
		
		if(showDebug){
			
			GUI.skin = mainSkin;
			GUI.contentColor = new Color(1f, 1f, 1f);
			
			// speed
			GUI.Label(new Rect(0.5f * Screen.width + 16f, Screen.height - 164f, 256f, 256f), "speed: " + Mathf.Round(FlyAndMove.Speed));
			
			// rotation
			GUI.Label(new Rect(0.5f * Screen.width + 176f, Screen.height - 164f, 256f, 256f), "local x:" + Mathf.Round(FlyAndMove.Rotation.x));
			GUI.Label(new Rect(0.5f * Screen.width - 288f, Screen.height - 174f, 96f, 20f), "global y: " + Mathf.Round(FlyAndMove.Rotation.y));
			GUI.Label(new Rect(0.5f * Screen.width - 288f, Screen.height - 164f, 96f, 20f), "local z: " + Mathf.Round(FlyAndMove.Rotation.z));
			
			// triggers
			GUI.Label(new Rect(0.5f * Screen.width - 144f, Screen.height - 164f, 256f, 256f), "groundtrigger: " + GroundTrigger.Triggered);
			GUI.Label(new Rect(0.5f * Screen.width, Screen.height - 184f, 256f, 256f), "sensor rear: " + RearTrigger.Triggered);
			GUI.Label(new Rect(0.5f * Screen.width, Screen.height -194f, 256f, 256f), "sensor front: "+ FrontTrigger.Triggered);
			GUI.Label(new Rect(0.5f * Screen.width, Screen.height - 204f, 256f, 256f), "sensor front up " + FrontUpTrigger.Triggered);
			
			// game over
			GUI.Label(new Rect(0.5f * Screen.width, Screen.height - 224f, 256f, 256f),  "game over: " + FlyAndMove.GameState);
		}
	}
}