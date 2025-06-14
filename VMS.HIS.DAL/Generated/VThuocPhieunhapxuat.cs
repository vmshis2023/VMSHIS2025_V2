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
namespace VMS.HIS.DAL{
    /// <summary>
    /// Strongly-typed collection for the VThuocPhieunhapxuat class.
    /// </summary>
    [Serializable]
    public partial class VThuocPhieunhapxuatCollection : ReadOnlyList<VThuocPhieunhapxuat, VThuocPhieunhapxuatCollection>
    {        
        public VThuocPhieunhapxuatCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the v_thuoc_phieunhapxuat view.
    /// </summary>
    [Serializable]
    public partial class VThuocPhieunhapxuat : ReadOnlyRecord<VThuocPhieunhapxuat>, IReadOnlyRecord
    {
    
	    #region Default Settings
	    protected static void SetSQLProps() 
	    {
		    GetTableSchema();
	    }
	    #endregion
        #region Schema Accessor
	    public static TableSchema.Table Schema
        {
            get
            {
                if (BaseSchema == null)
                {
                    SetSQLProps();
                }
                return BaseSchema;
            }
        }
    	
        private static void GetTableSchema() 
        {
            if(!IsSchemaInitialized)
            {
                //Schema declaration
                TableSchema.Table schema = new TableSchema.Table("v_thuoc_phieunhapxuat", TableType.View, DataService.GetInstance("ORM"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarIdPhieu = new TableSchema.TableColumn(schema);
                colvarIdPhieu.ColumnName = "id_phieu";
                colvarIdPhieu.DataType = DbType.Int64;
                colvarIdPhieu.MaxLength = 0;
                colvarIdPhieu.AutoIncrement = false;
                colvarIdPhieu.IsNullable = false;
                colvarIdPhieu.IsPrimaryKey = false;
                colvarIdPhieu.IsForeignKey = false;
                colvarIdPhieu.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdPhieu);
                
                TableSchema.TableColumn colvarKieuThuocvattu = new TableSchema.TableColumn(schema);
                colvarKieuThuocvattu.ColumnName = "kieu_thuocvattu";
                colvarKieuThuocvattu.DataType = DbType.String;
                colvarKieuThuocvattu.MaxLength = 10;
                colvarKieuThuocvattu.AutoIncrement = false;
                colvarKieuThuocvattu.IsNullable = true;
                colvarKieuThuocvattu.IsPrimaryKey = false;
                colvarKieuThuocvattu.IsForeignKey = false;
                colvarKieuThuocvattu.IsReadOnly = false;
                
                schema.Columns.Add(colvarKieuThuocvattu);
                
                TableSchema.TableColumn colvarMaPhieu = new TableSchema.TableColumn(schema);
                colvarMaPhieu.ColumnName = "ma_phieu";
                colvarMaPhieu.DataType = DbType.String;
                colvarMaPhieu.MaxLength = 50;
                colvarMaPhieu.AutoIncrement = false;
                colvarMaPhieu.IsNullable = false;
                colvarMaPhieu.IsPrimaryKey = false;
                colvarMaPhieu.IsForeignKey = false;
                colvarMaPhieu.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaPhieu);
                
                TableSchema.TableColumn colvarSoHoadon = new TableSchema.TableColumn(schema);
                colvarSoHoadon.ColumnName = "so_hoadon";
                colvarSoHoadon.DataType = DbType.String;
                colvarSoHoadon.MaxLength = 50;
                colvarSoHoadon.AutoIncrement = false;
                colvarSoHoadon.IsNullable = false;
                colvarSoHoadon.IsPrimaryKey = false;
                colvarSoHoadon.IsForeignKey = false;
                colvarSoHoadon.IsReadOnly = false;
                
                schema.Columns.Add(colvarSoHoadon);
                
                TableSchema.TableColumn colvarNgayLap = new TableSchema.TableColumn(schema);
                colvarNgayLap.ColumnName = "ngay_lap";
                colvarNgayLap.DataType = DbType.DateTime;
                colvarNgayLap.MaxLength = 0;
                colvarNgayLap.AutoIncrement = false;
                colvarNgayLap.IsNullable = true;
                colvarNgayLap.IsPrimaryKey = false;
                colvarNgayLap.IsForeignKey = false;
                colvarNgayLap.IsReadOnly = false;
                
                schema.Columns.Add(colvarNgayLap);
                
                TableSchema.TableColumn colvarNgayHoadon = new TableSchema.TableColumn(schema);
                colvarNgayHoadon.ColumnName = "ngay_hoadon";
                colvarNgayHoadon.DataType = DbType.DateTime;
                colvarNgayHoadon.MaxLength = 0;
                colvarNgayHoadon.AutoIncrement = false;
                colvarNgayHoadon.IsNullable = false;
                colvarNgayHoadon.IsPrimaryKey = false;
                colvarNgayHoadon.IsForeignKey = false;
                colvarNgayHoadon.IsReadOnly = false;
                
                schema.Columns.Add(colvarNgayHoadon);
                
                TableSchema.TableColumn colvarIdKhonhap = new TableSchema.TableColumn(schema);
                colvarIdKhonhap.ColumnName = "id_khonhap";
                colvarIdKhonhap.DataType = DbType.Int16;
                colvarIdKhonhap.MaxLength = 0;
                colvarIdKhonhap.AutoIncrement = false;
                colvarIdKhonhap.IsNullable = true;
                colvarIdKhonhap.IsPrimaryKey = false;
                colvarIdKhonhap.IsForeignKey = false;
                colvarIdKhonhap.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdKhonhap);
                
                TableSchema.TableColumn colvarVat = new TableSchema.TableColumn(schema);
                colvarVat.ColumnName = "VAT";
                colvarVat.DataType = DbType.Decimal;
                colvarVat.MaxLength = 0;
                colvarVat.AutoIncrement = false;
                colvarVat.IsNullable = true;
                colvarVat.IsPrimaryKey = false;
                colvarVat.IsForeignKey = false;
                colvarVat.IsReadOnly = false;
                
                schema.Columns.Add(colvarVat);
                
                TableSchema.TableColumn colvarMaNhacungcap = new TableSchema.TableColumn(schema);
                colvarMaNhacungcap.ColumnName = "MA_NHACUNGCAP";
                colvarMaNhacungcap.DataType = DbType.String;
                colvarMaNhacungcap.MaxLength = 20;
                colvarMaNhacungcap.AutoIncrement = false;
                colvarMaNhacungcap.IsNullable = true;
                colvarMaNhacungcap.IsPrimaryKey = false;
                colvarMaNhacungcap.IsForeignKey = false;
                colvarMaNhacungcap.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaNhacungcap);
                
