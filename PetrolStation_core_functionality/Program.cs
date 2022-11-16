using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace PetrolStation
{
    public class Program
    {
        
        static int newVehicle = 1500;
        static int refuelTime = 8000;
        static double litresDispensed = 0;
        static int avaliablePumps = 9;
        static int carsCreated = 0;
        static int carsServed = 0;
        static bool running = true;

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
            refuel = new Timer(refuelTime);
            refuel.Enabled = true;
            refuel.AutoReset = true;
            refuel.Elapsed += RefuelTimer;
            refuel.Start();
        }


        private static void VehicleTimer(Object source, ElapsedEventArgs e)
        {
            carsCreated += 1;
        }

        private static void RefuelTimer(Object source, ElapsedEventArgs e)
        {
            double fuelDispensed = 1.5 * 8;
            litresDispensed += fuelDispensed;
            carsServed += 1;
            avaliablePumps += 1;
            Console.WriteLine($"{carsServed} cars served; {litresDispensed} litres of fuel sold, {avaliablePumps} pumps avaliable");
        }

    }

}