using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace PetrolStation
{
    public class Program
    {
        static Timer createVechile;
        static Timer refuel;

        static int newVehicle = 1500;
        static int refuelTime = 8000;
        static double litresDispensed = 0;
        static int avaliablePumps = 9;
        static int carsCreated = 0;
        static int carsServed = 0;

        static void Main(string[] args)
        {
            createVechile = new Timer(newVehicle);
            refuel = new Timer(refuelTime);

            createVechile.Enabled = true;
            createVechile.AutoReset = true;
            createVechile.Elapsed += VehicleTimer;
            createVechile.Start();

            refuel.Enabled = true;
            refuel.AutoReset = true;
            refuel.Elapsed += RefuelTimer;
            refuel.Start();

            Console.ReadKey();

        }

        private static void VehicleTimer(Object source, ElapsedEventArgs e)
        {
            carsCreated += 1;
            avaliablePumps -= 1;
            Console.WriteLine($"{carsCreated} cars created, {avaliablePumps} pumps avaliable");
        }

        private static void RefuelTimer(Object source, ElapsedEventArgs e)
        {
            double fuelDispensed = 1.5 * 8;
            litresDispensed += fuelDispensed;
            carsServed += 1;
            avaliablePumps += 1;
            Console.WriteLine($"{carsServed} cars served and {litresDispensed} total fuel, {avaliablePumps} pumps avaliable");
        }

    }


}