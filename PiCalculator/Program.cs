using System.Collections.Concurrent;

namespace PiCalculator;

public static class Program
{
	// TODO: What is the correct epsilon value for a decimal?
	private const decimal DECIMAL_EPSILON = 0.00000000000000000000000000001M;

	/// <summary>
	/// Calculate the value of π using François Viète's formula.
	/// </summary>
	public static void Main()
	{
		Console.WriteLine($"Math.PI              = {Math.PI}");

		var pi0 = VietePi(HeronSqrt);
		Console.WriteLine($"Viète's PI           = {pi0}");

		var pi1 = GregoryLeibnizPi();
		Console.WriteLine($"Gregory-Leibniz's PI = {pi1}");
	}

	private static decimal GregoryLeibnizPi()
	{
		ConcurrentStack<decimal> values = new();

		// var options = new ParallelOptions()
		// {
		// 	MaxDegreeOfParallelism = int.MaxValue,
		// };

		var result = Parallel.For(0, 10_000_000_000, iter =>
		{
			var numerator = 4M;
			var sign = (iter % 2M == 0M) ? 1M : -1M;
			var denominator = 2M * iter + 1M;

			var pi0 = sign * numerator / denominator;
			// if (pi0 == pi)
			// {
			// 	return pi0;
			// }

			// pi = pi0;

			// iters++;
			// Console.WriteLine($"{iter}: {pi0}");

			values.Push(pi0);
		});

		var pi = values.Sum();
		return pi;
	} 

	/// <remarks>47 iterations to max out the decimal datatype.</remarks>
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