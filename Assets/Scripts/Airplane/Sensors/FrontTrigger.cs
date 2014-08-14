using UnityEngine;
using System.Collections;

/// <summary>
/// Triggers if the airplane drives downwards.
/// </summary>
public class FrontTrigger : MonoBehaviour {

	public static bool Triggered { get; private set; }

	public void OnTriggerEnter(Collider collider){
		Triggered = true;
	}	
	
	public void OnTriggerExit(Collider collider){
		Triggered = false;
	}
}