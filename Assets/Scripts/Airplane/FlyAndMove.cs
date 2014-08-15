using UnityEngine;
using System.Collections;

/// <summary>
/// Fly and move.
/// </summary>
public class FlyAndMove : MonoBehaviour {
	
	
	// attributes
	
	/// <summary>
	/// The current state of the game.
	/// </summary>
	private static int _airplaneState;
	
	/// <summary>
	/// Blocks sideways stagger flight while diving.
	/// </summary>
	protected float _blockFluctuate;

	/// <summary>
	/// Blocks a turn while diving forward.
	/// </summary>
	protected float _blockForwardTurn;
	
	/// <summary>
	/// The value for a soft curveflight.
	/// </summary>
	protected float _curveFlight;
	
	/// <summary>
	/// The force to crash the airplane.
	/// </summary>
	protected float _forceToCrash;
		
	/// <summary>
	/// The uplift to lift up.
	/// </summary>
	protected float _uplift;
	
	/// <summary>
	/// The acceleration.
	/// </summary>
	public float acceleration = 240f;
	
	/// <summary>
	/// The gravitation for downlift while driving through landscape.
	/// </summary>
	public float gravitation = -0.3f;
	
	/// <summary>
	/// The flight height to move the landing gears.
	/// </summary>
	public int heightToMoveLandingGears = 6;
	
	/// <summary>
	/// The maximum flight height.
	/// </summary>
	public float maxFlightHeight = 1000f;
	
	/// <summary>
	/// The maximum speed.
	/// </summary>
	public float maxSpeed = 800f;
	
	/// <summary>
	/// The maximum speed for driving.
	/// </summary>
	public float maxSpeedForDriving = 600f;
	
	
	// properties
	
	/// <summary>
	/// Gets or sets the airplane angle y.
	/// </summary>
	/// <value>
	/// The airplane angle y.
	/// </value>
	public static float AirplaneAngleY { get; protected set; }
	
	/// <summary>
	/// Gets the neutral speed.
	/// </summary>
	/// <value>
	/// The neutral speed.
	/// </value>
	public float NeutralSpeed {
		get { return 0.5f * (maxSpeed + maxSpeedForDriving); } 
	}
	
	/// <summary>
	/// Gets or sets the position.
	/// </summary>
	/// <value>
	/// The position.
	/// </value>
	public static Vector3 Position { get; set; }
	
	/// <summary>
	/// Gets or sets the rotation.
	/// </summary>
	/// <value>
	/// The rotation.
	/// </value>
	public static Vector3 Rotation { get; set; }
	
	/// <summary>
	/// Gets or sets the speed.
	/// </summary>
	/// <value>
	/// The speed.
	/// </value>
	public static float Speed { get; set; }
	
	/// <summary>
	/// Gets the absolute value of curve flight.
	/// </summary>
	/// <value>
	/// The absolute value for the curve flight.
	/// </value>
	public float AbsoluteCurveFlight { 
		get { return Mathf.Abs(_curveFlight); } 
	}

	/// <summary>
	/// Defines the game state.
	/// </summary>
	/// <value>
	/// The game state.
	/// </value>
	public static int GameState { 
		get { return _airplaneState; }
		set { _airplaneState = value; } 
	}
	
	
	// initializer
	
	/// <summary>
	/// Initializes the airplane attributes.
	/// </summary>
	public void Start(){
		
		_blockFluctuate = 0;
		_blockForwardTurn = 0;
		_curveFlight = 0;
		_forceToCrash = 0;
		_uplift = 0;
		
		AirplaneAngleY = 0;
		GameState = 0;
		Position = new Vector3(0, 1.67f, 0);
		Rotation = new Vector3(0, 0);
		Speed = 0;
	}

	
	// events
	
	/// <summary>
	/// Raises the collision enter event when flying and crashes with something.
	/// </summary>
	/// <param name='collision'>
	/// Collision.
	/// </param>
	public void OnCollisionEnter(Collision collision){
		
		if(!GroundTrigger.Triggered){
			
			GroundTrigger.Triggered = true;
			_forceToCrash = 10000f * Speed;
			Speed = 0;
			GameState = 1;
			rigidbody.useGravity = true;
		}
	}
	
	
	// methods
	
