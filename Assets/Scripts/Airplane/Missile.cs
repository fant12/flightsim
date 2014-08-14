using UnityEngine;
using System.Collections;

public class Missile : Initializer {
	
	
	// attributes
	
	/// <summary>
	/// The explosion.
	/// </summary>
	public GameObject explosionParticle;
	
	// t = s div v
	// 17.7km div 2500km/h
	// t = 0.00708 h * 60 = 0.4248 min * 60 = 25.488 s
	
	/// <summary>
	/// The maximum fly time.
	/// </summary>
	public float maxFlyTime = 25.488f;
	
	/// <summary>
	/// The speed.
	/// </summary>
	public float speed = 2500f;
	
	/// <summary>
	/// Defines whether the missile is shooted or not.
	/// </summary>
	public bool shooted;
	
	
	// properties
	
	/// <summary>
	/// Gets or sets the distance.
	/// </summary>
	/// <value>
	/// The distance.
	/// </value>
	public float Distance { get; private set; }
	
	/// <summary>
	/// Gets or sets the fly time.
	/// </summary>
	/// <value>
	/// The fly time.
	/// </value>
	public float FlyTime { get; private set; }
	
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Missile"/> is shooted.
	/// </summary>
	/// <value>
	/// <c>true</c> if shooted; otherwise, <c>false</c>.
	/// </value>
	public bool Shooted { get; set; }
	
	/// <summary>
	/// Gets or sets the target.
	/// </summary>
	/// <value>
	/// The target.
	/// </value>
	public Transform Target { get; set; }
	
	
	// events
	
	public void OnCollisionEnter(Collision collision){
		Explode();
	}

	public void OnDestroy(){
		explosionParticle = SafeInstantiate(explosionParticle, transform.position, Quaternion.identity) as GameObject;	
	}

	
	// methods
	
	private void Explode(){		
		Destroy(gameObject);
	}
	
	public void Update(){
	
		if(Shooted){
		
			transform.Translate(speed * Time.deltaTime * Vector3.forward);				
			
			FlyTime += Time.deltaTime;
			
			if(maxFlyTime < FlyTime)
				Explode();
			
			if(Target){
				//Target.rotation *= Quaternion.Euler(-90f, 0, 0);
				//gameObject.transform.rotation *= Quaternion.Euler(-180f, 0, 0);
				//Target.Rotate(-180f, 0, 0);
				transform.LookAt(Target);
			}
		}
	}
}