using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using HCS_Tatar.Manager;
using NUnitLite;

namespace HCS_Tatar
{
    class Program
    {
        // define a constant. amount of invalid inputs during selection
        const int MaxTryTillEnd = 5;
        
        static int Main(string[] args) {
            // before the program starts, check if it is a test run or a normal program run
            // check if it has -test option
            QuestionAnswerManager questionAnswerManager = new QuestionAnswerManager();
            
            if (args.Length > 0 && args.Contains("-test")) {
                // run NUnit test here
                return new AutoRun().Execute([]);
            }
            else {
                // run the normal program
                Console.WriteLine(@"
                #####################################################
                #                                                   #
                #         Answer/Question Coding Challenge          #
                #                                                   #
                #     Please make a selection by pressing a key:    #
                #     [Q] Add Question/Answer   [A] Ask Question    #
                #                                                   #
                #####################################################
                ");
                
                // declare a bool value. Will be use to control the lifecycle of app
                // if inout is invalid for X time, end program.
                int tryCounter = 0;
                
                // infinite loop. The state controls if the while loop continues or not.
                while (true)
                {
                    Console.Write("Your selection [Q]/[A]: ");
                    
                    // read the input from the console
                    string? input = Console.ReadLine();
                    
                    // if the input is valid as in either F or J
                    if (input != null && (input.Equals("Q") || input.Equals("A"))) {
                        Console.Clear();
                        switch (input) {
                            case "Q":
                                bool promptSuccessQuestions = questionAnswerManager.GetQuestionPrompt();
                                if (!promptSuccessQuestions) {
                                    Console.WriteLine("Questions/Answer is not in a valid format.");
                                    break;
                                }

                                // if prompt was successful, continue.
                                Console.WriteLine("Saving answer!");
                                
                                // Sleep for 2 sec
                                Thread.Sleep(2000);
                                
                                // clear the console
                                Console.Clear();
                                
                                break; 
                            case "A":
                                bool promptSuccessAnswers = questionAnswerManager.GetAnswersPrompt().isSuccess;
                                
                                if (!promptSuccessAnswers) {
                                    Console.WriteLine("Invalid input!");
                                    break;
                                }
                                
                                break;
                            default:
                                Console.WriteLine("Selection not valid!");
                                break;
                        }
                    }
                    else    // Invalid input
                    {
                        // write invalid output to console and increment the try counter
                        Console.WriteLine("Invalid input!");
                        tryCounter++;

                        if (tryCounter >= MaxTryTillEnd) {
                            Console.WriteLine("To many invalid inputs. Ending the program.");
                            
                            // break array
                            break;
                        }

                    }
                }
            }

            return 0;
        }
    }
}