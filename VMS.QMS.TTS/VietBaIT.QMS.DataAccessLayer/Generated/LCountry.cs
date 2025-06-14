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
	/// Strongly-typed collection for the LCountry class.
	/// </summary>
    [Serializable]
	public partial class LCountryCollection : ActiveList<LCountry, LCountryCollection>
	{	   
		public LCountryCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LCountryCollection</returns>
		public LCountryCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LCountry o = this[i];
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
	/// This is an ActiveRecord class which wraps the L_Countries table.
	/// </summary>
	[Serializable]
	public partial class LCountry : ActiveRecord<LCountry>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LCountry()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LCountry(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LCountry(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LCountry(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("L_Countries", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarCountryId = new TableSchema.TableColumn(schema);
				colvarCountryId.ColumnName = "Country_ID";
				colvarCountryId.DataType = DbType.Int16;
				colvarCountryId.MaxLength = 0;
				colvarCountryId.AutoIncrement = true;
				colvarCountryId.IsNullable = false;
				colvarCountryId.IsPrimaryKey = true;
				colvarCountryId.IsForeignKey = false;
				colvarCountryId.IsReadOnly = false;
				colvarCountryId.DefaultSetting = @"";
				colvarCountryId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountryId);
				
				TableSchema.TableColumn colvarCounytryCode = new TableSchema.TableColumn(schema);
				colvarCounytryCode.ColumnName = "Counytry_Code";
				colvarCounytryCode.DataType = DbType.AnsiString;
				colvarCounytryCode.MaxLength = 10;
				colvarCounytryCode.AutoIncrement = false;
				colvarCounytryCode.IsNullable = false;
				colvarCounytryCode.IsPrimaryKey = false;
				colvarCounytryCode.IsForeignKey = false;
				colvarCounytryCode.IsReadOnly = false;
				colvarCounytryCode.DefaultSetting = @"";
				colvarCounytryCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCounytryCode);
				
				TableSchema.TableColumn colvarCountryName = new TableSchema.TableColumn(schema);
				colvarCountryName.ColumnName = "Country_Name";
				colvarCountryName.DataType = DbType.String;
				colvarCountryName.MaxLength = 100;
				colvarCountryName.AutoIncrement = false;
				colvarCountryName.IsNullable = false;
				colvarCountryName.IsPrimaryKey = false;
				colvarCountryName.IsForeignKey = false;
				colvarCountryName.IsReadOnly = false;
				colvarCountryName.DefaultSetting = @"";
				colvarCountryName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountryName);
				
				TableSchema.TableColumn colvarIntOrder = new TableSchema.TableColumn(schema);
				colvarIntOrder.ColumnName = "intOrder";
				colvarIntOrder.DataType = DbType.Int16;
				colvarIntOrder.MaxLength = 0;
				colvarIntOrder.AutoIncrement = false;
				colvarIntOrder.IsNullable = false;
				colvarIntOrder.IsPrimaryKey = false;
				colvarIntOrder.IsForeignKey = false;
				colvarIntOrder.IsReadOnly = false;
				colvarIntOrder.DefaultSetting = @"";
				colvarIntOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIntOrder);
				
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
				DataService.Providers["ORM"].AddSchema("L_Countries",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("CountryId")]
		[Bindable(true)]
		public short CountryId 
		{
			get { return GetColumnValue<short>(Columns.CountryId); }
			set { SetColumnValue(Columns.CountryId, value); }
		}
		  
		[XmlAttribute("CounytryCode")]
		[Bindable(true)]
		public string CounytryCode 
		{
			get { return GetColumnValue<string>(Columns.CounytryCode); }
			set { SetColumnValue(Columns.CounytryCode, value); }
		}
		  
		[XmlAttribute("CountryName")]
		[Bindable(true)]
		public string CountryName 
		{
			get { return GetColumnValue<string>(Columns.CountryName); }
			set { SetColumnValue(Columns.CountryName, value); }
		}
		  
		[XmlAttribute("IntOrder")]
		[Bindable(true)]
		public short IntOrder 
		{
			get { return GetColumnValue<short>(Columns.IntOrder); }
			set { SetColumnValue(Columns.IntOrder, value); }
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
		public static void Insert(string varCounytryCode,string varCountryName,short varIntOrder,string varSDesc)
		{
			LCountry item = new LCountry();
			
			item.CounytryCode = varCounytryCode;
			
			item.CountryName = varCountryName;
			
			item.IntOrder = varIntOrder;
			
			item.SDesc = varSDesc;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(short varCountryId,string varCounytryCode,string varCountryName,short varIntOrder,string varSDesc)
		{
			LCountry item = new LCountry();
			
				item.CountryId = varCountryId;
			
				item.CounytryCode = varCounytryCode;
			
				item.CountryName = varCountryName;
			
				item.IntOrder = varIntOrder;
			
				item.SDesc = varSDesc;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn CountryIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn CounytryCodeColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn CountryNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IntOrderColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SDescColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string CountryId = @"Country_ID";
			 public static string CounytryCode = @"Counytry_Code";
			 public static string CountryName = @"Country_Name";
			 public static string IntOrder = @"intOrder";
			 public static string SDesc = @"sDesc";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
