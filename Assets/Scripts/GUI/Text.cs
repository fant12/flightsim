using UnityEngine;
using System.Collections;

/// <summary>
/// Shows the introduction.
/// </summary>
public class Text : MonoBehaviour {
	
	/// <summary>
	/// Raises the GUI event.
	/// </summary>
	public void OnGUI(){
		
		GUI.contentColor = new Color(0.1f, 0f, 0.1f);
		GUI.Label(new Rect(32f, 48f, 320f, 110f), 
			"Use:\n" +
			"Ctrl to accellerate\n" +
			"Shift to deccellerate\n" +
			"Arrows to change direction\n" +
			"Alt+F4 or ESC to exit\n" +
			"F2 to restart level \n\n" +
			"For Debug-Mode press 1 above the letters.");
	}
}