using Framework.Application;
using System.Collections.Generic;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public interface ICustomerDiscountApplication
    {
        public OperationResult Edit(EditCustomerDiscount command);
        public OperationResult Define(DefineCustomerDiscount command);
        public EditCustomerDiscount EditGet(long id);
        public OperationResult Delete(long id);
        public bool Exists(long id);
        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel query);
    }

}
