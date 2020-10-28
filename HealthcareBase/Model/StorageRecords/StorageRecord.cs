// File:    StorageRecord.cs
// Author:  Lana
// Created: 18 April 2020 18:19:45
// Purpose: Definition of Class StorageRecord

using System;
using System.Collections.Generic;

namespace Model.StorageRecords
{
    public abstract class StorageRecord : Repository.Generics.Entity<int>
    {
        protected int availableAmount;
        protected List<AmountChangeRecord> supplyHistory;
        protected List<AmountChangeRecord> usageHistory;
        protected int id;

        public int AvailableAmount { get => availableAmount; set => availableAmount = value; }
        public int Id { get => id; set => id = value; }

        public IEnumerable<AmountChangeRecord> SupplyHistory
        {
            get
            {
                if (supplyHistory == null)
                    supplyHistory = new List<AmountChangeRecord>();
                return supplyHistory;
            }
            set
            {
                RemoveAllSupplyHistory();
                if (value != null)
                {
                    foreach (AmountChangeRecord oAmountChangeRecord in value)
                        AddSupplyHistory(oAmountChangeRecord);
                }
            }
        }

        public void AddSupplyHistory(AmountChangeRecord newAmountChangeRecord)
        {
            if (newAmountChangeRecord == null)
                return;
            if (this.supplyHistory == null)
                this.supplyHistory = new List<AmountChangeRecord>();
            if (!this.supplyHistory.Contains(newAmountChangeRecord))
                this.supplyHistory.Add(newAmountChangeRecord);
        }

        public void RemoveSupplyHistory(AmountChangeRecord oldAmountChangeRecord)
        {
            if (oldAmountChangeRecord == null)
                return;
            if (this.supplyHistory != null)
                if (this.supplyHistory.Contains(oldAmountChangeRecord))
                    this.supplyHistory.Remove(oldAmountChangeRecord);
        }

        public void RemoveAllSupplyHistory()
        {
            if (supplyHistory != null)
                supplyHistory.Clear();
        }

        public IEnumerable<AmountChangeRecord> UsageHistory
        {
            get
            {
                if (usageHistory == null)
                    usageHistory = new List<AmountChangeRecord>();
                return usageHistory;
            }
            set
            {
                RemoveAllUsageHistory();
                if (value != null)
                {
                    foreach (AmountChangeRecord oAmountChangeRecord in value)
                        AddUsageHistory(oAmountChangeRecord);
                }
            }
        }

        public void AddUsageHistory(AmountChangeRecord newAmountChangeRecord)
        {
            if (newAmountChangeRecord == null)
                return;
            if (this.usageHistory == null)
                this.usageHistory = new List<AmountChangeRecord>();
            if (!this.usageHistory.Contains(newAmountChangeRecord))
                this.usageHistory.Add(newAmountChangeRecord);
        }

        public void RemoveUsageHistory(AmountChangeRecord oldAmountChangeRecord)
        {
            if (oldAmountChangeRecord == null)
                return;
            if (this.usageHistory != null)
                if (this.usageHistory.Contains(oldAmountChangeRecord))
                    this.usageHistory.Remove(oldAmountChangeRecord);
        }

        public void RemoveAllUsageHistory()
        {
            if (usageHistory != null)
                usageHistory.Clear();
        }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is StorageRecord record &&
                   id == record.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}