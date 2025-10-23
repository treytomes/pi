namespace PiCalculator;

public static class Program
{
	/// <summary>
	/// Calculate the value of π using François Viète's formula.
	/// </summary>
	public static void Main()
	{
		Console.WriteLine($"Math.PI    = {Math.PI}");

		var product = 1.0;
		var numerator = 0.0;

		for (var counter = 0; counter < 30; counter++)
		{
			numerator = Math.Sqrt(2.0 + numerator);
			product *= numerator / 2.0;
		}

		var pi = 2 / product;
		Console.WriteLine($"Viète's PI = {pi}");
	}
}