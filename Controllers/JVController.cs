using Microsoft.AspNetCore.Mvc;
using JVCalculator.Models;
using JVCalculator.Services;
using JVCalculator.Utils;

namespace JVCalculator.Controllers;

public class JVController : Controller
{
	private readonly JVCalculatorService _calculator;
    private readonly IPdfGenerator _pdfGenerator;


    public JVController(JVCalculatorService calculator, IPdfGenerator pdfGenerator)
	{
		_calculator = calculator;
        _pdfGenerator = pdfGenerator;

    }

	public IActionResult Index()
	{
		return View(new JVCalculatorModel());
	}

	[HttpPost]
	public IActionResult Calculate(JVCalculatorModel model)
	{
		var result = _calculator.CalculateEligibility(model);
        var pdfBytes = _pdfGenerator.GenerateJVResultPdf(model, result);
        return File(pdfBytes, "application/pdf", "JV_Calculation_Result.pdf");
        
	}
    [HttpPost]
    public IActionResult UploadCsv(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            var msg = "Please upload a valid CSV file.";

            return View("Result", msg);
        }

        using var stream = file.OpenReadStream();
        var objData = CsvImporter.ImportPartnersFromCsv(stream);
        string result = _calculator.CalculateEligibility(objData);

        var pdfBytes = _pdfGenerator.GenerateJVResultPdf(objData, result);
        return File(pdfBytes, "application/pdf", "JV_Calculation_Result.pdf");
    }
}
