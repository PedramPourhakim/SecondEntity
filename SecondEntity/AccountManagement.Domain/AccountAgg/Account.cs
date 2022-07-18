using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account :EntityBase
    {
        public string FullName { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Mobile { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }
        public string ProfilePhoto { get; private set; }

        public Account(string fullName, 
            string userName, string password,
            string mobile, long roleId,
            string profilePhoto)
        {
            FullName = fullName;
            UserName = userName;
            Password = password;
            Mobile = mobile;
            RoleId = roleId;

            if (roleId == 0)
                RoleId = 2;

            ProfilePhoto = profilePhoto;
        }
        public void Edit(string FullName,string UserName,
            string Mobile,long RoleId,string ProfilePhoto)
        {
            this.FullName = FullName;
            this.UserName = UserName;
            this.Mobile = Mobile;
            this.RoleId = RoleId;
            if (!string.IsNullOrWhiteSpace(ProfilePhoto))
                this.ProfilePhoto = ProfilePhoto;
        }
        public void ChangePassword(string Password)
        {
            this.Password = Password;
        }
    }
}
