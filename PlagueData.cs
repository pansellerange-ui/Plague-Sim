using System;

public static class PlagueData
{
    // Infection rate based on year and region
    public static double GetInfectionRate(int year, string region)
    {
        double baseRate;

        // Older years = faster spread
        if (year < 1500) baseRate = 0.30;        // Medieval plagues
        else if (year < 1900) baseRate = 0.20;   // 17th–18th century
        else if (year < 2000) baseRate = 0.15;   // Early modern medicine
        else baseRate = 0.10;                    // Modern era

        // Regional modifiers (simple)
        double regionMultiplier = region.ToLower() switch
        {
            "europe" => 1.0,
            "asia" => 1.1,
            "africa" => 1.2,
            "northamerica" => 0.9,
            _ => 1.0
        };

        return baseRate * regionMultiplier;
    }

    // Recovery rate based on year and region
    public static double GetRecoveryRate(int year, string region)
    {
        double baseRate;

        // Older years = poor recovery
        if (year < 1500) baseRate = 0.01;
        else if (year < 1900) baseRate = 0.02;
        else if (year < 2000) baseRate = 0.04;
        else baseRate = 0.08;

        // Densely populated areas recover slower
        double regionPenalty = region.ToLower() switch
        {
            "asia" => 0.9,
            "africa" => 0.8,
            _ => 1.0
        };

        return baseRate * regionPenalty;
    }
}
