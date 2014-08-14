using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
	
	
	// attributes
	
	public float maxAttackDistance = 20f;
	
	public float repeatAttackDelay = 1f;
	
	public float rotationSpeed = 3f;
	
	// properties
	
	private float AttackTime { get; set; }
	
	private bool Chasing { get; set; }
	
	private Transform CurrentTransform { get; set; }
	
	public float Speed { get; set; }
	
	public Transform Target { get; set; }
	
	
	// initializer
	
	public void Awake(){
		CurrentTransform = transform;	
	}
	
	// Use this for initialization
	void Start () {
		AttackTime = Time.time;
		Chasing = false;
		Speed = 3;
		Target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	
	// events
	
	public void OnTriggerEnter(Collider collider){
		
		if("Player".Equals(collider.gameObject.tag))
			Chasing = true;
	}
	
	public void OnTriggerExit(Collider collider){
		
		if("Player".Equals(collider.gameObject.tag))
			Chasing = false;
	}
	
	// methods
	
	// Update is called once per frame
	void Update () {
	
		if(Chasing){
			
			// rotate to look at the player
			/*float distance = Target.position - CurrentTransform.position;
			CurrentTransform.rotation = Quaternion.Slerp(CurrentTransform.rotation, Quaternion.LookRotation(distance), rotationSpeed * Time.deltaTime);
			
			// move towards the player
			CurrentTransform.position += Speed * CurrentTransform.forward * Time.deltaTime;
			
			// attack
			if(Time.time > repeatAttackDelay){
				// do anything thats very bad
			}
			
			AttackTime = repeatAttackDelay + Time.time;*/
		}
	}
}