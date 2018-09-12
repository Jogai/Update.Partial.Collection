using Example.Models;
using RefactorThis.GraphDiff;
using System;
using System.Data.Entity;
using System.Linq;

namespace Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PeopleDbContext myContext = new PeopleDbContext();
            myContext.Database.Delete();
            Console.WriteLine("Recreating the database...");

            Parent parent = Read();

            Console.WriteLine("Do you want to update [Enter = N] ? ");
            string line = Console.ReadLine();
            if (line.Length > 0 && line != "N")
            {
                foreach (Schema s in parent.Schemas)
                {
                    s.Visit = s.Visit.AddDays(3);
                    s.ChildSchemas.First().ChildId = 3;
                }

                Console.WriteLine();
                Console.WriteLine("_________|_________|_______ Modified:");

                foreach (Schema schema in parent.Schemas)
                {
                    foreach (ChildSchema cs in schema.ChildSchemas)
                    {
                        Console.WriteLine(
                            $"Parent {schema.Parent.Id} | Child {cs.ChildId} | Date {cs.Schema.Visit:d} ({schema.Id} / {cs.Id})");
                    }
                }
                Console.WriteLine();

                myContext.UpdateGraph(parent,
                    map => map
                        .OwnedCollection(x => x.Schemas, y => y.OwnedCollection(z => z.ChildSchemas))
                );

                int count = myContext.Schemata.Count();

                Console.WriteLine(myContext.SaveChanges());

                Console.WriteLine();
                if (myContext.Schemata.Count() < count)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{count - myContext.Schemata.Count()} schemas were lost in the process!");
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"Number of schemas: {myContext.Schemata.Count()}");
                Console.WriteLine();
            }
            Read();
            Console.WriteLine("Press any key to continue . . .");
            Console.ReadKey();
        }

        private static Parent Read()
        {
            Parent parent;
            using (PeopleDbContext myContext = new PeopleDbContext())
            {
                parent = myContext.Schemata
                    .Include(s => s.Parent)
                    .Include(s => s.ChildSchemas.Select(cs => cs.Schema))
                    .Include(s => s.ChildSchemas.Select(cs => cs.Child))
                    .Where(s => s.ParentId == 1 && s.Visit.Month == 3)
                    .ToList().First().Parent;

                Console.WriteLine();
                Console.WriteLine($"Number of schemas: {myContext.Schemata.Count()}");
                Console.WriteLine();
            }
            //Console.WriteLine(JsonConvert.SerializeObject(parent, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, Formatting = Formatting.Indented }));

            Console.WriteLine("_________|_________|_____ DB Values:");
            foreach (Schema schema in parent.Schemas)
            {
                foreach (ChildSchema cs in schema.ChildSchemas)
                {
                    Console.WriteLine(
                        $"Parent {schema.Parent.Id} | Child {cs.ChildId} | Date {cs.Schema.Visit:d} ({schema.Id} / {cs.Id})");
                }
            }

            return parent;
        }
    }
}