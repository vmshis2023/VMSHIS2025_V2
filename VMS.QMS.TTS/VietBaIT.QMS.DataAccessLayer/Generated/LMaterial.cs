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
	/// Strongly-typed collection for the LMaterial class.
	/// </summary>
    [Serializable]
	public partial class LMaterialCollection : ActiveList<LMaterial, LMaterialCollection>
	{	   
		public LMaterialCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LMaterialCollection</returns>
		public LMaterialCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LMaterial o = this[i];
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
	/// This is an ActiveRecord class which wraps the L_Materials table.
	/// </summary>
	[Serializable]
	public partial class LMaterial : ActiveRecord<LMaterial>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LMaterial()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LMaterial(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LMaterial(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LMaterial(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("L_Materials", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarMaterialId = new TableSchema.TableColumn(schema);
				colvarMaterialId.ColumnName = "Material_ID";
				colvarMaterialId.DataType = DbType.Int32;
				colvarMaterialId.MaxLength = 0;
				colvarMaterialId.AutoIncrement = true;
				colvarMaterialId.IsNullable = false;
				colvarMaterialId.IsPrimaryKey = true;
				colvarMaterialId.IsForeignKey = false;
				colvarMaterialId.IsReadOnly = false;
				colvarMaterialId.DefaultSetting = @"";
				colvarMaterialId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaterialId);
				
				TableSchema.TableColumn colvarMaterialTypeId = new TableSchema.TableColumn(schema);
				colvarMaterialTypeId.ColumnName = "MaterialType_ID";
				colvarMaterialTypeId.DataType = DbType.Int16;
				colvarMaterialTypeId.MaxLength = 0;
				colvarMaterialTypeId.AutoIncrement = false;
				colvarMaterialTypeId.IsNullable = false;
				colvarMaterialTypeId.IsPrimaryKey = false;
				colvarMaterialTypeId.IsForeignKey = false;
				colvarMaterialTypeId.IsReadOnly = false;
				colvarMaterialTypeId.DefaultSetting = @"";
				colvarMaterialTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaterialTypeId);
				
				TableSchema.TableColumn colvarUnitId = new TableSchema.TableColumn(schema);
				colvarUnitId.ColumnName = "Unit_ID";
				colvarUnitId.DataType = DbType.Int16;
				colvarUnitId.MaxLength = 0;
				colvarUnitId.AutoIncrement = false;
				colvarUnitId.IsNullable = false;
				colvarUnitId.IsPrimaryKey = false;
				colvarUnitId.IsForeignKey = false;
				colvarUnitId.IsReadOnly = false;
				colvarUnitId.DefaultSetting = @"";
				colvarUnitId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUnitId);
				
				TableSchema.TableColumn colvarMaterialCode = new TableSchema.TableColumn(schema);
				colvarMaterialCode.ColumnName = "Material_Code";
				colvarMaterialCode.DataType = DbType.String;
				colvarMaterialCode.MaxLength = 100;
				colvarMaterialCode.AutoIncrement = false;
				colvarMaterialCode.IsNullable = true;
				colvarMaterialCode.IsPrimaryKey = false;
				colvarMaterialCode.IsForeignKey = false;
				colvarMaterialCode.IsReadOnly = false;
				colvarMaterialCode.DefaultSetting = @"";
				colvarMaterialCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaterialCode);
				
				TableSchema.TableColumn colvarMaterialName = new TableSchema.TableColumn(schema);
				colvarMaterialName.ColumnName = "Material_Name";
				colvarMaterialName.DataType = DbType.String;
				colvarMaterialName.MaxLength = 100;
				colvarMaterialName.AutoIncrement = false;
				colvarMaterialName.IsNullable = false;
				colvarMaterialName.IsPrimaryKey = false;
				colvarMaterialName.IsForeignKey = false;
				colvarMaterialName.IsReadOnly = false;
				colvarMaterialName.DefaultSetting = @"";
				colvarMaterialName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaterialName);
				
				TableSchema.TableColumn colvarMaterialFee = new TableSchema.TableColumn(schema);
				colvarMaterialFee.ColumnName = "Material_Fee";
				colvarMaterialFee.DataType = DbType.Currency;
				colvarMaterialFee.MaxLength = 0;
				colvarMaterialFee.AutoIncrement = false;
				colvarMaterialFee.IsNullable = true;
				colvarMaterialFee.IsPrimaryKey = false;
				colvarMaterialFee.IsForeignKey = false;
				colvarMaterialFee.IsReadOnly = false;
				colvarMaterialFee.DefaultSetting = @"";
				colvarMaterialFee.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaterialFee);
				
				TableSchema.TableColumn colvarIntOrder = new TableSchema.TableColumn(schema);
				colvarIntOrder.ColumnName = "IntOrder";
				colvarIntOrder.DataType = DbType.Byte;
				colvarIntOrder.MaxLength = 0;
				colvarIntOrder.AutoIncrement = false;
				colvarIntOrder.IsNullable = true;
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
				DataService.Providers["ORM"].AddSchema("L_Materials",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("MaterialId")]
		[Bindable(true)]
		public int MaterialId 
		{
			get { return GetColumnValue<int>(Columns.MaterialId); }
			set { SetColumnValue(Columns.MaterialId, value); }
		}
		  
		[XmlAttribute("MaterialTypeId")]
		[Bindable(true)]
		public short MaterialTypeId 
		{
			get { return GetColumnValue<short>(Columns.MaterialTypeId); }
			set { SetColumnValue(Columns.MaterialTypeId, value); }
		}
		  
		[XmlAttribute("UnitId")]
		[Bindable(true)]
		public short UnitId 
		{
			get { return GetColumnValue<short>(Columns.UnitId); }
			set { SetColumnValue(Columns.UnitId, value); }
		}
		  
		[XmlAttribute("MaterialCode")]
		[Bindable(true)]
		public string MaterialCode 
		{
			get { return GetColumnValue<string>(Columns.MaterialCode); }
			set { SetColumnValue(Columns.MaterialCode, value); }
		}
		  
		[XmlAttribute("MaterialName")]
		[Bindable(true)]
		public string MaterialName 
		{
			get { return GetColumnValue<string>(Columns.MaterialName); }
			set { SetColumnValue(Columns.MaterialName, value); }
		}
		  
		[XmlAttribute("MaterialFee")]
		[Bindable(true)]
		public decimal? MaterialFee 
		{
			get { return GetColumnValue<decimal?>(Columns.MaterialFee); }
			set { SetColumnValue(Columns.MaterialFee, value); }
		}
		  
		[XmlAttribute("IntOrder")]
		[Bindable(true)]
		public byte? IntOrder 
		{
			get { return GetColumnValue<byte?>(Columns.IntOrder); }
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
		public static void Insert(short varMaterialTypeId,short varUnitId,string varMaterialCode,string varMaterialName,decimal? varMaterialFee,byte? varIntOrder,string varSDesc)
		{
			LMaterial item = new LMaterial();
			
			item.MaterialTypeId = varMaterialTypeId;
			
			item.UnitId = varUnitId;
			
			item.MaterialCode = varMaterialCode;
			
			item.MaterialName = varMaterialName;
			
			item.MaterialFee = varMaterialFee;
			
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
		public static void Update(int varMaterialId,short varMaterialTypeId,short varUnitId,string varMaterialCode,string varMaterialName,decimal? varMaterialFee,byte? varIntOrder,string varSDesc)
		{
			LMaterial item = new LMaterial();
			
				item.MaterialId = varMaterialId;
			
				item.MaterialTypeId = varMaterialTypeId;
			
				item.UnitId = varUnitId;
			
				item.MaterialCode = varMaterialCode;
			
				item.MaterialName = varMaterialName;
			
				item.MaterialFee = varMaterialFee;
			
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
        
        
        public static TableSchema.TableColumn MaterialIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaterialTypeIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn UnitIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn MaterialCodeColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn MaterialNameColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn MaterialFeeColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn IntOrderColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn SDescColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string MaterialId = @"Material_ID";
			 public static string MaterialTypeId = @"MaterialType_ID";
			 public static string UnitId = @"Unit_ID";
			 public static string MaterialCode = @"Material_Code";
			 public static string MaterialName = @"Material_Name";
			 public static string MaterialFee = @"Material_Fee";
			 public static string IntOrder = @"IntOrder";
			 public static string SDesc = @"sDesc";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
