using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Simple Plague Simulation ===");
        Console.WriteLine();

        // Ask user for a region
        Console.Write("Enter region (Europe, Asia, Africa, NorthAmerica): ");
        string region = Console.ReadLine();

        // Ask user for year
        Console.Write("Enter year (e.g. 1350, 1700, 1918, 2020): ");
        int year = int.Parse(Console.ReadLine());

        // Ask user for population
        Console.Write("Enter population size: ");
        int population = int.Parse(Console.ReadLine());

        // Ask user for starting infected
        Console.Write("Enter initial infected: ");
        int infected = int.Parse(Console.ReadLine());

        // Recovery starts at zero
        int recovered = 0;

        // Everyone not infected or recovered is susceptible
        int susceptible = population - infected;

        // Get infection + recovery rates based on year + region
        double infectionRate = PlagueData.GetInfectionRate(year, region);
        double recoveryRate = PlagueData.GetRecoveryRate(year, region);

        Console.WriteLine();
        Console.WriteLine($"Using infection rate = {infectionRate}");
        Console.WriteLine($"Using recovery rate = {recoveryRate}");
        Console.WriteLine();

        // Run simulation for 60 days
        for (int day = 1; day <= 60; day++)
        {
            // New infections depend on how many infected people exist
            int newInfections = (int)(infected * infectionRate);

            // Make sure we don't infect more than the number of susceptible people
            newInfections = Math.Min(newInfections, susceptible);

            // New recoveries depend on how many people are infected
            int newRecoveries = (int)(infected * recoveryRate);

            // Update counts
            susceptible -= newInfections;
            infected += newInfections;
            infected -= newRecoveries;
            recovered += newRecoveries;

            // Prevent negative weirdness
            if (infected < 0) infected = 0;

            Console.WriteLine(
                $"Day {day}: Susceptible={susceptible}, Infected={infected}, Recovered={recovered}"
            );

            // Stop if outbreak ends early
            if (infected == 0)
            {
                Console.WriteLine("The outbreak has ended.");
                break;
            }
        }

        Console.WriteLine();
        Console.WriteLine("Simulation complete.");
    }
}
