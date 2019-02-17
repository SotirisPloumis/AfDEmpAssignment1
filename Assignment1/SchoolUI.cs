using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

		public static void AutoFillStudents(List<Student> studentList)
		{
			string current = Directory.GetCurrentDirectory();
			
			string path = Path.Combine(current, @"..\..\Data\autostudents.txt");

			string[] allStudents = File.ReadAllLines(path);

			foreach(string line in allStudents)
			{
				bool correct;
				string[] items = line.Split(',');
				string fname = items[0];
				string lname = items[1];
				correct = Int32.TryParse(items[2],out int year);
				if (!correct)
				{
					Console.WriteLine("the year is not a number, defaulting to 0");
					year = 0;
				}
				correct = Int32.TryParse(items[3], out int month);
				if (!correct)
				{
					Console.WriteLine("the month is not a number, defaulting to 0");
					month = 0;
				}
				correct = Int32.TryParse(items[4], out int day);
				if (!correct)
				{
					Console.WriteLine("the day is not a number, defaulting to 0");
					day = 0;
				}
				correct = Double.TryParse(items[5], NumberStyles.Any, CultureInfo.InvariantCulture, out double fees);
				if (!correct)
				{
					Console.WriteLine("The fees are not a number, defaulting to 0");
					fees = 0;
				}
				Student s = new Student()
				{
					FirstName = fname,
					LastName = lname,
					DateOfBirth = new DateTime(year, month, day),
					TuitionFess = fees
				};

				studentList.Add(s);
				
			}
		}

		public static void ManualFillStudents(List<Student> studentList)
		{

			Student s = new Student()
			{
				FirstName = Console.ReadLine(),
				LastName = Console.ReadLine(),
				DateOfBirth = new DateTime(1950, 5, 4),
				TuitionFess = 4444.3
			};

			studentList.Add(s);
		}
	}
}
