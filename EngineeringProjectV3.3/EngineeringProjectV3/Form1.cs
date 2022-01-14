using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using System.Runtime.InteropServices;



namespace EngineeringProjectV3
{

    public partial class BeamCalculation : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();

        // Значения конструкции (основные).
        public static double VDistributedLoad = 5000;
        public static double VPowerOne = 2000;
        public static double VPowerTwo = 3000;
        public static double VLength = 1;

        // Размеры (которых нету на чертиже) отмеряющие растояние до точек приложения нагрузок.
        public static double VForceOneLength = 0.25;
        public static double VForceTwoLength = 0.8;
        public static double VDistributedStartLength = 0.3;
        public static double VDistributedEndLength = 0.6;

        // Переменные (временные для расчетов) момент и сила (Q и M). // Это могут быть не глобальные так как все расчеты видутся заного!!!!!!!
        public static double VLateralForce = 0;
        public static double VBendingMoment = 0;
        public BeamCalculation()
        {
            InitializeComponent();
        }

        private void LateraForce_Click(object sender, EventArgs e)
        {
            
            if (Examination())
            {
                return;
            }

            double Yb = Support.OneStageFastening(); // 2225
            double Ya = Support.TwoDegreeFastening(Yb); // 1275

            YA.Text = Convert.ToString(Ya);
            YB.Text = Convert.ToString(Yb);

            ShearForcePlot.GraphPane.CurveList.Clear();
            PointPairList list1 = new PointPairList();

            double x = 0;
            double Step = 0.01;
            // 0 <= z1 <= 0.25(0 - F1)
            for (double step = 0; step < VForceOneLength; step += Step)
            {
                x += Step;
                Plots.PlotOne(Ya, step);
                list1.Add(x, VLateralForce);
                //Console.ForegroundColor = ConsoleColor.Blue;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 30 + Convert.ToInt32(LateralForce / KHeight));
                //Console.ForegroundColor = ConsoleColor.Green;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 40 + Convert.ToInt32(BendingMoment / KHeight));
            }
            // 0 <= z2 <= 0.05(F1 - q)
            for (double step = 0; step < (VDistributedStartLength - VForceOneLength); step += Step)
            {
                x += Step;
                Plots.PlotTwo(Ya, step);
                list1.Add(x, VLateralForce);
                //Console.ForegroundColor = ConsoleColor.Blue;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 30 + Convert.ToInt32(LateralForce / KHeight));
                //Console.ForegroundColor = ConsoleColor.Green;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 40 + Convert.ToInt32(BendingMoment / KHeight));
            }
            // 0 <= z3 <= 0.3(q1 - q2)// парабола
            for (double step = 0; step < (VDistributedEndLength - VDistributedStartLength); step += Step)
            {
                x += Step;
                Plots.PlotThree(Ya, step);
                list1.Add(x, VLateralForce);
                //Console.ForegroundColor = ConsoleColor.Blue;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 30 + Convert.ToInt32(LateralForce / KHeight));
                //Console.ForegroundColor = ConsoleColor.Green;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 40 + Convert.ToInt32(BendingMoment / KHeight));
            }
            // 0 <= z4 <= 0.2(q2 - F2)
            for (double step = (VForceTwoLength - VDistributedEndLength); step > 0; step -= Step)
            {
                x += Step;
                Plots.PlotFive(Yb, step);
                list1.Add(x, VLateralForce);
                //Console.ForegroundColor = ConsoleColor.Blue;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 30 + Convert.ToInt32(LateralForce / KHeight));
                //Console.ForegroundColor = ConsoleColor.Green;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 40 + Convert.ToInt32(BendingMoment / KHeight));
            }
            //0 <= z4 <= 0.2(F2 - l)
            for (double step = (VLength - VForceTwoLength); step > 0; step -= Step)
            {
                x += Step;
                Plots.PlotFour(Yb, step);
                list1.Add(x, VLateralForce);
                //Console.ForegroundColor = ConsoleColor.Blue;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 30 + Convert.ToInt32(LateralForce / KHeight));
                //Console.ForegroundColor = ConsoleColor.Green;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 40 + Convert.ToInt32(BendingMoment / KHeight));
            }

            LineItem MyLine1 = ShearForcePlot.GraphPane.AddCurve("Эпюра поперечной  силы - Q", list1, Color.Black, SymbolType.None);
            MyLine1.Line.Width = 5;
            MyLine1.Line.Fill = new Fill(Color.Green);
            MyLine1.Symbol.IsVisible = false;

            ShearForcePlot.RestoreScale(ShearForcePlot.GraphPane);
        }

