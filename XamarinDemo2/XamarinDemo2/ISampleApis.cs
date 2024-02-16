using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace XamarinDemo2
{
	public interface ISampleApis
	{
		[Get("/futurama/info")]
		Task<List<Show>> GetFuturamaInfo();
	}

	public class Show
	{
		public string synopsis { get; set; }
		public string yearsAired { get; set; }
		public List<Creator> creators { get; set; }
		public int id { get; set; }
	}

	public class Creator
	{
		public string name { get; set; }
		public string url { get; set; }
	}
}

