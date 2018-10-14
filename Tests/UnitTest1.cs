using System;
using Xunit;
using TODOJson;
using TODONotes;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        // Test adding to a empty Json file
        public void TestAddEmptyJsonFile()
        {   
            // Start with a empty test
            List <TodoNote> updateList = new List<TodoNote>();
            HandleJson.WriteToJsonFile<List<TodoNote>>("Test.json", updateList);
            
            TodoNote addObject = new TodoNote();
            string testString = "This is a test string!";
            
            addObject.AddTask(testString, "Test.json");
            
            TodoNote temp = new TodoNote();
            List<TodoNote> todoList = HandleJson.ReadFromJsonFile<List<TodoNote>>("Test.json");
            
            int lastTodo = todoList.Count;
            string containedString = todoList[0].task;
            // The first object should be printed as the first position
            Assert.Equal(testString, containedString);
                
        }

        // Delete an object from the list. 
        [Fact]
        public void DeleteTodo()
        {
            List <TodoNote> updateList = new List<TodoNote>();
            HandleJson.WriteToJsonFile<List<TodoNote>>("Test.json", updateList);
            
            TodoNote addObject = new TodoNote();
            string testString = "This is a delete test!";
            string testString2 = "This is also a delete test!";

            addObject.AddTask(testString, "Test.json");
            addObject.AddTask(testString2, "Test.json");
         
            List<TodoNote> todoList = HandleJson.ReadFromJsonFile<List<TodoNote>>("Test.json");
            int sizeBeforeDelete = todoList.Count;
            
            TodoNote temp = new TodoNote();
            temp.DeleteTask(1, "Test.json");
            
            List<TodoNote> newTodoList = HandleJson.ReadFromJsonFile<List<TodoNote>>("Test.json");
            int sizeAfterDelete = newTodoList.Count;
            // The new size of the list should match the old size -1
            Assert.Equal(sizeBeforeDelete - 1, sizeAfterDelete);
        }
        
        [Fact]
        public void TestAppendToJsonFile()
        {
            List <TodoNote> updateList = new List<TodoNote>();
            HandleJson.WriteToJsonFile<List<TodoNote>>("Test.json", updateList);            
            
            TodoNote addObject = new TodoNote();
            string testString = "This is a new test!";
            string testString2 = "Append to the now test!";
            
            addObject.AddTask(testString, "Test.json");
            addObject.AddTask(testString2, "Test.json");
            
            TodoNote temp = new TodoNote();
            List<TodoNote> todoList = HandleJson.ReadFromJsonFile<List<TodoNote>>("Test.json");
            
            int lastTodo = todoList.Count;
            string expectTest2 = todoList[lastTodo - 1].task;
            // Last TODO object should be the last element
            Assert.Equal(testString2, expectTest2);              
        }      
    }
}
