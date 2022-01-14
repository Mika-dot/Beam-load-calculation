using System;


//__/\\\\\\\\\\\\\\\___________________________________________________________________________________ /\\\_________ /\\\\\\\\\____
// _\/\\\///////////___________________________________________________________________________________/\\\\\_______/\\\///////\\\__       
//  _\/\\\_____________________________/\\\\\\\\___/\\\_______________________________________________/\\\/\\\______/\\\______\//\\\_      
//   _\/\\\\\\\\\\\______/\\/\\\\\\____/\\\////\\\_\///___/\\/\\\\\\_______/\\\\\\\\_________________/\\\/\/\\\_____\//\\\_____/\\\\\_     
//    _\/\\\///////______\/\\\////\\\__\//\\\\\\\\\__/\\\_\/\\\////\\\____/\\\/////\\\______________/\\\/__\/\\\______\///\\\\\\\\/\\\_    
//     _\/\\\_____________\/\\\__\//\\\__\///////\\\_\/\\\_\/\\\__\//\\\__/\\\\\\\\\\\_____________/\\\\\\\\\\\\\\\\_____\////////\/\\\_   
//      _\/\\\_____________\/\\\___\/\\\__/\\_____\\\_\/\\\_\/\\\___\/\\\_\//\\///////_____________\///////////\\\//____/\\________/\\\__  
//       _\/\\\\\\\\\\\\\\\_\/\\\___\/\\\_\//\\\\\\\\__\/\\\_\/\\\___\/\\\__\//\\\\\\\\\\_____________________\/\\\_____\//\\\\\\\\\\\/___ 
//        _\///////////////__\///____\///___\////////___\///__\///____\///____\//////////______________________\///_______\///////////_____

namespace EngineeringProject_idiotic_
{
    class Program
    {
        // Значения конструкции (основные).
        public static double DistributedLoad = 5000;       
        public static double PowerOne = 2000;
        public static double PowerTwo = 3000;
        public static double Length = 1;

        // Размеры (которых нету на чертиже) отмеряющие растояние до точек приложения нагрузок.
        public static double ForceOneLength = 0.25;
        public static double ForceTwoLength = 0.8;
        public static double DistributedStartLength = 0.3;
        public static double DistributedEndLength = 0.6;

        // Переменные (временные для расчетов) момент и сила (Q и M). // Это могут быть не глобальные так как все расчеты видутся заного!!!!!!!
        public static double LateralForce = 0;
        public static double BendingMoment = 0;

        static void Main(string[] args)
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

            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("DistributedLoad(N/M) = " + DistributedLoad);
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("PowerOne(N) = " + PowerOne);
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("PowerTwo(N) = " + PowerTwo);
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Length(M) = " + Length);

            Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(); Console.WriteLine("---------------- Length ----------------"); Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("ForceOneLength(M) = " + ForceOneLength);
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("ForceTwoLength(M) = " + ForceTwoLength);
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("DistributedStartLength(M) = " + DistributedStartLength);
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("DistributedEndLength(M) = " + DistributedEndLength);

            Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(); Console.WriteLine("Want to change settings (n/y) ");

            // 14

