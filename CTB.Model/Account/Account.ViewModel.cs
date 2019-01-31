using System;
using System.Collections.Generic;
using System.Text;
using stLib.CS.Security;

namespace CTB.Model.Account {
    class AccountViewModel :  _SharedAccountModel, IViewModel {
        public string Password { get; set; }
        public string Grade { get; set; }
        public string Identity { get; set; }
        public IBackModel ToBackModel() {
            SHA sha = new SHA();
            return new AccountModel {
                Password_SHA2 = sha.SHA512Encrypt( Password ),
                College = this.College,
                Email = this.Email,
                Phone = this.Phone,
                City = this.City,
                District = this.District
            };
        }


    }
}
