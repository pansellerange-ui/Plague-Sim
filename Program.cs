using System;

class Program
{
    static int susceptible;
    static int infected;
    static int recovered;
    static double infectionRate;
    static double recoveryRate;

    static void Main()
    {
        Console.WriteLine("=== Simple Plague Simulation ===");
        Console.WriteLine();

        Console.Write("Enter region (Europe, Asia, Africa, NorthAmerica): ");
        string region = Console.ReadLine();

        Console.Write("Enter year (e.g. 1350, 1700, 1918, 2020): ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Enter population size: ");
        int population = int.Parse(Console.ReadLine());

        Console.Write("Enter initial infected: ");
        infected = int.Parse(Console.ReadLine());

        recovered = 0;
        susceptible = population - infected;

        infectionRate = PlagueData.GetInfectionRate(year, region);
        recoveryRate = PlagueData.GetRecoveryRate(year, region);

        Console.WriteLine();
        Console.WriteLine($"Using infection rate = {infectionRate}");
        Console.WriteLine($"Using recovery rate = {recoveryRate}");
        Console.WriteLine();

        int totalDays = 60;
        int stepDays = 5;

        for (int day = 1; day <= totalDays; day += stepDays)
        {
            for (int i = 0; i < stepDays; i++)
            {
                SimulateDay();
            }

            if (infected < 0) infected = 0;

            Console.WriteLine(
                $"Day {day + stepDays - 1}: Susceptible={susceptible}, Infected={infected}, Recovered={recovered}"
            );

            if (infected == 0)
            {
                Console.WriteLine("The outbreak has ended.");
                break;
            }
        }

        Console.WriteLine();
        Console.WriteLine("Simulation complete.");
    }

    static void SimulateDay()
    {
        int newInfections = (int)(infectionRate * infected * susceptible / (susceptible + infected + recovered + 1));
        int newRecoveries = (int)(recoveryRate * infected);

        susceptible -= newInfections;
        infected += newInfections - newRecoveries;
        recovered += newRecoveries;

        if (susceptible < 0) susceptible = 0;
        if (infected < 0) infected = 0;
    }
}
