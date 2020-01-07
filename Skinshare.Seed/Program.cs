using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Skinshare.Core.Entities;
using Skinshare.Data;

namespace Skinshare.Seed
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = ConfigurationLocator.GetDevelopmentConfiguration();
            var connectionString = config.GetConnectionString("Skinshare");
            var optionsBuilder = new DbContextOptionsBuilder<RoutineContext>();

            optionsBuilder.UseNpgsql(connectionString, options => options.UseAdminDatabase("postgres"));
            await using var context = new RoutineContext(optionsBuilder.Options);

            context.Database.EnsureDeleted();
            context.Database.Migrate();

            var routineRepo = new SqlRepository<Routine>(context);
            var routinesToAdd = CreateRoutines();
            foreach (var routine in routinesToAdd)
            {
                await routineRepo.AddAsync(routine);
            }
        }

        private static List<Routine> CreateRoutines()
        {
            return new List<Routine>
            {
                new Routine
                {
                    Title = "Basic Test Routine",
                    Description = "This is the Description of the routine and where it belongs.",
                    Steps = new List<Step>
                    {
                        new Step {Order = 0, Description = "Wash: Cetavil Hydrating facial wash", PartOfDay = PartOfDay.Morning},
                        new Step {Order = 1, Description = "Moisturize: Cerave Basic Moisturizer", PartOfDay = PartOfDay.Morning},
                        new Step {Order = 2, Description = "Toner: Paula's 10% acid Toner", PartOfDay = PartOfDay.Morning},
                        new Step {Order = 3, Description = "Sunscreen: Biore 50 SPF Sunscreen", PartOfDay = PartOfDay.Morning},
                        new Step {Order = 0, Description = "Wash: Cetavil Hydrating facial wash", PartOfDay = PartOfDay.Evening},
                        new Step {Order = 2, Description = "Apply Reitnoil", PartOfDay = PartOfDay.Evening},
                        new Step {Order = 1, Description = "Moisturize: Cerave Basic Moisturizer", PartOfDay = PartOfDay.Evening}
                    },
                },
                new Routine
                {
                    Title = "Bel's Skincare Routine",
                    Description =
                        "Source: https://www.reddit.com/r/SkincareAddiction/comments/bfxgzk/misc_i_made_an_infographic_for_my_routine/",
                    Steps = new List<Step>
                    {
                        new Step {Order = 0, Description = "Cleanser - CeraVe Hydrating Facial Cleanser", PartOfDay = PartOfDay.Morning},
                        new Step {Order = 1, Description = "Active - Melano CC Vitamin C Serum", PartOfDay = PartOfDay.Morning},
                        new Step {Order = 2, Description = "Essence - CosRX 96 Snail Mucin Power Essence", PartOfDay = PartOfDay.Morning},
                        new Step {Order = 3, Description = "Moisturizer - Cerave Moisturising Cream", PartOfDay = PartOfDay.Morning},
                        new Step {Order = 4, Description = "Sunscreen - Biore UV Watery Essence SPF50", PartOfDay = PartOfDay.Morning},
                        new Step {Order = 0, Description = "Cleanser - CeraVe Hydrating Facial Cleanser", PartOfDay = PartOfDay.Evening},
                        new Step {Order = 1, Description = "Active - AHA", PartOfDay = PartOfDay.Evening},
                        new Step {Order = 2, Description = "Essence - CosRX 96 Snail Mucin Power Essence", PartOfDay = PartOfDay.Evening},
                        new Step {Order = 3, Description = "Moisturize: Cerave Basic Moisturizer", PartOfDay = PartOfDay.Evening},
                        new Step {Order = 4, Description = "Optional - Feeling Cute? NEed some extra moisture? Grab a sheet mask", PartOfDay = PartOfDay.Evening},
                    }
                },
            };
        }
    }
}