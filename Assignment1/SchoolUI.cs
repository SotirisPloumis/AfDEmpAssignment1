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

			} while (!option.Equals("m") && !option.Equals("a") && !option.Equals("A") && !option.Equals("M"));

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
				string[] items = line.Split('-');
				string fname = items[0];
				string lname = items[1];
				correct = DateTime.TryParse(items[2], out DateTime dob);
				if (!correct)
				{
					Console.WriteLine("the date of birth is not correct, skipping line");
					continue;
				}
				
				correct = Double.TryParse(items[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double fees);
				if (!correct)
				{
					Console.WriteLine("The fees are not a number, skipping line");
					continue;
				}
				Student s = new Student()
				{
					FirstName = fname,
					LastName = lname,
					DateOfBirth = dob,
					TuitionFess = fees
				};

				studentList.Add(s);
				
			}
		}

		public static void ManualFillStudents(List<Student> studentList)
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

				studentList.Add(s);
			}

			
		}

		public static void AutoFillTrainers(List<Trainer> trainerList)
		{
			string current = Directory.GetCurrentDirectory();

			string path = Path.Combine(current, @"..\..\Data\autotrainers.txt");

			string[] allTrainerss = File.ReadAllLines(path);

			foreach (string line in allTrainerss)
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

				trainerList.Add(t);

			}
		}

		public static void ManualFillTrainers(List<Trainer> trainerList)
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

				trainerList.Add(t);
			}
		}

		public static void AutoFillAssignments(List<Assignment> assignmentList)
		{
			string current = Directory.GetCurrentDirectory();

			string path = Path.Combine(current, @"..\..\Data\autoassignments.txt");

			string[] allAssignments = File.ReadAllLines(path);

			foreach (string line in allAssignments)
			{
				string[] items = line.Split('-');
				string title = items[0];
				string description = items[1];
				bool correct = Int32.TryParse(items[2], out int year);
				if (!correct)
				{
					Console.WriteLine("year is not a valid number, skipping line");
					continue;
				}
				correct = Int32.TryParse(items[3], out int month);
				if (!correct)
				{
					Console.WriteLine("month is not a valid number, skipping line");
					continue;
				}
				correct = Int32.TryParse(items[4], out int day);
				if (!correct)
				{
					Console.WriteLine("day is not a valid number, skipping line");
					continue;
				}

				Assignment a = new Assignment()
				{

					Title = title,
					Description = description,
					SubmissionDateAndTime = new DateTime(year, month, day)
				};

				assignmentList.Add(a);

			}
		}
		
		public static void ManualFillAssignments(List<Assignment> assignmentList)
		{
			Assignment a;
			string choice = "";
			while (!choice.Equals("exit"))
			{
				Console.WriteLine("type a new assignment");
				Console.WriteLine("Title-description-Year-Month-Day-Oral Mark- Total Mark");
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
				bool correct = Int32.TryParse(items[2], out int year);
				if (!correct)
				{
					Console.WriteLine("year is not a valid number, skipping line");
					continue;
				}
				correct = Int32.TryParse(items[3], out int month);
				if (!correct)
				{
					Console.WriteLine("month is not a valid number, skipping line");
					continue;
				}
				correct = Int32.TryParse(items[4], out int day);
				if (!correct)
				{
					Console.WriteLine("day is not a valid number, skipping line");
					continue;
				}

				a = new Assignment()
				{
					Title = title,
					Description = description,
					SubmissionDateAndTime = new DateTime(year, month, day)
				};

				assignmentList.Add(a);
			}
		}

		public static void AutoFillCourses(List<Course> courseList)
		{
			string current = Directory.GetCurrentDirectory();

			string path = Path.Combine(current, @"..\..\Data\autocourses.txt");

			string[] allCourses = File.ReadAllLines(path);

			foreach (string line in allCourses)
			{
				string[] items = line.Split('-');
				string title = items[0];
				string stream = items[1];
				string type = items[2];
				bool correct = DateTime.TryParse(items[3], out DateTime startDate);
				if (!correct)
				{
					Console.WriteLine("start date is not correct, skipping line");
					continue;
				}
				correct = DateTime.TryParse(items[4], out DateTime endDate);
				if (!correct)
				{
					Console.WriteLine("end date is not correct, skipping line");
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

				courseList.Add(c);

			}
		}

		public static void ManualFillCourses(List<Course> courseList)
		{

		}
	}
}
