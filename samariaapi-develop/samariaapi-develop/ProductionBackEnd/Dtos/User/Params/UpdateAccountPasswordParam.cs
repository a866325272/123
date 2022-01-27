using System.ComponentModel.DataAnnotations;

namespace ProductionBackEnd.Dtos.User.Params
{
    public class UpdateAccountPasswordParam
    {
        /// <summary>
        /// 舊使用者密碼
        /// </summary>
        [Required(ErrorMessage = "舊密碼為必填欄位")]
        [MinLength(8, ErrorMessage = "密碼不能小於八碼")]
        public string OldPassword { get; set; }
        /// <summary>
        /// 使用者密碼
        /// </summary>
        [Required(ErrorMessage = "密碼為必填欄位")]
        [MinLength(8, ErrorMessage = "密碼不能小於八碼")]
        public string Password { get; set; }
        /// <summary>
        /// 確認密碼
        /// </summary>
        [Required(ErrorMessage = "確認密碼為必填欄位")]
        [Compare("Password", ErrorMessage = "密碼與確認密碼不一致")]
        public string CheckPassword { get; set; }
    }
}
