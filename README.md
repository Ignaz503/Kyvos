# Kyvos

<<<<<<< HEAD
=======
## VIEW ON DEVELOP BRANCH

>>>>>>> main
A C# based game engine, very much in it's infancy like pre-pre-alpha infancy

## What's in it?

Following are all the projects associated with Kyvos.It includes a hopefully completed list of used technolgies, or credits to people from whom code was adapted. Cause people are a lot smarter than i ever could be

### Kyvos.Core

Core of the engine. Establishes how the application is structured, as well as logging capabilities and app config capabilites. As well as some basic FileSystem additions.
Also some superflous memory helper classes and functions for C# which are already handled by System.Runtime.CompilerServices.Unsafe.

- [Logging](https://serilog.net/)

### Kyvos.VeldridIntegration  

Integrates veldrid to create a game window, as well as allow for graphics capabilites
using Vlukan or OpenGL or DirectX or Metal

- [Veldrid](https://veldrid.dev/index.html)

### Kyvos.Input

Provides classes for keyboard and mouse input as well as gamepads
Furthermore integrates ECS System to make them easily useable with Kyvos.ECS

### Kyvos.ECS

Provides ECS capabilies to design your game

- [DefaultECS](https://github.com/Doraku/DefaultEcs)

### Kyvos.Audio

Provides a simple audio engine

- [NAudio](https://github.com/naudio/NAudio)

### Kyvos.Neworking

Provides networking capabilities using Enet

- [Enet-CSharp](https://github.com/nxrighthere/ENet-CSharp)

### Kyvos.GameStates

Provides game states which can represent things like a main menu, a pause menu
Done via a simple stack, where only the top most layer of the stack is update during the
applications update loop.

### Kyvos.Graphics

Utilizes Kyvos.VeldridIntegration to provide some graphics capabilities

### Kyvos.Maths

Provides some mathematical ideas

- [Fast Noise](https://github.com/Auburn/FastNoise)
- [Curvature Maths](https://github.com/Scrawk/Terrain-Topology-Algorithms/blob/master/Assets/TerrainTopology/Scripts/CreateCurvatureMap.cs)
- [Interpolation](http://paulbourke.net/miscellaneous/interpolation/)
- [Numerics and Math.Net](https://numerics.mathdotnet.com/)
- [Tweening](https://easings.net/)
- [Flow Maps](https://github.com/Scrawk/Terrain-Topology-Algorithms/blob/master/Assets/TerrainTopology/Scripts/CreateFlowMap.cs)
- [Land Form](https://github.com/Scrawk/Terrain-Topology-Algorithms/blob/master/Assets/TerrainTopology/Scripts/CreateLandformMap.cs)
- [Residual Map](https://github.com/Scrawk/Terrain-Topology-Algorithms/blob/master/Assets/TerrainTopology/Scripts/CreateResidualMap.cs)

#### Hydraulic Erosion

- [JobTalle](https://jobtalle.com/simulating_hydraulic_erosion.html#:~:text=Hydraulic%20erosion%20is%20the%20process,the%20rocky%20environment%20around%20it.)
- [SebLague](https://github.com/SebLague/Hydraulic-Erosion)

### FullyAutomatedGayLuxurySpaceCommunism

The test project for a lot of engine tests. In it's infancy when it comes to easy testing or running many tests at once, so the fully automated part is very much a lie.
