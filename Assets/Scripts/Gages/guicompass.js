//Unity 2.61

//~ Kompass Instrument
//~ Compass Instrument

var  mainskin : GUISkin; //This is to connect the script with the guiskin

/* This example uses multiple Groups to clip the displayed Contents, as found in the GUI tutorial */

var bgImage : Texture2D; // background image, in our case the compass case
var fgImage : Texture2D; // foreground image, i our case the compass inlay
var compass = -95; // Unsere Offset Value fürs Inlay || Our Offset Value for the inlay

function OnGUI () {

GUI.skin =  mainskin; //The guiskin in use
	
	//Here we calculate the offset, dependand of the airplane Y angle. I calculate with 360 minus airplane angle because my compass inlay counts in the other direction. I could`ve also redone the compass inlay instead. -5 is calibrating the compass to north-south
	//Hier kalkulieren wir das Offset, abhänging vom Flugzeugwinkel um Y. Ich rechne hier mit 360 weniger Flugzeugwinkel Y weil mein Kompass in die andere Richtung anzeigt. Ich hätte natürlich auch einfach das Kompassinlay Grafik neu machen können. -5 kalibriert den Kompass auf Nord-Süd
	
	compass = ((360-moveairplane.airplaneangley)*-0.5)-5;
	
	// Create one Group to contain both images
	// Adjust the first 2 coordinates to place it somewhere else on-screen
	GUI.BeginGroup (Rect (Screen.width/2+176,Screen.height-144,256,256));

	// Create a second Group which will be clipped
	// We want to clip the image and not scale it, which is why we need the second Group	
	GUI.BeginGroup (Rect (24,40,80,48));

	// Draw the compass inlay
	GUI.Label (Rect (compass,0,512,512), fgImage);

	// End Group
	GUI.EndGroup ();
	
	// Draw the compass case image
    GUI.Label (Rect (0,0,128,128), bgImage);
	
	//End group
	GUI.EndGroup ();
	
}