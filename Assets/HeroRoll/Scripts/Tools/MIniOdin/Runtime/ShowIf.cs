using UnityEngine;
using System;

namespace NTHiep.MiniOdin
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ShowIf : PropertyAttribute
    {
        public string fieldName;
        public int enumValue;

        public ShowIf(string fieldName, int enumValue)
        {
            this.fieldName = fieldName;
            this.enumValue = enumValue;
        }
    }
}