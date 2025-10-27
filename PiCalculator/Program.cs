namespace PiCalculator;

public static class Program
{
	// TODO: What is the correct epsilon value for a decimal?
	const decimal DECIMAL_EPSILON = 0.00000000000000000000000000001M;

	/// <summary>
	/// Calculate the value of π using François Viète's formula.
	/// </summary>
	public static void Main()
	{
		Console.WriteLine($"Math.PI    = {Math.PI}");

		var pi = VietePi(HeronSqrt);
		Console.WriteLine($"Viète's PI = {pi}");
	}

	private static decimal VietePi(Func<decimal, decimal> sqrt)
	{
		var p0 = 1.0M;
		var numerator = 0.0M;

		while (true)
		{
			numerator = sqrt(2.0M + numerator);

			var p1 = p0 * numerator / 2.0M;
			if (p1 == p0)
			{
				break;
			}
			// if (Math.Abs(p1 - p0) <= decimalEpsilon)
			// {
			// 	p0 = p1;
			// 	break;
			// }
			p0 = p1;
		}

		var pi = 2 / p0;
		return pi;
	}

	private static decimal HeronSqrt(decimal value)
	{
		var x0 = value / 2.0m;
		while (true)
		{
			var x1 = (x0 + value / x0) / 2.0M;
			if (Math.Abs(x1 - x0) <= DECIMAL_EPSILON)
			{
				return x1;
			}
			x0 = x1;
		}
	}
}