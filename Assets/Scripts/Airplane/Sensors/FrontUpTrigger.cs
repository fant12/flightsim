using UnityEngine;
using System.Collections;

public class FrontUpTrigger : MonoBehaviour {

	public static bool Triggered { get; private set; }

	public void OnTriggerEnter(Collider collider){
		Triggered = true;
	}	
	
	public void OnTriggerExit(Collider collider){
		Triggered = false;
	}
}