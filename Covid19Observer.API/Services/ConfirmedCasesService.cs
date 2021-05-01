using Covid19Observer.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Covid19Observer.API
{
    public class ConfirmedCasesService : IConfirmedCasesService
    {
        public IEnumerable<Observation> GetConfirmedCases(DateTime observationDate, int maxResultCount)
        {
            using (var context = new COVIDCasesContext())
            {
                return context.covid_observations
                    .Where(x => x.ObservationDate == observationDate && (x.Confirmed > 0 || x.Deaths > 0 || x.Recovered > 0))
                    .OrderByDescending(x => x.Confirmed)
                    .Take(maxResultCount)
                    .ToList();
            }
        }
    }
}
