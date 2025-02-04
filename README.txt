Project 2 Graphics and Interaction COMP30019
Daniel Schulz - schulzd (635700), Diana Ruth - druth (851465), Maile Naito - mnaito (853244)

Repository located at: https://github.com/dano000/Graphics-Proj2
Gameplay Video: https://youtu.be/qvOLykQiNpE
Certification Report is included: ValidationResult.htm

Enchanting Guess

Section 1
=========
This application is a very basic game, a version of the popular "Simon Says Game". There are three objects: a bowl, a metal cup, and a porcelain dish. Each object is clearly visible on a table in the middle of the scene and each object emits a different sound. A sequence of sounds (the sequence contains the sounds that are emitted by the objects) is played at the start of every round, and the user must input guesses for the sounds in the correct order to move on to the next level. There are 10 rounds in the game which get progresively more difficult. If the user fails a level, they can try that level again at the same difficulty until they get it right.

Section 2
=========
The start menu allows the user to enter the main game, view the instructions, and exit. On the instructions page, the user can view a description of how to play the game. The exit button allows the user to quit the game. When the user clicks on the "Let's Play!" button, the game will start at level one. A series of sounds are played in the order the user should repeat them back. Once the user has heard the sounds and hopefully memorized them, they will make their guesses. To zoom in on an object, the user will double-tap on the object they want to zoom in on. Once zoomed in, the user can hear the sound that object makes by single-tapping on it. Additionally, they can tilt the device to change the lighting on the objects. Then the user can click on the "Guess" button to lock in the guess. If the guess is incorrect, text saying "You lose!" will appear and a button will appear for the user to attempt the level again. If the guess is correct, the guess counter at the top left of the screen will increment and the user will need to put in their next guess. The user can then zoom back out by double-tapping anywhere outside the object to get back to the main view and make another selection. When the user has locked in all the correct guesses, text saying "You win!" and a button allowing the user to go to the next level will appear. After completing all 10 levels, the user wins the game.

Section 3
=========
All of the objects in the game except the fire particle system came from the Tavern library in the Unity Assets Store (source can be found in section 5). The sounds are from sources on the internet that are licensed under various creative commons licenses (sources can be found in section 5). The fire is a particle system that is included in the Unity Standard Assets ParticleSystem package. All other objects used in this project were created using standard Unity game objects. There are empty GameObjects that the scripts are attached to that allow the game to spawn all of the objects, make guesses, move to different levels, and control the game based on touch and accelerometer input from the user.

Section 4
=========
The Graphics (as well as the game), were rendered/made using the Unity Game Engine. Two custom shaders were written: one for the metal cup and one for the porcelain plate. These shaders are called CubeReflect.shader and DiffuseSpecBumpSurf.shader, respectively. More information about these shaders can be found in the subsequent sections.
The camera starts out at a starting position where the table, as well as all three sound objects, can be seen. When the user double-taps on an object, the raycastController script calculates the 2D coordinate that has been selected based on the input raycast, and the camera moves to "zoom in" on the object that was double-tapped. Similarly, when the user is zoomed in on an object and the user double-taps outside the object, that raycast tells the camera to move back to the starting position so all three objects can be seen again. This camera zooming method works for all three objects and all three objects can be zoomed in on and zoomed out from.

CubeReflect Shader
=============
The cube reflect shader takes a cubemap as a parameter. The cubemap is generated at the initialization of the game, by moving the camera to the position where the metal glass should be and creating a cubemap of the surroundings. That is, treating the environment as if it was encased by a cube. This creates a sort of texture on which to base the reflection of the object. For the vertex shader, the intersection of the light ray coming from the normal of the vertex and the skybox is found, which serves as the color of the vertex. Then the normal, view, and vertex positions are calculated in terms of the world and in terms of the camera. In the fragment shader, the color calculated in the vertex shader is broken down into ambient, diffuse, and specular components, and these components are manipulated as would happen using the Phong reflection model to create a dark metal effect for the object. Without manipulating the components individually, the surface looks like a perfect mirror of its surroundings. Splitting up the components, changing their parameters, and recombining them allows the object to look less like a mirror and more like metal.


MyCustomShader
==========================
This Shader utilizes Vertex and Fragment shaders to implement multiple lights. This was done using multiple passes, as outlined in the references specified. By taking in ambinent light in the first pass, and then additively blending each successive pass, by summing built-in Unity varibles which take the four most important point/spots light positions/colours and then (as done in labs) calculating the diffuse amount and the attenuation (the rate at which the light intensity falls). This is then passed from the vertex shader to the fragment shader using a macro, which then allows it to be used to calculate specific colours and intensity. 

Section 5 Attributions
======================
Unity Tavern Library
https://www.assetstore.unity3d.com/en/#!/content/62346

62055__juskiddink__glass5.wav 
Source:https://www.freesound.org/people/juskiddink/sounds/62055/
Licensed Under Creative Commins Attribution License

104012__rutgermuller__wood-scraping-little.aif
source: http://www.freesound.org/people/RutgerMuller/sounds/104012/
This work is licensed under the Creative Commons 0 License.

321488__dslrguide__scraping-stone.wav
source: http://www.freesound.org/people/dslrguide/sounds/321488/
This work is licensed under the Creative Commons 0 License.
