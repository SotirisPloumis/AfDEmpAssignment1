using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
	class Student
	{
		//private DateTime dob;

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime DateOfBirth { get; set; }

		public double TuitionFess { get; set; }

		public List<int> CourseCodes { get; set; }

		public List<int> AssignmentCodes { get; set; }

		public Student()
		{
			CourseCodes = new List<int>();
			AssignmentCodes = new List<int>();
		}
	}
}
