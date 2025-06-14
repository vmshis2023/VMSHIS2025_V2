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
	/// Strongly-typed collection for the DThuocKhoDuTru class.
	/// </summary>
    [Serializable]
	public partial class DThuocKhoDuTruCollection : ActiveList<DThuocKhoDuTru, DThuocKhoDuTruCollection>
	{	   
		public DThuocKhoDuTruCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DThuocKhoDuTruCollection</returns>
		public DThuocKhoDuTruCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DThuocKhoDuTru o = this[i];
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
	/// This is an ActiveRecord class which wraps the D_THUOC_KHO_DU_TRU table.
	/// </summary>
	[Serializable]
	public partial class DThuocKhoDuTru : ActiveRecord<DThuocKhoDuTru>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DThuocKhoDuTru()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DThuocKhoDuTru(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DThuocKhoDuTru(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DThuocKhoDuTru(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("D_THUOC_KHO_DU_TRU", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdThuoc = new TableSchema.TableColumn(schema);
				colvarIdThuoc.ColumnName = "ID_THUOC";
				colvarIdThuoc.DataType = DbType.Int32;
				colvarIdThuoc.MaxLength = 0;
				colvarIdThuoc.AutoIncrement = false;
				colvarIdThuoc.IsNullable = false;
				colvarIdThuoc.IsPrimaryKey = true;
				colvarIdThuoc.IsForeignKey = false;
				colvarIdThuoc.IsReadOnly = false;
				colvarIdThuoc.DefaultSetting = @"";
				colvarIdThuoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThuoc);
				
				TableSchema.TableColumn colvarIdKho = new TableSchema.TableColumn(schema);
				colvarIdKho.ColumnName = "ID_KHO";
				colvarIdKho.DataType = DbType.Int16;
				colvarIdKho.MaxLength = 0;
				colvarIdKho.AutoIncrement = false;
				colvarIdKho.IsNullable = false;
				colvarIdKho.IsPrimaryKey = true;
				colvarIdKho.IsForeignKey = false;
				colvarIdKho.IsReadOnly = false;
				colvarIdKho.DefaultSetting = @"";
				colvarIdKho.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKho);
				
				TableSchema.TableColumn colvarKieuThuocVt = new TableSchema.TableColumn(schema);
				colvarKieuThuocVt.ColumnName = "KIEU_THUOC_VT";
				colvarKieuThuocVt.DataType = DbType.String;
				colvarKieuThuocVt.MaxLength = 10;
				colvarKieuThuocVt.AutoIncrement = false;
				colvarKieuThuocVt.IsNullable = false;
				colvarKieuThuocVt.IsPrimaryKey = true;
				colvarKieuThuocVt.IsForeignKey = false;
				colvarKieuThuocVt.IsReadOnly = false;
				colvarKieuThuocVt.DefaultSetting = @"";
				colvarKieuThuocVt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKieuThuocVt);
				
				TableSchema.TableColumn colvarSoLuong = new TableSchema.TableColumn(schema);
				colvarSoLuong.ColumnName = "SO_LUONG";
				colvarSoLuong.DataType = DbType.Int32;
				colvarSoLuong.MaxLength = 0;
				colvarSoLuong.AutoIncrement = false;
				colvarSoLuong.IsNullable = true;
				colvarSoLuong.IsPrimaryKey = false;
				colvarSoLuong.IsForeignKey = false;
				colvarSoLuong.IsReadOnly = false;
				colvarSoLuong.DefaultSetting = @"";
				colvarSoLuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLuong);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("D_THUOC_KHO_DU_TRU",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdThuoc")]
		[Bindable(true)]
		public int IdThuoc 
		{
			get { return GetColumnValue<int>(Columns.IdThuoc); }
			set { SetColumnValue(Columns.IdThuoc, value); }
		}
		  
		[XmlAttribute("IdKho")]
		[Bindable(true)]
		public short IdKho 
		{
			get { return GetColumnValue<short>(Columns.IdKho); }
			set { SetColumnValue(Columns.IdKho, value); }
		}
		  
		[XmlAttribute("KieuThuocVt")]
		[Bindable(true)]
		public string KieuThuocVt 
		{
			get { return GetColumnValue<string>(Columns.KieuThuocVt); }
			set { SetColumnValue(Columns.KieuThuocVt, value); }
		}
		  
		[XmlAttribute("SoLuong")]
		[Bindable(true)]
		public int? SoLuong 
		{
			get { return GetColumnValue<int?>(Columns.SoLuong); }
			set { SetColumnValue(Columns.SoLuong, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varIdThuoc,short varIdKho,string varKieuThuocVt,int? varSoLuong)
		{
			DThuocKhoDuTru item = new DThuocKhoDuTru();
			
			item.IdThuoc = varIdThuoc;
			
			item.IdKho = varIdKho;
			
			item.KieuThuocVt = varKieuThuocVt;
			
			item.SoLuong = varSoLuong;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdThuoc,short varIdKho,string varKieuThuocVt,int? varSoLuong)
		{
			DThuocKhoDuTru item = new DThuocKhoDuTru();
			
				item.IdThuoc = varIdThuoc;
			
				item.IdKho = varIdKho;
			
				item.KieuThuocVt = varKieuThuocVt;
			
				item.SoLuong = varSoLuong;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdThuocColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhoColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn KieuThuocVtColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLuongColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdThuoc = @"ID_THUOC";
			 public static string IdKho = @"ID_KHO";
			 public static string KieuThuocVt = @"KIEU_THUOC_VT";
			 public static string SoLuong = @"SO_LUONG";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
