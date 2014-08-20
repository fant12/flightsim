using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	
	
	// attributes
	
	//private QuadTree<Rectangle> _map;
	
	public Airplane airplane;
	
	public GameObject mapSelector;
	
	
	// properties
	
	private FlyAndMove Airplane {
		get { return airplane.GetComponent<FlyAndMove>(); }
	}
	
	private Bounds AirplaneBounds {
		get { return Airplane.renderer.bounds; }
	}
	
	public void Update(){
		
		Vector3 curPos = Airplane.transform.position;
		mapSelector.transform.localPosition = new Vector3(curPos.x / 10000, curPos.z / 10000, 2f);
	}
}