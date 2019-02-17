using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
	static class SchoolUI
	{
		public static void Greet()
		{
			Console.WriteLine("Welcome to School Manager 2019");
			Console.WriteLine("Created by Sotiris Ploumis");
			Console.WriteLine("Assignment 1 of the C# AfDEmp Bootcamp \n");
		}

		public static void showInputOptions()
		{
			Console.WriteLine("Options:");
			Console.WriteLine("");
		}
		public static string ManualOrAuto(string element)
		{
			string option;
			do
			{
				Console.WriteLine($"type \"m\" to enter {element} manualy, or \"a\" to get a default list");

				option = Console.ReadLine();

			} while (!option.Equals("m") && !option.Equals("a"));

			return option;
		}
		public static List<Student> GetInput(string element)
		{
			List<Student> tempList = new List<Student>();

			//string input = Console.ReadLine();

			Student tempStudent = new Student()
			{
				FirstName = "sotos",
				LastName = "ploumis",
				DateOfBirth = new DateTime(1993, 4, 6),
				TuitionFess = 5334.65
			};

			tempList.Add(tempStudent);

			tempStudent = new Student()
			{
				FirstName = "kostas",
				LastName = "papadopoulos",
				DateOfBirth = new DateTime(1980, 7, 2),
				TuitionFess = 2333.54
			};

			tempList.Add(tempStudent);

			return tempList;
		}
	}
}
