
namespace CTB.Model.Account {
    public enum AccountIdentity {
        Common,
        VIP,
        Admin
    }
    public class AccountModel : _SharedAccountModel, IBackModel {
        public int Id { get; set; }
        public string Password_SHA2 { get; set; }
        public Grade Grade { get; set; }
        public AccountIdentity AccountIdentity { get; set; }
        public IViewModel ToViewModel() {
            return new AccountViewModel {
                College = this.College,
                Email = this.Email,
                Phone = this.Phone,
                City = this.City,
                District = this.District
                // Grade
            };
        }
    }
}
