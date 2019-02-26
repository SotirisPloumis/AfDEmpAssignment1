using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
	public static class ConsoleUI
	{
		public static void StartConsoleUI()
		{
			ConsoleUI.ShowGreetMessage();

			MenuOptions MainOption;

			do
			{
				ShowMenu();
				MainOption = (MenuOptions)GetNumericInput();

				DoMainAction(MainOption);

			} while (MenuOptions.Exit != MainOption);
		}

		private static void ShowGreetMessage()
		{
			Console.WriteLine(SchoolManager.GreetMessage());
		}

		static void ShowMenu()
		{
			List<string> menu = SchoolManager.GetMainMenu();

			foreach(string option in menu)
			{
				Console.WriteLine(option);
			}
		}

		static int GetNumericInput(string message = "")
		{
			Console.Write($"{message}\n");

			string UserInput;
			bool correct;
			int IntValue;

			do
			{
				UserInput = Console.ReadLine();
				correct = int.TryParse(UserInput, out IntValue);
			} while (correct == false);

			return IntValue;
		}

		static string ManualOrAuto(string element)
		{
			string option;
			do
			{
				Console.WriteLine($"type 'm' to enter {element} manualy, or 'a' to get a default list");

				option = Console.ReadLine();

			} while (!option.Equals("m") && !option.Equals("a") && !option.Equals("A") && !option.Equals("M"));

			Console.WriteLine();
			return option;
		}

		static void DoMainAction(MenuOptions MainOption)
		{
			string option;

			switch (MainOption)
			{
				case MenuOptions.InputStudents:
					//get user choice for auto or manual input of students
					option = ManualOrAuto("students");
					StudentManager.InputStudents(option);
					break;
				case MenuOptions.ShowStudents:
					StudentManager.ShowStudents(true);
					Console.ReadKey();
					break;
				case MenuOptions.InputTrainers:
					option = ManualOrAuto("trainers");
					TrainerManager.InputTrainers(option);
					break;
				case MenuOptions.ShowTrainers:
					TrainerManager.ShowTrainers(true);
					Console.ReadKey();
					break;
				case MenuOptions.InputAssignments:
					option = ManualOrAuto("assignments");
					AssignmentManager.InputAssignments(option);
					break;
				case MenuOptions.ShowAssignments:
					AssignmentManager.ShowAssignments(true);
					Console.ReadKey();
					break;
				case MenuOptions.InputCourses:
					option = ManualOrAuto("corses");
					CourseManager.InputCourses(option);
					break;
				case MenuOptions.ShowCourses:
					CourseManager.ShowCourses(true);
					Console.ReadKey();
					break;
				case MenuOptions.ManageConnections:
					int ConnectChoice;
					ConnectionMenuOptions connectOption;

					do
					{
						ConnectChoice = SchoolManager.ShowConnectionsMenuAndChoose();

						connectOption = (ConnectionMenuOptions)ConnectChoice;

						SchoolManager.DoConnectionAction(connectOption);

					} while (connectOption != ConnectionMenuOptions.Exit);

					break;
				case MenuOptions.CheckDateForSubmissions:
					SchoolManager.CheckDateForSubmissions();
					break;
				default:
					break;
			}
		}
	}
}
