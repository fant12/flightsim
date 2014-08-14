using UnityEngine;
using System.Collections;

//This instrument works unfortunately just partially yet. I don`t find way to retreive the real angles from my airplane. Just euler angles. 
//Which flips around by 180 degrees from time to time. Which leads of course to the same behaviour in my instrument. 
//I have no clue how to work around this :/
public class Horizon : MonoBehaviour {

	public GUISkin mainSkin;
	
	// images: 256x32
	public Texture2D background;
	public Texture2D foreground;
	
	public void OnGUI(){
		
		float pitch, tilt;
		
		GUI.skin = mainSkin;
		
		// pitch forward / backward
		pitch = (180f > FlyAndMove.Rotation.x) ? -210f - FlyAndMove.Rotation.x : -210f - FlyAndMove.Rotation.x + 360f;
		
		// tilt sidewards
		tilt = FlyAndMove.Rotation.z;
		
		// group both images
		GUI.BeginGroup(new Rect(0.5f * Screen.width - 304f, Screen.height - 144f, 128f, 128f));
		
		// group which will be clipped
		GUI.BeginGroup(new Rect(22f, 19f, 84f, 92f));
		
		// draw foreground
		Tachometer.DrawNeedle(tilt, new Vector2(42f, 45f), new Rect(0, pitch, 84f, 512f), foreground);
		
		GUI.EndGroup();
		
		// draw background in front of the other texture
		GUI.Label(new Rect(0, 0, 128f, 128f), background);
	
		GUI.EndGroup ();
	}
}