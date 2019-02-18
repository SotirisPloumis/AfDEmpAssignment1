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


			//step 1: save students

			option = SchoolUI.ManualOrAuto("students");

			List<Student> StudentList = new List<Student>();

			if (option.Equals("a")|| option.Equals("A"))
			{
				SchoolUI.AutoFillStudents(StudentList);
			}
			else
			{
				SchoolUI.ManualFillStudents(StudentList);
			}

			foreach(Student s in StudentList)
			{
				Console.WriteLine($"{s.FirstName} {s.LastName}, born in {s.DateOfBirth}, pays: {s.TuitionFess}");
			}
			Console.WriteLine("\n");

			//step 2: save trainers
			option = SchoolUI.ManualOrAuto("trainers");

			List<Trainer> TrainerList = new List<Trainer>();

			if (option.Equals("a") || option.Equals("A"))
			{
				SchoolUI.AutoFillTrainers(TrainerList);
			}
			else
			{
				SchoolUI.ManualFillTrainers(TrainerList);
			}

			foreach (Trainer t in TrainerList)
			{
				Console.WriteLine($"{t.FirstName} {t.LastName}, teaches {t.Subject}");
			}
			Console.WriteLine("\n");

			//step 3: save assignments
			option = SchoolUI.ManualOrAuto("assignments");
			
			List<Assignment> AssignmentList = new List<Assignment>();

			if (option.Equals("a") || option.Equals("A"))
			{
				SchoolUI.AutoFillAssignments(AssignmentList);
			}
			else
			{
				SchoolUI.ManualFillAssignments(AssignmentList);
			}

			foreach (Assignment a in AssignmentList)
			{
				Console.WriteLine($"Title: {a.Title}, Desciption: {a.Description}, due {a.SubmissionDateAndTime}");
			}

			Console.WriteLine("\n");

			//step 4: save courses
			option = SchoolUI.ManualOrAuto("courses");

			List<Course> CourseList = new List<Course>();

			if (option.Equals("a") || option.Equals("A"))
			{
				SchoolUI.AutoFillCourses(CourseList);
			}
			else
			{
				SchoolUI.ManualFillCourses(CourseList);
			}

			foreach (Course c in CourseList)
			{
				Console.WriteLine($"Title: {c.Title}, Stream: {c.Stream}, type: {c.Type}, starts {c.StartDate}, ends {c.EndDate}");
			}

			Console.WriteLine("\n");

		}

	}
}
