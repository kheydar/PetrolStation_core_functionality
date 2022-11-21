using System;
using System.Timers;
using System.Threading;

namespace PetrolStation
{
    public class Program
    {
        Random random = new Random();
        private static int newVehicle;
        private static int refuelTime;
        private static double litresDispensed = 0;
        private static int avaliablePumps = 9;
        private static int carsCreated = 0;
        private static int carsServed = 0;
        private static bool running = true;
        private static bool correctPin;
        private static int carsQueue = 0;
        private static int carsLeftEarly = 0;
        private static double fuelCost = 1.5;
        private static double cost;
        private static double commision;
        

        private static System.Timers.Timer createVechile;
        private static System.Timers.Timer refuel;

        static void Main(string[] args)
        {
            Random random = new Random();
            newVehicle = random.Next(1500, 2200);
            refuelTime = random.Next(3000, 5000);

            Console.ForegroundColor = ConsoleColor.DarkYellow;        
            Console.WriteLine("Welcome to Broken Petrol Ltd");
            Console.WriteLine("\n\n");
            Console.WriteLine("Please login to continue");
            string login = Console.ReadLine();

            if (login =="123")
            {
                correctPin = true;
            }

            else
            {
                correctPin = false;
            }

            if (!correctPin)
            {
                do
                {
                   
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n");
                    Console.WriteLine("Incorrect pin, please try again...");
                    string input = Console.ReadLine();
                    if (input == "123")
                    {
                        correctPin = true;
                    }
                    
                } while (!correctPin);
            }

            if (correctPin)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n");
                    Console.WriteLine("Log in successful");
                    VechileTimer();
                    Refuel();

                    do
                    {
                        while (avaliablePumps==0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Waiting for a free pump...");
                            Thread.Sleep(2000);
                            Console.WriteLine("\n");
                        }

                        Console.ResetColor();

                        //newVehicle = random.Next(1500, 2200);
                        //refuelTime = random.Next(3000, 5000);

                        Console.WriteLine($"New vehicle after {newVehicle} \n" +
                        $"Refuel in {refuelTime}\n");

                        Console.WriteLine("New vehicle arrived, please select pump or type 'quit' to close the program'");
                        string userInput = Console.ReadLine();

                        if (userInput.ToLower() == "quit")
                        {
                            running = false;
                        }

                        avaliablePumps -= 1;
                        Console.WriteLine("\n");
                        Console.WriteLine($"Vechile assigned to pump {userInput}, {avaliablePumps} pumps avaliable");
                        Console.WriteLine("\n");
                        Console.WriteLine(new string('#', 100));
                        //Console.Clear();

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Queue: {carsQueue} \n" +
                            $"Cars: {carsCreated} \n"+
                            $"Litres sold: {litresDispensed} \n" +
                            $"Cost: £ {cost}\n" +
                            $"1%: £ {commision}\n" +
                            $"Vehicles serviced: {carsServed} \n" +
                            $"Avaliable pumps: {avaliablePumps} \n"
                            //+$"Left early: \n"
                            );


                    } while (running);
                }
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
            double fuelDispensed = 1.5 * (refuelTime/1000);
            litresDispensed += fuelDispensed;
            cost = litresDispensed * fuelCost;
            commision = cost * 0.01;
            carsServed += 1;
            avaliablePumps += 1;
            carsQueue -= 1;
        }


    }
}