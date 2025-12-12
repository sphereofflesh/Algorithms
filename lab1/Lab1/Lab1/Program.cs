using Lab01SortPerformance.Algorithms;
using Lab01SortPerformance.Reporting;
using Lab01SortPerformance.Testing;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lab01SortPerformance;

public class QuickSort : ISortingAlgorithm
{
    public string Name => "Quick Sort";

    public SortingResult Sort(int[] array)
    {
        var comparisons = 0;
        var swaps = 0;

        throw new NotImplementedException();

        return new SortingResult
        {
            Array = array,
            Comparisons = comparisons,
            Swaps = swaps
        };
    }
}










public enum TestCase
{
    Sorted,
    Random,
    ReverseSorted,
}

public class SortingTestResult
{
    public string AlgorithmName { get; set; } = string.Empty;
    public int ArraySize { get; set; }
    public TestCase TestCase { get; set; }
    public long ExecutionTimeMs { get; set; }
    public bool IsCorrect { get; set; }
    public int Comparisons { get; set; }
    public int Swaps { get; set; }

    public double ExecutionTimeSeconds => ExecutionTimeMs / 1000.0;

    public override string ToString()
    {
        return $"{AlgorithmName} | Size: {ArraySize} | Case: {TestCase} | Time: {ExecutionTimeMs}ms | Correct: {IsCorrect}";
    }
}

public class ComplexityMeasurement
{
    public List<SortingTestResult> Results { get; set; } = new();

    public void AddResult(SortingTestResult result)
    {
        Results.Add(result);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            RunComplexityAnalysis();
        }
        catch (NotImplementedException ex)
        {
            Console.WriteLine($"\nError during analysis: {ex.Message}");
            Console.WriteLine("This likely means some algorithms haven't been implemented yet.");
            Console.WriteLine("Please implement the sorting algorithms in the Algorithms/ folder.");
            Console.WriteLine("Please implement the array generation in the Testing/ folder.");
            Console.WriteLine("Please implement the table rendering in the Reporting/ folder.");
            Environment.Exit(1);
        }
    }

    static void RunComplexityAnalysis()
    {
        // Create instances of sorting algorithms
        var algorithms = new List<ISortingAlgorithm>
        {
            new InsertionSort(),
            new QuickSort(),
            new MergeSort()
        };

        // Initialize performance measurer
        var measurer = new PerformanceMeasurer();
        var renderer = new ConsoleTableRenderer();

        // Measure each algorithm
        var allMeasurements = new List<Models.ComplexityMeasurement>();

        foreach (var algorithm in algorithms)
        {
            try
            {
                var measurement = measurer.MeasureAlgorithm(algorithm);
                allMeasurements.Add(measurement);
            }
            catch (NotImplementedException)
            {
                Console.WriteLine($"⚠️ Exception caught during measuring {algorithm.Name} - skipping...");
                Console.WriteLine("1. Check if the algorithm is implemented in the Algorithms/ folder.");
                Console.WriteLine("2. If the algorithm is implemented, check if the array generation is implemented in the Testing/ folder.");
            }
        }

        Console.WriteLine("\n=== Final results ===\n");

        if (allMeasurements.Count == 0)
        {
            Console.WriteLine("❌ No algorithms implemented yet!");
            return;
        }

        try
        {
            renderer.RenderPerformanceTable(allMeasurements);
        }
        catch (NotImplementedException)
        {
            Console.WriteLine("\n📋 Table rendering not implemented yet.");
            Console.WriteLine("Implement methods in ConsoleTableRenderer.cs to see formatted tables.");
        }

    }

}