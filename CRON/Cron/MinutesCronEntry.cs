namespace CronClass
{
	public class MinutesCronEntry : CronEntryBase
	{
		public MinutesCronEntry(string expression)
		{
			Initialize(expression, 0, 59);
		}
	}
}