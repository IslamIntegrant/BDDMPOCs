using DM_AP_POC.Data;
using System;
using System.Diagnostics;

namespace TestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Program program = new Program();

			SharedData sharedData = new SharedData();
			var startTimeSpan = TimeSpan.Zero;
			var periodTimeSpan = TimeSpan.FromSeconds(5);
			var timeOutTimeSpan = sharedData.waitForProjectStatusChange;
			int projectStatus = 0;
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			do
			{
				projectStatus = program.getAlignmentProjectStatus();
				if (projectStatus == 5)
				{
					Console.WriteLine("Alignment project status key is: " + projectStatus.ToString());
					Console.ReadLine();
					break;					
				}
			} while (stopWatch.Elapsed <= sharedData.waitForProjectStatusChange);

			stopWatch.Stop();
			if (projectStatus != 5)
			{
				Console.Write("Database timeout");
				Console.ReadLine();
			}
		}

		private int getAlignmentProjectStatus() 
		{
			DBData data = new DBData();
			int statusKey = data.GetAlignmentProjectStatus(string.Empty);
			return statusKey;
		}
	}
}
