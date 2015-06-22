using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoWipe.types;
using Newtonsoft.Json;

namespace GoWipe.Tests
{
    public class Utility
    {
        public class Book
        {
            public string Title { get; set; }
            public string Publisher { get; set; }
            public string Author { get; set; }
            public int Price { get; set; }
            public string Category { get; set; }
            public string ISBN { get; set; }
        }
        public static string GenerateAValidBookWithoutISBN()
        {
            var book = new Book()
            {
                Author = "Dipanjan Nag",
                Category = "Management",
                Price = 540,
                Publisher = "Me",
                Title = "Getting Stuck in Irrelevent Things"
            };
            var ret = JsonConvert.SerializeObject(book);
            return ret;
        }
        public static string GenerateAValidBookWithISBN()
        {
            var book = new Book()
            {
                Author = "Dipanjan Nag",
                Category = "Management",
                Price = 540,
                Publisher = "Me",
                Title = "Getting Stuck in Irrelevent Things",
                ISBN = "sample-isbn"
            };
            var ret = JsonConvert.SerializeObject(book);
            return ret;
        }
        public static string GenerateAValidBookOutRange()
        {
            var book = new Book()
            {
                Author = "Dipanjan Nag",
                Category = "Management",
                Price = 5400000,
                Publisher = "Me",
                Title = "Getting Stuck in Irrelevent Things",
                ISBN = "sample-isbn"
            };
            var ret = JsonConvert.SerializeObject(book);
            return ret;
        }
    }
}
