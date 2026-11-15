## todo
- [ ] boom vs cave
- [ ] qwerty

an artificial world consisting of images and sound created by a computer and affected by the actions of the person experiencing it - vr

### four elements of vr - TAUO
- targeted behavior - experience designed by the creator, intentionall not random
- artificail sensory stimulation - vr replaces or enhances natural sensory input
- unawareness - the organism is unaware of the interference
- organism - the one experiencing vr

### immersion
- the user need to be part of the simulated world rather than the simulated world being a feature of the users world
- degree of immersion increases with moer and more consistent sensory modalities,body tracking,richer bony representation;decreased lag
- three Is of the VR
  - immersion 
  - interaction 
  - imagination
### five components of vr system
- vr engine
- i/o devices
- software and dbs
- user 
- task
### types of vr
- immersive
  - head referenced viewing - natural interface for navigating 3d space
  - stereoscopic viewing - enhances depth perception and sense of space
  - full scale representation
  - realistic interaction
  - illusion enhanced by auditory,haptic
  - network application
- shared 
  - users at different locations meet inthe same virtual works, all see the same VE from thier own viewpoint;each is shown as a virtual human(avatar) they cans ee vommunicated with and interact as a team
- non immersive
  - mouse controlled navigaion through 3d environment ona  monitor, stereo viewing via glassses
### VRML (Virtual reality modeling language)
- brings 3d worlds with integrated hyperlinks to the web 
vr - fully simulated environment
ar - digital overlays on the real world
mr - combination of real and virtual 
xr - umbrella term for all realities

*unity* - mostly widely used in vr,ar,mr dev 
*vuforia engine* - soft platform for creating ar apps; add computer vision functionality so apps can recognise images and objects with space in the real world

a combination of real scene viewed by a user and a virtual scene by a computer that augments the scene with additional information - ar

ar aims to create automatic, direct and intuitive links between the real world and digital data

### characteristics of AR
1. combine real and virtual elements
2. be interactive in real time
3. be registered in 3ds
### types of ar devices
1. heads of display (HUD) - transparent display that represents data without requiring users to look away from their usual view point
2. holographic displays - extract people/objects from the real world in real time and immerse them into VE where they can interact
3. smart glasses - wearable transparent device that can generate ar content in the users viewpoint; the user sees surroundings as with ordinary glasses
4. handheld - popular;basic hardware requirement touch screen interaction is a common method
### ar tracking techniques
- sensory based - optical,magnetic,acoustic,inertial
- vision based - camera/image based
#### advantages of ar
- reduces cognitive laoda
- context aware info
- improves accuracy and task perforamance
- enhances learning and training
- natural 
#### disadvantages
 - accurate tracking and registration
 - sensor inaccuracies, environmental variabilities
 - hardware limitations and latency
 - usability and ergonomics
 - scalability and robustness in real world environments
## mixed reality 
- blends real adn virual worlds seamlessly; digital content dynamically integrates with and responds to the physical environment
- digital content dynamically integrates with and responds to the physical environment
- *how?* 
  1. `sensor fusion` - combine data from cameras,depth sensors,IMUs
  2. `real time spatial mapping` 
- core pillars
  1. environmental perception - spatial mapping, scene understanding, world- anchoring
  2. advanced input systems - hand tracking, gesture input
  3. cloud and edge computing 
  4. high fidelity rendering - gpu accelerated holographic ;realistic occulsion; spatial sound
### applications of vr
- educational - immersive learning
- healthcare - medical training
- architecture - visualization before constuction
- gaming - highly immersive;interactive
- entertainment - concerts, movies ,theme park simulations
- industry - safety training
### application of ar
- industry - overlays cad models on physical structures
- maintenance - shows step by step instructions directly on equipment
- medical - accuvien (ar diagnosis)
- navigation - ar browsers
- media, gaming - live tv overlays
- retail - interactive shopping
- tourism - ar city
- education - immersion - 3d bear, aurasama
### application of mr
- digital twins - realtime 3d replicas
- remote collaboration 
- training and simulation
- material science and fabrication
### hardware
- HMD - head mounted devices (boom v cave)
- data glove - finger flex angles and position of wrist
- 3d mouse - based on ultrasonic position reference array 
- space ball - 2 dof force sensitive device
### software
- system software - bridge btw hardware and applications
- application software - what end users interact with
- development software - used by programmers, designers, developers

# unit 2

## unity basics 
- project - shows all you assets
- scene - view and navigate the game environment
- hierarchy - list all objects int he current scene
- inspector - view and edit properties of the selected object
- adding gameobject - game object -> 3d object -> cube
- Q W E R T Y = Hand, Move, Rotate, Scale, Rect, Transform
- adding place  - gameobject -> 3d object -> plane
- 