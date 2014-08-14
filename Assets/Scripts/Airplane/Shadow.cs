using UnityEngine;
using System.Collections;

/// <summary>
/// Sets the airplane shadow at the ground.
/// </summary>
public class Shadow : MonoBehaviour {

	void Update(){
		
		transform.eulerAngles = new Vector3(90f, FlyAndMove.AirplaneAngleY, 0);
	}
}