                TableSchema.TableColumn colvarMotaThem = new TableSchema.TableColumn(schema);
                colvarMotaThem.ColumnName = "mota_them";
                colvarMotaThem.DataType = DbType.String;
                colvarMotaThem.MaxLength = 255;
                colvarMotaThem.AutoIncrement = false;
                colvarMotaThem.IsNullable = true;
                colvarMotaThem.IsPrimaryKey = false;
                colvarMotaThem.IsForeignKey = false;
                colvarMotaThem.IsReadOnly = false;
                
                schema.Columns.Add(colvarMotaThem);
                
                TableSchema.TableColumn colvarNguoiGiao = new TableSchema.TableColumn(schema);
                colvarNguoiGiao.ColumnName = "NGUOI_GIAO";
                colvarNguoiGiao.DataType = DbType.String;
                colvarNguoiGiao.MaxLength = 100;
                colvarNguoiGiao.AutoIncrement = false;
                colvarNguoiGiao.IsNullable = true;
                colvarNguoiGiao.IsPrimaryKey = false;
                colvarNguoiGiao.IsForeignKey = false;
                colvarNguoiGiao.IsReadOnly = false;
                
                schema.Columns.Add(colvarNguoiGiao);
                
                TableSchema.TableColumn colvarIdNhanvien = new TableSchema.TableColumn(schema);
                colvarIdNhanvien.ColumnName = "id_nhanvien";
                colvarIdNhanvien.DataType = DbType.Int16;
                colvarIdNhanvien.MaxLength = 0;
                colvarIdNhanvien.AutoIncrement = false;
                colvarIdNhanvien.IsNullable = true;
                colvarIdNhanvien.IsPrimaryKey = false;
                colvarIdNhanvien.IsForeignKey = false;
                colvarIdNhanvien.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdNhanvien);
                
                TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
                colvarNgayTao.ColumnName = "NGAY_TAO";
                colvarNgayTao.DataType = DbType.DateTime;
                colvarNgayTao.MaxLength = 0;
                colvarNgayTao.AutoIncrement = false;
                colvarNgayTao.IsNullable = true;
                colvarNgayTao.IsPrimaryKey = false;
                colvarNgayTao.IsForeignKey = false;
                colvarNgayTao.IsReadOnly = false;
                
                schema.Columns.Add(colvarNgayTao);
                
                TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
                colvarNguoiTao.ColumnName = "NGUOI_TAO";
                colvarNguoiTao.DataType = DbType.String;
                colvarNguoiTao.MaxLength = 50;
                colvarNguoiTao.AutoIncrement = false;
                colvarNguoiTao.IsNullable = true;
                colvarNguoiTao.IsPrimaryKey = false;
                colvarNguoiTao.IsForeignKey = false;
                colvarNguoiTao.IsReadOnly = false;
                
                schema.Columns.Add(colvarNguoiTao);
                
                TableSchema.TableColumn colvarNguoiSua = new TableSchema.TableColumn(schema);
                colvarNguoiSua.ColumnName = "NGUOI_SUA";
                colvarNguoiSua.DataType = DbType.String;
                colvarNguoiSua.MaxLength = 50;
                colvarNguoiSua.AutoIncrement = false;
                colvarNguoiSua.IsNullable = true;
                colvarNguoiSua.IsPrimaryKey = false;
                colvarNguoiSua.IsForeignKey = false;
                colvarNguoiSua.IsReadOnly = false;
                
                schema.Columns.Add(colvarNguoiSua);
                
                TableSchema.TableColumn colvarNgaySua = new TableSchema.TableColumn(schema);
                colvarNgaySua.ColumnName = "NGAY_SUA";
                colvarNgaySua.DataType = DbType.DateTime;
                colvarNgaySua.MaxLength = 0;
                colvarNgaySua.AutoIncrement = false;
                colvarNgaySua.IsNullable = true;
                colvarNgaySua.IsPrimaryKey = false;
                colvarNgaySua.IsForeignKey = false;
                colvarNgaySua.IsReadOnly = false;
                
                schema.Columns.Add(colvarNgaySua);
                
                TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
                colvarTrangThai.ColumnName = "TRANG_THAI";
                colvarTrangThai.DataType = DbType.Byte;
                colvarTrangThai.MaxLength = 0;
                colvarTrangThai.AutoIncrement = false;
                colvarTrangThai.IsNullable = true;
                colvarTrangThai.IsPrimaryKey = false;
                colvarTrangThai.IsForeignKey = false;
                colvarTrangThai.IsReadOnly = false;
                
                schema.Columns.Add(colvarTrangThai);
                
                TableSchema.TableColumn colvarNgayXacnhan = new TableSchema.TableColumn(schema);
                colvarNgayXacnhan.ColumnName = "ngay_xacnhan";
                colvarNgayXacnhan.DataType = DbType.DateTime;
                colvarNgayXacnhan.MaxLength = 0;
                colvarNgayXacnhan.AutoIncrement = false;
                colvarNgayXacnhan.IsNullable = true;
                colvarNgayXacnhan.IsPrimaryKey = false;
                colvarNgayXacnhan.IsForeignKey = false;
                colvarNgayXacnhan.IsReadOnly = false;
                
                schema.Columns.Add(colvarNgayXacnhan);
                
                TableSchema.TableColumn colvarNguoiXacnhan = new TableSchema.TableColumn(schema);
                colvarNguoiXacnhan.ColumnName = "nguoi_xacnhan";
                colvarNguoiXacnhan.DataType = DbType.String;
                colvarNguoiXacnhan.MaxLength = 50;
                colvarNguoiXacnhan.AutoIncrement = false;
                colvarNguoiXacnhan.IsNullable = true;
                colvarNguoiXacnhan.IsPrimaryKey = false;
                colvarNguoiXacnhan.IsForeignKey = false;
                colvarNguoiXacnhan.IsReadOnly = false;
                
