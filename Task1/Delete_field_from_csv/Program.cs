using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Delete_field_from_csv
{
    class Program
    {
        static string ShowFields(string path)
        {
            Console.WriteLine("Here you can see the fields.");
            string line;
            using (StreamReader reader = new StreamReader(path))
            {
                line = reader.ReadLine();
                Console.WriteLine(line);

            }
            return line;
        }
        static int ReadingIndex(string path)
        {
            int index;
            string line = ShowFields(path);
            Console.WriteLine("You must to choose one to delete and write field's index.");
            index = Convert.ToInt32(Console.ReadLine()) - 1;
            var cols = line.Split(';');
            if (index < 0 || index > cols.Length)
                throw new ArgumentException("Incorrect index");

            return index;
        }
        static List<string> ChangingLines(ref List<string> lines, int index, string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                var line = reader.ReadLine();
                List<string> values = new List<string>();
                while (line != null)
                {
                    values.Clear();
                    var cols = line.Split(';');
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if (i != index)
                            values.Add(cols[i]);
                    }
                    var newLine = string.Join(";", values);
                    lines.Add(newLine);
                    line = reader.ReadLine();
                }
            }
            return lines;
        }
        static void WriteToFile(List<string> lines, string path)
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }
        static void Main(string[] args)
        {
            try
            {
                string path = @"D:\4 kurs\Bootcamp\Bootcamp\Task1\test.csv";
                int index = ReadingIndex(path);
                List<string> lines = new List<string>();
                ChangingLines(ref lines, index, path);
                WriteToFile(lines, path);
                Console.WriteLine("That's all");
                ShowFields(path);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex);
            }
            catch
            {
                Console.WriteLine("Invalid file or smth else");
            }
            Console.ReadLine();
        }
    }
}
