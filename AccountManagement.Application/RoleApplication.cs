using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Framework.Application;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
            OperationResult result = new OperationResult();
            if (roleRepository.Exists(x=>x.Name == command.Name))
                {
                return result.Failed(ApplicationMessages.DuplicatedMessage);
                }
            roleRepository.Create(new Role(command.Name,new List<Permission>()));
            return result.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            OperationResult result = new OperationResult();

            Role role = roleRepository.Get(command.Id);
            if (role == null)
            {
                return result.Failed(ApplicationMessages.NotFoundMessage);
            }
            if (roleRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return result.Failed(ApplicationMessages.DuplicatedMessage);
            }
            role.Edit(command.Name, new List<Permission>());
            roleRepository.Update(role);
            return result.Succeeded();
        }

        public EditRole EditGet(long id)
        {
            Role role = roleRepository.Get(id);
            if (role == null)
            {
                return null;
            }
            return new EditRole { Name = role.Name, Id=role.Id };
        }

        public List<RoleViewModel> Search(string name)
        {
            return roleRepository.Search(name).Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
