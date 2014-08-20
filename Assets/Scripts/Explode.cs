using UnityEditor;
using UnityEngine;
using System.Collections;

public class Explode : Initializer {
	
	
	// attributes
	
	/// <summary>
	/// Contains all tags which marks objects as not explodable.
	/// </summary>
	private static string[] _notExplodable;
	
	/// <summary>
	/// The explosion.
	/// </summary>
	public GameObject explosionParticle;
	
	
	// initializer
	
	static Explode(){
		_notExplodable = new string[] { "Weapon", "Terrain" };
	}
	
	// events
	
	public void OnCollisionEnter(Collision collision){
		
		// add explode ability to collision object and set necessary the particle effect
		if(!ArrayUtility.Contains<string>(_notExplodable, collision.collider.gameObject.tag)){
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