using System;
using System.IO;
using System.Globalization;
using Common;
using System.Text;
using System.Collections.Generic;

namespace School
{
	public static class StudentManager
	{
		public static List<string> InputStudents(bool auto)
		{
			if (auto)
			{
				 return AutoFillStudents();
			}
			else
			{
				return ManualFillStudents();
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

		private static List<string> AutoFillStudents()
		{
			//get the current directory
			string current = Directory.GetCurrentDirectory();

			//get the path to the file with students
			string path = Path.Combine(current, CommonValues.DataDirectory, CommonValues.StudentsFile);
			string[] allStudents;

			//try to read the lines of the file
			try
			{
				allStudents = File.ReadAllLines(path);
			}
			catch (FileNotFoundException)
			{
				string Line1 = $"ERROR: File '{CommonValues.StudentsFile}' not found";
				string Line2 = $"This program searches for a {CommonValues.StudentsFile} file in '{CommonValues.DataDirectory}' relative to the application\n";
				StringBuilder s = new StringBuilder();
				s.AppendLine(Line1);
				s.AppendLine(Line2);

				throw new FileNotFoundException(s.ToString());
			}
			catch (DirectoryNotFoundException)
			{
				string Line1 = $"ERROR: Directory '{CommonValues.DataDirectory}' not found";
				string Line2 = $"This program searches for an '{CommonValues.DataDirectory}' directory relative to '{current}'\n";

				StringBuilder s = new StringBuilder();
				s.AppendLine(Line1);
				s.AppendLine(Line2);

				throw new DirectoryNotFoundException(s.ToString());
			}

			//save the size before auto input, we need it for later
			int sizeBefore = SchoolManager.StudentList.Count;

			int position = 0;
			List<string> LineErrors = new List<string>();

			//for every line...
			foreach (string line in allStudents)
			{
				position++;
				bool correct;
				string[] items = line.Split('-');

				//check if something is missing
				if (items.Length < 4)
				{
					LineErrors.Add($"Line {position}: Arguments are missing");
					continue;
				}
				else if (items.Length > 4)
				{
					LineErrors.Add($"Line {position}: Too many arguments");
					continue;
				}

				string fname = items[0].Trim();
				string lname = items[1].Trim();

				//check if student exists already
				bool AlreadyExists = StudentExists(fname, lname);
				if (AlreadyExists)
				{
					LineErrors.Add($"Line {position}: {fname} {lname} already exists");
					continue;
				}

				//check the validity of date
				correct = DateTime.TryParse(items[2].Trim(), out DateTime dob);
				if (!correct)
				{
					LineErrors.Add($"Line {position}: Date of birth is not correct");
					continue;
				}

				//check the logical validity of date
				if (dob >= DateTime.Now || dob <= DateTime.Now.AddYears(-100))
				{
					LineErrors.Add($"Line {position}: {dob} doesn't make sense for birthday");
					continue;
				}

				//check the validity of tuition
				correct = Decimal.TryParse(items[3].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal fees);
				if (!correct)
				{
					LineErrors.Add($"Line {position}: Tuition is not a number");
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
				LineErrors.Add($"Couldn't auto save any students from file {CommonValues.StudentsFile}");
			}
			else
			{
				LineErrors.Add($"Successfully saved {SchoolManager.StudentList.Count - sizeBefore} new students");
			}
			return LineErrors;

		}

		private static List<string> ManualFillStudents()
		{
			//List<string> InputErrors = new List<string>();

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
				Student s = new Student()
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

			return new List<string>();
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
