using UnityEngine;
using System.Collections;

public class SwitchCamera : MonoBehaviour {
	
	public Camera fixedCamera;
	public Camera mainCamera;
	public Camera smoothCamera;
	
	public int selectedFieldIndex;
	private string[] selectionFields = new string[] { "Follow", "Fixed", "Smooth" };
	
	public void OnGUI(){
		
		selectedFieldIndex = GUI.SelectionGrid(new Rect(Screen.width - 100f, 60f, 100f, 100f), selectedFieldIndex, selectionFields, 1);
		ChangeCamera();
	}
	
	private void ChangeCamera(){
		
		Camera[] cameras = new Camera[]{ mainCamera, fixedCamera, smoothCamera };
		
		// if the game is over go back to the first camera
		if(2 == FlyAndMove.GameState){
			
			selectedFieldIndex = 0;
			fixedCamera.enabled = false;
			mainCamera.enabled = true;
			smoothCamera.enabled = false;
		}
		else {
			for(int i = 0; cameras.Length > i; ++i)
				cameras[i].enabled = selectedFieldIndex == i;
			
			FadeCross.DefaultCursorIsActive = (1 != selectedFieldIndex);
		}
	}
	
	public void Update(){
		
		if(Input.GetKeyUp(KeyCode.Keypad1) || Input.GetKeyUp(KeyCode.Alpha1))
			selectedFieldIndex = 0;
		else if(Input.GetKeyUp(KeyCode.Keypad2) || Input.GetKeyUp(KeyCode.Alpha2))
			selectedFieldIndex = 1;
		else if(Input.GetKeyUp(KeyCode.Keypad3) || Input.GetKeyUp(KeyCode.Alpha3))
			selectedFieldIndex = 2;
	}
}