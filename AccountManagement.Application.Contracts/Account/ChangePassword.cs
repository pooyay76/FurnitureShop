﻿namespace AccountManagement.Application.Contracts.Account
{
    public class ChangePassword
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
    }
}
