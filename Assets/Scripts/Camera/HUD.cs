using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HUD : MonoBehaviour {
	
	
	// attributes
	
	/// <summary>
	/// Contains all selectors. The key is the assigned enemy.
	/// </summary>
	private Dictionary<GameObject, GameObject> _selectors;
	
	public GameObject activeTargetArea;
	
	public TextMesh heightText;
	
	public Camera fixedCamera;
	
	public Camera hudCamera;
	
	public TextMesh leftPitchDisplay;
	
	public TextMesh lockTimeText;
	
	public GameObject pitchAttitude;
	
	public TextMesh rightPitchDisplay;
	
	public GameObject selector;
	
	public Texture2D selectorDefaultTexture;
	
	public Texture2D selectorHoverTexture;
	
	public TextMesh speedText;
	
	public GameObject targetDisplay;
	
	public Vector2 targetPos;
	
	
	// properties
	
	private float CurrentTilt { get; set; }
	
	private FlyAndMove FlyAndMoveScript {
		get { return gameObject.GetComponent<FlyAndMove>(); }
	}
	
	private string HeightStringValue {
		get { 
			// substract 2 because height of airplane
			return FormatValue(FlyAndMoveScript.Position.y - 2f, true); 
		}
	}
	
	private float LockTime { get; set; }
	
	private string LockTimeStringValue {
		get { return (0 == LockTime) ? "" : FormatValue(LockTime, false); }
	}
	
	private Transform MissileHasTarget { 
		get {
			if(Airplane.Instance.missile && Airplane.Instance.LeftMissile && Airplane.Instance.RightMissile){
				
				Transform target = Airplane.Instance.LeftMissileScript.Target;
				return target ? target : Airplane.Instance.RightMissileScript.Target;
			}
			return null;
		} 
	}
	
	private string PitchStringValue {
		get { return FormatValue(FlyAndMoveScript.Rotation.x, false); }
	}
	
	private string SpeedStringValue {
		get { return FormatValue(FlyAndMoveScript.SpeedValue, true); }
	}
	
	private float Tilt {
		get { return FlyAndMoveScript.Rotation.z; }
	}
	

	// initializer
	
	public void Start(){
		_selectors = new Dictionary<GameObject, GameObject>();
	}
	
	
	// events
	
	public void OnTriggerEnter(Collider collider){

		if("Enemy".Equals(collider.gameObject.tag)){
			
			GameObject newSelector = Instantiate(selector) as GameObject;
			newSelector.transform.parent = hudCamera.transform;
			
			if(!_selectors.ContainsKey(collider.gameObject))
				_selectors.Add(collider.gameObject, newSelector);
			else
				_selectors[collider.gameObject] = newSelector;
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
	
	private static string FormatValue(float val, bool overThousand){
	
		string result = val.ToString("F0");
		
		if(overThousand){
			if(10f > val)
				return "000" + result;
			else if(100f > val)
				return "00" + result;
			else if(1000f > val)
				return "0" + result;
		}
		else {
			if(10f > val)
				return "00" + result;
			else if(100f > val)
				return "0" + result;
		}
		
		return result;
	}
	
	public static Vector3 RelativePositionTo(Transform origin, Vector3 to){
		
		Vector3 distance = to - origin.position;
		return new Vector3(Vector3.Dot(distance, origin.right.normalized),
							Vector3.Dot(distance, origin.up.normalized),
							Vector3.Dot(distance, origin.forward.normalized));
		
	}
	
	private void ShowTarget(GameObject target, GameObject selector){
		
		if(target){
			if(selector){
				
				if(MissileHasTarget && MissileHasTarget == target.transform)
					selector.renderer.material.mainTexture = selectorHoverTexture;
				else
					selector.renderer.material.mainTexture = selectorDefaultTexture;
				
				// plane (origin) relative to enemy (to)
				Vector3 pos = RelativePositionTo(transform, target.transform.position);
				selector.transform.localPosition = new Vector3(0.1f * pos.x, 0.1f * pos.z, 14f);
				
			}
		}
		else {
	
			// target is destroyed (?), so remove selector, too
			Destroy(selector);
		}
	}
	
	public void Update(){
		
		// set the current values for pitch, height and speed
		SetPitch();
		heightText.text = HeightStringValue;
		lockTimeText.text = LockTimeStringValue;
		speedText.text = SpeedStringValue;
		
			
		// if enemies are in residence
		if(0 < _selectors.Count){
			
			// remove destroyed enemies from dictionary
			var removable = _selectors.Where(v => v.Value == null).ToArray();
			
			foreach(var item in removable){
				_selectors.Remove(item.Key);
				break;
			}
		
			// show targets on hud
			foreach(KeyValuePair<GameObject, GameObject> enemy in _selectors)
				ShowTarget(enemy.Key, enemy.Value);
		}
	}
	
	private void SetPitch(){
		
		leftPitchDisplay.text = PitchStringValue;
		rightPitchDisplay.text = PitchStringValue;
	}
}