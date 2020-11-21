// File:    Medication.cs
// Author:  Lana
// Created: 14 April 2020 20:48:37
// Purpose: Definition of Class Medication

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Medication
{
    public class Medication : Entity<int>
    {
        private List<Medication> alternatives;
        private List<Amount> ingredients;
        private List<Frequency> sideEffects;

        public Medication(string name, string manufacturer, List<Medication> alternatives, string description,
            MedicineType type, List<Frequency> sideEffects, List<Amount> ingredients)
        {
            Name = name;
            Manufacturer = manufacturer;
            this.alternatives = alternatives;
            Description = description;
            Type = type;
            this.sideEffects = sideEffects;
            this.ingredients = ingredients;
        }

        public Medication()
        {
        }

        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public MedicineType Type { get; set; }

        [Key]
        public int Id { get; set; }

        public IEnumerable<Medication> Alternatives
        {
            get
            {
                if (alternatives == null)
                    alternatives = new List<Medication>();
                return alternatives;
            }
            set
            {
                RemoveAllAlternatives();
                if (value != null)
                    foreach (var alternative in value)
                        AddAlternative(alternative);
            }
        }

        public IEnumerable<Frequency> SideEffects
        {
            get
            {
                if (sideEffects == null)
                    sideEffects = new List<Frequency>();
                return sideEffects;
            }
            set
            {
                RemoveAllSideEffects();
                if (value != null)
                    foreach (var sideEffect in value)
                        AddSideEffect(sideEffect);
            }
        }

        public IEnumerable<Amount> Ingredients
        {
            get
            {
                if (ingredients == null)
                    ingredients = new List<Amount>();
                return ingredients;
            }
            set
            {
                RemoveAllIngredients();
                if (value != null)
                    foreach (var ingrediets in value)
                        AddIngredient(ingrediets);
            }
        }

        public int GetKey()
        {
            return Id;
        }

        public void SetKey(int id)
        {
            Id = id;
        }

        public void AddAlternative(Medication newAlternative)
        {
            if (newAlternative == null)
                return;
            if (alternatives == null)
                alternatives = new List<Medication>();
            if (alternatives.Contains(newAlternative))
                alternatives.Add(newAlternative);
        }

        public void RemoveAlternative(Medication oldAlternative)
        {
            if (oldAlternative == null)
                return;
            if (alternatives != null)
                if (alternatives.Contains(oldAlternative))
                    alternatives.Remove(oldAlternative);
        }

        public void RemoveAllAlternatives()
        {
            if (alternatives != null)
                alternatives.Clear();
        }

        public void AddSideEffect(Frequency newSideEffect)
        {
            if (newSideEffect == null)
                return;
            if (sideEffects == null)
                sideEffects = new List<Frequency>();
            if (sideEffects.Contains(newSideEffect))
                sideEffects.Add(newSideEffect);
        }

        public void RemoveSideEffect(Frequency oldSideEffect)
        {
            if (oldSideEffect == null)
                return;
            if (sideEffects != null)
                if (sideEffects.Contains(oldSideEffect))
                    sideEffects.Remove(oldSideEffect);
        }

        public void RemoveAllSideEffects()
        {
            if (sideEffects != null)
                sideEffects.Clear();
        }

        public void AddIngredient(Amount newIngredient)
        {
            if (newIngredient == null)
                return;
            if (ingredients == null)
                ingredients = new List<Amount>();
            if (ingredients.Contains(newIngredient))
                ingredients.Add(newIngredient);
        }

        public void RemoveIngredient(Amount oldIngredient)
        {
            if (oldIngredient == null)
                return;
            if (ingredients != null)
                if (ingredients.Contains(oldIngredient))
                    ingredients.Remove(oldIngredient);
        }

        public void RemoveAllIngredients()
        {
            if (ingredients != null)
                ingredients.Clear();
        }
    }
}