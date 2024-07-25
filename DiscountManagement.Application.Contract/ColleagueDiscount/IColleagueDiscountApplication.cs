using Framework.Application;
using System.Collections.Generic;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public interface IColleagueDiscountApplication
    {
        public OperationResult Define(DefineColleagueDiscount command);
        public OperationResult Edit(EditColleagueDiscount command);
        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel command);
        public EditColleagueDiscount EditGet(long id);
        public OperationResult Remove(long id);
        public OperationResult Delete(long id);
        public OperationResult Restore(long id);
        public bool Exists(long id);
    }
}