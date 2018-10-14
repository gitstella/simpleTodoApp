using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace TODOJson
{
    public class HandleJson
    {
        public static void WriteToJsonFile<TodoNote>(string filePath, TodoNote todoObject)
        {
            TextWriter writer = null;
            try
            {
                var write = JsonConvert.SerializeObject(todoObject);
                writer = new StreamWriter(filePath, false);
                writer.Write(write);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        // Read from the file and convert back to TodoNote objects. 
        public static TodoNote ReadFromJsonFile<TodoNote>(string filePath)
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<TodoNote>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}