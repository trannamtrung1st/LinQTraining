using LinQTraining.EFCoreAdvanced;
using Microsoft.EntityFrameworkCore;

namespace LinQTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            //CustomDataSource.CustomDataSourceProgram.RunEnumerable();
            //CustomDataSource.CustomDataSourceProgram.RunQueryable();
            //CustomDataSource.CustomDataSourceProgram.BasicOperations();
            //CustomDataSource.CustomDataSourceProgram.RunQueryableConvertedToEnumerable();
            //GettingStarted.ExecutionProgram.Run();
            //GettingStarted.GettingStartedProgram.Run();
            //MannerOfExecution.Run();
            //MoreOperators.Run();
            //LinQToObjectsExtensions.Run();
            //LinQToXML.Run();
            //LinQInEFCore.Run();
            //SameQueryForMultiProvidersProgram.Run();
            EFCoreAdvancedProgram.Run();
        }

        public static void Init()
        {
            using var context = new LinQContext();
            context.Database.Migrate();
        }
    }
}
