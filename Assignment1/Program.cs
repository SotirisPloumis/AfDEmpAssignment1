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

			string option;


			//step 1: students

			option = SchoolUI.ManualOrAuto("students");

			List<Student> StudentList = new List<Student>();

			if (option.Equals("a"))
			{
				StudentList = SchoolUI.GetInput("students");
			}

			foreach(Student s in StudentList)
			{
				Console.WriteLine($"{s.FirstName} {s.LastName}, born in {s.DateOfBirth}, pays: {s.TuitionFess}");
			}


		}

	}
}
