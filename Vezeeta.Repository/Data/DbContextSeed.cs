using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;

namespace Vezeeta.Repository.Data
{
    public static class DbContextSeed
    {
        // For Add Specializations
        public static async Task SeedAsync(AppDbContext dbContext)
        {
            if (!dbContext.Specializations.Any())
            {
                var specializationData = File.ReadAllText("../Vezeeta.Repository/Data/DataSeed/specialization.json");
                var specializations = JsonSerializer.Deserialize<List<Specialization>>(specializationData);


                if (specializations is not null && specializations.Count > 0)
                {
                    foreach (var specialization in specializations)
                        await dbContext.Set<Specialization>().AddAsync(specialization);


                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }


}
