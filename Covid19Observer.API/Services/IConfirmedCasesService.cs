using System;
using System.Collections.Generic;

namespace Covid19Observer.API
{
    interface IConfirmedCasesService
    {
        public IEnumerable<Observation> GetConfirmedCases(DateTime observationDate, int maxResultCount);
    }
}
