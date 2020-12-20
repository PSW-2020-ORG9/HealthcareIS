// File:    StorageRecord.cs
// Author:  Lana
// Created: 18 April 2020 18:19:45
// Purpose: Definition of Class StorageRecord

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.StorageRecords
{
    public abstract class StorageRecord : IEntity<int>
    {
        protected int availableAmount;
        protected int id;
        protected List<AmountChangeRecord> supplyHistory;
        protected List<AmountChangeRecord> usageHistory;

        public int AvailableAmount
        {
            get => availableAmount;
            set => availableAmount = value;
        }

        [Key]
        public int Id
        {
            get => id;
            set => id = value;
        }

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
                    foreach (var oAmountChangeRecord in value)
                        AddSupplyHistory(oAmountChangeRecord);
            }
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
                    foreach (var oAmountChangeRecord in value)
                        AddUsageHistory(oAmountChangeRecord);
            }
        }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }

        public void AddSupplyHistory(AmountChangeRecord newAmountChangeRecord)
        {
            if (newAmountChangeRecord == null)
                return;
            if (supplyHistory == null)
                supplyHistory = new List<AmountChangeRecord>();
            if (!supplyHistory.Contains(newAmountChangeRecord))
                supplyHistory.Add(newAmountChangeRecord);
        }

        public void RemoveSupplyHistory(AmountChangeRecord oldAmountChangeRecord)
        {
            if (oldAmountChangeRecord == null)
                return;
            if (supplyHistory != null)
                if (supplyHistory.Contains(oldAmountChangeRecord))
                    supplyHistory.Remove(oldAmountChangeRecord);
        }

        public void RemoveAllSupplyHistory()
        {
            if (supplyHistory != null)
                supplyHistory.Clear();
        }

        public void AddUsageHistory(AmountChangeRecord newAmountChangeRecord)
        {
            if (newAmountChangeRecord == null)
                return;
            if (usageHistory == null)
                usageHistory = new List<AmountChangeRecord>();
            if (!usageHistory.Contains(newAmountChangeRecord))
                usageHistory.Add(newAmountChangeRecord);
        }

        public void RemoveUsageHistory(AmountChangeRecord oldAmountChangeRecord)
        {
            if (oldAmountChangeRecord == null)
                return;
            if (usageHistory != null)
                if (usageHistory.Contains(oldAmountChangeRecord))
                    usageHistory.Remove(oldAmountChangeRecord);
        }

        public void RemoveAllUsageHistory()
        {
            if (usageHistory != null)
                usageHistory.Clear();
        }

        public override bool Equals(object obj)
        {
            return obj is StorageRecord record &&
                   id == record.id;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}