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
	/// Strongly-typed collection for the LLsuHchinhDonthuocChitiet class.
	/// </summary>
    [Serializable]
	public partial class LLsuHchinhDonthuocChitietCollection : ActiveList<LLsuHchinhDonthuocChitiet, LLsuHchinhDonthuocChitietCollection>
	{	   
		public LLsuHchinhDonthuocChitietCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LLsuHchinhDonthuocChitietCollection</returns>
		public LLsuHchinhDonthuocChitietCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LLsuHchinhDonthuocChitiet o = this[i];
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
	/// This is an ActiveRecord class which wraps the L_LSU_HCHINH_DONTHUOC_CHITIET table.
	/// </summary>
	[Serializable]
	public partial class LLsuHchinhDonthuocChitiet : ActiveRecord<LLsuHchinhDonthuocChitiet>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LLsuHchinhDonthuocChitiet()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LLsuHchinhDonthuocChitiet(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LLsuHchinhDonthuocChitiet(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LLsuHchinhDonthuocChitiet(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("L_LSU_HCHINH_DONTHUOC_CHITIET", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdHchinh = new TableSchema.TableColumn(schema);
				colvarIdHchinh.ColumnName = "ID_HCHINH";
				colvarIdHchinh.DataType = DbType.Int64;
				colvarIdHchinh.MaxLength = 0;
				colvarIdHchinh.AutoIncrement = false;
				colvarIdHchinh.IsNullable = false;
				colvarIdHchinh.IsPrimaryKey = true;
				colvarIdHchinh.IsForeignKey = false;
				colvarIdHchinh.IsReadOnly = false;
				colvarIdHchinh.DefaultSetting = @"";
				colvarIdHchinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdHchinh);
				
				TableSchema.TableColumn colvarIdDonthuoc = new TableSchema.TableColumn(schema);
				colvarIdDonthuoc.ColumnName = "ID_DONTHUOC";
				colvarIdDonthuoc.DataType = DbType.Int32;
				colvarIdDonthuoc.MaxLength = 0;
				colvarIdDonthuoc.AutoIncrement = false;
				colvarIdDonthuoc.IsNullable = false;
				colvarIdDonthuoc.IsPrimaryKey = true;
				colvarIdDonthuoc.IsForeignKey = false;
				colvarIdDonthuoc.IsReadOnly = false;
				colvarIdDonthuoc.DefaultSetting = @"";
				colvarIdDonthuoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdDonthuoc);
				
				TableSchema.TableColumn colvarIdChitiet = new TableSchema.TableColumn(schema);
				colvarIdChitiet.ColumnName = "ID_CHITIET";
				colvarIdChitiet.DataType = DbType.Int32;
				colvarIdChitiet.MaxLength = 0;
				colvarIdChitiet.AutoIncrement = false;
				colvarIdChitiet.IsNullable = false;
				colvarIdChitiet.IsPrimaryKey = true;
				colvarIdChitiet.IsForeignKey = false;
				colvarIdChitiet.IsReadOnly = false;
				colvarIdChitiet.DefaultSetting = @"";
				colvarIdChitiet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChitiet);
				
				TableSchema.TableColumn colvarIdThuoc = new TableSchema.TableColumn(schema);
				colvarIdThuoc.ColumnName = "ID_THUOC";
				colvarIdThuoc.DataType = DbType.Int32;
				colvarIdThuoc.MaxLength = 0;
				colvarIdThuoc.AutoIncrement = false;
				colvarIdThuoc.IsNullable = false;
				colvarIdThuoc.IsPrimaryKey = false;
				colvarIdThuoc.IsForeignKey = false;
				colvarIdThuoc.IsReadOnly = false;
				
						colvarIdThuoc.DefaultSetting = @"((0))";
				colvarIdThuoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThuoc);
				
				TableSchema.TableColumn colvarSluongCu = new TableSchema.TableColumn(schema);
				colvarSluongCu.ColumnName = "SLUONG_CU";
				colvarSluongCu.DataType = DbType.Int32;
				colvarSluongCu.MaxLength = 0;
				colvarSluongCu.AutoIncrement = false;
				colvarSluongCu.IsNullable = false;
				colvarSluongCu.IsPrimaryKey = false;
				colvarSluongCu.IsForeignKey = false;
				colvarSluongCu.IsReadOnly = false;
				colvarSluongCu.DefaultSetting = @"";
				colvarSluongCu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSluongCu);
				
				TableSchema.TableColumn colvarSluongHchinh = new TableSchema.TableColumn(schema);
				colvarSluongHchinh.ColumnName = "SLUONG_HCHINH";
				colvarSluongHchinh.DataType = DbType.Int32;
				colvarSluongHchinh.MaxLength = 0;
				colvarSluongHchinh.AutoIncrement = false;
				colvarSluongHchinh.IsNullable = false;
				colvarSluongHchinh.IsPrimaryKey = false;
				colvarSluongHchinh.IsForeignKey = false;
				colvarSluongHchinh.IsReadOnly = false;
				colvarSluongHchinh.DefaultSetting = @"";
				colvarSluongHchinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSluongHchinh);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("L_LSU_HCHINH_DONTHUOC_CHITIET",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdHchinh")]
		[Bindable(true)]
		public long IdHchinh 
		{
			get { return GetColumnValue<long>(Columns.IdHchinh); }
			set { SetColumnValue(Columns.IdHchinh, value); }
		}
		  
		[XmlAttribute("IdDonthuoc")]
		[Bindable(true)]
		public int IdDonthuoc 
		{
			get { return GetColumnValue<int>(Columns.IdDonthuoc); }
			set { SetColumnValue(Columns.IdDonthuoc, value); }
		}
		  
		[XmlAttribute("IdChitiet")]
		[Bindable(true)]
		public int IdChitiet 
		{
			get { return GetColumnValue<int>(Columns.IdChitiet); }
			set { SetColumnValue(Columns.IdChitiet, value); }
		}
		  
		[XmlAttribute("IdThuoc")]
		[Bindable(true)]
		public int IdThuoc 
		{
			get { return GetColumnValue<int>(Columns.IdThuoc); }
			set { SetColumnValue(Columns.IdThuoc, value); }
		}
		  
		[XmlAttribute("SluongCu")]
		[Bindable(true)]
		public int SluongCu 
		{
			get { return GetColumnValue<int>(Columns.SluongCu); }
			set { SetColumnValue(Columns.SluongCu, value); }
		}
		  
		[XmlAttribute("SluongHchinh")]
		[Bindable(true)]
		public int SluongHchinh 
		{
			get { return GetColumnValue<int>(Columns.SluongHchinh); }
			set { SetColumnValue(Columns.SluongHchinh, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varIdHchinh,int varIdDonthuoc,int varIdChitiet,int varIdThuoc,int varSluongCu,int varSluongHchinh)
		{
			LLsuHchinhDonthuocChitiet item = new LLsuHchinhDonthuocChitiet();
			
			item.IdHchinh = varIdHchinh;
			
			item.IdDonthuoc = varIdDonthuoc;
			
			item.IdChitiet = varIdChitiet;
			
			item.IdThuoc = varIdThuoc;
			
			item.SluongCu = varSluongCu;
			
			item.SluongHchinh = varSluongHchinh;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varIdHchinh,int varIdDonthuoc,int varIdChitiet,int varIdThuoc,int varSluongCu,int varSluongHchinh)
		{
			LLsuHchinhDonthuocChitiet item = new LLsuHchinhDonthuocChitiet();
			
				item.IdHchinh = varIdHchinh;
			
				item.IdDonthuoc = varIdDonthuoc;
			
				item.IdChitiet = varIdChitiet;
			
				item.IdThuoc = varIdThuoc;
			
				item.SluongCu = varSluongCu;
			
				item.SluongHchinh = varSluongHchinh;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdHchinhColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdDonthuocColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IdChitietColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThuocColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SluongCuColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn SluongHchinhColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdHchinh = @"ID_HCHINH";
			 public static string IdDonthuoc = @"ID_DONTHUOC";
			 public static string IdChitiet = @"ID_CHITIET";
			 public static string IdThuoc = @"ID_THUOC";
			 public static string SluongCu = @"SLUONG_CU";
			 public static string SluongHchinh = @"SLUONG_HCHINH";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
