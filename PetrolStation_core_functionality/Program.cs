using System;
using System.Timers;

namespace PetrolStation
{
    public class Program
    {

        private static int newVehicle = 1500;
        private static int refuelTime = 8000;
        private static double litresDispensed = 0;
        private static int avaliablePumps = 9;
        private static int carsCreated = 0;
        private static int carsServed = 0;
        private static bool running = true;
        private static int carsQueue = 0;
        private static int carsLeftEarly = 0;

        private static System.Timers.Timer createVechile;
        private static System.Timers.Timer refuel;

        static void Main(string[] args)
        {
            VechileTimer();
            Refuel();

            do
            {
                Console.WriteLine("New vehicle arrived, please select pump");
                string userInput = Console.ReadLine();
                avaliablePumps -= 1;
                Console.WriteLine($"Vechile assigned to pump {userInput}, {avaliablePumps} pumps avaliable");
                Console.WriteLine("\n");
                Console.WriteLine(new string('#', 100));
                //Console.WriteLine("\n");
                //Console.Clear();

                Console.WriteLine($"Queue: {carsQueue} \n" +
                    $"Cars: {carsCreated} \n"+
                    $"Litres sold: {litresDispensed} \n" +
                    $"Cost: \n" +
                    $"1%: \n" +
                    $"Vehicles serviced: {carsServed} \n" +
                    $"Avaliable pumps: {avaliablePumps} \n" +
                    $"Left early: \n");

            } while (avaliablePumps > 0);
        }


        private static void VechileTimer()
        {
            createVechile = new System.Timers.Timer(newVehicle);
            createVechile.Enabled = true;
            createVechile.AutoReset = true;
            createVechile.Elapsed += VehicleTimer;
            createVechile.Start();
        }

        private static void Refuel()
        {
            refuel = new System.Timers.Timer(refuelTime);
            refuel.Enabled = true;
            refuel.AutoReset = true;
            refuel.Elapsed += RefuelTimer;
            refuel.Start();
        }


        private static void VehicleTimer(Object source, ElapsedEventArgs e)
        {
            carsCreated += 1;
            carsQueue += 1;
        }

        private static void RefuelTimer(Object source, ElapsedEventArgs e)
        {
            double fuelDispensed = 1.5 * 8;
            litresDispensed += fuelDispensed;
            carsServed += 1;
            avaliablePumps += 1;
            carsQueue -= 1;
            //Console.WriteLine($"{carsServed} cars served; {litresDispensed} litres of fuel sold, {avaliablePumps} pumps avaliable");
        }

    }

}