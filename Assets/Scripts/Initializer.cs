using UnityEngine;
using System.Collections;

public class Initializer : MonoBehaviour {
	
	private static bool IsShuttingDown { get; set; }
	
	public void OnApplicationQuit(){
		
		IsShuttingDown = true;
	}
	
	public static Object SafeInstantiate(Object desiredObject, Vector3 position, Quaternion rotation){
		
		return IsShuttingDown ? null : Instantiate(desiredObject, position, rotation);
	}
}