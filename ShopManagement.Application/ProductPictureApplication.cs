using AutoMapper;
using Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository productPictureRepository;
        private readonly IFileUploader fileUploader;
        private readonly IMapper mapper;
        private const string filePath = "ProductPicture";

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IMapper mapper, IFileUploader fileUploader)
        {
            this.productPictureRepository = productPictureRepository;
            this.mapper = mapper;
            this.fileUploader = fileUploader;
        }
        public OperationResult Delete(long id)
        {
            OperationResult op = new();
            var product = productPictureRepository.Get(id);
            if (product == null)
                return op.Failed(ApplicationMessages.NotFoundMessage);
            else
            {
                productPictureRepository.Delete(product);
                return op.Succeeded();
            }
        }
        public OperationResult Create(CreateProductPicture data)
        {
            var operation = new OperationResult();
            string pictureName = fileUploader.Upload(data.Picture, filePath);
            productPictureRepository.Create(new ProductPicture(pictureName, data.PictureAlt,
                data.PictureTitle, data.ProductId));
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductPicture data)
        {
            var operation = new OperationResult();
            var target = productPictureRepository.GetProductPicture(data.Id);

            //Not Found
            if (target == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);

            string pictureName = fileUploader.Upload(data.Picture, filePath);

            target.Edit(pictureName, data.PictureAlt, data.PictureTitle, data.ProductId);

            productPictureRepository.Update(target);
            return operation.Succeeded();
        }

        public EditProductPicture EditGet(long id)
        {
            var target = productPictureRepository.GetProductPicture(id);
            if (target == null)
                return null;
            return mapper.Map<ProductPicture, EditProductPicture>(target);
        }

        public ProductPictureViewModel Get(long id)
        {
            var target = productPictureRepository.GetProductPicture(id);
            if (target == null)
                return null;
            return mapper.Map<ProductPicture, ProductPictureViewModel>(target);

        }

        public List<ProductPictureMinimalViewModel> List()
        {
            return productPictureRepository.GetProductPictures().Select(x => mapper.Map<ProductPicture, ProductPictureMinimalViewModel>(x)).ToList();
        }

        public List<ProductPictureMinimalViewModel> Search(SearchProductPicture searchModel)
        {
            return productPictureRepository.Search(searchModel).Select(x => mapper.Map<ProductPicture, ProductPictureMinimalViewModel>(x)).ToList();
        }
    }
}
