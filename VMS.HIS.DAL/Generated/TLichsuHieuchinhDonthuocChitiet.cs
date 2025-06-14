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
	/// Strongly-typed collection for the TLichsuHieuchinhDonthuocChitiet class.
	/// </summary>
    [Serializable]
	public partial class TLichsuHieuchinhDonthuocChitietCollection : ActiveList<TLichsuHieuchinhDonthuocChitiet, TLichsuHieuchinhDonthuocChitietCollection>
	{	   
		public TLichsuHieuchinhDonthuocChitietCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TLichsuHieuchinhDonthuocChitietCollection</returns>
		public TLichsuHieuchinhDonthuocChitietCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TLichsuHieuchinhDonthuocChitiet o = this[i];
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
	/// This is an ActiveRecord class which wraps the t_lichsu_hieuchinh_donthuoc_chitiet table.
	/// </summary>
	[Serializable]
	public partial class TLichsuHieuchinhDonthuocChitiet : ActiveRecord<TLichsuHieuchinhDonthuocChitiet>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TLichsuHieuchinhDonthuocChitiet()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TLichsuHieuchinhDonthuocChitiet(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TLichsuHieuchinhDonthuocChitiet(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TLichsuHieuchinhDonthuocChitiet(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("t_lichsu_hieuchinh_donthuoc_chitiet", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdHieuchinh = new TableSchema.TableColumn(schema);
				colvarIdHieuchinh.ColumnName = "id_hieuchinh";
				colvarIdHieuchinh.DataType = DbType.Int64;
				colvarIdHieuchinh.MaxLength = 0;
				colvarIdHieuchinh.AutoIncrement = false;
				colvarIdHieuchinh.IsNullable = false;
				colvarIdHieuchinh.IsPrimaryKey = true;
				colvarIdHieuchinh.IsForeignKey = false;
				colvarIdHieuchinh.IsReadOnly = false;
				colvarIdHieuchinh.DefaultSetting = @"";
				colvarIdHieuchinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdHieuchinh);
				
				TableSchema.TableColumn colvarIdDonthuoc = new TableSchema.TableColumn(schema);
				colvarIdDonthuoc.ColumnName = "id_donthuoc";
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
				
				TableSchema.TableColumn colvarIdChitietDonthuoc = new TableSchema.TableColumn(schema);
				colvarIdChitietDonthuoc.ColumnName = "id_chitiet_donthuoc";
				colvarIdChitietDonthuoc.DataType = DbType.Int32;
				colvarIdChitietDonthuoc.MaxLength = 0;
				colvarIdChitietDonthuoc.AutoIncrement = false;
				colvarIdChitietDonthuoc.IsNullable = false;
				colvarIdChitietDonthuoc.IsPrimaryKey = true;
				colvarIdChitietDonthuoc.IsForeignKey = false;
				colvarIdChitietDonthuoc.IsReadOnly = false;
				colvarIdChitietDonthuoc.DefaultSetting = @"";
				colvarIdChitietDonthuoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChitietDonthuoc);
				
				TableSchema.TableColumn colvarIdThuoc = new TableSchema.TableColumn(schema);
				colvarIdThuoc.ColumnName = "id_thuoc";
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
				
				TableSchema.TableColumn colvarSoluongCu = new TableSchema.TableColumn(schema);
				colvarSoluongCu.ColumnName = "soluong_cu";
				colvarSoluongCu.DataType = DbType.Int32;
				colvarSoluongCu.MaxLength = 0;
				colvarSoluongCu.AutoIncrement = false;
				colvarSoluongCu.IsNullable = false;
				colvarSoluongCu.IsPrimaryKey = false;
				colvarSoluongCu.IsForeignKey = false;
				colvarSoluongCu.IsReadOnly = false;
				colvarSoluongCu.DefaultSetting = @"";
				colvarSoluongCu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoluongCu);
				
				TableSchema.TableColumn colvarSoluongMoi = new TableSchema.TableColumn(schema);
				colvarSoluongMoi.ColumnName = "soluong_moi";
				colvarSoluongMoi.DataType = DbType.Int32;
				colvarSoluongMoi.MaxLength = 0;
				colvarSoluongMoi.AutoIncrement = false;
				colvarSoluongMoi.IsNullable = false;
				colvarSoluongMoi.IsPrimaryKey = false;
				colvarSoluongMoi.IsForeignKey = false;
				colvarSoluongMoi.IsReadOnly = false;
				colvarSoluongMoi.DefaultSetting = @"";
				colvarSoluongMoi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoluongMoi);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("t_lichsu_hieuchinh_donthuoc_chitiet",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdHieuchinh")]
		[Bindable(true)]
		public long IdHieuchinh 
		{
			get { return GetColumnValue<long>(Columns.IdHieuchinh); }
			set { SetColumnValue(Columns.IdHieuchinh, value); }
		}
		  
		[XmlAttribute("IdDonthuoc")]
		[Bindable(true)]
		public int IdDonthuoc 
		{
			get { return GetColumnValue<int>(Columns.IdDonthuoc); }
			set { SetColumnValue(Columns.IdDonthuoc, value); }
		}
		  
		[XmlAttribute("IdChitietDonthuoc")]
		[Bindable(true)]
		public int IdChitietDonthuoc 
		{
			get { return GetColumnValue<int>(Columns.IdChitietDonthuoc); }
			set { SetColumnValue(Columns.IdChitietDonthuoc, value); }
		}
		  
		[XmlAttribute("IdThuoc")]
		[Bindable(true)]
		public int IdThuoc 
		{
			get { return GetColumnValue<int>(Columns.IdThuoc); }
			set { SetColumnValue(Columns.IdThuoc, value); }
		}
		  
		[XmlAttribute("SoluongCu")]
		[Bindable(true)]
		public int SoluongCu 
		{
			get { return GetColumnValue<int>(Columns.SoluongCu); }
			set { SetColumnValue(Columns.SoluongCu, value); }
		}
		  
		[XmlAttribute("SoluongMoi")]
		[Bindable(true)]
		public int SoluongMoi 
		{
			get { return GetColumnValue<int>(Columns.SoluongMoi); }
			set { SetColumnValue(Columns.SoluongMoi, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varIdHieuchinh,int varIdDonthuoc,int varIdChitietDonthuoc,int varIdThuoc,int varSoluongCu,int varSoluongMoi)
		{
			TLichsuHieuchinhDonthuocChitiet item = new TLichsuHieuchinhDonthuocChitiet();
			
			item.IdHieuchinh = varIdHieuchinh;
			
			item.IdDonthuoc = varIdDonthuoc;
			
			item.IdChitietDonthuoc = varIdChitietDonthuoc;
			
			item.IdThuoc = varIdThuoc;
			
			item.SoluongCu = varSoluongCu;
			
			item.SoluongMoi = varSoluongMoi;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varIdHieuchinh,int varIdDonthuoc,int varIdChitietDonthuoc,int varIdThuoc,int varSoluongCu,int varSoluongMoi)
		{
			TLichsuHieuchinhDonthuocChitiet item = new TLichsuHieuchinhDonthuocChitiet();
			
				item.IdHieuchinh = varIdHieuchinh;
			
				item.IdDonthuoc = varIdDonthuoc;
			
				item.IdChitietDonthuoc = varIdChitietDonthuoc;
			
				item.IdThuoc = varIdThuoc;
			
				item.SoluongCu = varSoluongCu;
			
				item.SoluongMoi = varSoluongMoi;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdHieuchinhColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdDonthuocColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IdChitietDonthuocColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThuocColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SoluongCuColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn SoluongMoiColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdHieuchinh = @"id_hieuchinh";
			 public static string IdDonthuoc = @"id_donthuoc";
			 public static string IdChitietDonthuoc = @"id_chitiet_donthuoc";
			 public static string IdThuoc = @"id_thuoc";
			 public static string SoluongCu = @"soluong_cu";
			 public static string SoluongMoi = @"soluong_moi";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
