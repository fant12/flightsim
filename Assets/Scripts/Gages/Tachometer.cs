using UnityEngine;
using System.Collections;

public class Tachometer : MonoBehaviour {

	public GUISkin mainSkin;
	
	public Texture2D needleTexture;
	public Texture tachoTexture;
	
	public void OnGUI(){
		
		GUI.skin = mainSkin;
		
		// tachometer
		GUI.Label(new Rect(0.5f * Screen.width - 144f, Screen.height - 144f, 128f, 128f), tachoTexture);
		
		// needle (with rotation)
		float angle = 0.32f * FlyAndMove.Speed - 128f;
		Vector2 anglePoint = new Vector2(0.5f * Screen.width - 80f, Screen.height - 80f); // center: 0.5 * 128 + 16
		Rect rect = new Rect(0.5f * Screen.width - 144f, Screen.height - 144f, 128f, 128f);
		DrawNeedle(angle, anglePoint, rect, needleTexture);
	}
	
	public static void DrawNeedle(float angle, Vector2 rotationPoint, Rect rect, Texture2D texture){

		Matrix4x4 temp = GUI.matrix;
		GUIUtility.RotateAroundPivot(angle, rotationPoint);
		GUI.DrawTexture(rect, texture, ScaleMode.ScaleToFit);
		GUI.matrix = temp;
	}
}