# 360ImmersiveCycle

An Augmented Reality Android application designed to assess a user's ability to identify road hazards while biking. The application presents three types of tests, each using multiple 360-degree videos: Perception Test, Comprehension Test, and Projection Test.

**This application is a work in progress

### Software overview
* Device orientation is crucial for directing the user’s vision. Users should be given an onboarding period to familiarize themselves with observing the 360-degree video.
* Data is stored based on the test being taken by the participant.
* Projection Test: Users tap on objects they consider hazards. Correct answers trigger sound feedback.
* Perception Test: Users tap the stop button when they encounter a hazard that requires braking.
* Comprehension Test: At the end of the video, users answer questions about the hazards encountered and rate their confidence in their responses.
* Videos are loaded from internal files. To run the application with videos, please contact me to obtain them.

### Reformatting video with premiere pro
* Some Android devices can not run videos higher than 30FPS. Moreover, they are also unable to run 4K videos internally. Therefore reformatting is necessary for some videos. Below are the steps
* 1.	Make a new sequence with the dimension size 3840 x 1920 and frame rate <30FPS
* 2.	When prompted do not change the sequence to match the clip's settings
* 3.	For each clip in the new sequence set to frame size (option can be found on right click)
* 4.	Render each clip
     
### Dependencies

* Unity 2022.3.27f
* Android system with gyroscope ability


## Common issues
* Canvas scaling issues in different device

## Android file system common bug
* Android has random bug where they will not allow files to be copied (therefore always check if correct file is in your android system). Below is the current workaround I use
* -	If this occurs when replacing a file
* o	Delete old file
* o	Disconnect android
* o	Connect android
* o	Copy the new file
* -	If this occurs with new file
* o	Change the file name
* o	Disconnect android
* o	Connect android
* o	Change the file name again on the device folder


## Authors
* Farah Farizi - https://github.com/farahfarizi24 / farahdfarizi@gmail.com


