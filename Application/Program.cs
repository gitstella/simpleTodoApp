using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using TODONotes;

namespace TODO
{
    class Program
    {
        static void Main(string[] args)
        {       
            Console.WriteLine("Welcome to this TODO application!");
            Console.WriteLine("Commands supported: Add 'Your task'/ Do #TaskNumber / Print -> Prints all items!");
            Console.WriteLine("Type 'quit' to exit the application");
            
            string path = "todoList.json";

            bool quitApplication = false;

            while (!quitApplication)
            {
                quitApplication = true;
                
                string userInput = Console.ReadLine();
                string[] wordSplit = userInput.Split(" ", 2);
                
                string inputCommand = wordSplit[0];
                // Close application if input = q
                quitApplication = (string.Compare(inputCommand.ToLower(), "quit") == 0) ? true : false;
                if (quitApplication)
                    break;
                // Removes type sensitive input and compares input
                if (string.Compare(inputCommand.ToLower(), "add") == 0)
                {
                    string inputText = wordSplit[1];
                    TodoNote newTask = new TodoNote();
                    newTask.AddTask(inputText, path);
                }

                else if (string.Compare(inputCommand.ToLower(), "print") == 0)
                { 
                    TodoNote newTask = new TodoNote();
                    newTask.ShowAllTasks();
                }

                else if (string.Compare(inputCommand.ToLower(), "do") == 0)
                {
                    string convertNumber = wordSplit[1];
                    int number;
                    if (int.TryParse(convertNumber, out number))
                    {
                        TodoNote newTask = new TodoNote();
                        newTask.DeleteTask(number, path);
                    }
                    else
                        Console.WriteLine("Something went wrong! Try again: Do <int>");
                }
                else
                    Console.WriteLine("Unknown command: {0}", userInput);
            }
        }       
    }
}
