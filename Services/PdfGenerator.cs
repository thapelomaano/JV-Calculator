using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using JVCalculator.Interfaces;
using JVCalculator.Models;
using System.IO;

namespace JVCalculator.Services;
public class PdfGenerator : IPdfGenerator
{
    public byte[] GenerateJVResultPdf(JVCalculatorModel calculatorModel, string result)
    {
        using var ms = new MemoryStream();
        var writer = new PdfWriter(ms);
        var pdf = new PdfDocument(writer);
        var document = new Document(pdf);

        document.Add(new Paragraph("JV Calculator Results").SetFontSize(18));
        document.Add(new Paragraph($"JV Approved: {(result.Contains("BR_8") ? "Yes" : "No")}"));

        document.Add(new Paragraph("Messages:"));
        document.Add(new Paragraph(result));

        document.Add(new Paragraph("Partners:"));
        foreach (var p in calculatorModel.Partners)
        {
            document.Add(new Paragraph($"{p.Company} - Grading: {p.Grading}, Capital Contribution: R{p.CapitalContribution:N0}, Active: {(p.IsActive ? "Yes" : "No")}, Lead Partner: {(p.IsLead ? "Yes" : "No")}"));
        }

        document.Close();
        return ms.ToArray();
    }
}
