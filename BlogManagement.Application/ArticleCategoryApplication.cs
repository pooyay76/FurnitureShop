using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Framework.Application;
using System.Collections.Generic;
using System.Linq;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository articleCategoryRepository;
        private readonly IFileUploader fileUploader;
        private const string filePath = "Blog\\ArticleCategory";
        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            this.articleCategoryRepository = articleCategoryRepository;
            this.fileUploader = fileUploader;
        }
        public OperationResult Delete(long id)
        {
            OperationResult op = new();
            var product = articleCategoryRepository.Get(id);
            if (product == null)
                return op.Failed(ApplicationMessages.NotFoundMessage);
            else
            {
                articleCategoryRepository.Delete(product);
                return op.Succeeded();
            }
        }
        public OperationResult Create(CreateArticleCategory command)
        {
            OperationResult result = new();
            if (articleCategoryRepository.Exists(x => x.Name == command.Name))
                return result.Failed(ApplicationMessages.DuplicatedMessage);
            var fileName = fileUploader.Upload(command.Picture, filePath);
            ArticleCategory articleCategory = new(command.Name, fileName, command.Description, command.ShowOrder,
                command.Slug.Slugify(), command.Keywords, command.MetaDescription, command.CanonicalAddress, command.PictureTitle, command.PictureAlt);
            articleCategoryRepository.Create(articleCategory);
            return result.Succeeded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            OperationResult result = new();
            if (articleCategoryRepository.Exists(x => x.Id != command.Id && x.Name == command.Name))
                return result.Failed(ApplicationMessages.DuplicatedMessage);
            ArticleCategory entity = articleCategoryRepository.Get(command.Id);
            if (entity == null)
                return result.Failed(ApplicationMessages.NotFoundMessage);
            var fileName = fileUploader.Upload(command.Picture, filePath);
            entity.Edit(command.Name, fileName, command.Description, command.ShowOrder,
                command.Slug.Slugify(), command.Keywords, command.MetaDescription, command.CanonicalAddress, command.PictureTitle, command.PictureAlt);
            articleCategoryRepository.Update(entity);
            return result.Succeeded();
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return articleCategoryRepository.Search(searchModel)
                .Select(x => new ArticleCategoryViewModel
                {
                    Id = x.Id,
                    CreationDateTime = x.CreationDateTime,
                    Description = x.Description,
                    Name = x.Name,
                    Picture = x.Picture,
                    ShowOrder = x.ShowOrder
                }).ToList();
        }

        public EditArticleCategory EditGet(int id)
        {
            ArticleCategory entity = articleCategoryRepository.Get(id);
            if (entity == null)
                return null;
            return new EditArticleCategory()
            {
                Name = entity.Name,
                Description = entity.Description,
                CanonicalAddress = entity.CanonicalAddress,
                Id = id,
                Keywords = entity.Keywords,
                MetaDescription = entity.MetaDescription,
                ShowOrder = entity.ShowOrder,
                Slug = entity.Slug
            };
        }
    }
}
