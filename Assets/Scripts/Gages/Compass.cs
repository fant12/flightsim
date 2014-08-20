using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour {
	
	public Camera fixedCamera;
	
	public GUISkin mainSkin;
	
	public Texture2D background;
	public Texture2D foreground;
	
	public void OnGUI(){
		
		if(fixedCamera.camera.enabled){
		
			GUI.skin = mainSkin;
			
			float inlayOffset = -95f;
			
			// calculate the offset, dependant of the airplane y angle
			// substraction 360 - airplane angle, because the compass inlay shows the other direction
			// -5 is calibrating the compass to north - south
			inlayOffset = -0.5f * (360f - FlyAndMove.AirplaneAngleY) - 5f;
			
			
			// clipping with groups
			
			// group both images
			GUI.BeginGroup(new Rect(0.75f * Screen.width + 10f, 40f, 256f, 256f));
			
			// group which will be clipped
			GUI.BeginGroup(new Rect(24f, 40f, 80f, 48f));
			
			// draw the compass inlay
			GUI.Label(new Rect(inlayOffset, 0, 512f, 512f), foreground);
			
			GUI.EndGroup();
			
			// compass background
			//GUI.Label(new Rect(0, 0, 128f, 128f), background);
			
			GUI.EndGroup();
		}
	}
	
	public static Rect ResizeGUI(Rect rect){
		
		float filScreenWidth = rect.width / 800f;
		float rectWidth = filScreenWidth * Screen.width;
		float filScreenHeight = rect.height / 600f;
		float rectHeight = filScreenHeight * Screen.height;
		float rectX = (rect.x / 800f) * Screen.width;
		float rectY = (rect.y / 600f) * Screen.height;
 
		return new Rect(rectX, rectY, rectWidth, rectHeight);
	}
	
	public static Rect ResizeGUI(float x, float y, float width, float height){
		
		float filScreenWidth = width / 800f;
		float rectWidth = filScreenWidth * Screen.width;
		float filScreenHeight = height / 600f;
		float rectHeight = filScreenHeight * Screen.height;
		float rectX = (x / 800f) * Screen.width;
		float rectY = (y / 600f) * Screen.height;
 
		return new Rect(rectX, rectY, rectWidth, rectHeight);
	}
}