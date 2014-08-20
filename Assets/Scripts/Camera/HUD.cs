using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour {
	
	
	// attributes
	
	/// <summary>
	/// Contains all selectors. The key is the assigned enemy.
	/// </summary>
	private Dictionary<GameObject, GameObject> _selectors;
	
	public Camera hudCamera;
	
	public TextMesh leftPitchDisplay;
	
	public TextMesh rightPitchDisplay;
	
	public GameObject targetDisplay;
	
	public GameObject selector;
	
	//private GameObject[] _targetSelectionBorders;
	
	
	// properties
	
	private string PitchStringValue {
		get { 
			float pitch = gameObject.GetComponent<FlyAndMove>().Rotation.x;
			string result = pitch.ToString("F0");
			
			if(10f > pitch)
				return "00" + result;
			else if(100f > pitch)
				return "0" + result;
			
			return result; 
		}
	}
	
	/*private GameObject[] PossibleTargets{
		get {
			return GameObject.FindGameObjectsWithTag("Enemy");
		}
	}*/
		
	
	// initializer
	
	public void Start(){
		_selectors = new Dictionary<GameObject, GameObject>();
		//_targetSelectionBorders = new GameObject[PossibleTargets.Length];
	}
	
	
	// events
	
	public void OnTriggerEnter(Collider collider){

		if("Enemy".Equals(collider.gameObject.tag)){
			
			GameObject newSelector = Instantiate(selector) as GameObject;
			newSelector.transform.parent = hudCamera.transform;
			_selectors.Add(collider.gameObject, newSelector);
		}
	}
	
	public void OnTriggerExit(Collider collider){
		
		if("Enemy".Equals(collider.gameObject.tag)){
			
			GameObject temp = null;
			_selectors.TryGetValue(collider.gameObject, out temp);
		
			if(temp){
				_selectors.Remove(collider.gameObject);
				Destroy(temp);
			}
		}
	}
	
	
	// methods
	
	public static Vector3 RelativePositionTo(Transform origin, Vector3 to){
		
		Vector3 distance = to - origin.position;
		Vector3 v= new Vector3(Vector3.Dot(distance, origin.right.normalized), Vector3.Dot(distance, origin.up.normalized), Vector3.Dot(distance, origin.forward.normalized));
		print (v.ToString());
		return v;
	}
	
	/*private void AddSelectionBorder(){
		
		// add a border
		GameObject border = Instantiate(selector) as GameObject;
		border.transform.parent = hudCamera.transform;
		
		// copy array
		GameObject[] temp = new GameObject[PossibleTargets.Length];

		for(int i = 0; _targetSelectionBorders.Length > i; ++i)
			temp[i] = _targetSelectionBorders[i];
		
		_targetSelectionBorders = temp;
		System.GC.Collect(); // call mono gc
	}
	*/
	
	private void ShowTarget(Transform target, GameObject selector){
		
		if(selector){
			
			// plane (origin) relative to selector (to)
			
			
			Vector3 curPos = target.transform.position;
			selector.transform.localPosition = RelativePositionTo(selector.transform, target.transform.position);
			
			//selector.transform.localPosition = new Vector3(target.position.x / 100f, target.position.z / 100f, 14f); 	
			//selector.transform.rotation = target.rotation;
		}
		//Vector3 point = hudCamera.WorldToScreenPoint(target.transform.position);
		//_targetSelectionBorders[borderIndex].transform.position = new Vector3(point.x / Screen.width, point.y / Screen.height, 0);
	}
	
	/*
	private void SearchTarget(){
		
		for(int i = 0; PossibleTargets.Length > i; ++i)
			if(PossibleTargets[i] && PossibleTargets[i].renderer.isVisible){
			
				// linecast
				Vector3 targetPos = PossibleTargets[i].transform.position;
				RaycastHit hit = new RaycastHit();
			
				// if nothing is obscuring the target
				if(Physics.Linecast(transform.position, targetPos, out hit) && PossibleTargets[i].transform == hit.transform){
					
					if(i > _targetSelectionBorders.Length)
						AddSelectionBorder();
				
					ShowTarget(PossibleTargets[i].transform, i);
				}
			}
			else {
				GameObject temp = _targetSelectionBorders[i];
				_targetSelectionBorders[i] = null;
				Destroy(temp);
			}
	}*/
	
	public void Update(){

		SetTilt();
		
		foreach(KeyValuePair<GameObject, GameObject> enemy in _selectors)
			ShowTarget(enemy.Key.transform, enemy.Value);
	}
	
	private void SetTilt(){
		
		leftPitchDisplay.text = PitchStringValue;
		rightPitchDisplay.text = PitchStringValue;
	}
}