using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour {

	public GUISkin mainSkin;
	
	public Texture2D background;
	public Texture2D foreground;
	
	public void OnGUI(){
		
		GUI.skin = mainSkin;
		
		float inlayOffset = -95f;
		
		// calculate the offset, dependant of the airplane y angle
		// substraction 360 - airplane angle, because the compass inlay shows the other direction
		// -5 is calibrating the compass to north - south
		inlayOffset = -0.5f * (360f - FlyAndMove.AirplaneAngleY) - 5f;
		
		
		// clipping with groups
		
		// group both images
		GUI.BeginGroup(new Rect(0.5f * Screen.width + 176f, Screen.height - 144f, 256f, 256f));
		
		// group which will be clipped
		GUI.BeginGroup(new Rect(24f, 40f, 80f, 48f));
		
		// draw the compass inlay
		GUI.Label(new Rect(inlayOffset, 0, 512f, 512f), foreground);
		
		GUI.EndGroup();
		
		// compass background
		GUI.Label(new Rect(0, 0, 128f, 128f), background);
		
		GUI.EndGroup();
	}
}