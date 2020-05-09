using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace Stats
{
    [Serializable]
    public class BaseStat
    {
        public float BaseValue;

        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        protected bool isDirty = true;
        protected float _value;
        protected float lastBaseValue = float.MinValue; //For triggering Recalculating value

        public BaseStat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }
        public BaseStat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public float CalculatedValue
        {
            get
            {
                if (isDirty || BaseValue != lastBaseValue)
                {
                    _value = CalculateFinalValue();
                    isDirty = false;
                }
                return _value;
            }
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
            {
                return -1;
            }
            else if (a.Order > b.Order)
            {
                return 1;
            }
            return 0;
        }
        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;
            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];
                switch (mod.Type)
                {
                    case StatModifierType.Raw:
                        finalValue += mod.Value;
                        break;
                    case StatModifierType.PercentAdd:
                        sumPercentAdd += mod.Value;

                        if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModifierType.PercentAdd)
                        {
                            finalValue *= 1 + sumPercentAdd;
                            sumPercentAdd = 0;
                        }
                        break;
                    case StatModifierType.PercentMultiply:
                        finalValue *= 1 + mod.Value;
                        break;
                    default:
                        break;
                }
            }
            return (float)System.Math.Round(finalValue, 4);
        }

        public virtual void AddModifier(StatModifier statModifier)
        {
            isDirty = true;
            statModifiers.Add(statModifier);
            statModifiers.Sort(CompareModifierOrder);
        }
        public virtual bool RemoveModifier(StatModifier statModifier)
        {
            if (statModifiers.Remove(statModifier))
            {
                isDirty = true;
                return true;
            }
            return false;
        }
        public virtual bool RemoveModifiersFromSource(object source)
        {
            bool didRemove = false;
            for (int i = statModifiers.Count - 1; i <= 0; i--)
            {
                if (statModifiers[i].Source == source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }
            return didRemove;
        }
    } 
}
