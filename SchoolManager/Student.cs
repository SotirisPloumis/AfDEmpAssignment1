﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School
{
	public class Student
	{
		//private DateTime dob;

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime DateOfBirth { get; set; }

		public decimal TuitionFess { get; set; }

		public List<int> CourseCodes { get; set; }

		public List<int> AssignmentCodes { get; set; }

		public Student()
		{
			CourseCodes = new List<int>();
			AssignmentCodes = new List<int>();
		}
	}
}
