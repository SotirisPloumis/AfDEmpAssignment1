﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace School
{
	public static class CourseManager
	{
		public static void InputCourses(bool option)
		{
			if (option)
			{
				AutoFillCourses();
				Console.ReadKey();
			}
			else
			{
				ManualFillCourses();
				Console.WriteLine();
			}
		}

		public static void ShowCourses(bool inFull)
		{
			if (SchoolManager.CourseList.Count < 1)
			{
				Console.WriteLine("No courses yet\n");
				return;
			}

			Console.WriteLine("COURSES");

			foreach (Course c in SchoolManager.CourseList)
			{
				if (inFull)
				{
					Console.WriteLine($"{SchoolManager.CourseList.IndexOf(c) + 1}: Title: {c.Title}, Stream: {c.Stream}, type: {c.Type}, starts {c.StartDate}, ends {c.EndDate}");
				}
				else
				{
					Console.WriteLine($"{SchoolManager.CourseList.IndexOf(c) + 1}: Title: {c.Title}");
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
				Console.WriteLine("autocourses.txt file not found");
				Console.WriteLine("this program searcher for an autocourses.txt file in '..\\..\\Data' relative to the application\n");
				return;
			}
			catch (DirectoryNotFoundException)
			{
				Console.WriteLine("directory 'Data' not found");
				Console.WriteLine("this program searcher for an autocourses.txt file in '..\\..\\Data' relative to the application\n");
				return;
			}

			int position = 0;

			int sizeBefore = SchoolManager.CourseList.Count;

			foreach (string line in allCourses)
			{
				position++;
				string[] items = line.Split('-');

				if (items.Length < 5)
				{
					Console.WriteLine("arguments missing");
					continue;
				}
				else if (items.Length > 5)
				{
					Console.WriteLine("too many arguments\n");
					continue;
				}

				string title = items[0].Trim();

				if (CourseExists(title))
				{
					Console.WriteLine($"course {title} exists already");
					continue;
				}

				string stream = items[1].Trim();
				string type = items[2].Trim();

				bool correct = DateTime.TryParse(items[3].Trim(), out DateTime startDate);
				if (!correct)
				{
					Console.WriteLine($"Line {position}: start date is not correct, skipping line");
					continue;
				}
				correct = DateTime.TryParse(items[4].Trim(), out DateTime endDate);
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

				SchoolManager.CourseList.Add(c);

			}
			if (SchoolManager.CourseList.Count == sizeBefore)
			{
				Console.WriteLine("Couldn't auto save any new courses");
			}
			else
			{
				Console.WriteLine($"Successfully saved {SchoolManager.CourseList.Count - sizeBefore} new courses");
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
					Console.WriteLine("arguments missing\n");
					continue;
				}
				else if (items.Length > 5)
				{
					Console.WriteLine("too many arguments\n");
					continue;
				}

				string title = items[0].Trim();

				if (CourseExists(title))
				{
					Console.WriteLine($"course {title} exists already");
					continue;
				}

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

				if (startDate >= endDate)
				{
					Console.WriteLine("start date is after end date\n");
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

				SchoolManager.CourseList.Add(c);

				Console.WriteLine($"Course {c.Title} saved\n");
			}
		}

		private static bool CourseExists(string title)
		{
			foreach (Course c in SchoolManager.CourseList)
			{
				if (c.Title.Equals(title))
				{
					return true;
				}
			}
			return false;
		}
	}
}
