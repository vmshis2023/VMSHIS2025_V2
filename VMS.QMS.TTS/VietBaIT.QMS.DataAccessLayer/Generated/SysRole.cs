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
	/// Strongly-typed collection for the SysRole class.
	/// </summary>
    [Serializable]
	public partial class SysRoleCollection : ActiveList<SysRole, SysRoleCollection>
	{	   
		public SysRoleCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysRoleCollection</returns>
		public SysRoleCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysRole o = this[i];
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
	/// This is an ActiveRecord class which wraps the Sys_Roles table.
	/// </summary>
	[Serializable]
	public partial class SysRole : ActiveRecord<SysRole>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public SysRole()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysRole(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysRole(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysRole(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("Sys_Roles", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIRole = new TableSchema.TableColumn(schema);
				colvarIRole.ColumnName = "iRole";
				colvarIRole.DataType = DbType.Int64;
				colvarIRole.MaxLength = 0;
				colvarIRole.AutoIncrement = true;
				colvarIRole.IsNullable = false;
				colvarIRole.IsPrimaryKey = true;
				colvarIRole.IsForeignKey = false;
				colvarIRole.IsReadOnly = false;
				colvarIRole.DefaultSetting = @"";
				colvarIRole.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIRole);
				
				TableSchema.TableColumn colvarFpSBranchID = new TableSchema.TableColumn(schema);
				colvarFpSBranchID.ColumnName = "FP_sBranchID";
				colvarFpSBranchID.DataType = DbType.String;
				colvarFpSBranchID.MaxLength = 10;
				colvarFpSBranchID.AutoIncrement = false;
				colvarFpSBranchID.IsNullable = false;
				colvarFpSBranchID.IsPrimaryKey = true;
				colvarFpSBranchID.IsForeignKey = false;
				colvarFpSBranchID.IsReadOnly = false;
				colvarFpSBranchID.DefaultSetting = @"";
				colvarFpSBranchID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFpSBranchID);
				
				TableSchema.TableColumn colvarIParentRole = new TableSchema.TableColumn(schema);
				colvarIParentRole.ColumnName = "iParentRole";
				colvarIParentRole.DataType = DbType.Int64;
				colvarIParentRole.MaxLength = 0;
				colvarIParentRole.AutoIncrement = false;
				colvarIParentRole.IsNullable = false;
				colvarIParentRole.IsPrimaryKey = false;
				colvarIParentRole.IsForeignKey = false;
				colvarIParentRole.IsReadOnly = false;
				colvarIParentRole.DefaultSetting = @"";
				colvarIParentRole.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIParentRole);
				
				TableSchema.TableColumn colvarSRoleName = new TableSchema.TableColumn(schema);
				colvarSRoleName.ColumnName = "sRoleName";
				colvarSRoleName.DataType = DbType.String;
				colvarSRoleName.MaxLength = 100;
				colvarSRoleName.AutoIncrement = false;
				colvarSRoleName.IsNullable = false;
				colvarSRoleName.IsPrimaryKey = false;
				colvarSRoleName.IsForeignKey = false;
				colvarSRoleName.IsReadOnly = false;
				colvarSRoleName.DefaultSetting = @"";
				colvarSRoleName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSRoleName);
				
				TableSchema.TableColumn colvarSEngRoleName = new TableSchema.TableColumn(schema);
				colvarSEngRoleName.ColumnName = "sEngRoleName";
				colvarSEngRoleName.DataType = DbType.String;
				colvarSEngRoleName.MaxLength = 100;
				colvarSEngRoleName.AutoIncrement = false;
				colvarSEngRoleName.IsNullable = true;
				colvarSEngRoleName.IsPrimaryKey = false;
				colvarSEngRoleName.IsForeignKey = false;
				colvarSEngRoleName.IsReadOnly = false;
				colvarSEngRoleName.DefaultSetting = @"";
				colvarSEngRoleName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSEngRoleName);
				
				TableSchema.TableColumn colvarIOrder = new TableSchema.TableColumn(schema);
				colvarIOrder.ColumnName = "iOrder";
				colvarIOrder.DataType = DbType.Int32;
				colvarIOrder.MaxLength = 0;
				colvarIOrder.AutoIncrement = false;
				colvarIOrder.IsNullable = true;
				colvarIOrder.IsPrimaryKey = false;
				colvarIOrder.IsForeignKey = false;
				colvarIOrder.IsReadOnly = false;
				colvarIOrder.DefaultSetting = @"";
				colvarIOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIOrder);
				
				TableSchema.TableColumn colvarFkIFunctionID = new TableSchema.TableColumn(schema);
				colvarFkIFunctionID.ColumnName = "FK_iFunctionID";
				colvarFkIFunctionID.DataType = DbType.Int64;
				colvarFkIFunctionID.MaxLength = 0;
				colvarFkIFunctionID.AutoIncrement = false;
				colvarFkIFunctionID.IsNullable = true;
				colvarFkIFunctionID.IsPrimaryKey = false;
				colvarFkIFunctionID.IsForeignKey = false;
				colvarFkIFunctionID.IsReadOnly = false;
				colvarFkIFunctionID.DefaultSetting = @"";
				colvarFkIFunctionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFkIFunctionID);
				
				TableSchema.TableColumn colvarTDateCreated = new TableSchema.TableColumn(schema);
				colvarTDateCreated.ColumnName = "tDateCreated";
				colvarTDateCreated.DataType = DbType.DateTime;
				colvarTDateCreated.MaxLength = 0;
				colvarTDateCreated.AutoIncrement = false;
				colvarTDateCreated.IsNullable = true;
				colvarTDateCreated.IsPrimaryKey = false;
				colvarTDateCreated.IsForeignKey = false;
				colvarTDateCreated.IsReadOnly = false;
				colvarTDateCreated.DefaultSetting = @"";
				colvarTDateCreated.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTDateCreated);
				
				TableSchema.TableColumn colvarSImgPath = new TableSchema.TableColumn(schema);
				colvarSImgPath.ColumnName = "sImgPath";
				colvarSImgPath.DataType = DbType.String;
				colvarSImgPath.MaxLength = 255;
				colvarSImgPath.AutoIncrement = false;
				colvarSImgPath.IsNullable = true;
				colvarSImgPath.IsPrimaryKey = false;
				colvarSImgPath.IsForeignKey = false;
				colvarSImgPath.IsReadOnly = false;
				colvarSImgPath.DefaultSetting = @"";
				colvarSImgPath.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSImgPath);
				
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
				
				TableSchema.TableColumn colvarSIconPath = new TableSchema.TableColumn(schema);
				colvarSIconPath.ColumnName = "sIconPath";
				colvarSIconPath.DataType = DbType.String;
				colvarSIconPath.MaxLength = 255;
				colvarSIconPath.AutoIncrement = false;
				colvarSIconPath.IsNullable = true;
				colvarSIconPath.IsPrimaryKey = false;
				colvarSIconPath.IsForeignKey = false;
				colvarSIconPath.IsReadOnly = false;
				colvarSIconPath.DefaultSetting = @"";
				colvarSIconPath.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSIconPath);
				
				TableSchema.TableColumn colvarIntShortCutKey = new TableSchema.TableColumn(schema);
				colvarIntShortCutKey.ColumnName = "intShortCutKey";
				colvarIntShortCutKey.DataType = DbType.Int32;
				colvarIntShortCutKey.MaxLength = 0;
				colvarIntShortCutKey.AutoIncrement = false;
				colvarIntShortCutKey.IsNullable = true;
				colvarIntShortCutKey.IsPrimaryKey = false;
				colvarIntShortCutKey.IsForeignKey = false;
				colvarIntShortCutKey.IsReadOnly = false;
				colvarIntShortCutKey.DefaultSetting = @"";
				colvarIntShortCutKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIntShortCutKey);
				
				TableSchema.TableColumn colvarIsMid = new TableSchema.TableColumn(schema);
				colvarIsMid.ColumnName = "IsMid";
				colvarIsMid.DataType = DbType.Int32;
				colvarIsMid.MaxLength = 0;
				colvarIsMid.AutoIncrement = false;
				colvarIsMid.IsNullable = true;
				colvarIsMid.IsPrimaryKey = false;
				colvarIsMid.IsForeignKey = false;
				colvarIsMid.IsReadOnly = false;
				
						colvarIsMid.DefaultSetting = @"((0))";
				colvarIsMid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsMid);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("Sys_Roles",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IRole")]
		[Bindable(true)]
		public long IRole 
		{
			get { return GetColumnValue<long>(Columns.IRole); }
			set { SetColumnValue(Columns.IRole, value); }
		}
		  
		[XmlAttribute("FpSBranchID")]
		[Bindable(true)]
		public string FpSBranchID 
		{
			get { return GetColumnValue<string>(Columns.FpSBranchID); }
			set { SetColumnValue(Columns.FpSBranchID, value); }
		}
		  
		[XmlAttribute("IParentRole")]
		[Bindable(true)]
		public long IParentRole 
		{
			get { return GetColumnValue<long>(Columns.IParentRole); }
			set { SetColumnValue(Columns.IParentRole, value); }
		}
		  
		[XmlAttribute("SRoleName")]
		[Bindable(true)]
		public string SRoleName 
		{
			get { return GetColumnValue<string>(Columns.SRoleName); }
			set { SetColumnValue(Columns.SRoleName, value); }
		}
		  
		[XmlAttribute("SEngRoleName")]
		[Bindable(true)]
		public string SEngRoleName 
		{
			get { return GetColumnValue<string>(Columns.SEngRoleName); }
			set { SetColumnValue(Columns.SEngRoleName, value); }
		}
		  
		[XmlAttribute("IOrder")]
		[Bindable(true)]
		public int? IOrder 
		{
			get { return GetColumnValue<int?>(Columns.IOrder); }
			set { SetColumnValue(Columns.IOrder, value); }
		}
		  
		[XmlAttribute("FkIFunctionID")]
		[Bindable(true)]
		public long? FkIFunctionID 
		{
			get { return GetColumnValue<long?>(Columns.FkIFunctionID); }
			set { SetColumnValue(Columns.FkIFunctionID, value); }
		}
		  
		[XmlAttribute("TDateCreated")]
		[Bindable(true)]
		public DateTime? TDateCreated 
		{
			get { return GetColumnValue<DateTime?>(Columns.TDateCreated); }
			set { SetColumnValue(Columns.TDateCreated, value); }
		}
		  
		[XmlAttribute("SImgPath")]
		[Bindable(true)]
		public string SImgPath 
		{
			get { return GetColumnValue<string>(Columns.SImgPath); }
			set { SetColumnValue(Columns.SImgPath, value); }
		}
		  
		[XmlAttribute("SDesc")]
		[Bindable(true)]
		public string SDesc 
		{
			get { return GetColumnValue<string>(Columns.SDesc); }
			set { SetColumnValue(Columns.SDesc, value); }
		}
		  
		[XmlAttribute("SIconPath")]
		[Bindable(true)]
		public string SIconPath 
		{
			get { return GetColumnValue<string>(Columns.SIconPath); }
			set { SetColumnValue(Columns.SIconPath, value); }
		}
		  
		[XmlAttribute("IntShortCutKey")]
		[Bindable(true)]
		public int? IntShortCutKey 
		{
			get { return GetColumnValue<int?>(Columns.IntShortCutKey); }
			set { SetColumnValue(Columns.IntShortCutKey, value); }
		}
		  
		[XmlAttribute("IsMid")]
		[Bindable(true)]
		public int? IsMid 
		{
			get { return GetColumnValue<int?>(Columns.IsMid); }
			set { SetColumnValue(Columns.IsMid, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varFpSBranchID,long varIParentRole,string varSRoleName,string varSEngRoleName,int? varIOrder,long? varFkIFunctionID,DateTime? varTDateCreated,string varSImgPath,string varSDesc,string varSIconPath,int? varIntShortCutKey,int? varIsMid)
		{
			SysRole item = new SysRole();
			
			item.FpSBranchID = varFpSBranchID;
			
			item.IParentRole = varIParentRole;
			
			item.SRoleName = varSRoleName;
			
			item.SEngRoleName = varSEngRoleName;
			
			item.IOrder = varIOrder;
			
			item.FkIFunctionID = varFkIFunctionID;
			
			item.TDateCreated = varTDateCreated;
			
			item.SImgPath = varSImgPath;
			
			item.SDesc = varSDesc;
			
			item.SIconPath = varSIconPath;
			
			item.IntShortCutKey = varIntShortCutKey;
			
			item.IsMid = varIsMid;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varIRole,string varFpSBranchID,long varIParentRole,string varSRoleName,string varSEngRoleName,int? varIOrder,long? varFkIFunctionID,DateTime? varTDateCreated,string varSImgPath,string varSDesc,string varSIconPath,int? varIntShortCutKey,int? varIsMid)
		{
			SysRole item = new SysRole();
			
				item.IRole = varIRole;
			
				item.FpSBranchID = varFpSBranchID;
			
				item.IParentRole = varIParentRole;
			
				item.SRoleName = varSRoleName;
			
				item.SEngRoleName = varSEngRoleName;
			
				item.IOrder = varIOrder;
			
				item.FkIFunctionID = varFkIFunctionID;
			
				item.TDateCreated = varTDateCreated;
			
				item.SImgPath = varSImgPath;
			
				item.SDesc = varSDesc;
			
				item.SIconPath = varSIconPath;
			
				item.IntShortCutKey = varIntShortCutKey;
			
				item.IsMid = varIsMid;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IRoleColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn FpSBranchIDColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IParentRoleColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SRoleNameColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SEngRoleNameColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn IOrderColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn FkIFunctionIDColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn TDateCreatedColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn SImgPathColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn SDescColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn SIconPathColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn IntShortCutKeyColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn IsMidColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IRole = @"iRole";
			 public static string FpSBranchID = @"FP_sBranchID";
			 public static string IParentRole = @"iParentRole";
			 public static string SRoleName = @"sRoleName";
			 public static string SEngRoleName = @"sEngRoleName";
			 public static string IOrder = @"iOrder";
			 public static string FkIFunctionID = @"FK_iFunctionID";
			 public static string TDateCreated = @"tDateCreated";
			 public static string SImgPath = @"sImgPath";
			 public static string SDesc = @"sDesc";
			 public static string SIconPath = @"sIconPath";
			 public static string IntShortCutKey = @"intShortCutKey";
			 public static string IsMid = @"IsMid";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
