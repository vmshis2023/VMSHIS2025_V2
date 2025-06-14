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
	/// Strongly-typed collection for the TPhieuNhapxuatthuocChitiet class.
	/// </summary>
    [Serializable]
	public partial class TPhieuNhapxuatthuocChitietCollection : ActiveList<TPhieuNhapxuatthuocChitiet, TPhieuNhapxuatthuocChitietCollection>
	{	   
		public TPhieuNhapxuatthuocChitietCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TPhieuNhapxuatthuocChitietCollection</returns>
		public TPhieuNhapxuatthuocChitietCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TPhieuNhapxuatthuocChitiet o = this[i];
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
	/// This is an ActiveRecord class which wraps the t_phieu_nhapxuatthuoc_chitiet table.
	/// </summary>
	[Serializable]
	public partial class TPhieuNhapxuatthuocChitiet : ActiveRecord<TPhieuNhapxuatthuocChitiet>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TPhieuNhapxuatthuocChitiet()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TPhieuNhapxuatthuocChitiet(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TPhieuNhapxuatthuocChitiet(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TPhieuNhapxuatthuocChitiet(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("t_phieu_nhapxuatthuoc_chitiet", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdPhieuchitiet = new TableSchema.TableColumn(schema);
				colvarIdPhieuchitiet.ColumnName = "id_phieuchitiet";
				colvarIdPhieuchitiet.DataType = DbType.Int64;
				colvarIdPhieuchitiet.MaxLength = 0;
				colvarIdPhieuchitiet.AutoIncrement = true;
				colvarIdPhieuchitiet.IsNullable = false;
				colvarIdPhieuchitiet.IsPrimaryKey = true;
				colvarIdPhieuchitiet.IsForeignKey = false;
				colvarIdPhieuchitiet.IsReadOnly = false;
				colvarIdPhieuchitiet.DefaultSetting = @"";
				colvarIdPhieuchitiet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdPhieuchitiet);
				
				TableSchema.TableColumn colvarIdPhieu = new TableSchema.TableColumn(schema);
				colvarIdPhieu.ColumnName = "id_phieu";
				colvarIdPhieu.DataType = DbType.Int64;
				colvarIdPhieu.MaxLength = 0;
				colvarIdPhieu.AutoIncrement = false;
				colvarIdPhieu.IsNullable = false;
				colvarIdPhieu.IsPrimaryKey = false;
				colvarIdPhieu.IsForeignKey = false;
				colvarIdPhieu.IsReadOnly = false;
				colvarIdPhieu.DefaultSetting = @"";
				colvarIdPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdPhieu);
				
				TableSchema.TableColumn colvarNgayHethan = new TableSchema.TableColumn(schema);
				colvarNgayHethan.ColumnName = "ngay_hethan";
				colvarNgayHethan.DataType = DbType.DateTime;
				colvarNgayHethan.MaxLength = 0;
				colvarNgayHethan.AutoIncrement = false;
				colvarNgayHethan.IsNullable = false;
				colvarNgayHethan.IsPrimaryKey = false;
				colvarNgayHethan.IsForeignKey = false;
				colvarNgayHethan.IsReadOnly = false;
				colvarNgayHethan.DefaultSetting = @"";
				colvarNgayHethan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayHethan);
				
				TableSchema.TableColumn colvarIdThuoc = new TableSchema.TableColumn(schema);
				colvarIdThuoc.ColumnName = "id_thuoc";
				colvarIdThuoc.DataType = DbType.Int32;
				colvarIdThuoc.MaxLength = 0;
				colvarIdThuoc.AutoIncrement = false;
				colvarIdThuoc.IsNullable = false;
				colvarIdThuoc.IsPrimaryKey = false;
				colvarIdThuoc.IsForeignKey = false;
				colvarIdThuoc.IsReadOnly = false;
				colvarIdThuoc.DefaultSetting = @"";
				colvarIdThuoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThuoc);
				
				TableSchema.TableColumn colvarGiaNhap = new TableSchema.TableColumn(schema);
				colvarGiaNhap.ColumnName = "gia_nhap";
				colvarGiaNhap.DataType = DbType.Decimal;
				colvarGiaNhap.MaxLength = 0;
				colvarGiaNhap.AutoIncrement = false;
				colvarGiaNhap.IsNullable = false;
				colvarGiaNhap.IsPrimaryKey = false;
				colvarGiaNhap.IsForeignKey = false;
				colvarGiaNhap.IsReadOnly = false;
				colvarGiaNhap.DefaultSetting = @"";
				colvarGiaNhap.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGiaNhap);
				
				TableSchema.TableColumn colvarGiaBan = new TableSchema.TableColumn(schema);
				colvarGiaBan.ColumnName = "gia_ban";
				colvarGiaBan.DataType = DbType.Decimal;
				colvarGiaBan.MaxLength = 0;
				colvarGiaBan.AutoIncrement = false;
				colvarGiaBan.IsNullable = false;
				colvarGiaBan.IsPrimaryKey = false;
				colvarGiaBan.IsForeignKey = false;
				colvarGiaBan.IsReadOnly = false;
				
						colvarGiaBan.DefaultSetting = @"((0))";
				colvarGiaBan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGiaBan);
				
				TableSchema.TableColumn colvarDonGia = new TableSchema.TableColumn(schema);
				colvarDonGia.ColumnName = "don_gia";
				colvarDonGia.DataType = DbType.Decimal;
				colvarDonGia.MaxLength = 0;
				colvarDonGia.AutoIncrement = false;
				colvarDonGia.IsNullable = true;
				colvarDonGia.IsPrimaryKey = false;
				colvarDonGia.IsForeignKey = false;
				colvarDonGia.IsReadOnly = false;
				colvarDonGia.DefaultSetting = @"";
				colvarDonGia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDonGia);
				
				TableSchema.TableColumn colvarThangDu = new TableSchema.TableColumn(schema);
				colvarThangDu.ColumnName = "thang_du";
				colvarThangDu.DataType = DbType.Int16;
				colvarThangDu.MaxLength = 0;
				colvarThangDu.AutoIncrement = false;
				colvarThangDu.IsNullable = false;
				colvarThangDu.IsPrimaryKey = false;
				colvarThangDu.IsForeignKey = false;
				colvarThangDu.IsReadOnly = false;
				
						colvarThangDu.DefaultSetting = @"((0))";
				colvarThangDu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarThangDu);
				
				TableSchema.TableColumn colvarSoLuong = new TableSchema.TableColumn(schema);
				colvarSoLuong.ColumnName = "so_luong";
				colvarSoLuong.DataType = DbType.Decimal;
				colvarSoLuong.MaxLength = 0;
				colvarSoLuong.AutoIncrement = false;
				colvarSoLuong.IsNullable = true;
				colvarSoLuong.IsPrimaryKey = false;
				colvarSoLuong.IsForeignKey = false;
				colvarSoLuong.IsReadOnly = false;
				colvarSoLuong.DefaultSetting = @"";
				colvarSoLuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLuong);
				
				TableSchema.TableColumn colvarSluongChia = new TableSchema.TableColumn(schema);
				colvarSluongChia.ColumnName = "sluong_chia";
				colvarSluongChia.DataType = DbType.Decimal;
				colvarSluongChia.MaxLength = 0;
				colvarSluongChia.AutoIncrement = false;
				colvarSluongChia.IsNullable = true;
				colvarSluongChia.IsPrimaryKey = false;
				colvarSluongChia.IsForeignKey = false;
				colvarSluongChia.IsReadOnly = false;
				colvarSluongChia.DefaultSetting = @"";
				colvarSluongChia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSluongChia);
				
				TableSchema.TableColumn colvarSoLo = new TableSchema.TableColumn(schema);
				colvarSoLo.ColumnName = "so_lo";
				colvarSoLo.DataType = DbType.String;
				colvarSoLo.MaxLength = 50;
				colvarSoLo.AutoIncrement = false;
				colvarSoLo.IsNullable = true;
				colvarSoLo.IsPrimaryKey = false;
				colvarSoLo.IsForeignKey = false;
				colvarSoLo.IsReadOnly = false;
				colvarSoLo.DefaultSetting = @"";
				colvarSoLo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLo);
				
				TableSchema.TableColumn colvarChietKhau = new TableSchema.TableColumn(schema);
				colvarChietKhau.ColumnName = "chiet_khau";
				colvarChietKhau.DataType = DbType.Decimal;
				colvarChietKhau.MaxLength = 0;
				colvarChietKhau.AutoIncrement = false;
				colvarChietKhau.IsNullable = true;
				colvarChietKhau.IsPrimaryKey = false;
				colvarChietKhau.IsForeignKey = false;
				colvarChietKhau.IsReadOnly = false;
				colvarChietKhau.DefaultSetting = @"";
				colvarChietKhau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChietKhau);
				
				TableSchema.TableColumn colvarThanhTien = new TableSchema.TableColumn(schema);
				colvarThanhTien.ColumnName = "thanh_tien";
				colvarThanhTien.DataType = DbType.Decimal;
				colvarThanhTien.MaxLength = 0;
				colvarThanhTien.AutoIncrement = false;
				colvarThanhTien.IsNullable = true;
				colvarThanhTien.IsPrimaryKey = false;
				colvarThanhTien.IsForeignKey = false;
				colvarThanhTien.IsReadOnly = false;
				colvarThanhTien.DefaultSetting = @"";
				colvarThanhTien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarThanhTien);
				
				TableSchema.TableColumn colvarVat = new TableSchema.TableColumn(schema);
				colvarVat.ColumnName = "vat";
				colvarVat.DataType = DbType.Decimal;
				colvarVat.MaxLength = 0;
				colvarVat.AutoIncrement = false;
				colvarVat.IsNullable = true;
				colvarVat.IsPrimaryKey = false;
				colvarVat.IsForeignKey = false;
				colvarVat.IsReadOnly = false;
				colvarVat.DefaultSetting = @"";
				colvarVat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVat);
				
				TableSchema.TableColumn colvarIdThuockho = new TableSchema.TableColumn(schema);
				colvarIdThuockho.ColumnName = "id_thuockho";
				colvarIdThuockho.DataType = DbType.Int64;
				colvarIdThuockho.MaxLength = 0;
				colvarIdThuockho.AutoIncrement = false;
				colvarIdThuockho.IsNullable = true;
				colvarIdThuockho.IsPrimaryKey = false;
				colvarIdThuockho.IsForeignKey = false;
				colvarIdThuockho.IsReadOnly = false;
				colvarIdThuockho.DefaultSetting = @"";
				colvarIdThuockho.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThuockho);
				
				TableSchema.TableColumn colvarIdChuyen = new TableSchema.TableColumn(schema);
				colvarIdChuyen.ColumnName = "id_chuyen";
				colvarIdChuyen.DataType = DbType.Int64;
				colvarIdChuyen.MaxLength = 0;
				colvarIdChuyen.AutoIncrement = false;
				colvarIdChuyen.IsNullable = true;
				colvarIdChuyen.IsPrimaryKey = false;
				colvarIdChuyen.IsForeignKey = false;
				colvarIdChuyen.IsReadOnly = false;
				colvarIdChuyen.DefaultSetting = @"";
				colvarIdChuyen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChuyen);
				
				TableSchema.TableColumn colvarMaNhacungcap = new TableSchema.TableColumn(schema);
				colvarMaNhacungcap.ColumnName = "ma_nhacungcap";
				colvarMaNhacungcap.DataType = DbType.String;
				colvarMaNhacungcap.MaxLength = 20;
				colvarMaNhacungcap.AutoIncrement = false;
				colvarMaNhacungcap.IsNullable = true;
				colvarMaNhacungcap.IsPrimaryKey = false;
				colvarMaNhacungcap.IsForeignKey = false;
				colvarMaNhacungcap.IsReadOnly = false;
				colvarMaNhacungcap.DefaultSetting = @"";
				colvarMaNhacungcap.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaNhacungcap);
				
				TableSchema.TableColumn colvarMotaThem = new TableSchema.TableColumn(schema);
				colvarMotaThem.ColumnName = "mota_them";
				colvarMotaThem.DataType = DbType.String;
				colvarMotaThem.MaxLength = 200;
				colvarMotaThem.AutoIncrement = false;
				colvarMotaThem.IsNullable = true;
				colvarMotaThem.IsPrimaryKey = false;
				colvarMotaThem.IsForeignKey = false;
				colvarMotaThem.IsReadOnly = false;
				colvarMotaThem.DefaultSetting = @"";
				colvarMotaThem.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMotaThem);
				
				TableSchema.TableColumn colvarKieuThuocvattu = new TableSchema.TableColumn(schema);
				colvarKieuThuocvattu.ColumnName = "kieu_thuocvattu";
				colvarKieuThuocvattu.DataType = DbType.String;
				colvarKieuThuocvattu.MaxLength = 10;
				colvarKieuThuocvattu.AutoIncrement = false;
				colvarKieuThuocvattu.IsNullable = true;
				colvarKieuThuocvattu.IsPrimaryKey = false;
				colvarKieuThuocvattu.IsForeignKey = false;
				colvarKieuThuocvattu.IsReadOnly = false;
				colvarKieuThuocvattu.DefaultSetting = @"";
				colvarKieuThuocvattu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKieuThuocvattu);
				
				TableSchema.TableColumn colvarCoBhyt = new TableSchema.TableColumn(schema);
				colvarCoBhyt.ColumnName = "co_BHYT";
				colvarCoBhyt.DataType = DbType.Byte;
				colvarCoBhyt.MaxLength = 0;
				colvarCoBhyt.AutoIncrement = false;
				colvarCoBhyt.IsNullable = true;
				colvarCoBhyt.IsPrimaryKey = false;
				colvarCoBhyt.IsForeignKey = false;
				colvarCoBhyt.IsReadOnly = false;
				colvarCoBhyt.DefaultSetting = @"";
				colvarCoBhyt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCoBhyt);
				
				TableSchema.TableColumn colvarGiaBhyt = new TableSchema.TableColumn(schema);
				colvarGiaBhyt.ColumnName = "gia_BHYT";
				colvarGiaBhyt.DataType = DbType.Decimal;
				colvarGiaBhyt.MaxLength = 0;
				colvarGiaBhyt.AutoIncrement = false;
				colvarGiaBhyt.IsNullable = true;
				colvarGiaBhyt.IsPrimaryKey = false;
				colvarGiaBhyt.IsForeignKey = false;
				colvarGiaBhyt.IsReadOnly = false;
				colvarGiaBhyt.DefaultSetting = @"";
				colvarGiaBhyt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGiaBhyt);
				
				TableSchema.TableColumn colvarGiaBhytCu = new TableSchema.TableColumn(schema);
				colvarGiaBhytCu.ColumnName = "gia_BHYT_cu";
				colvarGiaBhytCu.DataType = DbType.Decimal;
				colvarGiaBhytCu.MaxLength = 0;
				colvarGiaBhytCu.AutoIncrement = false;
				colvarGiaBhytCu.IsNullable = true;
				colvarGiaBhytCu.IsPrimaryKey = false;
				colvarGiaBhytCu.IsForeignKey = false;
				colvarGiaBhytCu.IsReadOnly = false;
				colvarGiaBhytCu.DefaultSetting = @"";
				colvarGiaBhytCu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGiaBhytCu);
				
				TableSchema.TableColumn colvarGiaPhuthuDungtuyen = new TableSchema.TableColumn(schema);
				colvarGiaPhuthuDungtuyen.ColumnName = "gia_phuthu_dungtuyen";
				colvarGiaPhuthuDungtuyen.DataType = DbType.Decimal;
				colvarGiaPhuthuDungtuyen.MaxLength = 0;
				colvarGiaPhuthuDungtuyen.AutoIncrement = false;
				colvarGiaPhuthuDungtuyen.IsNullable = true;
				colvarGiaPhuthuDungtuyen.IsPrimaryKey = false;
				colvarGiaPhuthuDungtuyen.IsForeignKey = false;
				colvarGiaPhuthuDungtuyen.IsReadOnly = false;
				colvarGiaPhuthuDungtuyen.DefaultSetting = @"";
				colvarGiaPhuthuDungtuyen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGiaPhuthuDungtuyen);
				
				TableSchema.TableColumn colvarGiaPhuthuTraituyen = new TableSchema.TableColumn(schema);
				colvarGiaPhuthuTraituyen.ColumnName = "gia_phuthu_traituyen";
				colvarGiaPhuthuTraituyen.DataType = DbType.Decimal;
				colvarGiaPhuthuTraituyen.MaxLength = 0;
				colvarGiaPhuthuTraituyen.AutoIncrement = false;
				colvarGiaPhuthuTraituyen.IsNullable = true;
				colvarGiaPhuthuTraituyen.IsPrimaryKey = false;
				colvarGiaPhuthuTraituyen.IsForeignKey = false;
				colvarGiaPhuthuTraituyen.IsReadOnly = false;
				colvarGiaPhuthuTraituyen.DefaultSetting = @"";
				colvarGiaPhuthuTraituyen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGiaPhuthuTraituyen);
				
				TableSchema.TableColumn colvarNgayNhap = new TableSchema.TableColumn(schema);
				colvarNgayNhap.ColumnName = "ngay_nhap";
				colvarNgayNhap.DataType = DbType.DateTime;
				colvarNgayNhap.MaxLength = 0;
				colvarNgayNhap.AutoIncrement = false;
				colvarNgayNhap.IsNullable = true;
				colvarNgayNhap.IsPrimaryKey = false;
				colvarNgayNhap.IsForeignKey = false;
				colvarNgayNhap.IsReadOnly = false;
				colvarNgayNhap.DefaultSetting = @"";
				colvarNgayNhap.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayNhap);
				
				TableSchema.TableColumn colvarSoDky = new TableSchema.TableColumn(schema);
				colvarSoDky.ColumnName = "so_dky";
				colvarSoDky.DataType = DbType.String;
				colvarSoDky.MaxLength = 30;
				colvarSoDky.AutoIncrement = false;
				colvarSoDky.IsNullable = true;
				colvarSoDky.IsPrimaryKey = false;
				colvarSoDky.IsForeignKey = false;
				colvarSoDky.IsReadOnly = false;
				colvarSoDky.DefaultSetting = @"";
				colvarSoDky.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoDky);
				
				TableSchema.TableColumn colvarSoQdinhthau = new TableSchema.TableColumn(schema);
				colvarSoQdinhthau.ColumnName = "so_qdinhthau";
				colvarSoQdinhthau.DataType = DbType.String;
				colvarSoQdinhthau.MaxLength = 30;
				colvarSoQdinhthau.AutoIncrement = false;
				colvarSoQdinhthau.IsNullable = true;
				colvarSoQdinhthau.IsPrimaryKey = false;
				colvarSoQdinhthau.IsForeignKey = false;
				colvarSoQdinhthau.IsReadOnly = false;
				colvarSoQdinhthau.DefaultSetting = @"";
				colvarSoQdinhthau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoQdinhthau);
				
				TableSchema.TableColumn colvarIdQdinh = new TableSchema.TableColumn(schema);
				colvarIdQdinh.ColumnName = "id_qdinh";
				colvarIdQdinh.DataType = DbType.Int64;
				colvarIdQdinh.MaxLength = 0;
				colvarIdQdinh.AutoIncrement = false;
				colvarIdQdinh.IsNullable = true;
				colvarIdQdinh.IsPrimaryKey = false;
				colvarIdQdinh.IsForeignKey = false;
				colvarIdQdinh.IsReadOnly = false;
				
						colvarIdQdinh.DefaultSetting = @"((0))";
				colvarIdQdinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdQdinh);
				
				TableSchema.TableColumn colvarIdThau = new TableSchema.TableColumn(schema);
				colvarIdThau.ColumnName = "id_thau";
				colvarIdThau.DataType = DbType.Int64;
				colvarIdThau.MaxLength = 0;
				colvarIdThau.AutoIncrement = false;
				colvarIdThau.IsNullable = true;
				colvarIdThau.IsPrimaryKey = false;
				colvarIdThau.IsForeignKey = false;
				colvarIdThau.IsReadOnly = false;
				
						colvarIdThau.DefaultSetting = @"((0))";
				colvarIdThau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThau);
				
				TableSchema.TableColumn colvarIdThauCt = new TableSchema.TableColumn(schema);
				colvarIdThauCt.ColumnName = "id_thau_ct";
				colvarIdThauCt.DataType = DbType.Int64;
				colvarIdThauCt.MaxLength = 0;
				colvarIdThauCt.AutoIncrement = false;
				colvarIdThauCt.IsNullable = true;
				colvarIdThauCt.IsPrimaryKey = false;
				colvarIdThauCt.IsForeignKey = false;
				colvarIdThauCt.IsReadOnly = false;
				
						colvarIdThauCt.DefaultSetting = @"((0))";
				colvarIdThauCt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThauCt);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("t_phieu_nhapxuatthuoc_chitiet",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdPhieuchitiet")]
		[Bindable(true)]
		public long IdPhieuchitiet 
		{
			get { return GetColumnValue<long>(Columns.IdPhieuchitiet); }
			set { SetColumnValue(Columns.IdPhieuchitiet, value); }
		}
		  
		[XmlAttribute("IdPhieu")]
		[Bindable(true)]
		public long IdPhieu 
		{
			get { return GetColumnValue<long>(Columns.IdPhieu); }
			set { SetColumnValue(Columns.IdPhieu, value); }
		}
		  
		[XmlAttribute("NgayHethan")]
		[Bindable(true)]
		public DateTime NgayHethan 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayHethan); }
			set { SetColumnValue(Columns.NgayHethan, value); }
		}
		  
		[XmlAttribute("IdThuoc")]
		[Bindable(true)]
		public int IdThuoc 
		{
			get { return GetColumnValue<int>(Columns.IdThuoc); }
			set { SetColumnValue(Columns.IdThuoc, value); }
		}
		  
		[XmlAttribute("GiaNhap")]
		[Bindable(true)]
		public decimal GiaNhap 
		{
			get { return GetColumnValue<decimal>(Columns.GiaNhap); }
			set { SetColumnValue(Columns.GiaNhap, value); }
		}
		  
		[XmlAttribute("GiaBan")]
		[Bindable(true)]
		public decimal GiaBan 
		{
			get { return GetColumnValue<decimal>(Columns.GiaBan); }
			set { SetColumnValue(Columns.GiaBan, value); }
		}
		  
		[XmlAttribute("DonGia")]
		[Bindable(true)]
		public decimal? DonGia 
		{
			get { return GetColumnValue<decimal?>(Columns.DonGia); }
			set { SetColumnValue(Columns.DonGia, value); }
		}
		  
		[XmlAttribute("ThangDu")]
		[Bindable(true)]
		public short ThangDu 
		{
			get { return GetColumnValue<short>(Columns.ThangDu); }
			set { SetColumnValue(Columns.ThangDu, value); }
		}
		  
		[XmlAttribute("SoLuong")]
		[Bindable(true)]
		public decimal? SoLuong 
		{
			get { return GetColumnValue<decimal?>(Columns.SoLuong); }
			set { SetColumnValue(Columns.SoLuong, value); }
		}
		  
		[XmlAttribute("SluongChia")]
		[Bindable(true)]
		public decimal? SluongChia 
		{
			get { return GetColumnValue<decimal?>(Columns.SluongChia); }
			set { SetColumnValue(Columns.SluongChia, value); }
		}
		  
		[XmlAttribute("SoLo")]
		[Bindable(true)]
		public string SoLo 
		{
			get { return GetColumnValue<string>(Columns.SoLo); }
			set { SetColumnValue(Columns.SoLo, value); }
		}
		  
		[XmlAttribute("ChietKhau")]
		[Bindable(true)]
		public decimal? ChietKhau 
		{
			get { return GetColumnValue<decimal?>(Columns.ChietKhau); }
			set { SetColumnValue(Columns.ChietKhau, value); }
		}
		  
		[XmlAttribute("ThanhTien")]
		[Bindable(true)]
		public decimal? ThanhTien 
		{
			get { return GetColumnValue<decimal?>(Columns.ThanhTien); }
			set { SetColumnValue(Columns.ThanhTien, value); }
		}
		  
		[XmlAttribute("Vat")]
		[Bindable(true)]
		public decimal? Vat 
		{
			get { return GetColumnValue<decimal?>(Columns.Vat); }
			set { SetColumnValue(Columns.Vat, value); }
		}
		  
		[XmlAttribute("IdThuockho")]
		[Bindable(true)]
		public long? IdThuockho 
		{
			get { return GetColumnValue<long?>(Columns.IdThuockho); }
			set { SetColumnValue(Columns.IdThuockho, value); }
		}
		  
		[XmlAttribute("IdChuyen")]
		[Bindable(true)]
		public long? IdChuyen 
		{
			get { return GetColumnValue<long?>(Columns.IdChuyen); }
			set { SetColumnValue(Columns.IdChuyen, value); }
		}
		  
		[XmlAttribute("MaNhacungcap")]
		[Bindable(true)]
		public string MaNhacungcap 
		{
			get { return GetColumnValue<string>(Columns.MaNhacungcap); }
			set { SetColumnValue(Columns.MaNhacungcap, value); }
		}
		  
		[XmlAttribute("MotaThem")]
		[Bindable(true)]
		public string MotaThem 
		{
			get { return GetColumnValue<string>(Columns.MotaThem); }
			set { SetColumnValue(Columns.MotaThem, value); }
		}
		  
		[XmlAttribute("KieuThuocvattu")]
		[Bindable(true)]
		public string KieuThuocvattu 
		{
			get { return GetColumnValue<string>(Columns.KieuThuocvattu); }
			set { SetColumnValue(Columns.KieuThuocvattu, value); }
		}
		  
		[XmlAttribute("CoBhyt")]
		[Bindable(true)]
		public byte? CoBhyt 
		{
			get { return GetColumnValue<byte?>(Columns.CoBhyt); }
			set { SetColumnValue(Columns.CoBhyt, value); }
		}
		  
		[XmlAttribute("GiaBhyt")]
		[Bindable(true)]
		public decimal? GiaBhyt 
		{
			get { return GetColumnValue<decimal?>(Columns.GiaBhyt); }
			set { SetColumnValue(Columns.GiaBhyt, value); }
		}
		  
		[XmlAttribute("GiaBhytCu")]
		[Bindable(true)]
		public decimal? GiaBhytCu 
		{
			get { return GetColumnValue<decimal?>(Columns.GiaBhytCu); }
			set { SetColumnValue(Columns.GiaBhytCu, value); }
		}
		  
		[XmlAttribute("GiaPhuthuDungtuyen")]
		[Bindable(true)]
		public decimal? GiaPhuthuDungtuyen 
		{
			get { return GetColumnValue<decimal?>(Columns.GiaPhuthuDungtuyen); }
			set { SetColumnValue(Columns.GiaPhuthuDungtuyen, value); }
		}
		  
		[XmlAttribute("GiaPhuthuTraituyen")]
		[Bindable(true)]
		public decimal? GiaPhuthuTraituyen 
		{
			get { return GetColumnValue<decimal?>(Columns.GiaPhuthuTraituyen); }
			set { SetColumnValue(Columns.GiaPhuthuTraituyen, value); }
		}
		  
		[XmlAttribute("NgayNhap")]
		[Bindable(true)]
		public DateTime? NgayNhap 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayNhap); }
			set { SetColumnValue(Columns.NgayNhap, value); }
		}
		  
		[XmlAttribute("SoDky")]
		[Bindable(true)]
		public string SoDky 
		{
			get { return GetColumnValue<string>(Columns.SoDky); }
			set { SetColumnValue(Columns.SoDky, value); }
		}
		  
		[XmlAttribute("SoQdinhthau")]
		[Bindable(true)]
		public string SoQdinhthau 
		{
			get { return GetColumnValue<string>(Columns.SoQdinhthau); }
			set { SetColumnValue(Columns.SoQdinhthau, value); }
		}
		  
		[XmlAttribute("IdQdinh")]
		[Bindable(true)]
		public long? IdQdinh 
		{
			get { return GetColumnValue<long?>(Columns.IdQdinh); }
			set { SetColumnValue(Columns.IdQdinh, value); }
		}
		  
		[XmlAttribute("IdThau")]
		[Bindable(true)]
		public long? IdThau 
		{
			get { return GetColumnValue<long?>(Columns.IdThau); }
			set { SetColumnValue(Columns.IdThau, value); }
		}
		  
		[XmlAttribute("IdThauCt")]
		[Bindable(true)]
		public long? IdThauCt 
		{
			get { return GetColumnValue<long?>(Columns.IdThauCt); }
			set { SetColumnValue(Columns.IdThauCt, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varIdPhieu,DateTime varNgayHethan,int varIdThuoc,decimal varGiaNhap,decimal varGiaBan,decimal? varDonGia,short varThangDu,decimal? varSoLuong,decimal? varSluongChia,string varSoLo,decimal? varChietKhau,decimal? varThanhTien,decimal? varVat,long? varIdThuockho,long? varIdChuyen,string varMaNhacungcap,string varMotaThem,string varKieuThuocvattu,byte? varCoBhyt,decimal? varGiaBhyt,decimal? varGiaBhytCu,decimal? varGiaPhuthuDungtuyen,decimal? varGiaPhuthuTraituyen,DateTime? varNgayNhap,string varSoDky,string varSoQdinhthau,long? varIdQdinh,long? varIdThau,long? varIdThauCt)
		{
			TPhieuNhapxuatthuocChitiet item = new TPhieuNhapxuatthuocChitiet();
			
			item.IdPhieu = varIdPhieu;
			
			item.NgayHethan = varNgayHethan;
			
			item.IdThuoc = varIdThuoc;
			
			item.GiaNhap = varGiaNhap;
			
			item.GiaBan = varGiaBan;
			
			item.DonGia = varDonGia;
			
			item.ThangDu = varThangDu;
			
			item.SoLuong = varSoLuong;
			
			item.SluongChia = varSluongChia;
			
			item.SoLo = varSoLo;
			
			item.ChietKhau = varChietKhau;
			
			item.ThanhTien = varThanhTien;
			
			item.Vat = varVat;
			
			item.IdThuockho = varIdThuockho;
			
			item.IdChuyen = varIdChuyen;
			
			item.MaNhacungcap = varMaNhacungcap;
			
			item.MotaThem = varMotaThem;
			
			item.KieuThuocvattu = varKieuThuocvattu;
			
			item.CoBhyt = varCoBhyt;
			
			item.GiaBhyt = varGiaBhyt;
			
			item.GiaBhytCu = varGiaBhytCu;
			
			item.GiaPhuthuDungtuyen = varGiaPhuthuDungtuyen;
			
			item.GiaPhuthuTraituyen = varGiaPhuthuTraituyen;
			
			item.NgayNhap = varNgayNhap;
			
			item.SoDky = varSoDky;
			
			item.SoQdinhthau = varSoQdinhthau;
			
			item.IdQdinh = varIdQdinh;
			
			item.IdThau = varIdThau;
			
			item.IdThauCt = varIdThauCt;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varIdPhieuchitiet,long varIdPhieu,DateTime varNgayHethan,int varIdThuoc,decimal varGiaNhap,decimal varGiaBan,decimal? varDonGia,short varThangDu,decimal? varSoLuong,decimal? varSluongChia,string varSoLo,decimal? varChietKhau,decimal? varThanhTien,decimal? varVat,long? varIdThuockho,long? varIdChuyen,string varMaNhacungcap,string varMotaThem,string varKieuThuocvattu,byte? varCoBhyt,decimal? varGiaBhyt,decimal? varGiaBhytCu,decimal? varGiaPhuthuDungtuyen,decimal? varGiaPhuthuTraituyen,DateTime? varNgayNhap,string varSoDky,string varSoQdinhthau,long? varIdQdinh,long? varIdThau,long? varIdThauCt)
		{
			TPhieuNhapxuatthuocChitiet item = new TPhieuNhapxuatthuocChitiet();
			
				item.IdPhieuchitiet = varIdPhieuchitiet;
			
				item.IdPhieu = varIdPhieu;
			
				item.NgayHethan = varNgayHethan;
			
				item.IdThuoc = varIdThuoc;
			
				item.GiaNhap = varGiaNhap;
			
				item.GiaBan = varGiaBan;
			
				item.DonGia = varDonGia;
			
				item.ThangDu = varThangDu;
			
				item.SoLuong = varSoLuong;
			
				item.SluongChia = varSluongChia;
			
				item.SoLo = varSoLo;
			
				item.ChietKhau = varChietKhau;
			
				item.ThanhTien = varThanhTien;
			
				item.Vat = varVat;
			
				item.IdThuockho = varIdThuockho;
			
				item.IdChuyen = varIdChuyen;
			
				item.MaNhacungcap = varMaNhacungcap;
			
				item.MotaThem = varMotaThem;
			
				item.KieuThuocvattu = varKieuThuocvattu;
			
				item.CoBhyt = varCoBhyt;
			
				item.GiaBhyt = varGiaBhyt;
			
				item.GiaBhytCu = varGiaBhytCu;
			
				item.GiaPhuthuDungtuyen = varGiaPhuthuDungtuyen;
			
				item.GiaPhuthuTraituyen = varGiaPhuthuTraituyen;
			
				item.NgayNhap = varNgayNhap;
			
				item.SoDky = varSoDky;
			
				item.SoQdinhthau = varSoQdinhthau;
			
				item.IdQdinh = varIdQdinh;
			
				item.IdThau = varIdThau;
			
				item.IdThauCt = varIdThauCt;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdPhieuchitietColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdPhieuColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayHethanColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThuocColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn GiaNhapColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn GiaBanColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn DonGiaColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn ThangDuColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLuongColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn SluongChiaColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLoColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn ChietKhauColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn ThanhTienColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn VatColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThuockhoColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn IdChuyenColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn MaNhacungcapColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn MotaThemColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn KieuThuocvattuColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn CoBhytColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn GiaBhytColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn GiaBhytCuColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn GiaPhuthuDungtuyenColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        public static TableSchema.TableColumn GiaPhuthuTraituyenColumn
        {
            get { return Schema.Columns[23]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayNhapColumn
        {
            get { return Schema.Columns[24]; }
        }
        
        
        
        public static TableSchema.TableColumn SoDkyColumn
        {
            get { return Schema.Columns[25]; }
        }
        
        
        
        public static TableSchema.TableColumn SoQdinhthauColumn
        {
            get { return Schema.Columns[26]; }
        }
        
        
        
        public static TableSchema.TableColumn IdQdinhColumn
        {
            get { return Schema.Columns[27]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThauColumn
        {
            get { return Schema.Columns[28]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThauCtColumn
        {
            get { return Schema.Columns[29]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdPhieuchitiet = @"id_phieuchitiet";
			 public static string IdPhieu = @"id_phieu";
			 public static string NgayHethan = @"ngay_hethan";
			 public static string IdThuoc = @"id_thuoc";
			 public static string GiaNhap = @"gia_nhap";
			 public static string GiaBan = @"gia_ban";
			 public static string DonGia = @"don_gia";
			 public static string ThangDu = @"thang_du";
			 public static string SoLuong = @"so_luong";
			 public static string SluongChia = @"sluong_chia";
			 public static string SoLo = @"so_lo";
			 public static string ChietKhau = @"chiet_khau";
			 public static string ThanhTien = @"thanh_tien";
			 public static string Vat = @"vat";
			 public static string IdThuockho = @"id_thuockho";
			 public static string IdChuyen = @"id_chuyen";
			 public static string MaNhacungcap = @"ma_nhacungcap";
			 public static string MotaThem = @"mota_them";
			 public static string KieuThuocvattu = @"kieu_thuocvattu";
			 public static string CoBhyt = @"co_BHYT";
			 public static string GiaBhyt = @"gia_BHYT";
			 public static string GiaBhytCu = @"gia_BHYT_cu";
			 public static string GiaPhuthuDungtuyen = @"gia_phuthu_dungtuyen";
			 public static string GiaPhuthuTraituyen = @"gia_phuthu_traituyen";
			 public static string NgayNhap = @"ngay_nhap";
			 public static string SoDky = @"so_dky";
			 public static string SoQdinhthau = @"so_qdinhthau";
			 public static string IdQdinh = @"id_qdinh";
			 public static string IdThau = @"id_thau";
			 public static string IdThauCt = @"id_thau_ct";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
