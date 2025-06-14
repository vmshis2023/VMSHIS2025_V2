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
	/// Strongly-typed collection for the DmucBogiadichvu class.
	/// </summary>
    [Serializable]
	public partial class DmucBogiadichvuCollection : ActiveList<DmucBogiadichvu, DmucBogiadichvuCollection>
	{	   
		public DmucBogiadichvuCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DmucBogiadichvuCollection</returns>
		public DmucBogiadichvuCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DmucBogiadichvu o = this[i];
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
	/// This is an ActiveRecord class which wraps the dmuc_bogiadichvu table.
	/// </summary>
	[Serializable]
	public partial class DmucBogiadichvu : ActiveRecord<DmucBogiadichvu>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DmucBogiadichvu()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DmucBogiadichvu(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DmucBogiadichvu(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DmucBogiadichvu(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("dmuc_bogiadichvu", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdBogia = new TableSchema.TableColumn(schema);
				colvarIdBogia.ColumnName = "id_bogia";
				colvarIdBogia.DataType = DbType.Int32;
				colvarIdBogia.MaxLength = 0;
				colvarIdBogia.AutoIncrement = true;
				colvarIdBogia.IsNullable = false;
				colvarIdBogia.IsPrimaryKey = true;
				colvarIdBogia.IsForeignKey = false;
				colvarIdBogia.IsReadOnly = false;
				colvarIdBogia.DefaultSetting = @"";
				colvarIdBogia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBogia);
				
				TableSchema.TableColumn colvarMaBogia = new TableSchema.TableColumn(schema);
				colvarMaBogia.ColumnName = "ma_bogia";
				colvarMaBogia.DataType = DbType.String;
				colvarMaBogia.MaxLength = 30;
				colvarMaBogia.AutoIncrement = false;
				colvarMaBogia.IsNullable = false;
				colvarMaBogia.IsPrimaryKey = false;
				colvarMaBogia.IsForeignKey = false;
				colvarMaBogia.IsReadOnly = false;
				colvarMaBogia.DefaultSetting = @"";
				colvarMaBogia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaBogia);
				
				TableSchema.TableColumn colvarTenBogia = new TableSchema.TableColumn(schema);
				colvarTenBogia.ColumnName = "ten_bogia";
				colvarTenBogia.DataType = DbType.String;
				colvarTenBogia.MaxLength = 255;
				colvarTenBogia.AutoIncrement = false;
				colvarTenBogia.IsNullable = false;
				colvarTenBogia.IsPrimaryKey = false;
				colvarTenBogia.IsForeignKey = false;
				colvarTenBogia.IsReadOnly = false;
				colvarTenBogia.DefaultSetting = @"";
				colvarTenBogia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenBogia);
				
				TableSchema.TableColumn colvarNgayBatdau = new TableSchema.TableColumn(schema);
				colvarNgayBatdau.ColumnName = "ngay_batdau";
				colvarNgayBatdau.DataType = DbType.DateTime;
				colvarNgayBatdau.MaxLength = 0;
				colvarNgayBatdau.AutoIncrement = false;
				colvarNgayBatdau.IsNullable = false;
				colvarNgayBatdau.IsPrimaryKey = false;
				colvarNgayBatdau.IsForeignKey = false;
				colvarNgayBatdau.IsReadOnly = false;
				colvarNgayBatdau.DefaultSetting = @"";
				colvarNgayBatdau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayBatdau);
				
				TableSchema.TableColumn colvarNgayKetthuc = new TableSchema.TableColumn(schema);
				colvarNgayKetthuc.ColumnName = "ngay_ketthuc";
				colvarNgayKetthuc.DataType = DbType.DateTime;
				colvarNgayKetthuc.MaxLength = 0;
				colvarNgayKetthuc.AutoIncrement = false;
				colvarNgayKetthuc.IsNullable = false;
				colvarNgayKetthuc.IsPrimaryKey = false;
				colvarNgayKetthuc.IsForeignKey = false;
				colvarNgayKetthuc.IsReadOnly = false;
				colvarNgayKetthuc.DefaultSetting = @"";
				colvarNgayKetthuc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayKetthuc);
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "trang_thai";
				colvarTrangThai.DataType = DbType.Byte;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = false;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				colvarTrangThai.DefaultSetting = @"";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
				TableSchema.TableColumn colvarMotaThem = new TableSchema.TableColumn(schema);
				colvarMotaThem.ColumnName = "mota_them";
				colvarMotaThem.DataType = DbType.String;
				colvarMotaThem.MaxLength = 500;
				colvarMotaThem.AutoIncrement = false;
				colvarMotaThem.IsNullable = true;
				colvarMotaThem.IsPrimaryKey = false;
				colvarMotaThem.IsForeignKey = false;
				colvarMotaThem.IsReadOnly = false;
				colvarMotaThem.DefaultSetting = @"";
				colvarMotaThem.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMotaThem);
				
				TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
				colvarNgayTao.ColumnName = "ngay_tao";
				colvarNgayTao.DataType = DbType.DateTime;
				colvarNgayTao.MaxLength = 0;
				colvarNgayTao.AutoIncrement = false;
				colvarNgayTao.IsNullable = false;
				colvarNgayTao.IsPrimaryKey = false;
				colvarNgayTao.IsForeignKey = false;
				colvarNgayTao.IsReadOnly = false;
				colvarNgayTao.DefaultSetting = @"";
				colvarNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTao);
				
				TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
				colvarNguoiTao.ColumnName = "nguoi_tao";
				colvarNguoiTao.DataType = DbType.String;
				colvarNguoiTao.MaxLength = 30;
				colvarNguoiTao.AutoIncrement = false;
				colvarNguoiTao.IsNullable = false;
				colvarNguoiTao.IsPrimaryKey = false;
				colvarNguoiTao.IsForeignKey = false;
				colvarNguoiTao.IsReadOnly = false;
				colvarNguoiTao.DefaultSetting = @"";
				colvarNguoiTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiTao);
				
				TableSchema.TableColumn colvarNgaySua = new TableSchema.TableColumn(schema);
				colvarNgaySua.ColumnName = "ngay_sua";
				colvarNgaySua.DataType = DbType.DateTime;
				colvarNgaySua.MaxLength = 0;
				colvarNgaySua.AutoIncrement = false;
				colvarNgaySua.IsNullable = true;
				colvarNgaySua.IsPrimaryKey = false;
				colvarNgaySua.IsForeignKey = false;
				colvarNgaySua.IsReadOnly = false;
				colvarNgaySua.DefaultSetting = @"";
				colvarNgaySua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgaySua);
				
				TableSchema.TableColumn colvarNguoiSua = new TableSchema.TableColumn(schema);
				colvarNguoiSua.ColumnName = "nguoi_sua";
				colvarNguoiSua.DataType = DbType.String;
				colvarNguoiSua.MaxLength = 30;
				colvarNguoiSua.AutoIncrement = false;
				colvarNguoiSua.IsNullable = true;
				colvarNguoiSua.IsPrimaryKey = false;
				colvarNguoiSua.IsForeignKey = false;
				colvarNguoiSua.IsReadOnly = false;
				colvarNguoiSua.DefaultSetting = @"";
				colvarNguoiSua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiSua);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("dmuc_bogiadichvu",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdBogia")]
		[Bindable(true)]
		public int IdBogia 
		{
			get { return GetColumnValue<int>(Columns.IdBogia); }
			set { SetColumnValue(Columns.IdBogia, value); }
		}
		  
		[XmlAttribute("MaBogia")]
		[Bindable(true)]
		public string MaBogia 
		{
			get { return GetColumnValue<string>(Columns.MaBogia); }
			set { SetColumnValue(Columns.MaBogia, value); }
		}
		  
		[XmlAttribute("TenBogia")]
		[Bindable(true)]
		public string TenBogia 
		{
			get { return GetColumnValue<string>(Columns.TenBogia); }
			set { SetColumnValue(Columns.TenBogia, value); }
		}
		  
		[XmlAttribute("NgayBatdau")]
		[Bindable(true)]
		public DateTime NgayBatdau 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayBatdau); }
			set { SetColumnValue(Columns.NgayBatdau, value); }
		}
		  
		[XmlAttribute("NgayKetthuc")]
		[Bindable(true)]
		public DateTime NgayKetthuc 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayKetthuc); }
			set { SetColumnValue(Columns.NgayKetthuc, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public byte TrangThai 
		{
			get { return GetColumnValue<byte>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("MotaThem")]
		[Bindable(true)]
		public string MotaThem 
		{
			get { return GetColumnValue<string>(Columns.MotaThem); }
			set { SetColumnValue(Columns.MotaThem, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime NgayTao 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		  
		[XmlAttribute("NguoiTao")]
		[Bindable(true)]
		public string NguoiTao 
		{
			get { return GetColumnValue<string>(Columns.NguoiTao); }
			set { SetColumnValue(Columns.NguoiTao, value); }
		}
		  
		[XmlAttribute("NgaySua")]
		[Bindable(true)]
		public DateTime? NgaySua 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgaySua); }
			set { SetColumnValue(Columns.NgaySua, value); }
		}
		  
		[XmlAttribute("NguoiSua")]
		[Bindable(true)]
		public string NguoiSua 
		{
			get { return GetColumnValue<string>(Columns.NguoiSua); }
			set { SetColumnValue(Columns.NguoiSua, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaBogia,string varTenBogia,DateTime varNgayBatdau,DateTime varNgayKetthuc,byte varTrangThai,string varMotaThem,DateTime varNgayTao,string varNguoiTao,DateTime? varNgaySua,string varNguoiSua)
		{
			DmucBogiadichvu item = new DmucBogiadichvu();
			
			item.MaBogia = varMaBogia;
			
			item.TenBogia = varTenBogia;
			
			item.NgayBatdau = varNgayBatdau;
			
			item.NgayKetthuc = varNgayKetthuc;
			
			item.TrangThai = varTrangThai;
			
			item.MotaThem = varMotaThem;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgaySua = varNgaySua;
			
			item.NguoiSua = varNguoiSua;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdBogia,string varMaBogia,string varTenBogia,DateTime varNgayBatdau,DateTime varNgayKetthuc,byte varTrangThai,string varMotaThem,DateTime varNgayTao,string varNguoiTao,DateTime? varNgaySua,string varNguoiSua)
		{
			DmucBogiadichvu item = new DmucBogiadichvu();
			
				item.IdBogia = varIdBogia;
			
				item.MaBogia = varMaBogia;
			
				item.TenBogia = varTenBogia;
			
				item.NgayBatdau = varNgayBatdau;
			
				item.NgayKetthuc = varNgayKetthuc;
			
				item.TrangThai = varTrangThai;
			
				item.MotaThem = varMotaThem;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgaySua = varNgaySua;
			
				item.NguoiSua = varNguoiSua;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdBogiaColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaBogiaColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TenBogiaColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayBatdauColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayKetthucColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn MotaThemColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdBogia = @"id_bogia";
			 public static string MaBogia = @"ma_bogia";
			 public static string TenBogia = @"ten_bogia";
			 public static string NgayBatdau = @"ngay_batdau";
			 public static string NgayKetthuc = @"ngay_ketthuc";
			 public static string TrangThai = @"trang_thai";
			 public static string MotaThem = @"mota_them";
			 public static string NgayTao = @"ngay_tao";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string NgaySua = @"ngay_sua";
			 public static string NguoiSua = @"nguoi_sua";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
