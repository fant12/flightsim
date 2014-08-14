//Unity 2.61

//~ Sensor front up is part of the driving behaviour to keep the airplane aligned with the ground
//~ Sensor front iup st Teil der Fahreigenschaften um die Flugzeugneigung dem Gelände anzugleichen

static var sensorfrontup=0;

function OnTriggerEnter  (other : Collider) {
sensorfrontup=1;
}

function OnTriggerExit  (other : Collider) {
sensorfrontup=0;
}