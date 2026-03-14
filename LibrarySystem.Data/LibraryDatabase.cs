namespace LibrarySystem.Data;

public static class LibraryDatabase
{
    private const string DatabaseFileName = "library.db";
    private const string DataProjectDirectoryName = "LibrarySystem.Data";
    private const string DatabaseDirectoryName = "db";

    public static string GetDatabasePath(string startDirectory)
    {
        var directory = new DirectoryInfo(Path.GetFullPath(startDirectory));

        while (directory is not null)
        {
            var dataProjectPath = Path.Combine(directory.FullName, DataProjectDirectoryName, DatabaseDirectoryName, DatabaseFileName);
            if (File.Exists(dataProjectPath))
            {
                return dataProjectPath;
            }

            var localPath = Path.Combine(directory.FullName, DatabaseDirectoryName, DatabaseFileName);
            if (directory.Name == DataProjectDirectoryName && File.Exists(localPath))
            {
                return localPath;
            }

            directory = directory.Parent;
        }

        return Path.GetFullPath(Path.Combine(startDirectory, DatabaseDirectoryName, DatabaseFileName));
    }

    public static string GetConnectionString(string startDirectory)
    {
        return $"Data Source={GetDatabasePath(startDirectory)}";
    }
}
