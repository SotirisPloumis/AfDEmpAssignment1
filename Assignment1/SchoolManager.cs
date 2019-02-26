using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
	static class SchoolManager
	{
		public static List<Student> StudentList = new List<Student>();
		public static List<Trainer> TrainerList = new List<Trainer>();
		public static List<Assignment> AssignmentList = new List<Assignment>();
		public static List<Course> CourseList = new List<Course>();

		public static string GreetMessage()
		{
			string Line1 = "Welcome to School Manager 2019\n";
			string Line2 = "Created by Sotiris Ploumis\n";
			string Line3 = "Assignment 1 of the C# AfDEmp Bootcamp \n";

			StringBuilder s = new StringBuilder();
			s.Append(Line1);
			s.Append(Line2);
			s.Append(Line3);

			string message = s.ToString();

			return message;
		}

		public static List<string> GetMainMenu()
		{
			List<string> menu = new List<string>
			{
				"1. Input students",
				"2. Show all students",
				"3. Input trainers",
				"4. Show all trainers",
				"5. Input assignments",
				"6. Show all assignments",
				"7. Input courses",
				"8. Show all courses",
				"9. Manage connections",
				"10. Check date for submissions",
				"0. Exit"
			};

			return menu;
		}

		//public static int ShowMenuAndChoose()
		//{
		//	Console.WriteLine("MAIN OPTIONS");

		//	Console.WriteLine("1. Input students");
		//	Console.WriteLine("2. Show all students");

		//	Console.WriteLine("3. Input trainers");
		//	Console.WriteLine("4. Show all trainers");

		//	Console.WriteLine("5. Input assignments");
		//	Console.WriteLine("6. Show all assignments");

		//	Console.WriteLine("7. Input courses");
		//	Console.WriteLine("8. Show all courses");

		//	Console.WriteLine("9. Manage connections");

		//	Console.WriteLine("10. Check date for submissions");

		//	Console.WriteLine("0. Exit");

		//	bool goodChoice;
		//	int choice;
		//	do
		//	{
		//		string input = Console.ReadLine();
		//		goodChoice = Int32.TryParse(input, out choice);
		//	} while (!goodChoice || choice < 0 || choice > 10);

		//	Console.WriteLine();
		//	return choice;
		//}

		//public static string ManualOrAuto(string element)
		//{
		//	string option;
		//	do
		//	{
		//		Console.WriteLine($"type 'm' to enter {element} manualy, or 'a' to get a default list");

		//		option = Console.ReadLine();

		//	} while (!option.Equals("m") && !option.Equals("a") && !option.Equals("A") && !option.Equals("M"));

		//	Console.WriteLine();
		//	return option;
		//}

		//public static void DoMainAction(MenuOptions MainOption)
		//{
		//	switch (MainOption)
		//	{
		//		case MenuOptions.InputStudents:
		//			StudentManager.InputStudents();
		//			break;
		//		case MenuOptions.ShowStudents:
		//			StudentManager.ShowStudents(true);
		//			Console.ReadKey();
		//			break;
		//		case MenuOptions.InputTrainers:
		//			TrainerManager.InputTrainers();
		//			break;
		//		case MenuOptions.ShowTrainers:
		//			TrainerManager.ShowTrainers(true);
		//			Console.ReadKey();
		//			break;
		//		case MenuOptions.InputAssignments:
		//			AssignmentManager.InputAssignments();
		//			break;
		//		case MenuOptions.ShowAssignments:
		//			AssignmentManager.ShowAssignments(true);
		//			Console.ReadKey();
		//			break;
		//		case MenuOptions.InputCourses:
		//			CourseManager.InputCourses();
		//			break;
		//		case MenuOptions.ShowCourses:
		//			CourseManager.ShowCourses(true);
		//			Console.ReadKey();
		//			break;
		//		case MenuOptions.ManageConnections:
		//			int ConnectChoice;
		//			ConnectionMenuOptions connectOption;

		//			do
		//			{
		//				ConnectChoice = ShowConnectionsMenuAndChoose();

		//				connectOption = (ConnectionMenuOptions)ConnectChoice;

		//				DoConnectionAction(connectOption);

		//			} while (connectOption != ConnectionMenuOptions.Exit);

		//			break;
		//		case MenuOptions.CheckDateForSubmissions:
		//			CheckDateForSubmissions();
		//			break;
		//		default:
		//			break;
		//	}

			
		//}

		//connections

		public static int ShowConnectionsMenuAndChoose()
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

		public static void DoConnectionAction(ConnectionMenuOptions ConnectionOption)
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
			if (StudentList.Count == 0)
			{
				Console.WriteLine("students do not exist\n");
				return;
			}
			else if (CourseList.Count == 0)
			{
				Console.WriteLine("courses do not exist\n");
				return;
			}

			StudentManager.ShowStudents(false);

			CourseManager.ShowCourses(false);

			string input;
			bool correctInput;

			while (true)
			{
				Console.WriteLine("type the number of student and course you want to connect, using -");
				Console.WriteLine("for example, type '1-2' to connect the first student with the second course");
				Console.WriteLine("type 'exit' or '0' to leave\n");

				input = Console.ReadLine();
				if (input.Trim().Equals("0") || input.Trim().Equals("exit"))
				{
					break;
				}

				string[] items = input.Split('-');
				if (items.Length < 2)
				{
					Console.WriteLine("argument is missing\n");
					continue;
				}
				else if (items.Length > 2)
				{
					Console.WriteLine("too many arguments\n");
					continue;
				}

				correctInput = Int32.TryParse(items[0].Trim(), out int StudentCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong student attribute\n");
					continue;
				}

				correctInput = Int32.TryParse(items[1].Trim(), out int CourseCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong course attribute\n");
					continue;
				}

				if (StudentCode < 1 || StudentCode > StudentList.Count)
				{
					Console.WriteLine("student number is not valid\n");
					continue;
				}
				if (CourseCode < 1 || CourseCode > CourseList.Count)
				{
					Console.WriteLine("course number is not valid\n");
					continue;
				}

				Student s = StudentList[StudentCode - 1];
				Course c = CourseList[CourseCode - 1];

				if (s.CourseCodes.Contains(CourseCode-1)&&c.StudentCodes.Contains(StudentCode-1))
				{
					Console.WriteLine("this relation already exists\n");
					continue;
				}

				s.CourseCodes.Add(CourseCode - 1);
				c.StudentCodes.Add(StudentCode - 1);

				Console.WriteLine($"you connected student {StudentCode}: {s.FirstName} {s.LastName} with course {CourseCode}: {c.Title}\n");

			}

		}

		private static void ShowStudentCourses()
		{
			if (CourseList.Count < 1)
			{
				Console.WriteLine("No courses yet\n");
				return;
			}
			foreach (Course c in CourseList)
			{
				Console.WriteLine($"Course: {c.Title}");
				if (c.StudentCodes.Count < 1)
				{
					Console.WriteLine("No students for this coourse\n");
					continue;
				}

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
			if (TrainerList.Count == 0)
			{
				Console.WriteLine("trainers do not exist\n");
				return;
			}
			else if (CourseList.Count == 0)
			{
				Console.WriteLine("courses do not exist\n");
				return;
			}

			TrainerManager.ShowTrainers(false);

			CourseManager.ShowCourses(false);

			string input;
			bool correctInput;

			while (true)
			{
				Console.WriteLine("type the number of trainer and course you want to connect, using -");
				Console.WriteLine("for example type '1-2' to connect the first trainer with the second course");
				Console.WriteLine("type '0' or 'exit' to leave\n");

				input = Console.ReadLine();
				if (input.Trim().Equals("0") || input.Trim().Equals("exit"))
				{
					break;
				}

				string[] items = input.Split('-');
				if (items.Length < 2)
				{
					Console.WriteLine("arguments missing\n");
				}
				else if (items.Length > 2)
				{
					Console.WriteLine("too many arguments\n");
					continue;
				}

				correctInput = Int32.TryParse(items[0].Trim(), out int TrainerCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong trainer attribute\n");
					continue;
				}

				correctInput = Int32.TryParse(items[1].Trim(), out int CourseCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong course attribute\n");
					continue;
				}

				if (TrainerCode < 1 || TrainerCode > TrainerList.Count)
				{
					Console.WriteLine("trainer number is not valid\n");
					continue;
				}
				if (CourseCode < 1 || CourseCode > CourseList.Count)
				{
					Console.WriteLine("course number is not valid\n");
					continue;
				}

				Trainer t = TrainerList[TrainerCode - 1];
				Course c = CourseList[CourseCode - 1];

				if (t.CourseCodes.Contains(CourseCode-1) && c.TrainerCodes.Contains(TrainerCode-1))
				{
					Console.WriteLine("this relation already exists\n");
					continue;
				}

				t.CourseCodes.Add(CourseCode - 1);
				c.TrainerCodes.Add(TrainerCode - 1);

				Console.WriteLine($"you connected trainer {TrainerCode}: {t.FirstName} {t.LastName} with course {CourseCode}: {c.Title}\n");

			}
		}

		private static void ShowTrainersCourses()
		{
			if (CourseList.Count < 1)
			{
				Console.WriteLine("no courses yet\n");
				return;
			}
			foreach (Course c in CourseList)
			{
				Console.WriteLine($"Course: {c.Title}");
				if (c.TrainerCodes.Count < 1)
				{
					Console.WriteLine("not trainers for this course\n");
					continue;
				}

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

			AssignmentManager.ShowAssignments(false);

			CourseManager.ShowCourses(false);

			string input;
			bool correctInput;

			while(true)
			{
				Console.WriteLine("type the number of assignment and course you want to connect, using -");
				Console.WriteLine("for example type '1-2' to connect the first assignment with the second course");
				Console.WriteLine("type '0' or 'exit' to leave\n");

				input = Console.ReadLine();
				if (input.Trim().Equals("0") || input.Trim().Equals("exit"))
				{
					break;
				}
				
				string[] items = input.Split('-');
				if (items.Length < 2)
				{
					Console.WriteLine("arguments missing");
					continue;
				}
				else if (items.Length > 2)
				{
					Console.WriteLine("too many arguments\n");
					continue;
				}

				correctInput = Int32.TryParse(items[0].Trim(), out int AssignmentCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong assignment attribute\n");
					continue;
				}

				correctInput = Int32.TryParse(items[1].Trim(), out int CourseCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong course attribute\n");
					continue;
				}

				if (AssignmentCode < 1 || AssignmentCode > AssignmentList.Count)
				{
					Console.WriteLine("assignment number is not valid");
					continue;
				}
				if (CourseCode < 1 || CourseCode > CourseList.Count)
				{
					Console.WriteLine("course number is not valid");
					continue;
				}

				Assignment a = AssignmentList[AssignmentCode - 1];
				Course c = CourseList[CourseCode - 1];

				if (a.CourseCodes.Contains(CourseCode-1) && c.AssignmentCodes.Contains(AssignmentCode-1))
				{
					Console.WriteLine("this relation already exists");
					continue;
				}

				a.CourseCodes.Add(CourseCode - 1);
				c.AssignmentCodes.Add(AssignmentCode - 1);

				Console.WriteLine($"you connected assignment {AssignmentCode}: {a.Title} with course {CourseCode}: {c.Title}\n");

			}
		}

		private static void ShowAssignmentsCourses()
		{
			if (CourseList.Count < 1)
			{
				Console.WriteLine("no courses yet");
				return;
			}
			foreach (Course c in CourseList)
			{
				Console.WriteLine($"Course: {c.Title}");
				if (c.AssignmentCodes.Count < 1)
				{
					Console.WriteLine("no assignments for this course\n");
					continue;
				}

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

			AssignmentManager.ShowAssignments(false);

			CourseManager.ShowCourses(false);

			StudentManager.ShowStudents(false);

			string input;
			bool correctInput;

			while (true)
			{
				Console.WriteLine("type the number of assignment,course and student you want to connect, using -");
				Console.WriteLine("for example type '1-2-3' to connect the first assignment with the second course for the third student");
				Console.WriteLine("student and assignment must already be connected to course");
				Console.WriteLine("type '0' or 'exit' to leave\n");

				input = Console.ReadLine();
				if (input.Trim().Equals("0") || input.Trim().Equals("exit"))
				{
					break;
				}
				
				string[] items = input.Split('-');
				if (items.Length < 3)
				{
					Console.WriteLine("arguments missing\n");
					continue;
				}
				else if (items.Length > 3)
				{
					Console.WriteLine("too many arguments\n");
					continue;
				}

				correctInput = Int32.TryParse(items[0].Trim(), out int AssignmentCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong assignment attribute\n");
					continue;
				}

				correctInput = Int32.TryParse(items[1].Trim(), out int CourseCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong course attribute\n");
					continue;
				}

				correctInput = Int32.TryParse(items[2].Trim(), out int StudentCode);
				if (!correctInput)
				{
					Console.WriteLine("wrong student attribute\n");
					continue;
				}

				//check codes are correct

				if (CourseCode > CourseList.Count || CourseCode < 1)
				{
					Console.WriteLine("course number is not valid\n");
					continue;
				}
				else if (AssignmentCode > AssignmentList.Count || AssignmentCode < 1)
				{
					Console.WriteLine("assignment number is not valid\n");
					continue;
				}
				else if (StudentCode > StudentList.Count || StudentCode < 1)
				{
					Console.WriteLine("student number is not valid\n");
					continue;
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

				if (s.AssignmentCodes.Contains(AssignmentCode-1) && a.StudentCodes.Contains(StudentCode-1))
				{
					Console.WriteLine("this relation already exists\n");
					continue;
				}

				s.AssignmentCodes.Add(AssignmentCode - 1);
				a.StudentCodes.Add(StudentCode - 1);

				Console.Write($"you connected assignment {AssignmentCode}: '{a.Title}'");
				Console.WriteLine($"with course {CourseCode}: '{c.Title}' ");
				Console.WriteLine($"for student {StudentCode}: '{s.FirstName} {s.LastName}'\n");


			}
		}

		private static void ShowAssignmentsStudents()
		{
			List<string> result = new List<string>();

			if (StudentList.Count < 1)
			{
				Console.WriteLine("no students yet");
				return;
			}

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

		public static void CheckDateForSubmissions()
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

			while (true)
			{
				Console.WriteLine("input a date to check for submissions (day/month/year)");
				Console.WriteLine("to quit type '0' or 'exit'\n");

				dateInput = Console.ReadLine().Trim();

				if (dateInput.Equals(String.Empty))
				{
					continue;
				}

				if (dateInput.Equals("0") || dateInput.Equals("exit"))
				{
					return;
				}


				goodDate = DateTime.TryParse(dateInput, out date);
				if (!goodDate)
				{
					Console.WriteLine("bad date input\n");
					continue;
				}

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

			}

			
		}
	}
}
