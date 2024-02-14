using SSSNP_SNSOP.Core.OleDbMapping.MappingPropertiesGenerator.Interfaces;
using SSSNP_SNSOP.Entity.MappingProperty.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.OleDbMapping.MappingPropertiesGenerator
{
    public static class ExtentionMethod
    {
        public static List<MappingProperty> ConvertToListOf<T>(this List<IAttributeMappingProperty> src) where T : IAttributeMappingProperty
        {

            List<MappingProperty> result = new List<MappingProperty>();
            MappingProperty mappingEntity;

            foreach (IAttributeMappingProperty value in src)
            {
                mappingEntity = new MappingProperty();
                value.CopyProperties(mappingEntity);
                result.Add(mappingEntity);
            }

            return result;
        }
        public static List<IEnumerable<T>> ExtractBlock<T>(this IEnumerable<T> items, int qtyPerBlock)
        {
            decimal iterations = items.Count() / qtyPerBlock;
            var roundedIterations = (int)Math.Ceiling(iterations);
            var batchItems = new List<IEnumerable<T>>();
            for (int i = 0; i < roundedIterations; i++)
            {
                batchItems.Add(items.Skip(i * qtyPerBlock).Take(qtyPerBlock).ToArray());
            }
            return batchItems;
        }
        public static bool ExistColumn(this IDataRecord dr, string columnName)
        {
            if (dr == null) return false;
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }
        public static void CopyProperties(this object source, object destination)
        {



            if (source == null || destination == null)
                throw new Exception("CopyProperties Extended Method - Source or/and Destination Objects are null");

            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();


            PropertyInfo[] srcProps = typeSrc.GetProperties();
            foreach (PropertyInfo srcProp in srcProps)
            {
                if (!srcProp.CanRead)
                {
                    continue;
                }
                PropertyInfo targetProperty = typeDest.GetProperty(srcProp.Name);
                if (targetProperty == null)
                {
                    continue;
                }
                if (!targetProperty.CanWrite)
                {
                    continue;
                }
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                {
                    continue;
                }
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                {
                    continue;
                }
                // Passed all tests, lets set the value
                targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);

            }
        }

    }
}
