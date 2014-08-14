//Unity 2.61

//~ Horizont Instrument
//~ Horizon Instrument

//This instrument works unfortunately just partially yet. I don`t find way to retreive the real angles from my airplane. Just euler angles. 
//Which flips around by 180 degrees from time to time. Which leads of course to the same behaviour in my instrument. 
//I have no clue how to work around this :/

//Dieses Instrument arbeitet zur Zeit leider nur Teilweise. Ich finde keine Möglichkeit den wirklichen Winkel meines Flugzeugs zu ermitteln 
//Nur die Euler Winkel. Und diese drehen sich von Zeit zu Zeit um 180 Grad um. Was natürlich dazu führt dass es meinem Instrument genauso ergeht. 
//Ich habe keine Ahnung wie ich das kompensieren soll :/

var  mainskin : GUISkin; //This is to connect the script with the guiskin

var bgImage : Texture2D; // background image that is 256 x 32
var fgImage : Texture2D; // foreground image that is 256 x 32
var pitch = 0; // a float between 0.0 and 1.0
var tilt:float=0.0;

function OnGUI () {

	GUI.skin =  mainskin; //The guiskin in use

	//Frontneigung || Pitch forward backward
	//210 ist der Zentrierpunkt in der Textur, der Horizont auf Null || 210 is the centerpoint in the texture, the horizon at 0
	if (moveairplane.rotationx < 180) pitch = -210-moveairplane.rotationx;
	else pitch = -210-moveairplane.rotationx+360;
	// Seitenneigung || Tilt sidewards
	tilt=moveairplane.rotationz;

	// Create one Group to contain both images
	// Adjust the first 2 coordinates to place it somewhere else on-screen
	GUI.BeginGroup (Rect ( Screen.width/2-304, Screen.height-144, 128, 128 ));

	// Create a second Group which will be clipped
	// We want to clip the image and not scale it, which is why we need the second Group	
	GUI.BeginGroup (Rect (22,19,84,92));

	// Draw the foreground image
	//---------------------------------------------------------
	//Here comes the rotation
	var matrixBackup:Matrix4x4  = GUI.matrix;
	//var thisAngle:float = tilt;
	var pos:Vector2 = Vector2(42,45); // rotatepoint in texture. 
	
	GUIUtility.RotateAroundPivot(tilt, pos);

	var thisRect:Rect = Rect(0,pitch,84,512); //x position, y position, size x, size y

	GUI.DrawTexture(thisRect, fgImage,ScaleMode.ScaleToFit);
	GUI.matrix = matrixBackup; 
	
	// End group
	GUI.EndGroup ();
	// Draw the background image. In front of the other texture. That`s why it is here.
	//Hintergrundbild zeichnen. Vor der anderen Textur. Deswegen steht sie hier.
    GUI.Label (Rect (0,0,128,128), bgImage);
	// End group
	GUI.EndGroup ();
		
	
}