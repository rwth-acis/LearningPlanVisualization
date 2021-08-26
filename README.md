# Learning Plan Visualizations

This Unity mixed reality application allows students to create layered learning plans which are visualized in 3D.
You can use the 3D layout of the learning plan to track long-term goals, as well as short-term goals to reach each step of your master plan.
In a calendar view, you can schedule your steps and keep track of your actions.

## Getting Started

### Prerequisites

- Recommended [Unity version](https://unity3d.com/de/get-unity/download/archive): 2020.3.13f1
- Required Unity module: Universal Windows Platform support
- [Microsoft Mixed Reality Toolkit v2.7.2](https://github.com/microsoft/MixedRealityToolkit-Unity/releases/tag/v2.7.2)
- [i5 Toolkit for Unity 1.6.3](https://github.com/rwth-acis/i5-Toolkit-for-Unity/releases/tag/v1.6.3)
- Visual Studio (tested with VS 2019)
- For HoloLens Development:
  - Windows 10 development machine
  - Windows 10 SDK ([10.0.18362.0](https://developer.microsoft.com/de-de/windows/downloads/windows-10-sdk))

- Supports both HoloLens 1 and HoloLens 2

### Project Setup

1. Install Unity and Visual Studio
2. Clone the project
3. Start Unity, select open and choose the folder Frontend/VIAProMa in the project files
4. Open the scene "Main Scene".
A prompt will appear which will ask to import TMP Essentials. Click on the upper button.
5. (Optional) It makes sense to scale the editor icons down.
This can be done under "Gizmos" in the top right of the 3D view.
Pull the top most slider next to "3D Icons" down until the icons in the scene have the right size

### Compiling the Application

#### Step 1: Unity Build

1. In Unity, go to "File > Build Settings" in the top menu.
2. Switch to the Universal Window Platform.
3. Make sure that the "Main Scene" is the only added scene in the list "Scenes in Build".
   If this is not the case and the main scene is currently open, you can click the "Add Open Scenes" button.
   Otherwise, you can add it by dragging and dropping the "MainScene" scene file into the list.
4. Click the "Build" button.
5. Choose an empty folder where you want to generate the Visual Studio solution for the application.
   Important: We do not recommend overwriting previous builds as Unity does not update all settings and you might end up with a mixture of the previous build and the current one.

#### Step 2: Visual Studio Build
1. Once Unity has finished the build, open the generated Visual Studio solution.
2. Open the Solution Explorer in Visual Studio and right-click on the Learning Plan Visualization project.
   Select "Publish > Create App Packages...".
3. Select the Sideloading option.
4. Use the current certificate.
5. For HoloLens 1 compilation, only select x86 as an architecture and set its solution configuration to "Release (x86)".
   If you want to deploy to the HoloLens 2, choose either ARM or ARM64.
   Again, also set the solution configuratin to "Release (ARM)" or "RElease (ARM64)".
6. Click the "Create" button to create the packages.

#### Step 3: Deployment

1. The resulting packages can be found in the folder "AppPackages" of the Unity's build target folder.
2. Start the HoloLens and go into its settings app.
   Go to the network settings.
   In the entry of the connected WiFi network, click the blue "Advanced Settings" link.
   Here, you can see the IP address of the device.
3. Enter the IP address in your development machine's browser.
4. Go to the apps section and install the package.
   To do this, navigate to the .appx package.
   Moreover, check "Allow me to specify additional dependencies".
   Navigate to the "Dependencies" tab, select the architecture for which you compiled the app, i.e. x86 for HoloLens 1 or ARM or ARM64 for HoloLens 2, and add the .appx file dependency.
5. Click install.
