		1/20/2022
I've got the map showing up, added infrastructure so that each tile knows its coordinate and tile type.
The next thing that should be done is implementing a pathfinding AI that can reach the endpoint.

		1/22/2022
Managed to upload the project to GitHub, very very nice.
I've converted the LevelManager class to a static class. Now I just need to call its initialization script and we should be back to regular functionality.
I'm partially done with basic enemy implementation, but I will need to work on the level manager initialization first.
TODO for today:
Add basic main menu with which to start the game.
Create a unity editor script to create enemies.

		1/23/2022
Yesterday I finished the TODOs, as well as basic enemy movement too.
Don't really know what to do today besides implementing basic tower logic.
TODO for today:
Implement basic tower logic
Maybe start implementing the UI, so that we can create towers

		1/24/2022
Yesterday I implemented the tower logic, and the core aspects of the game are starting to take shape
I've also decided to use ScriptableObjects to store enemy and tower data
TODO for today:
Implement ability to create towers during runtime
Maybe look into using ScriptableObjects to store single variables (https://www.youtube.com/watch?v=raQ3iHhE_Kk)
dunno, maybe take it easy or something
Give Tiles a reference to their tower, and vice versa


	TODO:
implement basic unit object (literally just a sphere)
units should, wherever they spawn, attempt to seek the exit point

implement turrets
implement stats for units, so that they can take damage
