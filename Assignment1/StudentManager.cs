using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Assignment1
{
	static class StudentManager
	{
		public static void InputStudents()
		{
			string option = SchoolUI.ManualOrAuto("students");

			if (option.Equals("a") || option.Equals("A"))
			{
				AutoFillStudents();
			}
			else
			{
				ManualFillStudents();
			}
		}

		public static void ShowStudents(bool inFull)
		{
			if (SchoolUI.StudentList.Count < 1)
			{
				Console.WriteLine("No students yet\n");
				return;
			}

			Console.WriteLine("STUDENTS");

			foreach (Student s in SchoolUI.StudentList)
			{
				if (inFull)
				{
					Console.WriteLine($"{SchoolUI.StudentList.IndexOf(s) + 1}: {s.FirstName} {s.LastName}, born in {s.DateOfBirth}, pays: {s.TuitionFess}");
				}
				else
				{
					Console.WriteLine($"{SchoolUI.StudentList.IndexOf(s) + 1}: {s.FirstName} {s.LastName}");
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
			catch (FileNotFoundException)
			{
				Console.WriteLine("auto students file not found\n");
				return;
			}

			int position = 1;
			foreach (string line in allStudents)
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
				if (dob >= DateTime.Now || dob <= DateTime.Now.AddYears(-100))
				{
					Console.WriteLine($"{dob} doesn't make sense for birthday\n");
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

				SchoolUI.StudentList.Add(s);

			}
			if (SchoolUI.StudentList.Count < 1)
			{
				Console.WriteLine("Couldn't auto save any students");
			}
			else
			{
				Console.WriteLine($"Successfully saved {SchoolUI.StudentList.Count} students");
			}
			Console.WriteLine();

		}

		private static void ManualFillStudents()
		{
			Student s;
			while (true)
			{
				Console.WriteLine("type a new student");
				Console.WriteLine("firstName-lastName-dayOfBirth/monthOfBirth/yearOfBirth-tuition");
				Console.WriteLine("to quit type 'exit' or '0' and hit Enter\n");

				string input = Console.ReadLine();

				if (input.Trim().Equals("exit") || input.Trim().Equals("0"))
				{
					break;
				}

				string[] items = input.Split('-');

				if (items.Length < 4)
				{
					Console.WriteLine("An argument is missing\n");
					continue;
				}

				string fname = items[0].Trim();
				string lname = items[1].Trim();

				bool correct = DateTime.TryParse(items[2].Trim(), out DateTime dob);
				if (!correct)
				{
					Console.WriteLine("Date of birth is not correct\n");
					continue;
				}
				if (dob >= DateTime.Now || dob <= DateTime.Now.AddYears(-100))
				{
					Console.WriteLine($"{dob} doesn't make sense for birthday\n");
					continue;
				}

				correct = Double.TryParse(items[3].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double tuition);
				if (!correct)
				{
					Console.WriteLine("Tuition is not a valid number\n");
					continue;
				}

				s = new Student()
				{
					FirstName = fname,
					LastName = lname,
					DateOfBirth = dob,
					TuitionFess = tuition
				};

				SchoolUI.StudentList.Add(s);

				Console.WriteLine($"Student {s.FirstName} {s.LastName} saved\n");
			}


		}
	}
}
