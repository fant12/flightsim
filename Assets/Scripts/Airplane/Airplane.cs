using UnityEngine;
using System.Collections;

public class Airplane : MonoBehaviour {
	
	
	// attributes
	
	/// <summary>
	/// The left missile.
	/// </summary>
	private GameObject _leftMissile;
	
	/// <summary>
	/// The right missile.
	/// </summary>
	private GameObject _rightMissile;
	
	/// <summary>
	/// The movement to lift the wheels down.
	/// </summary>
	private float[][] _movementWheelsDown;
	
	/// <summary>
	/// The movement to lift the wheels up.
	/// </summary>
	private float[][] _movementWheelsUp;
	
	/// <summary>
	/// The wheels.
	/// </summary>
	private GameObject[] _wheels;
	
	/// <summary>
	/// The bomb.
	/// </summary>
	public GameObject bomb;
	
	/// <summary>
	/// The bombs.
	/// </summary>
	public GameObject[] bombs;
	
	/// <summary>
	/// The front wheel.
	/// </summary>
	public GameObject frontWheel;
	
	/// <summary>
	/// The front wheel clap.
	/// </summary>
	public GameObject frontWheelClap;
	
	/// <summary>
	/// The start position of the left bomb on the airplane.
	/// </summary>
	public GameObject leftBombPosition;
	
	/// <summary>
	/// The start position of the left missile on the airplane.
	/// </summary>
	public GameObject leftMissilePosition;
	
	/// <summary>
	/// The left wheel.
	/// </summary>
	public GameObject leftWheel;
	
	/// <summary>
	/// The left wheel clap.
	/// </summary>
	public GameObject leftWheelClap;
	
	/// <summary>
	/// The maximum fuel.
	/// </summary>
	public int maxFuel = 3986;
	
	/// <summary>
	/// The maximum hits to destroy an airplane.
	/// </summary>
	public int maxHits = 2;
	
	/// <summary>
	/// The maximum count of missiles.
	/// </summary>
	public int maxMissiles = 6;
	
	/// <summary>
	/// The maximum reload time to shoot a missile.
	/// </summary>
	public int maxReloadTime = 30;
	
	/// <summary>
	/// The missile.
	/// </summary>
	public GameObject missile;
	
	/// <summary>
	/// The duration to move a clap.
	/// </summary>
	public float moveClapDuration = 5f;
	
	/// <summary>
	/// The duration to move a component.
	/// </summary>
	public float moveLandingGearDuration = 250f;
		
	/// <summary>
	/// The start position of the right bomb on the airplane.
	/// </summary>
	public GameObject rightBombPosition;
		
	/// <summary>
	/// The start position of the right missile on the airplane.
	/// </summary>
	public GameObject rightMissilePosition;
	
	/// <summary>
	/// The right wheel.
	/// </summary>
	public GameObject rightWheel;
	
	/// <summary>
	/// The right wheel clap.
	/// </summary>
	public GameObject rightWheelClap;
	
	
	// properties
	
	public int CurrentBombCount { get; set; }
	
	/// <summary>
	/// Gets or sets the current munition count.
	/// </summary>
	/// <value>
	/// The current munition count.
	/// </value>
	public int CurrentMunitionCount { get; set; }
	
	public float Damage { get; set; }
	
	/// <summary>
	/// Gets or sets the fuel.
	/// </summary>
	/// <value>
	/// The fuel.
	/// </value>
	public float Fuel { get; set; }
	
	/// <summary>
	/// Gets or sets a value indicating whether the front clap is open.
	/// </summary>
	/// <value>
	/// <c>true</c> if front clap is open; otherwise, <c>false</c>.
	/// </value>
	private bool FrontClapIsOpen { get; set; }
		
	/// <summary>
	/// Gets or sets the hit count.
	/// </summary>
	/// <value>
	/// The hit count.
	/// </value>
	public int HitCount { get; private set; }
	
	/// <summary>
	/// Gets or sets the singleton instance.
	/// </summary>
	/// <value>
	/// The instance.
	/// </value>
	public static Airplane Instance { get; private set; }
	
	/// <summary>
	/// Gets a value indicating whether the airplane munition is chargeable.
	/// </summary>
	/// <value>
	/// <c>true</c> if this instance is chargeable; otherwise, <c>false</c>.
	/// </value>
	public bool IsChargeable { 
		get { return maxMissiles >= CurrentMunitionCount; }
	}
	
