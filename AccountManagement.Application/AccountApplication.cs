using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AutoMapper;
using Framework.Application;
using Framework.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository accountRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;
        private readonly IFileUploader fileUploader;
        private readonly IAuthHelper authHelper;
        private const string filePath = "Account";
        private readonly List<RoleViewModel> roles;
        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IMapper mapper, IFileUploader fileUploader, IAuthHelper authHelper)
        {
            this.mapper = mapper;
            this.accountRepository = accountRepository;
            this.passwordHasher = passwordHasher;
            this.fileUploader = fileUploader;
            this.authHelper = authHelper;

        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            OperationResult operation = new();
            var target = accountRepository.Get(command.Id);
            if (command.Password != command.RepeatedPassword)
                return operation.Failed(ValidationMessages.PasswordsMismatchMessage);
            target.ChangePassword(passwordHasher.Hash(command.Password));
            accountRepository.Update(target);
            return operation.Succeeded();
        }

        public OperationResult Create(CreateAccount command)
        {
            OperationResult operation = new();
            if (accountRepository.Exists(x => x.PhoneNumber == command.PhoneNumber))
                return operation.Failed(ApplicationMessages.DuplicatedAccountMessage);
            if (accountRepository.Exists(x => x.Username == command.Username))
                return operation.Failed(ApplicationMessages.DuplicatedUsernameMessage);
            string fileName = "";
            if (command.ProfilePicture != null)
                fileName = fileUploader.Upload(command.ProfilePicture, filePath);
            var target = new Account(command.FullName,
                                     command.Username,
                                     passwordHasher.Hash(command.Password),
                                     fileName,
                                     command.RoleId,
                                     command.PhoneNumber);
            accountRepository.Create(target);
            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            OperationResult operation = new();
            var target = accountRepository.Get(command.Id);

            if (accountRepository.Exists(x => x.PhoneNumber == command.PhoneNumber && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedAccountMessage);
            if (accountRepository.Exists(x => x.Username == command.Username && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedUsernameMessage);
            string fileName = "";
            if (command.ProfilePicture != null)
                fileName = fileUploader.Upload(command.ProfilePicture, filePath);
            target.Edit(command.FullName,
                command.Username,
                fileName,
                command.RoleId,
                command.PhoneNumber);
            accountRepository.Update(target);
            return operation.Succeeded();
        }

        public EditAccount EditGet(long id)
        {
            var target = accountRepository.Get(id);
            if (target == null)
                return null;
            return mapper.Map<EditAccount>(target);
        }

        public List<AccountViewModel> Search(AccountSearchModel command)
        {
            var result = accountRepository.Search(command).Select(x => mapper.Map<AccountViewModel>(x));
            return result.ToList();
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = accountRepository.GetAccountByUsername(command.Username);
            if (account == null)
            {
                return operation.Failed(ApplicationMessages.UserNotFoundMessage);
            }
            if (passwordHasher.Check(account.Password, command.Password).Verified == false)
                return operation.Failed(ApplicationMessages.UserNotFoundMessage);
            else
            {
                authHelper.SignIn(new AuthViewModel(account.Id, account.RoleId, account.Username, account.FullName));
                return operation.Succeeded();
            }
        }
        public void Logout()
        {
            authHelper.SignOut();
        }
        public OperationResult Register(RegisterAccount command)
        {
            return Create(new CreateAccount
            {
                FullName = command.FullName,
                ProfilePicture = command.ProfilePicture,
                PhoneNumber = command.PhoneNumber,
                Password = command.Password,
                RoleId = RoleDefinitionHelper.NormalUser.Id,
                Username = command.Username
            });
        }
    }
}
