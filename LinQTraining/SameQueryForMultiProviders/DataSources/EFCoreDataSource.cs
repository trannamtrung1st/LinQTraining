using LinQTraining.LinqExtensions;

namespace LinQTraining.SameQueryForMultiProviders.DataSources
{
    public static class EFCoreDataSource
    {
        public static LinQContext Context => new LinQContext();
    }
}
