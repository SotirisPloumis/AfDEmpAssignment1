using System;
using System.IO;
using System.Globalization;

namespace Assignment1
{
	static class StudentManager
	{
		public static void InputStudents(string option)
		{
			if (option.Equals("a") || option.Equals("A"))
			{
				AutoFillStudents();
				Console.ReadKey();
			}
			else
			{
				ManualFillStudents();
				Console.WriteLine();
			}
		}

		public static void ShowStudents(bool inFull)
		{
			//check if students exist
			if (SchoolManager.StudentList.Count < 1)
			{
				Console.WriteLine("No students yet\n");
				return;
			}

			Console.WriteLine("STUDENTS");

			//print all students, all info or only names
			foreach (Student s in SchoolManager.StudentList)
			{
				if (inFull)
				{
					Console.WriteLine($"{SchoolManager.StudentList.IndexOf(s) + 1}: {s.FirstName} {s.LastName}, born in {s.DateOfBirth}, pays: {s.TuitionFess}");
				}
				else
				{
					Console.WriteLine($"{SchoolManager.StudentList.IndexOf(s) + 1}: {s.FirstName} {s.LastName}");
				}

			}
			Console.WriteLine();
		}

		private static void AutoFillStudents()
		{
			//get the current directory
			string current = Directory.GetCurrentDirectory();

			//get the path to the file with students
			string path = Path.Combine(current, @"..\..\Data\autostudents.txt");
			string[] allStudents;

			//try to read the lines of the file
			try
			{
				allStudents = File.ReadAllLines(path);
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("autostudents.txt file not found");
				Console.WriteLine("this program searcher for an autostudents.txt file in '..\\..\\Data' relative to the application\n");
				return;
			}
			catch (DirectoryNotFoundException)
			{
				Console.WriteLine("directory 'Data' not found");
				Console.WriteLine("this program searcher for an autostudents.txt file in '..\\..\\Data' relative to the application\n");
				return;
			}

			//save the size before auto input, we need it for later
			int sizeBefore = SchoolManager.StudentList.Count;

			//for every line...
			int position = 1;
			foreach (string line in allStudents)
			{
				bool correct;
				string[] items = line.Split('-');

				//check if something is missing
				if (items.Length < 4)
				{
					Console.WriteLine("An argument is missing\n");
					continue;
				}
				else if (items.Length > 4)
				{
					Console.WriteLine("too many arguments\n");
					continue;
				}

				string fname = items[0];
				string lname = items[1];

				//check if student exists already
				bool AlreadyExists = StudentExists(fname, lname);
				if (AlreadyExists)
				{
					Console.WriteLine($"{fname} {lname} already exists\n");
					continue;
				}

				//check the validity of date
				correct = DateTime.TryParse(items[2], out DateTime dob);
				if (!correct)
				{
					Console.WriteLine($"Line {position}: the date of birth is not correct, skipping line");
					continue;
				}

				//check the logical validity of date
				if (dob >= DateTime.Now || dob <= DateTime.Now.AddYears(-100))
				{
					Console.WriteLine($"{dob} doesn't make sense for birthday\n");
					continue;
				}

				//check the validity of tuition
				correct = Decimal.TryParse(items[3], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal fees);
				if (!correct)
				{
					Console.WriteLine($"Line {position}: the tuition are not a number, skipping line");
					continue;
				}

				//create the new student
				Student s = new Student()
				{
					FirstName = fname,
					LastName = lname,
					DateOfBirth = dob,
					TuitionFess = fees
				};

				//add the student to the list
				SchoolManager.StudentList.Add(s);

			}
			//check if any students got added
			if (SchoolManager.StudentList.Count == sizeBefore)
			{
				Console.WriteLine("Couldn't auto save any students from the file");
			}
			else
			{
				Console.WriteLine($"Successfully saved {SchoolManager.StudentList.Count - sizeBefore} new students");
			}
			Console.WriteLine();

		}

		private static void ManualFillStudents()
		{
			Student s;
			while (true)
			{
				//prompt
				Console.WriteLine("type a new student");
				Console.WriteLine("firstName-lastName-dayOfBirth/monthOfBirth/yearOfBirth-tuition");
				Console.WriteLine("to quit type 'exit' or '0' and hit Enter\n");

				//get the line of new student from user
				string input = Console.ReadLine();

				//if user types '0' or 'exit', break and go back
				if (input.Trim().Equals("exit") || input.Trim().Equals("0"))
				{
					break;
				}

				//split the input line by '-' and put it into an array
				string[] items = input.Split('-');

				//check if something is missing
				if (items.Length < 4)
				{
					Console.WriteLine("An argument is missing\n");
					continue;
				}
				else if (items.Length > 4)
				{
					Console.WriteLine("too many arguements\n");
					continue;
				}

				//first argument is firstname, second is lastname
				string fname = items[0].Trim();
				string lname = items[1].Trim();

				//check if student exists already
				if (StudentExists(fname,lname))
				{
					Console.WriteLine($"Student {fname} {lname} already exists\n");
					continue;
				}

				//check type validity of input date
				bool correct = DateTime.TryParse(items[2].Trim(), out DateTime dob);
				if (!correct)
				{
					Console.WriteLine("Date of birth is not correct\n");
					continue;
				}

				//check logical validity of date
				//date of birth cannot be in the future
				//student cannot be over 100 years old
				if (dob >= DateTime.Now || dob <= DateTime.Now.AddYears(-100))
				{
					Console.WriteLine($"{dob} doesn't make sense for birthday\n");
					continue;
				}

				//check type validity of tuition
				correct = Decimal.TryParse(items[3].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tuition);
				if (!correct)
				{
					Console.WriteLine("Tuition is not a valid number\n");
					continue;
				}

				//create the new student
				s = new Student()
				{
					FirstName = fname,
					LastName = lname,
					DateOfBirth = dob,
					TuitionFess = tuition
				};

				//add the student to StudentList
				SchoolManager.StudentList.Add(s);

				//print result
				Console.WriteLine($"Student {s.FirstName} {s.LastName} saved\n");
			}
		}
		private static bool StudentExists(string firstName,string lastName)
		{
			//criteria for existence is firstname,lastname
			foreach (Student st in SchoolManager.StudentList)
			{
				if (st.FirstName.Equals(firstName) && st.LastName.Equals(lastName))
				{
					return true;
				}
			}
			return false;
		}
	}
}
