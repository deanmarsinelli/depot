using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
	class Program
	{
		static void Main(string[] args)
		{
			float tempF;
			float tempC;
			float tempFConverted;

			Console.WriteLine("Enter temperature in Fahrenheit: ");
			tempF = float.Parse(Console.ReadLine());

			tempC = (tempF - 32.0f) * (5.0f / 9.0f);
			Console.WriteLine(tempF + " degrees Fahrenheit is " + tempC + " degrees Celcius");

			tempFConverted = (tempC * (9.0f / 5.0f)) + 32.0f;
			Console.WriteLine(tempC + " degrees Celcius is " + tempFConverted + " degrees Fahrenheit");
		}
	}
}
