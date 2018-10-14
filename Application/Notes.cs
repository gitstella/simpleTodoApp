using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using TODOJson;
using TODONotes;

namespace TODONotes
{
    public class TodoNote
    {
        public int ID { get; set; }
        public string task { get; set; }
        
        // Add an object of todoNote and append it to the list of already made tasks.
        // Already made tasks are added to a list using the read Json function before appending a new task.
        public void AddTask(string whatToDo, string path)
        {                
            List<TodoNote> todoList = HandleJson.ReadFromJsonFile<List<TodoNote>>(path);
            List <TodoNote> updateList = new List<TodoNote>();

            TodoNote todo = new TodoNote
            {
                ID = 0,
                task = whatToDo,
            };

            if(todoList == null)
            {
                todo.ID = 1;
                updateList.Add(todo);
            }
            else
            {
                todo.ID = todoList.Count + 1;
                updateList = todoList;
                updateList.Add(todo);
            }  

            Console.WriteLine("#{0}  {1}", todo.ID, todo.task);
            HandleJson.WriteToJsonFile<List<TodoNote>>(path, updateList);
        
        }
        // Print all task in the console by iterating though the object list.
        public void ShowAllTasks()
        {
            List<TodoNote> taskList = HandleJson.ReadFromJsonFile<List<TodoNote>>("todoList.json");

            if (taskList == null || taskList.Count == 0)
            {
                Console.WriteLine("You got no tasks! Type Add + \"MyTask\"");
                return;
            }

            Console.WriteLine("\nTODO list:"); 
            
            foreach (TodoNote p in taskList)
            {
                Console.WriteLine("#{0}\t{1}",p.ID, p.task);
            }
        }
        // Delete specified object if its present in the list. 
        // New IDs are given when a task is completed.
        public void DeleteTask(int taskNumber, string path)
        {
            List<TodoNote> taskList = HandleJson.ReadFromJsonFile<List<TodoNote>>(path);

            if (taskList == null || taskList.Count == 0)
            {
                Console.WriteLine("TODO list is empty!");
                return;
            }

            TodoNote temp = new TodoNote();

            foreach (TodoNote p in taskList)
            {
                if (taskNumber == p.ID)
                    temp = p;
                
                if (p.ID > taskNumber)
                    p.ID--;
            }

            if (temp.ID == 0)
                Console.WriteLine("Couldnt find task looking for!");

            else
            {
                taskList.Remove(temp);
                Console.WriteLine("Completed #{0} {1}", temp.ID, temp.task);
            }

            if(taskList.Count == 0)
                Console.WriteLine("\nGood job, you completed all tasks!\nType <Add \"MyTask\"> for creating new tasks");

            HandleJson.WriteToJsonFile<List<TodoNote>>(path, taskList);

        }
    }
}