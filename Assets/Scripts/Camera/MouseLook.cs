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
	/// The rotation on y axis.
	/// </summary>
	private float rotationY = 0;
	
	/// <summary>
	/// The sensitivity on x axis.
	/// </summary>
	public float sensitivityX = 15f;
	
	/// <summary>
	/// The sensitivity on y axis.
	/// </summary>
	public float sensitivityY = 15f;
	
	
	// initializer
	public void Start(){
			
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
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
		
		if(axes == RotationAxes.MouseXAndY) {
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
		
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
		
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if(axes == RotationAxes.MouseX)
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		else {
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
		
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}
}