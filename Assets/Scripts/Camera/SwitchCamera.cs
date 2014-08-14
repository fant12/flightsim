using UnityEngine;
using System.Collections;

public class SwitchCamera : MonoBehaviour {
	
	public Camera fixedCamera;
	public Camera mainCamera;
	public Camera smoothCamera;
	
	public int selectedFieldIndex;
	
	public void OnGUI(){
		
		Camera[] cameras = new Camera[]{ mainCamera, fixedCamera, smoothCamera };
		string[] selectionFields = new string[] { "Follow", "Fixed", "Smooth" };
		
		// if the game is over go back to the first camera
		if(2 == FlyAndMove.GameState){
			
			selectedFieldIndex = 0;
			fixedCamera.enabled = false;
			mainCamera.enabled = true;
			smoothCamera.enabled = false;
		}
		else {
		
			selectedFieldIndex = GUI.SelectionGrid(new Rect(Screen.width - 100f, 60f, 100f, 100f), selectedFieldIndex, selectionFields, 1);
		
			for(int i = 0; cameras.Length > i; ++i)
				cameras[i].enabled = selectedFieldIndex == i;
		}	
	}
}