using JVCalculator.Models;

public interface IPdfGenerator
{
    byte[] GenerateJVResultPdf(JVCalculatorModel calculatorModel, string result);
}
