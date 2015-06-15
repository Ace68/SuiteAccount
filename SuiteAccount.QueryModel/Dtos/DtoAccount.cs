using System;
namespace SuiteAccount.QueryModel.Dtos
{
    public class DtoAccount : DtoBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public bool IsOnline { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
