using System;
using System.ComponentModel.DataAnnotations;

namespace ProductionBackEnd.Dtos.User.Params
{
    public class RegisterParam
    {
        /// <summary>
        /// 使用者帳號
        /// </summary>
        [Required(ErrorMessage = "帳號為必填欄位")]
        public string UserName { get; set; }
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
        /// <summary>
        /// 真實姓名
        /// </summary>
        [Required(ErrorMessage = "真實姓名為必填欄位")]
        public string RealName { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        [Required(ErrorMessage = "信箱為必填欄位")]
        [EmailAddress(ErrorMessage = "信箱格式錯誤")]
        public string Email { get; set; }
        /// <summary>
        /// 角色權限
        /// </summary>
        [Required(ErrorMessage = "角色權限為必填欄位")]
        public string Role { get; set; }
    }
}
