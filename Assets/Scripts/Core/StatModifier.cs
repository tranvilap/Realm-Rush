using System;
using UnityEngine;

namespace Stats
{
    public enum StatModifierType
    {
        Raw = 100,
        PercentAdd = 200,
        PercentMultiply = 300
    }
    [Serializable]
    public class StatModifier
    {
        [SerializeField] private float value = 0f;
        public StatModifierType Type = StatModifierType.Raw;
        [SerializeField] bool usingTypeOrder = true;
        [SerializeField] private int order;

        public object Source;

        public float Value => value;
        public int Order => order;
        public StatModifier() { }
        //public StatModifier(float value, StatModifierType type, int order, object source)
        //{
        //    this.value = value;
        //    this.Type = type;
        //    this.order = order;
        //    Source = source;
        //}
        public StatModifier(float value, StatModifierType type, bool useTypeOrder, int order, object source)
        {
            this.value = value;
            this.Type = type;
            if (useTypeOrder)
            {
                this.order = (int)type;
            }
            else
            {
                this.order = order;
            }
            Source = source;
        }
        public StatModifier(float value, StatModifierType type) : this(value, type, true, (int)type, null) { }
        public StatModifier(float value, StatModifierType type, int order) : this(value, type, false, order, null) { }
        public StatModifier(float value, StatModifierType type, object source) : this(value, type, true, (int)type, source) { }
        public StatModifier(StatModifier statModifier)
            : this(statModifier.value, statModifier.Type, statModifier.usingTypeOrder, statModifier.order, statModifier.Source) { }
        public StatModifier(StatModifier statModifier, object source)
           : this(statModifier.value, statModifier.Type, statModifier.usingTypeOrder, statModifier.order, source) { }
    }
}