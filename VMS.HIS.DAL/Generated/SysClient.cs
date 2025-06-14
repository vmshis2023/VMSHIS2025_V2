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
	/// Strongly-typed collection for the SysClient class.
	/// </summary>
    [Serializable]
	public partial class SysClientCollection : ActiveList<SysClient, SysClientCollection>
	{	   
		public SysClientCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysClientCollection</returns>
		public SysClientCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysClient o = this[i];
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
	/// This is an ActiveRecord class which wraps the Sys_Clients table.
	/// </summary>
	[Serializable]
	public partial class SysClient : ActiveRecord<SysClient>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public SysClient()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysClient(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysClient(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysClient(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("Sys_Clients", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "ID";
				colvarId.DataType = DbType.Int16;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarLicKey = new TableSchema.TableColumn(schema);
				colvarLicKey.ColumnName = "LicKey";
				colvarLicKey.DataType = DbType.String;
				colvarLicKey.MaxLength = 100;
				colvarLicKey.AutoIncrement = false;
				colvarLicKey.IsNullable = false;
				colvarLicKey.IsPrimaryKey = false;
				colvarLicKey.IsForeignKey = false;
				colvarLicKey.IsReadOnly = false;
				colvarLicKey.DefaultSetting = @"";
				colvarLicKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLicKey);
				
				TableSchema.TableColumn colvarExp = new TableSchema.TableColumn(schema);
				colvarExp.ColumnName = "Exp";
				colvarExp.DataType = DbType.String;
				colvarExp.MaxLength = 1000;
				colvarExp.AutoIncrement = false;
				colvarExp.IsNullable = true;
				colvarExp.IsPrimaryKey = false;
				colvarExp.IsForeignKey = false;
				colvarExp.IsReadOnly = false;
				colvarExp.DefaultSetting = @"";
				colvarExp.ForeignKeyTableName = "";
				schema.Columns.Add(colvarExp);
				
				TableSchema.TableColumn colvarIpaddress = new TableSchema.TableColumn(schema);
				colvarIpaddress.ColumnName = "ipaddress";
				colvarIpaddress.DataType = DbType.String;
				colvarIpaddress.MaxLength = 50;
				colvarIpaddress.AutoIncrement = false;
				colvarIpaddress.IsNullable = true;
				colvarIpaddress.IsPrimaryKey = false;
				colvarIpaddress.IsForeignKey = false;
				colvarIpaddress.IsReadOnly = false;
				colvarIpaddress.DefaultSetting = @"";
				colvarIpaddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIpaddress);
				
				TableSchema.TableColumn colvarComputername = new TableSchema.TableColumn(schema);
				colvarComputername.ColumnName = "computername";
				colvarComputername.DataType = DbType.String;
				colvarComputername.MaxLength = 255;
				colvarComputername.AutoIncrement = false;
				colvarComputername.IsNullable = true;
				colvarComputername.IsPrimaryKey = false;
				colvarComputername.IsForeignKey = false;
				colvarComputername.IsReadOnly = false;
				colvarComputername.DefaultSetting = @"";
				colvarComputername.ForeignKeyTableName = "";
				schema.Columns.Add(colvarComputername);
				
				TableSchema.TableColumn colvarMacAddress = new TableSchema.TableColumn(schema);
				colvarMacAddress.ColumnName = "mac_address";
				colvarMacAddress.DataType = DbType.String;
				colvarMacAddress.MaxLength = 50;
				colvarMacAddress.AutoIncrement = false;
				colvarMacAddress.IsNullable = true;
				colvarMacAddress.IsPrimaryKey = false;
				colvarMacAddress.IsForeignKey = false;
				colvarMacAddress.IsReadOnly = false;
				colvarMacAddress.DefaultSetting = @"";
				colvarMacAddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMacAddress);
				
				TableSchema.TableColumn colvarNgayKichhoat = new TableSchema.TableColumn(schema);
				colvarNgayKichhoat.ColumnName = "ngay_kichhoat";
				colvarNgayKichhoat.DataType = DbType.DateTime;
				colvarNgayKichhoat.MaxLength = 0;
				colvarNgayKichhoat.AutoIncrement = false;
				colvarNgayKichhoat.IsNullable = true;
				colvarNgayKichhoat.IsPrimaryKey = false;
				colvarNgayKichhoat.IsForeignKey = false;
				colvarNgayKichhoat.IsReadOnly = false;
				colvarNgayKichhoat.DefaultSetting = @"";
				colvarNgayKichhoat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayKichhoat);
				
				TableSchema.TableColumn colvarNgayTruycapGannhat = new TableSchema.TableColumn(schema);
				colvarNgayTruycapGannhat.ColumnName = "ngay_truycap_gannhat";
				colvarNgayTruycapGannhat.DataType = DbType.DateTime;
				colvarNgayTruycapGannhat.MaxLength = 0;
				colvarNgayTruycapGannhat.AutoIncrement = false;
				colvarNgayTruycapGannhat.IsNullable = true;
				colvarNgayTruycapGannhat.IsPrimaryKey = false;
				colvarNgayTruycapGannhat.IsForeignKey = false;
				colvarNgayTruycapGannhat.IsReadOnly = false;
				colvarNgayTruycapGannhat.DefaultSetting = @"";
				colvarNgayTruycapGannhat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTruycapGannhat);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("Sys_Clients",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public short Id 
		{
			get { return GetColumnValue<short>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("LicKey")]
		[Bindable(true)]
		public string LicKey 
		{
			get { return GetColumnValue<string>(Columns.LicKey); }
			set { SetColumnValue(Columns.LicKey, value); }
		}
		  
		[XmlAttribute("Exp")]
		[Bindable(true)]
		public string Exp 
		{
			get { return GetColumnValue<string>(Columns.Exp); }
			set { SetColumnValue(Columns.Exp, value); }
		}
		  
		[XmlAttribute("Ipaddress")]
		[Bindable(true)]
		public string Ipaddress 
		{
			get { return GetColumnValue<string>(Columns.Ipaddress); }
			set { SetColumnValue(Columns.Ipaddress, value); }
		}
		  
		[XmlAttribute("Computername")]
		[Bindable(true)]
		public string Computername 
		{
			get { return GetColumnValue<string>(Columns.Computername); }
			set { SetColumnValue(Columns.Computername, value); }
		}
		  
		[XmlAttribute("MacAddress")]
		[Bindable(true)]
		public string MacAddress 
		{
			get { return GetColumnValue<string>(Columns.MacAddress); }
			set { SetColumnValue(Columns.MacAddress, value); }
		}
		  
		[XmlAttribute("NgayKichhoat")]
		[Bindable(true)]
		public DateTime? NgayKichhoat 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayKichhoat); }
			set { SetColumnValue(Columns.NgayKichhoat, value); }
		}
		  
		[XmlAttribute("NgayTruycapGannhat")]
		[Bindable(true)]
		public DateTime? NgayTruycapGannhat 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayTruycapGannhat); }
			set { SetColumnValue(Columns.NgayTruycapGannhat, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varLicKey,string varExp,string varIpaddress,string varComputername,string varMacAddress,DateTime? varNgayKichhoat,DateTime? varNgayTruycapGannhat)
		{
			SysClient item = new SysClient();
			
			item.LicKey = varLicKey;
			
			item.Exp = varExp;
			
			item.Ipaddress = varIpaddress;
			
			item.Computername = varComputername;
			
			item.MacAddress = varMacAddress;
			
			item.NgayKichhoat = varNgayKichhoat;
			
			item.NgayTruycapGannhat = varNgayTruycapGannhat;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(short varId,string varLicKey,string varExp,string varIpaddress,string varComputername,string varMacAddress,DateTime? varNgayKichhoat,DateTime? varNgayTruycapGannhat)
		{
			SysClient item = new SysClient();
			
				item.Id = varId;
			
				item.LicKey = varLicKey;
			
				item.Exp = varExp;
			
				item.Ipaddress = varIpaddress;
			
				item.Computername = varComputername;
			
				item.MacAddress = varMacAddress;
			
				item.NgayKichhoat = varNgayKichhoat;
			
				item.NgayTruycapGannhat = varNgayTruycapGannhat;
			
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
        
        
        
        public static TableSchema.TableColumn LicKeyColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ExpColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IpaddressColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn ComputernameColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn MacAddressColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayKichhoatColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTruycapGannhatColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string LicKey = @"LicKey";
			 public static string Exp = @"Exp";
			 public static string Ipaddress = @"ipaddress";
			 public static string Computername = @"computername";
			 public static string MacAddress = @"mac_address";
			 public static string NgayKichhoat = @"ngay_kichhoat";
			 public static string NgayTruycapGannhat = @"ngay_truycap_gannhat";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