	/// <summary>
	/// Accelerate or deccelerate.
	/// </summary>
	protected void Accelerate(){
		
		transform.Translate(0, 0, 0.05f * Speed * Time.deltaTime);
		
		// control minimum Speed for flying
		
		// at ground
		if(GroundTrigger.Triggered){
			
			// accelerate and deccelerate
			if(Input.GetButton("Fire1") && maxSpeed > Speed)
				Speed += acceleration * Time.deltaTime;
			if(Input.GetButton("Fire2") && 0 < Speed)
				Speed -= acceleration * Time.deltaTime;
		}
		// in the air
		else {
			
			// accelerate and deccelerate
			if(Input.GetButton("Fire1") && maxSpeed > Speed)
				Speed += acceleration * Time.deltaTime;
			if(Input.GetButton("Fire2") && maxSpeedForDriving < Speed)
				Speed -= acceleration * Time.deltaTime;
		}
		
		// float fixes
		
		if(0 > Speed)
			Speed = 0;
		
		if(!GroundTrigger.Triggered && !Input.GetButton("Fire1") && !Input.GetButton("Fire2") && 
			NeutralSpeed-5f < Speed && NeutralSpeed+5f > Speed)
			
			Speed = NeutralSpeed;
	}
	
	/// <summary>
	/// Blocks a turn forward.
	/// </summary>
	protected void BlockTurn(){
		
		_blockFluctuate = 0;
		
		if(90f > Rotation.x){
			_blockForwardTurn = 0.01f * Rotation.x;
			_blockFluctuate = 0.02f * Rotation.x;
		}

		if(90f < Rotation.x)
			_blockForwardTurn = -0.2f;
	}
	
	/// <summary>
	/// Calculates the tilt.
	/// </summary>
	protected void CalculateTilt(){
		
		// prevent that the airplane fly to the left while it is still tilted to the right
		if(0 >= Input.GetAxis("Horizontal") && 0 < Rotation.z && 90f > Rotation.z)
			_curveFlight = -0.022f * Rotation.z;
		if(0 <= Input.GetAxis("Horizontal") && 270f < Rotation.z)
			_curveFlight = 7.92f - 0.022f * Rotation.z;
		
		// limit the value of rightLeftSoft, so the switch between flying overhead and not isn't so hard
		if(1f < _curveFlight)
			_curveFlight = 1f;
		if(-1f > _curveFlight)
			_curveFlight = -1f;
		
		// fix float
		if(-0.01f < _curveFlight && 0.01f > _curveFlight)
			_curveFlight = 0;		
	}
	
	/// <summary>
	/// Drive.
	/// </summary>
	protected void Drive(){
	
		// upwards or downwards drive
		
		if(!FrontTrigger.Triggered && RearTrigger.Triggered)
			transform.Rotate(20f * Time.deltaTime, 0, 0);
	
		if(FrontTrigger.Triggered && !RearTrigger.Triggered)
			transform.Rotate(-20f * Time.deltaTime, 0, 0);
	
		if(FrontUpTrigger.Triggered)
			transform.Rotate(-20f * Time.deltaTime, 0, 0);
		
		// gravitation
		if(!GroundTrigger.Triggered)
			transform.Translate(0, 0.1f * gravitation * Time.deltaTime, 0);		
	}
	
	/// <summary>
	/// Limits the area.
	/// </summary>
	protected void LimitArea(){
	
		// area width and depth
		if(900f <= transform.position.x)
			transform.position = new Vector3(0, transform.position.y, transform.position.z);
		else if(-900f >= transform.position.x)
			transform.position = new Vector3(900f, transform.position.y, transform.position.z);
		else if(900f <= transform.position.z) 
			transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		else if(-900f >= transform.position.z)
			transform.position = new Vector3(transform.position.x, transform.position.y, 900f);
	
		// area height
		if(maxFlightHeight < Position.y)
			transform.position = new Vector3(transform.position.x, maxFlightHeight, transform.position.z);
	}
	
	/// <summary>
	/// Reset the airplane.
	/// </summary>
	protected void Reset(){
		
		GameState = 0;
		rigidbody.useGravity = false;
		transform.position = new Vector3(0, 1.67f, 0);
		transform.eulerAngles = new Vector3(1.328f, -3.23f, -3.75f);
	}
	
	/// <summary>
	/// Restart the level.
	/// </summary>
	public void Restart(){
		
		Speed = 0;
		GameState = 0;
		rigidbody.useGravity = false;
		Application.LoadLevel(0);
	}
	
