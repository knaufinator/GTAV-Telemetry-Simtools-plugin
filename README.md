# GTAV-Telemetry-Simtools-plugin
GTAV scripthookvdotnet script/telemetry server and Simtools plugin for motion simulator

# Plugin provides
pitch,roll,yaw,surge,sway,heave - rotationAccelerationX,rotationAccelerationY,rotationAccelerationZ

# Source code included
- GTAScriptHookPlugin - Scripthookvdotnet script, this acts as a server of the telemetry data, it is relayed over UDP... often...
- GrandTheftAutoV_GamePlugin - Simtools Plugin very basic all the actual logic/calculations are done in the scripthook script.

# Install instructions 

- 1) Install scripthookv
- 2) Install scripthookvdotnet 
- 3) Copy GTAScriptHookPlugin.dll into the "scripts" folder (create it..) within your GTAV directory
	"C:\Program Files (x86)\Steam\steamapps\common\Grand Theft Auto V\scripts"

- 4) Copy  GrandTheftAutoV_GamePlugin.dll into the simtools plugin directory... i have not bothered checking if it installs using normal methods...reopen simtools, and it should appear in the list. patch as normal...
C:\Users\userName\AppData\Local\SimTools\GamePlugins

- 5) Open gtav, telemetry is triggered when game protagonist enters vehicle... it should work with the jets as well... and .. boats .. and sub,.... but i have not tried yet. Scripthookdotnet has a log file if there are issue it may show up in it inside the GTAV directory.


note,... you will need to set up min max limits using the tuner, they will default to 0's 
