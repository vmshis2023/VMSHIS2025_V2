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
	/// Strongly-typed collection for the NoitruChiphithem class.
	/// </summary>
    [Serializable]
	public partial class NoitruChiphithemCollection : ActiveList<NoitruChiphithem, NoitruChiphithemCollection>
	{	   
		public NoitruChiphithemCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>NoitruChiphithemCollection</returns>
		public NoitruChiphithemCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                NoitruChiphithem o = this[i];
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
	/// This is an ActiveRecord class which wraps the noitru_chiphithem table.
	/// </summary>
	[Serializable]
	public partial class NoitruChiphithem : ActiveRecord<NoitruChiphithem>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public NoitruChiphithem()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public NoitruChiphithem(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public NoitruChiphithem(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public NoitruChiphithem(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("noitru_chiphithem", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "Id";
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
				
				TableSchema.TableColumn colvarIdBenhnhan = new TableSchema.TableColumn(schema);
				colvarIdBenhnhan.ColumnName = "id_benhnhan";
				colvarIdBenhnhan.DataType = DbType.Int64;
				colvarIdBenhnhan.MaxLength = 0;
				colvarIdBenhnhan.AutoIncrement = false;
				colvarIdBenhnhan.IsNullable = false;
				colvarIdBenhnhan.IsPrimaryKey = false;
				colvarIdBenhnhan.IsForeignKey = false;
				colvarIdBenhnhan.IsReadOnly = false;
				colvarIdBenhnhan.DefaultSetting = @"";
				colvarIdBenhnhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBenhnhan);
				
				TableSchema.TableColumn colvarMaLuotkham = new TableSchema.TableColumn(schema);
				colvarMaLuotkham.ColumnName = "ma_luotkham";
				colvarMaLuotkham.DataType = DbType.String;
				colvarMaLuotkham.MaxLength = 10;
				colvarMaLuotkham.AutoIncrement = false;
				colvarMaLuotkham.IsNullable = false;
				colvarMaLuotkham.IsPrimaryKey = false;
				colvarMaLuotkham.IsForeignKey = false;
				colvarMaLuotkham.IsReadOnly = false;
				colvarMaLuotkham.DefaultSetting = @"";
				colvarMaLuotkham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaLuotkham);
				
				TableSchema.TableColumn colvarMaChiphi = new TableSchema.TableColumn(schema);
				colvarMaChiphi.ColumnName = "ma_chiphi";
				colvarMaChiphi.DataType = DbType.String;
				colvarMaChiphi.MaxLength = 20;
				colvarMaChiphi.AutoIncrement = false;
				colvarMaChiphi.IsNullable = false;
				colvarMaChiphi.IsPrimaryKey = false;
				colvarMaChiphi.IsForeignKey = false;
				colvarMaChiphi.IsReadOnly = false;
				colvarMaChiphi.DefaultSetting = @"";
				colvarMaChiphi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaChiphi);
				
				TableSchema.TableColumn colvarSoLuong = new TableSchema.TableColumn(schema);
				colvarSoLuong.ColumnName = "so_luong";
				colvarSoLuong.DataType = DbType.Int32;
				colvarSoLuong.MaxLength = 0;
				colvarSoLuong.AutoIncrement = false;
				colvarSoLuong.IsNullable = false;
				colvarSoLuong.IsPrimaryKey = false;
				colvarSoLuong.IsForeignKey = false;
				colvarSoLuong.IsReadOnly = false;
				colvarSoLuong.DefaultSetting = @"";
				colvarSoLuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLuong);
				
				TableSchema.TableColumn colvarDonGia = new TableSchema.TableColumn(schema);
				colvarDonGia.ColumnName = "don_gia";
				colvarDonGia.DataType = DbType.Decimal;
				colvarDonGia.MaxLength = 0;
				colvarDonGia.AutoIncrement = false;
				colvarDonGia.IsNullable = false;
				colvarDonGia.IsPrimaryKey = false;
				colvarDonGia.IsForeignKey = false;
				colvarDonGia.IsReadOnly = false;
				colvarDonGia.DefaultSetting = @"";
				colvarDonGia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDonGia);
				
				TableSchema.TableColumn colvarBnhanChitra = new TableSchema.TableColumn(schema);
				colvarBnhanChitra.ColumnName = "bnhan_chitra";
				colvarBnhanChitra.DataType = DbType.Decimal;
				colvarBnhanChitra.MaxLength = 0;
				colvarBnhanChitra.AutoIncrement = false;
				colvarBnhanChitra.IsNullable = false;
				colvarBnhanChitra.IsPrimaryKey = false;
				colvarBnhanChitra.IsForeignKey = false;
				colvarBnhanChitra.IsReadOnly = false;
				colvarBnhanChitra.DefaultSetting = @"";
				colvarBnhanChitra.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBnhanChitra);
				
				TableSchema.TableColumn colvarBhytChitra = new TableSchema.TableColumn(schema);
				colvarBhytChitra.ColumnName = "bhyt_chitra";
				colvarBhytChitra.DataType = DbType.Decimal;
				colvarBhytChitra.MaxLength = 0;
				colvarBhytChitra.AutoIncrement = false;
				colvarBhytChitra.IsNullable = true;
				colvarBhytChitra.IsPrimaryKey = false;
				colvarBhytChitra.IsForeignKey = false;
				colvarBhytChitra.IsReadOnly = false;
				colvarBhytChitra.DefaultSetting = @"";
				colvarBhytChitra.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBhytChitra);
				
				TableSchema.TableColumn colvarPhuThu = new TableSchema.TableColumn(schema);
				colvarPhuThu.ColumnName = "phu_thu";
				colvarPhuThu.DataType = DbType.Decimal;
				colvarPhuThu.MaxLength = 0;
				colvarPhuThu.AutoIncrement = false;
				colvarPhuThu.IsNullable = true;
				colvarPhuThu.IsPrimaryKey = false;
				colvarPhuThu.IsForeignKey = false;
				colvarPhuThu.IsReadOnly = false;
				colvarPhuThu.DefaultSetting = @"";
				colvarPhuThu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhuThu);
				
				TableSchema.TableColumn colvarNgayThanhtoan = new TableSchema.TableColumn(schema);
				colvarNgayThanhtoan.ColumnName = "ngay_thanhtoan";
				colvarNgayThanhtoan.DataType = DbType.DateTime;
				colvarNgayThanhtoan.MaxLength = 0;
				colvarNgayThanhtoan.AutoIncrement = false;
				colvarNgayThanhtoan.IsNullable = true;
				colvarNgayThanhtoan.IsPrimaryKey = false;
				colvarNgayThanhtoan.IsForeignKey = false;
				colvarNgayThanhtoan.IsReadOnly = false;
				colvarNgayThanhtoan.DefaultSetting = @"";
				colvarNgayThanhtoan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayThanhtoan);
				
				TableSchema.TableColumn colvarTrangthaiThanhtoan = new TableSchema.TableColumn(schema);
				colvarTrangthaiThanhtoan.ColumnName = "trangthai_thanhtoan";
				colvarTrangthaiThanhtoan.DataType = DbType.Byte;
				colvarTrangthaiThanhtoan.MaxLength = 0;
				colvarTrangthaiThanhtoan.AutoIncrement = false;
				colvarTrangthaiThanhtoan.IsNullable = true;
				colvarTrangthaiThanhtoan.IsPrimaryKey = false;
				colvarTrangthaiThanhtoan.IsForeignKey = false;
				colvarTrangthaiThanhtoan.IsReadOnly = false;
				colvarTrangthaiThanhtoan.DefaultSetting = @"";
				colvarTrangthaiThanhtoan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangthaiThanhtoan);
				
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
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("noitru_chiphithem",schema);
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
		  
		[XmlAttribute("IdBenhnhan")]
		[Bindable(true)]
		public long IdBenhnhan 
		{
			get { return GetColumnValue<long>(Columns.IdBenhnhan); }
			set { SetColumnValue(Columns.IdBenhnhan, value); }
		}
		  
		[XmlAttribute("MaLuotkham")]
		[Bindable(true)]
		public string MaLuotkham 
		{
			get { return GetColumnValue<string>(Columns.MaLuotkham); }
			set { SetColumnValue(Columns.MaLuotkham, value); }
		}
		  
		[XmlAttribute("MaChiphi")]
		[Bindable(true)]
		public string MaChiphi 
		{
			get { return GetColumnValue<string>(Columns.MaChiphi); }
			set { SetColumnValue(Columns.MaChiphi, value); }
		}
		  
		[XmlAttribute("SoLuong")]
		[Bindable(true)]
		public int SoLuong 
		{
			get { return GetColumnValue<int>(Columns.SoLuong); }
			set { SetColumnValue(Columns.SoLuong, value); }
		}
		  
		[XmlAttribute("DonGia")]
		[Bindable(true)]
		public decimal DonGia 
		{
			get { return GetColumnValue<decimal>(Columns.DonGia); }
			set { SetColumnValue(Columns.DonGia, value); }
		}
		  
		[XmlAttribute("BnhanChitra")]
		[Bindable(true)]
		public decimal BnhanChitra 
		{
			get { return GetColumnValue<decimal>(Columns.BnhanChitra); }
			set { SetColumnValue(Columns.BnhanChitra, value); }
		}
		  
		[XmlAttribute("BhytChitra")]
		[Bindable(true)]
		public decimal? BhytChitra 
		{
			get { return GetColumnValue<decimal?>(Columns.BhytChitra); }
			set { SetColumnValue(Columns.BhytChitra, value); }
		}
		  
		[XmlAttribute("PhuThu")]
		[Bindable(true)]
		public decimal? PhuThu 
		{
			get { return GetColumnValue<decimal?>(Columns.PhuThu); }
			set { SetColumnValue(Columns.PhuThu, value); }
		}
		  
		[XmlAttribute("NgayThanhtoan")]
		[Bindable(true)]
		public DateTime? NgayThanhtoan 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayThanhtoan); }
			set { SetColumnValue(Columns.NgayThanhtoan, value); }
		}
		  
		[XmlAttribute("TrangthaiThanhtoan")]
		[Bindable(true)]
		public byte? TrangthaiThanhtoan 
		{
			get { return GetColumnValue<byte?>(Columns.TrangthaiThanhtoan); }
			set { SetColumnValue(Columns.TrangthaiThanhtoan, value); }
		}
		  
		[XmlAttribute("NguoiTao")]
		[Bindable(true)]
		public string NguoiTao 
		{
			get { return GetColumnValue<string>(Columns.NguoiTao); }
			set { SetColumnValue(Columns.NguoiTao, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime NgayTao 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		  
		[XmlAttribute("NguoiSua")]
		[Bindable(true)]
		public string NguoiSua 
		{
			get { return GetColumnValue<string>(Columns.NguoiSua); }
			set { SetColumnValue(Columns.NguoiSua, value); }
		}
		  
		[XmlAttribute("NgaySua")]
		[Bindable(true)]
		public DateTime? NgaySua 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgaySua); }
			set { SetColumnValue(Columns.NgaySua, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varIdBenhnhan,string varMaLuotkham,string varMaChiphi,int varSoLuong,decimal varDonGia,decimal varBnhanChitra,decimal? varBhytChitra,decimal? varPhuThu,DateTime? varNgayThanhtoan,byte? varTrangthaiThanhtoan,string varNguoiTao,DateTime varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			NoitruChiphithem item = new NoitruChiphithem();
			
			item.IdBenhnhan = varIdBenhnhan;
			
			item.MaLuotkham = varMaLuotkham;
			
			item.MaChiphi = varMaChiphi;
			
			item.SoLuong = varSoLuong;
			
			item.DonGia = varDonGia;
			
			item.BnhanChitra = varBnhanChitra;
			
			item.BhytChitra = varBhytChitra;
			
			item.PhuThu = varPhuThu;
			
			item.NgayThanhtoan = varNgayThanhtoan;
			
			item.TrangthaiThanhtoan = varTrangthaiThanhtoan;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiSua = varNguoiSua;
			
			item.NgaySua = varNgaySua;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varId,long varIdBenhnhan,string varMaLuotkham,string varMaChiphi,int varSoLuong,decimal varDonGia,decimal varBnhanChitra,decimal? varBhytChitra,decimal? varPhuThu,DateTime? varNgayThanhtoan,byte? varTrangthaiThanhtoan,string varNguoiTao,DateTime varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			NoitruChiphithem item = new NoitruChiphithem();
			
				item.Id = varId;
			
				item.IdBenhnhan = varIdBenhnhan;
			
				item.MaLuotkham = varMaLuotkham;
			
				item.MaChiphi = varMaChiphi;
			
				item.SoLuong = varSoLuong;
			
				item.DonGia = varDonGia;
			
				item.BnhanChitra = varBnhanChitra;
			
				item.BhytChitra = varBhytChitra;
			
				item.PhuThu = varPhuThu;
			
				item.NgayThanhtoan = varNgayThanhtoan;
			
				item.TrangthaiThanhtoan = varTrangthaiThanhtoan;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiSua = varNguoiSua;
			
				item.NgaySua = varNgaySua;
			
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
        
        
        
        public static TableSchema.TableColumn IdBenhnhanColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn MaLuotkhamColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn MaChiphiColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLuongColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn DonGiaColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn BnhanChitraColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn BhytChitraColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn PhuThuColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayThanhtoanColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangthaiThanhtoanColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string IdBenhnhan = @"id_benhnhan";
			 public static string MaLuotkham = @"ma_luotkham";
			 public static string MaChiphi = @"ma_chiphi";
			 public static string SoLuong = @"so_luong";
			 public static string DonGia = @"don_gia";
			 public static string BnhanChitra = @"bnhan_chitra";
			 public static string BhytChitra = @"bhyt_chitra";
			 public static string PhuThu = @"phu_thu";
			 public static string NgayThanhtoan = @"ngay_thanhtoan";
			 public static string TrangthaiThanhtoan = @"trangthai_thanhtoan";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string NgayTao = @"ngay_tao";
			 public static string NguoiSua = @"nguoi_sua";
			 public static string NgaySua = @"ngay_sua";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
