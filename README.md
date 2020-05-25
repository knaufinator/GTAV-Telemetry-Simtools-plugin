# GTAV-Telemetry-Simtools-plugin
GTAV scripthookvdotnet script/telemetry server and Simtools plugin for motion simulator

# Plugin provides
pitch,roll,yaw,surge,sway,heave - rotationAccelerationX,rotationAccelerationY,tractionLoss (extra_1,extra_2,extra_3)

Basic dash and vibe support

# Source code included
- GTAScriptHookPlugin - Scripthookvdotnet script, this acts as a server of the telemetry data, it is relayed over UDP... often...
- GrandTheftAutoV_GamePlugin - Simtools Plugin very basic all the actual logic/calculations are done in the scripthook script.

# Install instructions 

- 1) Install scripthookv
- 2) Install scripthookvdotnet 
- 3) Copy GTAScriptHookPlugin.dll into the "scripts" folder (create it..) within your GTAV directory
	"C:\Program Files (x86)\Steam\steamapps\common\Grand Theft Auto V\scripts"

- 4) install  GrandTheftAutoV_GamePlugin.dll using the simtools installer
I have in the past just dropped it into the simtools plugin directory...and it work, but some users report otherwise...fyi the plugin directory is.. C:\Users\userName\AppData\Local\SimTools\GamePlugins if needed

- 5) Open gtav, telemetry is triggered when game protagonist enters vehicle... it should work with the jets as well... and .. boats .. and sub.


note,... you will need to set up min max limits using the tuner, they will default to 0's 
