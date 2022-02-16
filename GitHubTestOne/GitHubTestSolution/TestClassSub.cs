using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubTestSolution
{
    public class TestClassSub : TestClass
    {
        public string MeaningfullMessage;
        
        public TestClassSub(string HerroWorldu, string meaningfullMessage)
            : base(HerroWorldu)
        {
            MeaningfullMessage = meaningfullMessage;
        }
    }
}
