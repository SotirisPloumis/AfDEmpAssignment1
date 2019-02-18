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
			MenuOptions UserOption;

			do
			{
				choice = SchoolUI.ShowMenuAndChoose();
				UserOption = (MenuOptions)choice;

				switch (UserOption)
				{
					case MenuOptions.InputStudents:
						SchoolUI.InputStudents();
						break;
					case MenuOptions.ShowStudents:
						SchoolUI.ShowStudents(true);
						break;
					case MenuOptions.InputTrainers:
						SchoolUI.InputTrainers();
						break;
					case MenuOptions.ShowTrainers:
						SchoolUI.ShowTrainers(true);
						break;
					case MenuOptions.InputAssignments:
						SchoolUI.InputAssignments();
						break;
					case MenuOptions.ShowAssignments:
						SchoolUI.ShowAssignments(true);
						break;
					case MenuOptions.InputCourses:
						SchoolUI.InputCourses();
						break;
					case MenuOptions.ShowCourses:
						SchoolUI.ShowCourses(true);
						break;
					case MenuOptions.ConnectStudentCourse:
						SchoolUI.ShowStudentCourses();
						break;
					default:
						break;
				}

			} while (MenuOptions.Exit != UserOption);
		}

	}
}
