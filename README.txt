Project 2 Graphics and Interaction COMP30019
Daniel Schulz - schulzd (635700), Diana Ruth - druth (851465
), Maile Naito - mnaito (853244)

Enchanting Guess

Section 1
=========
This application is a very basic game, a version of the popular "Simon Says Game". 
Each object displayed on a table, emmits a sound. 
Sounds are played at the start of every round, and this pattern must be matched to coninue to the next round.
There are 10 rounds in the game which get progresively more difficult.

Section 2
=========
The start menu, allows the user to enter the main game, view the instructions and exit.

Within the main game:
Each item on the table makes a different sound. 
Imitate the sounds produced at the beginning of each level by guessing with the correct object. 
Guessing correctly will move you on to the next level. 

	
*Double tap an object to zoom in/out
	
*When zoomed in, single tap to hear the sound it makes

	*When zoomed in, tilt to rotate the object
	
*Touch the guess button to make a guess

Section 3
=========
Objects were obtained from the Unity Asset store, while textures, sound and other assets were obtained from sources the internet.

Section 4
=========
The Graphics (as well as the game), were rendered/made using the Unity Game Engine. Two specific custom shaders were written. 
Included shaders from Unity, were also used, as well as other custom shaders from asset packages.

CubeReflect Shader
=============




DiffuseSpecularBumpmapShader
============================
This shader was written, exploting the use of surface shaders within Unity's Shader Language. 
This allowed for a style of PhongBlinn shading to be implemented, as well as, adding a Bumpmap.
Textures are samples, on each 2D point (ie. pixel), for Diffuse, Specular and BumpMap components.
Diffuse RGB components sets the Albedo component(main colour) of the surface, Specular components RGB are multiplied,
ontop of of Diffuse components, while BumpMap components (ie. from the BumpMap texture) are unpacked to the Normal component
of the output. Thus giving a surface which appears to be slightly bumpy, when paired with the correct bumpmap textures.

Section 5 Attributions
======================
62055__juskiddink__glass5.wav 
Source:https://www.freesound.org/people/juskiddink/sounds/62055/
Licensed Under Creative Commins Attribution License

104012__rutgermuller__wood-scraping-little.aif
source: http://www.freesound.org/people/RutgerMuller/sounds/104012/
This work is licensed under the Creative Commons 0 License.

321488__dslrguide__scraping-stone.wav
source: http://www.freesound.org/people/dslrguide/sounds/321488/
This work is licensed under the Creative Commons 0 License.