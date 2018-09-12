using Example.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Example.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PeopleDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PeopleDbContext context)
        {
            if (!context.Schemata.Any())
            {
                context.Children.Add(new Child
                {
                    Name =
                        $"Ch{DateTime.Now.DayOfYear}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}ld 1"
                });
                context.Children.Add(new Child
                {
                    Name =
                        $"Ch{DateTime.Now.DayOfYear}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}ld 2"
                });
                context.Children.Add(new Child
                {
                    Name =
                        $"Ch{DateTime.Now.DayOfYear}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}ld 3"
                });
                context.Parents.Add(new Parent
                {
                    Name =
                        $"P{DateTime.Now.DayOfYear}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}rent",
                    KeyDate = DateTime.Today
                });

                context.Schemata.AddRange(Enumerable.Range(2, 28)
                    .Select(x => new Schema
                    {
                        Visit = new DateTime(DateTime.Today.Year, 1 + x / 3, x),
                        ParentId = 1,
                        ChildSchemas = new List<ChildSchema> { new ChildSchema { ChildId = x % 2 + 1 } }
                    }).ToList());
            }
        }
    }
}