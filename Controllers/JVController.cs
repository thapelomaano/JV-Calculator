using Microsoft.AspNetCore.Mvc;
using JVCalculator.Models;
using JVCalculator.Services;
using JVCalculator.Utils;
using System.Reflection;

namespace JVCalculator.Controllers;

public class JVController : Controller
{
	private readonly JVCalculatorService _calculator;

	public JVController(JVCalculatorService calculator)
	{
		_calculator = calculator;
	}

	public IActionResult Index()
	{
		return View(new JVCalculatorModel());
	}

	[HttpPost]
	public IActionResult Calculate(JVCalculatorModel model)
	{
		var result = _calculator.CalculateEligibility(model);
		return View("Result", result);
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
        var partners = CsvImporter.ImportPartnersFromCsv(stream);
        string result = _calculator.CalculateEligibility(partners);

        return View("Result", result);
    }
}
