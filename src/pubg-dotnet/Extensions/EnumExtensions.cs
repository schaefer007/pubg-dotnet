﻿using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Pubg.Net.Extensions
{
    public static class EnumExtensions
    {
        public static string Serialize(this Enum e)
        {
            return JsonConvert.SerializeObject(e).Trim('"');
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
            {
                return null;
            }

            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}
