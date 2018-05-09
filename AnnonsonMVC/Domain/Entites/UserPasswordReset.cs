using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class UserPasswordReset
    {
        public int UserPasswordResetId { get; set; }
        public string GuId { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public bool IsCompleted { get; set; }
    }
}
