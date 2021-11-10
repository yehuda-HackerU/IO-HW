using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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

        public Book()
        {

        }

        public Book(int id, Guid iSBN, string author, string name, int numberOfPages, DateTime datePublished)
        {
            Id = id;
            ISBN = iSBN;
            Author = author;
            Name = name;
            NumberOfPages = numberOfPages;
            DatePublished = datePublished;
        }

        public string ToJSON()
        {
            return JsonSerializer.Serialize(this);
        }

        public string ToCSV()
        {
            return $"{Id},{ISBN},{Author},{Name},{NumberOfPages},{DatePublished}";
        }

        public string ToFixedLength()
        {
            return string.Format("{0,5}{1,36}{2,16}{3,25}{4,4}{5,20}", Id, ISBN, Author, Name, NumberOfPages, DatePublished);
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
                    var jsonObject = book.ToJSON() + ",";
                    if (book == lastItem)
                    {
                        jsonObject = book.ToJSON();
                    }
                    streamWriter.WriteLine("\t" + jsonObject);
                }
                streamWriter.WriteLine("]");
            }
            #endregion

            #region Ex4
            streamWriter = new StreamWriter(@"C:\Users\ybsh1\Desktop\projects\bookList.txt");
            using (streamWriter)
            {
                foreach (var book in bookList)
                {
                    streamWriter.WriteLine(book.ToFixedLength());
                }
            }
            #endregion

            #region Ex5
            streamWriter = new StreamWriter(@"C:\Users\ybsh1\Desktop\projects\bookList.csv");
            using (streamWriter)
            {
                foreach (var book in bookList)
                {
                    streamWriter.WriteLine(book.ToCSV());
                }
            }
            #endregion

            #region Ex6
            List<Book> bookListFromCSV = new List<Book>();
            foreach (var line in File.ReadAllLines(@"C:\Users\ybsh1\Desktop\projects\bookList.csv"))
            {
                string[] bookInfo =  line.Split(',');
                int id = int.Parse(bookInfo[0]);
                Guid isbn = Guid.Parse(bookInfo[1]);
                string author = bookInfo[2];
                string name = bookInfo[3];
                int numberOfPages = int.Parse(bookInfo[4]);
                DateTime datePublished = DateTime.Parse(bookInfo[5]);

                bookListFromCSV.Add(new Book(id, isbn, author, name, numberOfPages, datePublished));
            }
            #endregion

            #region Ex7
            //CSV takes less memory because it does not keep blank characters like Fixed Length.
            #endregion

            #region Ex8
            // I don't like it. (hard to read, lost data, more memory)
            #endregion

            #region Ex9
            //takes even less memory and computers love 0100110011.
            #endregion



            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
