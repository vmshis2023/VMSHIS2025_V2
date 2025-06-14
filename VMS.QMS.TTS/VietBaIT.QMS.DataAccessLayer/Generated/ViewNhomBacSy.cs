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
namespace VietBaIT.HISLink.DataAccessLayer{
    /// <summary>
    /// Strongly-typed collection for the ViewNhomBacSy class.
    /// </summary>
    [Serializable]
    public partial class ViewNhomBacSyCollection : ReadOnlyList<ViewNhomBacSy, ViewNhomBacSyCollection>
    {        
        public ViewNhomBacSyCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the view_Nhom_BacSy view.
    /// </summary>
    [Serializable]
    public partial class ViewNhomBacSy : ReadOnlyRecord<ViewNhomBacSy>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("view_Nhom_BacSy", TableType.View, DataService.GetInstance("ORM"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarStaffId = new TableSchema.TableColumn(schema);
                colvarStaffId.ColumnName = "Staff_ID";
                colvarStaffId.DataType = DbType.Int16;
                colvarStaffId.MaxLength = 0;
                colvarStaffId.AutoIncrement = false;
                colvarStaffId.IsNullable = false;
                colvarStaffId.IsPrimaryKey = false;
                colvarStaffId.IsForeignKey = false;
                colvarStaffId.IsReadOnly = false;
                
                schema.Columns.Add(colvarStaffId);
                
                TableSchema.TableColumn colvarStaffCode = new TableSchema.TableColumn(schema);
                colvarStaffCode.ColumnName = "Staff_Code";
                colvarStaffCode.DataType = DbType.String;
                colvarStaffCode.MaxLength = 50;
                colvarStaffCode.AutoIncrement = false;
                colvarStaffCode.IsNullable = true;
                colvarStaffCode.IsPrimaryKey = false;
                colvarStaffCode.IsForeignKey = false;
                colvarStaffCode.IsReadOnly = false;
                
                schema.Columns.Add(colvarStaffCode);
                
                TableSchema.TableColumn colvarStaffName = new TableSchema.TableColumn(schema);
                colvarStaffName.ColumnName = "Staff_Name";
                colvarStaffName.DataType = DbType.String;
                colvarStaffName.MaxLength = 100;
                colvarStaffName.AutoIncrement = false;
                colvarStaffName.IsNullable = false;
                colvarStaffName.IsPrimaryKey = false;
                colvarStaffName.IsForeignKey = false;
                colvarStaffName.IsReadOnly = false;
                
                schema.Columns.Add(colvarStaffName);
                
                TableSchema.TableColumn colvarDepartmentId = new TableSchema.TableColumn(schema);
                colvarDepartmentId.ColumnName = "Department_ID";
                colvarDepartmentId.DataType = DbType.Int16;
                colvarDepartmentId.MaxLength = 0;
                colvarDepartmentId.AutoIncrement = false;
                colvarDepartmentId.IsNullable = false;
                colvarDepartmentId.IsPrimaryKey = false;
                colvarDepartmentId.IsForeignKey = false;
                colvarDepartmentId.IsReadOnly = false;
                
                schema.Columns.Add(colvarDepartmentId);
                
                TableSchema.TableColumn colvarParentId = new TableSchema.TableColumn(schema);
                colvarParentId.ColumnName = "Parent_ID";
                colvarParentId.DataType = DbType.Int32;
                colvarParentId.MaxLength = 0;
                colvarParentId.AutoIncrement = false;
                colvarParentId.IsNullable = true;
                colvarParentId.IsPrimaryKey = false;
                colvarParentId.IsForeignKey = false;
                colvarParentId.IsReadOnly = false;
                
                schema.Columns.Add(colvarParentId);
                
                TableSchema.TableColumn colvarStaffTypeId = new TableSchema.TableColumn(schema);
                colvarStaffTypeId.ColumnName = "StaffType_ID";
                colvarStaffTypeId.DataType = DbType.Int16;
                colvarStaffTypeId.MaxLength = 0;
                colvarStaffTypeId.AutoIncrement = false;
                colvarStaffTypeId.IsNullable = false;
                colvarStaffTypeId.IsPrimaryKey = false;
                colvarStaffTypeId.IsForeignKey = false;
                colvarStaffTypeId.IsReadOnly = false;
                
                schema.Columns.Add(colvarStaffTypeId);
                
                TableSchema.TableColumn colvarRankId = new TableSchema.TableColumn(schema);
                colvarRankId.ColumnName = "Rank_ID";
                colvarRankId.DataType = DbType.Int16;
                colvarRankId.MaxLength = 0;
                colvarRankId.AutoIncrement = false;
                colvarRankId.IsNullable = false;
                colvarRankId.IsPrimaryKey = false;
                colvarRankId.IsForeignKey = false;
                colvarRankId.IsReadOnly = false;
                
                schema.Columns.Add(colvarRankId);
                
                TableSchema.TableColumn colvarUid = new TableSchema.TableColumn(schema);
                colvarUid.ColumnName = "UID";
                colvarUid.DataType = DbType.AnsiString;
                colvarUid.MaxLength = 50;
                colvarUid.AutoIncrement = false;
                colvarUid.IsNullable = true;
                colvarUid.IsPrimaryKey = false;
                colvarUid.IsForeignKey = false;
                colvarUid.IsReadOnly = false;
                
                schema.Columns.Add(colvarUid);
                
                TableSchema.TableColumn colvarStatus = new TableSchema.TableColumn(schema);
                colvarStatus.ColumnName = "Status";
                colvarStatus.DataType = DbType.Byte;
                colvarStatus.MaxLength = 0;
                colvarStatus.AutoIncrement = false;
                colvarStatus.IsNullable = true;
                colvarStatus.IsPrimaryKey = false;
                colvarStatus.IsForeignKey = false;
                colvarStatus.IsReadOnly = false;
                
                schema.Columns.Add(colvarStatus);
                
                TableSchema.TableColumn colvarSDesc = new TableSchema.TableColumn(schema);
                colvarSDesc.ColumnName = "sDesc";
                colvarSDesc.DataType = DbType.String;
                colvarSDesc.MaxLength = 255;
                colvarSDesc.AutoIncrement = false;
                colvarSDesc.IsNullable = true;
                colvarSDesc.IsPrimaryKey = false;
                colvarSDesc.IsForeignKey = false;
                colvarSDesc.IsReadOnly = false;
                
                schema.Columns.Add(colvarSDesc);
                
                TableSchema.TableColumn colvarSpecMoney = new TableSchema.TableColumn(schema);
                colvarSpecMoney.ColumnName = "SpecMoney";
                colvarSpecMoney.DataType = DbType.Currency;
                colvarSpecMoney.MaxLength = 0;
                colvarSpecMoney.AutoIncrement = false;
                colvarSpecMoney.IsNullable = false;
                colvarSpecMoney.IsPrimaryKey = false;
                colvarSpecMoney.IsForeignKey = false;
                colvarSpecMoney.IsReadOnly = false;
                
                schema.Columns.Add(colvarSpecMoney);
                
                TableSchema.TableColumn colvarActived = new TableSchema.TableColumn(schema);
                colvarActived.ColumnName = "Actived";
                colvarActived.DataType = DbType.Byte;
                colvarActived.MaxLength = 0;
                colvarActived.AutoIncrement = false;
                colvarActived.IsNullable = true;
                colvarActived.IsPrimaryKey = false;
                colvarActived.IsForeignKey = false;
                colvarActived.IsReadOnly = false;
                
                schema.Columns.Add(colvarActived);
                
                TableSchema.TableColumn colvarCanSign = new TableSchema.TableColumn(schema);
                colvarCanSign.ColumnName = "CanSign";
                colvarCanSign.DataType = DbType.Int32;
                colvarCanSign.MaxLength = 0;
                colvarCanSign.AutoIncrement = false;
                colvarCanSign.IsNullable = true;
                colvarCanSign.IsPrimaryKey = false;
                colvarCanSign.IsForeignKey = false;
                colvarCanSign.IsReadOnly = false;
                
                schema.Columns.Add(colvarCanSign);
                
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
                
                TableSchema.TableColumn colvarChoPhepChuyenGoi = new TableSchema.TableColumn(schema);
                colvarChoPhepChuyenGoi.ColumnName = "CHO_PHEP_CHUYEN_GOI";
                colvarChoPhepChuyenGoi.DataType = DbType.Byte;
                colvarChoPhepChuyenGoi.MaxLength = 0;
                colvarChoPhepChuyenGoi.AutoIncrement = false;
                colvarChoPhepChuyenGoi.IsNullable = true;
                colvarChoPhepChuyenGoi.IsPrimaryKey = false;
                colvarChoPhepChuyenGoi.IsForeignKey = false;
                colvarChoPhepChuyenGoi.IsReadOnly = false;
                
                schema.Columns.Add(colvarChoPhepChuyenGoi);
                
                TableSchema.TableColumn colvarChoPhepDchinhTtoan = new TableSchema.TableColumn(schema);
                colvarChoPhepDchinhTtoan.ColumnName = "CHO_PHEP_DCHINH_TTOAN";
                colvarChoPhepDchinhTtoan.DataType = DbType.Byte;
                colvarChoPhepDchinhTtoan.MaxLength = 0;
                colvarChoPhepDchinhTtoan.AutoIncrement = false;
                colvarChoPhepDchinhTtoan.IsNullable = true;
                colvarChoPhepDchinhTtoan.IsPrimaryKey = false;
                colvarChoPhepDchinhTtoan.IsForeignKey = false;
                colvarChoPhepDchinhTtoan.IsReadOnly = false;
                
                schema.Columns.Add(colvarChoPhepDchinhTtoan);
                
                TableSchema.TableColumn colvarChoPhepHuyKhoat = new TableSchema.TableColumn(schema);
                colvarChoPhepHuyKhoat.ColumnName = "CHO_PHEP_HUY_KHOAT";
                colvarChoPhepHuyKhoat.DataType = DbType.Byte;
                colvarChoPhepHuyKhoat.MaxLength = 0;
                colvarChoPhepHuyKhoat.AutoIncrement = false;
                colvarChoPhepHuyKhoat.IsNullable = true;
                colvarChoPhepHuyKhoat.IsPrimaryKey = false;
                colvarChoPhepHuyKhoat.IsForeignKey = false;
                colvarChoPhepHuyKhoat.IsReadOnly = false;
                
                schema.Columns.Add(colvarChoPhepHuyKhoat);
                
                TableSchema.TableColumn colvarChoPhepKhoat = new TableSchema.TableColumn(schema);
                colvarChoPhepKhoat.ColumnName = "CHO_PHEP_KHOAT";
                colvarChoPhepKhoat.DataType = DbType.Byte;
                colvarChoPhepKhoat.MaxLength = 0;
                colvarChoPhepKhoat.AutoIncrement = false;
                colvarChoPhepKhoat.IsNullable = true;
                colvarChoPhepKhoat.IsPrimaryKey = false;
                colvarChoPhepKhoat.IsForeignKey = false;
                colvarChoPhepKhoat.IsReadOnly = false;
                
                schema.Columns.Add(colvarChoPhepKhoat);
                
                TableSchema.TableColumn colvarChoPhepNvien = new TableSchema.TableColumn(schema);
                colvarChoPhepNvien.ColumnName = "CHO_PHEP_NVIEN";
                colvarChoPhepNvien.DataType = DbType.Byte;
                colvarChoPhepNvien.MaxLength = 0;
                colvarChoPhepNvien.AutoIncrement = false;
                colvarChoPhepNvien.IsNullable = true;
                colvarChoPhepNvien.IsPrimaryKey = false;
                colvarChoPhepNvien.IsForeignKey = false;
                colvarChoPhepNvien.IsReadOnly = false;
                
                schema.Columns.Add(colvarChoPhepNvien);
                
                TableSchema.TableColumn colvarChoPhepNhapTheBhyt = new TableSchema.TableColumn(schema);
                colvarChoPhepNhapTheBhyt.ColumnName = "CHO_PHEP_NHAP_THE_BHYT";
                colvarChoPhepNhapTheBhyt.DataType = DbType.Byte;
                colvarChoPhepNhapTheBhyt.MaxLength = 0;
                colvarChoPhepNhapTheBhyt.AutoIncrement = false;
                colvarChoPhepNhapTheBhyt.IsNullable = true;
                colvarChoPhepNhapTheBhyt.IsPrimaryKey = false;
                colvarChoPhepNhapTheBhyt.IsForeignKey = false;
                colvarChoPhepNhapTheBhyt.IsReadOnly = false;
                
                schema.Columns.Add(colvarChoPhepNhapTheBhyt);
                
                TableSchema.TableColumn colvarChoPhepSuaDlieu = new TableSchema.TableColumn(schema);
                colvarChoPhepSuaDlieu.ColumnName = "CHO_PHEP_SUA_DLIEU";
                colvarChoPhepSuaDlieu.DataType = DbType.Byte;
                colvarChoPhepSuaDlieu.MaxLength = 0;
                colvarChoPhepSuaDlieu.AutoIncrement = false;
                colvarChoPhepSuaDlieu.IsNullable = true;
                colvarChoPhepSuaDlieu.IsPrimaryKey = false;
                colvarChoPhepSuaDlieu.IsForeignKey = false;
                colvarChoPhepSuaDlieu.IsReadOnly = false;
                
                schema.Columns.Add(colvarChoPhepSuaDlieu);
                
                TableSchema.TableColumn colvarChoPhepChonBly = new TableSchema.TableColumn(schema);
                colvarChoPhepChonBly.ColumnName = "CHO_PHEP_CHON_BLY";
                colvarChoPhepChonBly.DataType = DbType.Byte;
                colvarChoPhepChonBly.MaxLength = 0;
                colvarChoPhepChonBly.AutoIncrement = false;
                colvarChoPhepChonBly.IsNullable = true;
                colvarChoPhepChonBly.IsPrimaryKey = false;
                colvarChoPhepChonBly.IsForeignKey = false;
                colvarChoPhepChonBly.IsReadOnly = false;
                
                schema.Columns.Add(colvarChoPhepChonBly);
                
                TableSchema.TableColumn colvarChoPhepDieuChinhBg = new TableSchema.TableColumn(schema);
                colvarChoPhepDieuChinhBg.ColumnName = "CHO_PHEP_DIEU_CHINH_BG";
                colvarChoPhepDieuChinhBg.DataType = DbType.Byte;
                colvarChoPhepDieuChinhBg.MaxLength = 0;
                colvarChoPhepDieuChinhBg.AutoIncrement = false;
                colvarChoPhepDieuChinhBg.IsNullable = true;
                colvarChoPhepDieuChinhBg.IsPrimaryKey = false;
                colvarChoPhepDieuChinhBg.IsForeignKey = false;
                colvarChoPhepDieuChinhBg.IsReadOnly = false;
                
                schema.Columns.Add(colvarChoPhepDieuChinhBg);
                
                TableSchema.TableColumn colvarMaQuyen = new TableSchema.TableColumn(schema);
                colvarMaQuyen.ColumnName = "MA_QUYEN";
                colvarMaQuyen.DataType = DbType.String;
                colvarMaQuyen.MaxLength = 1000;
                colvarMaQuyen.AutoIncrement = false;
                colvarMaQuyen.IsNullable = true;
                colvarMaQuyen.IsPrimaryKey = false;
                colvarMaQuyen.IsForeignKey = false;
                colvarMaQuyen.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaQuyen);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("view_Nhom_BacSy",schema);
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
	    public ViewNhomBacSy()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public ViewNhomBacSy(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public ViewNhomBacSy(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public ViewNhomBacSy(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("StaffId")]
        [Bindable(true)]
        public short StaffId 
	    {
		    get
		    {
			    return GetColumnValue<short>("Staff_ID");
		    }
            set 
		    {
			    SetColumnValue("Staff_ID", value);
            }
        }
	      
        [XmlAttribute("StaffCode")]
        [Bindable(true)]
        public string StaffCode 
	    {
		    get
		    {
			    return GetColumnValue<string>("Staff_Code");
		    }
            set 
		    {
			    SetColumnValue("Staff_Code", value);
            }
        }
	      
        [XmlAttribute("StaffName")]
        [Bindable(true)]
        public string StaffName 
	    {
		    get
		    {
			    return GetColumnValue<string>("Staff_Name");
		    }
            set 
		    {
			    SetColumnValue("Staff_Name", value);
            }
        }
	      
        [XmlAttribute("DepartmentId")]
        [Bindable(true)]
        public short DepartmentId 
	    {
		    get
		    {
			    return GetColumnValue<short>("Department_ID");
		    }
            set 
		    {
			    SetColumnValue("Department_ID", value);
            }
        }
	      
        [XmlAttribute("ParentId")]
        [Bindable(true)]
        public int? ParentId 
	    {
		    get
		    {
			    return GetColumnValue<int?>("Parent_ID");
		    }
            set 
		    {
			    SetColumnValue("Parent_ID", value);
            }
        }
	      
        [XmlAttribute("StaffTypeId")]
        [Bindable(true)]
        public short StaffTypeId 
	    {
		    get
		    {
			    return GetColumnValue<short>("StaffType_ID");
		    }
            set 
		    {
			    SetColumnValue("StaffType_ID", value);
            }
        }
	      
        [XmlAttribute("RankId")]
        [Bindable(true)]
        public short RankId 
	    {
		    get
		    {
			    return GetColumnValue<short>("Rank_ID");
		    }
            set 
		    {
			    SetColumnValue("Rank_ID", value);
            }
        }
	      
        [XmlAttribute("Uid")]
        [Bindable(true)]
        public string Uid 
	    {
		    get
		    {
			    return GetColumnValue<string>("UID");
		    }
            set 
		    {
			    SetColumnValue("UID", value);
            }
        }
	      
        [XmlAttribute("Status")]
        [Bindable(true)]
        public byte? Status 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("Status");
		    }
            set 
		    {
			    SetColumnValue("Status", value);
            }
        }
	      
        [XmlAttribute("SDesc")]
        [Bindable(true)]
        public string SDesc 
	    {
		    get
		    {
			    return GetColumnValue<string>("sDesc");
		    }
            set 
		    {
			    SetColumnValue("sDesc", value);
            }
        }
	      
        [XmlAttribute("SpecMoney")]
        [Bindable(true)]
        public decimal SpecMoney 
	    {
		    get
		    {
			    return GetColumnValue<decimal>("SpecMoney");
		    }
            set 
		    {
			    SetColumnValue("SpecMoney", value);
            }
        }
	      
        [XmlAttribute("Actived")]
        [Bindable(true)]
        public byte? Actived 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("Actived");
		    }
            set 
		    {
			    SetColumnValue("Actived", value);
            }
        }
	      
