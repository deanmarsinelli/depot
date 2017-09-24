using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
	class Program
	{
		static void Main(string[] args)
		{
			int age = 30;
			const int MaxScore = 100;
			int score = 15;
			float percent = (float)score / (float)MaxScore;

			Console.WriteLine("My age is: " + age);
			Console.WriteLine("My score % is " + percent);
		}
	}
}
