using Game_PluginAPI;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace GrandTheftAutoV_GamePlugin
{
    public class GamePlugin : IPlugin_Game
    {
        private string Dash_1_Output = "";
        private string Dash_2_Output = "";
        private string Dash_3_Output = "";
        private string Dash_4_Output = "";
        private string Dash_5_Output = "";
        private string Dash_6_Output = "";
        private string Dash_7_Output = "";
        private string Dash_8_Output = "";
        private string Dash_9_Output = "";
        private string Dash_10_Output = "";
        private string Dash_11_Output = "";
        private string Dash_12_Output = "";
        private string Dash_13_Output = "";
        private string Dash_14_Output = "";
        private string Dash_15_Output = "";
        private string Dash_16_Output = "";
        private string Dash_17_Output = "";
        private string Dash_18_Output = "";
        private string Dash_19_Output = "";
        private string Dash_20_Output = "";
        private string Vibe_1_Output = "";
        private string Vibe_2_Output = "";
        private string Vibe_3_Output = "";
        private string Vibe_4_Output = "";
        private string Vibe_5_Output = "";
        private string Vibe_6_Output = "";
        private string Vibe_7_Output = "";
        private string Vibe_8_Output = "";
        private string Vibe_9_Output = "";
     
        private const string _gameName = "GrandTheftAutoV";
        private const string _processName = "GTA5";
        private const string _pluginAuthorsName = "christopher knauf";
        private const string _port = "20777";
        private const string _pluginOptions = "";
        private const bool _requiresPatchingPath = false;
        private const bool _requiresSecondCheck = false;
        private const bool _enable_DashBoard = true;
        private const bool _enable_GameVibe = true;
  
        private const string _DOF_Support_Extra1 = "RotxAccel";
        private const string _DOF_Support_Extra2 = "RotyAccel";
        private const string _DOF_Support_Extra3 = "Traction Loss";
     
        private const bool _enable_MemoryHook = false;
        private const bool _enable_MemoryMap = false;

  
        private double Roll_Output;
        private double Pitch_Output;
        private double Heave_Output;
        private double Yaw_Output;
        private double Sway_Output;
        private double Surge_Output;
        private double Extra1_Output;
        private double Extra2_Output;
        private double Extra3_Output;
        private double Roll_MemHook;
        private double Pitch_MemHook;
        private double Heave_MemHook;
        private double Yaw_MemHook;
        private double Sway_MemHook;
        private double Surge_MemHook;
        private double Extra1_MemHook;
        private double Extra2_MemHook;
        private double Extra3_MemHook;
        private double Roll_MemMap;
        private double Pitch_MemMap;
        private double Heave_MemMap;
        private double Yaw_MemMap;
        private double Sway_MemMap;
        private double Surge_MemMap;
        private double Extra1_MemMap;
        private double Extra2_MemMap;
        private double Extra3_MemMap;

        public void GameStart()
        {
        }

        public void GameEnd()
        {
        }

        public bool PatchGame(string myPath, string myIp)
        {
            try
            {
                int num = (int)MessageBox.Show("Game patched successfully!", "Patching info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return true;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("You must first install and run the game once prior to patching.\nGame Not Patched!", "Patch failed!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        public void PatchPathInfo()
        {
            int num1 = (int)MessageBox.Show("Before patching, please complete one race in the game.", "Patching info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            if (Environment.Is64BitOperatingSystem)
            {
                int num2 = (int)MessageBox.Show("Please select game save directory.", "Patching info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                int num3 = (int)MessageBox.Show("Please select installation directory.", "Patching info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        public void UnPatchGame(string myPath)
        {
            int num = (int)MessageBox.Show("Game patch removed!", "Patching info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public bool ValidatePatchPath(string myPath)
        {
            return true;
        }

        public void Process_PacketRecieved(string text)
        {
            try
            {
                if (text.StartsWith("S:") & text.EndsWith(":E"))
                {
             
                    string[] strlist = text.Split(':');

                    this.Surge_Output = Double.Parse(strlist[1]);
                    this.Sway_Output = Double.Parse(strlist[2]);
                    this.Heave_Output = Double.Parse(strlist[3]);

                    this.Pitch_Output = Double.Parse(strlist[4]);
                    this.Roll_Output = Double.Parse(strlist[5]);
                    this.Yaw_Output = Double.Parse(strlist[6]);

                    this.Extra1_Output = Double.Parse(strlist[7]);//rxX
                    this.Extra2_Output = Double.Parse(strlist[8]);//rxY
                    this.Extra3_Output = Double.Parse(strlist[9]);//trac

                    // Dash,vice stuff
                    int speed = (int)double.Parse(strlist[12]);
                    int gear = (int)double.Parse(strlist[11]);
                    int rpm = (int)double.Parse(strlist[10]);

                    Dash_1_Output = "Speed," + speed.ToString();
                    Dash_2_Output = "Gear," + gear.ToString();
                    Dash_3_Output = "RPM," + rpm.ToString();

                    Vibe_1_Output = "RPM," + rpm.ToString();
                    Vibe_2_Output = "Gear Shift," + gear.ToString();
                    Vibe_3_Output = "Collision L/R," + Sway_Output.ToString();
                    Vibe_4_Output = "Collision F/B," + Surge_Output.ToString();
                    Vibe_5_Output = "Road Detail," + Heave_Output.ToString();
                    Vibe_7_Output = "Turbulence," + speed.ToString();
                }
            }
            catch (Exception)
            {
                this.Surge_Output = 1;//flag to me that it be broke...
          
            }
        }

        public void Process_MemoryHook()
        {
        }

        public void Process_MemoryMap()
        {
        }

        public void ResetDashVars()
        {
            this.Dash_1_Output = "";
            this.Dash_2_Output = "";
            this.Dash_3_Output = "";
            this.Dash_4_Output = "";
            this.Dash_5_Output = "";
            this.Dash_6_Output = "";
            this.Dash_7_Output = "";
            this.Dash_8_Output = "";
            this.Dash_9_Output = "";
            this.Dash_10_Output = "";
            this.Dash_11_Output = "";
            this.Dash_12_Output = "";
            this.Dash_13_Output = "";
            this.Dash_14_Output = "";
            this.Dash_15_Output = "";
            this.Dash_16_Output = "";
            this.Dash_17_Output = "";
            this.Dash_18_Output = "";
            this.Dash_19_Output = "";
            this.Dash_20_Output = "";
        }

        public void ResetDOFVars()
        {
            this.Roll_Output = 0.0;
            this.Pitch_Output = 0.0;
            this.Heave_Output = 0.0;
            this.Yaw_Output = 0.0;
            this.Sway_Output = 0.0;
            this.Surge_Output = 0.0;
            this.Extra1_Output = 0.0;
            this.Extra2_Output = 0.0;
            this.Extra3_Output = 0.0;
        }

        public void ResetHookVars()
        {
            this.Roll_MemHook = 0.0;
            this.Pitch_MemHook = 0.0;
            this.Heave_MemHook = 0.0;
            this.Yaw_MemHook = 0.0;
            this.Sway_MemHook = 0.0;
            this.Surge_MemHook = 0.0;
            this.Extra1_MemHook = 0.0;
            this.Extra2_MemHook = 0.0;
            this.Extra3_MemHook = 0.0;
        }

        public void ResetMapVars()
        {
            this.Roll_MemMap = 0.0;
            this.Pitch_MemMap = 0.0;
            this.Heave_MemMap = 0.0;
            this.Yaw_MemMap = 0.0;
            this.Sway_MemMap = 0.0;
            this.Surge_MemMap = 0.0;
            this.Extra1_MemMap = 0.0;
            this.Extra2_MemMap = 0.0;
            this.Extra3_MemMap = 0.0;
        }

        public void ResetVibeVars()
        {
            this.Vibe_1_Output = "";
            this.Vibe_2_Output = "";
            this.Vibe_3_Output = "";
            this.Vibe_4_Output = "";
            this.Vibe_5_Output = "";
            this.Vibe_6_Output = "";
            this.Vibe_7_Output = "";
            this.Vibe_8_Output = "";
            this.Vibe_9_Output = "";
        }

        public string GameName
        {
            get
            {
                return _gameName;
            }
        }

        public string ProcessName
        {
            get
            {
                return _processName;
            }
        }

        public string PluginAuthorsName
        {
            get
            {
                return _pluginAuthorsName;
            }
        }

        public string Port
        {
            get
            {
                return _port;
            }
        }

        public string PluginOptions
        {
            get
            {
                return _pluginOptions;
            }
        }

        public bool Enable_MemoryMap
        {
            get
            {
                return _enable_MemoryMap;
            }
        }

        public bool Enable_DashBoard
        {
            get
            {
                return _enable_DashBoard;
            }
        }

        public bool Enable_GameVibe
        {
            get
            {
                return _enable_GameVibe;
            }
        }

        public bool Enable_MemoryHook
        {
            get
            {
                return _enable_MemoryHook;
            }
        }

        public bool RequiresPatchingPath
        {
            get
            {
                return _requiresPatchingPath;
            }
        }

        public bool RequiresSecondCheck
        {
            get
            {
                return _requiresSecondCheck;
            }
        }

        public string Get_PluginVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public string GetDOFsUsed()
        {
            return true.ToString() + "," + true.ToString() + "," + true.ToString() + "," + true.ToString() + "," + true.ToString() + "," + true.ToString() + "," + _DOF_Support_Extra1 + "," + _DOF_Support_Extra2 + "," + _DOF_Support_Extra3;
        }


        public string Get_Dash1_Output()
        {
            return this.Dash_1_Output;
        }

        public string Get_Dash2_Output()
        {
            return this.Dash_2_Output;
        }

        public string Get_Dash3_Output()
        {
            return this.Dash_3_Output;
        }

        public string Get_Dash4_Output()
        {
            return this.Dash_4_Output;
        }

        public string Get_Dash5_Output()
        {
            return this.Dash_5_Output;
        }

        public string Get_Dash6_Output()
        {
            return this.Dash_6_Output;
        }

        public string Get_Dash7_Output()
        {
            return this.Dash_7_Output;
        }

        public string Get_Dash8_Output()
        {
            return this.Dash_8_Output;
        }

        public string Get_Dash9_Output()
        {
            return this.Dash_9_Output;
        }

        public string Get_Dash10_Output()
        {
            return this.Dash_10_Output;
        }

        public string Get_Dash11_Output()
        {
            return this.Dash_11_Output;
        }

        public string Get_Dash12_Output()
        {
            return this.Dash_12_Output;
        }

        public string Get_Dash13_Output()
        {
            return this.Dash_13_Output;
        }

        public string Get_Dash14_Output()
        {
            return this.Dash_14_Output;
        }

        public string Get_Dash15_Output()
        {
            return this.Dash_15_Output;
        }

        public string Get_Dash16_Output()
        {
            return this.Dash_16_Output;
        }

        public string Get_Dash17_Output()
        {
            return this.Dash_17_Output;
        }

        public string Get_Dash18_Output()
        {
            return this.Dash_18_Output;
        }

        public string Get_Dash19_Output()
        {
            return this.Dash_19_Output;
        }

        public string Get_Dash20_Output()
        {
            return this.Dash_20_Output;
        }

        public double Get_Extra1MemHook()
        {
            return this.Extra1_MemHook;
        }

        public double Get_Extra1MemMap()
        {
            return this.Extra1_MemMap;
        }

        public double Get_Extra1Output()
        {
            return this.Extra1_Output;
        }

        public double Get_Extra2MemHook()
        {
            return this.Extra2_MemHook;
        }

        public double Get_Extra2MemMap()
        {
            return this.Extra2_MemMap;
        }

        public double Get_Extra2Output()
        {
            return this.Extra2_Output;
        }

        public double Get_Extra3MemHook()
        {
            return this.Extra3_MemHook;
        }

        public double Get_Extra3MemMap()
        {
            return this.Extra3_MemMap;
        }

        public double Get_Extra3Output()
        {
            return this.Extra3_Output;
        }

        public double Get_HeaveMemHook()
        {
            return this.Heave_MemHook;
        }

        public double Get_HeaveMemMap()
        {
            return this.Heave_MemMap;
        }

        public double Get_HeaveOutput()
        {
            return this.Heave_Output;
        }

        public double Get_PitchMemHook()
        {
            return this.Pitch_MemHook;
        }

        public double Get_PitchMemMap()
        {
            return this.Pitch_MemMap;
        }

        public double Get_PitchOutput()
        {
            return this.Pitch_Output;
        }

        public double Get_RollMemHook()
        {
            return this.Roll_MemHook;
        }

        public double Get_RollMemMap()
        {
            return this.Roll_MemMap;
        }

        public double Get_RollOutput()
        {
            return this.Roll_Output;
        }

        public double Get_SurgeMemHook()
        {
            return this.Surge_MemHook;
        }

        public double Get_SurgeMemMap()
        {
            return this.Surge_MemMap;
        }

        public double Get_SurgeOutput()
        {
            return this.Surge_Output;
        }

        public double Get_SwayMemHook()
        {
            return this.Sway_MemHook;
        }

        public double Get_SwayMemMap()
        {
            return this.Sway_MemMap;
        }

        public double Get_SwayOutput()
        {
            return this.Sway_Output;
        }

        public string Get_Vibe1_Output()
        {
            return this.Vibe_1_Output;
        }

        public string Get_Vibe2_Output()
        {
            return this.Vibe_2_Output;
        }

        public string Get_Vibe3_Output()
        {
            return this.Vibe_3_Output;
        }

        public string Get_Vibe4_Output()
        {
            return this.Vibe_4_Output;
        }

        public string Get_Vibe5_Output()
        {
            return this.Vibe_5_Output;
        }

        public string Get_Vibe6_Output()
        {
            return this.Vibe_6_Output;
        }

        public string Get_Vibe7_Output()
        {
            return this.Vibe_7_Output;
        }

        public string Get_Vibe8_Output()
        {
            return this.Vibe_8_Output;
        }

        public string Get_Vibe9_Output()
        {
            return this.Vibe_9_Output;
        }

        public double Get_YawMemHook()
        {
            return this.Yaw_MemHook;
        }

        public double Get_YawMemMap()
        {
            return this.Yaw_MemMap;
        }

        public double Get_YawOutput()
        {
            return this.Yaw_Output;
        }
    }
}
