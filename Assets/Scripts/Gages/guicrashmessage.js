//Unity 2.61

//~ Wenn das Flugzeug crasht wird diese Nachricht angezeigt.
//~ When the airplane crashes this message gets displayed.

function OnGUI(){

if (moveairplane.gameover==2){
	GUI.Label( Rect( Screen.width/2-128, 32, 256, 320),"Crash. Bitte Enter oder Return drücken um das Flugzeug neu zu starten. Oder F2 um das Level neu zu starten\nCrash. Please press Enter or Return to restart airplane. Or F2 to restart the level");
}
}