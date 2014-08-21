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
		
		// w: 42000, h: 28000, scale: 1/8
		Vector3 curPos = Airplane.transform.position;
		mapSelector.transform.localPosition = new Vector3(curPos.x / 80000f, curPos.z / 80000f, 2f);
	}
}