        private void BendingMoment_Click(object sender, EventArgs e)
        {
            if (Examination())
            {
                return;
            }

            double Yb = Support.OneStageFastening(); // 2225
            double Ya = Support.TwoDegreeFastening(Yb); // 1275

            YA.Text = Convert.ToString(Ya);
            YB.Text = Convert.ToString(Yb);

            BendingMomentPlot.GraphPane.CurveList.Clear();
            PointPairList list1 = new PointPairList();

            double x = 0;
            double Step = 0.01;
            // 0 <= z1 <= 0.25(0 - F1)
            for (double step = 0; step < VForceOneLength; step += Step)
            {
                x += Step;
                Plots.PlotOne(Ya, step);
                list1.Add(x, VBendingMoment);
                //Console.ForegroundColor = ConsoleColor.Blue;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 30 + Convert.ToInt32(LateralForce / KHeight));
                //Console.ForegroundColor = ConsoleColor.Green;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 40 + Convert.ToInt32(BendingMoment / KHeight));
            }
            // 0 <= z2 <= 0.05(F1 - q)
            for (double step = 0; step < (VDistributedStartLength - VForceOneLength); step += Step)
            {
                x += Step;
                Plots.PlotTwo(Ya, step);
                list1.Add(x, VBendingMoment);
                //Console.ForegroundColor = ConsoleColor.Blue;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 30 + Convert.ToInt32(LateralForce / KHeight));
                //Console.ForegroundColor = ConsoleColor.Green;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 40 + Convert.ToInt32(BendingMoment / KHeight));
            }
            // 0 <= z3 <= 0.3(q1 - q2)// парабола
            for (double step = 0; step < (VDistributedEndLength - VDistributedStartLength); step += Step)
            {
                x += Step;
                Plots.PlotThree(Ya, step);
                list1.Add(x, VBendingMoment);
                //Console.ForegroundColor = ConsoleColor.Blue;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 30 + Convert.ToInt32(LateralForce / KHeight));
                //Console.ForegroundColor = ConsoleColor.Green;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 40 + Convert.ToInt32(BendingMoment / KHeight));
            }
            // 0 <= z4 <= 0.2(q2 - F2)
            for (double step = (VForceTwoLength - VDistributedEndLength); step > 0; step -= Step)
            {
                x += Step;
                Plots.PlotFive(Yb, step);
                list1.Add(x, VBendingMoment);
                //Console.ForegroundColor = ConsoleColor.Blue;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 30 + Convert.ToInt32(LateralForce / KHeight));
                //Console.ForegroundColor = ConsoleColor.Green;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 40 + Convert.ToInt32(BendingMoment / KHeight));
            }
            //// 0 <= z5 <= 0.2(F2 - l)
            for (double step = (VLength - VForceTwoLength); step > 0; step -= Step) // Очень дебильный код (не хочу испровлять)
            {
                x += Step;
                //Plots.PlotFour(Yb, step);
                //list1.Add(x, VBendingMoment);
                //Console.ForegroundColor = ConsoleColor.Blue;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 30 + Convert.ToInt32(LateralForce / KHeight));
                //Console.ForegroundColor = ConsoleColor.Green;
                //Painting.PutPixel(Convert.ToInt32(x * KLong), 40 + Convert.ToInt32(BendingMoment / KHeight));
            }
            Plots.PlotFour(Yb, 0);
            list1.Add(x, VBendingMoment); // типа замена 5 этапу

            LineItem MyLine1 = BendingMomentPlot.GraphPane.AddCurve("M экстремум = ", list1, Color.Black, SymbolType.None);
            MyLine1.Line.Width = 5;
            MyLine1.Line.Fill = new Fill(Color.Green);
            MyLine1.Symbol.IsVisible = false;

            BendingMomentPlot.RestoreScale(BendingMomentPlot.GraphPane);
        }

