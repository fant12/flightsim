using UnityEngine;
using System.Collections;

public class TextColor : MonoBehaviour {

	public Color color;
	
	public void Awake(){
		transform.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
	}
}