# Flight Simulator Inspection

In this project we are about to collect data and fill up attributes fields such as: velocity, altitude, direction etc.
The whole data would be displayed through a suitable simulator, and will be examined by pilots or pilot investigators.
The data would be documented into a text file, which will be loaded into our application.
The application will play the simulator respectively to the entire text file, and supply visual demonstration of the latter,
including a feature which destined to present the anomalies that have been found.

![1](https://raw.githubusercontent.com/morya12/FlightSimulatorInspection/master/FGinspection.PNG)

# Controllers
The different app's controllers are detailed below:

## Player controller
Supports a controller to play the flight.
Except of the default player's options such as: play, pause and stop, 
It also contains two scrollbars, the first one enables the user to scroll through the timesteps list,
and the second one enables full control of the playback playing speed.

## Rudder controller
This controller is in charge of displaying the status of the airplane rudders.
Its representation is based on the data's textfile, and supports solely visual demonstration , neither controlling
the rudder nor alter its values, exactly as required following the instructions.

## Feature Selection Controller
Enables the user to have further details regarding different specific attributes: altitude, velocity, direction, pitch,
roll and yaw.

## Graph Controller
This controller splits into different classes, each one is in charge of different requirement.
**GraphA** - displays the user's choice as following: X represents the timeline and Y represents the data,
practically we read line-by-line from the text file respectively to the feature has been chosen by the user.
**GraphB** - calculates the most correlated feature to A and functions the same as GraphA.
**RegressionGraph** - Holds the X value of the chosen feature, and Y value of the correlated feature of the latter, and present their line regression.

**These different algorithms have been implemented separately as a plug-in, hence, DLL files that dynamically loaded from the application.**

**Furthermore, we support a special feature which enables the user to load the FlightGear from within the application.**


# Project's File Organization

![Files](https://user-images.githubusercontent.com/60241230/114762425-6d40f580-9d6a-11eb-8c7d-cf7aa0eddb34.JPG)

As required, we based our design on the MVVM architectural pattern ,that facilitates the separation of the development of the GUI (the view)
from the development of the business logic (the model) so that the view is not dependent on any specific model platform.
Hence, our folders ordered as follow:
**Models
ViewModels
Views**
Each conatains the matching code files of every controller.

# Requirements:
We used .NET Framework 4.7.2

## UML diagrams

![UML](https://user-images.githubusercontent.com/60241230/114762474-7a5de480-9d6a-11eb-9af7-15173cc516ef.jpg)

## Video 
[Part 1](https://www.loom.com/share/9b828d41e359491998d86ee29b7b90f0)
[Part 2](https://www.loom.com/share/61c891a6d00f4ff2a572d2eb68c15693)
