using UnityEngine;
using System.Collections;

/// <summary>
/// Rotates the transform based on the mouse delta.
/// </summary>
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {
	
	
	// attributes
	
	/// <summary>
	/// Rotation axes.
	/// </summary>
	public enum RotationAxes { 
		MouseXAndY = 0, 
		MouseX = 1, 
		MouseY = 2 
	}
	
	/// <summary>
	/// The origin.
	/// </summary>
	private Quaternion _origin;

	/// <summary>
	/// The rotation on x axis.
	/// </summary>
	private float _rotationX = 0;
	
	/// <summary>
	/// The rotation on y axis.
	/// </summary>
	private float _rotationY = 0;
		
	/// <summary>
	/// The axes.
	/// </summary>
	public RotationAxes axes = RotationAxes.MouseXAndY;
	
	/// <summary>
	/// The maximum x.
	/// </summary>
	public float maximumX = 360f;
	
	/// <summary>
	/// The maximum y.
	/// </summary>
	public float maximumY = 60f;
	
	/// <summary>
	/// The minimum x.
	/// </summary>
	public float minimumX = -360f;
	
	/// <summary>
	/// The minimum y.
	/// </summary>
	public float minimumY = -60f;
	
	/// <summary>
	/// The sensitivity on x axis.
	/// </summary>
	public float sensitivityX = 15f;
	
	/// <summary>
	/// The sensitivity on y axis.
	/// </summary>
	public float sensitivityY = 15f;

	
	// initializer
	
	/// <summary>
	/// Initializes this instance.
	/// </summary>
	public void Start(){
			
		// make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
		
		_origin = transform.localRotation;	
	}
	
	
	// methods
	
	/// <summary>
	/// Clamps the angle.
	/// </summary>
	public static float ClampAngle(float angle, float min, float max){
		
		if(-360f > angle)
			angle += 360f;
		
		if(360f < angle)
			angle -= 360f;
		
		return Mathf.Clamp(angle, min, max);
	}
	
	/// <summary>
	/// Focus on a target.
	/// </summary>
	private void Focus(){
		
		if(Input.GetMouseButtonUp(0)){
		
			Ray origin = gameObject.camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			
			if(Physics.Raycast(origin, out hit, 1000))
				if(hit.collider.tag.Equals("Enemy"))
					Airplane.Instance.Shoot(hit.collider.gameObject.transform);
			
		}
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	public void Update(){
		
		Focus();
		
		if(RotationAxes.MouseXAndY == axes){
			
			// mouse input
			_rotationX += sensitivityX * Input.GetAxis("Mouse X");
			_rotationY += sensitivityY * Input.GetAxis("Mouse Y");
			
			_rotationX = ClampAngle(_rotationX, minimumX, maximumX);
			_rotationY = ClampAngle(_rotationY, minimumY, maximumY);
			
			Quaternion xQuaternion = Quaternion.AngleAxis(_rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis(_rotationY, Vector3.left);
			
			transform.localRotation = _origin * xQuaternion * yQuaternion;
		}
		else if(RotationAxes.MouseX == axes){
			
			_rotationX += sensitivityX * Input.GetAxis("Mouse X");
			_rotationX = ClampAngle(_rotationX, minimumX, maximumX);
			
			Quaternion xQuaternion = Quaternion.AngleAxis(_rotationX, Vector3.up);
			transform.localRotation = _origin * xQuaternion;
		}
		else {
			
			_rotationY += sensitivityY * Input.GetAxis("Mouse Y");
			_rotationY = ClampAngle(_rotationY, minimumY, maximumY);
			
			Quaternion yQuaternion = Quaternion.AngleAxis(_rotationY, Vector3.left);
			transform.localRotation = _origin * yQuaternion;
		}
	}
}