	/// <summary>
	/// Gets or sets a value indicating whether the left clap is open.
	/// </summary>
	/// <value>
	/// <c>true</c> if left clap is open; otherwise, <c>false</c>.
	/// </value>
	private bool LeftClapIsOpen { get; set; }
	
	/// <summary>
	/// Gets the left missile.
	/// </summary>
	/// <value>
	/// The left missile.
	/// </value>
	public GameObject LeftMissile {
		get { return _leftMissile; }
	}
	
	/// <summary>
	/// Gets the left missile.
	/// </summary>
	/// <value>
	/// The get left missile.
	/// </value>
	public Missile LeftMissileScript {
		get { return _leftMissile.GetComponent<Missile>(); }
	}
	
	/// <summary>
	/// Gets a value indicating whether the left missile reservoir is manned.
	/// </summary>
	/// <value>
	/// <c>true</c> if left missile reservoir is manned; otherwise, <c>false</c>.
	/// </value>
	public bool LeftMissileReservoirIsManned { 
		get { return leftMissilePosition && "Armed".Equals(leftMissilePosition.gameObject.tag); } 
	}
	
	/// <summary>
	/// Gets or sets the left reload time.
	/// </summary>
	/// <value>
	/// The left reload time.
	/// </value>
	public float LeftReloadTime { get; private set; }
		
	/// <summary>
	/// Gets or sets a value indicating whether the right clap is open.
	/// </summary>
	/// <value>
	/// <c>true</c> if right clap is open; otherwise, <c>false</c>.
	/// </value>
	private bool RightClapIsOpen { get; set; }
	
	/// <summary>
	/// Gets the right missile.
	/// </summary>
	/// <value>
	/// The right missile.
	/// </value>
	public GameObject RightMissile {
		get { return _rightMissile; }
	}
	
	/// <summary>
	/// Gets the right missile.
	/// </summary>
	/// <value>
	/// The get right missile.
	/// </value>
	public Missile RightMissileScript {
		get { return _rightMissile.GetComponent<Missile>(); }
	}
	
	/// <summary>
	/// Gets a value indicating whether the right missile reservoir is manned.
	/// </summary>
	/// <value>
	/// <c>true</c> if right missile reservoir is manned; otherwise, <c>false</c>.
	/// </value>
	public bool RightMissileReservoirIsManned { 
		get { return rightMissilePosition && "Armed".Equals(rightMissilePosition.gameObject.tag); } 
	}
	
	/// <summary>
	/// Gets or sets the right munition reload time.
	/// </summary>
	/// <value>
	/// The right reload time.
	/// </value>
	public float RightReloadTime { get; private set; }
	
	
	// initializer
	
	/// <summary>
	/// Awake the airplane object.
	/// </summary>
	public void Awake(){
		Instance = this;
	}
	
	/// <summary>
	/// Start the airplane.
	/// </summary>
	public void Start(){
		
		// equip
		
		SetMissile(ref _leftMissile, leftMissilePosition);
		SetMissile(ref _rightMissile, rightMissilePosition);
		
		PrepareBombs();
		
		
		// airplane wheel positions
		
		// airplane wheel clap is opened at the beginning
		FrontClapIsOpen = LeftClapIsOpen = RightClapIsOpen = true;
		
		_wheels = new GameObject[] { frontWheel, leftWheel, rightWheel };
		
		_movementWheelsDown = new float[][] {
			new float[] { 0.14f, -0.6f, 0.68f }, // front wheel
			new float[] { 0.79f, -0.2f, 0 }, // left wheel
			new float[] { 0.77f, -0.2f, 0 } // right wheel
		};
		
		_movementWheelsUp = new float[][] {
			new float[] { 0.14f, -0.24f, 0 }, // front wheel
			new float[] { 1.09f, 0.2f, 0.2f  }, // left wheel
			new float[] { 0.44f, 0.2f, 0  } // right wheel
		};
	}
	
	
	// events
	
	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name='collider'>
	/// Collider.
	/// </param>
	public void OnTriggerEnter(Collider collider){
		
		if(((int)(0.5f * maxHits)) < HitCount)
			Demolish();
		
		if(maxHits < HitCount){
			if("Player".Equals(gameObject.tag))
				Level.Restart(gameObject.GetComponent<FlyAndMove>());
			else
				DestroyObject(gameObject);
		}
	}
	
	
	// methods
	
