using CsvHelper.Configuration;
using System;

namespace Covid19Observer.API.Mappers
{
    public class ObservationModelAdapter
    {
        public int SNo { get; set; }
        public DateTime ObservationDate { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
    }

    public class ObservationMap : ClassMap<ObservationModelAdapter>
    {
        public  ObservationMap()
        {
            Map(x => x.SNo).Name("SNo").TypeConverter(new TypeConverters.IntegerConverter());
            Map(x => x.ObservationDate).Name("ObservationDate");
            Map(x => x.Province).Name("Province/State");
            Map(x => x.Country).Name("Country/Region");
            Map(x => x.Confirmed).TypeConverter(new TypeConverters.IntegerConverter()); ;
            Map(x => x.Deaths).Name("Deaths").TypeConverter(new TypeConverters.IntegerConverter()); ;
            Map(x => x.Recovered).Name("Recovered").TypeConverter(new TypeConverters.IntegerConverter()); ;
        }
    }
}
