using System;

namespace DesignPatterns.solid
{
    public class SingleResponsible
    {
        private readonly List<String> entries = new List<string>();
        private static int count = 0;

        public int AddEntry(string entry)
        {
            entries.Add($"{++count}:{entry}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return String.Join(Environment.NewLine, entries);
        }

        // Till here all methods were used - follows the SRP but below breaks the SRP.
        // Save and other functionalities are not related to the Journal, it breaks SRP.
        public void Save(string filename)
        {
            File.WriteAllText(filename, ToString());
        }

    }


    // To overcome the SRP issue - To create a class that to do other responsibility

    public class SaveFiles
    {
        public void SaveFile(SingleResponsible j, string filename, bool overide)
        {
            File.WriteAllText(filename, j.ToString());
        }
    }

    public class Solid
    {
        public static void ExecutionSRP()
        {
            var srp = new SingleResponsible();
            int val1 = srp.AddEntry("Hi All");
            int val2 = srp.AddEntry("My Journey");

            Console.WriteLine(srp);


            //Creating object for other responsibility

            var saveFile = new SaveFiles();
            var filename = "journal.txt";

            saveFile.SaveFile(srp, filename, true);
            
        }

    }
}