	private void Demolish(){}
	
	public void DropBomb(){
	
		if(0 < CurrentBombCount){
			
			const float t = 0.2f;
			
			// search first non null object
			for(int i = 0; bombs.Length > i; ++i)
				if(bombs[i]){
					//bombs[i].GetComponent<Bomb>().IsReady = true;
					bombs[i].collider.enabled = true;
				
					// set bomb on a position thats can not collide the airplane self
					Vector3 p = bombs[i].transform.localPosition;
					bombs[i].transform.localPosition = new Vector3(
														Mathf.Lerp(p.x, 0, t), 
														Mathf.Lerp(p.y, -7f, t), 
														Mathf.Lerp(p.z, 0, t));
					
					// detach the bomb from parent and add a velocity
					bombs[i].transform.parent = null;
					bombs[i].rigidbody.velocity = 0.5f * rigidbody.velocity;
					bombs[i].rigidbody.useGravity = true;
					
					// remove bomb reference from array
					bombs[i] = null;
					break;
				}
			
			--CurrentBombCount;
		}	
	}
	
	/// <summary>
	/// Lifts the landing gear down.
	/// </summary>
	public void LiftLandingGearDown(){
		
		MoveWheels(ref _movementWheelsDown);
		
		Quaternion r = new Quaternion(0, 0, 0, 0);
		
		if(!FrontClapIsOpen){
    		frontWheelClap.transform.localRotation = Quaternion.Slerp(frontWheelClap.transform.localRotation, r, moveClapDuration);		
			FrontClapIsOpen = true;
		}
		
		if(!LeftClapIsOpen){
			leftWheelClap.transform.localRotation = Quaternion.Slerp(leftWheelClap.transform.localRotation, r, moveClapDuration);
			LeftClapIsOpen = true;
		}
		
		if(!RightClapIsOpen){
			rightWheelClap.transform.localRotation = Quaternion.Slerp(rightWheelClap.transform.localRotation, r, moveClapDuration);
			RightClapIsOpen = true;
		}
	}
	
	/// <summary>
	/// Lifts the landing gear up.
	/// </summary>
	public void LiftLandingGearUp(){
		
		MoveWheels(ref _movementWheelsUp);
		
		if(FrontClapIsOpen){
    		frontWheelClap.transform.localRotation = Quaternion.Slerp(frontWheelClap.transform.localRotation, new Quaternion(0, 0, -53.25f, 0), moveClapDuration);
			FrontClapIsOpen = false;
		}
		
		if(LeftClapIsOpen){
    		leftWheelClap.transform.localRotation = Quaternion.Slerp(leftWheelClap.transform.localRotation, new Quaternion(0, 0, 50.6f, 0), moveClapDuration);
			LeftClapIsOpen = false;
		}
		
		if(RightClapIsOpen){
    		rightWheelClap.transform.localRotation = Quaternion.Slerp(rightWheelClap.transform.localRotation, new Quaternion(0, 0, -50.6f, 0), moveClapDuration);
			RightClapIsOpen = false;
		}
	}
	
	private void MoveClaps(ref GameObject clap, Quaternion rotation, ref bool isOpen, float duration){
		
		if(!isOpen){
    		clap.transform.localRotation = Quaternion.Slerp(clap.transform.localRotation, rotation, duration);
			isOpen = true;
		}
		else {
			clap.transform.localRotation = Quaternion.Slerp(clap.transform.localRotation, rotation, duration);	
			isOpen = false;
		}
	}

	/// <summary>
	/// Moves the airplane wheels.
	/// </summary>
	/// <param name='movement'>
	/// Movement.
	/// </param>
	private void MoveWheels(ref float[][] movement){
		
		float t = Time.time / moveLandingGearDuration;
		
		for(int i = 0; movement.Length > i; ++i){
			
			Vector3 p = _wheels[i].transform.localPosition;
			_wheels[i].transform.localPosition = new Vector3(
														Mathf.Lerp(p.x, movement[i][0], t), 
														Mathf.Lerp(p.y, movement[i][1], t), 
														Mathf.Lerp(p.z, movement[i][2], t)); 
		}
	}
	
