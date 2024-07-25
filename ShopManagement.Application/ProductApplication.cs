using AutoMapper;
using Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IFileUploader fileUploader;
        private const string filePath = "Product";

        public ProductApplication(IProductRepository productRepository, IMapper mapper, IFileUploader fileUploader)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProduct dataEntry)
        {

            var operation = new OperationResult();
            if (productRepository.Exists(x => x.Name == dataEntry.Name))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            string slug = dataEntry.Name.Slugify();
            string pictureName = fileUploader.Upload(dataEntry.Picture, filePath);
            var data = new Product(dataEntry.Name, dataEntry.Description, pictureName,
                dataEntry.PictureAlt, dataEntry.PictureTitle, dataEntry.Keywords, dataEntry.MetaDescription,
                slug, dataEntry.CategoryId, dataEntry.Code, dataEntry.ShortDescription);
            productRepository.Create(data);
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct dataEntry)
        {
            var operation = new OperationResult();

            //fetch entity
            var entity = productRepository.GetProduct(dataEntry.Id);

            //if entity not found
            if (entity == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            //if entering duplicated data
            if (productRepository.Exists(x => x.Name == dataEntry.Name && x.Id != dataEntry.Id))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);

            string slug = dataEntry.Name.Slugify();
            string pictureName = fileUploader.Upload(dataEntry.Picture, filePath);
            entity.Edit(dataEntry.Name, dataEntry.Description, pictureName,
                dataEntry.PictureAlt, dataEntry.PictureTitle, dataEntry.Keywords, dataEntry.MetaDescription, slug,
                dataEntry.CategoryId, dataEntry.Code, dataEntry.ShortDescription);

            productRepository.Update(entity);

            return operation.Succeeded();
        }

        public EditProduct EditGet(long id)
        {
            var entity = productRepository.GetProduct(id);

            //if entity was not found return null
            if (entity == null)
                return null;
            return mapper.Map<Product, EditProduct>(entity);
        }

        public ProductViewModel Get(long id)
        {

            var entity = productRepository.GetProduct(id);
            if (entity == null)
                return null;
            return mapper.Map<Product, ProductViewModel>(entity);
        }

        public List<ProductMinimalViewModel> List()
        {
            return productRepository.GetProductsWithCategories().Select(x => mapper.Map<Product, ProductMinimalViewModel>(x)).ToList();
        }


        public List<ProductMinimalViewModel> Search(ProductSearchModel query)
        {
            return productRepository.Search(query).Select(x => mapper.Map<Product, ProductMinimalViewModel>(x)).ToList();
        }

        public OperationResult Delete(long id)
        {
            OperationResult op = new();
            var product = productRepository.Get(id);
            if (product == null)
                return op.Failed(ApplicationMessages.NotFoundMessage);
            else
            {
                productRepository.Delete(product);
                return op.Succeeded();
            }
        }
    }
}
