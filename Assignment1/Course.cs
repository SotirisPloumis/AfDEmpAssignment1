using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
	class Course
	{
		public string Title { get; set; }

		public string Stream { get; set; }

		public string Type { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public List<int> StudentCodes { get; set; }

		public Course()
		{
			StudentCodes = new List<int>();
		}
	}
}
