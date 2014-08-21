using UnityEngine;
using System.Collections;

public class FadeCross : MonoBehaviour {
	
	
	// attributes
	
	public Texture2D cursorTexture;
	
	public int cursorTextureHeight = 258;
	
	public int cursorTextureWidth = 258;
	
	
	// properties
	
	/// <summary>
	/// Gets the size of the cross.
	/// </summary>
	/// <value>
	/// The size of the cross.
	/// </value>
	private Vector3 CrossSize {
		get { return renderer.bounds.size; }
	}
	
	/// <summary>
	/// Gets or sets a value indicating whether the custom cursor is active.
	/// </summary>
	/// <value>
	/// <c>true</c> if custom cursor is active; otherwise, <c>false</c>.
	/// </value>
	public static bool DefaultCursorIsActive { 
		get { return Screen.showCursor; } 
		set {Screen.showCursor = value; } 
	}
	
	
	// initializer
	
	public void OnGUI(){
	
		if(!DefaultCursorIsActive)
			GUI.DrawTexture(new Rect(
				Input.mousePosition.x - cursorTextureWidth / 8, 
				(Screen.height - Input.mousePosition.y) - cursorTextureHeight / 8, 
				0.25f * cursorTextureWidth, 
				0.25f * cursorTextureHeight), cursorTexture);
	}
	
	public void Start(){
	
		// mouse cursor
		DefaultCursorIsActive = false;
	}
	
	
	// methods
	
	/*private void SetFadeCross(){
		
		Vector3 point = gameObject.camera.ScreenToWorldPoint(Input.mousePosition);
		
		cross.transform.localPosition = new Vector3(
			point.x - CrossSize.x, 
			(Screen.height + point.y) - Input.mousePosition.y,
			14f);
	}*/
	
	public void Update(){}
}