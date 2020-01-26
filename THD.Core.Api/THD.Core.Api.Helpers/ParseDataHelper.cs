﻿using System;
using System.Collections.Generic;
using System.Text;

namespace THD.Core.Api.Helpers
{
    public class ParseDataHelper
    {
        public static int ParseInt(string value)
        {
            return int.TryParse(value, out int x) ? int.Parse(value) : 0;
        }

        public static DateTime? ParseDateTime(string value)
        {
            DateTime? dateTime = null;
            if (DateTime.TryParse(value, out DateTime x))
            {
                dateTime = DateTime.Parse(value);
            }
            return dateTime;
        }

        public static bool Parsebool(string value)
        {
            return Boolean.TryParse(value, out bool x) ? Boolean.Parse(value) : false;
        }

        public static object ConvertDBNull(string _value)
        {
            if (string.IsNullOrEmpty(_value)) return System.DBNull.Value;
            else return _value;
        }
        public static object ConvertDBNullDate(DateTime? _value)
        {
            if (_value == null) return System.DBNull.Value;
            else return _value;
        }
        public static object ConvertDBNullTime(TimeSpan? _value)
        {
            if (_value == null) return System.DBNull.Value;
            else return _value;
        }

    }
}
