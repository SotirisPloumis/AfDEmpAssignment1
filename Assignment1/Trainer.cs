using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
	class Trainer
	{
		//private string _subject;
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Subject { get; set; }

		public List<int> CourseCodes { get; set; }

		public Trainer()
		{
			CourseCodes = new List<int>();
		}
	}
}