            if (Console.ReadLine() == "y")
            {
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("DistributedLoad(N/M) = "); DistributedLoad = Convert.ToDouble(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Green; Console.Write("PowerOne(N) = "); PowerOne = Convert.ToDouble(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Green; Console.Write("PowerTwo(N) = "); PowerTwo = Convert.ToDouble(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Red; Console.Write("Length(M) = "); Length = Convert.ToDouble(Console.ReadLine());

                Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(); Console.WriteLine("---------------- Length ----------------"); Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green; Console.Write("ForceOneLength(M) = "); ForceOneLength = Convert.ToDouble(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Green; Console.Write("ForceTwoLength(M) = "); ForceTwoLength = Convert.ToDouble(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("DistributedStartLength(M) = "); DistributedStartLength = Convert.ToDouble(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("DistributedEndLength(M) = "); DistributedEndLength = Convert.ToDouble(Console.ReadLine());

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

            kol += Convert.ToInt32(DistributedLoad / KHeight);

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
            Console.Write("1 " + LateralForce + " ");
            Console.WriteLine(BendingMoment);
            Plots.PlotOne(Ya, ForceOneLength);
            Console.Write(LateralForce);
            Console.WriteLine(BendingMoment);
            // 0 <= z2 <= 0.05(F1 - q)
            Plots.PlotTwo(Ya, 0);
            Console.Write("2 " + LateralForce + " ");
            Console.WriteLine(BendingMoment);
            Plots.PlotTwo(Ya, (DistributedStartLength - ForceOneLength));
            Console.Write(LateralForce);
            Console.WriteLine(BendingMoment);
            // 0 <= z3 <= 0.3(q1 - q2)// парабола
            Plots.PlotThree(Ya, 0);
            Console.Write("3 " + LateralForce + " ");
            Console.WriteLine(BendingMoment);
            Plots.PlotThree(Ya, ((DistributedEndLength - DistributedStartLength) / 2));
            Console.Write(LateralForce);
            Console.WriteLine(BendingMoment);
            Plots.PlotThree(Ya, (DistributedEndLength - DistributedStartLength));
            Console.Write(LateralForce);
            Console.WriteLine(BendingMoment);
            // 0 <= z4 <= 0.2(F2 - l)
            Plots.PlotFour(Yb, 0);
            Console.Write("5 " + LateralForce + " ");
            Console.WriteLine(BendingMoment);
            Plots.PlotFour(Yb, (Length - ForceTwoLength));
            Console.Write(LateralForce);
            Console.WriteLine(BendingMoment);
            // 0 <= z4 <= 0.2(q2 - F2)
            Plots.PlotFive(Yb, 0);
            Console.Write("4 " + LateralForce + " ");
            Console.WriteLine(BendingMoment);
            Plots.PlotFive(Yb, (ForceTwoLength - DistributedEndLength));
            Console.Write(LateralForce);
            Console.WriteLine(BendingMoment);

            // 11


            Console.ResetColor();
            Console.SetCursorPosition(0, GRAPHICS[0]);



            Console.ForegroundColor = ConsoleColor.Red; Painting.BresenhamLine(0, GRAPHICS[0], Convert.ToInt32(Length * KLong), GRAPHICS[0]);
            Console.ForegroundColor = ConsoleColor.Green; Painting.BresenhamLine(Convert.ToInt32(ForceOneLength * KLong), GRAPHICS[0] - Convert.ToInt32(PowerOne) / KHeight, Convert.ToInt32(ForceOneLength * KLong), GRAPHICS[0]);
            Console.ForegroundColor = ConsoleColor.Green; Painting.BresenhamLine(Convert.ToInt32(ForceTwoLength * KLong), GRAPHICS[0] - Convert.ToInt32(PowerTwo) / KHeight, Convert.ToInt32(ForceTwoLength * KLong), GRAPHICS[0]);
            for (int i = 0; i < Convert.ToInt32(DistributedLoad / KHeight); i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow; Painting.BresenhamLine(Convert.ToInt32(DistributedStartLength * KLong), GRAPHICS[0] - i, Convert.ToInt32(DistributedEndLength * KLong), GRAPHICS[0] - i);
            }
            Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(0, GRAPHICS[0] + 1); Console.WriteLine("█");
            Console.ForegroundColor = ConsoleColor.Blue; Console.SetCursorPosition(Convert.ToInt32(Length * KLong), GRAPHICS[0] + 1); Console.WriteLine("█");

            Console.ResetColor();
            Console.SetCursorPosition(0, GRAPHICS[1]);

            Console.ForegroundColor = ConsoleColor.Red; Painting.BresenhamLine(0, GRAPHICS[1], Convert.ToInt32(Length * KLong), GRAPHICS[1]);
            Console.ForegroundColor = ConsoleColor.Red; Painting.BresenhamLine(0, GRAPHICS[2], Convert.ToInt32(Length * KLong), GRAPHICS[2]);



            double x = 0;
            double Step = 0.01;

            // 0 <= z1 <= 0.25(0 - F1)
            for (double step = 0; step < ForceOneLength; step += Step)
            {
                x += Step;
                Plots.PlotOne(Ya, step);
                Console.ForegroundColor = ConsoleColor.Blue;
                Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[1] + Convert.ToInt32(LateralForce / KHeight));
                Console.ForegroundColor = ConsoleColor.Green;
                Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[2] + Convert.ToInt32(BendingMoment / KHeight));
            }
            // 0 <= z2 <= 0.05(F1 - q)
            for (double step = 0; step < (DistributedStartLength - ForceOneLength); step += Step)
            {
                x += Step;
                Plots.PlotTwo(Ya, step);
                Console.ForegroundColor = ConsoleColor.Blue;
                Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[1] + Convert.ToInt32(LateralForce / KHeight));
                Console.ForegroundColor = ConsoleColor.Green;
                Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[2] + Convert.ToInt32(BendingMoment / KHeight));
            }
            // 0 <= z3 <= 0.3(q1 - q2)// парабола
            for (double step = 0; step < (DistributedEndLength - DistributedStartLength); step += Step)
            {
                x += Step;
                Plots.PlotThree(Ya, step);
                Console.ForegroundColor = ConsoleColor.Blue;
                Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[1] + Convert.ToInt32(LateralForce / KHeight));
                Console.ForegroundColor = ConsoleColor.Green;
                Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[2] + Convert.ToInt32(BendingMoment / KHeight));
            }
            // 0 <= z4 <= 0.2(q2 - F2)
            for (double step = 0; step < (ForceTwoLength - DistributedEndLength); step += Step)
            {
                x += Step;
                Plots.PlotFive(Yb, step);
                Console.ForegroundColor = ConsoleColor.Blue;
                Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[1] + Convert.ToInt32(LateralForce / KHeight));
                Console.ForegroundColor = ConsoleColor.Green;
                Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[2] + Convert.ToInt32(BendingMoment / KHeight));
            }
            // 0 <= z4 <= 0.2(F2 - l)
            for (double step = 0; step < (Length - ForceTwoLength); step += Step)
            {
                x += Step;
                Plots.PlotFour(Yb, step);
                Console.ForegroundColor = ConsoleColor.Blue;
                Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[1] + Convert.ToInt32(LateralForce / KHeight));
                Console.ForegroundColor = ConsoleColor.Green;
                Painting.PutPixel(Convert.ToInt32(x * KLong), GRAPHICS[2] + Convert.ToInt32(BendingMoment / KHeight));
            }



            Console.ReadKey();
        }
    }
}
