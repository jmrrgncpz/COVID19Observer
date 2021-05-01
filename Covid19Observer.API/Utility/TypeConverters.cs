using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Observer.API
{
    public class TypeConverters
    {
        public class IntegerConverter : DefaultTypeConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (int.TryParse(text, out int result))
                {
                    return result;
                }

                if (double.TryParse(text, out double doubleResult))
                {
                    if (doubleResult % 1 == 0)
                    {
                        return (int)doubleResult;
                    }
                }

                return base.ConvertFromString(text, row, memberMapData);
            }
        }
    }
}
