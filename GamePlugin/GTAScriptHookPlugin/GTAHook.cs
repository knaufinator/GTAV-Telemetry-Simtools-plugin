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
		LinearAcceleration linearAcceleration = new LinearAcceleration();
		LinearAcceleration linearRotationalAcceleration = new LinearAcceleration();

		public Main()
		{
			Tick += OnTick;
			Interval = 1;
			udpClient = new UdpClient();
			udpClient.Connect("127.0.0.1", 20777);
		}

		void OnTick(object sender, EventArgs e)
		{
			if (Game.Player.Character.IsSittingInVehicle())
			{
				//this algorithm, based on the players changing position in the world, apply those changing accellerations to the appropriate side of the player based on the way they are facing.
				Vector3 position = new Vector3(Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);

				Vector3 vector;
				linearAcceleration.LinearAccelerationSample(out vector, position, 25);//increase samples from 25 if too rough.... increase latency, if its to slow... reduce...
				
		
				double Surge_Output = (double)vector.X * (double)Game.Player.Character.ForwardVector.X + (double)vector.Y * (double)Game.Player.Character.ForwardVector.Y;
				double Sway_Output = (double)vector.X * (double)Game.Player.Character.RightVector.X + (double)vector.Y * (double)Game.Player.Character.RightVector.Y + (double)vector.Z * (double)Game.Player.Character.RightVector.Z;
				double Heave_Output = (double)vector.X * (double)Game.Player.Character.UpVector.X + (double)vector.Y * (double)Game.Player.Character.UpVector.Y + (double)vector.Z * (double)Game.Player.Character.UpVector.Z;

				Vector3 twistyVector;
				linearRotationalAcceleration.LinearAccelerationSample(out twistyVector, Game.Player.Character.Rotation, 25);

				string[] toSend = new string[10];
				toSend[0] = Surge_Output.ToString("n14");
				toSend[1] = Sway_Output.ToString("n14");
				toSend[2] = Heave_Output.ToString("n14");

				toSend[3] = Game.Player.Character.Rotation.X.ToString("n14");
				toSend[4] = Game.Player.Character.Rotation.Y.ToString("n14");
				toSend[5] = Game.Player.Character.Rotation.Z.ToString("n14");
				
				toSend[6] = twistyVector.X.ToString("n14");
				toSend[7] = twistyVector.Y.ToString("n14");
				toSend[8] = twistyVector.Z.ToString("n14");

				string toSendString = getSendString(toSend);

				byte[] data = Encoding.UTF8.GetBytes(toSendString);
				udpClient.Send(data, data.Length);
			}
			else
			{
				string[] toSend = new string[10];
				toSend[0] = "0";
				toSend[1] = "0";
				toSend[2] = "0";
				toSend[3] = "0";
				toSend[4] = "0";
				toSend[5] = "0";
				toSend[6] = "0";
				toSend[7] = "0";
				toSend[8] = "0";

				string toSendString = getSendString(toSend);

				byte[] data = Encoding.UTF8.GetBytes(toSendString);
				udpClient.Send(data, data.Length);
		
			}
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