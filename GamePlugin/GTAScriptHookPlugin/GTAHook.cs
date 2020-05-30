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
		FilterVelocity filterVelocity = new FilterVelocity(25);
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

				Vector3 velocityVector = Game.Player.LastVehicle.Velocity;

				Vector3 filteredVelocityVector = filterVelocity.Sample(Game.Player.LastVehicle.Velocity);
				Vector3 accelVector = linearAcceleration.LinearAccelerationSample(velocityVector);

				double Surge_Output = (double)accelVector.X * (double)Game.Player.LastVehicle.ForwardVector.X + (double)accelVector.Y * (double)Game.Player.LastVehicle.ForwardVector.Y;
				double Sway_Output = (double)accelVector.X * (double)Game.Player.LastVehicle.RightVector.X + (double)accelVector.Y * (double)Game.Player.LastVehicle.RightVector.Y + (double)accelVector.Z * (double)Game.Player.LastVehicle.RightVector.Z;
				double Heave_Output = (double)accelVector.X * (double)Game.Player.LastVehicle.UpVector.X + (double)accelVector.Y * (double)Game.Player.LastVehicle.UpVector.Y + (double)accelVector.Z * (double)Game.Player.LastVehicle.UpVector.Z;

				toSend[0] = Surge_Output.ToString("n14");
				toSend[1] = Sway_Output.ToString("n14");
				toSend[2] = Heave_Output.ToString("n14");

				toSend[3] = Game.Player.LastVehicle.Rotation.X.ToString("n14");
				toSend[4] = Game.Player.LastVehicle.Rotation.Y.ToString("n14");
				toSend[5] = Game.Player.LastVehicle.Rotation.Z.ToString("n14");

				Vector3 twistyVector = linearRotationalAcceleration.LinearAccelerationSample(Game.Player.LastVehicle.RotationVelocity);

				double tracLoss = -((double)filteredVelocityVector.X * (double)Game.Player.LastVehicle.RightVector.X + (double)filteredVelocityVector.Y * (double)Game.Player.LastVehicle.RightVector.Y + (double)filteredVelocityVector.Z * (double)Game.Player.LastVehicle.RightVector.Z);

				toSend[6] = twistyVector.X.ToString("n14");
				toSend[7] = twistyVector.Y.ToString("n14");
				toSend[8] = tracLoss.ToString("n14");

				//dash vibe stuff
				toSend[9] = Game.Player.LastVehicle.CurrentRPM.ToString("n14");
				toSend[10] = Game.Player.LastVehicle.CurrentGear.ToString("n14");
				toSend[11] = Game.Player.LastVehicle.Speed.ToString("n14");

				toSend[14] = Game.Player.LastVehicle.SteeringAngle.ToString("n14");
				toSend[15] = Game.Player.LastVehicle.WheelSpeed.ToString("n14");
				toSend[16] = Game.Player.LastVehicle.MaxTraction.ToString("n14");
				toSend[17] = Game.Player.LastVehicle.ThrottlePower.ToString("n14");
				toSend[18] = Game.Player.LastVehicle.Throttle.ToString("n14");

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
				toSend[9] = "0";
				toSend[10] = "0";
				toSend[11] = "0";
				toSend[12] = "0";
				toSend[13] = "0";

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
