using AutoMapper;
using Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IFileUploader fileUploader;
        private readonly IMapper mapper;
        private const string filePath = "ProductCategory";
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IMapper mapper, IFileUploader fileUploader = null)
        {
            this.productCategoryRepository = productCategoryRepository;
            this.mapper = mapper;
            this.fileUploader = fileUploader;
        }

        public ProductCategoryViewModel Get(long id)
        {
            var entity = productCategoryRepository.GetProductCategory(id);
            if (entity == null)
                return null;
            return mapper.Map<ProductCategory, ProductCategoryViewModel>(entity);
        }
        public OperationResult Create(CreateProductCategory form)
        {
            var operationResult = new OperationResult();

            if (productCategoryRepository.Exists(x => form.Name == x.Name))
                return operationResult.Failed(ApplicationMessages.DuplicatedMessage);

            string slug = form.Name.Slugify();
            string fileName = fileUploader.Upload(form.Picture, filePath);
            var productCategory = new ProductCategory(form.Name, form.Description, fileName, form.PictureAlt, form.PictureTitle,
                form.Keywords, form.MetaDescription, slug);

            productCategoryRepository.Create(productCategory);
            return operationResult.Succeeded();
        }
        public OperationResult Edit(EditProductCategory form)
        {
            var operation = new OperationResult();

            ProductCategory entity = productCategoryRepository.GetProductCategory(form.Id);
            if (entity == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            if (productCategoryRepository.Exists(x => x.Name == form.Name && x.Id != form.Id))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);

            string slug = form.Name.Slugify();
            string fileName = fileUploader.Upload(form.Picture, filePath);
            entity.Edit(form.Name, form.Description, fileName, form.PictureAlt,
                form.PictureTitle, form.Keywords, form.MetaDescription, slug);

            productCategoryRepository.Update(entity);
            return operation.Succeeded();
        }
        public EditProductCategory EditGet(long id)
        {
            var entity = productCategoryRepository.GetProductCategory(id);
            if (entity == null)
                return null;
            return mapper.Map<ProductCategory, EditProductCategory>(entity);
        }

        public List<ProductCategoryMinimalViewModel> List()
        {
            return productCategoryRepository.GetProductCategories().Select(x => mapper.Map<ProductCategory, ProductCategoryMinimalViewModel>(x)).ToList();
        }
        public List<ProductCategoryMinimalViewModel> Search(SearchProductCategoy searchModel)
        {
            return productCategoryRepository.Search(searchModel).Select(x => mapper.Map<ProductCategory, ProductCategoryMinimalViewModel>(x)).ToList();
        }

        public OperationResult Delete(long id)
        {
            OperationResult op = new();
            var product = productCategoryRepository.Get(id);
            if (product == null)
                return op.Failed(ApplicationMessages.NotFoundMessage);
            else
            {
                productCategoryRepository.Delete(product);
                return op.Succeeded();
            }
        }
    }
}
