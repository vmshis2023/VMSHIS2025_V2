using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
// <auto-generated />
namespace VietBaIT.HISLink.DataAccessLayer
{
	/// <summary>
	/// Strongly-typed collection for the LObjectTypePolicy class.
	/// </summary>
    [Serializable]
	public partial class LObjectTypePolicyCollection : ActiveList<LObjectTypePolicy, LObjectTypePolicyCollection>
	{	   
		public LObjectTypePolicyCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LObjectTypePolicyCollection</returns>
		public LObjectTypePolicyCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LObjectTypePolicy o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the L_ObjectType_Policies table.
	/// </summary>
	[Serializable]
	public partial class LObjectTypePolicy : ActiveRecord<LObjectTypePolicy>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LObjectTypePolicy()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LObjectTypePolicy(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LObjectTypePolicy(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LObjectTypePolicy(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("L_ObjectType_Policies", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarPoliciesId = new TableSchema.TableColumn(schema);
				colvarPoliciesId.ColumnName = "Policies_ID";
				colvarPoliciesId.DataType = DbType.Int16;
				colvarPoliciesId.MaxLength = 0;
				colvarPoliciesId.AutoIncrement = true;
				colvarPoliciesId.IsNullable = false;
				colvarPoliciesId.IsPrimaryKey = true;
				colvarPoliciesId.IsForeignKey = false;
				colvarPoliciesId.IsReadOnly = false;
				colvarPoliciesId.DefaultSetting = @"";
				colvarPoliciesId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPoliciesId);
				
				TableSchema.TableColumn colvarObjectTypeId = new TableSchema.TableColumn(schema);
				colvarObjectTypeId.ColumnName = "ObjectType_ID";
				colvarObjectTypeId.DataType = DbType.Int16;
				colvarObjectTypeId.MaxLength = 0;
				colvarObjectTypeId.AutoIncrement = false;
				colvarObjectTypeId.IsNullable = false;
				colvarObjectTypeId.IsPrimaryKey = false;
				colvarObjectTypeId.IsForeignKey = false;
				colvarObjectTypeId.IsReadOnly = false;
				colvarObjectTypeId.DefaultSetting = @"";
				colvarObjectTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarObjectTypeId);
				
				TableSchema.TableColumn colvarDiscountRate = new TableSchema.TableColumn(schema);
				colvarDiscountRate.ColumnName = "Discount_Rate";
				colvarDiscountRate.DataType = DbType.Decimal;
				colvarDiscountRate.MaxLength = 0;
				colvarDiscountRate.AutoIncrement = false;
				colvarDiscountRate.IsNullable = false;
				colvarDiscountRate.IsPrimaryKey = false;
				colvarDiscountRate.IsForeignKey = false;
				colvarDiscountRate.IsReadOnly = false;
				colvarDiscountRate.DefaultSetting = @"";
				colvarDiscountRate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDiscountRate);
				
				TableSchema.TableColumn colvarDiscountType = new TableSchema.TableColumn(schema);
				colvarDiscountType.ColumnName = "Discount_Type";
				colvarDiscountType.DataType = DbType.Byte;
				colvarDiscountType.MaxLength = 0;
				colvarDiscountType.AutoIncrement = false;
				colvarDiscountType.IsNullable = false;
				colvarDiscountType.IsPrimaryKey = false;
				colvarDiscountType.IsForeignKey = false;
				colvarDiscountType.IsReadOnly = false;
				colvarDiscountType.DefaultSetting = @"";
				colvarDiscountType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDiscountType);
				
				TableSchema.TableColumn colvarLevel = new TableSchema.TableColumn(schema);
				colvarLevel.ColumnName = "Level";
				colvarLevel.DataType = DbType.Byte;
				colvarLevel.MaxLength = 0;
				colvarLevel.AutoIncrement = false;
				colvarLevel.IsNullable = true;
				colvarLevel.IsPrimaryKey = false;
				colvarLevel.IsForeignKey = false;
				colvarLevel.IsReadOnly = false;
				colvarLevel.DefaultSetting = @"";
				colvarLevel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLevel);
				
				TableSchema.TableColumn colvarSDesc = new TableSchema.TableColumn(schema);
				colvarSDesc.ColumnName = "sDesc";
				colvarSDesc.DataType = DbType.String;
				colvarSDesc.MaxLength = 255;
				colvarSDesc.AutoIncrement = false;
				colvarSDesc.IsNullable = true;
				colvarSDesc.IsPrimaryKey = false;
				colvarSDesc.IsForeignKey = false;
				colvarSDesc.IsReadOnly = false;
				colvarSDesc.DefaultSetting = @"";
				colvarSDesc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSDesc);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("L_ObjectType_Policies",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("PoliciesId")]
		[Bindable(true)]
		public short PoliciesId 
		{
			get { return GetColumnValue<short>(Columns.PoliciesId); }
			set { SetColumnValue(Columns.PoliciesId, value); }
		}
		  
		[XmlAttribute("ObjectTypeId")]
		[Bindable(true)]
		public short ObjectTypeId 
		{
			get { return GetColumnValue<short>(Columns.ObjectTypeId); }
			set { SetColumnValue(Columns.ObjectTypeId, value); }
		}
		  
		[XmlAttribute("DiscountRate")]
		[Bindable(true)]
		public decimal DiscountRate 
		{
			get { return GetColumnValue<decimal>(Columns.DiscountRate); }
			set { SetColumnValue(Columns.DiscountRate, value); }
		}
		  
		[XmlAttribute("DiscountType")]
		[Bindable(true)]
		public byte DiscountType 
		{
			get { return GetColumnValue<byte>(Columns.DiscountType); }
			set { SetColumnValue(Columns.DiscountType, value); }
		}
		  
		[XmlAttribute("Level")]
		[Bindable(true)]
		public byte? Level 
		{
			get { return GetColumnValue<byte?>(Columns.Level); }
			set { SetColumnValue(Columns.Level, value); }
		}
		  
		[XmlAttribute("SDesc")]
		[Bindable(true)]
		public string SDesc 
		{
			get { return GetColumnValue<string>(Columns.SDesc); }
			set { SetColumnValue(Columns.SDesc, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(short varObjectTypeId,decimal varDiscountRate,byte varDiscountType,byte? varLevel,string varSDesc)
		{
			LObjectTypePolicy item = new LObjectTypePolicy();
			
			item.ObjectTypeId = varObjectTypeId;
			
			item.DiscountRate = varDiscountRate;
			
			item.DiscountType = varDiscountType;
			
			item.Level = varLevel;
			
			item.SDesc = varSDesc;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(short varPoliciesId,short varObjectTypeId,decimal varDiscountRate,byte varDiscountType,byte? varLevel,string varSDesc)
		{
			LObjectTypePolicy item = new LObjectTypePolicy();
			
				item.PoliciesId = varPoliciesId;
			
				item.ObjectTypeId = varObjectTypeId;
			
				item.DiscountRate = varDiscountRate;
			
				item.DiscountType = varDiscountType;
			
				item.Level = varLevel;
			
				item.SDesc = varSDesc;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn PoliciesIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn ObjectTypeIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn DiscountRateColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn DiscountTypeColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn LevelColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn SDescColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string PoliciesId = @"Policies_ID";
			 public static string ObjectTypeId = @"ObjectType_ID";
			 public static string DiscountRate = @"Discount_Rate";
			 public static string DiscountType = @"Discount_Type";
			 public static string Level = @"Level";
			 public static string SDesc = @"sDesc";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
