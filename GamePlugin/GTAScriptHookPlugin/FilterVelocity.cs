using GTA.Math;
using GTAHook;
using System;

namespace GTAHook
{
    class FilterVelocity
	{
		private int samples;

		private Vector3 lastVelocity;
		
		private Vector3[] prev; 

		public FilterVelocity(int samples)
		{
			this.samples = samples;
			prev = new Vector3[samples];
		}

		public Vector3 Sample(Vector3 rawVelocity)
		{
            Vector3 sum = new Vector3();
            for (int i = 0; i <= samples - 2; i++)
            {
                prev[i] = prev[i + 1];
                sum += prev[i];
            }
            prev[samples - 1] = rawVelocity;
            sum += rawVelocity;

            // Obtain filtered velocity
            sum /= samples;
            return sum;
        }
	}
}
