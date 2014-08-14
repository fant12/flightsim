//Unity 2.61
//Hier stellen wir den Blobshadow auf die Rotation des Flugzeugs ein. Um die Y Achse
//Here we rotate the blobshadow to the angle of the airplane. Around Y axis. 
function Update () {
transform.eulerAngles = Vector3(90,moveairplane.airplaneangley, 0);
}