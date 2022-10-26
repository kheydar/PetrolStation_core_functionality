using System;
using System.Timers;

//int newVehicle = 1500;
//double litresDispensed = 0;
//int fuelProcess = 8000;
//int avaliablePupms = 9;


public class Example
{
    private static System.Timers.Timer aTimer;

    public static void Main()
    {
        SetTimer();

        Console.WriteLine("\nPress the Enter key to exit the application...\n");
        Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
        Console.ReadLine();
        aTimer.Stop();
        aTimer.Dispose();

        //Console.WriteLine("Terminating the application...");
    }

    private static void SetTimer()
    {
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(1500);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }

    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        int avaliablePupms = 9;
        //Console.WriteLine("New vechile created at {0:HH:mm:ss.fff}", e.SignalTime);
        

        do
        {
            Console.WriteLine("New vehicle created, select which pump to use");
            int pumpChoice = Int32.Parse(Console.ReadLine());
        }

        while (avaliablePupms >=1);
    }
}
