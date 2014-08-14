//Unity 2.61

//~ Tachometer

var  mainskin : GUISkin; //This is to connect the script with the guiskin

public var needleTexture: Texture2D = null;
var		tacho_texture 		: Texture;

function OnGUI(){

GUI.skin =  mainskin; //The guiskin in use
	
//This is the tachotexture
GUI.Label( Rect( Screen.width/2-144, Screen.height-144, 128,128 ), tacho_texture );	// x position, y position, size x, size y

//Here comes the tachoneedle rotation
var matrixBackup:Matrix4x4  = GUI.matrix;
var thisAngle:float = -128+moveairplane.speed*0.32;
var pos:Vector2 = Vector2(Screen.width/2-144+64,Screen.height-144+64); // rotatepoint in texture plus x/y coordinates. our needle is at 16/16. Texture is 128/128. Makes middle 64 plus 16 = 80

GUIUtility.RotateAroundPivot(thisAngle, pos);

var thisRect:Rect = Rect(Screen.width/2-144,Screen.height-144,128,128); //x position, y position, size x, size y

GUI.DrawTexture(thisRect, needleTexture,ScaleMode.ScaleToFit);  
GUI.matrix = matrixBackup; 

//---------------------------------------------- Debug ------------------------------------------------------------------------------------------
//~ //Speed value
//~ GUI.Label( Rect( Screen.width/2-144, Screen.height-144-20, 256, 256 ),"Speed   "+(Mathf.Round( moveairplane.speed)));
}