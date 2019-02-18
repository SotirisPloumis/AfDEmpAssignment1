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
		public static List<Student> StudentList = new List<Student>();
		public static List<Trainer> TrainerList = new List<Trainer>();
		public static List<Assignment> AssignmentList = new List<Assignment>();
		public static List<Course> CourseList = new List<Course>();

		public static void Greet()
		{
			Console.WriteLine("Welcome to School Manager 2019");
			Console.WriteLine("Created by Sotiris Ploumis");
			Console.WriteLine("Assignment 1 of the C# AfDEmp Bootcamp \n");
		}

		public static int ShowMenuAndChoose()
		{
			Console.WriteLine("1. Input students");
			Console.WriteLine("2. Show all students");

			Console.WriteLine("3. Input trainers");
			Console.WriteLine("4. Show all trainers");

			Console.WriteLine("5. Input assignments");
			Console.WriteLine("6. Show all assignments");

			Console.WriteLine("7. Input courses");
			Console.WriteLine("8. Show all courses");

			Console.WriteLine("9. Connect student with course");

			Console.WriteLine("0. Exit");

			bool goodChoice;
			int choice;
			do
			{
				string input = Console.ReadLine();
				goodChoice = Int32.TryParse(input, out choice);
			} while (!goodChoice || choice < 0 || choice > 9);

			Console.WriteLine();
			return choice;


		}

		private static string ManualOrAuto(string element)
		{
			string option;
			do
			{
				Console.WriteLine($"type \"m\" to enter {element} manualy, or \"a\" to get a default list");

				option = Console.ReadLine();

			} while (!option.Equals("m") && !option.Equals("a") && !option.Equals("A") && !option.Equals("M"));

			Console.WriteLine();
			return option;
		}

		//students

		public static void InputStudents()
		{
			string option = SchoolUI.ManualOrAuto("students");

			if (option.Equals("a") || option.Equals("A"))
			{
				SchoolUI.AutoFillStudents();
			}
			else
			{
				SchoolUI.ManualFillStudents();
			}
		}

		public static void ShowStudents(bool inFull)
		{
			if (StudentList.Count < 1)
			{
				Console.WriteLine("No students yet\n");
				return;
			}
			foreach (Student s in StudentList)
			{
				if (inFull)
				{
					Console.WriteLine($"{StudentList.IndexOf(s) + 1}: {s.FirstName} {s.LastName}, born in {s.DateOfBirth}, pays: {s.TuitionFess}");
				}
				else
				{
					Console.WriteLine($"{StudentList.IndexOf(s) + 1}: {s.FirstName} {s.LastName}");
				}
				
			}
			Console.WriteLine();
		}

		private static void AutoFillStudents()
		{
			string current = Directory.GetCurrentDirectory();

			string path = Path.Combine(current, @"..\..\Data\autostudents.txt");
			string[] allStudents = new string[1];
			try
			{
				allStudents = File.ReadAllLines(path);
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine("auto students file not found\n");
				return;
			}

			int position = 1;
			foreach(string line in allStudents)
			{
				bool correct;
				string[] items = line.Split('-');
				string fname = items[0];
				string lname = items[1];
				correct = DateTime.TryParse(items[2], out DateTime dob);
				if (!correct)
				{
					Console.WriteLine($"Line {position}: the date of birth is not correct, skipping line");
					continue;
				}
				
				correct = Double.TryParse(items[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double fees);
				if (!correct)
				{
					Console.WriteLine($"Line {position}: the tuition are not a number, skipping line");
					continue;
				}
				Student s = new Student()
				{
					FirstName = fname,
					LastName = lname,
					DateOfBirth = dob,
					TuitionFess = fees
				};

				StudentList.Add(s);
				
			}
			if (StudentList.Count < 1)
			{
				Console.WriteLine("Couldn't auto save any students");
			}
			else
			{
				Console.WriteLine($"Successfully saved {StudentList.Count} students");
			}
			Console.WriteLine();
			
		}

		private static void ManualFillStudents()
		{
			Student s;
			string choice = "";
			while (!choice.Equals("exit"))
			{
				Console.WriteLine("type a new student");
				Console.WriteLine("firstName-lastName-dayOfBirth/monthOfBirth/yearOfBirth-tuition");
				Console.WriteLine("to quit type \"exit\" and hit Enter");
				string input = Console.ReadLine();
				string[] items = input.Split('-');
				choice = items[0];
				if (choice.Equals("exit"))
				{
					break;
				}
				string fname = items[0];
				string lname = items[1];
				bool correct = DateTime.TryParse(items[2], out DateTime dob);
				if (!correct)
				{
					Console.WriteLine("Date of birth is not correct");
					continue;
				}
				
				correct = Double.TryParse(items[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double tuition);
				if (!correct)
				{
					Console.WriteLine("fees are not a valid number");
					continue;
				}
				s = new Student()
				{
					FirstName = fname,
					LastName = lname,
					DateOfBirth = dob,
					TuitionFess = tuition
				};

				StudentList.Add(s);
			}

			
		}

		//trainers

		public static void InputTrainers()
		{
			string option = SchoolUI.ManualOrAuto("trainers");

			if (option.Equals("a") || option.Equals("A"))
			{
				SchoolUI.AutoFillTrainers();
			}
			else
			{
				SchoolUI.ManualFillTrainers();
			}
		}

		public static void ShowTrainers(bool inFull)
		{
			if (TrainerList.Count < 1)
			{
				Console.WriteLine("No trainers yet\n");
				return;
			}
			foreach (Trainer t in TrainerList)
			{
				if (inFull)
				{
					Console.WriteLine($"{TrainerList.IndexOf(t) + 1}: {t.FirstName} {t.LastName}, teaches {t.Subject}");
				}
				else
				{
					Console.WriteLine($"{TrainerList.IndexOf(t) + 1}: {t.FirstName} {t.LastName}");
				}
			}
			Console.WriteLine();
		}

		private static void AutoFillTrainers()
		{
			string current = Directory.GetCurrentDirectory();

			string path = Path.Combine(current, @"..\..\Data\autotrainers.txt");

			string[] allTrainers = new string[1];
			try
			{
				allTrainers = File.ReadAllLines(path);
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine("auto trainers file not found\n");
				return;
			}

			foreach (string line in allTrainers)
			{
				string[] items = line.Split('-');
				string fname = items[0];
				string lname = items[1];
				string subject = items[2];
				
				Trainer t = new Trainer()
				{

					FirstName = fname,
					LastName = lname,
					Subject = subject
				};

				TrainerList.Add(t);

			}
			if (TrainerList.Count < 1)
			{
				Console.WriteLine("Couldn't auto save any trainers");
			}
			else
			{
				Console.WriteLine($"Successfully saved {TrainerList.Count} trainers");
			}
			Console.WriteLine();
		}

		private static void ManualFillTrainers()
		{
			Trainer t;
			string choice = "";
			while (!choice.Equals("exit"))
			{
				Console.WriteLine("type a new trainer");
				Console.WriteLine("firstName-lastName-Subject");
				Console.WriteLine("to quit type \"exit\" and hit Enter");
				string input = Console.ReadLine();
				string[] items = input.Split('-');
				choice = items[0];
				if (choice.Equals("exit"))
				{
					break;
				}
				string fname = items[0];
				string lname = items[1];
				string subject = items[2];

				t = new Trainer()
				{
					FirstName = fname,
					LastName = lname,
					Subject = subject
				};

				TrainerList.Add(t);
			}
		}

		//assignments

		public static void InputAssignments()
		{
			string option = SchoolUI.ManualOrAuto("assignments");

			if (option.Equals("a") || option.Equals("a"))
			{
				SchoolUI.AutoFillAssignments();
			}
			else
			{
				SchoolUI.ManualFillAssignments();
			}
		}

		public static void ShowAssignments(bool inFull)
		{
			if (AssignmentList.Count < 1)
			{
				Console.WriteLine("No assignments yet\n");
				return;
			}

			foreach (Assignment a in AssignmentList)
			{
				if (inFull)
				{
					Console.WriteLine($"{AssignmentList.IndexOf(a) + 1}: Title: {a.Title}, Desciption: {a.Description}, due {a.SubmissionDateAndTime}");
				}
				else
				{
					Console.WriteLine($"{AssignmentList.IndexOf(a) + 1}: Title: {a.Title}");
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

				AssignmentList.Add(a);

			}
			if (AssignmentList.Count < 1)
			{
				Console.WriteLine("Couldn't auto save any assignments");
			}
			else
			{
				Console.WriteLine($"Successfully saved {AssignmentList.Count} assignments");
			}
			Console.WriteLine();

		}
		
		private static void ManualFillAssignments()
		{
			Assignment a;
			string choice = "";
			while (!choice.Equals("exit"))
			{
				Console.WriteLine("type a new assignment");
				Console.WriteLine("Title-description - day/month/year");
				Console.WriteLine("to quit type \"exit\" and hit Enter");
				string input = Console.ReadLine();
				string[] items = input.Split('-');
				choice = items[0];
				if (choice.Equals("exit"))
				{
					break;
				}
				string title = items[0];
				string description = items[1];
				bool correct = DateTime.TryParse(items[2], out DateTime submissionDate);
				if (!correct)
				{
					Console.WriteLine("date is not valid, skipping line");
					continue;
				}

				a = new Assignment()
				{
					Title = title,
					Description = description,
					SubmissionDateAndTime = submissionDate
				};

				AssignmentList.Add(a);
			}
		}

		//courses

		public static void InputCourses() {
			string option = SchoolUI.ManualOrAuto("courses");

			if (option.Equals("a") || option.Equals("A"))
			{
				SchoolUI.AutoFillCourses();
			}
			else
			{
				SchoolUI.ManualFillCourses();
			}
		}

		public static void ShowCourses(bool inFull)
		{
			if (CourseList.Count < 1)
			{
				Console.WriteLine("No courses yet\n");
				return;
			}

			foreach (Course c in CourseList)
			{
				if (inFull)
				{
					Console.WriteLine($"{CourseList.IndexOf(c) + 1}: Title: {c.Title}, Stream: {c.Stream}, type: {c.Type}, starts {c.StartDate}, ends {c.EndDate}");
				}
				else
				{
					Console.WriteLine($"{CourseList.IndexOf(c) + 1}: Title: {c.Title}");
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
				

				Course c = new Course()
				{

					Title = title,
					Stream = stream,
					Type = type,
					StartDate = startDate,
					EndDate = endDate
				};

				CourseList.Add(c);

			}
			if (CourseList.Count < 1)
			{
				Console.WriteLine("Couldn't auto save any courses");
			}
			else
			{
				Console.WriteLine($"Successfully saved {CourseList.Count} courses");
			}
			Console.WriteLine();
		}

		private static void ManualFillCourses()
		{
			Course c;
			string choice = "";
			while (!choice.Equals("exit"))
			{
				Console.WriteLine("type a new course");
				Console.WriteLine("Title-stream - type - day/month/year of start - day/month/year of end");
				Console.WriteLine("to quit type \"exit\" and hit Enter");
				string input = Console.ReadLine();
				string[] items = input.Split('-');
				choice = items[0];
				if (choice.Equals("exit"))
				{
					break;
				}
				string title = items[0];
				string stream = items[1];
				string type = items[2];
				bool correct = DateTime.TryParse(items[3], out DateTime startDate);
				if (!correct)
				{
					Console.WriteLine("start date is not valid, skipping line");
					continue;
				}
				correct = DateTime.TryParse(items[4], out DateTime endDate);
				if (!correct)
				{
					Console.WriteLine("end date is not correct, skipping line");
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

				CourseList.Add(c);
			}
		}

		// student course
		public static void ShowStudentCourses()
		{
			Console.WriteLine("List of students:\n");

			ShowStudents(false);

			Console.WriteLine("List of courses:\n");

			ShowCourses(false);


		}
	}
}
