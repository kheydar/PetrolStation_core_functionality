using System;
using System.Timers;
using System.Threading;
using System.IO;

namespace PetrolStation
{
    public class Program
    {
        private static double litresDispensed = 0;
        private static int avaliablePumps = 9;
        private static int carsCreated = 0;
        private static int carsServed = 0;
        private static bool running = true;
        private static bool correctPin;
        private static int carsQueue = 0;
        private static int carsLeftEarly = 0;
        private static readonly double fuelCost = 1.5;
        private static double cost;
        private static double commision;
        private static System.Timers.Timer createVechile;
        private static System.Timers.Timer refuel;
        private static System.Timers.Timer display;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;        
            Console.WriteLine("Welcome to Broken Petrol Ltd");
            Console.WriteLine("\n\n");

            Login();

            if (correctPin)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n");
                    Console.WriteLine("Log in successful");
                    Console.ResetColor();

                    //Display();
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
                        Console.WriteLine("New vehicle arrived, please select pump or type 'quit' to close the program");
                        string userInput = Console.ReadLine();

                        if (userInput.ToLower() == "quit")
                        {
                            running = false;
                            Console.WriteLine("Closing application...");
                            File.WriteAllText(@"../../../output.txt", "Daily report: \n\n" +
                            $"Litres dispensed: {litresDispensed} \n" +
                            $"Fuel cost: £ {cost}\n" +
                            $"1%: £ {commision}\n" +
                            $"Vehicles serviced: {carsServed} \n" +
                            $"Left early {carsLeftEarly}\n");    

                        }
                        
                        avaliablePumps -= 1;
                        
                        Console.WriteLine("\n");
                        Console.WriteLine($"Vechile assigned to pump {userInput}, {avaliablePumps} pumps avaliable");
                        Console.WriteLine("\n");
                        Console.WriteLine(new string('#', 80));

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        var output = 
                            $"Queue: {carsQueue} \n" +
                            //$"Cars: {carsCreated} \n" +
                            $"Litres dispensed: {litresDispensed} \n" +
                            $"Fuel cost: £ {cost}\n" +
                            $"1%: £ {commision}\n" +
                            $"Vehicles serviced: {carsServed} \n" +
                            $"Avaliable pumps: {avaliablePumps} \n" +
                            $"Left early {carsLeftEarly}\n"
                            ;

                        Console.WriteLine(output);

                    } while (running);
                }
            }
        private static void Login()
        {
            string password = System.IO.File.ReadAllText(@"../../../login.txt");
            Console.WriteLine("Please login to continue or type 'quit' to close the program");
            string login = Console.ReadLine();

            if (login.ToLower() == "quit")
            {
                Console.WriteLine("Closing application...");
                System.Environment.Exit(1);
            }

            else if (login == password)
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
                    Console.WriteLine("Incorrect pin, please try again... or type 'quit' to close the program");
                    string input = Console.ReadLine();

                    if (input.ToLower() == "quit")
                    {
                        Console.WriteLine("Closing application...");
                        System.Environment.Exit(1);
                    }

                    else if (input == password)
                    {
                        correctPin = true;
                    }

                } while (!correctPin);
            }
        }


        private static void Display()
        {
            display = new System.Timers.Timer(1000);
            display.Enabled = true;
            display.AutoReset = true;
            display.Elapsed += DisplayTimer;
            display.Start();
        }

        private static void VechileTimer()
        {
            Random random = new Random();
            int newVehicle = random.Next(1500, 2200);

            createVechile = new System.Timers.Timer();
            createVechile.Interval = newVehicle;
            createVechile.Enabled = true;
            createVechile.AutoReset = false;
            createVechile.Elapsed += VehicleTimer;
            createVechile.Start();
        }

        private static void Refuel()
        {
            Random random = new Random();
            int refuelTime = random.Next(3000, 5000);
            refuel = new System.Timers.Timer();
            refuel.Interval = refuelTime;
            refuel.Enabled = true;
            refuel.AutoReset = false;
            refuel.Elapsed += RefuelTimer;
            refuel.Start();
        }

        private static void DisplayTimer(Object source, ElapsedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(
                $"Queue: {carsQueue} \n" +
                //$"Cars: {carsCreated} \n" +
                $"Litres dispensed: {litresDispensed} \n" +
                $"Fuel cost: £ {cost}\n" +
                $"1%: £ {commision}\n" +
                $"Vehicles serviced: {carsServed} \n" +
                $"Avaliable pumps: {avaliablePumps} \n" +
                $"Left early {carsLeftEarly}"
                );
        }

        private static void VehicleTimer(Object source, ElapsedEventArgs e)
        {
            Random random = new Random();
            int newVehicle = random.Next(1500, 2200);
            createVechile.Interval = newVehicle;

            if (avaliablePumps == 0)
            {
                carsCreated += 1;
                carsQueue += 1;
            }

            if (carsQueue >5)
            {
                carsCreated += 1;
                carsLeftEarly += 1;
            }

            else
            {
                carsCreated += 1;
                carsQueue += 1;
            }
            
        }

        private static void RefuelTimer(Object source, ElapsedEventArgs e)
        {
            Random random = new Random();
            int refuelTime = random.Next(3000, 5000);
            refuel.Interval = refuelTime;

            double fuelDispensed = 1.5 * (refuelTime/1000);
            litresDispensed += fuelDispensed;
            cost = litresDispensed * fuelCost;
            commision = cost * 0.01;
            carsServed += 1;

            if (avaliablePumps < 9)
            {
                avaliablePumps += 1;
            }

            if (carsQueue > 0)
            {
                carsQueue -= 1;
            }
        }
    }
}



