namespace ADTTest
{
    internal static class Program
    {
        //dotnet ef dbcontext scaffold "Server=localhost;Database=Multi;User Id=sa;Password=AntoniLyubo;encrypt=false;" Microsoft.EntityFrameworkCore.SqlServer -o Micro
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Reset());
        }
    }
}