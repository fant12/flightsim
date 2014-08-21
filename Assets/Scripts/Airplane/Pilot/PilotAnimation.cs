using UnityEngine;
using System.Collections;

public class PilotAnimation : MonoBehaviour {
	
	
	// attributes
	
	private Animation _anim;
	
	private bool _down = false;
	
	private bool _left = false;
	
	private bool _right = false;
	
	private bool _up = false;
	
	public AnimationClip centerLeft;
	
	public AnimationClip centerRight;
	
	public AnimationClip centerNoseUp;
	
	public AnimationClip centerNoseDown;
	
	public AnimationClip idle;
	
	public AnimationClip leftCenter;
	
	public AnimationClip noseDownCenter;
	
	public AnimationClip noseUpCenter;
	
	public AnimationClip rightCenter;
	
	
	// initializer
	
	public void Start(){
		_anim = gameObject.GetComponent<Animation>();	
	}
	
	
	// methods

	private void AxisTilt(){
		
		if(Input.GetButtonDown("Horizontal") && 0.1f < Input.GetAxisRaw("Horizontal")){
			PlayAnimation(centerLeft.name);	
			_left = true;
		}
		
		if(Input.GetButtonUp("Horizontal") && _left){
			PlayAnimation(leftCenter.name);
			_left = false;
		}
		
		if(Input.GetButtonDown("Horizontal") && 0.1f > Input.GetAxisRaw("Horizontal")){
			PlayAnimation(centerRight.name);
			_right = true;
		}
		
		if(Input.GetButtonUp("Horizontal") && _right){
			PlayAnimation(rightCenter.name);	
			_right = false;
		}
		
		if(Input.GetButtonDown("Vertical") && 0.1f > Input.GetAxisRaw("Vertical")){
			PlayAnimation(centerNoseUp.name);
			_up = true;
		}
		
		if(Input.GetButtonUp("Vertical") && _up){
			PlayAnimation(noseUpCenter.name);
			_up = false;
		}
		
		if(Input.GetButtonDown("Vertical") && 0.1f < Input.GetAxisRaw("Vertical")){
			PlayAnimation(centerNoseDown.name);	
			_down = true;
		}
		
		if(Input.GetButtonUp("Vertical") && _down){
			PlayAnimation(noseDownCenter.name);
			_down = false;
		}
		
	}
	
	private void PlayAnimation(string animName){
		_anim[animName].speed = 1f;
		_anim[animName].wrapMode = WrapMode.Once;
		_anim.CrossFade(animName);				
	}
	
	public void Update(){
		AxisTilt();
	}
}
