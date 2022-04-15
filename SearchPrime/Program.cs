using System;

namespace SearchPrime
{
    public class App
    {
        static void Main(string[] args)
        {
            
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            
            
            PrimeFinder p = new PrimeFinder();
            p.FindSolution(1000);
            
            watch.Stop();
            
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds } ms");
        }
    }
}