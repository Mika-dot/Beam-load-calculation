using System;
using System.Collections.Generic;
using System.Text;

namespace EngineeringProject_idiotic_
{
    class Support
    {
        public static double OneStageFastening()
        {
            // Фактически расчет момента относительно ya (для нахождения yb).
            return ((Program.PowerOne * Program.ForceOneLength) + (Program.PowerTwo * Program.ForceTwoLength) - (Program.DistributedLoad * Program.DistributedStartLength * (Program.DistributedStartLength + ((Program.DistributedEndLength - Program.DistributedStartLength) / 2)))) / Program.Length;
        }
        public static double TwoDegreeFastening(double OneStageFastening)
        {
            // Фактически расчет силы при известном ya (для нахождения ya).
            return Program.PowerOne + Program.PowerTwo - (Program.DistributedLoad * (Program.DistributedEndLength - Program.DistributedStartLength)) - OneStageFastening;
        }
    }
}
