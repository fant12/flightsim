#pragma strict

	@script AddComponentMenu("Camera-Control/Smooth Follow")


	// global vars

	/// <summary>
	/// The distance to the target.
	/// </summary>
	var distance: float = 10f;

	/// <summary>
	/// The height above the target.
	/// </summary>
	var heightAboveTarget: float = 5f;
	
	/// <summary>
	/// The height damping value.
	/// </summary>
	var heightDamping: float = 2f;
	
	/// <summary>
	/// The rotation damping value.
	/// </summary>
	var rotationDamping: float = 3f;
	
	/// <summary>
	/// The target to follow.
	/// </summary>
	var target: Transform;
	
	
	// functions
	
	/// <summary>
	/// Smooth camera follow to rotate around y axis and height.
	/// The horizontal distance to the target is always fixed.
	/// </summary>
	function LateUpdate(){
	
		if(!target)
			return;	
	
		// calculate the current and desired rotation angles
		var currentRotationAngle = transform.eulerAngles.y;
		var desiredRotationAngle = target.eulerAngles.y;

		// calculate the current and desired height 		
		var currentHeight = transform.position.y;
		var desiredHeight = target.position.y + heightAboveTarget;
				
		// damping height and rotation by using math lerp function
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, desiredRotationAngle, rotationDamping * Time.deltaTime);
		currentHeight = Mathf.Lerp(currentHeight, desiredHeight, heightDamping * Time.deltaTime);
	
		// convert the angle into a quaternion
		var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

		// camera position (behind the target)
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		
		// camera height
		transform.position.y = currentHeight;
	
		// always look at the target
		transform.LookAt(target);
	}