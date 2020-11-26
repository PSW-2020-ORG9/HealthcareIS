// File:    AmountChangeRecord.cs
// Author:  Lana
// Created: 18 April 2020 18:19:45
// Purpose: Definition of Class AmountChangeRecord

using System;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Model.StorageRecords
{
    [Owned]
    public class AmountChangeRecord
    {
        public int Amount { get; set; }

        public DateTime Date { get; set; }
    }
}