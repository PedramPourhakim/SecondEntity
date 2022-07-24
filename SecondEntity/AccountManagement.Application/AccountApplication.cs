using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IRoleRepository roleRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IFileUploader fileUploader;
        private readonly IAuthHelper authHelper;
        public AccountApplication(IRoleRepository roleRepository,
            IAccountRepository accountRepository,
            IPasswordHasher passwordHasher,
            IFileUploader fileUploader,IAuthHelper authHelper)
        {
            this.roleRepository = roleRepository;
            this.accountRepository = accountRepository;
            this.passwordHasher = passwordHasher;
            this.fileUploader = fileUploader;
            this.authHelper = authHelper;
        }
        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);

            var password = passwordHasher.Hash(command.Password);
            account.ChangePassword(password);
            accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (accountRepository.Exists(x => (x.UserName ==
             command.UserName || x.Mobile == command.Mobile)
             && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var path = $"ProfilePhotos";
            var picturePath = fileUploader.Upload(command.ProfilePhoto, path);
            account.Edit(command.FullName, command.UserName,
                command.Mobile, command.RoleId, picturePath);

            accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public AccountViewModel GetAccountBy(long id)
        {
            var account = accountRepository.Get(id);
            return new AccountViewModel()
            {
                FullName=account.FullName,
                Mobile=account.Mobile
            };
        }

        public List<AccountViewModel> GetAccounts()
        {
            return accountRepository.GetAccounts();
        }

        public EditAccount GetDetails(long id)
        {
            return accountRepository.GetDetails(id);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = accountRepository.GetBy(command.UserName);
            if (account == null)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            var result = passwordHasher.Check(account.Password,
                command.Password);
            if (!result.Verified)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            var permissions = roleRepository.Get(account.RoleId).Permissions.Select(x => x.Code).ToList();
               

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.FullName
            , account.UserName, account.Mobile, permissions);

            authHelper.Signin(authViewModel);
            return operation.Succeeded();

        }

        public void Logout()
        {
            authHelper.SignOut();
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();
            if (accountRepository.Exists(x => x.UserName ==
             command.UserName || x.Mobile == command.Mobile))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var password = passwordHasher.Hash(command.Password);

            var path = $"ProfilePhotos";
            var picturePath = fileUploader.Upload(command.ProfilePhoto, path);

            var account = new Account(command.FullName,
                command.UserName, password, command.Mobile,
                command.RoleId, picturePath);
            accountRepository.Create(account);

            accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return accountRepository.Search(searchModel);
        }
    }
}
