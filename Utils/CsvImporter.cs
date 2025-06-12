using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;
using JVCalculator.Models;

namespace JVCalculator.Utils;

public class CsvImporter
{
    public static JVCalculatorModel ImportPartnersFromCsv(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<Partner>().ToList();
        var tempObj = new JVCalculatorModel();
        tempObj.Partners = records;
        return tempObj;
    }
}
