using Covid19Observer.API.Mappers;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Covid19Observer.API
{
    public static class DatabaseInitializer
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {

            // Read CSV Files
            using var reader = new StreamReader(@"covid_19_data.csv", Encoding.Default);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ObservationMap>();
            var records = csv.GetRecords<ObservationModelAdapter>();

            // Group entries by country and date
            var groupedRecords = records.GroupBy(x => new { x.Country, x.ObservationDate });
            int id = 1;
            var flatRecords = groupedRecords.Aggregate(new List<Observation>(),
            (prev, next) =>
            {
                int confirmed = 0;
                int deaths = 0;
                int recovered = 0;
                foreach (ObservationModelAdapter observationModelAdapter in next)
                {
                    confirmed += observationModelAdapter.Confirmed;
                    deaths += observationModelAdapter.Deaths;
                    recovered += observationModelAdapter.Recovered;
                }

                prev.Add(new Observation
                {
                    Id = id++,
                    Country = next.Key.Country,
                    ObservationDate = next.Key.ObservationDate,
                    Confirmed = confirmed,
                    Deaths = deaths,
                    Recovered = recovered
                });

                return prev;
            });

            modelBuilder.Entity<Observation>().HasData(flatRecords);
        }
    }
}
