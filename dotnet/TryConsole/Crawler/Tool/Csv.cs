using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Crawler.Tool
{
    public class Csv
    {
        public void WriteAppend<T>(T entity, string csvFilePath)
        {
            using var writer = new StreamWriter(csvFilePath, true);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Configuration.HasHeaderRecord = FileIsEmpty(csvFilePath);
            csv.WriteRecords(new List<T> { entity });
        }

        public IList<T> Read<T>(string csvFilePath)
        {
            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>().ToList();
        }

        private bool FileIsEmpty(string filePath)
        {
            return new FileInfo(filePath).Length == 0;
        }
    }
}