	/// <summary>
	/// Rotates the airplane.
	/// </summary>
	protected void RotateAirplane(){
		
		// limit up and down to a minimum speed
		if(maxSpeedForDriving - 5f < Speed){
		
			if(0 >= Input.GetAxis("Vertical"))
				transform.Rotate(80f * Input.GetAxis("Vertical") * Time.deltaTime, 0, 0);
			else
				// if dive above 90 degrees
				transform.Rotate(80f * (0.8f - _blockForwardTurn) * Input.GetAxis("Vertical") * Time.deltaTime, 0, 0);
		}
		
		// right-left at the ground or in the air
		if(GroundTrigger.Triggered)
			transform.Rotate(0, 30f * Input.GetAxis("Horizontal") * Time.deltaTime, 0, Space.World);
		else {
			transform.Rotate(0, 100f * _curveFlight * Time.deltaTime, 0, Space.World);
			
			// tilt (only in the air)
			// multiply with -1 to go into the right direction
			transform.Rotate(0, 0, 100f * (1f - AbsoluteCurveFlight - _blockFluctuate) * -Input.GetAxis("Horizontal") * Time.deltaTime);
		}
	}
	
	/// <summary>
	/// Rotates back.
	/// </summary>
	protected void RotateBack(){
	
		// rotate back when turn to odd direction
		if(180f != Rotation.z && 0 < Input.GetAxis ("Horizontal"))
			transform.Rotate(0, 0, 80f * _curveFlight * Time.deltaTime);
		
		// rotate back on z-axis if horizontal button isn't pressed
		if(!Input.GetButton("Horizontal")){
			if(135f > Rotation.z) 
				transform.Rotate(0, 0, -100f * AbsoluteCurveFlight * Time.deltaTime);
			if(225f < Rotation.z) 
				transform.Rotate(0, 0, 100f * AbsoluteCurveFlight * Time.deltaTime);
		}
		
		// rotate back on x-axis if vertical button isn't pressed
		if(!Input.GetButton("Vertical") && !GroundTrigger.Triggered)
			if(0 < Rotation.x){
				if(180f > Rotation.x)
					transform.Rotate(-50f * Time.deltaTime, 0, 0);
				//if(180f < Rotation.x) 
				//	transform.Rotate(50f * Time.deltaTime, 0, 0);
			}
	}
	
	/// <summary>
	/// Update the airplane.
	/// </summary>
	public void Update(){
		
		AirplaneAngleY= transform.eulerAngles.y; 
		
		// reset
		if(2 == GameState && (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return)))
			Reset();
		
		if(1 == GameState){
			rigidbody.AddRelativeForce(0, 0, _forceToCrash);
			GameState = 2;
		}
		
		// level restart
		if(Input.GetKey(KeyCode.F2))
			Restart();
		
		// moving
		if(0 == GameState){
			
			// set position and rotation
			Position = transform.position;
			Rotation = transform.eulerAngles;
			
			RotateAirplane();
			CalculateTilt();
			BlockTurn();
			RotateBack();
			Accelerate();
			Uplift();
			
			if(heightToMoveLandingGears < Position.y)
				Airplane.Instance.LiftLandingGearUp();
			if(!GroundTrigger.Triggered && heightToMoveLandingGears > Position.y)
				Airplane.Instance.LiftLandingGearDown();
	
			if(maxSpeedForDriving - 5f > Speed)
				Drive();
		}
	
		LimitArea();
	}
	
	/// <summary>
	/// Uplift.
	/// </summary>
	protected void Uplift(){
	
		// neutral speed and height if either accelerate nor deccelerate in the air
		// above this neutral value the airplane has to climb, with a lower speed it has to  sink
		if(maxSpeedForDriving - 5f < Speed && !Input.GetButton("Fire1") && !Input.GetButton("Fire2")){
			
			if(NeutralSpeed > Speed)
				Speed += acceleration * Time.deltaTime;
			if(NeutralSpeed < Speed) 
				Speed -= acceleration * Time.deltaTime;
		}
		
		// uplift
		transform.Translate(0, 0.1f * _uplift * Time.deltaTime, 0);
		_uplift = -NeutralSpeed + Speed;
		
		// no downlift at the ground
		if(GroundTrigger.Triggered && 0 > _uplift)
			_uplift = 0; 
	}

}