using UnityEngine;
using System.Collections;

public class Altimeter : MonoBehaviour {

	public GUISkin mainSkin;
	
	public Texture altimeter;
	public Texture2D needleTexture;
	public Texture2D smallNeedleTexture;
	
	public void OnGUI(){
		
		GUI.skin = mainSkin;
		
		// the airplane wheels are the center for the flightheight
		float flightHeight = Mathf.Round(FlyAndMove.Position.y - 2f);
		
		// tacho texture
		GUI.Label(new Rect(0.5f * Screen.width + 16f, Screen.height - 144f, 128f, 128f), altimeter);
		
		// text values
		GUI.contentColor = Color.black;
		
		string text = flightHeight.ToString();
		
		if(9f >= flightHeight)
			text = "000" + text;
		else if(9f < flightHeight && 99f >= flightHeight)
			text = "00" + text;
		else if(99f < flightHeight && 999f >= flightHeight)
			text = "0" + text;
			
		GUI.Label(new Rect(0.5f * Screen.width + 66f, Screen.height -66f, 36, 30), text);
		
		// needles (with rotation)
		DrawNeedle(2f, needleTexture);
		DrawNeedle(0, smallNeedleTexture);
	}
	
	private void DrawNeedle(float angleShift, Texture2D needleTexture){
	
		float angle = angleShift + 3.6f * FlyAndMove.Position.y;
		
		// rotation point in texture added with x and y
		// needle: 16x16, texture: 128x128 > center: 64x64 + 16 = 80
		Vector2 rotationPoint = new Vector2(0.5f * Screen.width + 80f, Screen.height - 80f);
		
		Rect rect = new Rect(0.5f * Screen.width + 16f, Screen.height - 144f, 128f, 128f);
		Tachometer.DrawNeedle(angle, rotationPoint, rect, needleTexture);
	}	
}