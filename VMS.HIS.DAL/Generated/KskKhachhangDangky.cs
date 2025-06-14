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
	/// Strongly-typed collection for the KskKhachhangDangky class.
	/// </summary>
    [Serializable]
	public partial class KskKhachhangDangkyCollection : ActiveList<KskKhachhangDangky, KskKhachhangDangkyCollection>
	{	   
		public KskKhachhangDangkyCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>KskKhachhangDangkyCollection</returns>
		public KskKhachhangDangkyCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                KskKhachhangDangky o = this[i];
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
	/// This is an ActiveRecord class which wraps the ksk_khachhang_dangky table.
	/// </summary>
	[Serializable]
	public partial class KskKhachhangDangky : ActiveRecord<KskKhachhangDangky>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public KskKhachhangDangky()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public KskKhachhangDangky(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public KskKhachhangDangky(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public KskKhachhangDangky(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("ksk_khachhang_dangky", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarMaDangKy = new TableSchema.TableColumn(schema);
				colvarMaDangKy.ColumnName = "MaDangKy";
				colvarMaDangKy.DataType = DbType.String;
				colvarMaDangKy.MaxLength = 50;
				colvarMaDangKy.AutoIncrement = false;
				colvarMaDangKy.IsNullable = false;
				colvarMaDangKy.IsPrimaryKey = true;
				colvarMaDangKy.IsForeignKey = false;
				colvarMaDangKy.IsReadOnly = false;
				colvarMaDangKy.DefaultSetting = @"";
				colvarMaDangKy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaDangKy);
				
				TableSchema.TableColumn colvarIdKhachHang = new TableSchema.TableColumn(schema);
				colvarIdKhachHang.ColumnName = "IdKhachHang";
				colvarIdKhachHang.DataType = DbType.Int64;
				colvarIdKhachHang.MaxLength = 0;
				colvarIdKhachHang.AutoIncrement = false;
				colvarIdKhachHang.IsNullable = false;
				colvarIdKhachHang.IsPrimaryKey = false;
				colvarIdKhachHang.IsForeignKey = false;
				colvarIdKhachHang.IsReadOnly = false;
				colvarIdKhachHang.DefaultSetting = @"";
				colvarIdKhachHang.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhachHang);
				
				TableSchema.TableColumn colvarSoLo = new TableSchema.TableColumn(schema);
				colvarSoLo.ColumnName = "SoLo";
				colvarSoLo.DataType = DbType.String;
				colvarSoLo.MaxLength = 50;
				colvarSoLo.AutoIncrement = false;
				colvarSoLo.IsNullable = false;
				colvarSoLo.IsPrimaryKey = false;
				colvarSoLo.IsForeignKey = false;
				colvarSoLo.IsReadOnly = false;
				colvarSoLo.DefaultSetting = @"";
				colvarSoLo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLo);
				
				TableSchema.TableColumn colvarNgayDangKy = new TableSchema.TableColumn(schema);
				colvarNgayDangKy.ColumnName = "NgayDangKy";
				colvarNgayDangKy.DataType = DbType.String;
				colvarNgayDangKy.MaxLength = 50;
				colvarNgayDangKy.AutoIncrement = false;
				colvarNgayDangKy.IsNullable = false;
				colvarNgayDangKy.IsPrimaryKey = false;
				colvarNgayDangKy.IsForeignKey = false;
				colvarNgayDangKy.IsReadOnly = false;
				colvarNgayDangKy.DefaultSetting = @"";
				colvarNgayDangKy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayDangKy);
				
				TableSchema.TableColumn colvarNguoiDangKy = new TableSchema.TableColumn(schema);
				colvarNguoiDangKy.ColumnName = "NguoiDangKy";
				colvarNguoiDangKy.DataType = DbType.String;
				colvarNguoiDangKy.MaxLength = 50;
				colvarNguoiDangKy.AutoIncrement = false;
				colvarNguoiDangKy.IsNullable = true;
				colvarNguoiDangKy.IsPrimaryKey = false;
				colvarNguoiDangKy.IsForeignKey = false;
				colvarNguoiDangKy.IsReadOnly = false;
				colvarNguoiDangKy.DefaultSetting = @"";
				colvarNguoiDangKy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiDangKy);
				
				TableSchema.TableColumn colvarSoHopDong = new TableSchema.TableColumn(schema);
				colvarSoHopDong.ColumnName = "SoHopDong";
				colvarSoHopDong.DataType = DbType.String;
				colvarSoHopDong.MaxLength = 50;
				colvarSoHopDong.AutoIncrement = false;
				colvarSoHopDong.IsNullable = true;
				colvarSoHopDong.IsPrimaryKey = false;
				colvarSoHopDong.IsForeignKey = false;
				colvarSoHopDong.IsReadOnly = false;
				colvarSoHopDong.DefaultSetting = @"";
				colvarSoHopDong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoHopDong);
				
				TableSchema.TableColumn colvarSoLuongMau = new TableSchema.TableColumn(schema);
				colvarSoLuongMau.ColumnName = "SoLuongMau";
				colvarSoLuongMau.DataType = DbType.Int32;
				colvarSoLuongMau.MaxLength = 0;
				colvarSoLuongMau.AutoIncrement = false;
				colvarSoLuongMau.IsNullable = true;
				colvarSoLuongMau.IsPrimaryKey = false;
				colvarSoLuongMau.IsForeignKey = false;
				colvarSoLuongMau.IsReadOnly = false;
				colvarSoLuongMau.DefaultSetting = @"";
				colvarSoLuongMau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLuongMau);
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "TrangThai";
				colvarTrangThai.DataType = DbType.Byte;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = true;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				
						colvarTrangThai.DefaultSetting = @"((0))";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
				TableSchema.TableColumn colvarNgayThanhToan = new TableSchema.TableColumn(schema);
				colvarNgayThanhToan.ColumnName = "NgayThanhToan";
				colvarNgayThanhToan.DataType = DbType.DateTime;
				colvarNgayThanhToan.MaxLength = 0;
				colvarNgayThanhToan.AutoIncrement = false;
				colvarNgayThanhToan.IsNullable = true;
				colvarNgayThanhToan.IsPrimaryKey = false;
				colvarNgayThanhToan.IsForeignKey = false;
				colvarNgayThanhToan.IsReadOnly = false;
				colvarNgayThanhToan.DefaultSetting = @"";
				colvarNgayThanhToan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayThanhToan);
				
				TableSchema.TableColumn colvarTongTien = new TableSchema.TableColumn(schema);
				colvarTongTien.ColumnName = "TongTien";
				colvarTongTien.DataType = DbType.Decimal;
				colvarTongTien.MaxLength = 0;
				colvarTongTien.AutoIncrement = false;
				colvarTongTien.IsNullable = true;
				colvarTongTien.IsPrimaryKey = false;
				colvarTongTien.IsForeignKey = false;
				colvarTongTien.IsReadOnly = false;
				colvarTongTien.DefaultSetting = @"";
				colvarTongTien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTongTien);
				
				TableSchema.TableColumn colvarTongGiamGia = new TableSchema.TableColumn(schema);
				colvarTongGiamGia.ColumnName = "TongGiamGia";
				colvarTongGiamGia.DataType = DbType.Decimal;
				colvarTongGiamGia.MaxLength = 0;
				colvarTongGiamGia.AutoIncrement = false;
				colvarTongGiamGia.IsNullable = true;
				colvarTongGiamGia.IsPrimaryKey = false;
				colvarTongGiamGia.IsForeignKey = false;
				colvarTongGiamGia.IsReadOnly = false;
				colvarTongGiamGia.DefaultSetting = @"";
				colvarTongGiamGia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTongGiamGia);
				
				TableSchema.TableColumn colvarSoTienTamUng = new TableSchema.TableColumn(schema);
				colvarSoTienTamUng.ColumnName = "SoTienTamUng";
				colvarSoTienTamUng.DataType = DbType.Decimal;
				colvarSoTienTamUng.MaxLength = 0;
				colvarSoTienTamUng.AutoIncrement = false;
				colvarSoTienTamUng.IsNullable = true;
				colvarSoTienTamUng.IsPrimaryKey = false;
				colvarSoTienTamUng.IsForeignKey = false;
				colvarSoTienTamUng.IsReadOnly = false;
				colvarSoTienTamUng.DefaultSetting = @"";
				colvarSoTienTamUng.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoTienTamUng);
				
				TableSchema.TableColumn colvarSoTienConLai = new TableSchema.TableColumn(schema);
				colvarSoTienConLai.ColumnName = "SoTienConLai";
				colvarSoTienConLai.DataType = DbType.Decimal;
				colvarSoTienConLai.MaxLength = 0;
				colvarSoTienConLai.AutoIncrement = false;
				colvarSoTienConLai.IsNullable = true;
				colvarSoTienConLai.IsPrimaryKey = false;
				colvarSoTienConLai.IsForeignKey = false;
				colvarSoTienConLai.IsReadOnly = false;
				colvarSoTienConLai.DefaultSetting = @"";
				colvarSoTienConLai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoTienConLai);
				
				TableSchema.TableColumn colvarNguoiTiepDon = new TableSchema.TableColumn(schema);
				colvarNguoiTiepDon.ColumnName = "NguoiTiepDon";
				colvarNguoiTiepDon.DataType = DbType.String;
				colvarNguoiTiepDon.MaxLength = 50;
				colvarNguoiTiepDon.AutoIncrement = false;
				colvarNguoiTiepDon.IsNullable = true;
				colvarNguoiTiepDon.IsPrimaryKey = false;
				colvarNguoiTiepDon.IsForeignKey = false;
				colvarNguoiTiepDon.IsReadOnly = false;
				colvarNguoiTiepDon.DefaultSetting = @"";
				colvarNguoiTiepDon.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiTiepDon);
				
				TableSchema.TableColumn colvarNgayHenTraKQ = new TableSchema.TableColumn(schema);
				colvarNgayHenTraKQ.ColumnName = "NgayHenTraKQ";
				colvarNgayHenTraKQ.DataType = DbType.DateTime;
				colvarNgayHenTraKQ.MaxLength = 0;
				colvarNgayHenTraKQ.AutoIncrement = false;
				colvarNgayHenTraKQ.IsNullable = true;
				colvarNgayHenTraKQ.IsPrimaryKey = false;
				colvarNgayHenTraKQ.IsForeignKey = false;
				colvarNgayHenTraKQ.IsReadOnly = false;
				colvarNgayHenTraKQ.DefaultSetting = @"";
				colvarNgayHenTraKQ.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayHenTraKQ);
				
				TableSchema.TableColumn colvarNgayKetThucHD = new TableSchema.TableColumn(schema);
				colvarNgayKetThucHD.ColumnName = "NgayKetThucHD";
				colvarNgayKetThucHD.DataType = DbType.DateTime;
				colvarNgayKetThucHD.MaxLength = 0;
				colvarNgayKetThucHD.AutoIncrement = false;
				colvarNgayKetThucHD.IsNullable = true;
				colvarNgayKetThucHD.IsPrimaryKey = false;
				colvarNgayKetThucHD.IsForeignKey = false;
				colvarNgayKetThucHD.IsReadOnly = false;
				colvarNgayKetThucHD.DefaultSetting = @"";
				colvarNgayKetThucHD.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayKetThucHD);
				
				TableSchema.TableColumn colvarLocked = new TableSchema.TableColumn(schema);
				colvarLocked.ColumnName = "Locked";
				colvarLocked.DataType = DbType.Byte;
				colvarLocked.MaxLength = 0;
				colvarLocked.AutoIncrement = false;
				colvarLocked.IsNullable = true;
				colvarLocked.IsPrimaryKey = false;
				colvarLocked.IsForeignKey = false;
				colvarLocked.IsReadOnly = false;
				
						colvarLocked.DefaultSetting = @"((0))";
				colvarLocked.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLocked);
				
				TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
				colvarNguoiTao.ColumnName = "NguoiTao";
				colvarNguoiTao.DataType = DbType.String;
				colvarNguoiTao.MaxLength = 50;
				colvarNguoiTao.AutoIncrement = false;
				colvarNguoiTao.IsNullable = true;
				colvarNguoiTao.IsPrimaryKey = false;
				colvarNguoiTao.IsForeignKey = false;
				colvarNguoiTao.IsReadOnly = false;
				colvarNguoiTao.DefaultSetting = @"";
				colvarNguoiTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiTao);
				
				TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
				colvarNgayTao.ColumnName = "NgayTao";
				colvarNgayTao.DataType = DbType.DateTime;
				colvarNgayTao.MaxLength = 0;
				colvarNgayTao.AutoIncrement = false;
				colvarNgayTao.IsNullable = true;
				colvarNgayTao.IsPrimaryKey = false;
				colvarNgayTao.IsForeignKey = false;
				colvarNgayTao.IsReadOnly = false;
				colvarNgayTao.DefaultSetting = @"";
				colvarNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTao);
				
				TableSchema.TableColumn colvarNguoiSua = new TableSchema.TableColumn(schema);
				colvarNguoiSua.ColumnName = "NguoiSua";
				colvarNguoiSua.DataType = DbType.String;
				colvarNguoiSua.MaxLength = 50;
				colvarNguoiSua.AutoIncrement = false;
				colvarNguoiSua.IsNullable = true;
				colvarNguoiSua.IsPrimaryKey = false;
				colvarNguoiSua.IsForeignKey = false;
				colvarNguoiSua.IsReadOnly = false;
				colvarNguoiSua.DefaultSetting = @"";
				colvarNguoiSua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiSua);
				
				TableSchema.TableColumn colvarNgaySua = new TableSchema.TableColumn(schema);
				colvarNgaySua.ColumnName = "NgaySua";
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
				DataService.Providers["ORM"].AddSchema("ksk_khachhang_dangky",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("MaDangKy")]
		[Bindable(true)]
		public string MaDangKy 
		{
			get { return GetColumnValue<string>(Columns.MaDangKy); }
			set { SetColumnValue(Columns.MaDangKy, value); }
		}
		  
		[XmlAttribute("IdKhachHang")]
		[Bindable(true)]
		public long IdKhachHang 
		{
			get { return GetColumnValue<long>(Columns.IdKhachHang); }
			set { SetColumnValue(Columns.IdKhachHang, value); }
		}
		  
		[XmlAttribute("SoLo")]
		[Bindable(true)]
		public string SoLo 
		{
			get { return GetColumnValue<string>(Columns.SoLo); }
			set { SetColumnValue(Columns.SoLo, value); }
		}
		  
		[XmlAttribute("NgayDangKy")]
		[Bindable(true)]
		public string NgayDangKy 
		{
			get { return GetColumnValue<string>(Columns.NgayDangKy); }
			set { SetColumnValue(Columns.NgayDangKy, value); }
		}
		  
		[XmlAttribute("NguoiDangKy")]
		[Bindable(true)]
		public string NguoiDangKy 
		{
			get { return GetColumnValue<string>(Columns.NguoiDangKy); }
			set { SetColumnValue(Columns.NguoiDangKy, value); }
		}
		  
		[XmlAttribute("SoHopDong")]
		[Bindable(true)]
		public string SoHopDong 
		{
			get { return GetColumnValue<string>(Columns.SoHopDong); }
			set { SetColumnValue(Columns.SoHopDong, value); }
		}
		  
		[XmlAttribute("SoLuongMau")]
		[Bindable(true)]
		public int? SoLuongMau 
		{
			get { return GetColumnValue<int?>(Columns.SoLuongMau); }
			set { SetColumnValue(Columns.SoLuongMau, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public byte? TrangThai 
		{
			get { return GetColumnValue<byte?>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("NgayThanhToan")]
		[Bindable(true)]
		public DateTime? NgayThanhToan 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayThanhToan); }
			set { SetColumnValue(Columns.NgayThanhToan, value); }
		}
		  
		[XmlAttribute("TongTien")]
		[Bindable(true)]
		public decimal? TongTien 
		{
			get { return GetColumnValue<decimal?>(Columns.TongTien); }
			set { SetColumnValue(Columns.TongTien, value); }
		}
		  
		[XmlAttribute("TongGiamGia")]
		[Bindable(true)]
		public decimal? TongGiamGia 
		{
			get { return GetColumnValue<decimal?>(Columns.TongGiamGia); }
			set { SetColumnValue(Columns.TongGiamGia, value); }
		}
		  
		[XmlAttribute("SoTienTamUng")]
		[Bindable(true)]
		public decimal? SoTienTamUng 
		{
			get { return GetColumnValue<decimal?>(Columns.SoTienTamUng); }
			set { SetColumnValue(Columns.SoTienTamUng, value); }
		}
		  
		[XmlAttribute("SoTienConLai")]
		[Bindable(true)]
		public decimal? SoTienConLai 
		{
			get { return GetColumnValue<decimal?>(Columns.SoTienConLai); }
			set { SetColumnValue(Columns.SoTienConLai, value); }
		}
		  
		[XmlAttribute("NguoiTiepDon")]
		[Bindable(true)]
		public string NguoiTiepDon 
		{
			get { return GetColumnValue<string>(Columns.NguoiTiepDon); }
			set { SetColumnValue(Columns.NguoiTiepDon, value); }
		}
		  
		[XmlAttribute("NgayHenTraKQ")]
		[Bindable(true)]
		public DateTime? NgayHenTraKQ 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayHenTraKQ); }
			set { SetColumnValue(Columns.NgayHenTraKQ, value); }
		}
		  
		[XmlAttribute("NgayKetThucHD")]
		[Bindable(true)]
		public DateTime? NgayKetThucHD 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayKetThucHD); }
			set { SetColumnValue(Columns.NgayKetThucHD, value); }
		}
		  
		[XmlAttribute("Locked")]
		[Bindable(true)]
		public byte? Locked 
		{
			get { return GetColumnValue<byte?>(Columns.Locked); }
			set { SetColumnValue(Columns.Locked, value); }
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
		public DateTime? NgayTao 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayTao); }
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
		public static void Insert(string varMaDangKy,long varIdKhachHang,string varSoLo,string varNgayDangKy,string varNguoiDangKy,string varSoHopDong,int? varSoLuongMau,byte? varTrangThai,DateTime? varNgayThanhToan,decimal? varTongTien,decimal? varTongGiamGia,decimal? varSoTienTamUng,decimal? varSoTienConLai,string varNguoiTiepDon,DateTime? varNgayHenTraKQ,DateTime? varNgayKetThucHD,byte? varLocked,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			KskKhachhangDangky item = new KskKhachhangDangky();
			
			item.MaDangKy = varMaDangKy;
			
			item.IdKhachHang = varIdKhachHang;
			
			item.SoLo = varSoLo;
			
			item.NgayDangKy = varNgayDangKy;
			
			item.NguoiDangKy = varNguoiDangKy;
			
			item.SoHopDong = varSoHopDong;
			
			item.SoLuongMau = varSoLuongMau;
			
			item.TrangThai = varTrangThai;
			
			item.NgayThanhToan = varNgayThanhToan;
			
			item.TongTien = varTongTien;
			
			item.TongGiamGia = varTongGiamGia;
			
			item.SoTienTamUng = varSoTienTamUng;
			
			item.SoTienConLai = varSoTienConLai;
			
			item.NguoiTiepDon = varNguoiTiepDon;
			
			item.NgayHenTraKQ = varNgayHenTraKQ;
			
			item.NgayKetThucHD = varNgayKetThucHD;
			
			item.Locked = varLocked;
			
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
		public static void Update(string varMaDangKy,long varIdKhachHang,string varSoLo,string varNgayDangKy,string varNguoiDangKy,string varSoHopDong,int? varSoLuongMau,byte? varTrangThai,DateTime? varNgayThanhToan,decimal? varTongTien,decimal? varTongGiamGia,decimal? varSoTienTamUng,decimal? varSoTienConLai,string varNguoiTiepDon,DateTime? varNgayHenTraKQ,DateTime? varNgayKetThucHD,byte? varLocked,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			KskKhachhangDangky item = new KskKhachhangDangky();
			
				item.MaDangKy = varMaDangKy;
			
				item.IdKhachHang = varIdKhachHang;
			
				item.SoLo = varSoLo;
			
				item.NgayDangKy = varNgayDangKy;
			
				item.NguoiDangKy = varNguoiDangKy;
			
				item.SoHopDong = varSoHopDong;
			
				item.SoLuongMau = varSoLuongMau;
			
				item.TrangThai = varTrangThai;
			
				item.NgayThanhToan = varNgayThanhToan;
			
				item.TongTien = varTongTien;
			
				item.TongGiamGia = varTongGiamGia;
			
				item.SoTienTamUng = varSoTienTamUng;
			
				item.SoTienConLai = varSoTienConLai;
			
				item.NguoiTiepDon = varNguoiTiepDon;
			
				item.NgayHenTraKQ = varNgayHenTraKQ;
			
				item.NgayKetThucHD = varNgayKetThucHD;
			
				item.Locked = varLocked;
			
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
        
        
        public static TableSchema.TableColumn MaDangKyColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhachHangColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLoColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayDangKyColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiDangKyColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn SoHopDongColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLuongMauColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayThanhToanColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn TongTienColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn TongGiamGiaColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn SoTienTamUngColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn SoTienConLaiColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTiepDonColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayHenTraKQColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayKetThucHDColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn LockedColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string MaDangKy = @"MaDangKy";
			 public static string IdKhachHang = @"IdKhachHang";
			 public static string SoLo = @"SoLo";
			 public static string NgayDangKy = @"NgayDangKy";
			 public static string NguoiDangKy = @"NguoiDangKy";
			 public static string SoHopDong = @"SoHopDong";
			 public static string SoLuongMau = @"SoLuongMau";
			 public static string TrangThai = @"TrangThai";
			 public static string NgayThanhToan = @"NgayThanhToan";
			 public static string TongTien = @"TongTien";
			 public static string TongGiamGia = @"TongGiamGia";
			 public static string SoTienTamUng = @"SoTienTamUng";
			 public static string SoTienConLai = @"SoTienConLai";
			 public static string NguoiTiepDon = @"NguoiTiepDon";
			 public static string NgayHenTraKQ = @"NgayHenTraKQ";
			 public static string NgayKetThucHD = @"NgayKetThucHD";
			 public static string Locked = @"Locked";
			 public static string NguoiTao = @"NguoiTao";
			 public static string NgayTao = @"NgayTao";
			 public static string NguoiSua = @"NguoiSua";
			 public static string NgaySua = @"NgaySua";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
