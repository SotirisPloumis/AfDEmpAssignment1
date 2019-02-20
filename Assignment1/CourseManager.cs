﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Assignment1
{
	static class CourseManager
	{
		public static void InputCourses()
		{
			string option = SchoolUI.ManualOrAuto("courses");

			if (option.Equals("a") || option.Equals("A"))
			{
				AutoFillCourses();
			}
			else
			{
				ManualFillCourses();
			}
		}

		public static void ShowCourses(bool inFull)
		{
			if (SchoolUI.CourseList.Count < 1)
			{
				Console.WriteLine("No courses yet\n");
				return;
			}

			Console.WriteLine("COURSES");

			foreach (Course c in SchoolUI.CourseList)
			{
				if (inFull)
				{
					Console.WriteLine($"{SchoolUI.CourseList.IndexOf(c) + 1}: Title: {c.Title}, Stream: {c.Stream}, type: {c.Type}, starts {c.StartDate}, ends {c.EndDate}");
				}
				else
				{
					Console.WriteLine($"{SchoolUI.CourseList.IndexOf(c) + 1}: Title: {c.Title}");
				}
			}
			Console.WriteLine();
		}

		private static void AutoFillCourses()
		{
			string current = Directory.GetCurrentDirectory();

			string path = Path.Combine(current, @"..\..\Data\autocourses.txt");

			string[] allCourses;

			try
			{
				allCourses = File.ReadAllLines(path);
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("auto courses file not found\n");
				return;
			}

			int position = 0;
			foreach (string line in allCourses)
			{
				position++;
				string[] items = line.Split('-');
				string title = items[0];
				string stream = items[1];
				string type = items[2];
				bool correct = DateTime.TryParse(items[3], out DateTime startDate);
				if (!correct)
				{
					Console.WriteLine($"Line {position}: start date is not correct, skipping line");
					continue;
				}
				correct = DateTime.TryParse(items[4], out DateTime endDate);
				if (!correct)
				{
					Console.WriteLine($"Line {position}: end date is not correct, skipping line");
					continue;
				}
				if (startDate >= endDate)
				{
					Console.WriteLine("start date is after end date\n");
					continue;
				}


				Course c = new Course()
				{

					Title = title,
					Stream = stream,
					Type = type,
					StartDate = startDate,
					EndDate = endDate
				};

				SchoolUI.CourseList.Add(c);

			}
			if (SchoolUI.CourseList.Count < 1)
			{
				Console.WriteLine("Couldn't auto save any courses");
			}
			else
			{
				Console.WriteLine($"Successfully saved {SchoolUI.CourseList.Count} courses");
			}
			Console.WriteLine();
		}

		private static void ManualFillCourses()
		{
			Course c;
			while (true)
			{
				Console.WriteLine("type a new course");
				Console.WriteLine("Title - stream - type - day/month/year of start - day/month/year of end");
				Console.WriteLine("to quit type 'exit' or '0' and hit Enter");

				string input = Console.ReadLine();

				if (input.Trim().Equals("exit") || input.Trim().Equals("0"))
				{
					break;
				}

				string[] items = input.Split('-');

				if (items.Length < 5)
				{
					Console.WriteLine("An argument is missing\n");
					continue;
				}

				string title = items[0].Trim();
				string stream = items[1].Trim();
				string type = items[2].Trim();

				bool correct = DateTime.TryParse(items[3].Trim(), out DateTime startDate);
				if (!correct)
				{
					Console.WriteLine("Start date is not valid\n");
					continue;
				}

				correct = DateTime.TryParse(items[4].Trim(), out DateTime endDate);
				if (!correct)
				{
					Console.WriteLine("End date is not valid\n");
					continue;
				}

				c = new Course()
				{
					Title = title,
					Stream = stream,
					Type = type,
					StartDate = startDate,
					EndDate = endDate
				};

				SchoolUI.CourseList.Add(c);

				Console.WriteLine($"Course {c.Title} saved\n");
			}
		}
	}
}