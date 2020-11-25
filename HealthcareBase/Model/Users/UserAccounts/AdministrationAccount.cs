﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.UserAccounts
{
    public class AdministrationAccount : UserAccount
    {
        [ForeignKey("AdministrationWorker")]
        public int AdministrationWorkerId { get; set; }
        public Employee.AdministrationWorker AdministrationWorker { get; set; }
    }
}