                schema.Columns.Add(colvarNguoiXacnhan);
                
                TableSchema.TableColumn colvarIdNhanvienXacnhan = new TableSchema.TableColumn(schema);
                colvarIdNhanvienXacnhan.ColumnName = "id_nhanvien_xacnhan";
                colvarIdNhanvienXacnhan.DataType = DbType.Int16;
                colvarIdNhanvienXacnhan.MaxLength = 0;
                colvarIdNhanvienXacnhan.AutoIncrement = false;
                colvarIdNhanvienXacnhan.IsNullable = true;
                colvarIdNhanvienXacnhan.IsPrimaryKey = false;
                colvarIdNhanvienXacnhan.IsForeignKey = false;
                colvarIdNhanvienXacnhan.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdNhanvienXacnhan);
                
                TableSchema.TableColumn colvarLoaiPhieu = new TableSchema.TableColumn(schema);
                colvarLoaiPhieu.ColumnName = "loai_phieu";
                colvarLoaiPhieu.DataType = DbType.Byte;
                colvarLoaiPhieu.MaxLength = 0;
                colvarLoaiPhieu.AutoIncrement = false;
                colvarLoaiPhieu.IsNullable = true;
                colvarLoaiPhieu.IsPrimaryKey = false;
                colvarLoaiPhieu.IsForeignKey = false;
                colvarLoaiPhieu.IsReadOnly = false;
                
                schema.Columns.Add(colvarLoaiPhieu);
                
                TableSchema.TableColumn colvarNoiTru = new TableSchema.TableColumn(schema);
                colvarNoiTru.ColumnName = "noi_tru";
                colvarNoiTru.DataType = DbType.Byte;
                colvarNoiTru.MaxLength = 0;
                colvarNoiTru.AutoIncrement = false;
                colvarNoiTru.IsNullable = true;
                colvarNoiTru.IsPrimaryKey = false;
                colvarNoiTru.IsForeignKey = false;
                colvarNoiTru.IsReadOnly = false;
                
                schema.Columns.Add(colvarNoiTru);
                