	private void PrepareBombs(){
	
		if(0 == CurrentBombCount){
		
			bombs = new GameObject[6];
			CurrentBombCount = bombs.Length;
			
			for(int i = 0, j = 0; bombs.Length > i; ++i, ++j){
				
				if(2 < j)
					j = 0;
				
				SetBomb(ref bombs[i], (2 < i) ? rightBombPosition : leftBombPosition, j);
				bombs[i].name = "bomb" + i;
			}
		}
	}
	
	/// <summary>
	/// Recharge the airplane.
	/// </summary>
	public void Recharge(){
		
		if(IsChargeable){
		
			if(!LeftMissileReservoirIsManned){
				if(LeftReloadTime > maxReloadTime){
					SetMissile(ref _leftMissile, leftMissilePosition);
					LeftMissileScript.Shooted = false;
				}
				
				LeftReloadTime += Time.deltaTime;
			}
				
			if(!RightMissileReservoirIsManned){
				if(RightReloadTime > maxReloadTime){
					SetMissile(ref _rightMissile, rightMissilePosition);
					RightMissileScript.Shooted = false;
				}
				
				RightReloadTime += Time.deltaTime;
			}
		}
	}
	
	/// <summary>
	/// Refuel the fuel level.
	/// </summary>
	private void Refuel(){
		
		if(maxFuel > Fuel)
			Fuel = maxFuel;
	}
	
	private void SetBomb(ref GameObject bombObject, GameObject bombPosition, int position){
		
		Vector3 dif;
		const float distanceBetweenABomb = 0.7f;
		const float distanceBetweenWing = 0.4f;
		
		// calc position
		switch(position){
			case 1: // left
				dif = new Vector3(-distanceBetweenABomb, -distanceBetweenWing, 0);
				break;
			case 2: // right
				dif = new Vector3(distanceBetweenABomb, -distanceBetweenWing, 0);
				break;
			default: // center
				dif = new Vector3(0, -distanceBetweenWing, 0);
				break;
		}
		
		bombObject = Instantiate(bomb, bombPosition.transform.position + dif, transform.rotation) as GameObject;
		bombObject.transform.parent = transform;
		bombObject.transform.Rotate(90f, 0, 0);
		bombObject.rigidbody.useGravity = false;
		bombPosition.gameObject.tag = "Armed";
	}
	
	/// <summary>
	/// Sets the missile.
	/// </summary>
	/// <param name='missileObject'>
	/// Missile object.
	/// </param>
	/// <param name='missilePosition'>
	/// Missile position.
	/// </param>
	private void SetMissile(ref GameObject missileObject, GameObject missilePosition){
		
		// instantiate in right orientation
		missileObject = Instantiate(missile, missilePosition.transform.position, missilePosition.transform.rotation) as GameObject;
		missileObject.transform.parent = transform;
		missilePosition.gameObject.tag = "Armed";
	}
	
	/// <summary>
	/// Shoot the specified target.
	/// </summary>
	/// <param name='target'>
	/// Target.
	/// </param>
	public void Shoot(Transform target){
		
		if(LeftMissileReservoirIsManned){
			LeftMissileScript.Target = target;
			LeftMissileScript.Shooted = true;
			_leftMissile.gameObject.transform.Translate(0, -2f, 0);
			_leftMissile.gameObject.transform.parent = null;
			leftMissilePosition.gameObject.tag = "Disarmed";
			LeftReloadTime = 0;
		}
		else if(RightMissileReservoirIsManned){
			RightMissileScript.Target = target;
			RightMissileScript.Shooted = true;
			_rightMissile.gameObject.transform.Translate(0, -2f, 0);
			_rightMissile.gameObject.transform.parent = null;
			rightMissilePosition.gameObject.tag = "Disarmed";
			RightReloadTime = 0;
		}
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	public void Update() {
		
		// ammo
		
		if(Input.GetKeyUp(KeyCode.Space))
			DropBomb();
		
		Recharge();
		
		if(Input.GetKeyUp(KeyCode.B))
			PrepareBombs();
	
		//	Fuel
		Fuel -= Time.deltaTime;
		
		if(maxFuel <= Fuel)
			FlyAndMove.GameState = 2;
	}
}