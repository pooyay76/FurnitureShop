using Framework.Application;
using System.Collections.Generic;

namespace BlogManagement.Application.Contracts.Article
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle command);
        EditArticle EditGet(int id);
        OperationResult Edit(EditArticle command);
        public OperationResult Delete(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
