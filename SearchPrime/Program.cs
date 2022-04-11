namespace SearchPrime
{
    public class App
    {
        static void Main(string[] args)
        {
            PrimeFinder p = new PrimeFinder(1000);
            p.findSolution();
        }
    }
}