#pragma strict

	@script AddComponentMenu("Camera-Control/Mouse Orbit")


	// global vars
	
	/// <summary>
	/// The horizontal coordinate on x axis.
	/// </summary>
	private var _x: float = 0f;
	
	/// <summary>
	/// The vertical coordinate on y axis.
	/// </summary>
	private var _y: float = 0f;
	
	/// <summary>
	/// The distance to the target.
	/// </summary>
	public var distance: float = 10f;
	
	/// <summary>
	/// The target.
	/// </summary>
	public var target: Transform;

	/// <summary>
	/// The speed on x axis.
	/// </summary>
	public var xSpeed: float = 250f;
	
	/// <summary>
	/// The maximum limit on y axis.
	/// </summary>
	public var yMaxLimit: int = 80;
	
	/// <summary>
	/// The minimum limit on y axis.
	/// </summary>
	public var yMinLimit: int = -20;
	
	/// <summary>
	/// The speed on y axis.
	/// </summary>
	public var ySpeed: float = 120f;


	// initializer

	/// <summary>
	/// The maximum limit on y axis.
	/// </summary>
	function Start(){
	
		var angles = transform.eulerAngles;
		_x = angles.y;
		_y = angles.x;
		
		// makes the rigidbody not change rotation
		if(rigidbody)
			rigidbody.freezeRotation = true;
	}
	

	// functions
	
	/// <summary>
	/// Clamps the angle.
	/// </summary>
	/// <param name='angle'>
	/// the angle
	/// </param>
	/// <param name='min'>
	/// the minimum value on specific axis
	/// </param>
	/// <param name='max'>
	/// the maximum value on specific axis
	/// </param>
	public static function ClampAngle(angle: float, min: float, max: float){
	
		if(-360f > angle)
			angle += 360f;
			
		if(360f < angle)
			angle -= 360f;
			
		return Mathf.Clamp(angle, min, max);	
	}
	
	/// <summary>
	/// Smooth camera follow to rotate around y axis and height.
	/// The horizontal distance to the target is always fixed.
	/// </summary>
	public function LateUpdate(){
	
		if(target){
			
			_x += 0.02f * xSpeed * Input.GetAxis("Mouse X");
			_y -= 0.02f * ySpeed * Input.GetAxis("Mouse Y");
			_y = ClampAngle(_y, yMinLimit, yMaxLimit);
			
			var rotation = Quaternion.Euler(_y, _x, 0);
			var position = rotation * Vector3(0, 0, -distance) + target.position;
			
			transform.rotation = rotation;
			transform.position = position;
		}
	}