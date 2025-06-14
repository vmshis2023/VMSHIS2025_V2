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
	/// Strongly-typed collection for the LMaterialTemplate class.
	/// </summary>
    [Serializable]
	public partial class LMaterialTemplateCollection : ActiveList<LMaterialTemplate, LMaterialTemplateCollection>
	{	   
		public LMaterialTemplateCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LMaterialTemplateCollection</returns>
		public LMaterialTemplateCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LMaterialTemplate o = this[i];
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
	/// This is an ActiveRecord class which wraps the L_Material_Template table.
	/// </summary>
	[Serializable]
	public partial class LMaterialTemplate : ActiveRecord<LMaterialTemplate>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LMaterialTemplate()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LMaterialTemplate(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LMaterialTemplate(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LMaterialTemplate(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("L_Material_Template", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarSamMaterialId = new TableSchema.TableColumn(schema);
				colvarSamMaterialId.ColumnName = "SamMaterial_Id";
				colvarSamMaterialId.DataType = DbType.Int32;
				colvarSamMaterialId.MaxLength = 0;
				colvarSamMaterialId.AutoIncrement = true;
				colvarSamMaterialId.IsNullable = false;
				colvarSamMaterialId.IsPrimaryKey = true;
				colvarSamMaterialId.IsForeignKey = false;
				colvarSamMaterialId.IsReadOnly = false;
				colvarSamMaterialId.DefaultSetting = @"";
				colvarSamMaterialId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSamMaterialId);
				
				TableSchema.TableColumn colvarSamMaterialCode = new TableSchema.TableColumn(schema);
				colvarSamMaterialCode.ColumnName = "SamMaterial_Code";
				colvarSamMaterialCode.DataType = DbType.String;
				colvarSamMaterialCode.MaxLength = 100;
				colvarSamMaterialCode.AutoIncrement = false;
				colvarSamMaterialCode.IsNullable = true;
				colvarSamMaterialCode.IsPrimaryKey = false;
				colvarSamMaterialCode.IsForeignKey = false;
				colvarSamMaterialCode.IsReadOnly = false;
				colvarSamMaterialCode.DefaultSetting = @"";
				colvarSamMaterialCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSamMaterialCode);
				
				TableSchema.TableColumn colvarSamMaterialName = new TableSchema.TableColumn(schema);
				colvarSamMaterialName.ColumnName = "SamMaterial_Name";
				colvarSamMaterialName.DataType = DbType.String;
				colvarSamMaterialName.MaxLength = 100;
				colvarSamMaterialName.AutoIncrement = false;
				colvarSamMaterialName.IsNullable = true;
				colvarSamMaterialName.IsPrimaryKey = false;
				colvarSamMaterialName.IsForeignKey = false;
				colvarSamMaterialName.IsReadOnly = false;
				colvarSamMaterialName.DefaultSetting = @"";
				colvarSamMaterialName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSamMaterialName);
				
				TableSchema.TableColumn colvarParentId = new TableSchema.TableColumn(schema);
				colvarParentId.ColumnName = "Parent_Id";
				colvarParentId.DataType = DbType.Int32;
				colvarParentId.MaxLength = 0;
				colvarParentId.AutoIncrement = false;
				colvarParentId.IsNullable = true;
				colvarParentId.IsPrimaryKey = false;
				colvarParentId.IsForeignKey = false;
				colvarParentId.IsReadOnly = false;
				
						colvarParentId.DefaultSetting = @"((-1))";
				colvarParentId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarParentId);
				
				TableSchema.TableColumn colvarMaterialTypeId = new TableSchema.TableColumn(schema);
				colvarMaterialTypeId.ColumnName = "MaterialType_Id";
				colvarMaterialTypeId.DataType = DbType.Int32;
				colvarMaterialTypeId.MaxLength = 0;
				colvarMaterialTypeId.AutoIncrement = false;
				colvarMaterialTypeId.IsNullable = true;
				colvarMaterialTypeId.IsPrimaryKey = false;
				colvarMaterialTypeId.IsForeignKey = false;
				colvarMaterialTypeId.IsReadOnly = false;
				colvarMaterialTypeId.DefaultSetting = @"";
				colvarMaterialTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaterialTypeId);
				
				TableSchema.TableColumn colvarMaterialId = new TableSchema.TableColumn(schema);
				colvarMaterialId.ColumnName = "Material_Id";
				colvarMaterialId.DataType = DbType.Int32;
				colvarMaterialId.MaxLength = 0;
				colvarMaterialId.AutoIncrement = false;
				colvarMaterialId.IsNullable = true;
				colvarMaterialId.IsPrimaryKey = false;
				colvarMaterialId.IsForeignKey = false;
				colvarMaterialId.IsReadOnly = false;
				colvarMaterialId.DefaultSetting = @"";
				colvarMaterialId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaterialId);
				
				TableSchema.TableColumn colvarQuantity = new TableSchema.TableColumn(schema);
				colvarQuantity.ColumnName = "Quantity";
				colvarQuantity.DataType = DbType.Decimal;
				colvarQuantity.MaxLength = 0;
				colvarQuantity.AutoIncrement = false;
				colvarQuantity.IsNullable = true;
				colvarQuantity.IsPrimaryKey = false;
				colvarQuantity.IsForeignKey = false;
				colvarQuantity.IsReadOnly = false;
				colvarQuantity.DefaultSetting = @"";
				colvarQuantity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarQuantity);
				
				TableSchema.TableColumn colvarCreateDate = new TableSchema.TableColumn(schema);
				colvarCreateDate.ColumnName = "Create_Date";
				colvarCreateDate.DataType = DbType.DateTime;
				colvarCreateDate.MaxLength = 0;
				colvarCreateDate.AutoIncrement = false;
				colvarCreateDate.IsNullable = true;
				colvarCreateDate.IsPrimaryKey = false;
				colvarCreateDate.IsForeignKey = false;
				colvarCreateDate.IsReadOnly = false;
				
						colvarCreateDate.DefaultSetting = @"(getdate())";
				colvarCreateDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreateDate);
				
				TableSchema.TableColumn colvarCreateBy = new TableSchema.TableColumn(schema);
				colvarCreateBy.ColumnName = "Create_By";
				colvarCreateBy.DataType = DbType.String;
				colvarCreateBy.MaxLength = 50;
				colvarCreateBy.AutoIncrement = false;
				colvarCreateBy.IsNullable = true;
				colvarCreateBy.IsPrimaryKey = false;
				colvarCreateBy.IsForeignKey = false;
				colvarCreateBy.IsReadOnly = false;
				colvarCreateBy.DefaultSetting = @"";
				colvarCreateBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreateBy);
				
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
				DataService.Providers["ORM"].AddSchema("L_Material_Template",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("SamMaterialId")]
		[Bindable(true)]
		public int SamMaterialId 
		{
			get { return GetColumnValue<int>(Columns.SamMaterialId); }
			set { SetColumnValue(Columns.SamMaterialId, value); }
		}
		  
		[XmlAttribute("SamMaterialCode")]
		[Bindable(true)]
		public string SamMaterialCode 
		{
			get { return GetColumnValue<string>(Columns.SamMaterialCode); }
			set { SetColumnValue(Columns.SamMaterialCode, value); }
		}
		  
		[XmlAttribute("SamMaterialName")]
		[Bindable(true)]
		public string SamMaterialName 
		{
			get { return GetColumnValue<string>(Columns.SamMaterialName); }
			set { SetColumnValue(Columns.SamMaterialName, value); }
		}
		  
		[XmlAttribute("ParentId")]
		[Bindable(true)]
		public int? ParentId 
		{
			get { return GetColumnValue<int?>(Columns.ParentId); }
			set { SetColumnValue(Columns.ParentId, value); }
		}
		  
		[XmlAttribute("MaterialTypeId")]
		[Bindable(true)]
		public int? MaterialTypeId 
		{
			get { return GetColumnValue<int?>(Columns.MaterialTypeId); }
			set { SetColumnValue(Columns.MaterialTypeId, value); }
		}
		  
		[XmlAttribute("MaterialId")]
		[Bindable(true)]
		public int? MaterialId 
		{
			get { return GetColumnValue<int?>(Columns.MaterialId); }
			set { SetColumnValue(Columns.MaterialId, value); }
		}
		  
		[XmlAttribute("Quantity")]
		[Bindable(true)]
		public decimal? Quantity 
		{
			get { return GetColumnValue<decimal?>(Columns.Quantity); }
			set { SetColumnValue(Columns.Quantity, value); }
		}
		  
		[XmlAttribute("CreateDate")]
		[Bindable(true)]
		public DateTime? CreateDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.CreateDate); }
			set { SetColumnValue(Columns.CreateDate, value); }
		}
		  
		[XmlAttribute("CreateBy")]
		[Bindable(true)]
		public string CreateBy 
		{
			get { return GetColumnValue<string>(Columns.CreateBy); }
			set { SetColumnValue(Columns.CreateBy, value); }
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
		public static void Insert(string varSamMaterialCode,string varSamMaterialName,int? varParentId,int? varMaterialTypeId,int? varMaterialId,decimal? varQuantity,DateTime? varCreateDate,string varCreateBy,string varSDesc)
		{
			LMaterialTemplate item = new LMaterialTemplate();
			
			item.SamMaterialCode = varSamMaterialCode;
			
			item.SamMaterialName = varSamMaterialName;
			
			item.ParentId = varParentId;
			
			item.MaterialTypeId = varMaterialTypeId;
			
			item.MaterialId = varMaterialId;
			
			item.Quantity = varQuantity;
			
			item.CreateDate = varCreateDate;
			
			item.CreateBy = varCreateBy;
			
			item.SDesc = varSDesc;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varSamMaterialId,string varSamMaterialCode,string varSamMaterialName,int? varParentId,int? varMaterialTypeId,int? varMaterialId,decimal? varQuantity,DateTime? varCreateDate,string varCreateBy,string varSDesc)
		{
			LMaterialTemplate item = new LMaterialTemplate();
			
				item.SamMaterialId = varSamMaterialId;
			
				item.SamMaterialCode = varSamMaterialCode;
			
				item.SamMaterialName = varSamMaterialName;
			
				item.ParentId = varParentId;
			
				item.MaterialTypeId = varMaterialTypeId;
			
				item.MaterialId = varMaterialId;
			
				item.Quantity = varQuantity;
			
				item.CreateDate = varCreateDate;
			
				item.CreateBy = varCreateBy;
			
				item.SDesc = varSDesc;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn SamMaterialIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn SamMaterialCodeColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn SamMaterialNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn ParentIdColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn MaterialTypeIdColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn MaterialIdColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn QuantityColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateDateColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateByColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn SDescColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string SamMaterialId = @"SamMaterial_Id";
			 public static string SamMaterialCode = @"SamMaterial_Code";
			 public static string SamMaterialName = @"SamMaterial_Name";
			 public static string ParentId = @"Parent_Id";
			 public static string MaterialTypeId = @"MaterialType_Id";
			 public static string MaterialId = @"Material_Id";
			 public static string Quantity = @"Quantity";
			 public static string CreateDate = @"Create_Date";
			 public static string CreateBy = @"Create_By";
			 public static string SDesc = @"sDesc";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
