using UnityEngine;
using System.Collections;

public class Altimeter : MonoBehaviour {
	
	public int maxHeight = 15;
	public int minHeight = 0;
	
	public GUISkin mainSkin;
	
	public void OnGUI(){
		
		GUI.skin = mainSkin;
		
		// the airplane wheels are the center for the flightheight
	//	float currentFlightHeight = Mathf.Round(FlyAndMove.Position.y - 2f);
		
		// text values
		GUI.contentColor = new Color(113f, 174f, 33f, 255f);
		
		/*
		
		if(10f > flightHeight)
			text = "000" + text;
		else if(100f > flightHeight)
			text = "00" + text;
		else if(1000f > flightHeight)
			text = "0" + text;
			
		GUI.Label(new Rect(0.5f * Screen.width + 66f, Screen.height -66f, 36, 30), text);
		
		*/
	}
}