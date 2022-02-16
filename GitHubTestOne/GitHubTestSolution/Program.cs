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
            TestClassSub testClass = new TestClassSub("Konichiwa worurudo!!!", "Here kitty, kitty, kitty. Meaow. Here Jonesy.");
            Console.WriteLine("{0}, \n{1}",testClass.HerroWorldu, testClass.MeaningfullMessage);

            Console.ReadKey();
        }
    }
}
