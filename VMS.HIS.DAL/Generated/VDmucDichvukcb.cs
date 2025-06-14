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
    /// Strongly-typed collection for the VDmucDichvukcb class.
    /// </summary>
    [Serializable]
    public partial class VDmucDichvukcbCollection : ReadOnlyList<VDmucDichvukcb, VDmucDichvukcbCollection>
    {        
        public VDmucDichvukcbCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the v_dmuc_dichvukcb view.
    /// </summary>
    [Serializable]
    public partial class VDmucDichvukcb : ReadOnlyRecord<VDmucDichvukcb>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("v_dmuc_dichvukcb", TableType.View, DataService.GetInstance("ORM"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarIdDichvukcb = new TableSchema.TableColumn(schema);
                colvarIdDichvukcb.ColumnName = "id_dichvukcb";
                colvarIdDichvukcb.DataType = DbType.Int32;
                colvarIdDichvukcb.MaxLength = 0;
                colvarIdDichvukcb.AutoIncrement = false;
                colvarIdDichvukcb.IsNullable = false;
                colvarIdDichvukcb.IsPrimaryKey = false;
                colvarIdDichvukcb.IsForeignKey = false;
                colvarIdDichvukcb.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdDichvukcb);
                
                TableSchema.TableColumn colvarMaDichvukcb = new TableSchema.TableColumn(schema);
                colvarMaDichvukcb.ColumnName = "ma_dichvukcb";
                colvarMaDichvukcb.DataType = DbType.String;
                colvarMaDichvukcb.MaxLength = 50;
                colvarMaDichvukcb.AutoIncrement = false;
                colvarMaDichvukcb.IsNullable = true;
                colvarMaDichvukcb.IsPrimaryKey = false;
                colvarMaDichvukcb.IsForeignKey = false;
                colvarMaDichvukcb.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaDichvukcb);
                
                TableSchema.TableColumn colvarMaBhyt = new TableSchema.TableColumn(schema);
                colvarMaBhyt.ColumnName = "ma_bhyt";
                colvarMaBhyt.DataType = DbType.String;
                colvarMaBhyt.MaxLength = 50;
                colvarMaBhyt.AutoIncrement = false;
                colvarMaBhyt.IsNullable = true;
                colvarMaBhyt.IsPrimaryKey = false;
                colvarMaBhyt.IsForeignKey = false;
                colvarMaBhyt.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaBhyt);
                
                TableSchema.TableColumn colvarTenDichvukcb = new TableSchema.TableColumn(schema);
                colvarTenDichvukcb.ColumnName = "ten_dichvukcb";
                colvarTenDichvukcb.DataType = DbType.String;
                colvarTenDichvukcb.MaxLength = 100;
                colvarTenDichvukcb.AutoIncrement = false;
                colvarTenDichvukcb.IsNullable = true;
                colvarTenDichvukcb.IsPrimaryKey = false;
                colvarTenDichvukcb.IsForeignKey = false;
                colvarTenDichvukcb.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenDichvukcb);
                
                TableSchema.TableColumn colvarIdKieukham = new TableSchema.TableColumn(schema);
                colvarIdKieukham.ColumnName = "id_kieukham";
                colvarIdKieukham.DataType = DbType.Int16;
                colvarIdKieukham.MaxLength = 0;
                colvarIdKieukham.AutoIncrement = false;
                colvarIdKieukham.IsNullable = false;
                colvarIdKieukham.IsPrimaryKey = false;
                colvarIdKieukham.IsForeignKey = false;
                colvarIdKieukham.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdKieukham);
                
                TableSchema.TableColumn colvarIdKhoaphong = new TableSchema.TableColumn(schema);
                colvarIdKhoaphong.ColumnName = "id_khoaphong";
                colvarIdKhoaphong.DataType = DbType.Int16;
                colvarIdKhoaphong.MaxLength = 0;
                colvarIdKhoaphong.AutoIncrement = false;
                colvarIdKhoaphong.IsNullable = false;
                colvarIdKhoaphong.IsPrimaryKey = false;
                colvarIdKhoaphong.IsForeignKey = false;
                colvarIdKhoaphong.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdKhoaphong);
                
                TableSchema.TableColumn colvarIdBacsy = new TableSchema.TableColumn(schema);
                colvarIdBacsy.ColumnName = "id_bacsy";
                colvarIdBacsy.DataType = DbType.Int16;
                colvarIdBacsy.MaxLength = 0;
                colvarIdBacsy.AutoIncrement = false;
                colvarIdBacsy.IsNullable = false;
                colvarIdBacsy.IsPrimaryKey = false;
                colvarIdBacsy.IsForeignKey = false;
                colvarIdBacsy.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdBacsy);
                
                TableSchema.TableColumn colvarIdDoituongKcb = new TableSchema.TableColumn(schema);
                colvarIdDoituongKcb.ColumnName = "id_doituong_kcb";
                colvarIdDoituongKcb.DataType = DbType.Int16;
                colvarIdDoituongKcb.MaxLength = 0;
                colvarIdDoituongKcb.AutoIncrement = false;
                colvarIdDoituongKcb.IsNullable = false;
                colvarIdDoituongKcb.IsPrimaryKey = false;
                colvarIdDoituongKcb.IsForeignKey = false;
                colvarIdDoituongKcb.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdDoituongKcb);
                
                TableSchema.TableColumn colvarIdPhongkham = new TableSchema.TableColumn(schema);
                colvarIdPhongkham.ColumnName = "id_phongkham";
                colvarIdPhongkham.DataType = DbType.Int16;
                colvarIdPhongkham.MaxLength = 0;
                colvarIdPhongkham.AutoIncrement = false;
                colvarIdPhongkham.IsNullable = false;
                colvarIdPhongkham.IsPrimaryKey = false;
                colvarIdPhongkham.IsForeignKey = false;
                colvarIdPhongkham.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdPhongkham);
                
                TableSchema.TableColumn colvarDonGia = new TableSchema.TableColumn(schema);
                colvarDonGia.ColumnName = "don_gia";
                colvarDonGia.DataType = DbType.Decimal;
                colvarDonGia.MaxLength = 0;
                colvarDonGia.AutoIncrement = false;
                colvarDonGia.IsNullable = false;
                colvarDonGia.IsPrimaryKey = false;
                colvarDonGia.IsForeignKey = false;
                colvarDonGia.IsReadOnly = false;
                
                schema.Columns.Add(colvarDonGia);
                
                TableSchema.TableColumn colvarPhuthuDungtuyen = new TableSchema.TableColumn(schema);
                colvarPhuthuDungtuyen.ColumnName = "phuthu_dungtuyen";
                colvarPhuthuDungtuyen.DataType = DbType.Decimal;
                colvarPhuthuDungtuyen.MaxLength = 0;
                colvarPhuthuDungtuyen.AutoIncrement = false;
                colvarPhuthuDungtuyen.IsNullable = true;
                colvarPhuthuDungtuyen.IsPrimaryKey = false;
                colvarPhuthuDungtuyen.IsForeignKey = false;
                colvarPhuthuDungtuyen.IsReadOnly = false;
                
                schema.Columns.Add(colvarPhuthuDungtuyen);
                
                TableSchema.TableColumn colvarPhuthuTraituyen = new TableSchema.TableColumn(schema);
                colvarPhuthuTraituyen.ColumnName = "phuthu_traituyen";
                colvarPhuthuTraituyen.DataType = DbType.Decimal;
                colvarPhuthuTraituyen.MaxLength = 0;
                colvarPhuthuTraituyen.AutoIncrement = false;
                colvarPhuthuTraituyen.IsNullable = true;
                colvarPhuthuTraituyen.IsPrimaryKey = false;
                colvarPhuthuTraituyen.IsForeignKey = false;
                colvarPhuthuTraituyen.IsReadOnly = false;
                
                schema.Columns.Add(colvarPhuthuTraituyen);
                
                TableSchema.TableColumn colvarDongiaNgoaigio = new TableSchema.TableColumn(schema);
                colvarDongiaNgoaigio.ColumnName = "dongia_ngoaigio";
                colvarDongiaNgoaigio.DataType = DbType.Decimal;
                colvarDongiaNgoaigio.MaxLength = 0;
                colvarDongiaNgoaigio.AutoIncrement = false;
                colvarDongiaNgoaigio.IsNullable = true;
                colvarDongiaNgoaigio.IsPrimaryKey = false;
                colvarDongiaNgoaigio.IsForeignKey = false;
                colvarDongiaNgoaigio.IsReadOnly = false;
                
                schema.Columns.Add(colvarDongiaNgoaigio);
                
                TableSchema.TableColumn colvarPhuthuNgoaigio = new TableSchema.TableColumn(schema);
                colvarPhuthuNgoaigio.ColumnName = "phuthu_ngoaigio";
                colvarPhuthuNgoaigio.DataType = DbType.Decimal;
                colvarPhuthuNgoaigio.MaxLength = 0;
                colvarPhuthuNgoaigio.AutoIncrement = false;
                colvarPhuthuNgoaigio.IsNullable = true;
                colvarPhuthuNgoaigio.IsPrimaryKey = false;
                colvarPhuthuNgoaigio.IsForeignKey = false;
                colvarPhuthuNgoaigio.IsReadOnly = false;
                
                schema.Columns.Add(colvarPhuthuNgoaigio);
                
                TableSchema.TableColumn colvarMaDoituongKcb = new TableSchema.TableColumn(schema);
                colvarMaDoituongKcb.ColumnName = "ma_doituong_kcb";
                colvarMaDoituongKcb.DataType = DbType.String;
                colvarMaDoituongKcb.MaxLength = 50;
                colvarMaDoituongKcb.AutoIncrement = false;
                colvarMaDoituongKcb.IsNullable = true;
                colvarMaDoituongKcb.IsPrimaryKey = false;
                colvarMaDoituongKcb.IsForeignKey = false;
                colvarMaDoituongKcb.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaDoituongKcb);
                
                TableSchema.TableColumn colvarIdPhikemtheo = new TableSchema.TableColumn(schema);
                colvarIdPhikemtheo.ColumnName = "id_phikemtheo";
                colvarIdPhikemtheo.DataType = DbType.Int32;
                colvarIdPhikemtheo.MaxLength = 0;
                colvarIdPhikemtheo.AutoIncrement = false;
                colvarIdPhikemtheo.IsNullable = true;
                colvarIdPhikemtheo.IsPrimaryKey = false;
                colvarIdPhikemtheo.IsForeignKey = false;
                colvarIdPhikemtheo.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdPhikemtheo);
                
                TableSchema.TableColumn colvarIdPhikemtheongoaigio = new TableSchema.TableColumn(schema);
                colvarIdPhikemtheongoaigio.ColumnName = "id_phikemtheongoaigio";
                colvarIdPhikemtheongoaigio.DataType = DbType.Int32;
                colvarIdPhikemtheongoaigio.MaxLength = 0;
                colvarIdPhikemtheongoaigio.AutoIncrement = false;
                colvarIdPhikemtheongoaigio.IsNullable = true;
                colvarIdPhikemtheongoaigio.IsPrimaryKey = false;
                colvarIdPhikemtheongoaigio.IsForeignKey = false;
                colvarIdPhikemtheongoaigio.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdPhikemtheongoaigio);
                
                TableSchema.TableColumn colvarNhomBaocao = new TableSchema.TableColumn(schema);
                colvarNhomBaocao.ColumnName = "nhom_baocao";
                colvarNhomBaocao.DataType = DbType.String;
                colvarNhomBaocao.MaxLength = 20;
                colvarNhomBaocao.AutoIncrement = false;
                colvarNhomBaocao.IsNullable = true;
                colvarNhomBaocao.IsPrimaryKey = false;
                colvarNhomBaocao.IsForeignKey = false;
                colvarNhomBaocao.IsReadOnly = false;
                
                schema.Columns.Add(colvarNhomBaocao);
                
                TableSchema.TableColumn colvarTuTuc = new TableSchema.TableColumn(schema);
                colvarTuTuc.ColumnName = "tu_tuc";
                colvarTuTuc.DataType = DbType.Byte;
                colvarTuTuc.MaxLength = 0;
                colvarTuTuc.AutoIncrement = false;
                colvarTuTuc.IsNullable = true;
                colvarTuTuc.IsPrimaryKey = false;
                colvarTuTuc.IsForeignKey = false;
                colvarTuTuc.IsReadOnly = false;
                
                schema.Columns.Add(colvarTuTuc);
                
                TableSchema.TableColumn colvarSttHthi = new TableSchema.TableColumn(schema);
                colvarSttHthi.ColumnName = "stt_hthi";
                colvarSttHthi.DataType = DbType.Int16;
                colvarSttHthi.MaxLength = 0;
                colvarSttHthi.AutoIncrement = false;
                colvarSttHthi.IsNullable = true;
                colvarSttHthi.IsPrimaryKey = false;
                colvarSttHthi.IsForeignKey = false;
                colvarSttHthi.IsReadOnly = false;
                
                schema.Columns.Add(colvarSttHthi);
                
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
                
                TableSchema.TableColumn colvarMaGia = new TableSchema.TableColumn(schema);
                colvarMaGia.ColumnName = "ma_gia";
                colvarMaGia.DataType = DbType.String;
                colvarMaGia.MaxLength = 50;
                colvarMaGia.AutoIncrement = false;
                colvarMaGia.IsNullable = true;
                colvarMaGia.IsPrimaryKey = false;
                colvarMaGia.IsForeignKey = false;
                colvarMaGia.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaGia);
                
                TableSchema.TableColumn colvarHoatDong = new TableSchema.TableColumn(schema);
                colvarHoatDong.ColumnName = "hoat_dong";
                colvarHoatDong.DataType = DbType.Boolean;
                colvarHoatDong.MaxLength = 0;
                colvarHoatDong.AutoIncrement = false;
                colvarHoatDong.IsNullable = true;
                colvarHoatDong.IsPrimaryKey = false;
                colvarHoatDong.IsForeignKey = false;
                colvarHoatDong.IsReadOnly = false;
                
                schema.Columns.Add(colvarHoatDong);
                
                TableSchema.TableColumn colvarIdLoaidoituongKcb = new TableSchema.TableColumn(schema);
                colvarIdLoaidoituongKcb.ColumnName = "id_loaidoituong_kcb";
                colvarIdLoaidoituongKcb.DataType = DbType.Int16;
                colvarIdLoaidoituongKcb.MaxLength = 0;
                colvarIdLoaidoituongKcb.AutoIncrement = false;
                colvarIdLoaidoituongKcb.IsNullable = true;
                colvarIdLoaidoituongKcb.IsPrimaryKey = false;
                colvarIdLoaidoituongKcb.IsForeignKey = false;
                colvarIdLoaidoituongKcb.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdLoaidoituongKcb);
                
                TableSchema.TableColumn colvarKhamThiluc = new TableSchema.TableColumn(schema);
                colvarKhamThiluc.ColumnName = "kham_thiluc";
                colvarKhamThiluc.DataType = DbType.Byte;
                colvarKhamThiluc.MaxLength = 0;
                colvarKhamThiluc.AutoIncrement = false;
                colvarKhamThiluc.IsNullable = true;
                colvarKhamThiluc.IsPrimaryKey = false;
                colvarKhamThiluc.IsForeignKey = false;
                colvarKhamThiluc.IsReadOnly = false;
                
                schema.Columns.Add(colvarKhamThiluc);
                
                TableSchema.TableColumn colvarCapKinh = new TableSchema.TableColumn(schema);
                colvarCapKinh.ColumnName = "cap_kinh";
                colvarCapKinh.DataType = DbType.Boolean;
                colvarCapKinh.MaxLength = 0;
                colvarCapKinh.AutoIncrement = false;
                colvarCapKinh.IsNullable = true;
                colvarCapKinh.IsPrimaryKey = false;
                colvarCapKinh.IsForeignKey = false;
                colvarCapKinh.IsReadOnly = false;
                
                schema.Columns.Add(colvarCapKinh);
                
                TableSchema.TableColumn colvarTenBacsi = new TableSchema.TableColumn(schema);
                colvarTenBacsi.ColumnName = "ten_bacsi";
                colvarTenBacsi.DataType = DbType.String;
                colvarTenBacsi.MaxLength = 100;
                colvarTenBacsi.AutoIncrement = false;
                colvarTenBacsi.IsNullable = false;
                colvarTenBacsi.IsPrimaryKey = false;
                colvarTenBacsi.IsForeignKey = false;
                colvarTenBacsi.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenBacsi);
                
                TableSchema.TableColumn colvarTenKhoa = new TableSchema.TableColumn(schema);
                colvarTenKhoa.ColumnName = "ten_khoa";
                colvarTenKhoa.DataType = DbType.String;
                colvarTenKhoa.MaxLength = 100;
                colvarTenKhoa.AutoIncrement = false;
                colvarTenKhoa.IsNullable = false;
                colvarTenKhoa.IsPrimaryKey = false;
                colvarTenKhoa.IsForeignKey = false;
                colvarTenKhoa.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenKhoa);
                
                TableSchema.TableColumn colvarTenPhong = new TableSchema.TableColumn(schema);
                colvarTenPhong.ColumnName = "ten_phong";
                colvarTenPhong.DataType = DbType.String;
                colvarTenPhong.MaxLength = 100;
                colvarTenPhong.AutoIncrement = false;
                colvarTenPhong.IsNullable = false;
                colvarTenPhong.IsPrimaryKey = false;
                colvarTenPhong.IsForeignKey = false;
                colvarTenPhong.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenPhong);
                
                TableSchema.TableColumn colvarTenKieukham = new TableSchema.TableColumn(schema);
                colvarTenKieukham.ColumnName = "ten_kieukham";
                colvarTenKieukham.DataType = DbType.String;
                colvarTenKieukham.MaxLength = 100;
                colvarTenKieukham.AutoIncrement = false;
                colvarTenKieukham.IsNullable = false;
                colvarTenKieukham.IsPrimaryKey = false;
                colvarTenKieukham.IsForeignKey = false;
                colvarTenKieukham.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenKieukham);
                
                TableSchema.TableColumn colvarTenDoituongKcb = new TableSchema.TableColumn(schema);
                colvarTenDoituongKcb.ColumnName = "ten_doituong_kcb";
                colvarTenDoituongKcb.DataType = DbType.String;
                colvarTenDoituongKcb.MaxLength = 100;
                colvarTenDoituongKcb.AutoIncrement = false;
                colvarTenDoituongKcb.IsNullable = false;
                colvarTenDoituongKcb.IsPrimaryKey = false;
                colvarTenDoituongKcb.IsForeignKey = false;
                colvarTenDoituongKcb.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenDoituongKcb);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("v_dmuc_dichvukcb",schema);
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
	    public VDmucDichvukcb()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public VDmucDichvukcb(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public VDmucDichvukcb(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public VDmucDichvukcb(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("IdDichvukcb")]
        [Bindable(true)]
        public int IdDichvukcb 
	    {
		    get
		    {
			    return GetColumnValue<int>("id_dichvukcb");
		    }
            set 
		    {
			    SetColumnValue("id_dichvukcb", value);
            }
        }
	      
        [XmlAttribute("MaDichvukcb")]
        [Bindable(true)]
        public string MaDichvukcb 
	    {
		    get
		    {
			    return GetColumnValue<string>("ma_dichvukcb");
		    }
            set 
		    {
			    SetColumnValue("ma_dichvukcb", value);
            }
        }
	      
        [XmlAttribute("MaBhyt")]
        [Bindable(true)]
        public string MaBhyt 
	    {
		    get
		    {
			    return GetColumnValue<string>("ma_bhyt");
		    }
            set 
		    {
			    SetColumnValue("ma_bhyt", value);
            }
        }
	      
        [XmlAttribute("TenDichvukcb")]
        [Bindable(true)]
        public string TenDichvukcb 
	    {
		    get
		    {
			    return GetColumnValue<string>("ten_dichvukcb");
		    }
            set 
		    {
			    SetColumnValue("ten_dichvukcb", value);
            }
        }
	      
        [XmlAttribute("IdKieukham")]
        [Bindable(true)]
        public short IdKieukham 
	    {
		    get
		    {
			    return GetColumnValue<short>("id_kieukham");
		    }
            set 
		    {
			    SetColumnValue("id_kieukham", value);
            }
        }
	      
        [XmlAttribute("IdKhoaphong")]
        [Bindable(true)]
        public short IdKhoaphong 
	    {
		    get
		    {
			    return GetColumnValue<short>("id_khoaphong");
		    }
            set 
		    {
			    SetColumnValue("id_khoaphong", value);
            }
        }
	      
        [XmlAttribute("IdBacsy")]
        [Bindable(true)]
        public short IdBacsy 
	    {
		    get
		    {
			    return GetColumnValue<short>("id_bacsy");
		    }
            set 
		    {
			    SetColumnValue("id_bacsy", value);
            }
        }
	      
        [XmlAttribute("IdDoituongKcb")]
        [Bindable(true)]
        public short IdDoituongKcb 
	    {
		    get
		    {
			    return GetColumnValue<short>("id_doituong_kcb");
		    }
            set 
		    {
			    SetColumnValue("id_doituong_kcb", value);
            }
        }
	      
        [XmlAttribute("IdPhongkham")]
        [Bindable(true)]
        public short IdPhongkham 
	    {
		    get
		    {
			    return GetColumnValue<short>("id_phongkham");
		    }
            set 
		    {
			    SetColumnValue("id_phongkham", value);
            }
        }
	      
        [XmlAttribute("DonGia")]
        [Bindable(true)]
        public decimal DonGia 
	    {
		    get
		    {
			    return GetColumnValue<decimal>("don_gia");
		    }
            set 
		    {
			    SetColumnValue("don_gia", value);
            }
        }
	      
        [XmlAttribute("PhuthuDungtuyen")]
        [Bindable(true)]
        public decimal? PhuthuDungtuyen 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("phuthu_dungtuyen");
		    }
            set 
		    {
			    SetColumnValue("phuthu_dungtuyen", value);
            }
        }
	      
        [XmlAttribute("PhuthuTraituyen")]
        [Bindable(true)]
        public decimal? PhuthuTraituyen 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("phuthu_traituyen");
		    }
            set 
		    {
			    SetColumnValue("phuthu_traituyen", value);
            }
        }
	      
        [XmlAttribute("DongiaNgoaigio")]
        [Bindable(true)]
        public decimal? DongiaNgoaigio 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("dongia_ngoaigio");
		    }
            set 
		    {
			    SetColumnValue("dongia_ngoaigio", value);
            }
        }
	      
        [XmlAttribute("PhuthuNgoaigio")]
        [Bindable(true)]
        public decimal? PhuthuNgoaigio 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("phuthu_ngoaigio");
		    }
            set 
		    {
			    SetColumnValue("phuthu_ngoaigio", value);
            }
        }
	      
        [XmlAttribute("MaDoituongKcb")]
        [Bindable(true)]
        public string MaDoituongKcb 
	    {
		    get
		    {
			    return GetColumnValue<string>("ma_doituong_kcb");
		    }
            set 
		    {
			    SetColumnValue("ma_doituong_kcb", value);
            }
        }
	      
        [XmlAttribute("IdPhikemtheo")]
        [Bindable(true)]
        public int? IdPhikemtheo 
	    {
		    get
		    {
			    return GetColumnValue<int?>("id_phikemtheo");
		    }
            set 
		    {
			    SetColumnValue("id_phikemtheo", value);
            }
        }
	      
        [XmlAttribute("IdPhikemtheongoaigio")]
        [Bindable(true)]
        public int? IdPhikemtheongoaigio 
	    {
		    get
		    {
			    return GetColumnValue<int?>("id_phikemtheongoaigio");
		    }
            set 
		    {
			    SetColumnValue("id_phikemtheongoaigio", value);
            }
        }
	      
        [XmlAttribute("NhomBaocao")]
        [Bindable(true)]
        public string NhomBaocao 
	    {
		    get
		    {
			    return GetColumnValue<string>("nhom_baocao");
		    }
            set 
		    {
			    SetColumnValue("nhom_baocao", value);
            }
        }
	      
        [XmlAttribute("TuTuc")]
        [Bindable(true)]
        public byte? TuTuc 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("tu_tuc");
		    }
            set 
		    {
			    SetColumnValue("tu_tuc", value);
            }
        }
	      
        [XmlAttribute("SttHthi")]
        [Bindable(true)]
        public short? SttHthi 
	    {
		    get
		    {
			    return GetColumnValue<short?>("stt_hthi");
		    }
            set 
		    {
			    SetColumnValue("stt_hthi", value);
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
	      
        [XmlAttribute("MaGia")]
        [Bindable(true)]
        public string MaGia 
	    {
		    get
		    {
			    return GetColumnValue<string>("ma_gia");
		    }
            set 
		    {
			    SetColumnValue("ma_gia", value);
            }
        }
	      
        [XmlAttribute("HoatDong")]
        [Bindable(true)]
        public bool? HoatDong 
	    {
		    get
		    {
			    return GetColumnValue<bool?>("hoat_dong");
		    }
            set 
		    {
			    SetColumnValue("hoat_dong", value);
            }
        }
	      
        [XmlAttribute("IdLoaidoituongKcb")]
        [Bindable(true)]
        public short? IdLoaidoituongKcb 
	    {
		    get
		    {
			    return GetColumnValue<short?>("id_loaidoituong_kcb");
		    }
            set 
		    {
			    SetColumnValue("id_loaidoituong_kcb", value);
            }
        }
	      
        [XmlAttribute("KhamThiluc")]
        [Bindable(true)]
        public byte? KhamThiluc 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("kham_thiluc");
		    }
            set 
		    {
			    SetColumnValue("kham_thiluc", value);
            }
        }
	      
        [XmlAttribute("CapKinh")]
        [Bindable(true)]
        public bool? CapKinh 
	    {
		    get
		    {
			    return GetColumnValue<bool?>("cap_kinh");
		    }
            set 
		    {
			    SetColumnValue("cap_kinh", value);
            }
        }
	      
        [XmlAttribute("TenBacsi")]
        [Bindable(true)]
        public string TenBacsi 
	    {
		    get
		    {
			    return GetColumnValue<string>("ten_bacsi");
		    }
            set 
		    {
			    SetColumnValue("ten_bacsi", value);
            }
        }
	      
        [XmlAttribute("TenKhoa")]
        [Bindable(true)]
        public string TenKhoa 
	    {
		    get
		    {
			    return GetColumnValue<string>("ten_khoa");
		    }
            set 
		    {
			    SetColumnValue("ten_khoa", value);
            }
        }
	      
        [XmlAttribute("TenPhong")]
        [Bindable(true)]
        public string TenPhong 
	    {
		    get
		    {
			    return GetColumnValue<string>("ten_phong");
		    }
            set 
		    {
			    SetColumnValue("ten_phong", value);
            }
        }
	      
        [XmlAttribute("TenKieukham")]
        [Bindable(true)]
        public string TenKieukham 
	    {
		    get
		    {
			    return GetColumnValue<string>("ten_kieukham");
		    }
            set 
		    {
			    SetColumnValue("ten_kieukham", value);
            }
        }
	      
        [XmlAttribute("TenDoituongKcb")]
        [Bindable(true)]
        public string TenDoituongKcb 
	    {
		    get
		    {
			    return GetColumnValue<string>("ten_doituong_kcb");
		    }
            set 
		    {
			    SetColumnValue("ten_doituong_kcb", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string IdDichvukcb = @"id_dichvukcb";
            
            public static string MaDichvukcb = @"ma_dichvukcb";
            
            public static string MaBhyt = @"ma_bhyt";
            
            public static string TenDichvukcb = @"ten_dichvukcb";
            
            public static string IdKieukham = @"id_kieukham";
            
            public static string IdKhoaphong = @"id_khoaphong";
            
            public static string IdBacsy = @"id_bacsy";
            
            public static string IdDoituongKcb = @"id_doituong_kcb";
            
            public static string IdPhongkham = @"id_phongkham";
            
            public static string DonGia = @"don_gia";
            
            public static string PhuthuDungtuyen = @"phuthu_dungtuyen";
            
            public static string PhuthuTraituyen = @"phuthu_traituyen";
            
            public static string DongiaNgoaigio = @"dongia_ngoaigio";
            
            public static string PhuthuNgoaigio = @"phuthu_ngoaigio";
            
            public static string MaDoituongKcb = @"ma_doituong_kcb";
            
            public static string IdPhikemtheo = @"id_phikemtheo";
            
            public static string IdPhikemtheongoaigio = @"id_phikemtheongoaigio";
            
            public static string NhomBaocao = @"nhom_baocao";
            
            public static string TuTuc = @"tu_tuc";
            
            public static string SttHthi = @"stt_hthi";
            
            public static string MotaThem = @"mota_them";
            
            public static string MaGia = @"ma_gia";
            
            public static string HoatDong = @"hoat_dong";
            
            public static string IdLoaidoituongKcb = @"id_loaidoituong_kcb";
            
            public static string KhamThiluc = @"kham_thiluc";
            
            public static string CapKinh = @"cap_kinh";
            
            public static string TenBacsi = @"ten_bacsi";
            
            public static string TenKhoa = @"ten_khoa";
            
            public static string TenPhong = @"ten_phong";
            
            public static string TenKieukham = @"ten_kieukham";
            
            public static string TenDoituongKcb = @"ten_doituong_kcb";
            
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
