using Covid19Observer.API;
using System;
using System.Collections.Generic;

namespace Covid19Observer.API.Models
{
    public class ConfirmedCasesResponse
    {
        public ConfirmedCasesResponse(DateTime observationDate, List<CountryObservation> observations = null)
        {
            observation_date = observationDate;
            this.observations = observations == null ? new List<CountryObservation>() : observations ;
        }

        public DateTime observation_date { get; set; }
        public List<CountryObservation> observations { get; set; }
    }

    public class CountryObservation
    {
        public string country { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public int recovered { get; set; }
    }
}
