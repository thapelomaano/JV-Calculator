namespace JVCalculator.Models;

public class Partner
{
	public string Company { get; set; }
	public decimal TurnoverYear1 { get; set; }
	public int Year1 { get; set; }
	public decimal TurnoverYear2 { get; set; }
	public int Year2 { get; set; }
	public string Grading { get; set; }
	public decimal CapitalContribution { get; set; }
	public bool IsLead { get; set; }
	public bool IsActive { get; set; }
}
