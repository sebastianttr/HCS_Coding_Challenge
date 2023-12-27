using System.ComponentModel;
using HCS_Tatar.Manager;
using NUnit.Framework;

namespace HCS_Tatar
{
    [TestFixture]
    public class UnitTest
    {
        private readonly QuestionAnswerManager _questionAnswerManager;

        public UnitTest()
        {
            _questionAnswerManager = new QuestionAnswerManager();
        }
        
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            // Disable console output from test methods
            Console.SetOut(TextWriter.Null);
        }


        [Test, Order(1)]
        [TestCase(@"What is Jane's favorite colors? ""Red"" ""Purple""")]
        [TestCase(@"What is Peters favorite food? ""Pizza"" ""Spaghetti"" ""Ice cream""")]
        [TestCase(@"What are three german car brands? ""Volkswagen"" ""Mercedes"" ""Audi""")]
        public void AddQuestionsTest(string input)
        {
            bool isPrompt = _questionAnswerManager.GetQuestionPrompt(input);
            
            if(isPrompt)
                Assert.Pass();
            else 
                Assert.Fail();
        }
        
        [Test, Order(2)]
        [TestCase("What is Jane's favorite colors?", new [] {"Red", "Purple" },true)]
        [TestCase("What is Peters favorite food?", new [] {"Pizza", "Spaghetti", "Ice cream" },true)]
        [TestCase("What are three german car brands?", new [] {"Volkswagen", "Mercedes", "Nissan" },false)]
        public void RetrieveAnswerTest(string input, string[] answers, bool isPass)
        {
            (bool isPrompt,HashSet<string> ans) = _questionAnswerManager.GetAnswersPrompt(input);

            foreach(string a in ans)
                Console.Write($" - {a}");
            
            HashSet<string> answerSet = answers.ToHashSet();
            
            if(isPrompt && ans.SetEquals(answerSet) == isPass)
                Assert.Pass();
            else 
                Assert.Fail();
        }
    }
}

