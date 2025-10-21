using System.ComponentModel.DataAnnotations;

namespace MVCS3PL.ViewModels.IdentityViewModels
{
    public class ForgetPasswordViewModel
    {

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Can't Be Empty")]
        public string Email { get; set; }
    }
}
