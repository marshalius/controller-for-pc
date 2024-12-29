# ControllerForPC

This project is a gamepad application built using .NET MAUI, which allows you to use your Android phone as a gamepad for your Windows PC. The application communicates with a server running on the PC side, transmitting input data from the mobile device to the PC and transforming it into gamepad inputs. Additionally, the application offers a visual interface on the PC to control elements like button size and positioning.

## Features
- Use your phone as a gamepad for PC gaming.
- PC server handles mobile input data and translates it into gamepad commands.
- Visual feedback on PC side for gamepad adjustments (e.g., button size, position).
- Works on Windows 10/11 only.

## Prerequisites
To run this application, you need:

- .NET MAUI SDK (for mobile development).
- Visual Studio with .NET MAUI support (for development and testing).
- A PC running Windows 10/11 for the server-side part of the application.
- An Android phone to use gamepad screen (client-side part of the application).

## Installation
1. Clone this repository to your local machine:
   ```
   git clone https://github.com/marshalius/ControllerForPC.git
   ```
3. Open the solution in Visual Studio.

4. For the mobile app:
   - Ensure you have .NET MAUI installed and configured for your platform.
   - Build and deploy the mobile app to your device or emulator.

5. For the PC server:
   - Navigate to the server-side project folder.
   - Build and run the server application on your Windows PC.

## Usage
1. PC Server: Start the server application and wait for the connection of the mobile application.

2. Mobile App: Open the mobile app and wait for it to scan the server automatically. Make sure the server application is active on the computer and connect when the server name appears on the mobile application screen.
3. Checking: When the connection is established, the gamepad screen will open in the mobile application and the control panel will open in the server application. To check if it works, interact with the gamepad screen in the mobile app and examine the visual feedback in the gamepad interface on the control panel.

4. *Adjustments (under development): You can adjust the gamepad button sizes and positions directly from the PC interface.*

## Contributing
Feel free to fork this project and submit pull requests. If you have any ideas for improvements or new features, don't hesitate to open an issue.

License
This project is licensed under the MIT License.
