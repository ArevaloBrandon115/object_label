# object_label
### Description:
GameObject tool that shows more information about the parts that make that object.

# Table of Contents 

  - [Controls](#Controls)  
  - [Features](#Features)  
  - [Scripts used](#Scripts)  


<a name="Controls"/>

## Controls (note: make sure the mouse isn't on any UI element)
- Move: You can move around by clicking and holding the left mouse button 
- Zoom: You can zoom by scrolling your mouse wheel
- Rotate: You can rotate by holding down the left control key on your keyboard and scrolling with your mouse wheel

<a name="Features"/>

## Features
An object could be highlighted in two ways:
1. Hovering over the object itself.
2. Hovering over the button on the left hand side of the screen corresponding to that object.

<a name="Scripts"/>

## Main scripts (Assets/Scripts):

### Assets/Scripts/ObjectSelection:

This controls the Highlighing, the buttons, the arrow pointing to the object
  - HighlighingObject
  - ISelectionResponse
  - SelectionManager


### Assets/Scripts/CameraContoller:

This controls the zoom, panning, rotating of the camera
  - CameraController
  - Map
  - MouseInputUIBlocker
  
