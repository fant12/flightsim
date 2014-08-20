#pragma strict


	// global vars

	var damping = 5f;

	var distance: float = 3f;

	var heightAboveTarget = 3f;
	
	var rotationDamping: float = 10f;
	
	var target: Transform;
	
	var useSmoothRotation = true;


	// functions
	
	function Update(){

		var desiredPosition = target.TransformPoint(0, heightAboveTarget, -distance);
		transform.position = Vector3.Lerp(transform.position, desiredPosition, damping * Time.deltaTime);
		
		if(useSmoothRotation){
			var desiredRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
			transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationDamping * Time.deltaTime);
		}
		else 
			transform.LookAt(target, target.up);
	}