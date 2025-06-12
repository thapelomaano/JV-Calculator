using JVCalculator.Models;
namespace JVCalculator.Interfaces;
public interface IPdfGenerator
{
    byte[] GenerateJVResultPdf(JVCalculatorModel calculatorModel, string result);
}
