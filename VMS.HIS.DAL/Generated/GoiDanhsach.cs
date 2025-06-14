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
	/// Strongly-typed collection for the GoiDanhsach class.
	/// </summary>
    [Serializable]
	public partial class GoiDanhsachCollection : ActiveList<GoiDanhsach, GoiDanhsachCollection>
	{	   
		public GoiDanhsachCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>GoiDanhsachCollection</returns>
		public GoiDanhsachCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                GoiDanhsach o = this[i];
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
	/// This is an ActiveRecord class which wraps the goi_danhsach table.
	/// </summary>
	[Serializable]
	public partial class GoiDanhsach : ActiveRecord<GoiDanhsach>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public GoiDanhsach()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public GoiDanhsach(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public GoiDanhsach(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public GoiDanhsach(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("goi_danhsach", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdGoi = new TableSchema.TableColumn(schema);
				colvarIdGoi.ColumnName = "id_goi";
				colvarIdGoi.DataType = DbType.Int32;
				colvarIdGoi.MaxLength = 0;
				colvarIdGoi.AutoIncrement = true;
				colvarIdGoi.IsNullable = false;
				colvarIdGoi.IsPrimaryKey = true;
				colvarIdGoi.IsForeignKey = false;
				colvarIdGoi.IsReadOnly = false;
				colvarIdGoi.DefaultSetting = @"";
				colvarIdGoi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdGoi);
				
				TableSchema.TableColumn colvarMaGoi = new TableSchema.TableColumn(schema);
				colvarMaGoi.ColumnName = "ma_goi";
				colvarMaGoi.DataType = DbType.AnsiString;
				colvarMaGoi.MaxLength = 30;
				colvarMaGoi.AutoIncrement = false;
				colvarMaGoi.IsNullable = false;
				colvarMaGoi.IsPrimaryKey = false;
				colvarMaGoi.IsForeignKey = false;
				colvarMaGoi.IsReadOnly = false;
				colvarMaGoi.DefaultSetting = @"";
				colvarMaGoi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaGoi);
				
				TableSchema.TableColumn colvarTenGoi = new TableSchema.TableColumn(schema);
				colvarTenGoi.ColumnName = "ten_goi";
				colvarTenGoi.DataType = DbType.String;
				colvarTenGoi.MaxLength = 250;
				colvarTenGoi.AutoIncrement = false;
				colvarTenGoi.IsNullable = false;
				colvarTenGoi.IsPrimaryKey = false;
				colvarTenGoi.IsForeignKey = false;
				colvarTenGoi.IsReadOnly = false;
				colvarTenGoi.DefaultSetting = @"";
				colvarTenGoi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenGoi);
				
				TableSchema.TableColumn colvarSoTien = new TableSchema.TableColumn(schema);
				colvarSoTien.ColumnName = "so_tien";
				colvarSoTien.DataType = DbType.Decimal;
				colvarSoTien.MaxLength = 0;
				colvarSoTien.AutoIncrement = false;
				colvarSoTien.IsNullable = false;
				colvarSoTien.IsPrimaryKey = false;
				colvarSoTien.IsForeignKey = false;
				colvarSoTien.IsReadOnly = false;
				colvarSoTien.DefaultSetting = @"";
				colvarSoTien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoTien);
				
				TableSchema.TableColumn colvarMienGiam = new TableSchema.TableColumn(schema);
				colvarMienGiam.ColumnName = "mien_giam";
				colvarMienGiam.DataType = DbType.Decimal;
				colvarMienGiam.MaxLength = 0;
				colvarMienGiam.AutoIncrement = false;
				colvarMienGiam.IsNullable = true;
				colvarMienGiam.IsPrimaryKey = false;
				colvarMienGiam.IsForeignKey = false;
				colvarMienGiam.IsReadOnly = false;
				colvarMienGiam.DefaultSetting = @"";
				colvarMienGiam.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMienGiam);
				
				TableSchema.TableColumn colvarGiamBhyt = new TableSchema.TableColumn(schema);
				colvarGiamBhyt.ColumnName = "giam_bhyt";
				colvarGiamBhyt.DataType = DbType.Decimal;
				colvarGiamBhyt.MaxLength = 0;
				colvarGiamBhyt.AutoIncrement = false;
				colvarGiamBhyt.IsNullable = true;
				colvarGiamBhyt.IsPrimaryKey = false;
				colvarGiamBhyt.IsForeignKey = false;
				colvarGiamBhyt.IsReadOnly = false;
				colvarGiamBhyt.DefaultSetting = @"";
				colvarGiamBhyt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGiamBhyt);
				
				TableSchema.TableColumn colvarHieulucTungay = new TableSchema.TableColumn(schema);
				colvarHieulucTungay.ColumnName = "hieuluc_tungay";
				colvarHieulucTungay.DataType = DbType.DateTime;
				colvarHieulucTungay.MaxLength = 0;
				colvarHieulucTungay.AutoIncrement = false;
				colvarHieulucTungay.IsNullable = false;
				colvarHieulucTungay.IsPrimaryKey = false;
				colvarHieulucTungay.IsForeignKey = false;
				colvarHieulucTungay.IsReadOnly = false;
				colvarHieulucTungay.DefaultSetting = @"";
				colvarHieulucTungay.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHieulucTungay);
				
				TableSchema.TableColumn colvarHieulucDenngay = new TableSchema.TableColumn(schema);
				colvarHieulucDenngay.ColumnName = "hieuluc_denngay";
				colvarHieulucDenngay.DataType = DbType.DateTime;
				colvarHieulucDenngay.MaxLength = 0;
				colvarHieulucDenngay.AutoIncrement = false;
				colvarHieulucDenngay.IsNullable = false;
				colvarHieulucDenngay.IsPrimaryKey = false;
				colvarHieulucDenngay.IsForeignKey = false;
				colvarHieulucDenngay.IsReadOnly = false;
				colvarHieulucDenngay.DefaultSetting = @"";
				colvarHieulucDenngay.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHieulucDenngay);
				
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
				
				TableSchema.TableColumn colvarLoaiGoi = new TableSchema.TableColumn(schema);
				colvarLoaiGoi.ColumnName = "loai_goi";
				colvarLoaiGoi.DataType = DbType.Byte;
				colvarLoaiGoi.MaxLength = 0;
				colvarLoaiGoi.AutoIncrement = false;
				colvarLoaiGoi.IsNullable = true;
				colvarLoaiGoi.IsPrimaryKey = false;
				colvarLoaiGoi.IsForeignKey = false;
				colvarLoaiGoi.IsReadOnly = false;
				
						colvarLoaiGoi.DefaultSetting = @"((0))";
				colvarLoaiGoi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoaiGoi);
				
				TableSchema.TableColumn colvarKieuChietkhau = new TableSchema.TableColumn(schema);
				colvarKieuChietkhau.ColumnName = "kieu_chietkhau";
				colvarKieuChietkhau.DataType = DbType.String;
				colvarKieuChietkhau.MaxLength = 1;
				colvarKieuChietkhau.AutoIncrement = false;
				colvarKieuChietkhau.IsNullable = true;
				colvarKieuChietkhau.IsPrimaryKey = false;
				colvarKieuChietkhau.IsForeignKey = false;
				colvarKieuChietkhau.IsReadOnly = false;
				
						colvarKieuChietkhau.DefaultSetting = @"(N'T')";
				colvarKieuChietkhau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKieuChietkhau);
				
				TableSchema.TableColumn colvarKhuyenmaiTatcadvu = new TableSchema.TableColumn(schema);
				colvarKhuyenmaiTatcadvu.ColumnName = "khuyenmai_tatcadvu";
				colvarKhuyenmaiTatcadvu.DataType = DbType.Byte;
				colvarKhuyenmaiTatcadvu.MaxLength = 0;
				colvarKhuyenmaiTatcadvu.AutoIncrement = false;
				colvarKhuyenmaiTatcadvu.IsNullable = true;
				colvarKhuyenmaiTatcadvu.IsPrimaryKey = false;
				colvarKhuyenmaiTatcadvu.IsForeignKey = false;
				colvarKhuyenmaiTatcadvu.IsReadOnly = false;
				
						colvarKhuyenmaiTatcadvu.DefaultSetting = @"((0))";
				colvarKhuyenmaiTatcadvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKhuyenmaiTatcadvu);
				
				TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
				colvarNguoiTao.ColumnName = "nguoi_tao";
				colvarNguoiTao.DataType = DbType.AnsiString;
				colvarNguoiTao.MaxLength = 20;
				colvarNguoiTao.AutoIncrement = false;
				colvarNguoiTao.IsNullable = true;
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
				colvarNgayTao.IsNullable = true;
				colvarNgayTao.IsPrimaryKey = false;
				colvarNgayTao.IsForeignKey = false;
				colvarNgayTao.IsReadOnly = false;
				
						colvarNgayTao.DefaultSetting = @"(getdate())";
				colvarNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTao);
				
				TableSchema.TableColumn colvarNguoiSua = new TableSchema.TableColumn(schema);
				colvarNguoiSua.ColumnName = "nguoi_sua";
				colvarNguoiSua.DataType = DbType.AnsiString;
				colvarNguoiSua.MaxLength = 20;
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
				
				TableSchema.TableColumn colvarIpMaytao = new TableSchema.TableColumn(schema);
				colvarIpMaytao.ColumnName = "ip_maytao";
				colvarIpMaytao.DataType = DbType.String;
				colvarIpMaytao.MaxLength = 30;
				colvarIpMaytao.AutoIncrement = false;
				colvarIpMaytao.IsNullable = true;
				colvarIpMaytao.IsPrimaryKey = false;
				colvarIpMaytao.IsForeignKey = false;
				colvarIpMaytao.IsReadOnly = false;
				colvarIpMaytao.DefaultSetting = @"";
				colvarIpMaytao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIpMaytao);
				
				TableSchema.TableColumn colvarIpMaysua = new TableSchema.TableColumn(schema);
				colvarIpMaysua.ColumnName = "ip_maysua";
				colvarIpMaysua.DataType = DbType.String;
				colvarIpMaysua.MaxLength = 30;
				colvarIpMaysua.AutoIncrement = false;
				colvarIpMaysua.IsNullable = true;
				colvarIpMaysua.IsPrimaryKey = false;
				colvarIpMaysua.IsForeignKey = false;
				colvarIpMaysua.IsReadOnly = false;
				colvarIpMaysua.DefaultSetting = @"";
				colvarIpMaysua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIpMaysua);
				
				TableSchema.TableColumn colvarTenMaytao = new TableSchema.TableColumn(schema);
				colvarTenMaytao.ColumnName = "ten_maytao";
				colvarTenMaytao.DataType = DbType.String;
				colvarTenMaytao.MaxLength = 100;
				colvarTenMaytao.AutoIncrement = false;
				colvarTenMaytao.IsNullable = true;
				colvarTenMaytao.IsPrimaryKey = false;
				colvarTenMaytao.IsForeignKey = false;
				colvarTenMaytao.IsReadOnly = false;
				colvarTenMaytao.DefaultSetting = @"";
				colvarTenMaytao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenMaytao);
				
				TableSchema.TableColumn colvarTenMaysua = new TableSchema.TableColumn(schema);
				colvarTenMaysua.ColumnName = "ten_maysua";
				colvarTenMaysua.DataType = DbType.String;
				colvarTenMaysua.MaxLength = 100;
				colvarTenMaysua.AutoIncrement = false;
				colvarTenMaysua.IsNullable = true;
				colvarTenMaysua.IsPrimaryKey = false;
				colvarTenMaysua.IsForeignKey = false;
				colvarTenMaysua.IsReadOnly = false;
				colvarTenMaysua.DefaultSetting = @"";
				colvarTenMaysua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenMaysua);
				
				TableSchema.TableColumn colvarGoikham1lan = new TableSchema.TableColumn(schema);
				colvarGoikham1lan.ColumnName = "goikham_1lan";
				colvarGoikham1lan.DataType = DbType.Boolean;
				colvarGoikham1lan.MaxLength = 0;
				colvarGoikham1lan.AutoIncrement = false;
				colvarGoikham1lan.IsNullable = true;
				colvarGoikham1lan.IsPrimaryKey = false;
				colvarGoikham1lan.IsForeignKey = false;
				colvarGoikham1lan.IsReadOnly = false;
				colvarGoikham1lan.DefaultSetting = @"";
				colvarGoikham1lan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGoikham1lan);
				
				TableSchema.TableColumn colvarKieuGoi = new TableSchema.TableColumn(schema);
				colvarKieuGoi.ColumnName = "kieu_goi";
				colvarKieuGoi.DataType = DbType.Byte;
				colvarKieuGoi.MaxLength = 0;
				colvarKieuGoi.AutoIncrement = false;
				colvarKieuGoi.IsNullable = true;
				colvarKieuGoi.IsPrimaryKey = false;
				colvarKieuGoi.IsForeignKey = false;
				colvarKieuGoi.IsReadOnly = false;
				
						colvarKieuGoi.DefaultSetting = @"((0))";
				colvarKieuGoi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKieuGoi);
				
				TableSchema.TableColumn colvarKhuyenmaiTong = new TableSchema.TableColumn(schema);
				colvarKhuyenmaiTong.ColumnName = "khuyenmai_tong";
				colvarKhuyenmaiTong.DataType = DbType.Boolean;
				colvarKhuyenmaiTong.MaxLength = 0;
				colvarKhuyenmaiTong.AutoIncrement = false;
				colvarKhuyenmaiTong.IsNullable = true;
				colvarKhuyenmaiTong.IsPrimaryKey = false;
				colvarKhuyenmaiTong.IsForeignKey = false;
				colvarKhuyenmaiTong.IsReadOnly = false;
				colvarKhuyenmaiTong.DefaultSetting = @"";
				colvarKhuyenmaiTong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKhuyenmaiTong);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("goi_danhsach",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdGoi")]
		[Bindable(true)]
		public int IdGoi 
		{
			get { return GetColumnValue<int>(Columns.IdGoi); }
			set { SetColumnValue(Columns.IdGoi, value); }
		}
		  
		[XmlAttribute("MaGoi")]
		[Bindable(true)]
		public string MaGoi 
		{
			get { return GetColumnValue<string>(Columns.MaGoi); }
			set { SetColumnValue(Columns.MaGoi, value); }
		}
		  
		[XmlAttribute("TenGoi")]
		[Bindable(true)]
		public string TenGoi 
		{
			get { return GetColumnValue<string>(Columns.TenGoi); }
			set { SetColumnValue(Columns.TenGoi, value); }
		}
		  
		[XmlAttribute("SoTien")]
		[Bindable(true)]
		public decimal SoTien 
		{
			get { return GetColumnValue<decimal>(Columns.SoTien); }
			set { SetColumnValue(Columns.SoTien, value); }
		}
		  
		[XmlAttribute("MienGiam")]
		[Bindable(true)]
		public decimal? MienGiam 
		{
			get { return GetColumnValue<decimal?>(Columns.MienGiam); }
			set { SetColumnValue(Columns.MienGiam, value); }
		}
		  
		[XmlAttribute("GiamBhyt")]
		[Bindable(true)]
		public decimal? GiamBhyt 
		{
			get { return GetColumnValue<decimal?>(Columns.GiamBhyt); }
			set { SetColumnValue(Columns.GiamBhyt, value); }
		}
		  
		[XmlAttribute("HieulucTungay")]
		[Bindable(true)]
		public DateTime HieulucTungay 
		{
			get { return GetColumnValue<DateTime>(Columns.HieulucTungay); }
			set { SetColumnValue(Columns.HieulucTungay, value); }
		}
		  
		[XmlAttribute("HieulucDenngay")]
		[Bindable(true)]
		public DateTime HieulucDenngay 
		{
			get { return GetColumnValue<DateTime>(Columns.HieulucDenngay); }
			set { SetColumnValue(Columns.HieulucDenngay, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public byte TrangThai 
		{
			get { return GetColumnValue<byte>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("LoaiGoi")]
		[Bindable(true)]
		public byte? LoaiGoi 
		{
			get { return GetColumnValue<byte?>(Columns.LoaiGoi); }
			set { SetColumnValue(Columns.LoaiGoi, value); }
		}
		  
		[XmlAttribute("KieuChietkhau")]
		[Bindable(true)]
		public string KieuChietkhau 
		{
			get { return GetColumnValue<string>(Columns.KieuChietkhau); }
			set { SetColumnValue(Columns.KieuChietkhau, value); }
		}
		  
		[XmlAttribute("KhuyenmaiTatcadvu")]
		[Bindable(true)]
		public byte? KhuyenmaiTatcadvu 
		{
			get { return GetColumnValue<byte?>(Columns.KhuyenmaiTatcadvu); }
			set { SetColumnValue(Columns.KhuyenmaiTatcadvu, value); }
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
		  
		[XmlAttribute("IpMaytao")]
		[Bindable(true)]
		public string IpMaytao 
		{
			get { return GetColumnValue<string>(Columns.IpMaytao); }
			set { SetColumnValue(Columns.IpMaytao, value); }
		}
		  
		[XmlAttribute("IpMaysua")]
		[Bindable(true)]
		public string IpMaysua 
		{
			get { return GetColumnValue<string>(Columns.IpMaysua); }
			set { SetColumnValue(Columns.IpMaysua, value); }
		}
		  
		[XmlAttribute("TenMaytao")]
		[Bindable(true)]
		public string TenMaytao 
		{
			get { return GetColumnValue<string>(Columns.TenMaytao); }
			set { SetColumnValue(Columns.TenMaytao, value); }
		}
		  
		[XmlAttribute("TenMaysua")]
		[Bindable(true)]
		public string TenMaysua 
		{
			get { return GetColumnValue<string>(Columns.TenMaysua); }
			set { SetColumnValue(Columns.TenMaysua, value); }
		}
		  
		[XmlAttribute("Goikham1lan")]
		[Bindable(true)]
		public bool? Goikham1lan 
		{
			get { return GetColumnValue<bool?>(Columns.Goikham1lan); }
			set { SetColumnValue(Columns.Goikham1lan, value); }
		}
		  
		[XmlAttribute("KieuGoi")]
		[Bindable(true)]
		public byte? KieuGoi 
		{
			get { return GetColumnValue<byte?>(Columns.KieuGoi); }
			set { SetColumnValue(Columns.KieuGoi, value); }
		}
		  
		[XmlAttribute("KhuyenmaiTong")]
		[Bindable(true)]
		public bool? KhuyenmaiTong 
		{
			get { return GetColumnValue<bool?>(Columns.KhuyenmaiTong); }
			set { SetColumnValue(Columns.KhuyenmaiTong, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaGoi,string varTenGoi,decimal varSoTien,decimal? varMienGiam,decimal? varGiamBhyt,DateTime varHieulucTungay,DateTime varHieulucDenngay,byte varTrangThai,byte? varLoaiGoi,string varKieuChietkhau,byte? varKhuyenmaiTatcadvu,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua,string varIpMaytao,string varIpMaysua,string varTenMaytao,string varTenMaysua,bool? varGoikham1lan,byte? varKieuGoi,bool? varKhuyenmaiTong)
		{
			GoiDanhsach item = new GoiDanhsach();
			
			item.MaGoi = varMaGoi;
			
			item.TenGoi = varTenGoi;
			
			item.SoTien = varSoTien;
			
			item.MienGiam = varMienGiam;
			
			item.GiamBhyt = varGiamBhyt;
			
			item.HieulucTungay = varHieulucTungay;
			
			item.HieulucDenngay = varHieulucDenngay;
			
			item.TrangThai = varTrangThai;
			
			item.LoaiGoi = varLoaiGoi;
			
			item.KieuChietkhau = varKieuChietkhau;
			
			item.KhuyenmaiTatcadvu = varKhuyenmaiTatcadvu;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiSua = varNguoiSua;
			
			item.NgaySua = varNgaySua;
			
			item.IpMaytao = varIpMaytao;
			
			item.IpMaysua = varIpMaysua;
			
			item.TenMaytao = varTenMaytao;
			
			item.TenMaysua = varTenMaysua;
			
			item.Goikham1lan = varGoikham1lan;
			
			item.KieuGoi = varKieuGoi;
			
			item.KhuyenmaiTong = varKhuyenmaiTong;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdGoi,string varMaGoi,string varTenGoi,decimal varSoTien,decimal? varMienGiam,decimal? varGiamBhyt,DateTime varHieulucTungay,DateTime varHieulucDenngay,byte varTrangThai,byte? varLoaiGoi,string varKieuChietkhau,byte? varKhuyenmaiTatcadvu,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua,string varIpMaytao,string varIpMaysua,string varTenMaytao,string varTenMaysua,bool? varGoikham1lan,byte? varKieuGoi,bool? varKhuyenmaiTong)
		{
			GoiDanhsach item = new GoiDanhsach();
			
				item.IdGoi = varIdGoi;
			
				item.MaGoi = varMaGoi;
			
				item.TenGoi = varTenGoi;
			
				item.SoTien = varSoTien;
			
				item.MienGiam = varMienGiam;
			
				item.GiamBhyt = varGiamBhyt;
			
				item.HieulucTungay = varHieulucTungay;
			
				item.HieulucDenngay = varHieulucDenngay;
			
				item.TrangThai = varTrangThai;
			
				item.LoaiGoi = varLoaiGoi;
			
				item.KieuChietkhau = varKieuChietkhau;
			
				item.KhuyenmaiTatcadvu = varKhuyenmaiTatcadvu;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiSua = varNguoiSua;
			
				item.NgaySua = varNgaySua;
			
				item.IpMaytao = varIpMaytao;
			
				item.IpMaysua = varIpMaysua;
			
				item.TenMaytao = varTenMaytao;
			
				item.TenMaysua = varTenMaysua;
			
				item.Goikham1lan = varGoikham1lan;
			
				item.KieuGoi = varKieuGoi;
			
				item.KhuyenmaiTong = varKhuyenmaiTong;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdGoiColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaGoiColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TenGoiColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SoTienColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn MienGiamColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn GiamBhytColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn HieulucTungayColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn HieulucDenngayColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiGoiColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn KieuChietkhauColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn KhuyenmaiTatcadvuColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn IpMaytaoColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn IpMaysuaColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn TenMaytaoColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn TenMaysuaColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn Goikham1lanColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn KieuGoiColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn KhuyenmaiTongColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdGoi = @"id_goi";
			 public static string MaGoi = @"ma_goi";
			 public static string TenGoi = @"ten_goi";
			 public static string SoTien = @"so_tien";
			 public static string MienGiam = @"mien_giam";
			 public static string GiamBhyt = @"giam_bhyt";
			 public static string HieulucTungay = @"hieuluc_tungay";
			 public static string HieulucDenngay = @"hieuluc_denngay";
			 public static string TrangThai = @"trang_thai";
			 public static string LoaiGoi = @"loai_goi";
			 public static string KieuChietkhau = @"kieu_chietkhau";
			 public static string KhuyenmaiTatcadvu = @"khuyenmai_tatcadvu";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string NgayTao = @"ngay_tao";
			 public static string NguoiSua = @"nguoi_sua";
			 public static string NgaySua = @"ngay_sua";
			 public static string IpMaytao = @"ip_maytao";
			 public static string IpMaysua = @"ip_maysua";
			 public static string TenMaytao = @"ten_maytao";
			 public static string TenMaysua = @"ten_maysua";
			 public static string Goikham1lan = @"goikham_1lan";
			 public static string KieuGoi = @"kieu_goi";
			 public static string KhuyenmaiTong = @"khuyenmai_tong";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
