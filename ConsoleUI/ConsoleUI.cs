using System;
using System.Collections.Generic;
using School;
using System.IO;

namespace Common
{
	public class ConsoleUI
	{
		public void StartConsoleUI()
		{
			ShowGreetMessage();

			MenuOptions MainOption;

			do
			{
				ShowMenu();
				MainOption = (MenuOptions)GetNumericInput(0,10, "Only numbers between from 0 to 10 allowed!!!", "Type a number from the menu");

				DoMainAction(MainOption);

			} while (MenuOptions.Exit != MainOption);
		}

		private void ShowGreetMessage()
		{
			Console.WriteLine(SchoolManager.GetGreetMessage());
		}

		private void ShowMenu()
		{
			List<string> menu = SchoolManager.GetMainMenu();

			Console.WriteLine("MAIN MENU");

			foreach(string option in menu)
			{
				Console.WriteLine(option);
			}
			Console.WriteLine();
		}

		private int GetNumericInput(int min, int max, string errorMessage = "", string prompt = "")
		{
			Console.Write($"{prompt}\n");

			string UserInput;
			bool correct = false;
			int IntValue = 0;

			while (!correct || IntValue < min || IntValue > max)
			{
				UserInput = Console.ReadLine();
				correct = int.TryParse(UserInput, out IntValue);

				if (!correct || IntValue < min || IntValue > max)
				{
					Console.Write($"{errorMessage}\n");
				}
			}

			return IntValue;
		}

		private bool ChooseInputType(string element)
		{
			bool option;
			string input;
			do
			{
				Console.WriteLine($"type 'm' to enter {element} manualy, or 'a' to get a default list");

				input = Console.ReadLine();

				option = input.Equals("A") || input.Equals("a") || input.Equals("m") || input.Equals("M");

			} while (!input.Equals("m") && !input.Equals("a") && !input.Equals("A") && !input.Equals("M"));

			Console.WriteLine();
			return option;
		}

		private void InsertStudents()
		{
			bool AutoOrManualInput = ChooseInputType("students");
			List<string> result = null;

			try
			{
				result = StudentManager.InputStudents(AutoOrManualInput);
			}
			catch (FileNotFoundException f)
			{
				Console.WriteLine(f.Message);
			}
			catch(DirectoryNotFoundException d)
			{
				Console.WriteLine(d.Message);
			}

			Console.Clear();
			Console.WriteLine($"Import finished with {result.Count - 1} errors\n");

			foreach (string line in result)
			{
				Console.WriteLine(line);
			}

			Console.WriteLine();
			Console.ReadKey();
			Console.Clear();
			
		}

		private void DoMainAction(MenuOptions MainOption)
		{
			bool AutoOrManualInput;

			switch (MainOption)
			{
				case MenuOptions.InputStudents:
					InsertStudents();
					break;
				case MenuOptions.ShowStudents:
					StudentManager.ShowStudents(true);
					Console.ReadKey();
					break;
				case MenuOptions.InputTrainers:
					AutoOrManualInput = ChooseInputType("trainers");
					TrainerManager.InputTrainers(AutoOrManualInput);
					break;
				case MenuOptions.ShowTrainers:
					TrainerManager.ShowTrainers(true);
					Console.ReadKey();
					break;
				case MenuOptions.InputAssignments:
					AutoOrManualInput = ChooseInputType("assignments");
					AssignmentManager.InputAssignments(AutoOrManualInput);
					break;
				case MenuOptions.ShowAssignments:
					AssignmentManager.ShowAssignments(true);
					Console.ReadKey();
					break;
				case MenuOptions.InputCourses:
					AutoOrManualInput = ChooseInputType("corses");
					CourseManager.InputCourses(AutoOrManualInput);
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
