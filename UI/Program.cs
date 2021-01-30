using ElevenNote.Data;
using ElevenNote.Models;
using ElevenNote.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ApplicationDbContext();

            foreach (var c in context.Notes)
            {
                Console.WriteLine(c.Title);
            }

            Console.ReadKey();

        }
    }
}
