Rubiks Cube Simulator
=====================

Provides a console application to display a flat over view of a Rubiks cube, to mimic the basic functionailty from https://rubiks-cube-solver.com/

As per the assignment the user can start with a solved Rubiks cube and by pressing 'Enter' it will run the commands as specified in the assignment. (A delay was added so the working can be shown)
The output of this has been checked against the same set of operations from https://rubiks-cube-solver.com/

The following operations are also avalible (Also shown in the console):
	[Esc] Exit application
	[Space] Reset Cube
	[Enter] Perfom assgnment steps F/R'/U/B'/L/D'- Can only be run from a solved state
	[F] Front
	[R] Right
	[U] Up
	[B] Back
	[L] Left
	[D] Down
	Holding shift and one of the keys above performs the operation in a counter clockwise direction.
	Without shift the operation is in a clockwise direction.

Executing the app
======================
The app can be launched by ruinning 'RubikCube.exe' located in the debug folder, alternatively the 'RubikCube.sln' visual studio file can be opened is visual studio is installed.

Caveats
======================
The Rubiks cube is currently fixed size with 6 faces and 9 blocks on each face.
The ouput to the console window is only for test purposes, and a limited colour plalette is avaliable.