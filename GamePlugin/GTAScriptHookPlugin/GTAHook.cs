using System;
using System.Net.Sockets;
using System.Text;
using GTA;
using GTA.Math;

namespace GTAHook
{

	public class Main : Script
	{
		UdpClient udpClient;

		LinearAccelerationFromVelocity linearAcceleration = new LinearAccelerationFromVelocity(25);
		LinearAccelerationFromVelocity linearRotationalAcceleration = new LinearAccelerationFromVelocity(25);

		public Main()
		{
			Tick += OnTick;
			Interval = 1;
			udpClient = new UdpClient();
			udpClient.Connect("127.0.0.1", 20777);
		}

		void OnTick(object sender, EventArgs e)
		{
			string[] toSend = new string[30];

			if (Game.Player.Character.IsSittingInVehicle())
			{			
				Vector3 vector = linearAcceleration.LinearAccelerationSample(Game.Player.LastVehicle.Velocity);

				double Surge_Output = (double)vector.X * (double)Game.Player.LastVehicle.ForwardVector.X + (double)vector.Y * (double)Game.Player.LastVehicle.ForwardVector.Y;
				double Sway_Output = (double)vector.X * (double)Game.Player.LastVehicle.RightVector.X + (double)vector.Y * (double)Game.Player.LastVehicle.RightVector.Y + (double)vector.Z * (double)Game.Player.LastVehicle.RightVector.Z;
				double Heave_Output = (double)vector.X * (double)Game.Player.LastVehicle.UpVector.X + (double)vector.Y * (double)Game.Player.LastVehicle.UpVector.Y + (double)vector.Z * (double)Game.Player.LastVehicle.UpVector.Z;

				toSend[0] = Surge_Output.ToString("n14");
				toSend[1] = Sway_Output.ToString("n14");
				toSend[2] = Heave_Output.ToString("n14");

				toSend[3] = Game.Player.LastVehicle.Rotation.X.ToString("n14");
				toSend[4] = Game.Player.LastVehicle.Rotation.Y.ToString("n14");
				toSend[5] = Game.Player.LastVehicle.Rotation.Z.ToString("n14");


				Vector3 twistyVector = linearRotationalAcceleration.LinearAccelerationSample(Game.Player.LastVehicle.RotationVelocity);

				toSend[6] = twistyVector.X.ToString("n14");
				toSend[7] = twistyVector.Y.ToString("n14");
				toSend[8] = twistyVector.Z.ToString("n14");
			}
			else
			{
				toSend[0] = "0";
				toSend[1] = "0";
				toSend[2] = "0";
				toSend[3] = "0";
				toSend[4] = "0";
				toSend[5] = "0";
				toSend[6] = "0";
				toSend[7] = "0";
				toSend[8] = "0";
			}

			string toSendString = getSendString(toSend);

			byte[] data = Encoding.UTF8.GetBytes(toSendString);
			udpClient.Send(data, data.Length);
		}

		private string getSendString(string[] toSend)
		{
			string toSendString = "S:";
			string toSendEndString = "E";

			foreach (var item in toSend)
			{
				toSendString += item + ":";
			}

			toSendString += toSendEndString;
			return toSendString;
		}
	}
}