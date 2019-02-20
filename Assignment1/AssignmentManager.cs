using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Assignment1
{
	static class AssignmentManager
	{
		public static void InputAssignments()
		{
			string option = SchoolUI.ManualOrAuto("assignments");

			if (option.Equals("a") || option.Equals("a"))
			{
				AutoFillAssignments();
			}
			else
			{
				ManualFillAssignments();
			}
		}

		public static void ShowAssignments(bool inFull)
		{
			if (SchoolUI.AssignmentList.Count < 1)
			{
				Console.WriteLine("No assignments yet\n");
				return;
			}

			Console.WriteLine("ASSIGNMENTS");

			foreach (Assignment a in SchoolUI.AssignmentList)
			{
				if (inFull)
				{
					Console.WriteLine($"{SchoolUI.AssignmentList.IndexOf(a) + 1}: Title: {a.Title}, Desciption: {a.Description}, due {a.SubmissionDateAndTime}");
				}
				else
				{
					Console.WriteLine($"{SchoolUI.AssignmentList.IndexOf(a) + 1}: Title: {a.Title}");
				}
			}
			Console.WriteLine();
		}

		private static void AutoFillAssignments()
		{
			string current = Directory.GetCurrentDirectory();

			string path = Path.Combine(current, @"..\..\Data\autoassignments.txt");

			string[] allAssignments;

			try
			{
				allAssignments = File.ReadAllLines(path);
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("auto assignments file not found\n");
				return;
			}

			int position = 0;
			foreach (string line in allAssignments)
			{
				position++;
				string[] items = line.Split('-');
				string title = items[0];
				string description = items[1];
				bool correct = DateTime.TryParse(items[2], out DateTime submissionDate);
				if (!correct)
				{
					Console.WriteLine("submission date is not a valid date, skipping line");
					continue;
				}


				Assignment a = new Assignment()
				{

					Title = title,
					Description = description,
					SubmissionDateAndTime = submissionDate
				};

				SchoolUI.AssignmentList.Add(a);

			}
			if (SchoolUI.AssignmentList.Count < 1)
			{
				Console.WriteLine("Couldn't auto save any assignments");
			}
			else
			{
				Console.WriteLine($"Successfully saved {SchoolUI.AssignmentList.Count} assignments");
			}
			Console.WriteLine();

		}

		private static void ManualFillAssignments()
		{
			Assignment a;
			while (true)
			{
				Console.WriteLine("type a new assignment");
				Console.WriteLine("Title - description - day/month/year");
				Console.WriteLine("to quit type 'exit' or '0' and hit Enter");

				string input = Console.ReadLine();

				if (input.Trim().Equals("exit") || input.Trim().Equals("0"))
				{
					break;
				}

				string[] items = input.Split('-');

				if (items.Length < 3)
				{
					Console.WriteLine("An argument is missing\n");
					continue;
				}

				string title = items[0].Trim();
				string description = items[1].Trim();
				bool correct = DateTime.TryParse(items[2].Trim(), out DateTime submissionDate);
				if (!correct)
				{
					Console.WriteLine("Date is not valid\n");
					continue;
				}

				a = new Assignment()
				{
					Title = title,
					Description = description,
					SubmissionDateAndTime = submissionDate
				};

				SchoolUI.AssignmentList.Add(a);

				Console.WriteLine($"Assignment {a.Title} saved\n");
			}
		}
	}
}
