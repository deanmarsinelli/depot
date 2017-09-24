using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment1
{
	class Program
	{
		static void Main(string[] args)
		{
			float x1, x2, y1, y2;
			Console.WriteLine("Welcome. This program will calculate the distance between two points and the angle between those points");
			Console.WriteLine("Enter x1");
			x1 = float.Parse(Console.ReadLine());

			Console.WriteLine("Enter y1");
			y1 = float.Parse(Console.ReadLine());

			Console.WriteLine("Enter x2");
			x2 = float.Parse(Console.ReadLine());

			Console.WriteLine("Enter y2");
			y2 = float.Parse(Console.ReadLine());

			double deltaX = x2 - x1;
			double deltaY = y2 - y1;

			double distance = Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
			double angle = Math.Atan2(deltaY, deltaX);
			double angleDegrees = angle * (180.0 / Math.PI);

			Console.WriteLine("The distance between the two points is " + distance.ToString("g3"));
			Console.WriteLine("The angle in degrees between the two points is " + angleDegrees.ToString("g3"));
		}
	}
}
