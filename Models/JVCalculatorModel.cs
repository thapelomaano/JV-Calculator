namespace JVCalculator.Models;

public class JVCalculatorModel
{
	public List<Partner> Partners { get; set; } = new List<Partner>();
	public int RequiredGrading { get; set; } = 7;
}
