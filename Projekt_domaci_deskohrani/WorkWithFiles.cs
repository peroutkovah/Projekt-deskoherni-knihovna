using System.IO;



namespace HomeGaming
{
	public static class WorkWithFiles
	{
        public static void CreateDirectory(string directoryPath)
		{
            if (!Directory.Exists(directoryPath))
			{
                Directory.CreateDirectory(directoryPath);
            }
		}

    }
}
