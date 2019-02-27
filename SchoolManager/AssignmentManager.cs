using System;
using System.IO;

namespace School
{
	public static class AssignmentManager
	{
		public static void InputAssignments(bool option)
		{
			if (option)
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
			if (SchoolManager.AssignmentList.Count < 1)
			{
				Console.WriteLine("No assignments yet\n");
				return;
			}

			Console.WriteLine("ASSIGNMENTS");

			//print assignments with all details or only title
			foreach (Assignment a in SchoolManager.AssignmentList)
			{
				if (inFull)
				{
					Console.Write($"{SchoolManager.AssignmentList.IndexOf(a) + 1}: Title: {a.Title}, ");
					Console.Write($"Desciption: {a.Description}, due {a.SubmissionDateAndTime}, ");
					Console.WriteLine($"oral mark {a.OralMark}, total mark {a.TotalMark}");
				}
				else
				{
					Console.WriteLine($"{SchoolManager.AssignmentList.IndexOf(a) + 1}: Title: {a.Title}");
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
				Console.WriteLine("autoassignments.txt file not found");
				Console.WriteLine("this program searcher for an autoassignments.txt file in '..\\..\\Data' relative to the application\n");
				return;
			}
			catch (DirectoryNotFoundException)
			{
				Console.WriteLine("directory 'Data' not found");
				Console.WriteLine("this program searcher for an autoassignments.txt file in '..\\..\\Data' relative to the application\n");
				return;
			}

			int position = 0;

			//size of assignmentsList, we 'll need for later
			int sizeBefore = SchoolManager.AssignmentList.Count;

			//for every line...
			foreach (string line in allAssignments)
			{
				position++;

				//split the line into pieces and save it to an array
				string[] items = line.Split('-');

				//check if arguments are missing
				if (items.Length < 5)
				{
					Console.WriteLine("arguments are missing\n");
					continue;
				}
				else if (items.Length > 5)
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
					Console.WriteLine("submission date is not a valid date, skipping line\n");
					continue;
				}
				if (submissionDate <= DateTime.Now || submissionDate >= DateTime.Now.AddYears(100))
				{
					Console.WriteLine($"{submissionDate} doesn't make sense for submission date\n");
					continue;
				}
				//submission date on weekend is not allowed
				DayOfWeek dayOfSubmission = submissionDate.DayOfWeek;
				if (dayOfSubmission == DayOfWeek.Saturday || dayOfSubmission == DayOfWeek.Sunday)
				{
					Console.WriteLine("submission day cannot be on a weekend\n");
					continue;
				}

				correct = Decimal.TryParse(items[3].Trim(), out decimal oralMark);
				if (!correct)
				{
					Console.WriteLine("oral mark is not a valid number\n");
					continue;
				}

				correct = decimal.TryParse(items[4].Trim(), out decimal totalMark);
				if (!correct)
				{
					Console.WriteLine("total mark is not a valid number\n");
					continue;
				}

				//create the new assignment
				Assignment a = new Assignment()
				{
					Title = title,
					Description = description,
					SubmissionDateAndTime = submissionDate,
					OralMark = oralMark,
					TotalMark = totalMark
				};
				
				//save it to the list
				SchoolManager.AssignmentList.Add(a);

			}

			//check if we saved any new assignments
			if (SchoolManager.AssignmentList.Count == sizeBefore)
			{
				Console.WriteLine("Couldn't auto save any new assignments");
			}
			else
			{
				Console.WriteLine($"Successfully saved {SchoolManager.AssignmentList.Count - sizeBefore} new assignments");
			}
			Console.WriteLine();

		}

		private static void ManualFillAssignments()
		{
			while (true)
			{
				Console.WriteLine("type a new assignment");
				Console.WriteLine("Title - description - day/month/year - oral mark - total mark");
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
				else if (items.Length > 5)
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
				//submission date on weekend is not allowed
				DayOfWeek dayOfSubmission = submissionDate.DayOfWeek;
				if (dayOfSubmission == DayOfWeek.Saturday || dayOfSubmission == DayOfWeek.Sunday)
				{
					Console.WriteLine("submission day cannot be on a weekend\n");
					continue;
				}

				correct = Decimal.TryParse(items[3].Trim(), out decimal oralMark);
				if (!correct)
				{
					Console.WriteLine("oral mark is not a valid number\n");
					continue;
				}

				correct = decimal.TryParse(items[4].Trim(), out decimal totalMark);
				if (!correct)
				{
					Console.WriteLine("total mark is not a valid number\n");
					continue;
				}

				Assignment a = new Assignment()
				{
					Title = title,
					Description = description,
					SubmissionDateAndTime = submissionDate,
					OralMark = oralMark,
					TotalMark = totalMark
				};

				SchoolManager.AssignmentList.Add(a);

				Console.WriteLine($"Assignment {a.Title} saved\n");
			}
		}

		private static bool AssignmentExists(string title)
		{
			foreach (Assignment a in SchoolManager.AssignmentList)
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
