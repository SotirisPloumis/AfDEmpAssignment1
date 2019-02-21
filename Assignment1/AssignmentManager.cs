using System;
using System.IO;

namespace Assignment1
{
	static class AssignmentManager
	{
		public static void InputAssignments()
		{
			//check user choice for manual or auto inut
			string option = SchoolUI.ManualOrAuto("assignments");

			if (option.Equals("a") || option.Equals("a"))
			{
				AutoFillAssignments();
				Console.ReadKey();
			}
			else
			{
				ManualFillAssignments();
				Console.WriteLine();
			}
		}

		public static void ShowAssignments(bool inFull)
		{
			//check if assignments exist
			if (SchoolUI.AssignmentList.Count < 1)
			{
				Console.WriteLine("No assignments yet\n");
				return;
			}

			Console.WriteLine("ASSIGNMENTS");

			//print assignments with all details or only title
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
			//current direcotry
			string current = Directory.GetCurrentDirectory();

			//path to the file for auto import of assignments
			string path = Path.Combine(current, @"..\..\Data\autoassignments.txt");

			string[] allAssignments;

			//try to read all the lines of the file
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

			//size of assignmentsList, we 'll need for later
			int sizeBefore = SchoolUI.AssignmentList.Count;

			//for every line...
			foreach (string line in allAssignments)
			{
				position++;

				//split the line into pieces and save it to an array
				string[] items = line.Split('-');

				//check if arguments are missing
				if (items.Length < 3)
				{
					Console.WriteLine("arguments are missing\n");
					continue;
				}
				else if (items.Length > 3)
				{
					Console.WriteLine("too many arguments\n");
					continue;
				}

				string title = items[0].Trim();

				//check if assignment exists already
				if (AssignmentExists(title))
				{
					Console.WriteLine($"assignmet {title} exists already\n");
					continue;
				}

				string description = items[1].Trim();

				bool correct = DateTime.TryParse(items[2].Trim(), out DateTime submissionDate);
				if (!correct)
				{
					Console.WriteLine("submission date is not a valid date, skipping line");
					continue;
				}
				if (submissionDate <= DateTime.Now || submissionDate >= DateTime.Now.AddYears(100))
				{
					Console.WriteLine($"{submissionDate} doesn't make sense for submission date\n");
					continue;
				}

				//create the new assignment
				Assignment a = new Assignment()
				{
					Title = title,
					Description = description,
					SubmissionDateAndTime = submissionDate
				};

				//save it to the list
				SchoolUI.AssignmentList.Add(a);

			}

			//check if we saved any new assignments
			if (SchoolUI.AssignmentList.Count == sizeBefore)
			{
				Console.WriteLine("Couldn't auto save any new assignments");
			}
			else
			{
				Console.WriteLine($"Successfully saved {SchoolUI.AssignmentList.Count - sizeBefore} new assignments");
			}
			Console.WriteLine();

		}

		private static void ManualFillAssignments()
		{
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
				else if (items.Length > 3)
				{
					Console.WriteLine("too many arguments\n");
					continue;
				}

				string title = items[0].Trim();
				if (AssignmentExists(title))
				{
					Console.WriteLine($"assignment {title} already exists\n");
					continue;
				}

				string description = items[1].Trim();

				bool correct = DateTime.TryParse(items[2].Trim(), out DateTime submissionDate);
				if (!correct)
				{
					Console.WriteLine("Date is not valid\n");
					continue;
				}
				if (submissionDate <= DateTime.Now || submissionDate >= DateTime.Now.AddYears(100))
				{
					Console.WriteLine($"{submissionDate} doesn't make sense for submission date\n");
					continue;
				}

				Assignment a = new Assignment()
				{
					Title = title,
					Description = description,
					SubmissionDateAndTime = submissionDate
				};

				SchoolUI.AssignmentList.Add(a);

				Console.WriteLine($"Assignment {a.Title} saved\n");
			}
		}

		private static bool AssignmentExists(string title)
		{
			foreach (Assignment a in SchoolUI.AssignmentList)
			{
				if (a.Title.Equals(title))
				{
					return true;
				}
			}
			return false;
		}
	}
}
