using SSSNP_SNSOP.Core.OleDbMapping.MappingPropertiesGenerator.Interfaces;
using SSSNP_SNSOP.Entity.MappingProperty;
using SSSNP_SNSOP.Entity.MappingProperty.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.OleDbMapping.MappingPropertiesGenerator
{
    public class MappingPropertiesGeneratorBase : IDisposable
    {
        private string _multipleColumnCharSeparator = "#";
        private IDataReader _reader;
        private IDataReader _reader2;
        private DataTable _dt;
        private List<IAttributeMappingProperty> _attributePropertiesMapping;
        private List<IAttributeMappingProperty> _attributePropertiesAllowMultipleMapping;
        private List<MappingProperty> _mappingPropertyCache;
        private List<MappingProperty> _mappingPropertyMultipleColumnCache;

        private List<ColumnProperty> _columnMapping;
        private string _referenceColumnName;
        private string _referencePropertyName;

        //Costructor
        public MappingPropertiesGeneratorBase(DataTable reader)
        {

            if (reader == null) throw new Exception("Source Reader Null");

            this._dt = reader;
            this._columnMapping = new List<ColumnProperty>();

            LoadSettings();

        }

        //Mapping Columns and Properties
        protected void MappingColumnSource()
        {
            try
            {

                for (int i = 0; i < this._dt.Columns.Count; i++)
                {
                    ColumnProperty columnProperty = new ColumnProperty();
                    columnProperty.ColumnName = this._dt.Columns[i].ToString().Trim();
                    columnProperty.PositionIndex = i;

                    this._columnMapping.Add(columnProperty);

                }

            }
            catch (Exception ex)
            {

                throw;
            }


        }
        protected void MappingPropertiesAttribute<T>()
        {
            try
            {

                this.ExecuteMappingPropertiesAttribute<T>();

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        protected void MappingPropertiesAttribute<T, M>()
        {
            try
            {

                this.ExecuteMappingPropertiesAttribute<T>();
                this.ExecuteMappingPropertiesAttribute<M>();

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        protected List<T> ExecuteOperationMapping<T>() where T : class, new()
        {
            List<T> result = new List<T>();


            this._reader = this._dt.CreateDataReader();

            MapObjectList<T>(result, this._reader);

            return result;
        }
        protected List<T> ExecuteOperationMapping2<T>() where T : class, new()
        {
            List<T> result = new List<T>();


            this._reader2 = this._dt.CreateDataReader();

            MapObjectList<T>(result, this._reader2);

            return result;
        }
        protected List<T> ExecuteOperationMappingMultiple<T>() where T : class, new()
        {
            List<T> result = new List<T>();


            this._reader = this._dt.CreateDataReader();


            //Set BlockSizeProperties
            int blockSizeProperties = BlockSizeProperties();

            ColumnProperty lastColumn = this.ColumnsMapping.Last(x => x.ColumnName.Contains(_multipleColumnCharSeparator));
            int maxColumnLoop = this.GetMaxValueMultipleColumnExpression(lastColumn.ColumnName);
            if (maxColumnLoop == 0) { throw new Exception("Method GetMaxValueMultipleColumnExpression Exception"); }

            //Extending Columns
            this.ExtendingProperties(maxColumnLoop);


            MapObjectList<T>(result, this._reader, blockSizeProperties);

            return result;
        }
        protected virtual object LookupMappingProperty(IAttributeMappingProperty mProperty, object value)
        {
            throw new NotImplementedException();
        }

        protected virtual int BlockSizeProperties()
        {
            try
            {
                return this._attributePropertiesAllowMultipleMapping.Count(x => x.AllowMultiple);

            }
            catch (Exception)
            {
                return 0;
            }

        }
        protected virtual T Lookup<T>()
        {
            T result = default(T);
            try
            {
                Type _type = typeof(T);
                result = Activator.CreateInstance<T>();

            }
            catch (Exception _err)
            {
                throw new Exception("T Lookup<T>", _err);
            }

            return result;

        }
        protected void ExtendingProperties(int maxValueExtended)
        {

            if (this._attributePropertiesAllowMultipleMapping == null || this._attributePropertiesAllowMultipleMapping.Count == 0) return;

            List<IAttributeMappingProperty> resultValue = new List<IAttributeMappingProperty>();
            IAttributeMappingProperty mProperty = null;

            for (int i = 1; i <= maxValueExtended; i++)
            {

                foreach (IAttributeMappingProperty element in this._attributePropertiesAllowMultipleMapping)
                {
                    mProperty = new AttributeMappingProperty();
                    element.CopyProperties(mProperty);

                    mProperty.ColumnName = string.Format("{0}{1}{2}", element.ColumnName, this._multipleColumnCharSeparator, i);
                    //mProperty.AllowMultiple = element.AllowMultiple;
                    //mProperty.ApplyLookupMapping = element.ApplyLookupMapping;
                    //mProperty.IsMandatory = element.IsMandatory;
                    //mProperty.LookupMappingName = element.LookupMappingName;
                    //mProperty.PropertyName = element.PropertyName;
                    //mProperty.ReferenceOwnerPropertyName = element.ReferenceOwnerPropertyName;
                    //mProperty.ReferenceOwner = element.ReferenceOwner;

                    resultValue.Add(mProperty);
                }

            }
            if (resultValue.Count != 0) { this._attributePropertiesAllowMultipleMapping = resultValue; }


        }
        protected virtual string SetMultipleColumnCharSeparator()
        {
            return "#";
        }

        protected void Reset()
        {
            this._attributePropertiesMapping = new List<IAttributeMappingProperty>();
            this._attributePropertiesAllowMultipleMapping = new List<IAttributeMappingProperty>();
            this._mappingPropertyCache = null;  

            this._mappingPropertyMultipleColumnCache = null; 

            _columnMapping = new List<ColumnProperty>();
            _referenceColumnName = "";
            _referencePropertyName = "";
    }

        //Private
        private void LoadSettings()
        {

            //Initialing Mapping
            this._attributePropertiesMapping = new List<IAttributeMappingProperty>();
            this._attributePropertiesAllowMultipleMapping = new List<IAttributeMappingProperty>();
            this._mappingPropertyCache = null; //new List<MappingProperty>();

            this._mappingPropertyMultipleColumnCache = null; // new List<MappingProperty>();
            //Set MultipleColumnChar Separator
            this._multipleColumnCharSeparator = SetMultipleColumnCharSeparator();

            //Mapping Header Reader Source
            MappingColumnSource();

        }
        private int GetMaxValueMultipleColumnExpression(string column)
        {
            int resultValue = 0;
            //Regex regex = new Regex(@"\d+");
            Regex regex = new Regex(@"(?<=\" + this._multipleColumnCharSeparator + @")\d*");
            Match match = regex.Match(column);

            if (match.Success) { int.TryParse(match.Value, out resultValue); }
            else { resultValue = 0; }

            return resultValue;

        }
        private void ExecuteMappingPropertiesAttribute<T>()
        {
            try
            {

                PropertyInfo[] lstAttributeType = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);


                foreach (PropertyInfo pInfo in lstAttributeType)
                {

                    AttributeMappingProperty _propAttribute = pInfo.GetCustomAttributes(typeof(AttributeMappingProperty), true).FirstOrDefault() as AttributeMappingProperty;

                    if (_propAttribute != null)
                    {
                        if (_propAttribute.ReferenceOwner)
                        {
                            _referenceColumnName = _propAttribute.ReferenceOwnerPropertyName;
                            _referencePropertyName = _propAttribute.PropertyName;
                            //if (!this._reader.ExistColumn(this._referenceColumnName)) { throw new Exception("Reference ColumnName Not Exist"); }
                            if (this._dt.Columns[this._referenceColumnName] == null) { throw new Exception("Reference ColumnName Not Exist"); }
                        }
                        else if (!_propAttribute.AllowMultiple) { this._attributePropertiesMapping.Add(_propAttribute); }
                        else { this._attributePropertiesAllowMultipleMapping.Add(_propAttribute); }

                    }

                }


            }
            catch (Exception ex)
            {

                throw;
            }

        }
        private void MapObjectList<T>(IList destination, IDataReader dr) where T : new()
        {

            try
            {

                if (this._reader != null && destination != null)
                {
                    ((IList)destination).Clear();

                    T _classEntity = Lookup<T>();
                    Type typeT = typeof(T);


                    while (dr.Read())
                    {
                        _classEntity = new T();

                        for (int i = 0; i <= dr.FieldCount - 1; i++)
                        {

                            foreach (MappingProperty mProperty in MappingListProperties)
                            {
                                if (dr.GetName(i).Trim().Equals(mProperty.ColumnName, StringComparison.InvariantCultureIgnoreCase))
                                {

                                    if (mProperty.PropertyName != null || !string.IsNullOrEmpty(mProperty.PropertyName))
                                    {
                                        PropertyInfo pInfo = typeT.GetProperty(mProperty.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                                        if (dr.IsDBNull(i) == false)
                                        {
                                            MapProperty(_classEntity, pInfo, mProperty, dr.GetValue(i), false);
                                        }
                                        else
                                        {
                                            MapProperty(_classEntity, pInfo, mProperty, dr.GetValue(i), true);
                                        }

                                    }

                                }

                            }

                        }

                        //Add result to List
                        ((IList)destination).Add(_classEntity);

                    }

                    dr.Close();

                } //end 

            }
            catch (Exception ex)
            {

                throw new Exception($"MapObjectList Exception {ex.Message}", ex);
            }


        }
        private void MapObjectList<T>(IList destination, IDataReader dr, int blockSizeProperties) where T : new()
        {

            try
            {

                if (this._reader != null && destination != null)
                {
                    ((IList)destination).Clear();

                    //T _classEntity = Lookup<T>();
                    Type typeT = typeof(T);

                    List<MappingProperty> lstReverseProperty = new List<MappingProperty>();
                    MappingProperty propertyMapping = null;

                    object columnReferenceValue = null;


                    while (dr.Read())
                    {
                        //_classEntity = new T();

                        if (this.HasReferenceKey)
                        {
                            columnReferenceValue = dr[this._referenceColumnName];
                        }

                        for (int i = 0; i <= dr.FieldCount - 1; i++)
                        {

                            foreach (MappingProperty mProperty in this.MappingListPropertiesMultipleColumn)
                            {

                                if (dr.GetName(i).Trim().Equals(mProperty.ColumnName, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    propertyMapping = new MappingProperty();
                                    mProperty.CopyProperties(propertyMapping);

                                    //xx.ApplyLookupMapping = mProperty.ApplyLookupMapping;
                                    //xx.LookupMappingName = mProperty.LookupMappingName;
                                    //xx.IsMandatory = mProperty.IsMandatory;
                                    //xx.ColumnName = mProperty.ColumnName;
                                    //xx.PropertyName = mProperty.PropertyName;

                                    propertyMapping.ReferenceOwnerPropertyName = this.GetReferenceKey;
                                    propertyMapping.ReferenceOwner = this.HasReferenceKey;
                                    propertyMapping.ValueProperty = dr.GetValue(i);
                                    propertyMapping.ValueReferenceOwnerProperty = columnReferenceValue;
                                    propertyMapping.IsDBNull = dr.IsDBNull(i) ? true : false;
                                    lstReverseProperty.Add(propertyMapping);

                                    break;

                                }

                            }

                        }

                        MapObjectToList<T>(destination, lstReverseProperty, blockSizeProperties);
                        lstReverseProperty = new List<MappingProperty>();

                    }

                    dr.Close();

                } //end 

            }
            catch (Exception ex)
            {

                throw new Exception("MapObjectList Revert Exception", ex);
            }


        }
        private void MapObjectToList<TObject>(object destination, List<MappingProperty> propertyList, int maxBlockColumn) where TObject : new()
        {
            if (propertyList == null || propertyList.Count == 0) return;

            Type typeT = typeof(TObject);
            TObject result = Lookup<TObject>();

            foreach (var blockProperties in propertyList.ExtractBlock(maxBlockColumn))
            {

                result = new TObject();


                foreach (MappingProperty cProperty in blockProperties)
                {
                    PropertyInfo pInfoProperty = typeT.GetProperty(cProperty.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);


                    if (!cProperty.IsDBNull)
                    {
                        MapProperty(result, pInfoProperty, cProperty, cProperty.ValueProperty, false);

                    }
                    else
                    {
                        MapProperty(result, pInfoProperty, cProperty, cProperty.ValueProperty, true);
                    }



                }


                if (ValidateProperties<TObject>(result))
                {
                    //Set Value ReferenceBlock Properties
                    SetValueReferenceProperties(result, typeT, blockProperties.First().ValueReferenceOwnerProperty);

                    ((IList)destination).Add(result);

                }


            }

        }
        private void LookupMappingPropertyFailed(object destination, IAttributeMappingProperty mProperty, object value)
        {

            PropertyInfo pInfoProperty = destination.GetType().GetProperty(mProperty.LookupPropertyNameOverride, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            MapProperty(destination, pInfoProperty, value, false);



        }
        private void MapProperty(object destination, PropertyInfo pInfo, IAttributeMappingProperty pMapping, object value, bool isDBNull)
        {
            if (pInfo != null && pInfo.PropertyType != null)
            {
                try
                {
                    object valueMapping = value;

                    if (pMapping.ApplyLookupMapping)
                    {
                        valueMapping = null;
                        valueMapping = LookupMappingProperty(pMapping, value);

                        #region Override LookupMapping Failed
                        if (valueMapping == null && !string.IsNullOrEmpty(pMapping.LookupPropertyNameOverride))
                        {
                            //override 
                            LookupMappingPropertyFailed(destination, pMapping, value);
                            return;
                        }

                        #endregion

                    }

                    //Set Value Property 
                    MapProperty(destination, pInfo, valueMapping, isDBNull);


                }
                catch (Exception ex)
                {
                    if (pMapping.IsMandatory)
                    {
                        throw new Exception(string.Format("MapProperty Exception [{0}]", pInfo.Name), ex);
                    }

                }

            }

        }

        private void MapProperty(object destination, PropertyInfo pInfo, object valueMapping, bool isDBNull)
        {

            Type propertyType = pInfo.PropertyType;

            if (!isDBNull)
            {
                #region Set_Property_NOT_NULLABLE

                if (propertyType.IsGenericType && typeof(Nullable<>) == propertyType.GetGenericTypeDefinition())
                {
                    // se l'oggetto e' nullable

                    Type[] arguments = pInfo.PropertyType.GetGenericArguments();
                    Type genericArgumentType = typeof(object);
                    if (arguments != null && arguments.Length > 0)
                    {
                        genericArgumentType = arguments[0];
                    }


                    ConstructorInfo elemConstructor = pInfo.PropertyType.GetConstructor(new Type[] { genericArgumentType });


                    if (genericArgumentType == typeof(Decimal))
                    {
                        pInfo.SetValue(destination, elemConstructor.Invoke(new object[] { Convert.ToDecimal(valueMapping) }), null);
                    }
                    else
                    {
                        if (genericArgumentType == typeof(Int32))
                        {
                            pInfo.SetValue(destination, elemConstructor.Invoke(new object[] { Convert.ToInt32(valueMapping) }), null);
                        }
                        else
                        {
                            pInfo.SetValue(destination, elemConstructor.Invoke(new object[] { valueMapping }), null);
                        }

                    }


                }
                else
                {
                    #region Set Property

                    if (pInfo.PropertyType.IsEnum)
                    {
                        pInfo.SetValue(destination,
                            Enum.ToObject(pInfo.PropertyType, valueMapping),
                            null);

                    }
                    else
                    {
                        if (pInfo.PropertyType == typeof(string))
                        {
                            pInfo.SetValue(destination, valueMapping.ToString(), null);
                        }
                        else
                        {

                            if (pInfo.PropertyType == typeof(bool))
                            {
                                pInfo.SetValue(destination, Convert.ToBoolean(valueMapping), null);
                            }
                            else
                            {
                                if (pInfo.PropertyType == typeof(Guid))
                                {
                                    if (valueMapping != null && valueMapping.ToString().Trim() != "")
                                    {
                                        pInfo.SetValue(destination,
                                            new Guid(valueMapping.ToString()), null);
                                    }
                                    else
                                    {
                                        pInfo.SetValue(destination,
                                            Guid.Empty, null);
                                    }

                                }
                                else
                                {
                                    if (pInfo.PropertyType == typeof(Decimal))
                                    {
                                        pInfo.SetValue(destination,
                                            Convert.ToDecimal(valueMapping), null);

                                    }
                                    else
                                    {
                                        if (pInfo.PropertyType == typeof(Int32))
                                        {
                                            pInfo.SetValue(destination,
                                                Convert.ToInt32(valueMapping), null);

                                        }
                                        else
                                        {
                                            if (pInfo.PropertyType == typeof(DateTime))
                                            {
                                                pInfo.SetValue(destination, Convert.ToDateTime(valueMapping), null);

                                            }
                                            else { pInfo.SetValue(destination, valueMapping, null); }
                                        }

                                    }

                                }
                            }
                        }

                    }

                    #endregion

                }


                #endregion

            }
            else
            {
                #region  Set_Property_ISNULLABLE
                if (propertyType.IsGenericType && typeof(Nullable<>) == propertyType.GetGenericTypeDefinition())
                {
                    // DB Value is NUll like property value
                    pInfo.SetValue(destination, null, null);

                }
                else
                {
                    // valore db NULL ma l'oggetto NO
                    if (pInfo.PropertyType.IsEnum)
                    {

                        pInfo.SetValue(destination, Enum.ToObject(pInfo.PropertyType, 0), null);

                    }
                    else
                    {
                        pInfo.SetValue(destination, null, null);
                    }
                }


                #endregion

            }

        }
        private void SetValueReferenceProperties(object destination, Type pType, object value)
        {
            PropertyInfo pInfoReferenceProperty = pType.GetProperty(this._referencePropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            MappingProperty mProperty = new MappingProperty();
            mProperty.PropertyName = this._referencePropertyName;
            mProperty.ReferenceOwner = true;

            MapProperty(destination, pInfoReferenceProperty, mProperty, value, false);


        }

        //Properties
        protected List<IAttributeMappingProperty> PropertiesMapping { get { return this._attributePropertiesMapping; } }
        protected List<IAttributeMappingProperty> PropertiesAllowMultipleMapping { get { return this._attributePropertiesAllowMultipleMapping; } }
        protected List<ColumnProperty> ColumnsMapping { get { return this._columnMapping; } }
        protected IDataReader DataReaderSource { get { return this._reader; } }
        protected DataTable DataTableSource { get { return this._dt; } }
        protected bool HasReferenceKey
        {
            get
            {
                if (_referenceColumnName == null || string.IsNullOrEmpty(this._referenceColumnName)) return false;
                return true;
            }
        }
        protected string GetReferenceKey { get { return this._referenceColumnName; } }
        protected string GetReferenceProperty { get { return this._referenceColumnName; } }
        protected List<MappingProperty> MappingListProperties
        {
            get
            {
                if (this._mappingPropertyCache == null)
                {
                    this._mappingPropertyCache = this._attributePropertiesMapping.ConvertToListOf<MappingProperty>();

                }

                return this._mappingPropertyCache;

            }


        }
        protected List<MappingProperty> MappingListPropertiesMultipleColumn
        {
            get
            {
                if (this._mappingPropertyMultipleColumnCache == null)
                {
                    this._mappingPropertyMultipleColumnCache = this._attributePropertiesAllowMultipleMapping.ConvertToListOf<MappingProperty>();

                }

                return this._mappingPropertyMultipleColumnCache;

            }


        }
        protected virtual bool ValidateProperties<T>(T obj)
        {
            return true;
        }



        /*IDisposable Implementing */
        private bool _isDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_isDisposed)
                {

                    _reader.Dispose();
                    _reader = null;
                    //Do your unmanaged disposing here
                    _isDisposed = true;
                }
            }
        }
        ~MappingPropertiesGeneratorBase()
        {
            Dispose(false);
        }


    }
}
