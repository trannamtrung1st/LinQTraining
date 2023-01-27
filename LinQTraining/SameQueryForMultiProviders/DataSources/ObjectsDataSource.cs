namespace LinQTraining.SameQueryForMultiProviders.DataSources
{
    public static class ObjectsDataSource
    {
        private static readonly IEnumerable<Product> _seedProducts = Data.Products;
        private static readonly IEnumerable<ProductCategory> _seedCategories = Data.Categories;

        private static readonly IEnumerable<ProductCategory> _categories = Data.Categories
            .Select(c =>
            {
                c.Products = _seedProducts.Where(p => p.CategoryId == c.Id).ToArray();
                return c;
            }).ToArray();
        public static IEnumerable<ProductCategory> Categories => _categories;


        private static readonly IEnumerable<Product> _products = Data.Products
            .Select(p =>
            {
                p.Category = _seedCategories.FirstOrDefault(c => p.CategoryId == c.Id);
                return p;
            }).ToArray();
        public static IEnumerable<Product> Products => _products;
    }
}
