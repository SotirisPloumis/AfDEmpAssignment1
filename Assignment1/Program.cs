using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
	class Program
	{
		static void Main(string[] args)
		{
			SchoolUI.Greet();

			int choice;
			MenuOptions MainOption;
			

			do
			{
				choice = SchoolUI.ShowMenuAndChoose();
				MainOption = (MenuOptions)choice;

				SchoolUI.DoMainAction(MainOption);

			} while (MenuOptions.Exit != MainOption);
		}

	}
}
