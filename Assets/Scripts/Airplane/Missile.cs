using UnityEngine;
using System.Collections;

public class Missile : Explode {
	
	
	// attributes
		
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
	
	
	// methods
	
	public void Update(){
	
		if(Shooted){
			
			collider.enabled = true;
		
			transform.Translate(speed * Time.deltaTime * Vector3.forward);				
			
			FlyTime += Time.deltaTime;
			
			if(maxFlyTime < FlyTime)
				ExplodeSelf();
			
			if(Target){
				transform.LookAt(Target); //Target.Rotate(-180f, 0, 0);
			}
		}
	}
}