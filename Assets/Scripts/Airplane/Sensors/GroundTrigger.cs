using UnityEngine;
using System.Collections;

/// <summary>
/// The ground trigger, visually describes as a the wheels of the airplane.
/// </summary>
public class GroundTrigger : MonoBehaviour {
	
	/// <summary>
	/// Defines whether the wheels has contact with the ground or not.
	/// </summary>
	public static bool Triggered { get; set; }
	
	public void OnTriggerEnter(Collider other){
		Triggered = true;
	}
	
	public void OnTriggerExit(Collider other){
		Triggered = false;
	}
}