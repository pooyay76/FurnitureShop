using Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        public OperationResult Edit(EditProductCategory editProductCategory);
        public OperationResult Create(CreateProductCategory createProductCategory);
        public List<ProductCategoryMinimalViewModel> List();
        public List<ProductCategoryMinimalViewModel> Search(SearchProductCategoy searchModel);
        public ProductCategoryViewModel Get(long id);
        public EditProductCategory EditGet(long id);
        public OperationResult Delete(long id);
    }
}
