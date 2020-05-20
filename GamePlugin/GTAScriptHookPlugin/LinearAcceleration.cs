using GTA.Math;
using GTAHook;
using System;

namespace GTAHook
{
    class LinearAcceleration
    {

		private Vector3[] positionRegister;
		private float[] posTimeRegister;
		private long positionSamplesTaken;
		private long time;
		public float TimeOld;
		public float DeltaTime;
		private AccurateTimer mTimer1;
		private int samples;

		public LinearAcceleration(int samples)
		{
			this.mTimer1 = new AccurateTimer(new Action(this.TimerTick1), 1);
			this.samples = samples;
		}

		private void TimerTick1()
		{
			++this.time;
		}

		public bool LinearAccelerationSample(out Vector3 vector, Vector3 position)
		{
			this.DeltaTime = (float)this.time - this.TimeOld;
			this.TimeOld = (float)this.time;

			Vector3 zero = Vector3.Zero;
			vector = Vector3.Zero;

			//Vector3
			if (samples < 3)
				samples = 3;
			if (this.positionRegister == null)
			{
				this.positionRegister = new Vector3[samples];
				this.posTimeRegister = new float[samples];
			}
			for (int index = 0; index < this.positionRegister.Length - 1; ++index)
			{
				this.positionRegister[index] = this.positionRegister[index + 1];
				this.posTimeRegister[index] = this.posTimeRegister[index + 1];
			}
			this.positionRegister[this.positionRegister.Length - 1] = position;
			this.posTimeRegister[this.posTimeRegister.Length - 1] = (float)this.time;
			++this.positionSamplesTaken;
			if (this.positionSamplesTaken < (long)samples)
				return false;
			for (int index = 0; index < this.positionRegister.Length - 2; ++index)
			{
				Vector3 vector3_1 = this.positionRegister[index + 1] - this.positionRegister[index];
				float num1 = this.posTimeRegister[index + 1] - this.posTimeRegister[index];
				if ((double)num1 == 0.0)
					return false;
				Vector3 vector3_2 = vector3_1 / num1;
				Vector3 vector3_3 = this.positionRegister[index + 2] - this.positionRegister[index + 1];
				float num2 = this.posTimeRegister[index + 2] - this.posTimeRegister[index + 1];
				if ((double)num2 == 0.0)
					return false;
				Vector3 vector3_4 = vector3_3 / num2;
				zero += vector3_4 - vector3_2;
			}
			Vector3 vector3 = zero / (float)(this.positionRegister.Length - 2);
			float num = this.posTimeRegister[this.posTimeRegister.Length - 1] - this.posTimeRegister[0];
			vector = vector3 / num;
			return true;
		}


	}
}
