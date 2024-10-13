using API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = "Full name is required.")]
        [MaxLength(255, ErrorMessage = "Full name cannot exceed 255 characters.")] 
        public string FullName { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(255, ErrorMessage = "Phone number cannot exceed 255 characters.")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(255, ErrorMessage = "Email cannot exceed 255 characters.")] 
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public ReferralType ReferralType { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}