                TableSchema.TableColumn colvarIdKhoxuat = new TableSchema.TableColumn(schema);
                colvarIdKhoxuat.ColumnName = "id_khoxuat";
                colvarIdKhoxuat.DataType = DbType.Int16;
                colvarIdKhoxuat.MaxLength = 0;
                colvarIdKhoxuat.AutoIncrement = false;
                colvarIdKhoxuat.IsNullable = true;
                colvarIdKhoxuat.IsPrimaryKey = false;
                colvarIdKhoxuat.IsForeignKey = false;
                colvarIdKhoxuat.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdKhoxuat);
                
                TableSchema.TableColumn colvarTenLoaiphieu = new TableSchema.TableColumn(schema);
                colvarTenLoaiphieu.ColumnName = "ten_loaiphieu";
                colvarTenLoaiphieu.DataType = DbType.String;
                colvarTenLoaiphieu.MaxLength = 50;
                colvarTenLoaiphieu.AutoIncrement = false;
                colvarTenLoaiphieu.IsNullable = true;
                colvarTenLoaiphieu.IsPrimaryKey = false;
                colvarTenLoaiphieu.IsForeignKey = false;
                colvarTenLoaiphieu.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenLoaiphieu);
                
                TableSchema.TableColumn colvarIdKhoalinh = new TableSchema.TableColumn(schema);
                colvarIdKhoalinh.ColumnName = "id_khoalinh";
                colvarIdKhoalinh.DataType = DbType.Int16;
                colvarIdKhoalinh.MaxLength = 0;
                colvarIdKhoalinh.AutoIncrement = false;
                colvarIdKhoalinh.IsNullable = true;
                colvarIdKhoalinh.IsPrimaryKey = false;
                colvarIdKhoalinh.IsForeignKey = false;
                colvarIdKhoalinh.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdKhoalinh);
                
                TableSchema.TableColumn colvarSoChungtuKemtheo = new TableSchema.TableColumn(schema);
                colvarSoChungtuKemtheo.ColumnName = "so_chungtu_kemtheo";
                colvarSoChungtuKemtheo.DataType = DbType.String;
                colvarSoChungtuKemtheo.MaxLength = 30;
                colvarSoChungtuKemtheo.AutoIncrement = false;
                colvarSoChungtuKemtheo.IsNullable = true;
                colvarSoChungtuKemtheo.IsPrimaryKey = false;
                colvarSoChungtuKemtheo.IsForeignKey = false;
                colvarSoChungtuKemtheo.IsReadOnly = false;
                
                schema.Columns.Add(colvarSoChungtuKemtheo);
                
                TableSchema.TableColumn colvarTenNhacungcap = new TableSchema.TableColumn(schema);
                colvarTenNhacungcap.ColumnName = "Ten_nhacungcap";
                colvarTenNhacungcap.DataType = DbType.String;
                colvarTenNhacungcap.MaxLength = 255;
                colvarTenNhacungcap.AutoIncrement = false;
                colvarTenNhacungcap.IsNullable = false;
                colvarTenNhacungcap.IsPrimaryKey = false;
                colvarTenNhacungcap.IsForeignKey = false;
                colvarTenNhacungcap.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenNhacungcap);
                
                TableSchema.TableColumn colvarTenKhoa = new TableSchema.TableColumn(schema);
                colvarTenKhoa.ColumnName = "TEN_KHOA";
                colvarTenKhoa.DataType = DbType.String;
                colvarTenKhoa.MaxLength = 100;
                colvarTenKhoa.AutoIncrement = false;
                colvarTenKhoa.IsNullable = true;
                colvarTenKhoa.IsPrimaryKey = false;
                colvarTenKhoa.IsForeignKey = false;
                colvarTenKhoa.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenKhoa);
                
                TableSchema.TableColumn colvarTenNhanvien = new TableSchema.TableColumn(schema);
                colvarTenNhanvien.ColumnName = "ten_nhanvien";
                colvarTenNhanvien.DataType = DbType.String;
                colvarTenNhanvien.MaxLength = 100;
                colvarTenNhanvien.AutoIncrement = false;
                colvarTenNhanvien.IsNullable = true;
                colvarTenNhanvien.IsPrimaryKey = false;
                colvarTenNhanvien.IsForeignKey = false;
                colvarTenNhanvien.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenNhanvien);
                
                TableSchema.TableColumn colvarTenKhonhap = new TableSchema.TableColumn(schema);
                colvarTenKhonhap.ColumnName = "ten_khonhap";
                colvarTenKhonhap.DataType = DbType.String;
                colvarTenKhonhap.MaxLength = 50;
                colvarTenKhonhap.AutoIncrement = false;
                colvarTenKhonhap.IsNullable = true;
                colvarTenKhonhap.IsPrimaryKey = false;
                colvarTenKhonhap.IsForeignKey = false;
                colvarTenKhonhap.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenKhonhap);
                
                TableSchema.TableColumn colvarTenKhoxuat = new TableSchema.TableColumn(schema);
                colvarTenKhoxuat.ColumnName = "ten_khoxuat";
                colvarTenKhoxuat.DataType = DbType.String;
                colvarTenKhoxuat.MaxLength = 50;
                colvarTenKhoxuat.AutoIncrement = false;
                colvarTenKhoxuat.IsNullable = true;
                colvarTenKhoxuat.IsPrimaryKey = false;
                colvarTenKhoxuat.IsForeignKey = false;
                colvarTenKhoxuat.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenKhoxuat);
                
                TableSchema.TableColumn colvarTenNguoigiao = new TableSchema.TableColumn(schema);
                colvarTenNguoigiao.ColumnName = "ten_nguoigiao";
                colvarTenNguoigiao.DataType = DbType.String;
                colvarTenNguoigiao.MaxLength = 255;
                colvarTenNguoigiao.AutoIncrement = false;
                colvarTenNguoigiao.IsNullable = true;
                colvarTenNguoigiao.IsPrimaryKey = false;
                colvarTenNguoigiao.IsForeignKey = false;
                colvarTenNguoigiao.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenNguoigiao);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("v_thuoc_phieunhapxuat",schema);
            }
        }
        #endregion
        
        #region Query Accessor
	    public static Query CreateQuery()
	    {
		    return new Query(Schema);
	    }
	    #endregion
	    
	    #region .ctors
	    public VThuocPhieunhapxuat()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public VThuocPhieunhapxuat(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public VThuocPhieunhapxuat(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public VThuocPhieunhapxuat(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("IdPhieu")]
        [Bindable(true)]
        public long IdPhieu 
	    {
		    get
		    {
			    return GetColumnValue<long>("id_phieu");
		    }
            set 
		    {
			    SetColumnValue("id_phieu", value);
            }
        }
	      
        [XmlAttribute("KieuThuocvattu")]
        [Bindable(true)]
        public string KieuThuocvattu 
	    {
		    get
		    {
			    return GetColumnValue<string>("kieu_thuocvattu");
		    }
            set 
		    {
			    SetColumnValue("kieu_thuocvattu", value);
            }
        }
	      
        [XmlAttribute("MaPhieu")]
        [Bindable(true)]
        public string MaPhieu 
	    {
		    get
		    {
			    return GetColumnValue<string>("ma_phieu");
		    }
            set 
		    {
			    SetColumnValue("ma_phieu", value);
            }
        }
	      
        [XmlAttribute("SoHoadon")]
        [Bindable(true)]
        public string SoHoadon 
	    {
		    get
		    {
			    return GetColumnValue<string>("so_hoadon");
		    }
            set 
		    {
			    SetColumnValue("so_hoadon", value);
            }
        }
	      
        [XmlAttribute("NgayLap")]
        [Bindable(true)]
        public DateTime? NgayLap 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("ngay_lap");
		    }
            set 
		    {
			    SetColumnValue("ngay_lap", value);
            }
        }
	      
        [XmlAttribute("NgayHoadon")]
        [Bindable(true)]
        public DateTime NgayHoadon 
	    {
		    get
		    {
			    return GetColumnValue<DateTime>("ngay_hoadon");
		    }
            set 
		    {
			    SetColumnValue("ngay_hoadon", value);
            }
        }
	      
        [XmlAttribute("IdKhonhap")]
        [Bindable(true)]
        public short? IdKhonhap 
	    {
		    get
		    {
			    return GetColumnValue<short?>("id_khonhap");
		    }
            set 
		    {
			    SetColumnValue("id_khonhap", value);
            }
        }
	      
        [XmlAttribute("Vat")]
        [Bindable(true)]
        public decimal? Vat 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("VAT");
		    }
            set 
		    {
			    SetColumnValue("VAT", value);
            }
        }
	      
        [XmlAttribute("MaNhacungcap")]
        [Bindable(true)]
        public string MaNhacungcap 
	    {
		    get
		    {
			    return GetColumnValue<string>("MA_NHACUNGCAP");
		    }
            set 
		    {
			    SetColumnValue("MA_NHACUNGCAP", value);
            }
        }
	      
        [XmlAttribute("MotaThem")]
        [Bindable(true)]
        public string MotaThem 
	    {
		    get
		    {
			    return GetColumnValue<string>("mota_them");
		    }
            set 
		    {
			    SetColumnValue("mota_them", value);
            }
        }
	      
        [XmlAttribute("NguoiGiao")]
        [Bindable(true)]
        public string NguoiGiao 
	    {
		    get
		    {
			    return GetColumnValue<string>("NGUOI_GIAO");
		    }
            set 
		    {
			    SetColumnValue("NGUOI_GIAO", value);
            }
        }
	      
        [XmlAttribute("IdNhanvien")]
        [Bindable(true)]
        public short? IdNhanvien 
	    {
		    get
		    {
			    return GetColumnValue<short?>("id_nhanvien");
		    }
            set 
		    {
			    SetColumnValue("id_nhanvien", value);
            }
        }
	      
        [XmlAttribute("NgayTao")]
        [Bindable(true)]
        public DateTime? NgayTao 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("NGAY_TAO");
		    }
            set 
		    {
			    SetColumnValue("NGAY_TAO", value);
            }
        }
	      
        [XmlAttribute("NguoiTao")]
        [Bindable(true)]
        public string NguoiTao 
	    {
		    get
		    {
			    return GetColumnValue<string>("NGUOI_TAO");
		    }
            set 
		    {
			    SetColumnValue("NGUOI_TAO", value);
            }
        }
	      
        [XmlAttribute("NguoiSua")]
        [Bindable(true)]
        public string NguoiSua 
	    {
		    get
		    {
			    return GetColumnValue<string>("NGUOI_SUA");
		    }
            set 
		    {
			    SetColumnValue("NGUOI_SUA", value);
            }
        }
	      
        [XmlAttribute("NgaySua")]
        [Bindable(true)]
        public DateTime? NgaySua 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("NGAY_SUA");
		    }
            set 
		    {
			    SetColumnValue("NGAY_SUA", value);
            }
        }
	      
        [XmlAttribute("TrangThai")]
        [Bindable(true)]
        public byte? TrangThai 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("TRANG_THAI");
		    }
            set 
		    {
			    SetColumnValue("TRANG_THAI", value);
            }
        }
	      
        [XmlAttribute("NgayXacnhan")]
        [Bindable(true)]
        public DateTime? NgayXacnhan 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("ngay_xacnhan");
		    }
            set 
		    {
			    SetColumnValue("ngay_xacnhan", value);
            }
        }
	      
        [XmlAttribute("NguoiXacnhan")]
        [Bindable(true)]
        public string NguoiXacnhan 
	    {
		    get
		    {
			    return GetColumnValue<string>("nguoi_xacnhan");
		    }
            set 
		    {
			    SetColumnValue("nguoi_xacnhan", value);
            }
        }
	      
        [XmlAttribute("IdNhanvienXacnhan")]
        [Bindable(true)]
        public short? IdNhanvienXacnhan 
	    {
		    get
		    {
			    return GetColumnValue<short?>("id_nhanvien_xacnhan");
		    }
            set 
		    {
			    SetColumnValue("id_nhanvien_xacnhan", value);
            }
        }
	      
        [XmlAttribute("LoaiPhieu")]
        [Bindable(true)]
        public byte? LoaiPhieu 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("loai_phieu");
		    }
            set 
		    {
			    SetColumnValue("loai_phieu", value);
            }
        }
	      
        [XmlAttribute("NoiTru")]
        [Bindable(true)]
        public byte? NoiTru 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("noi_tru");
		    }
            set 
		    {
			    SetColumnValue("noi_tru", value);
            }
        }
	      
        [XmlAttribute("IdKhoxuat")]
        [Bindable(true)]
        public short? IdKhoxuat 
	    {
		    get
		    {
			    return GetColumnValue<short?>("id_khoxuat");
		    }
            set 
		    {
			    SetColumnValue("id_khoxuat", value);
            }
        }
	      
        [XmlAttribute("TenLoaiphieu")]
        [Bindable(true)]
        public string TenLoaiphieu 
	    {
		    get
		    {
			    return GetColumnValue<string>("ten_loaiphieu");
		    }
            set 
		    {
			    SetColumnValue("ten_loaiphieu", value);
            }
        }
	      
        [XmlAttribute("IdKhoalinh")]
        [Bindable(true)]
        public short? IdKhoalinh 
	    {
		    get
		    {
			    return GetColumnValue<short?>("id_khoalinh");
		    }
            set 
		    {
			    SetColumnValue("id_khoalinh", value);
            }
        }
	      
        [XmlAttribute("SoChungtuKemtheo")]
        [Bindable(true)]
        public string SoChungtuKemtheo 
	    {
		    get
		    {
			    return GetColumnValue<string>("so_chungtu_kemtheo");
		    }
            set 
		    {
			    SetColumnValue("so_chungtu_kemtheo", value);
            }
        }
	      
        [XmlAttribute("TenNhacungcap")]
        [Bindable(true)]
        public string TenNhacungcap 
	    {
		    get
		    {
			    return GetColumnValue<string>("Ten_nhacungcap");
		    }
            set 
		    {
			    SetColumnValue("Ten_nhacungcap", value);
            }
        }
	      
        [XmlAttribute("TenKhoa")]
        [Bindable(true)]
        public string TenKhoa 
	    {
		    get
		    {
			    return GetColumnValue<string>("TEN_KHOA");
		    }
            set 
		    {
			    SetColumnValue("TEN_KHOA", value);
            }
        }
	      
        [XmlAttribute("TenNhanvien")]
        [Bindable(true)]
        public string TenNhanvien 
	    {
		    get
		    {
			    return GetColumnValue<string>("ten_nhanvien");
		    }
            set 
		    {
			    SetColumnValue("ten_nhanvien", value);
            }
        }
	      
        [XmlAttribute("TenKhonhap")]
        [Bindable(true)]
        public string TenKhonhap 
	    {
		    get
		    {
			    return GetColumnValue<string>("ten_khonhap");
		    }
            set 
		    {
			    SetColumnValue("ten_khonhap", value);
            }
        }
	      
        [XmlAttribute("TenKhoxuat")]
        [Bindable(true)]
        public string TenKhoxuat 
	    {
		    get
		    {
			    return GetColumnValue<string>("ten_khoxuat");
		    }
            set 
		    {
			    SetColumnValue("ten_khoxuat", value);
            }
        }
	      
        [XmlAttribute("TenNguoigiao")]
        [Bindable(true)]
        public string TenNguoigiao 
	    {
		    get
		    {
			    return GetColumnValue<string>("ten_nguoigiao");
		    }
            set 
		    {
			    SetColumnValue("ten_nguoigiao", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string IdPhieu = @"id_phieu";
            
            public static string KieuThuocvattu = @"kieu_thuocvattu";
            
            public static string MaPhieu = @"ma_phieu";
            
            public static string SoHoadon = @"so_hoadon";
            
            public static string NgayLap = @"ngay_lap";
            
            public static string NgayHoadon = @"ngay_hoadon";
            
            public static string IdKhonhap = @"id_khonhap";
            
            public static string Vat = @"VAT";
            
            public static string MaNhacungcap = @"MA_NHACUNGCAP";
            
            public static string MotaThem = @"mota_them";
            
            public static string NguoiGiao = @"NGUOI_GIAO";
            
            public static string IdNhanvien = @"id_nhanvien";
            
            public static string NgayTao = @"NGAY_TAO";
            
            public static string NguoiTao = @"NGUOI_TAO";
            
            public static string NguoiSua = @"NGUOI_SUA";
            
            public static string NgaySua = @"NGAY_SUA";
            
            public static string TrangThai = @"TRANG_THAI";
            
            public static string NgayXacnhan = @"ngay_xacnhan";
            
            public static string NguoiXacnhan = @"nguoi_xacnhan";
            
            public static string IdNhanvienXacnhan = @"id_nhanvien_xacnhan";
            
            public static string LoaiPhieu = @"loai_phieu";
            
            public static string NoiTru = @"noi_tru";
            
            public static string IdKhoxuat = @"id_khoxuat";
            
            public static string TenLoaiphieu = @"ten_loaiphieu";
            
            public static string IdKhoalinh = @"id_khoalinh";
            
            public static string SoChungtuKemtheo = @"so_chungtu_kemtheo";
            
            public static string TenNhacungcap = @"Ten_nhacungcap";
            
            public static string TenKhoa = @"TEN_KHOA";
            
            public static string TenNhanvien = @"ten_nhanvien";
            
            public static string TenKhonhap = @"ten_khonhap";
            
            public static string TenKhoxuat = @"ten_khoxuat";
            
            public static string TenNguoigiao = @"ten_nguoigiao";
            
	    }
	    #endregion
	    
	    
	    #region IAbstractRecord Members
        public new CT GetColumnValue<CT>(string columnName) {
            return base.GetColumnValue<CT>(columnName);
        }
        public object GetColumnValue(string columnName) {
            return base.GetColumnValue<object>(columnName);
        }
        #endregion
	    
    }
}
