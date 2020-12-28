using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Crawler.Tool
{
    public class Csv
    {
        public void WriteAppend<T>(T entity, string csvFilePath)
        {
            using var writer = new StreamWriter(csvFilePath, true);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Configuration.HasHeaderRecord = false;
            csv.WriteRecords(new List<T> { entity });
        }
    }
}
