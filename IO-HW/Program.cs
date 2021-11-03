using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_HW
{
    class Book
    {


    }
    class Program
    {
        static void Main(string[] args)
        {
            #region Ex1
            string[] filesArray = Directory.GetDirectories(@"C:\");
            #endregion

            #region Ex2
            Console.WriteLine("Enter directory path: ");
            string path = Console.ReadLine();
            //filesArray = Directory.GetFiles(directory);
            DirectoryInfo directory = new DirectoryInfo(path);
            var files = directory.GetFiles();
            foreach (var file in directory.GetFiles().OrderByDescending(x => x.Length).Take(3))
            {
                Console.WriteLine($"name: {file.Name}, size: {file.Length} Bytes, ceated on: {file.CreationTime}.");
            }
            #endregion

            #region Ex3

            #endregion



            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
