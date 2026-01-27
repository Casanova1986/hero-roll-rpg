using System;
using UnityEngine;

namespace NTHiep.MiniOdin
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ShowIfAttribute : PropertyAttribute
    {
        public string fieldName;
        public int enumValue;

        public ShowIfAttribute(string fieldName, int enumValue)
        {
            this.fieldName = fieldName;
            this.enumValue = enumValue;
        }
    }
}
