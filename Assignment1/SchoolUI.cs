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
			Console.WriteLine("MAIN OPTIONS");

			Console.WriteLine("1. Input students");
			Console.WriteLine("2. Show all students");

			Console.WriteLine("3. Input trainers");
			Console.WriteLine("4. Show all trainers");

			Console.WriteLine("5. Input assignments");
			Console.WriteLine("6. Show all assignments");

			Console.WriteLine("7. Input courses");
			Console.WriteLine("8. Show all courses");

			Console.WriteLine("9. Manage connections");

			Console.WriteLine("10. Check date for submissions");

			Console.WriteLine("0. Exit");

			bool goodChoice;
			int choice;
			do
			{
				string input = Console.ReadLine();
				goodChoice = Int32.TryParse(input, out choice);
			} while (!goodChoice || choice < 0 || choice > 10);

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

		public static void DoMainAction(MenuOptions MainOption)
		{
			switch (MainOption)
			{
				case MenuOptions.InputStudents:
					InputStudents();
					break;
				case MenuOptions.ShowStudents:
					ShowStudents(true);
					Console.ReadKey();
					break;
				case MenuOptions.InputTrainers:
					InputTrainers();
					break;
				case MenuOptions.ShowTrainers:
					ShowTrainers(true);
					Console.ReadKey();
					break;
				case MenuOptions.InputAssignments:
					InputAssignments();
					break;
				case MenuOptions.ShowAssignments:
					ShowAssignments(true);
					Console.ReadKey();
					break;
				case MenuOptions.InputCourses:
					InputCourses();
					break;
				case MenuOptions.ShowCourses:
					ShowCourses(true);
					Console.ReadKey();
					break;
				case MenuOptions.ManageConnections:
					int ConnectChoice;
					ConnectionMenuOptions connectOption;

					do
					{
						ConnectChoice = ShowConnectionsMenuAndChoose();

						connectOption = (ConnectionMenuOptions)ConnectChoice;

						DoConnectionAction(connectOption);

					} while (connectOption != ConnectionMenuOptions.Exit);

					break;
				case MenuOptions.CheckDateForSubmissions:
					CheckDateForSubmissions();
					break;
				default:
					break;
			}

			
		}

		//students

		private static void InputStudents()
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

		private static void ShowStudents(bool inFull)
		{
			if (StudentList.Count < 1)
			{
				Console.WriteLine("No students yet\n");
				return;
			}

			Console.WriteLine("STUDENTS");

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
			catch (FileNotFoundException)
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
			string choice;
			while (true)
			{
				Console.WriteLine("type a new student");
				Console.WriteLine("firstName-lastName-dayOfBirth/monthOfBirth/yearOfBirth-tuition");
				Console.WriteLine("to quit type 'exit' or '0' and hit Enter");
				string input = Console.ReadLine();
				string[] items = input.Split('-');
				if (items.Length < 4)
				{
					Console.WriteLine("something is missing\n");
					continue;
				}
				choice = items[0];
				if (choice.Equals("exit") || choice.Equals("0"))
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

		private static void InputTrainers()
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

		private static void ShowTrainers(bool inFull)
		{
			if (TrainerList.Count < 1)
			{
				Console.WriteLine("No trainers yet\n");
				return;
			}

			Console.WriteLine("TRAINERS");

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
			catch (FileNotFoundException)
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
			string choice;
			while (true)
			{
				Console.WriteLine("type a new trainer");
				Console.WriteLine("firstName-lastName-Subject");
				Console.WriteLine("to quit type 'exit' or '0' and hit Enter");
				string input = Console.ReadLine();
				string[] items = input.Split('-');
				if (items.Length < 3)
				{
					Console.WriteLine("something is missing\n");
					continue;
				}
				choice = items[0];
				if (choice.Equals("exit") || choice.Equals("0"))
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

		private static void InputAssignments()
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

		private static void ShowAssignments(bool inFull)
		{
			if (AssignmentList.Count < 1)
			{
				Console.WriteLine("No assignments yet\n");
				return;
			}

			Console.WriteLine("ASSIGNMENTS");

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
				Console.WriteLine("to quit type 'exit' or '0' and hit Enter");
				string input = Console.ReadLine();
				string[] items = input.Split('-');
				if (items.Length < 3)
				{
					Console.WriteLine("something is missing\n");
					continue;
				}

				choice = items[0];
				if (choice.Equals("exit") || choice.Equals("0"))
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

		private static void InputCourses() {
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

		private static void ShowCourses(bool inFull)
		{
			if (CourseList.Count < 1)
			{
				Console.WriteLine("No courses yet\n");
				return;
			}

			Console.WriteLine("COURSES");

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
				Console.WriteLine("to quit type 'exit' or '0' and hit Enter");
				string input = Console.ReadLine();
				string[] items = input.Split('-');
				if (items.Length < 5)
				{
					Console.WriteLine("something is missing\n");
					continue;
				}

				choice = items[0];
				if (choice.Equals("exit") || choice.Equals("0"))
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

		//connections

		private static int ShowConnectionsMenuAndChoose()
		{
			Console.WriteLine("CONNECT OPTIONS");

			Console.WriteLine("1. Connect students with courses");
			Console.WriteLine("2. Show all students-courses connections");

			Console.WriteLine("3. Connect trainers with courses");
			Console.WriteLine("4. Show all trainers-courses connections");

			Console.WriteLine("5. Connect assignments with courses");
			Console.WriteLine("6. Show all assignments-courses connections");

			Console.WriteLine("7. Connect assignments with students");
			Console.WriteLine("8. Show all assignments-students connections");

			Console.WriteLine("9. Show students with more than one course");

			Console.WriteLine("0. Exit to basic menu");

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

		private static void DoConnectionAction(ConnectionMenuOptions ConnectionOption)
		{
			switch (ConnectionOption)
			{
				case ConnectionMenuOptions.ConnectStudentsCourses:
					ConnectStudentsCourses();
					break;
				case ConnectionMenuOptions.ShowStudentsCourses:
					ShowStudentCourses();
					Console.ReadKey();
					break;
				case ConnectionMenuOptions.ConnectTrainersCourses:
					ConnectTrainersCourses();
					break;
				case ConnectionMenuOptions.ShowTrainersCourses:
					ShowTrainersCourses();
					Console.ReadKey();
					break;
				case ConnectionMenuOptions.ConnectAssignmentsCourses:
					ConnectAssignmentsCourses();
					break;
				case ConnectionMenuOptions.ShowAssignmentsCourses:
					ShowAssignmentsCourses();
					Console.ReadKey();
					break;
				case ConnectionMenuOptions.ConnectAssignmentsStudents:
					ConnectAssignmentsStudents();
					break;
				case ConnectionMenuOptions.ShowAssignmentsStudents:
					ShowAssignmentsStudents();
					Console.ReadKey();
					break;
				case ConnectionMenuOptions.ShowStudentsManyCourses:
					ShowStudentsManyCourses();
					Console.ReadKey();
					break;
				default:
					break;
			}
		}

		// students courses

		private static void ConnectStudentsCourses()
		{
			if (StudentList.Count == 0 || CourseList.Count == 0)
			{
				Console.WriteLine("students or courses do not exist\n");
				return;
			}

			Console.WriteLine("All students:\n");

			ShowStudents(false);

			Console.WriteLine("All courses:\n");

			ShowCourses(false);

			string input;
			bool correctInput;

			do
			{
				Console.WriteLine("type the number of student and course you want to connect, using -");
				Console.WriteLine("for example type '1-2' to connect the first student with the second course");
				Console.WriteLine("type '0' to leave\n");

				input = Console.ReadLine();
				if (input.Trim().Equals("0"))
				{
					break;
				}
				else if (input.Trim().Equals(""))
				{
					continue;
				}
				string[] items = input.Split('-');

				string StudentInput = items[0];
				correctInput = Int32.TryParse(StudentInput, out int StudentCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong student attribute\n");
					continue;
				}

				string CourseInput = items[1];
				correctInput = Int32.TryParse(CourseInput, out int CourseCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong course attribute\n");
					continue;
				}

				StudentList[StudentCode - 1].CourseCodes.Add(CourseCode - 1);
				CourseList[CourseCode - 1].StudentCodes.Add(StudentCode - 1);

				Console.WriteLine($"you connected student {StudentCode}: {StudentList[StudentCode - 1].LastName} with course {CourseCode}: {CourseList[CourseCode - 1].Title}\n");



			} while (!input.Equals("0"));

		}

		private static void ShowStudentCourses()
		{
			foreach (Course c in CourseList)
			{
				Console.WriteLine($"Course: {c.Title}");

				foreach (int studentCode in c.StudentCodes)
				{
					Console.WriteLine($"{StudentList[studentCode].LastName}");
				}
				Console.WriteLine();
			}
		}

		private static void ShowStudentsManyCourses()
		{
			Console.WriteLine("List of students with more than one courses\n");

			List<string> result = new List<string>();

			foreach (Student s in StudentList)
			{
				if (s.CourseCodes.Count > 1)
				{
					result.Add($"{StudentList.IndexOf(s)}: {s.FirstName} {s.LastName}");
				}
			}
			if (result.Count < 1)
			{
				Console.WriteLine("no students with these critria found\n");
				
			}
			foreach (string line in result)
			{
				Console.WriteLine(line);
			}

			Console.WriteLine();
		}

		// trainers courses

		private static void ConnectTrainersCourses()
		{
			if (TrainerList.Count == 0 || CourseList.Count == 0)
			{
				Console.WriteLine("trainers or courses do not exist\n");
				return;
			}

			Console.WriteLine("All trainers:\n");

			ShowTrainers(false);

			Console.WriteLine("All courses:\n");

			ShowCourses(false);

			string input;
			bool correctInput;

			do
			{
				Console.WriteLine("type the number of trainer and course you want to connect, using -");
				Console.WriteLine("for example type '1-2' to connect the first trainer with the second course");
				Console.WriteLine("type '0' to leave\n");

				input = Console.ReadLine();
				if (input.Trim().Equals("0"))
				{
					break;
				}
				else if (input.Trim().Equals(""))
				{
					continue;
				}
				string[] items = input.Split('-');

				string TrainerInput = items[0];
				correctInput = Int32.TryParse(TrainerInput, out int TrainerCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong trainer attribute\n");
					continue;
				}

				string CourseInput = items[1];
				correctInput = Int32.TryParse(CourseInput, out int CourseCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong course attribute\n");
					continue;
				}

				TrainerList[TrainerCode - 1].CourseCodes.Add(CourseCode - 1);
				CourseList[CourseCode - 1].TrainerCodes.Add(TrainerCode - 1);

				Console.WriteLine($"you connected trainer {TrainerCode}: {TrainerList[TrainerCode - 1].LastName} with course {CourseCode}: {CourseList[CourseCode - 1].Title}\n");



			} while (!input.Equals("0"));
		}

		private static void ShowTrainersCourses()
		{
			foreach (Course c in CourseList)
			{
				Console.WriteLine($"Course: {c.Title}");

				foreach (int trainerCode in c.TrainerCodes)
				{
					Console.WriteLine($"{TrainerList[trainerCode].LastName}");
				}
				Console.WriteLine();
			}
		}

		// assignments courses

		private static void ConnectAssignmentsCourses()
		{
			if (AssignmentList.Count == 0 || CourseList.Count == 0)
			{
				Console.WriteLine("assignments or courses do not exist\n");
				return;
			}

			Console.WriteLine("All assignments:\n");

			ShowAssignments(false);

			Console.WriteLine("All courses:\n");

			ShowCourses(false);

			string input;
			bool correctInput;

			do
			{
				Console.WriteLine("type the number of assignment and course you want to connect, using -");
				Console.WriteLine("for example type '1-2' to connect the first assignment with the second course");
				Console.WriteLine("type '0' to leave\n");

				input = Console.ReadLine();
				if (input.Trim().Equals("0"))
				{
					break;
				}
				else if (input.Trim().Equals(""))
				{
					continue;
				}
				string[] items = input.Split('-');

				string AssignmentInput = items[0];
				correctInput = Int32.TryParse(AssignmentInput, out int AssignmentCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong assignment attribute\n");
					continue;
				}

				string CourseInput = items[1];
				correctInput = Int32.TryParse(CourseInput, out int CourseCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong course attribute\n");
					continue;
				}

				AssignmentList[AssignmentCode - 1].CourseCodes.Add(CourseCode - 1);
				CourseList[CourseCode - 1].AssignmentCodes.Add(AssignmentCode - 1);

				Console.WriteLine($"you connected assignment {AssignmentCode}: {AssignmentList[AssignmentCode - 1].Title} with course {CourseCode}: {CourseList[CourseCode - 1].Title}\n");



			} while (!input.Equals("0"));
		}

		private static void ShowAssignmentsCourses()
		{
			foreach (Course c in CourseList)
			{
				Console.WriteLine($"Course: {c.Title}");

				foreach (int assignmentCode in c.AssignmentCodes)
				{
					Console.WriteLine($"{AssignmentList[assignmentCode].Title}");
				}
				Console.WriteLine();
			}
		}

		// assignments student

		private static void ConnectAssignmentsStudents()
		{
			if (AssignmentList.Count == 0)
			{
				Console.WriteLine("assignments do not exist\n");
				return;
			}
			else if (CourseList.Count == 0)
			{
				Console.WriteLine("courses do not exist\n");
				return;
			}
			else if (StudentList.Count == 0)
			{
				Console.WriteLine("students do not exist\n");
				return;
			}

			Console.WriteLine("All assignments:\n");

			ShowAssignments(false);

			Console.WriteLine("All courses:\n");

			ShowCourses(false);

			Console.WriteLine("All students:\n");

			ShowStudents(false);

			string input;
			bool correctInput;

			do
			{
				Console.WriteLine("type the number of assignment,course and student you want to connect, using -");
				Console.WriteLine("for example type '1-2-3' to connect the first assignment with the second course for the third student");
				Console.WriteLine("student and assignment must already be connected to course");
				Console.WriteLine("type '0' to leave\n");

				input = Console.ReadLine();
				if (input.Trim().Equals("0"))
				{
					break;
				}
				else if (input.Trim().Equals(""))
				{
					continue;
				}
				string[] items = input.Split('-');

				string AssignmentInput = items[0];
				correctInput = Int32.TryParse(AssignmentInput, out int AssignmentCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong assignment attribute\n");
					continue;
				}

				string CourseInput = items[1];
				correctInput = Int32.TryParse(CourseInput, out int CourseCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong course attribute\n");
					continue;
				}

				string StudentInput = items[2];
				correctInput = Int32.TryParse(StudentInput, out int StudentCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong student attribute\n");
					continue;
				}

				//check codes are correct

				if (CourseCode > CourseList.Count)
				{
					Console.WriteLine("course doesn't exist");
					return;
				}
				else if (AssignmentCode > AssignmentList.Count)
				{
					Console.WriteLine("assignment doesn't exist");
					return;
				}
				else if (StudentCode > StudentList.Count)
				{
					Console.WriteLine("student doesn't exist");
					return;
				}

				
				Course c = CourseList[CourseCode - 1];
				Assignment a = AssignmentList[AssignmentCode - 1];
				Student s = StudentList[StudentCode - 1];

				//check course has assignment

				bool CourseHasAssignment = c.AssignmentCodes.Contains(AssignmentCode - 1);
				if (!CourseHasAssignment)
				{
					Console.WriteLine($"Course '{c.Title}' doesn't have assignment '{a.Title}'\n");
					continue;
				}

				//check assignment has course

				bool AssignmentHasCourse = a.CourseCodes.Contains(CourseCode - 1);
				if (!AssignmentHasCourse)
				{
					Console.WriteLine($"Assignment '{c.Title}' doesn't belong to course '{c.Title}'\n");
					continue;
				}

				//check course has student

				bool CourseHasStudent = c.StudentCodes.Contains(StudentCode - 1);
				if (!CourseHasStudent)
				{
					Console.WriteLine($"Course '{c.Title}' doesn't have student '{s.LastName}'\n");
					continue;
				}

				//check student has course

				bool StudentHasCourse = s.CourseCodes.Contains(CourseCode - 1);
				if (!StudentHasCourse)
				{
					Console.WriteLine($"Student '{s.LastName}' doesn't belong to course '{c.Title}'\n");
					continue;
				}

				//add them

				s.AssignmentCodes.Add(AssignmentCode - 1);
				a.StudentCodes.Add(StudentCode - 1);

				Console.Write($"you connected assignment {AssignmentCode}: '{a.Title}'");
				Console.WriteLine($"with course {CourseCode}: '{c.Title}'");
				Console.WriteLine($" for student {StudentCode}: '{s.FirstName} {s.LastName}'\n");


			} while (!input.Equals("0"));
		}

		private static void ShowAssignmentsStudents()
		{
			List<string> result = new List<string>();

			foreach (Student s in StudentList)
			{
				Console.WriteLine($"Student: {s.FirstName} {s.LastName}:");
				if (s.AssignmentCodes.Count < 1)
				{
					Console.WriteLine("No assignments for this student\n");
				}

				foreach (int assignmentCode in s.AssignmentCodes)
				{
					Console.WriteLine($"{AssignmentList[assignmentCode].Title}");
				}
				Console.WriteLine();
			}

		}

		// check date for submissions

		private static void CheckDateForSubmissions()
		{
			DayOfWeek[] days = {
				DayOfWeek.Monday,
				DayOfWeek.Tuesday,
				DayOfWeek.Wednesday,
				DayOfWeek.Thursday,
				DayOfWeek.Friday,
				DayOfWeek.Saturday,
				DayOfWeek.Sunday
			};

			

			bool goodDate;
			DateTime date;
			string dateInput;

			do
			{
				Console.WriteLine("input a date to check for submissions (day/month/year)");
				Console.WriteLine("to quit type '0'\n");

				dateInput = Console.ReadLine();
				if (dateInput.Equals("0"))
				{
					return;
				}


				goodDate = DateTime.TryParse(dateInput, out date);

				//dayDistance is the index of the day in the week
				//it actualy represents the distance of the input date from the closest previous Monday
				int dayDistance = Array.IndexOf(days, date.DayOfWeek);

				DateTime startDate = date.AddDays(-dayDistance);
				
				DateTime endDate = startDate.AddDays(4);

				Console.WriteLine($"Searching from {startDate} until {endDate}\n");

				List<string> result = new List<string>();

				foreach (Student s in StudentList)
				{
					foreach (int i in s.AssignmentCodes)
					{
						Assignment a = AssignmentList[i];
						DateTime submissionDate = a.SubmissionDateAndTime;
						if (submissionDate >= startDate && submissionDate <= endDate)
						{
							result.Add($"'{s.FirstName} {s.LastName}' has '{a.Title}' due {submissionDate}");
						}
					}
				}
				if (result.Count < 1)
				{
					Console.WriteLine("No students found for these criteria\n");
				}
				foreach (string line in result)
				{
					Console.WriteLine(line);
				}

				Console.WriteLine();

			} while (!dateInput.Equals("0"));

			
		}
	}
}
