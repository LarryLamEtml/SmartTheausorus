using System;

namespace CronClass
{
	public class CronEntryException : Exception
	{
		public CronEntryException(string message)
			: base(message)
		{

		}
	}
}