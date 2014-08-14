using UnityEngine;
using System.Collections;

/// <summary>
/// FPS counter.
/// It calculates frames per second over each update interval, so the 
/// display doesn't keep changing wildly.
/// </summary>
public class FPSCounter : MonoBehaviour {
	
	/// <summary>
	/// FPS accumulated over the interval.
	/// </summary>
	private float accumulation;
	
	/// <summary>
	/// Frames drawn over the interval.
	/// </summary>
	private int frames;
	
	/// <summary>
	/// Left time for current interval.
	/// </summary>
	private float timeLeft;
	
	public float updateInterval;
	
	public void Start () {

		updateInterval = 0.5f;
		
		if(!guiText){
			print("FramesPerSecond needs a GUIText component!");
			enabled = false;
        	return;
		}
		
		timeLeft = updateInterval;
	}
	
	// Update is called once per frame
	public void Update(){
	
		timeLeft -= Time.deltaTime;
		accumulation += Time.timeScale / Time.deltaTime;
		++frames;
		
		// interval ended - update gui text and start new interval
		if(0 >= timeLeft){
		
			// display two fractional digits (f2 format)
			guiText.text = (accumulation / frames).ToString("f2");
			timeLeft = updateInterval;
			accumulation = 0;
			frames = 0;
		}
	}
}