using UnityEngine;
using System.Collections;

public class FlyAndMoveEnemy : FlyAndMove {

	// Update is called once per frame
	public new void Update() {
	
		AirplaneAngleY = transform.eulerAngles.y; 
		
		// set position and rotation
		Position = transform.position;
		Rotation = transform.eulerAngles;
		
		RotateAirplane();
		CalculateTilt();
		BlockTurn();
		RotateBack();
		Accelerate();
		Uplift();
	
		if(maxSpeedForDriving - 5f > Speed)
			Drive();
	
		LimitArea();
	}
}
