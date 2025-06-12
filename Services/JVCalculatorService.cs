using JVCalculator.Models;

namespace JVCalculator.Services;

public class JVCalculatorService
{
	public string CalculateEligibility(JVCalculatorModel model)
	{
		var partners = model.Partners;

		if (partners.Count < 2)
			return "BR_9: Please add more partner(s) to form a JV";

		if (!partners.All(p => p.IsActive))
			return "BR_2: One or more partner has expired registration status";

		var lead = partners.FirstOrDefault(p => p.IsLead);
		if (lead == null)
			return "No lead partner defined.";

		int leadGrade = GetGradeValue(lead.Grading);
		if (leadGrade < 3 || leadGrade < model.RequiredGrading - 1)
			return "BR_3: The JV does not meet the lead partner minimum grading requirement";

		var totalTurnover = partners.Sum(p =>
			(p.Year1 >= 2012 ? p.TurnoverYear1 : 0) + (p.Year2 >= 2012 ? p.TurnoverYear2 : 0));

		if (totalTurnover < 20000000)
			return "BR_6.1: Total turnover is below Requirement A";

		var leadMaxTurnover = Math.Max(
			lead.Year1 >= 2012 ? lead.TurnoverYear1 : 0,
			lead.Year2 >= 2012 ? lead.TurnoverYear2 : 0);

		if (leadMaxTurnover < 3000000)
			return "BR_6.2: Lead partner turnover is below Requirement B";

		var totalCapital = partners.Sum(p => p.CapitalContribution);
		if (totalCapital < 4000000)
			return "BR_6.3: Capital contribution is below Requirement C";

		return "BR_8: The calculated grading and class of work for this JV is Met.";
	}

	private int GetGradeValue(string grading)
	{
		return int.TryParse(grading.Replace("CE", ""), out int grade) ? grade : 0;
	}
}
