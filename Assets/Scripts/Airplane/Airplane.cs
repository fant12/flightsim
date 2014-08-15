using UnityEngine;
using System.Collections;

public class Airplane : MonoBehaviour {
	
	
	// attributes
	
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
	/// The front wheel.
	/// </summary>
	public GameObject frontWheel;
	
	/// <summary>
	/// The front wheel clap.
	/// </summary>
	public GameObject frontWheelClap;
	
	/// <summary>
	/// The left bomb.
	/// </summary>
	public GameObject leftBomb;
	
	/// <summary>
	/// The start position of the left bomb on the airplane.
	/// </summary>
	public GameObject[] leftBombPosition;
	
	/// <summary>
	/// The left missile.
	/// </summary>
	public GameObject leftMissile;
	
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
	public int maxHits;
	
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
	/// The duration to move a component.
	/// </summary>
	public float moveLandingGearDuration = 250f;
	
	/// <summary>
	/// The right bomb.
	/// </summary>
	public GameObject rightBomb;
	
	/// <summary>
	/// The start position of the right bomb on the airplane.
	/// </summary>
	public GameObject[] rightBombPosition;
	
	/// <summary>
	/// The right missile.
	/// </summary>
	public GameObject rightMissile;
	
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
	
	/// <summary>
	/// Gets or sets the current munition count.
	/// </summary>
	/// <value>
	/// The current munition count.
	/// </value>
	public int CurrentMunitionCount { get; set; }
	
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
	/// Gets the front wheel clap transform.
	/// </summary>
	/// <value>
	/// The front wheel clap transform.
	/// </value>
	public Transform FrontWheelClapTransform {
		get { return frontWheelClap.transform; }
	}
	
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
	/// The get left missile.
	/// </value>
	public Missile LeftMissile {
		get { return leftMissile.GetComponent<Missile>(); }
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
	/// Gets the left wheel clap transform.
	/// </summary>
	/// <value>
	/// The left wheel clap transform.
	/// </value>
	public Transform LeftWheelClapTransform {
		get { return leftWheelClap.transform; }
	}
	
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
	/// The get right missile.
	/// </value>
	public Missile RightMissile {
		get { return rightMissile.GetComponent<Missile>(); }
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
	
	/// <summary>
	/// Gets the right wheel clap transform.
	/// </summary>
	/// <value>
	/// The right wheel clap transform.
	/// </value>
	public Transform RightWheelClapTransform {
		get { return rightWheelClap.transform; }
	}
	
	
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
		SetMissile(ref leftMissile, leftMissilePosition);
		SetMissile(ref rightMissile, rightMissilePosition);
		
		
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
			new float[] { 0.44f, 0.15f, 0  } // right wheel
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
			Damage();
		
		if(maxHits < HitCount){
			if("Player".Equals(gameObject.tag))
				Level.Restart(gameObject.GetComponent<FlyAndMove>());
			else
				DestroyObject(gameObject);
		}
	}
	
	
	// methods
	
	private void Damage(){}
	
	/// <summary>
	/// Lifts the landing gear down.
	/// </summary>
	public void LiftLandingGearDown(){
		
		MoveWheels(ref _movementWheelsDown);
		
		if(!FrontClapIsOpen){
			Quaternion r = new Quaternion(0, 0, 0, 0);
    		frontWheelClap.transform.localRotation = Quaternion.Slerp(frontWheelClap.transform.localRotation, r, 5f);		
			//FrontWheelClapTransform.Rotate(Vector3.forward, 106.5f, Space.Self);
			FrontClapIsOpen = true;
		}
	}
	
	/// <summary>
	/// Lifts the landing gear up.
	/// </summary>
	public void LiftLandingGearUp(){
		
		MoveWheels(ref _movementWheelsUp);
		
		//FrontWheelClapTransform = Quaternion.Lerp(FrontWheelClapTransform.z,0f,(Time.time / duration));
		//FrontWheelClapTransform = new Quaternion(0,0,Mathf.Lerp(FrontWheelClapTransform.z,-106.5f,(Time.time / duration)),0);
			
		if(FrontClapIsOpen){
			Quaternion r = new Quaternion(0, 0, -106.5f, 0);
    		frontWheelClap.transform.localRotation = Quaternion.Slerp(frontWheelClap.transform.localRotation, r, 5f);
			//FrontWheelClapTransform.Rotate(Vector3.forward, Mathf.Lerp(FrontWheelClapTransform.rotation.z, -106.5f, Time.time / moveLandingGearDuration), Space.Self);
			FrontClapIsOpen = false;
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
	
	/// <summary>
	/// Recharge the airplane.
	/// </summary>
	public void Recharge(){
		
		if(IsChargeable){
		
			if(!LeftMissileReservoirIsManned){
				if(LeftReloadTime > maxReloadTime){
					SetMissile(ref leftMissile, leftMissilePosition);
					LeftMissile.Shooted = false;
				}
				
				LeftReloadTime += Time.deltaTime;
			}
				
			if(!RightMissileReservoirIsManned){
				if(RightReloadTime > maxReloadTime){
					SetMissile(ref rightMissile, rightMissilePosition);
					RightMissile.Shooted = false;
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
			LeftMissile.Target = target;
			LeftMissile.Shooted = true;
			leftMissile.gameObject.transform.parent = null;
			leftMissilePosition.gameObject.tag = "Disarmed";
			LeftReloadTime = 0;
		}
		else if(RightMissileReservoirIsManned){
			RightMissile.Target = target;
			RightMissile.Shooted = true;
			rightMissile.gameObject.transform.parent = null;
			rightMissilePosition.gameObject.tag = "Disarmed";
			RightReloadTime = 0;
		}
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	public void Update() {
		
		Recharge();
	
		//	Fuel
		Fuel -= Time.deltaTime;
		
		if(maxFuel <= Fuel)
			FlyAndMove.GameState = 2;
	}
}