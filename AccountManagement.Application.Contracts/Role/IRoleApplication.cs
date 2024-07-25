using Framework.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        OperationResult Edit(EditRole command);
        EditRole EditGet(long id);
        List<RoleViewModel> Search(string roleName);
    }
}
