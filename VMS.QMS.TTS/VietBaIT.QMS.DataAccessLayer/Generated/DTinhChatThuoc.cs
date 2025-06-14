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
	/// Strongly-typed collection for the DTinhChatThuoc class.
	/// </summary>
    [Serializable]
	public partial class DTinhChatThuocCollection : ActiveList<DTinhChatThuoc, DTinhChatThuocCollection>
	{	   
		public DTinhChatThuocCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DTinhChatThuocCollection</returns>
		public DTinhChatThuocCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DTinhChatThuoc o = this[i];
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
	/// This is an ActiveRecord class which wraps the D_TinhChat_Thuoc table.
	/// </summary>
	[Serializable]
	public partial class DTinhChatThuoc : ActiveRecord<DTinhChatThuoc>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DTinhChatThuoc()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DTinhChatThuoc(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DTinhChatThuoc(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DTinhChatThuoc(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("D_TinhChat_Thuoc", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdTinhChat = new TableSchema.TableColumn(schema);
				colvarIdTinhChat.ColumnName = "Id_TinhChat";
				colvarIdTinhChat.DataType = DbType.Int16;
				colvarIdTinhChat.MaxLength = 0;
				colvarIdTinhChat.AutoIncrement = false;
				colvarIdTinhChat.IsNullable = false;
				colvarIdTinhChat.IsPrimaryKey = true;
				colvarIdTinhChat.IsForeignKey = false;
				colvarIdTinhChat.IsReadOnly = false;
				colvarIdTinhChat.DefaultSetting = @"";
				colvarIdTinhChat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdTinhChat);
				
				TableSchema.TableColumn colvarMaTinhChat = new TableSchema.TableColumn(schema);
				colvarMaTinhChat.ColumnName = "Ma_TinhChat";
				colvarMaTinhChat.DataType = DbType.String;
				colvarMaTinhChat.MaxLength = 10;
				colvarMaTinhChat.AutoIncrement = false;
				colvarMaTinhChat.IsNullable = true;
				colvarMaTinhChat.IsPrimaryKey = false;
				colvarMaTinhChat.IsForeignKey = false;
				colvarMaTinhChat.IsReadOnly = false;
				colvarMaTinhChat.DefaultSetting = @"";
				colvarMaTinhChat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaTinhChat);
				
				TableSchema.TableColumn colvarTenTinhChat = new TableSchema.TableColumn(schema);
				colvarTenTinhChat.ColumnName = "Ten_TinhChat";
				colvarTenTinhChat.DataType = DbType.String;
				colvarTenTinhChat.MaxLength = 50;
				colvarTenTinhChat.AutoIncrement = false;
				colvarTenTinhChat.IsNullable = true;
				colvarTenTinhChat.IsPrimaryKey = false;
				colvarTenTinhChat.IsForeignKey = false;
				colvarTenTinhChat.IsReadOnly = false;
				colvarTenTinhChat.DefaultSetting = @"";
				colvarTenTinhChat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenTinhChat);
				
				TableSchema.TableColumn colvarThuTu = new TableSchema.TableColumn(schema);
				colvarThuTu.ColumnName = "Thu_Tu";
				colvarThuTu.DataType = DbType.Int16;
				colvarThuTu.MaxLength = 0;
				colvarThuTu.AutoIncrement = false;
				colvarThuTu.IsNullable = true;
				colvarThuTu.IsPrimaryKey = false;
				colvarThuTu.IsForeignKey = false;
				colvarThuTu.IsReadOnly = false;
				colvarThuTu.DefaultSetting = @"";
				colvarThuTu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarThuTu);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("D_TinhChat_Thuoc",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdTinhChat")]
		[Bindable(true)]
		public short IdTinhChat 
		{
			get { return GetColumnValue<short>(Columns.IdTinhChat); }
			set { SetColumnValue(Columns.IdTinhChat, value); }
		}
		  
		[XmlAttribute("MaTinhChat")]
		[Bindable(true)]
		public string MaTinhChat 
		{
			get { return GetColumnValue<string>(Columns.MaTinhChat); }
			set { SetColumnValue(Columns.MaTinhChat, value); }
		}
		  
		[XmlAttribute("TenTinhChat")]
		[Bindable(true)]
		public string TenTinhChat 
		{
			get { return GetColumnValue<string>(Columns.TenTinhChat); }
			set { SetColumnValue(Columns.TenTinhChat, value); }
		}
		  
		[XmlAttribute("ThuTu")]
		[Bindable(true)]
		public short? ThuTu 
		{
			get { return GetColumnValue<short?>(Columns.ThuTu); }
			set { SetColumnValue(Columns.ThuTu, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(short varIdTinhChat,string varMaTinhChat,string varTenTinhChat,short? varThuTu)
		{
			DTinhChatThuoc item = new DTinhChatThuoc();
			
			item.IdTinhChat = varIdTinhChat;
			
			item.MaTinhChat = varMaTinhChat;
			
			item.TenTinhChat = varTenTinhChat;
			
			item.ThuTu = varThuTu;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(short varIdTinhChat,string varMaTinhChat,string varTenTinhChat,short? varThuTu)
		{
			DTinhChatThuoc item = new DTinhChatThuoc();
			
				item.IdTinhChat = varIdTinhChat;
			
				item.MaTinhChat = varMaTinhChat;
			
				item.TenTinhChat = varTenTinhChat;
			
				item.ThuTu = varThuTu;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdTinhChatColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaTinhChatColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TenTinhChatColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn ThuTuColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdTinhChat = @"Id_TinhChat";
			 public static string MaTinhChat = @"Ma_TinhChat";
			 public static string TenTinhChat = @"Ten_TinhChat";
			 public static string ThuTu = @"Thu_Tu";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
