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

		// var pi1 = GregoryLeibnizPi();
		// Console.WriteLine($"Gregory-Leibniz's PI = {pi1}");

		Console.WriteLine(ToString(5, 16));
		Console.WriteLine(ToString(15, 16));
		Console.WriteLine(ToString(16, 16));
		Console.WriteLine(ToString(0x4A, 16));
		Console.WriteLine(ToString(0x6B9F, 16));
		Console.WriteLine(ToString(10.5M, 10));
		Console.WriteLine(ToString(123.4567M, 10));
		Console.WriteLine(ToString(1.000001M, 10));


		Console.WriteLine(1M / 3M);
		Console.WriteLine(ToString(1M / 3M, 3));
		Console.WriteLine(ToString(1M / 3M, 5));
		Console.WriteLine(ToString(1M / 3M, 7));

		var pi = 3.1415926535897932384626433832795028841971693993751058M;
		Console.WriteLine(pi);
		Console.WriteLine(ToString(pi, 10));
		Console.WriteLine(ToString(pi, 2));
		Console.WriteLine(ToString(pi, 3));
		Console.WriteLine(ToString(pi, 5));
		Console.WriteLine(ToString(pi, 7));
		Console.WriteLine(ToString(pi, 9));
		Console.WriteLine(ToString(pi, 11));
		Console.WriteLine(ToString(pi, 13));
	}

	private static string ToString(decimal value, int b)
	{
		var digits = new List<char>();

		var whole = (int)decimal.Round(value);
		var frac = (value - whole);

		while (whole > 0)
		{
			var v0 = whole % b;
			if (v0 < 10)
			{
				digits.Add((char)(v0 + '0'));
			}
			else
			{
				digits.Add((char)(v0 - 10 + 'A'));
			}

			whole /= b;
		}
		digits.Reverse();

		if (frac != 0)
		{
			digits.Add('.');

			var counter = 100;
			while (frac != 0 && counter >= 0)
			{
				frac *= b;
				var v0 = (int)frac % b;
				// Console.WriteLine(v0);
				if (v0 < 10)
				{
					digits.Add((char)(v0 + '0'));
				}
				else
				{
					digits.Add((char)(v0 - 10 + 'A'));
				}

				frac = frac - (int)frac;
				counter--;
			}
		}

		return string.Join(string.Empty, digits);
	}

	private static decimal GregoryLeibnizPi()
	{
		ConcurrentStack<decimal> values = new();

		var result = Parallel.For(0, 1000, iter =>
		{
			var numerator = 4M;
			var sign = (iter % 2M == 0M) ? 1M : -1M;
			var denominator = 2M * iter + 1M;

			var pi0 = sign * numerator / denominator;

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