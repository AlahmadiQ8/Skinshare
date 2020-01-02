using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Skinshare.Core.Entities;
using Skinshare.Core.Misc;
using Skinshare.Data;
using Xunit;

namespace Skinshare.Tests
{
    public class SqlRepository
    {
        [Fact]
        public async void TestAllMethods()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<RoutineContext>().UseSqlite(connection).Options;

                await using var context = new RoutineContext(options);
                context.Database.EnsureCreated();

                var routineRepository = new SqlRepository<Routine>(context);
                await routineRepository.AddAsync(CreateRoutine());
                var routines = await routineRepository.ListAllAsync();
                Assert.Equal(1, routines.Count);

                var id = routines[0].Id;
                var r = await routineRepository.GetByIdAsync(id);
                Assert.Equal("Title Test", r.Title);

                r.Title = "Changed";
                await routineRepository.UpdateAsync(r);
                r = await routineRepository.GetByIdAsync(id);
                Assert.Equal("Changed", r.Title);

                await routineRepository.AddAsync(CreateRoutine());
                routines = await routineRepository.ListAllAsync();
                Assert.Equal(2, routines.Count);
                Assert.Contains(routines, rr => rr.Title == "Title Test");

                await routineRepository.DeleteAsync(r);
                routines = await routineRepository.ListAllAsync();
                Assert.Equal(1, routines.Count);
            }
            finally
            {
                connection.Close();
            }
        }

        private static Routine CreateRoutine()
        {
            return new Routine
            {
                Title = "Title Test",
                Description = "Description Test",
                Identifier = "Unique",
                Steps = new List<Step>
                {
                    new Step {Description = "Step 0", Order = 0, PartOfDay = PartOfDay.Morning},
                    new Step {Description = "Step 1", Order = 1, PartOfDay = PartOfDay.Morning},
                    new Step {Description = "Step 2", Order = 2, PartOfDay = PartOfDay.Morning},
                    new Step {Description = "Step 0", Order = 0, PartOfDay = PartOfDay.Evening},
                    new Step {Description = "Step 1", Order = 1, PartOfDay = PartOfDay.Evening},
                    new Step {Description = "Step 2", Order = 2, PartOfDay = PartOfDay.Evening}
                }
            };
        }
    }
}