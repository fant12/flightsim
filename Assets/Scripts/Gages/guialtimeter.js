//Unity 2.61

//~ Höhenmesser Instrument
//~ Atimeter Instrument

var  mainskin : GUISkin; //This is to connect the script with the guiskin

public var needleTexture: Texture2D = null;
public var smallneedletexture:Texture2D=null;
var	altimeter: Texture;

function OnGUI(){
GUI.skin =  mainskin; //The guiskin in use

//Flightheight is the airplane position Y minus 2. The wheels are the center for height, not the airplane middle. Which is around 2 ...
//Flightheight ist die Flugzeugposition weniger 2. Die Reifen sind Null Meter. Nicht der Flugzeugmittelpunkt, der auf ungefähr 2 liegt ...
flightheight=Mathf.Round(moveairplane.positiony-2);

//This is the tachotexture
GUI.Label( Rect( Screen.width/2+16, Screen.height-144, 128, 128 ), altimeter );	// x position, y position, size x, size y

//---------------------------------------------------------------------- textvalues --------------------------------------------------

GUI.contentColor = Color.black; 

	if (flightheight<=9){
		GUI.Label( Rect( Screen.width/2+16+50, Screen.height-144+78, 36, 30),"000" + flightheight );
		}
	else if ((flightheight>9)&&(flightheight<=99)){
		GUI.Label( Rect( Screen.width/2+16+50, Screen.height-144+78, 36, 30),"00" + flightheight);
		}
	else if ((flightheight>99)&&(flightheight<=999)){
		GUI.Label( Rect( Screen.width/2+16+50, Screen.height-144+78, 36,30),"0" +flightheight );
		}
	else if (flightheight>999){
		GUI.Label( Rect( Screen.width/2+16+50, Screen.height-144+78, 36, 30), flightheight +"");
		}


//----------------------------------------------------------------------------- big needle --------------------------------------------
	//Here comes the tachoneedle rotation
	var matrixBackup:Matrix4x4  = GUI.matrix;
	var thisAngle:float = -2+moveairplane.positiony*360/100;
	var pos:Vector2 = Vector2(Screen.width/2+16+64,Screen.height-144+64); // rotatepoint in texture plus x/y coordinates. our needle is at 16/16. Texture is 128/128. Makes middle 64 plus 16 = 80

	GUIUtility.RotateAroundPivot(thisAngle, pos);

	var thisRect:Rect = Rect(Screen.width/2+16,Screen.height-144,128,128); //x position, y position, size x, size y

	GUI.DrawTexture(thisRect, needleTexture,ScaleMode.ScaleToFit);  
	GUI.matrix = matrixBackup; 

//----------------------------------------------------------------------------- small needle --------------------------------------------
//Here comes the tachoneedle rotation
	var matrixBackup2:Matrix4x4  = GUI.matrix;
	var thisAngle2:float = moveairplane.positiony*36/100;
	var pos2:Vector2 = Vector2(Screen.width/2+16+64,Screen.height-144+64); // rotatepoint in texture plus x/y coordinates. our needle is at 16/16. Texture is 128/128. Makes middle 64 plus 16 = 80

	GUIUtility.RotateAroundPivot(thisAngle2, pos2);

	var thisRect2:Rect = Rect(Screen.width/2+16,Screen.height-144,128,128); //x position, y position, size x, size y

	GUI.DrawTexture(thisRect2, smallneedletexture,ScaleMode.ScaleToFit);  
	GUI.matrix = matrixBackup2; 

}
