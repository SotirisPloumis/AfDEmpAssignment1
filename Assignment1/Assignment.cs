using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
	class Assignment
	{
		public string Title{ get; set; }

		public string Description { get; set; }

		public DateTime SubmissionDateAndTime { get; set; }

		public decimal OralMark { get; set; }

		public decimal TotalMark { get; set; }

		public List<int> CourseCodes { get; set; }

		public List<int> StudentCodes { get; set; }

		public Assignment()
		{
			CourseCodes = new List<int>();
			StudentCodes = new List<int>();
		}
	}
}
