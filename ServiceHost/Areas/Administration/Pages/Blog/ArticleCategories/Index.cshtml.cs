using BlogManagement.Application.Contracts.ArticleCategory;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategories
{
    public class IndexModel : PageModel
    {
        private readonly IArticleCategoryApplication articleCategoryApplication;
        public List<ArticleCategoryViewModel> Items { get; set; }
        public ArticleCategorySearchModel SearchModel { get; set; }


        public IndexModel(IArticleCategoryApplication _articleCategoryApplication)
        {

            articleCategoryApplication = _articleCategoryApplication;
        }


        public void OnGet(ArticleCategorySearchModel command)
        {
            SearchModel = command;
            Items = articleCategoryApplication.Search(SearchModel);
        }
        public PartialViewResult OnGetCreate()
        {
            return Partial("./Create",new CreateArticleCategory());
        }
        public PartialViewResult OnGetEdit(int id)
        {
            var item = articleCategoryApplication.EditGet(id);
            if(item != null)
                return Partial("./Edit",item);
            return null;

        }
        public JsonResult OnPostCreate(CreateArticleCategory form)
        {
            if (ModelState.IsValid)
                return new JsonResult(articleCategoryApplication.Create(form));
            else
            {
                OperationResult operation = new();
                return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
            }
        }
        public JsonResult OnPostEdit(EditArticleCategory form)
        {
            if (ModelState.IsValid)
            {
                return new JsonResult(articleCategoryApplication.Edit(form));
            }
            else
            {
                OperationResult operation = new();
                return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
            }
        }
    }
}
