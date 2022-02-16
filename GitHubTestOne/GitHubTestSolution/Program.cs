using System;

namespace GitHubTestSolution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program test = new Program();
            test.Start();
        }

        void Start()
        {
            TestClass testClass = new TestClass("Konichiwa worurudo!!!");
            Console.WriteLine(testClass.HerroWorldu);

            Console.ReadKey();
        }
    }
}
