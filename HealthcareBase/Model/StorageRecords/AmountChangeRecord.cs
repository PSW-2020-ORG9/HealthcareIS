// File:    AmountChangeRecord.cs
// Author:  Lana
// Created: 18 April 2020 18:19:45
// Purpose: Definition of Class AmountChangeRecord

using System;

namespace Model.StorageRecords
{
    public class AmountChangeRecord
    {
        private int amount;
        private DateTime date;

        public int Amount { get => amount; set => amount = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}