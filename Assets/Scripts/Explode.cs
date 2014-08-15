using UnityEngine;
using System.Collections;

public class Explode : Initializer {
	
	
	// attributes
	
	/// <summary>
	/// The explosion.
	/// </summary>
	public GameObject explosionParticle;

	
	// events
	
	public void OnCollisionEnter(Collision collision){
		
		// add explode ability to collision object and set necessary the particle effect
		if(!"Weapon".Equals(collision.collider.gameObject.tag)){
			collision.collider.gameObject.AddComponent<Explode>();
			collision.collider.gameObject.GetComponent<Explode>().explosionParticle = explosionParticle;
		}
		
		ExplodeSelf();
	}

	public void OnDestroy(){
		
		if(explosionParticle)
			explosionParticle = SafeInstantiate(explosionParticle, transform.position, Quaternion.identity) as GameObject;	
	}
	
	
	// methods
	
	protected void ExplodeSelf(){		
		Destroy(gameObject);
	}
}