        private bool Examination()
        {
            if (((0 < (double)MeaningStrengthOneM.Value) && ((double)MeaningStrengthOneM.Value < (double)MeaningStartDistributedLoadM.Value)) != true)
            {
                MessageBox.Show("Растояние (Сила 1) не в диапазоне (0 - Распр. нагр. 1) !");
                return true;
            }
            if ((((double)MeaningStrengthOneM.Value < (double)MeaningStartDistributedLoadM.Value) && ((double)MeaningStartDistributedLoadM.Value < (double)MeaningEndOfDistributedLoadM.Value)) != true)
            {
                MessageBox.Show("Растояние (Распр. нагр. 1) не в диапазоне (Сила 1 - Распр. нагр. 2) !");
                return true;
            }
            if ((((double)MeaningStartDistributedLoadM.Value < (double)MeaningEndOfDistributedLoadM.Value) && ((double)MeaningEndOfDistributedLoadM.Value < (double)MeaningStrengthTwoM.Value)) != true)
            {
                MessageBox.Show("Растояние (Распр. нагр. 2) не в диапазоне (Распр. нагр. 1 - Сила 2) !");
                return true;
            }
            if ((((double)MeaningEndOfDistributedLoadM.Value < (double)MeaningStrengthTwoM.Value) && ((double)MeaningStrengthTwoM.Value < (double)MeaningLengthM.Value)) != true)
            {
                MessageBox.Show("Растояние (Сила 2) не в диапазоне (Распр. нагр. 2 - Длина) !");
                return true;
            }

            VDistributedLoad = (double)MeaningDistributedForceNM.Value;
            VPowerOne = (double)MeaningStrengthOneN.Value;
            VPowerTwo = (double)MeaningStrengthTwoN.Value;
            VLength = (double)MeaningLengthM.Value;

            VForceOneLength = (double)MeaningStrengthOneM.Value;
            VForceTwoLength = (double)MeaningStrengthTwoM.Value;
            VDistributedStartLength = (double)MeaningStartDistributedLoadM.Value;
            VDistributedEndLength = (double)MeaningEndOfDistributedLoadM.Value;

            return false;
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            if (AllocConsole())
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine(" ██████╗ ██████╗ ██████╗ ███████╗    ██╗  ██╗ █████╗ ");
                Console.WriteLine("██╔════╝██╔═══██╗██╔══██╗██╔════╝    ██║  ██║██╔══██╗");
                Console.WriteLine("██║     ██║   ██║██████╔╝█████╗      ███████║╚██████║");
                Console.WriteLine("██║     ██║   ██║██╔══██╗██╔══╝      ╚════██║ ╚═══██║");
                Console.WriteLine("╚██████╗╚██████╔╝██║  ██║███████╗         ██║ █████╔╝");
                Console.WriteLine(" ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚══════╝         ╚═╝ ╚════╝ ");
                Console.SetCursorPosition(0, 10);

                int kol = 28;

                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("DistributedLoad(N/M) = " + VDistributedLoad);
                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("PowerOne(N) = " + VPowerOne); 
                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("PowerTwo(N) = " + VPowerTwo);
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Length(M) = " + VLength);

                Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(); Console.WriteLine("---------------- Length ----------------"); Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("ForceOneLength(M) = " + VForceOneLength); 
                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("ForceTwoLength(M) = " + VForceTwoLength); 
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("DistributedStartLength(M) = " + VDistributedStartLength); 
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("DistributedEndLength(M) = " + VDistributedEndLength); 

                Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(); Console.WriteLine("Want to change settings (n/y) "); 
                
                // 14
                
                if (Console.ReadLine() == "y") 
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("DistributedLoad(N/M) = "); VDistributedLoad = Convert.ToDouble(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Green; Console.Write("PowerOne(N) = "); VPowerOne = Convert.ToDouble(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Green; Console.Write("PowerTwo(N) = "); VPowerTwo = Convert.ToDouble(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Red; Console.Write("Length(M) = "); VLength = Convert.ToDouble(Console.ReadLine());

                    Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(); Console.WriteLine("---------------- Length ----------------"); Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Green; Console.Write("ForceOneLength(M) = "); VForceOneLength = Convert.ToDouble(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Green; Console.Write("ForceTwoLength(M) = "); VForceTwoLength = Convert.ToDouble(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("DistributedStartLength(M) = "); VDistributedStartLength = Convert.ToDouble(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("DistributedEndLength(M) = "); VDistributedEndLength = Convert.ToDouble(Console.ReadLine());

                    kol += 11;
                } //11

                Console.WriteLine();


                Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine("OneStageFastening");
                Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("TwoDegreeFastening");


                int[] GRAPHICS = new int[] { 10, 20, 40 };
                // Коофиценты маштабирование
                int KLong = 100;
                int KHeight = 250;

                Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(); Console.WriteLine("---------------- Output settings ----------------"); Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Conclusion 1 = " + GRAPHICS[0]);
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Conclusion 2 = " + GRAPHICS[1]);
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Conclusion 3 = " + GRAPHICS[2]);
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Beam length = " + KLong);
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Height = " + KHeight);

                Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(); Console.WriteLine("Want to change your graphics settings (n/y) ");

                //14

                if (Console.ReadLine() == "y")
                {
                    Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(); Console.WriteLine("---------------- Output settings ----------------"); Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red; Console.Write("Conclusion 1 = "); GRAPHICS[0] = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Red; Console.Write("Conclusion 2 = "); GRAPHICS[1] = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Red; Console.Write("Conclusion 3 = "); GRAPHICS[2] = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Beam length = "); KLong = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Height = "); KHeight = Convert.ToInt32(Console.ReadLine());

                    kol += 8;
                }//8

                kol += Convert.ToInt32(VDistributedLoad / KHeight);

                GRAPHICS[0] += kol + 11;
                GRAPHICS[1] += kol + 11;
                GRAPHICS[2] += kol + 11;

                double Yb = Support.OneStageFastening(); // 2225
                double Ya = Support.TwoDegreeFastening(Yb); // 1275

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Реакция " + Yb + " ");
                Console.WriteLine(Ya);
                // 0 <= z1 <= 0.25(0 - F1)
                Plots.PlotOne(Ya, 0);
                Console.Write("1 " + VLateralForce + " ");
                Console.WriteLine(VBendingMoment);
                Plots.PlotOne(Ya, VForceOneLength);
                Console.Write(VLateralForce);
                Console.WriteLine(VBendingMoment);
                // 0 <= z2 <= 0.05(F1 - q)
                Plots.PlotTwo(Ya, 0);
                Console.Write("2 " + VLateralForce + " ");
                Console.WriteLine(VBendingMoment);
                Plots.PlotTwo(Ya, (VDistributedStartLength - VForceOneLength));
                Console.Write(VLateralForce);
                Console.WriteLine(VBendingMoment);
                // 0 <= z3 <= 0.3(q1 - q2)// парабола
                Plots.PlotThree(Ya, 0);
                Console.Write("3 " + VLateralForce + " ");
                Console.WriteLine(VBendingMoment);
                Plots.PlotThree(Ya, ((VDistributedEndLength - VDistributedStartLength) / 2));
                Console.Write(VLateralForce);
                Console.WriteLine(VBendingMoment);
                Plots.PlotThree(Ya, (VDistributedEndLength - VDistributedStartLength));
                Console.Write(VLateralForce);
                Console.WriteLine(VBendingMoment);
                // 0 <= z4 <= 0.2(F2 - l)
                Plots.PlotFour(Yb, 0);
                Console.Write("5 " + VLateralForce + " ");
                Console.WriteLine(VBendingMoment);
                Plots.PlotFour(Yb, (VLength - VForceTwoLength));
                Console.Write(VLateralForce);
                Console.WriteLine(VBendingMoment);
                // 0 <= z4 <= 0.2(q2 - F2)
                Plots.PlotFive(Yb, 0);
                Console.Write("4 " + VLateralForce + " ");
                Console.WriteLine(VBendingMoment);
                Plots.PlotFive(Yb, (VForceTwoLength - VDistributedEndLength));
                Console.Write(VLateralForce);
                Console.WriteLine(VBendingMoment);

                // 11


                Console.ResetColor();
                Console.SetCursorPosition(0, GRAPHICS[0]);



                Console.ForegroundColor = ConsoleColor.Red; Painting.BresenhamLine(0, GRAPHICS[0], Convert.ToInt32(VLength * KLong), GRAPHICS[0]);
                Console.ForegroundColor = ConsoleColor.Green; Painting.BresenhamLine(Convert.ToInt32(VForceOneLength * KLong), GRAPHICS[0] - Convert.ToInt32(VPowerOne) / KHeight, Convert.ToInt32(VForceOneLength * KLong), GRAPHICS[0]);
                Console.ForegroundColor = ConsoleColor.Green; Painting.BresenhamLine(Convert.ToInt32(VForceTwoLength * KLong), GRAPHICS[0] - Convert.ToInt32(VPowerTwo) / KHeight, Convert.ToInt32(VForceTwoLength * KLong), GRAPHICS[0]);
                for (int i = 0; i < Convert.ToInt32(VDistributedLoad / KHeight); i++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; Painting.BresenhamLine(Convert.ToInt32(VDistributedStartLength * KLong), GRAPHICS[0] - i, Convert.ToInt32(VDistributedEndLength * KLong), GRAPHICS[0] - i);
                }
                Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(0, GRAPHICS[0] + 1); Console.WriteLine("█");
                Console.ForegroundColor = ConsoleColor.Blue; Console.SetCursorPosition(Convert.ToInt32(VLength * KLong), GRAPHICS[0] + 1); Console.WriteLine("█");

                Console.ResetColor();
                Console.SetCursorPosition(0, GRAPHICS[1]);

                Console.ForegroundColor = ConsoleColor.Red; Painting.BresenhamLine(0, GRAPHICS[1], Convert.ToInt32(VLength * KLong), GRAPHICS[1]);
                Console.ForegroundColor = ConsoleColor.Red; Painting.BresenhamLine(0, GRAPHICS[2], Convert.ToInt32(VLength * KLong), GRAPHICS[2]);

                

                double x = 0;
                double Step = 0.01;

                // 0 <= z1 <= 0.25(0 - F1)
                for (double step = 0; step < VForceOneLength; step += Step)
                {
                    x += Step;
                    Plots.PlotOne(Ya, step);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[1] + Convert.ToInt32(VLateralForce / KHeight));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[2] + Convert.ToInt32(VBendingMoment / KHeight));
                }
                // 0 <= z2 <= 0.05(F1 - q)
                for (double step = 0; step < (VDistributedStartLength - VForceOneLength); step += Step)
                {
                    x += Step;
                    Plots.PlotTwo(Ya, step);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[1] + Convert.ToInt32(VLateralForce / KHeight));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[2] + Convert.ToInt32(VBendingMoment / KHeight));
                }
                // 0 <= z3 <= 0.3(q1 - q2)// парабола
                for (double step = 0; step < (VDistributedEndLength - VDistributedStartLength); step += Step)
                {
                    x += Step;
                    Plots.PlotThree(Ya, step);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[1] + Convert.ToInt32(VLateralForce / KHeight));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[2] + Convert.ToInt32(VBendingMoment / KHeight));
                }
                // 0 <= z4 <= 0.2(q2 - F2)
                for (double step = 0; step < (VForceTwoLength - VDistributedEndLength); step += Step)
                {
                    x += Step;
                    Plots.PlotFive(Yb, step);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[1] + Convert.ToInt32(VLateralForce / KHeight));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[2] + Convert.ToInt32(VBendingMoment / KHeight));
                }
                // 0 <= z4 <= 0.2(F2 - l)
                for (double step = 0; step < (VLength - VForceTwoLength); step += Step)
                {
                    x += Step;
                    Plots.PlotFour(Yb, step);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[1] + Convert.ToInt32(VLateralForce / KHeight));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[2] + Convert.ToInt32(VBendingMoment / KHeight));
                }



                Console.ReadKey();

                FreeConsole();
            }
        }
    }
}
