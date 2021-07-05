using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public abstract class CompareObjectExtension<T1>
    {
        private static readonly string dateFormat = "dd-MMM-yyyy";
        private static readonly string datetimeFormat = "dd-MMM-yyy hh:mm:ss tt";
        private static readonly string ignore = "Ignore";
        private static readonly int deltahours = 0;
        private static readonly string utc = "(UTC)";
        private static readonly List<string> ignoreFields = new List<string>() { };

        
        public virtual IEnumerable<T1> CompareObject<T2>(T2 oldObject, T2 newObject) where T2 : class
        {
            var result = new List<T1>();
            Type type = newObject.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var field = property.Name;
                if (!ignoreFields.Contains(field))
                {
                    var displayNameAttr = property.GetCustomAttributes(typeof(DisplayNameAttribute), false).Cast<DisplayNameAttribute>().FirstOrDefault();
                    if (displayNameAttr != null)
                    {
                        field = displayNameAttr.DisplayName;
                        if (field == ignore)
                        {
                            continue;
                        }

                    }

                    // Generic Type
                    var propertyType = property.PropertyType;
                    var newValue = property.GetValue(newObject, null);
                    var oldValue = oldObject != null ? property.GetValue(oldObject, null) : null;
                    if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                    {
                        continue;
                    }

                    if (propertyType.IsGenericType &&
                        (propertyType.GetGenericTypeDefinition() == typeof(IList<>)
                        || propertyType.GetGenericTypeDefinition() == typeof(List<>)
                        || propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                    {
                        Type itemType = propertyType.GetGenericArguments()[0];
                    }

                    if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?) && newValue != null)
                    {
                        var newDateValue = (DateTime)newValue;
                        newValue = newDateValue.Date == newDateValue.AddHours(deltahours) ? newDateValue.ToString(dateFormat) : newDateValue.ToString(datetimeFormat) + " " + utc;
                        if (oldValue != null)
                        {
                            var oldDateValue = (DateTime)oldValue;
                            oldValue = oldDateValue.Date == oldDateValue.AddHours(deltahours) ? oldDateValue.ToString(dateFormat) : oldDateValue.ToString(datetimeFormat) + " " + utc;

                        }
                    }

                    if (!Equals(oldValue, newValue))
                    {
                        result.Add(InStanceOfHistoryDetail(field, oldValue, newValue));
                    }
                }
            }

            return result;
        }

        public virtual T1 InStanceOfHistoryDetail(string field, object oldValue, object newValue)
        {
            var item = new CompareObjectModel   
            {
                Field = field,
                OldValue = oldValue != null ? oldValue.ToString() : string.Empty,
                NewValue = newValue != null ? newValue.ToString() : string.Empty

            };
            return (T1)Convert.ChangeType(item, typeof(T1));
        }

    }
}
