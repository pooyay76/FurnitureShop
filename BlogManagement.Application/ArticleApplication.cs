using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Framework.Application;
using System.Collections.Generic;

namespace BlogManagement.Application
{
    public class ArticleApplication
    {

        private readonly IArticleRepository articleRepository;
        private readonly IFileUploader fileUploader;
        private const string filePath = "Blog\\Article";
        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader)
        {
            this.articleRepository = articleRepository;
            this.fileUploader = fileUploader;
        }
        public OperationResult Delete(long id)
        {
            OperationResult op = new();
            var product = articleRepository.Get(id);
            if (product == null)
                return op.Failed(ApplicationMessages.NotFoundMessage);
            else
            {
                articleRepository.Delete(product);
                return op.Succeeded();
            }
        }
        public OperationResult Create(CreateArticle command)
        {
            OperationResult result = new();
            if (articleRepository.Exists(x => x.Title == command.Title))
                return result.Failed(ApplicationMessages.DuplicatedMessage);
            var fileName = fileUploader.Upload(command.Picture, filePath);
            Article article = new(command.Title, command.ShortDescription, command.Description,
                fileName, command.PictureAlt, command.PictureTitle,
                command.Slug.Slugify(), command.CanonicalAddress, command.Keywords,
                command.ArticleCategoryId, command.AuthorId);
            articleRepository.Create(article);
            return result.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            OperationResult result = new();
            if (articleRepository.Exists(x => x.Id != command.Id && x.Title == command.Title))
                return result.Failed(ApplicationMessages.DuplicatedMessage);
            Article entity = articleRepository.Get(command.Id);
            if (entity == null)
                return result.Failed(ApplicationMessages.NotFoundMessage);
            var fileName = fileUploader.Upload(command.Picture, filePath);
            entity.Edit(command.Title, command.ShortDescription, command.Description,
                fileName, command.PictureAlt, command.PictureTitle,
                command.Slug.Slugify(), command.CanonicalAddress, command.Keywords,
                command.ArticleCategoryId);
            articleRepository.Update(entity);
            return result.Succeeded();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return articleRepository.Search(searchModel);
        }

        public EditArticle EditGet(int id)
        {
            Article entity = articleRepository.Get(id);
            if (entity == null)
                return null;
            return new EditArticle()
            {
                Title = entity.Title,
                Description = entity.Description,
                CanonicalAddress = entity.CanonicalAddress,
                Id = id,
                Keywords = entity.Keywords,
                ArticleCategoryId = entity.ArticleCategoryId,
                PictureAlt = entity.PictureAlt,
                PictureTitle = entity.PictureTitle,
                ShortDescription = entity.ShortDescription,
                Slug = entity.Slug
            };
        }
    }
}

