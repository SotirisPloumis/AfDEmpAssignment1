using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Assignment1
{
	class TrainerManager
	{
		public static void InputTrainers()
		{
			string option = SchoolUI.ManualOrAuto("trainers");

			if (option.Equals("a") || option.Equals("A"))
			{
				AutoFillTrainers();
				Console.ReadKey();
			}
			else
			{
				ManualFillTrainers();
				Console.WriteLine();
			}
		}

		public static void ShowTrainers(bool inFull)
		{
			if (SchoolUI.TrainerList.Count < 1)
			{
				Console.WriteLine("No trainers yet\n");
				return;
			}

			Console.WriteLine("TRAINERS");

			foreach (Trainer t in SchoolUI.TrainerList)
			{
				if (inFull)
				{
					Console.WriteLine($"{SchoolUI.TrainerList.IndexOf(t) + 1}: {t.FirstName} {t.LastName}, teaches {t.Subject}");
				}
				else
				{
					Console.WriteLine($"{SchoolUI.TrainerList.IndexOf(t) + 1}: {t.FirstName} {t.LastName}");
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

				SchoolUI.TrainerList.Add(t);

			}
			if (SchoolUI.TrainerList.Count < 1)
			{
				Console.WriteLine("Couldn't auto save any trainers");
			}
			else
			{
				Console.WriteLine($"Successfully saved {SchoolUI.TrainerList.Count} trainers");
			}
			Console.WriteLine();
		}

		private static void ManualFillTrainers()
		{
			Trainer t;
			while (true)
			{
				Console.WriteLine("type a new trainer");
				Console.WriteLine("firstName - lastName - Subject");
				Console.WriteLine("to quit type 'exit' or '0' and hit Enter");

				string input = Console.ReadLine();

				if (input.Trim().Equals("exit") || input.Trim().Equals("0"))
				{
					break;
				}

				string[] items = input.Split('-');

				if (items.Length < 3)
				{
					Console.WriteLine("An argument is missing\n");
					continue;
				}


				string fname = items[0].Trim();
				string lname = items[1].Trim();
				string subject = items[2].Trim();

				t = new Trainer()
				{
					FirstName = fname,
					LastName = lname,
					Subject = subject
				};

				SchoolUI.TrainerList.Add(t);

				Console.WriteLine($"Trainer {t.FirstName} {t.LastName} saved\n");
			}
		}
	}
}
