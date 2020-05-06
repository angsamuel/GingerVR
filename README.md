# Cybersickness Reduction Techniques
 This is a collection of programatic techniques to help reduce cybersickness when using head mounted displays.

## Techniques
The following techniques can be found in the Prefabs and Scripts folders. Instructions on how to set them up are given below.

### SingleNose
The SingleNose prefab creates a rest-frame for the user. 

To use SingleNose, drag the prefab into the scene. SingleNose will detect the main camera in the scene, and attach a virtual nose. Then configure the noses in the Unity Editor with the following options:
* Y Position (slider) - configures the y-axis position of the nose
* Z Position (slider) - configures the z-axis position of the nose
* Nose Width(slider) - configures how wide the nose model is.
* Flatness (slider) - configures how flat the nose models is.
* Nose Color (color) - sets the color of the nose model.

The nose model was taken from Poly by Google and can be found here: https://poly.google.com/view/9AUcVMxZLCp

Our implementation was inspired by the following:

Wienrich, Carolin, et al. "A Virtual Nose as a Rest-Frame-The Impact on Simulator Sickness and Game Experience." 2018 10th International Conference on Virtual Worlds and Games for Serious Applications (VS-Games). IEEE, 2018.


### AuthenticNose
The AuthenticNose prefab acts as a rest-frame for the user.

To use AuthenticNose prefab must be used in tandem with the OVRCameraRig asset from the Unity Standard Assets package. Click the “Use Per Eye Camera” box on the OVRCameraRig. Drag the DoubleNose  prefab into the scene, and AuthenticNose will automatically attach two nose models, one to each per eye camera. Each nose model is only visible through the corresponding camera, providing a more realistic representation of a human nose. Configure the noses in the Unity Editor with the following options:
* Spacing(slider) - configures how far apart the two nose models are.
* Y Position(slider) -configures the y-axis position of the nose
* Z Position (slider) - configures the z-axis position of the nose
* Nose Width(slider) - configures how wide the nose model is.
* Nose Flatness (slider) - configures how flat the two nose models are.
* Nose Color (color) - sets the color of the two nose models.

The nose model was taken from Poly by Google and can be found here: https://poly.google.com/view/9AUcVMxZLCp

Our implementation was inspired by the following:

Wienrich, Carolin, et al. "A Virtual Nose as a Rest-Frame-The Impact on Simulator Sickness and Game Experience." 2018 10th International Conference on Virtual Worlds and Games for Serious Applications (VS-Games). IEEE, 2018.


### DynamicGaussianBlur 
The DynamicGaussianBlur script dynamically blurs the user’s vision based on their translational speed. The rotational speed of the camera determines the sigma value used in the gaussian function.

To use DynamicGaussianBlur, attach the script to the main camera, and configure the effect with the following options:
* Sigma Maximum (float) - the maximum sigma value that can be used in the gaussian function (larger sigma value = more blurring).
* Angular Speed Threshold (float) - the speed (degrees per second) at which the user’s vision begins blurring.
* Angular Speed Maximum (float) - the speed (degrees per second) at which the image is blurred as much as Sigma Maximum allows.
* Smoothness (slider) - the number of times the effect is applied. A higher value will result in smoother blurring.
* Adjust Based on Rotation (bool) - controls whether the blur is impacted by rotation, or set to the constant values set in the editor.

Our implementation was inspired by the following:

Budhiraja, Pulkit, et al. "Rotation blurring: use of artificial blurring to reduce cybersickness in virtual reality first person shooters." arXiv preprint arXiv:1710.02599 (2017).

### ColorBlur 
The ColorBlur script dynamically blurs the user’s vision based on their translational speed. Portions of the image surpassing the specified color thresholds (i.e. brightness) are not dynamically blurred. 

To use ColorBlur, attach the script to the main camera, and configure with the following options:
* Sigma Maximum (float) - the maximum sigma value that can be used in the gaussian function (larger sigma value = more blurring).
* Angular Speed Threshold (float) - the speed (degrees per second) at which the user’s vision begins blurring.
* Angular Speed Maximum (float) - the speed (degrees per second) at which the image is blurred as much as Sigma Maximum allows.
* Smoothness (slider) - the number of times the effect is applied. A higher value will result in smoother blurring.
* Brightness Threshold (slider) - portions of the image with combined RGB valuesa combined RGB values above this will not be blurred. 
* Red Threshold (slider) - portions of the image with red values above this will not be blurred. 
* Green Threshold (slider) - portions of the image with green values above this will not be blurred. 
* Blue Threshold (slider) - portions of the image with blue values above this will not be blurred. 
* Dark Saliency (bool) - check this box to use the threshold sliders as upper bounds, so that the darkest. 
* Adjust Based on Rotation (bool) - controls whether the blur is impacted by rotation, or set to the constant values set in the editor.

Our implementation was inspired by the following:

Nie, Guang-Yu, et al. "Analysis on Mitigation of Visually Induced Motion Sickness by Applying Dynamical Blurring on a User's Retina." IEEE transactions on visualization and computer graphics (2019).

