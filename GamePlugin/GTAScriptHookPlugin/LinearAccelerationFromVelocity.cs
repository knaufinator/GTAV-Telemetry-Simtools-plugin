using GTA.Math;
using GTAHook;
using System;

namespace GTAHook
{
    class LinearAccelerationFromVelocity
	{
		private long time;
		public float TimeOld;
		public float DeltaTime;
		private AccurateTimer mTimer1;
		private int samples;

		private Vector3 lastVelocity;
		private Vector3 rawAcceleration;
		private Vector3 filteredAcceleration;
		private Vector3[] prev; 

		public LinearAccelerationFromVelocity(int samples)
		{
			this.mTimer1 = new AccurateTimer(new Action(this.TimerTick1), 1);
			this.samples = samples;
			prev = new Vector3[samples];
		}

		private void TimerTick1()
		{
			++this.time;
		}



		public Vector3 LinearAccelerationSample(Vector3 velocity)
		{

			//a = v − v0 / t
			this.DeltaTime = (float)this.time - this.TimeOld;
			this.TimeOld = (float)this.time;

			// Get instantaneous acceleration (unfiltered, noisy)
			rawAcceleration = 1.0f * (velocity - lastVelocity) / DeltaTime;
			lastVelocity = velocity;

			Vector3 sum = new Vector3();
			for (int i = 0; i <= samples - 2; i++)
			{
				prev[i] = prev[i + 1];
				sum += prev[i];
			}
			prev[samples - 1] = rawAcceleration;
			sum += rawAcceleration;

			// Obtain filtered acceleration
			sum /= samples;
			filteredAcceleration = sum;

			return filteredAcceleration;
		}


	}
}
