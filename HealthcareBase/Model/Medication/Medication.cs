// File:    Medication.cs
// Author:  Lana
// Created: 14 April 2020 20:48:37
// Purpose: Definition of Class Medication

using System;
using System.Collections.Generic;

namespace Model.Medication
{
    public class Medication : Repository.Generics.Entity<int>
    {
        private String name;
        private String manufacturer;
        private List<Medication> alternatives;
        private String description;
        private MedicineType type;
        private List<Frequency> sideEffects;
        private List<Amount> ingredients;
        private int id;

        public Medication(string name, string manufacturer, List<Medication> alternatives, string description, MedicineType type, List<Frequency> sideEffects, List<Amount> ingredients)
        {
            this.name = name;
            this.manufacturer = manufacturer;
            this.alternatives = alternatives;
            this.description = description;
            this.type = type;
            this.sideEffects = sideEffects;
            this.ingredients = ingredients;
        }

        public Medication()
        {
        }

        public string Name { get => name; set => name = value; }
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        public string Description { get => description; set => description = value; }
        public MedicineType Type { get => type; set => type = value; }

        public int Id { get => id; set => id = value; }

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
                {
                    foreach (Medication alternative in value)
                        AddAlternative(alternative);
                }
            }
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
                {
                    foreach (Frequency sideEffect in value)
                        AddSideEffect(sideEffect);
                }
            }
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
                {
                    foreach (Amount ingrediets in value)
                        AddIngredient(ingrediets);
                }
            }
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

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }
    }
}