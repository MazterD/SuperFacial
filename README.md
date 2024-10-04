# SuperFacial

This repository is being used to host the code and assets for the first experiment in the Master of Science thesis titled "SuperFacial: Enhancing Facial Expressions Using Avatar Distortions to Improve Collaboration in Virtual Environments", written by Afonso Dias and supervised by Prof. Joaquim Jorge, both affiliated with Instituto Superior Técnico. The objective of this program is to verify the impact of using distortions to exaggerate facial expressions in VR. We aim to study the effects of this technique in the facial expressions' recognition by our users.

Link to the GitHub repository hosting the first experiment's prototype: https://github.com/MazterD/SuecaMoji

### Introduction

This project consists of a simple recognition task where the user will face an avatar that will display a series of facial expressions. These facial expressions can either be more realistic or exaggerated versions of six emotions: happiness, sadness, anger, disgust, fear and surprise. Both our avatars will display each of these six facial expressions in their natural state, as well as in three distinct distortion levels.

### Requirements

Below we present the tools and software that we have used when developing this project. Please do keep in mind that if you use an alternative setup the project's integrity may become compromised, leading to abnormal behaviour.

- Meta Quest 2 HMD with two Quest 2 controllers
- Unity version 2022.3.14f1
- Unity modules:
    - Android Build Support
    - OpenJDK
    - Android SDK and NDK Tools
- Unity packages:
    - Meta XR All-in-One SDK
    - XR Plug-in Management
    - Oculus XR Plugin
    - TextMeshPro


### Setting up the Unity Packages

Meta XR All-in-One SDK is available in the Unity Asset Store (link: https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657). To install this package you must login into the store with your Unity user and add the package to your asset library. From there you can find the package in your Package Manager. After the installation, you can open the Project Setup Tool (in the Unity Editor: Meta > Tools > Project Setup Tool) and apply all recommended changes for PC and Android.

To install the Oculus XR Plugin follow the instructions on this link: https://developers.meta.com/horizon/documentation/unity/unity-project-setup#install-the-oculus-xr-plugin. This tutorial will also go over the installation of the XR Plug-in Management.
 

### How to run this project

1 - Download and unzip the GitHub project
2 - Open a new Unity project using the stock 3D template
3 - Install the necessary packages (mentioned in the previous section) using the Package Manager/Unity Asset Store
4 - Drag and drop the unzipped GitHub project files into the Unity project's "Assets" folder
5 - Build the project while selecting Android as a platform. You can either build directily into the HMD or build the .apk first and then import it into the headset
6 - Lauch the application on the HMD

### Controls

Right Controller Index Trigger - Select Button