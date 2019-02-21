using System;
using System.IO;

namespace Assignment1
{
	class TrainerManager
	{
		public static void InputTrainers()
		{
			//check user choice for manual or auto input
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
			//check if trainers exist
			if (SchoolUI.TrainerList.Count < 1)
			{
				Console.WriteLine("No trainers yet\n");
				return;
			}

			Console.WriteLine("TRAINERS");

			//prnt every trainer, all details or only first and last name
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
			//currect directory
			string current = Directory.GetCurrentDirectory();

			//the path to the file with the trainers for auto import
			string path = Path.Combine(current, @"..\..\Data\autotrainers.txt");

			//read the lines of the file
			string[] allTrainers = new string[0];
			try
			{
				allTrainers = File.ReadAllLines(path);
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("auto trainers file not found\n");
				return;
			}
			//get the size of trainers list, we need for later
			int sizeBefore = SchoolUI.TrainerList.Count;

			//for every trainer
			foreach (string line in allTrainers)
			{
				//split the line and save the parts in an array
				string[] items = line.Split('-');

				//check if arguments are missing
				if (items.Length < 3)
				{
					Console.WriteLine("arguments are missing\n");
					continue;
				}

				string fname = items[0].Trim();
				string lname = items[1].Trim();
				string subject = items[2].Trim();

				//check if trainer exists already
				if (TrainerExists(fname,lname))
				{
					Console.WriteLine($"{fname} {lname} exists already\n");
					continue;
				}

				//create a new trainer
				Trainer t = new Trainer()
				{

					FirstName = fname,
					LastName = lname,
					Subject = subject
				};

				//add it to the list
				SchoolUI.TrainerList.Add(t);

			}
			//check if we added any trainers
			if (SchoolUI.TrainerList.Count == sizeBefore)
			{
				Console.WriteLine("Couldn't auto save any trainers from the file");
			}
			else
			{
				Console.WriteLine($"Successfully saved {SchoolUI.TrainerList.Count - sizeBefore} new trainers");
			}
			Console.WriteLine();
		}

		private static void ManualFillTrainers()
		{
			while (true)
			{
				Console.WriteLine("type a new trainer");
				Console.WriteLine("firstName - lastName - Subject");
				Console.WriteLine("to quit type 'exit' or '0' and hit Enter");

				//get user input for new trainer
				string input = Console.ReadLine();

				if (input.Trim().Equals("exit") || input.Trim().Equals("0"))
				{
					break;
				}

				//split the input and save it to an array
				string[] items = input.Split('-');

				//check if arguments are missing
				if (items.Length < 3)
				{
					Console.WriteLine("An argument is missing\n");
					continue;
				}


				string fname = items[0].Trim();
				string lname = items[1].Trim();
				string subject = items[2].Trim();

				if (TrainerExists(fname,lname))
				{
					Console.WriteLine($"trainer {fname} {lname} already exists\n");
					continue;
				}

				Trainer t = new Trainer()
				{
					FirstName = fname,
					LastName = lname,
					Subject = subject
				};

				SchoolUI.TrainerList.Add(t);

				Console.WriteLine($"Trainer {t.FirstName} {t.LastName} saved\n");
			}
		}

		private static bool TrainerExists(string firstname, string lastname)
		{
			//check if trainer exists for this firstname and lastname
			foreach (Trainer tr in SchoolUI.TrainerList)
			{
				if (tr.FirstName.Equals(firstname) && tr.LastName.Equals(lastname))
				{
					return true;
				}
			}
			return false;
		}
	}
}
