using Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        public OperationResult Create(CreateProduct dataEntry);
        public OperationResult Edit(EditProduct dataEntry);
        public ProductViewModel Get(long id);
        public EditProduct EditGet(long id);
        public OperationResult Delete(long id);
        public List<ProductMinimalViewModel> List();
        public List<ProductMinimalViewModel> Search(ProductSearchModel query);
    }
}
