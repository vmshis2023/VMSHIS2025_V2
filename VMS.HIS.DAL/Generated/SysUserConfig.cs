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
namespace VMS.HIS.DAL
{
	/// <summary>
	/// Strongly-typed collection for the SysUserConfig class.
	/// </summary>
    [Serializable]
	public partial class SysUserConfigCollection : ActiveList<SysUserConfig, SysUserConfigCollection>
	{	   
		public SysUserConfigCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysUserConfigCollection</returns>
		public SysUserConfigCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysUserConfig o = this[i];
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
	/// This is an ActiveRecord class which wraps the sys_user_configs table.
	/// </summary>
	[Serializable]
	public partial class SysUserConfig : ActiveRecord<SysUserConfig>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public SysUserConfig()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysUserConfig(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysUserConfig(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysUserConfig(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("sys_user_configs", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "id";
				colvarId.DataType = DbType.Int64;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarUsername = new TableSchema.TableColumn(schema);
				colvarUsername.ColumnName = "username";
				colvarUsername.DataType = DbType.String;
				colvarUsername.MaxLength = 30;
				colvarUsername.AutoIncrement = false;
				colvarUsername.IsNullable = false;
				colvarUsername.IsPrimaryKey = false;
				colvarUsername.IsForeignKey = false;
				colvarUsername.IsReadOnly = false;
				colvarUsername.DefaultSetting = @"";
				colvarUsername.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUsername);
				
				TableSchema.TableColumn colvarConfigName = new TableSchema.TableColumn(schema);
				colvarConfigName.ColumnName = "config_name";
				colvarConfigName.DataType = DbType.String;
				colvarConfigName.MaxLength = 100;
				colvarConfigName.AutoIncrement = false;
				colvarConfigName.IsNullable = false;
				colvarConfigName.IsPrimaryKey = false;
				colvarConfigName.IsForeignKey = false;
				colvarConfigName.IsReadOnly = false;
				colvarConfigName.DefaultSetting = @"";
				colvarConfigName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarConfigName);
				
				TableSchema.TableColumn colvarConfigValue = new TableSchema.TableColumn(schema);
				colvarConfigValue.ColumnName = "config_value";
				colvarConfigValue.DataType = DbType.Byte;
				colvarConfigValue.MaxLength = 0;
				colvarConfigValue.AutoIncrement = false;
				colvarConfigValue.IsNullable = false;
				colvarConfigValue.IsPrimaryKey = false;
				colvarConfigValue.IsForeignKey = false;
				colvarConfigValue.IsReadOnly = false;
				colvarConfigValue.DefaultSetting = @"";
				colvarConfigValue.ForeignKeyTableName = "";
				schema.Columns.Add(colvarConfigValue);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("sys_user_configs",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public long Id 
		{
			get { return GetColumnValue<long>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("Username")]
		[Bindable(true)]
		public string Username 
		{
			get { return GetColumnValue<string>(Columns.Username); }
			set { SetColumnValue(Columns.Username, value); }
		}
		  
		[XmlAttribute("ConfigName")]
		[Bindable(true)]
		public string ConfigName 
		{
			get { return GetColumnValue<string>(Columns.ConfigName); }
			set { SetColumnValue(Columns.ConfigName, value); }
		}
		  
		[XmlAttribute("ConfigValue")]
		[Bindable(true)]
		public byte ConfigValue 
		{
			get { return GetColumnValue<byte>(Columns.ConfigValue); }
			set { SetColumnValue(Columns.ConfigValue, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varUsername,string varConfigName,byte varConfigValue)
		{
			SysUserConfig item = new SysUserConfig();
			
			item.Username = varUsername;
			
			item.ConfigName = varConfigName;
			
			item.ConfigValue = varConfigValue;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varId,string varUsername,string varConfigName,byte varConfigValue)
		{
			SysUserConfig item = new SysUserConfig();
			
				item.Id = varId;
			
				item.Username = varUsername;
			
				item.ConfigName = varConfigName;
			
				item.ConfigValue = varConfigValue;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn UsernameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ConfigNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn ConfigValueColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"id";
			 public static string Username = @"username";
			 public static string ConfigName = @"config_name";
			 public static string ConfigValue = @"config_value";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
