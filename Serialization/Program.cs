using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Serialization
{
    internal class Program
    {
        static void Main()
        {
            Event myEvent = new Event(1, "Calgary");

            //Serialize the object to a file
            SerializeObjectToFile(myEvent, "event.txt");

            //Deserialize the object from the file and display values
            Event deserializedEvent = DeserializeObjectFromFile<Event>("event.txt");
            Console.WriteLine(deserializedEvent.EventNumber);
            Console.WriteLine(deserializedEvent.Location);

            //Create a method to read from a file and display characters
            ReadFromFile("hackathon.txt");
        }

        // Serialization method
        static void SerializeObjectToFile<T>(T obj, string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, obj);
            }
        }

        // Deserialization method
        static T DeserializeObjectFromFile<T>(string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(fileStream);
            }
        }

        // Method to read from a file and display characters
        static void ReadFromFile(string fileName)
        {
            // Write the word "Hackathon" to the file
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write("Hackathon");
            }

            // Read the first, middle, and last characters
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    Console.WriteLine("\nIn Word: " + reader.ReadToEnd());

                    // Reset the position to the beginning of the file
                    fileStream.Seek(0, SeekOrigin.Begin);

                    // Read the first character
                    Console.WriteLine("The First Character is: \"" + (char)reader.Read() + "\"");

                    // Move to the middle character
                    fileStream.Seek(fileStream.Length / 2, SeekOrigin.Begin);

                    // Read the middle character
                    Console.WriteLine("The Middle Character is: \"" + (char)reader.Read() + "\"");

                    // Move to the last character
                    fileStream.Seek(-1, SeekOrigin.End);

                    // Read the last character
                    Console.WriteLine("The Last Character is: \"" + (char)reader.Read() + "\"");
                }
            }
        }
    }
}


[Serializable]
public class Event
{
    public int EventNumber { get; set; }
    public string Location { get; set; }

    public Event(int eventNumber, string location)
    {
        EventNumber = eventNumber;
        Location = location;
    }
}
