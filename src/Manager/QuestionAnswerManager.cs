using System.Text.RegularExpressions;

namespace HCS_Tatar.Manager;


/**
 *
 * Question Answer Manager
 * This is inside a class because we need it for the testability of the program.
 *
 */

/*
    Explanation of Regex pattern for the question.
    
    (.*)?[?] (.*["].{0,}[^\s"]["])+?$
    
    This regex matches two groups
    
    First group: (.*)?[?]
    Matches the question. So every text content that has a question mark at the end
    
    Second group: (.*["].{0,}[^\s"]["])+?$
    Matches every answers that are valid strings and are surrounded by string literals. 
    
    There is also a 0 Group. That one matches to the entire text.
*/
public class QuestionAnswerManager
{
    const string QuestionPattern = @"(.*)?[?] (.*[""].{0,}[^\s""][""])+?$";
    const int MaxStrLen = 255;

    private readonly Dictionary<string, HashSet<string>?> _questionsAnswers = new();

    public bool GetQuestionPrompt(string? input = null)
    {
        // Write a prompt to the console
        Console.WriteLine("Introduce a question in the following format: <question>? “<answer1>” “<answer2>” “<answerX>”");

        string? inputAddQuestions = input ?? Console.ReadLine();

        Match m = Regex.Match(inputAddQuestions ?? "", QuestionPattern);

        // if there are more then two groups
        // i.e. if there is a match for a question and a match for multiple answers surrounded by string literals
        if (m.Groups.Count >= 3)
        {
            // get answers and questions. 
            // group 0 is the entire text; we need the other ones. 
            string questionToAdd = m.Groups[1].Value + "?";

            // check restriction for the question
            if (questionToAdd.Length >= MaxStrLen)
            {
                Console.WriteLine("Question is too long! Question cannot be longer than 255 characters");
                return false; // break = restart flow
            }

            // get the answers and preprocess before converting from string array to List
            // for this we could use yet another regex to solve it.
            // the reason we use another regex here is because we cannot simply split by whitespace, the values might contain whitespaces
            HashSet<string>? answers = Regex.Matches(m.Groups[2].Value, "\"([^\"]+)\"\\s*")  // regex pattern to match string enclosed values
                .Select(match => match.Groups[1].Value)                     // map the matches collection to an array of string
                .Select(x => x.Replace("\"", ""))      // like map/flatmap 
                .ToHashSet();

            // check restriction for the answer
            bool answerReachMaxLen = answers.Any(a => a.Length > MaxStrLen);
            if (answerReachMaxLen)
            {
                Console.WriteLine("Answers is too long! Answers cannot be longer than 255 characters");
                return false; // break = restart flow
            }

            // add to the dictionary
            _questionsAnswers[questionToAdd] = answers;
            return true;
        }

        // if the format is not correct, return false.
        return false;
    }

    public (bool isSuccess, HashSet<string> answers) GetAnswersPrompt(string? input = null)
    {
        // Write a question prompt to console
        Console.Write("Ask a question: ");

        // wait for the question input from the console
        string? question = input ?? Console.ReadLine();
                                
        if (question != null ) {
            bool isKeyExists = _questionsAnswers.TryGetValue(question, out HashSet<string>? answers);
                                    
            if(isKeyExists)
                foreach (string answer in answers ?? new HashSet<string> ())
                    Console.WriteLine($"- {answer}");
            else Console.WriteLine("- The answer to life, universe and everything is 42");
            return (true,answers ?? new HashSet<string> ());
        }

        return (false, []);
    }

}