        [XmlAttribute("CanSign")]
        [Bindable(true)]
        public int? CanSign 
	    {
		    get
		    {
			    return GetColumnValue<int?>("CanSign");
		    }
            set 
		    {
			    SetColumnValue("CanSign", value);
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
	      
        [XmlAttribute("ChoPhepChuyenGoi")]
        [Bindable(true)]
        public byte? ChoPhepChuyenGoi 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("CHO_PHEP_CHUYEN_GOI");
		    }
            set 
		    {
			    SetColumnValue("CHO_PHEP_CHUYEN_GOI", value);
            }
        }
	      
        [XmlAttribute("ChoPhepDchinhTtoan")]
        [Bindable(true)]
        public byte? ChoPhepDchinhTtoan 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("CHO_PHEP_DCHINH_TTOAN");
		    }
            set 
		    {
			    SetColumnValue("CHO_PHEP_DCHINH_TTOAN", value);
            }
        }
	      
        [XmlAttribute("ChoPhepHuyKhoat")]
        [Bindable(true)]
        public byte? ChoPhepHuyKhoat 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("CHO_PHEP_HUY_KHOAT");
		    }
            set 
		    {
			    SetColumnValue("CHO_PHEP_HUY_KHOAT", value);
            }
        }
	      
        [XmlAttribute("ChoPhepKhoat")]
        [Bindable(true)]
        public byte? ChoPhepKhoat 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("CHO_PHEP_KHOAT");
		    }
            set 
		    {
			    SetColumnValue("CHO_PHEP_KHOAT", value);
            }
        }
	      
        [XmlAttribute("ChoPhepNvien")]
        [Bindable(true)]
        public byte? ChoPhepNvien 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("CHO_PHEP_NVIEN");
		    }
            set 
		    {
			    SetColumnValue("CHO_PHEP_NVIEN", value);
            }
        }
	      
        [XmlAttribute("ChoPhepNhapTheBhyt")]
        [Bindable(true)]
        public byte? ChoPhepNhapTheBhyt 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("CHO_PHEP_NHAP_THE_BHYT");
		    }
            set 
		    {
			    SetColumnValue("CHO_PHEP_NHAP_THE_BHYT", value);
            }
        }
	      
        [XmlAttribute("ChoPhepSuaDlieu")]
        [Bindable(true)]
        public byte? ChoPhepSuaDlieu 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("CHO_PHEP_SUA_DLIEU");
		    }
            set 
		    {
			    SetColumnValue("CHO_PHEP_SUA_DLIEU", value);
            }
        }
	      
        [XmlAttribute("ChoPhepChonBly")]
        [Bindable(true)]
        public byte? ChoPhepChonBly 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("CHO_PHEP_CHON_BLY");
		    }
            set 
		    {
			    SetColumnValue("CHO_PHEP_CHON_BLY", value);
            }
        }
	      
        [XmlAttribute("ChoPhepDieuChinhBg")]
        [Bindable(true)]
        public byte? ChoPhepDieuChinhBg 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("CHO_PHEP_DIEU_CHINH_BG");
		    }
            set 
		    {
			    SetColumnValue("CHO_PHEP_DIEU_CHINH_BG", value);
            }
        }
	      
        [XmlAttribute("MaQuyen")]
        [Bindable(true)]
        public string MaQuyen 
	    {
		    get
		    {
			    return GetColumnValue<string>("MA_QUYEN");
		    }
            set 
		    {
			    SetColumnValue("MA_QUYEN", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string StaffId = @"Staff_ID";
            
            public static string StaffCode = @"Staff_Code";
            
            public static string StaffName = @"Staff_Name";
            
            public static string DepartmentId = @"Department_ID";
            
            public static string ParentId = @"Parent_ID";
            
            public static string StaffTypeId = @"StaffType_ID";
            
            public static string RankId = @"Rank_ID";
            
            public static string Uid = @"UID";
            
            public static string Status = @"Status";
            
            public static string SDesc = @"sDesc";
            
            public static string SpecMoney = @"SpecMoney";
            
            public static string Actived = @"Actived";
            
            public static string CanSign = @"CanSign";
            
            public static string NguoiTao = @"NGUOI_TAO";
            
            public static string NgayTao = @"NGAY_TAO";
            
            public static string NguoiSua = @"NGUOI_SUA";
            
            public static string NgaySua = @"NGAY_SUA";
            
            public static string ChoPhepChuyenGoi = @"CHO_PHEP_CHUYEN_GOI";
            
            public static string ChoPhepDchinhTtoan = @"CHO_PHEP_DCHINH_TTOAN";
            
            public static string ChoPhepHuyKhoat = @"CHO_PHEP_HUY_KHOAT";
            
            public static string ChoPhepKhoat = @"CHO_PHEP_KHOAT";
            
            public static string ChoPhepNvien = @"CHO_PHEP_NVIEN";
            
            public static string ChoPhepNhapTheBhyt = @"CHO_PHEP_NHAP_THE_BHYT";
            
            public static string ChoPhepSuaDlieu = @"CHO_PHEP_SUA_DLIEU";
            
            public static string ChoPhepChonBly = @"CHO_PHEP_CHON_BLY";
            
            public static string ChoPhepDieuChinhBg = @"CHO_PHEP_DIEU_CHINH_BG";
            
            public static string MaQuyen = @"MA_QUYEN";
            
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
