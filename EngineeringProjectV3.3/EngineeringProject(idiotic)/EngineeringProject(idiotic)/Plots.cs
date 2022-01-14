using System;
using System.Collections.Generic;
using System.Text;

namespace EngineeringProject_idiotic_
{
    class Plots
    {
        public static void PlotOne(double Ya, double z1)
        { 
            // 0 <= z1 <= 0.25(0 - F1)
            Program.LateralForce = (-1) * Ya;
            Program.BendingMoment = (-1) * Ya * z1;
        }
        public static void PlotTwo(double Ya, double z2)
        {
            // 0 <= z2 <= 0.05(F1 - q)
            Program.LateralForce = Program.PowerOne - Ya;
            Program.BendingMoment = (Program.PowerOne * z2) - (Ya * (z2 + Program.ForceOneLength));
        }
        public static void PlotThree(double Ya, double z3)
        {
            // 0 <= z3 <= 0.3(q1 - q2)
            double z2 = Program.DistributedStartLength - Program.ForceOneLength;
            Program.LateralForce = ((-1) * Program.DistributedLoad * z3) + Program.PowerOne - Ya;
            Program.BendingMoment = ((-1) * (Program.DistributedLoad * Math.Pow(z3, 2) / 2)) + (Program.PowerOne * (z3 + z2)) - (Ya * (z3 + Program.DistributedStartLength));
        }
        public static void PlotFour(double Yb, double z4)
        {
            // 0 <= z4 <= 0.2(F2 - l)
            Program.LateralForce = Yb;
            Program.BendingMoment = (-1) * Yb * z4;
        }
        public static void PlotFive(double Yb, double z5)
        {
            // 0 <= z4 <= 0.2(q2 - F2)
            Program.LateralForce = ((-1) * Program.PowerTwo) + Yb;
            Program.BendingMoment = (Program.PowerTwo * z5) - (Yb * (z5 + (Program.ForceTwoLength - Program.DistributedEndLength)));
        }
    }
}
