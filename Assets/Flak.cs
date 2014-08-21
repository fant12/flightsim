using UnityEngine;
using System.Collections;

public class Flak : MonoBehaviour {
	
	
	// attributes
	
	private GameObject _target;
	
	private Quaternion _turn;
	
	public float coolDown;
	
	public GameObject holder;
	
	public int maxDamage = 100;
	
	public float maxDistance = 800f;
	
	public float nextFireTime = 0.5f;
	
	public float rotationDamp = 4f;
	
	public GameObject tube;
	
	
	// properties
	
	public int Damage { get; set; }
	
	private bool ReadyToShoot { get; set; }
	
	private float TargetDamage {
		get { return _target.GetComponent<Airplane>().Damage; }
	}
	
	private bool TargetIsAvaiable {
		get { return _target && maxDistance > Vector3.Distance(transform.position, _target.transform.position); }
	}
	
	
	public void Awake(){
		_target = GameObject.FindGameObjectWithTag("Player");
	}
	
	public void Start(){
		Damage = 0;
		ReadyToShoot = true;
	}
	
	// Update is called once per frame
	public void Update(){
		
		_turn = TargetIsAvaiable ? Quaternion.LookRotation(_target.transform.position - holder.transform.position) : Quaternion.identity;
		holder.transform.rotation = Quaternion.Lerp(holder.transform.rotation, _turn, rotationDamp * Time.deltaTime);
		holder.transform.rotation = new Quaternion(holder.transform.rotation.x, holder.transform.rotation.y + 90f, holder.transform.rotation.z, holder.transform.rotation.w);
		
		/*if(0 >= TargetDamage)
			DestroyObject(_target);
		
		if(0 >= Damage)
			DestroyObject(gameObject);*/
	//	}
	}
}
/*

void OnDestroy(){
RaycastHit hit = new RaycastHit();
if (Physics.Raycast (transform.position, -transform.up,out hit,100,layerMask))
hit.collider.GetComponent<PlacementPlane>().PlacePlane = false;
}
void FireProjectile (){
readyToShoot = false;
CurrentTarget.GetComponent<Creature>().AddjustCurrentHealth(upgrade.getDamageUpdate(dmgLvl));
}*/