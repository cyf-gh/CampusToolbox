using CTB.Model.Account;
using CTB.Model.HHI;

namespace MockModels {
    public class HHI {
        public HHIUserModel GetUser_Cyf_Admin() {
            return new HHIUserModel {
                Identity = Identity.Admin,
                StudentIndex = 170600233
            };
        }
        public HHIUserModel GetUser_PouPou_User() {
            return new HHIUserModel {
                Identity = Identity.User,
                StudentIndex = 160600225
            };
        }
        public AccountModel GetAccount_Cyf_Admin() {
            return new AccountModel {
                Id = 0,
                AccountIdentity = AccountIdentity.Admin,
                College = "东华大学",
                Email = "cyf-ms@hotmail.com",
                Phone = "18717780698",
                City = "上海",
                District = "浦东新区",
                Name = "曹逸凡"
            };
        }
        public AccountModel GetAccount_PouPou_VIP() {
            return new AccountModel {
                Id = 0,
                AccountIdentity = AccountIdentity.VIP,
                College = "东华大学",
                Email = "poupoukawa@pp.com",
                Phone = "18818881888",
                City = "重庆",
                District = "某个区",
                Name = "蹦蹦"
            };
        }
    }
}