### DynamicFOV
The DynamicFOV script dynamically reduces the user’s field of view (FOV) based on their translational and angular speed. The rate of change in FOV is calculated with the following formula.

 CRate = Abs(angularSpeed * angularSpeedModifier) + (translationalSpeed ∗ translationalSpeedModifier)

To use DynamicFOV, attach the script to main camera in the scene. Then configure the effect in the Unity editor with the options described below.
* Angular Speed Modifier (float) - used in the rate of change formula detailed above (default value 20). 
* Translational Speed Modifier - used in the rate of change formula detailed above (default value 4). 
* Translational Speed Threshold (float) - the speed at which players must be moving for the FOV to begin decreasing.
* Minimum FOV (slider) - determines the minimum diagonal field of view when the user has been moving fast enough for a long enough period of time.
* Decay Rate (float) - determines the rate at which FOV returns to normal when the user is stationary, or moving at a speed below the Translational Speed Threshold 

Our implementation was inspired by the following:

Fernandes, Ajoy S., and Steven K. Feiner. "Combating VR sickness through subtle dynamic field-of-view modification." 2016 IEEE Symposium on 3D User Interfaces (3DUI). IEEE, 2016.

### DotEffect
The DotEffect prefab suspends virtual orbs around the user which move at twice the user’s velocity. 

To use DotEffect, drag the prefab into the scene, and drag the player Rigidbody into the “player” reference field. The orbs should then appear in the game view window. Then configure the nose in the Unity editor with the following options:
* Dot Size (slider) - configures the size of the dots
* Dot Spacing (slider) - configures the size of the spaces between dots
* Matrix Size (float) - configures dimensions of the matrix. For example value of 12 would spawn a 12 x 12 x 12 cube of dots around the player.

Our implementation was inspired by the following:

Buhler, Helmut, Sebastian Misztal, and Jonas Schild. "Reducing VR sickness through peripheral visual effects." 2018 IEEE Conference on Virtual Reality and 3D User Interfaces (VR). IEEE, 2018.

### HeadSnapper
The HeadSnapper prefab detects when the user’s head rotation speed passes a certain threshold, at which point their perspective is “snapped” in the direction they were turning. After a brief fade to black transition, the camera’s orientation is locked, and then rotated by a specified angle along the y axis. The user’s vision then fades from black so that they can see again.

To use HeadSnapper, drag the prefab into the scene. Then configure the effect with the following options:
* Transition Time (float) - total time from start of fade to black, to end of fade from black.
* Fade Time (float) - the time it takes to fade to black, and fade from black, not including the time the user’s vision is completely blocked. 
* Snapping Angle (float) - the magnitude of the angle at which the camera snaps
* Speed Threshold (float) - the speed (degrees per second) at which the camera needs to be rotating around the y axis for the effect to begin. 

Our implementation was inspired by the following:

Farmani, Yasin, and Robert John Teather. "Viewpoint snapping to reduce cybersickness in virtual reality." (2018).

### VisionLock
The VisionLock prefab allows users to lock what they are seeing on screen with a button press. While the effect is active, the image users see will not change with head movement. This effect is currently limited in that all objects in the scene which you wish to rotate with the user’s camera must be children of a single parent object, and because physics interactions in the scene are not rotated while VisionLock is active. 

To use VisionLock, drag the VisionLock prefab into the scene. Then drag the parent object containing all objects which you would like to rotate with the user’s vision to the Main Parent field in the VisionLock object. 

You may then configure the effect with the following options:
* Axis To Activate (string) - the axis which triggers the effect (i.e. Fire1).

Our implementation was inspired by the following:

Kemeny, Andras, et al. "New vr navigation techniques to reduce cybersickness." Electronic Imaging 2017.3 (2017): 48-53.


### VirtualCAVE 

The VirtualCAVE asset spawns a wireframe cube around the user. This cube follows, and rotates along with the user to simulate a cave automatic virtual environment. To use VirtualCAVE, simply drag the asset onto the user’s virtual camera in the scene. The asset will spawn the wireframe cube at runtime. The following settings can then be adjusted through the editor.

* Width (slider) - determines the width of the box.
* Height (slider) - determines the height of the box.
* Depth (slider) - determines the depth of the box.
* Line Thickness (slider) - determines the thickness of the lines used to generate the box.

This approach was inspired by the following paper: 
Nguyen-Vo, Thinh, Bernhard E. Riecke, and Wolfgang Stuerzlinger. "Simulated reference frame: A cost-effective solution to improve spatial orientation in vr." 2018 IEEE Conference on Virtual Reality and 3D User Interfaces (VR). IEEE, 2018.



## Utilities
These can be found in the Utilities folder. These are tools to help set up the techniques.

### FOVUtility
The FOVUtility allows you to measure the user’s field of view (FOV), as Unity’s Camera.fieldOfView does not currently support head mounted displays. 

To use the FOVUtility, first drag the prefab into the scene. A bar will appear in front of the user’s vision. Select diagonal, horizontal, or vertical from the drop down menu to select the type of FOV you would like to measure. Then drag the Stick Horizontal Scale and Stick Vertical Scale slider until the bar reaches the edges of the user’s vision. The Horizontal FOV, Vertical FOV, and Diagonal FOV fields on the game object will update in real time to show you the user’s FOV. 

