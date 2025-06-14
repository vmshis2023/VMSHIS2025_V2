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
	/// Strongly-typed collection for the LStaff class.
	/// </summary>
    [Serializable]
	public partial class LStaffCollection : ActiveList<LStaff, LStaffCollection>
	{	   
		public LStaffCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LStaffCollection</returns>
		public LStaffCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LStaff o = this[i];
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
	/// This is an ActiveRecord class which wraps the L_Staffs table.
	/// </summary>
	[Serializable]
	public partial class LStaff : ActiveRecord<LStaff>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LStaff()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LStaff(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LStaff(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LStaff(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("L_Staffs", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarStaffId = new TableSchema.TableColumn(schema);
				colvarStaffId.ColumnName = "Staff_ID";
				colvarStaffId.DataType = DbType.Int16;
				colvarStaffId.MaxLength = 0;
				colvarStaffId.AutoIncrement = true;
				colvarStaffId.IsNullable = false;
				colvarStaffId.IsPrimaryKey = true;
				colvarStaffId.IsForeignKey = false;
				colvarStaffId.IsReadOnly = false;
				colvarStaffId.DefaultSetting = @"";
				colvarStaffId.ForeignKeyTableName = "";
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
				colvarStaffCode.DefaultSetting = @"";
				colvarStaffCode.ForeignKeyTableName = "";
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
				colvarStaffName.DefaultSetting = @"";
				colvarStaffName.ForeignKeyTableName = "";
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
				colvarDepartmentId.DefaultSetting = @"";
				colvarDepartmentId.ForeignKeyTableName = "";
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
				colvarParentId.DefaultSetting = @"";
				colvarParentId.ForeignKeyTableName = "";
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
				colvarStaffTypeId.DefaultSetting = @"";
				colvarStaffTypeId.ForeignKeyTableName = "";
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
				colvarRankId.DefaultSetting = @"";
				colvarRankId.ForeignKeyTableName = "";
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
				colvarUid.DefaultSetting = @"";
				colvarUid.ForeignKeyTableName = "";
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
				colvarStatus.DefaultSetting = @"";
				colvarStatus.ForeignKeyTableName = "";
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
				colvarSDesc.DefaultSetting = @"";
				colvarSDesc.ForeignKeyTableName = "";
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
				
						colvarSpecMoney.DefaultSetting = @"((0))";
				colvarSpecMoney.ForeignKeyTableName = "";
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
				
						colvarActived.DefaultSetting = @"((0))";
				colvarActived.ForeignKeyTableName = "";
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
				
						colvarCanSign.DefaultSetting = @"((0))";
				colvarCanSign.ForeignKeyTableName = "";
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
				colvarNguoiTao.DefaultSetting = @"";
				colvarNguoiTao.ForeignKeyTableName = "";
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
				colvarNgayTao.DefaultSetting = @"";
				colvarNgayTao.ForeignKeyTableName = "";
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
				colvarNguoiSua.DefaultSetting = @"";
				colvarNguoiSua.ForeignKeyTableName = "";
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
				colvarNgaySua.DefaultSetting = @"";
				colvarNgaySua.ForeignKeyTableName = "";
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
				colvarChoPhepChuyenGoi.DefaultSetting = @"";
				colvarChoPhepChuyenGoi.ForeignKeyTableName = "";
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
				colvarChoPhepDchinhTtoan.DefaultSetting = @"";
				colvarChoPhepDchinhTtoan.ForeignKeyTableName = "";
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
				colvarChoPhepHuyKhoat.DefaultSetting = @"";
				colvarChoPhepHuyKhoat.ForeignKeyTableName = "";
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
				colvarChoPhepKhoat.DefaultSetting = @"";
				colvarChoPhepKhoat.ForeignKeyTableName = "";
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
				colvarChoPhepNvien.DefaultSetting = @"";
				colvarChoPhepNvien.ForeignKeyTableName = "";
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
				colvarChoPhepNhapTheBhyt.DefaultSetting = @"";
				colvarChoPhepNhapTheBhyt.ForeignKeyTableName = "";
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
				colvarChoPhepSuaDlieu.DefaultSetting = @"";
				colvarChoPhepSuaDlieu.ForeignKeyTableName = "";
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
				
						colvarChoPhepChonBly.DefaultSetting = @"((0))";
				colvarChoPhepChonBly.ForeignKeyTableName = "";
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
				
						colvarChoPhepDieuChinhBg.DefaultSetting = @"((0))";
				colvarChoPhepDieuChinhBg.ForeignKeyTableName = "";
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
				colvarMaQuyen.DefaultSetting = @"";
				colvarMaQuyen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaQuyen);
				
				TableSchema.TableColumn colvarSoThuTu = new TableSchema.TableColumn(schema);
				colvarSoThuTu.ColumnName = "SO_THU_TU";
				colvarSoThuTu.DataType = DbType.Int32;
				colvarSoThuTu.MaxLength = 0;
				colvarSoThuTu.AutoIncrement = false;
				colvarSoThuTu.IsNullable = true;
				colvarSoThuTu.IsPrimaryKey = false;
				colvarSoThuTu.IsForeignKey = false;
				colvarSoThuTu.IsReadOnly = false;
				colvarSoThuTu.DefaultSetting = @"";
				colvarSoThuTu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoThuTu);
				
				TableSchema.TableColumn colvarChoMGiam = new TableSchema.TableColumn(schema);
				colvarChoMGiam.ColumnName = "Cho_MGiam";
				colvarChoMGiam.DataType = DbType.Byte;
				colvarChoMGiam.MaxLength = 0;
				colvarChoMGiam.AutoIncrement = false;
				colvarChoMGiam.IsNullable = true;
				colvarChoMGiam.IsPrimaryKey = false;
				colvarChoMGiam.IsForeignKey = false;
				colvarChoMGiam.IsReadOnly = false;
				
						colvarChoMGiam.DefaultSetting = @"((0))";
				colvarChoMGiam.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChoMGiam);
				
				TableSchema.TableColumn colvarIsUpdatePhieuDtri = new TableSchema.TableColumn(schema);
				colvarIsUpdatePhieuDtri.ColumnName = "IsUpdatePhieuDtri";
				colvarIsUpdatePhieuDtri.DataType = DbType.Boolean;
				colvarIsUpdatePhieuDtri.MaxLength = 0;
				colvarIsUpdatePhieuDtri.AutoIncrement = false;
				colvarIsUpdatePhieuDtri.IsNullable = true;
				colvarIsUpdatePhieuDtri.IsPrimaryKey = false;
				colvarIsUpdatePhieuDtri.IsForeignKey = false;
				colvarIsUpdatePhieuDtri.IsReadOnly = false;
				
						colvarIsUpdatePhieuDtri.DefaultSetting = @"((0))";
				colvarIsUpdatePhieuDtri.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsUpdatePhieuDtri);
				
				TableSchema.TableColumn colvarIsGiayBhyt = new TableSchema.TableColumn(schema);
				colvarIsGiayBhyt.ColumnName = "Is_Giay_Bhyt";
				colvarIsGiayBhyt.DataType = DbType.Boolean;
				colvarIsGiayBhyt.MaxLength = 0;
				colvarIsGiayBhyt.AutoIncrement = false;
				colvarIsGiayBhyt.IsNullable = true;
				colvarIsGiayBhyt.IsPrimaryKey = false;
				colvarIsGiayBhyt.IsForeignKey = false;
				colvarIsGiayBhyt.IsReadOnly = false;
				
						colvarIsGiayBhyt.DefaultSetting = @"((0))";
				colvarIsGiayBhyt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsGiayBhyt);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("L_Staffs",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("StaffId")]
		[Bindable(true)]
		public short StaffId 
		{
			get { return GetColumnValue<short>(Columns.StaffId); }
			set { SetColumnValue(Columns.StaffId, value); }
		}
		  
		[XmlAttribute("StaffCode")]
		[Bindable(true)]
		public string StaffCode 
		{
			get { return GetColumnValue<string>(Columns.StaffCode); }
			set { SetColumnValue(Columns.StaffCode, value); }
		}
		  
		[XmlAttribute("StaffName")]
		[Bindable(true)]
		public string StaffName 
		{
			get { return GetColumnValue<string>(Columns.StaffName); }
			set { SetColumnValue(Columns.StaffName, value); }
		}
		  
		[XmlAttribute("DepartmentId")]
		[Bindable(true)]
		public short DepartmentId 
		{
			get { return GetColumnValue<short>(Columns.DepartmentId); }
			set { SetColumnValue(Columns.DepartmentId, value); }
		}
		  
		[XmlAttribute("ParentId")]
		[Bindable(true)]
		public int? ParentId 
		{
			get { return GetColumnValue<int?>(Columns.ParentId); }
			set { SetColumnValue(Columns.ParentId, value); }
		}
		  
		[XmlAttribute("StaffTypeId")]
		[Bindable(true)]
		public short StaffTypeId 
		{
			get { return GetColumnValue<short>(Columns.StaffTypeId); }
			set { SetColumnValue(Columns.StaffTypeId, value); }
		}
		  
		[XmlAttribute("RankId")]
		[Bindable(true)]
		public short RankId 
		{
			get { return GetColumnValue<short>(Columns.RankId); }
			set { SetColumnValue(Columns.RankId, value); }
		}
		  
		[XmlAttribute("Uid")]
		[Bindable(true)]
		public string Uid 
		{
			get { return GetColumnValue<string>(Columns.Uid); }
			set { SetColumnValue(Columns.Uid, value); }
		}
		  
		[XmlAttribute("Status")]
		[Bindable(true)]
		public byte? Status 
		{
			get { return GetColumnValue<byte?>(Columns.Status); }
			set { SetColumnValue(Columns.Status, value); }
		}
		  
		[XmlAttribute("SDesc")]
		[Bindable(true)]
		public string SDesc 
		{
			get { return GetColumnValue<string>(Columns.SDesc); }
			set { SetColumnValue(Columns.SDesc, value); }
		}
		  
		[XmlAttribute("SpecMoney")]
		[Bindable(true)]
		public decimal SpecMoney 
		{
			get { return GetColumnValue<decimal>(Columns.SpecMoney); }
			set { SetColumnValue(Columns.SpecMoney, value); }
		}
		  
		[XmlAttribute("Actived")]
		[Bindable(true)]
		public byte? Actived 
		{
			get { return GetColumnValue<byte?>(Columns.Actived); }
			set { SetColumnValue(Columns.Actived, value); }
		}
		  
		[XmlAttribute("CanSign")]
		[Bindable(true)]
		public int? CanSign 
		{
			get { return GetColumnValue<int?>(Columns.CanSign); }
			set { SetColumnValue(Columns.CanSign, value); }
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
		  
		[XmlAttribute("ChoPhepChuyenGoi")]
		[Bindable(true)]
		public byte? ChoPhepChuyenGoi 
		{
			get { return GetColumnValue<byte?>(Columns.ChoPhepChuyenGoi); }
			set { SetColumnValue(Columns.ChoPhepChuyenGoi, value); }
		}
		  
		[XmlAttribute("ChoPhepDchinhTtoan")]
		[Bindable(true)]
		public byte? ChoPhepDchinhTtoan 
		{
			get { return GetColumnValue<byte?>(Columns.ChoPhepDchinhTtoan); }
			set { SetColumnValue(Columns.ChoPhepDchinhTtoan, value); }
		}
		  
		[XmlAttribute("ChoPhepHuyKhoat")]
		[Bindable(true)]
		public byte? ChoPhepHuyKhoat 
		{
			get { return GetColumnValue<byte?>(Columns.ChoPhepHuyKhoat); }
			set { SetColumnValue(Columns.ChoPhepHuyKhoat, value); }
		}
		  
		[XmlAttribute("ChoPhepKhoat")]
		[Bindable(true)]
		public byte? ChoPhepKhoat 
		{
			get { return GetColumnValue<byte?>(Columns.ChoPhepKhoat); }
			set { SetColumnValue(Columns.ChoPhepKhoat, value); }
		}
		  
		[XmlAttribute("ChoPhepNvien")]
		[Bindable(true)]
		public byte? ChoPhepNvien 
		{
			get { return GetColumnValue<byte?>(Columns.ChoPhepNvien); }
			set { SetColumnValue(Columns.ChoPhepNvien, value); }
		}
		  
		[XmlAttribute("ChoPhepNhapTheBhyt")]
		[Bindable(true)]
		public byte? ChoPhepNhapTheBhyt 
		{
			get { return GetColumnValue<byte?>(Columns.ChoPhepNhapTheBhyt); }
			set { SetColumnValue(Columns.ChoPhepNhapTheBhyt, value); }
		}
		  
		[XmlAttribute("ChoPhepSuaDlieu")]
		[Bindable(true)]
		public byte? ChoPhepSuaDlieu 
		{
			get { return GetColumnValue<byte?>(Columns.ChoPhepSuaDlieu); }
			set { SetColumnValue(Columns.ChoPhepSuaDlieu, value); }
		}
		  
		[XmlAttribute("ChoPhepChonBly")]
		[Bindable(true)]
		public byte? ChoPhepChonBly 
		{
			get { return GetColumnValue<byte?>(Columns.ChoPhepChonBly); }
			set { SetColumnValue(Columns.ChoPhepChonBly, value); }
		}
		  
		[XmlAttribute("ChoPhepDieuChinhBg")]
		[Bindable(true)]
		public byte? ChoPhepDieuChinhBg 
		{
			get { return GetColumnValue<byte?>(Columns.ChoPhepDieuChinhBg); }
			set { SetColumnValue(Columns.ChoPhepDieuChinhBg, value); }
		}
		  
		[XmlAttribute("MaQuyen")]
		[Bindable(true)]
		public string MaQuyen 
		{
			get { return GetColumnValue<string>(Columns.MaQuyen); }
			set { SetColumnValue(Columns.MaQuyen, value); }
		}
		  
		[XmlAttribute("SoThuTu")]
		[Bindable(true)]
		public int? SoThuTu 
		{
			get { return GetColumnValue<int?>(Columns.SoThuTu); }
			set { SetColumnValue(Columns.SoThuTu, value); }
		}
		  
		[XmlAttribute("ChoMGiam")]
		[Bindable(true)]
		public byte? ChoMGiam 
		{
			get { return GetColumnValue<byte?>(Columns.ChoMGiam); }
			set { SetColumnValue(Columns.ChoMGiam, value); }
		}
		  
		[XmlAttribute("IsUpdatePhieuDtri")]
		[Bindable(true)]
		public bool? IsUpdatePhieuDtri 
		{
			get { return GetColumnValue<bool?>(Columns.IsUpdatePhieuDtri); }
			set { SetColumnValue(Columns.IsUpdatePhieuDtri, value); }
		}
		  
		[XmlAttribute("IsGiayBhyt")]
		[Bindable(true)]
		public bool? IsGiayBhyt 
		{
			get { return GetColumnValue<bool?>(Columns.IsGiayBhyt); }
			set { SetColumnValue(Columns.IsGiayBhyt, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varStaffCode,string varStaffName,short varDepartmentId,int? varParentId,short varStaffTypeId,short varRankId,string varUid,byte? varStatus,string varSDesc,decimal varSpecMoney,byte? varActived,int? varCanSign,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua,byte? varChoPhepChuyenGoi,byte? varChoPhepDchinhTtoan,byte? varChoPhepHuyKhoat,byte? varChoPhepKhoat,byte? varChoPhepNvien,byte? varChoPhepNhapTheBhyt,byte? varChoPhepSuaDlieu,byte? varChoPhepChonBly,byte? varChoPhepDieuChinhBg,string varMaQuyen,int? varSoThuTu,byte? varChoMGiam,bool? varIsUpdatePhieuDtri,bool? varIsGiayBhyt)
		{
			LStaff item = new LStaff();
			
			item.StaffCode = varStaffCode;
			
			item.StaffName = varStaffName;
			
			item.DepartmentId = varDepartmentId;
			
			item.ParentId = varParentId;
			
			item.StaffTypeId = varStaffTypeId;
			
			item.RankId = varRankId;
			
			item.Uid = varUid;
			
			item.Status = varStatus;
			
			item.SDesc = varSDesc;
			
			item.SpecMoney = varSpecMoney;
			
			item.Actived = varActived;
			
			item.CanSign = varCanSign;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiSua = varNguoiSua;
			
			item.NgaySua = varNgaySua;
			
			item.ChoPhepChuyenGoi = varChoPhepChuyenGoi;
			
			item.ChoPhepDchinhTtoan = varChoPhepDchinhTtoan;
			
			item.ChoPhepHuyKhoat = varChoPhepHuyKhoat;
			
			item.ChoPhepKhoat = varChoPhepKhoat;
			
			item.ChoPhepNvien = varChoPhepNvien;
			
			item.ChoPhepNhapTheBhyt = varChoPhepNhapTheBhyt;
			
			item.ChoPhepSuaDlieu = varChoPhepSuaDlieu;
			
			item.ChoPhepChonBly = varChoPhepChonBly;
			
			item.ChoPhepDieuChinhBg = varChoPhepDieuChinhBg;
			
			item.MaQuyen = varMaQuyen;
			
			item.SoThuTu = varSoThuTu;
			
			item.ChoMGiam = varChoMGiam;
			
			item.IsUpdatePhieuDtri = varIsUpdatePhieuDtri;
			
			item.IsGiayBhyt = varIsGiayBhyt;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(short varStaffId,string varStaffCode,string varStaffName,short varDepartmentId,int? varParentId,short varStaffTypeId,short varRankId,string varUid,byte? varStatus,string varSDesc,decimal varSpecMoney,byte? varActived,int? varCanSign,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua,byte? varChoPhepChuyenGoi,byte? varChoPhepDchinhTtoan,byte? varChoPhepHuyKhoat,byte? varChoPhepKhoat,byte? varChoPhepNvien,byte? varChoPhepNhapTheBhyt,byte? varChoPhepSuaDlieu,byte? varChoPhepChonBly,byte? varChoPhepDieuChinhBg,string varMaQuyen,int? varSoThuTu,byte? varChoMGiam,bool? varIsUpdatePhieuDtri,bool? varIsGiayBhyt)
		{
			LStaff item = new LStaff();
			
				item.StaffId = varStaffId;
			
				item.StaffCode = varStaffCode;
			
				item.StaffName = varStaffName;
			
				item.DepartmentId = varDepartmentId;
			
				item.ParentId = varParentId;
			
				item.StaffTypeId = varStaffTypeId;
			
				item.RankId = varRankId;
			
				item.Uid = varUid;
			
				item.Status = varStatus;
			
				item.SDesc = varSDesc;
			
				item.SpecMoney = varSpecMoney;
			
				item.Actived = varActived;
			
				item.CanSign = varCanSign;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiSua = varNguoiSua;
			
				item.NgaySua = varNgaySua;
			
				item.ChoPhepChuyenGoi = varChoPhepChuyenGoi;
			
				item.ChoPhepDchinhTtoan = varChoPhepDchinhTtoan;
			
				item.ChoPhepHuyKhoat = varChoPhepHuyKhoat;
			
				item.ChoPhepKhoat = varChoPhepKhoat;
			
				item.ChoPhepNvien = varChoPhepNvien;
			
				item.ChoPhepNhapTheBhyt = varChoPhepNhapTheBhyt;
			
				item.ChoPhepSuaDlieu = varChoPhepSuaDlieu;
			
				item.ChoPhepChonBly = varChoPhepChonBly;
			
				item.ChoPhepDieuChinhBg = varChoPhepDieuChinhBg;
			
				item.MaQuyen = varMaQuyen;
			
				item.SoThuTu = varSoThuTu;
			
				item.ChoMGiam = varChoMGiam;
			
				item.IsUpdatePhieuDtri = varIsUpdatePhieuDtri;
			
				item.IsGiayBhyt = varIsGiayBhyt;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn StaffIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn StaffCodeColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn StaffNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn DepartmentIdColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn ParentIdColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn StaffTypeIdColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn RankIdColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn UidColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn StatusColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn SDescColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn SpecMoneyColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn ActivedColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn CanSignColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn ChoPhepChuyenGoiColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn ChoPhepDchinhTtoanColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn ChoPhepHuyKhoatColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn ChoPhepKhoatColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn ChoPhepNvienColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn ChoPhepNhapTheBhytColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        public static TableSchema.TableColumn ChoPhepSuaDlieuColumn
        {
            get { return Schema.Columns[23]; }
        }
        
        
        
        public static TableSchema.TableColumn ChoPhepChonBlyColumn
        {
            get { return Schema.Columns[24]; }
        }
        
        
        
        public static TableSchema.TableColumn ChoPhepDieuChinhBgColumn
        {
            get { return Schema.Columns[25]; }
        }
        
        
        
        public static TableSchema.TableColumn MaQuyenColumn
        {
            get { return Schema.Columns[26]; }
        }
        
        
        
        public static TableSchema.TableColumn SoThuTuColumn
        {
            get { return Schema.Columns[27]; }
        }
        
        
        
        public static TableSchema.TableColumn ChoMGiamColumn
        {
            get { return Schema.Columns[28]; }
        }
        
        
        
        public static TableSchema.TableColumn IsUpdatePhieuDtriColumn
        {
            get { return Schema.Columns[29]; }
        }
        
        
        
        public static TableSchema.TableColumn IsGiayBhytColumn
        {
            get { return Schema.Columns[30]; }
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
			 public static string SoThuTu = @"SO_THU_TU";
			 public static string ChoMGiam = @"Cho_MGiam";
			 public static string IsUpdatePhieuDtri = @"IsUpdatePhieuDtri";
			 public static string IsGiayBhyt = @"Is_Giay_Bhyt";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
