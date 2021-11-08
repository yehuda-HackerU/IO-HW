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
        public int Id { get; set; }
        public Guid ISBN { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime DatePublished { get; set; }

        public string ToJSON() 
        {
            string id = $"\"ID\": \"{Id}\"";
            string isbn = $"\"ISBN\": \"{ISBN}\"";
            string author = $"\"Author\": \"{Author}\"";
            string name = $"\"Name\": \"{Name}\"";
            string numberOfPages = $"\"NumberOfPages\": {NumberOfPages}";
            string datePublished = $"\"DatePublished\": \"{DatePublished}\"";
            return "{" + $"{id}, {isbn}, {author}, {name}, {numberOfPages}, {datePublished}" + "}";
        }

        public string ToCSV()
        {
            return $"{Id},{ISBN},{Author},{Name},{NumberOfPages},{DatePublished}";
        }
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
            DirectoryInfo directory = new DirectoryInfo(path);
            var files = directory.GetFiles();
            foreach (var file in directory.GetFiles().OrderByDescending(x => x.Length).Take(3))
            {
                Console.WriteLine($"name: {file.Name}, size: {file.Length} Bytes, ceated on: {file.CreationTime}.");
            }
            #endregion

            #region Ex3
            List<Book> bookList = new List<Book>()
            {
                new Book { Id = 987, ISBN = Guid.NewGuid(), Author = "Mozes", Name = "Efshar Gam Bly Qavier", NumberOfPages = 321, DatePublished = new DateTime(1947, 02, 01) },
                new Book { Id = 964, ISBN = Guid.NewGuid(), Author = "Ariel Naim", Name = "My First Book", NumberOfPages = 543, DatePublished = new DateTime(2007, 05, 24) },
                new Book { Id = 864, ISBN = Guid.NewGuid(), Author = "Haim Mosh", Name = "Ein li Shem Amiti", NumberOfPages = 111,  DatePublished = new DateTime(1985, 11, 11) },
                new Book { Id = 653, ISBN = Guid.NewGuid(), Author = "Gady Halper", Name = "My New Car", NumberOfPages = 444, DatePublished = new DateTime(2021, 10, 21) }
            };

            StreamWriter streamWriter = new StreamWriter(@"C:\Users\ybsh1\Desktop\projects\bookList.json");
            using (streamWriter)
            {
                streamWriter.WriteLine("[");
                Book lastItem = bookList.Last();
                foreach (var book in bookList)
                {
                    var jsonObject =book.ToJSON() + ",";
                    if (book == lastItem)
                    {
                        jsonObject = book.ToJSON();
                    }
                    streamWriter.WriteLine("\t" + jsonObject);
                }
                streamWriter.WriteLine("]");
            }
            #endregion



            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
