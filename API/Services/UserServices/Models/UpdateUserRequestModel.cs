using API.Models.Enums;

namespace API.Services.UserServices.Models
{
    public class UpdateUserRequestModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ReferralType ReferralType { get; set; }
    }
}
