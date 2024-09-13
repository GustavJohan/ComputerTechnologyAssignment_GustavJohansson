All objects in game use unity dots.
As i understood it using Jobs for movement was more for demonstration purposes, due to this and my previos technical issues with jobs i opted to use sytemAPI queries depite it only iterating over single components. 
To my understanding this shouldnt have a performance impact.
other than that i burst compiled all commonly called functions, particularly update functions

the games functionality is as follows, 
A player that can move
The ability to spawn "bullets"
bullets will travel in a direction and despawn after a few seconds
Spawning enemies
enmeies will move towards the player

WS to move bakwards and forwards
AD to rotate side to side

SPACE to shoot
