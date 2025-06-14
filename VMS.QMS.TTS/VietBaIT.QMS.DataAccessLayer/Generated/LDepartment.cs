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
	/// Strongly-typed collection for the LDepartment class.
	/// </summary>
    [Serializable]
	public partial class LDepartmentCollection : ActiveList<LDepartment, LDepartmentCollection>
	{	   
		public LDepartmentCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LDepartmentCollection</returns>
		public LDepartmentCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LDepartment o = this[i];
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
	/// This is an ActiveRecord class which wraps the L_Departments table.
	/// </summary>
	[Serializable]
	public partial class LDepartment : ActiveRecord<LDepartment>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LDepartment()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LDepartment(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LDepartment(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LDepartment(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("L_Departments", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarDepartmentId = new TableSchema.TableColumn(schema);
				colvarDepartmentId.ColumnName = "Department_ID";
				colvarDepartmentId.DataType = DbType.Int16;
				colvarDepartmentId.MaxLength = 0;
				colvarDepartmentId.AutoIncrement = true;
				colvarDepartmentId.IsNullable = false;
				colvarDepartmentId.IsPrimaryKey = true;
				colvarDepartmentId.IsForeignKey = false;
				colvarDepartmentId.IsReadOnly = false;
				colvarDepartmentId.DefaultSetting = @"";
				colvarDepartmentId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDepartmentId);
				
				TableSchema.TableColumn colvarDepartmentCode = new TableSchema.TableColumn(schema);
				colvarDepartmentCode.ColumnName = "Department_Code";
				colvarDepartmentCode.DataType = DbType.String;
				colvarDepartmentCode.MaxLength = 20;
				colvarDepartmentCode.AutoIncrement = false;
				colvarDepartmentCode.IsNullable = true;
				colvarDepartmentCode.IsPrimaryKey = false;
				colvarDepartmentCode.IsForeignKey = false;
				colvarDepartmentCode.IsReadOnly = false;
				colvarDepartmentCode.DefaultSetting = @"";
				colvarDepartmentCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDepartmentCode);
				
				TableSchema.TableColumn colvarDepartmentName = new TableSchema.TableColumn(schema);
				colvarDepartmentName.ColumnName = "Department_Name";
				colvarDepartmentName.DataType = DbType.String;
				colvarDepartmentName.MaxLength = 100;
				colvarDepartmentName.AutoIncrement = false;
				colvarDepartmentName.IsNullable = false;
				colvarDepartmentName.IsPrimaryKey = false;
				colvarDepartmentName.IsForeignKey = false;
				colvarDepartmentName.IsReadOnly = false;
				colvarDepartmentName.DefaultSetting = @"";
				colvarDepartmentName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDepartmentName);
				
				TableSchema.TableColumn colvarParentId = new TableSchema.TableColumn(schema);
				colvarParentId.ColumnName = "Parent_ID";
				colvarParentId.DataType = DbType.Int16;
				colvarParentId.MaxLength = 0;
				colvarParentId.AutoIncrement = false;
				colvarParentId.IsNullable = true;
				colvarParentId.IsPrimaryKey = false;
				colvarParentId.IsForeignKey = false;
				colvarParentId.IsReadOnly = false;
				
						colvarParentId.DefaultSetting = @"((0))";
				colvarParentId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarParentId);
				
				TableSchema.TableColumn colvarSpeciality = new TableSchema.TableColumn(schema);
				colvarSpeciality.ColumnName = "Speciality";
				colvarSpeciality.DataType = DbType.Byte;
				colvarSpeciality.MaxLength = 0;
				colvarSpeciality.AutoIncrement = false;
				colvarSpeciality.IsNullable = false;
				colvarSpeciality.IsPrimaryKey = false;
				colvarSpeciality.IsForeignKey = false;
				colvarSpeciality.IsReadOnly = false;
				colvarSpeciality.DefaultSetting = @"";
				colvarSpeciality.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSpeciality);
				
				TableSchema.TableColumn colvarIntOrder = new TableSchema.TableColumn(schema);
				colvarIntOrder.ColumnName = "intOrder";
				colvarIntOrder.DataType = DbType.Int16;
				colvarIntOrder.MaxLength = 0;
				colvarIntOrder.AutoIncrement = false;
				colvarIntOrder.IsNullable = false;
				colvarIntOrder.IsPrimaryKey = false;
				colvarIntOrder.IsForeignKey = false;
				colvarIntOrder.IsReadOnly = false;
				colvarIntOrder.DefaultSetting = @"";
				colvarIntOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIntOrder);
				
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
				
				TableSchema.TableColumn colvarDeptType = new TableSchema.TableColumn(schema);
				colvarDeptType.ColumnName = "Dept_Type";
				colvarDeptType.DataType = DbType.Int32;
				colvarDeptType.MaxLength = 0;
				colvarDeptType.AutoIncrement = false;
				colvarDeptType.IsNullable = true;
				colvarDeptType.IsPrimaryKey = false;
				colvarDeptType.IsForeignKey = false;
				colvarDeptType.IsReadOnly = false;
				colvarDeptType.DefaultSetting = @"";
				colvarDeptType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeptType);
				
				TableSchema.TableColumn colvarDeptFee = new TableSchema.TableColumn(schema);
				colvarDeptFee.ColumnName = "Dept_Fee";
				colvarDeptFee.DataType = DbType.Currency;
				colvarDeptFee.MaxLength = 0;
				colvarDeptFee.AutoIncrement = false;
				colvarDeptFee.IsNullable = true;
				colvarDeptFee.IsPrimaryKey = false;
				colvarDeptFee.IsForeignKey = false;
				colvarDeptFee.IsReadOnly = false;
				colvarDeptFee.DefaultSetting = @"";
				colvarDeptFee.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeptFee);
				
				TableSchema.TableColumn colvarLoaiTamUng = new TableSchema.TableColumn(schema);
				colvarLoaiTamUng.ColumnName = "LOAI_TAM_UNG";
				colvarLoaiTamUng.DataType = DbType.String;
				colvarLoaiTamUng.MaxLength = 50;
				colvarLoaiTamUng.AutoIncrement = false;
				colvarLoaiTamUng.IsNullable = true;
				colvarLoaiTamUng.IsPrimaryKey = false;
				colvarLoaiTamUng.IsForeignKey = false;
				colvarLoaiTamUng.IsReadOnly = false;
				colvarLoaiTamUng.DefaultSetting = @"";
				colvarLoaiTamUng.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoaiTamUng);
				
				TableSchema.TableColumn colvarTienTamUng = new TableSchema.TableColumn(schema);
				colvarTienTamUng.ColumnName = "TIEN_TAM_UNG";
				colvarTienTamUng.DataType = DbType.Decimal;
				colvarTienTamUng.MaxLength = 0;
				colvarTienTamUng.AutoIncrement = false;
				colvarTienTamUng.IsNullable = true;
				colvarTienTamUng.IsPrimaryKey = false;
				colvarTienTamUng.IsForeignKey = false;
				colvarTienTamUng.IsReadOnly = false;
				colvarTienTamUng.DefaultSetting = @"";
				colvarTienTamUng.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTienTamUng);
				
				TableSchema.TableColumn colvarKieuKphong = new TableSchema.TableColumn(schema);
				colvarKieuKphong.ColumnName = "KIEU_KPHONG";
				colvarKieuKphong.DataType = DbType.String;
				colvarKieuKphong.MaxLength = 50;
				colvarKieuKphong.AutoIncrement = false;
				colvarKieuKphong.IsNullable = true;
				colvarKieuKphong.IsPrimaryKey = false;
				colvarKieuKphong.IsForeignKey = false;
				colvarKieuKphong.IsReadOnly = false;
				colvarKieuKphong.DefaultSetting = @"";
				colvarKieuKphong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKieuKphong);
				
				TableSchema.TableColumn colvarKhoaCapcuu = new TableSchema.TableColumn(schema);
				colvarKhoaCapcuu.ColumnName = "KHOA_CAPCUU";
				colvarKhoaCapcuu.DataType = DbType.Int32;
				colvarKhoaCapcuu.MaxLength = 0;
				colvarKhoaCapcuu.AutoIncrement = false;
				colvarKhoaCapcuu.IsNullable = true;
				colvarKhoaCapcuu.IsPrimaryKey = false;
				colvarKhoaCapcuu.IsForeignKey = false;
				colvarKhoaCapcuu.IsReadOnly = false;
				colvarKhoaCapcuu.DefaultSetting = @"";
				colvarKhoaCapcuu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKhoaCapcuu);
				
				TableSchema.TableColumn colvarLoaiKhoa = new TableSchema.TableColumn(schema);
				colvarLoaiKhoa.ColumnName = "LOAI_KHOA";
				colvarLoaiKhoa.DataType = DbType.String;
				colvarLoaiKhoa.MaxLength = 50;
				colvarLoaiKhoa.AutoIncrement = false;
				colvarLoaiKhoa.IsNullable = true;
				colvarLoaiKhoa.IsPrimaryKey = false;
				colvarLoaiKhoa.IsForeignKey = false;
				colvarLoaiKhoa.IsReadOnly = false;
				colvarLoaiKhoa.DefaultSetting = @"";
				colvarLoaiKhoa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoaiKhoa);
				
				TableSchema.TableColumn colvarUnitId = new TableSchema.TableColumn(schema);
				colvarUnitId.ColumnName = "Unit_ID";
				colvarUnitId.DataType = DbType.Int32;
				colvarUnitId.MaxLength = 0;
				colvarUnitId.AutoIncrement = false;
				colvarUnitId.IsNullable = true;
				colvarUnitId.IsPrimaryKey = false;
				colvarUnitId.IsForeignKey = false;
				colvarUnitId.IsReadOnly = false;
				colvarUnitId.DefaultSetting = @"";
				colvarUnitId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUnitId);
				
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
				
				TableSchema.TableColumn colvarPhongThien = new TableSchema.TableColumn(schema);
				colvarPhongThien.ColumnName = "PHONG_THIEN";
				colvarPhongThien.DataType = DbType.String;
				colvarPhongThien.MaxLength = 100;
				colvarPhongThien.AutoIncrement = false;
				colvarPhongThien.IsNullable = true;
				colvarPhongThien.IsPrimaryKey = false;
				colvarPhongThien.IsForeignKey = false;
				colvarPhongThien.IsReadOnly = false;
				colvarPhongThien.DefaultSetting = @"";
				colvarPhongThien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhongThien);
				
				TableSchema.TableColumn colvarMaPhongStt = new TableSchema.TableColumn(schema);
				colvarMaPhongStt.ColumnName = "MA_PHONG_STT";
				colvarMaPhongStt.DataType = DbType.String;
				colvarMaPhongStt.MaxLength = 50;
				colvarMaPhongStt.AutoIncrement = false;
				colvarMaPhongStt.IsNullable = true;
				colvarMaPhongStt.IsPrimaryKey = false;
				colvarMaPhongStt.IsForeignKey = false;
				colvarMaPhongStt.IsReadOnly = false;
				colvarMaPhongStt.DefaultSetting = @"";
				colvarMaPhongStt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaPhongStt);
				
				TableSchema.TableColumn colvarHienThi = new TableSchema.TableColumn(schema);
				colvarHienThi.ColumnName = "Hien_Thi";
				colvarHienThi.DataType = DbType.Byte;
				colvarHienThi.MaxLength = 0;
				colvarHienThi.AutoIncrement = false;
				colvarHienThi.IsNullable = true;
				colvarHienThi.IsPrimaryKey = false;
				colvarHienThi.IsForeignKey = false;
				colvarHienThi.IsReadOnly = false;
				
						colvarHienThi.DefaultSetting = @"((1))";
				colvarHienThi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHienThi);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("L_Departments",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("DepartmentId")]
		[Bindable(true)]
		public short DepartmentId 
		{
			get { return GetColumnValue<short>(Columns.DepartmentId); }
			set { SetColumnValue(Columns.DepartmentId, value); }
		}
		  
		[XmlAttribute("DepartmentCode")]
		[Bindable(true)]
		public string DepartmentCode 
		{
			get { return GetColumnValue<string>(Columns.DepartmentCode); }
			set { SetColumnValue(Columns.DepartmentCode, value); }
		}
		  
		[XmlAttribute("DepartmentName")]
		[Bindable(true)]
		public string DepartmentName 
		{
			get { return GetColumnValue<string>(Columns.DepartmentName); }
			set { SetColumnValue(Columns.DepartmentName, value); }
		}
		  
		[XmlAttribute("ParentId")]
		[Bindable(true)]
		public short? ParentId 
		{
			get { return GetColumnValue<short?>(Columns.ParentId); }
			set { SetColumnValue(Columns.ParentId, value); }
		}
		  
		[XmlAttribute("Speciality")]
		[Bindable(true)]
		public byte Speciality 
		{
			get { return GetColumnValue<byte>(Columns.Speciality); }
			set { SetColumnValue(Columns.Speciality, value); }
		}
		  
		[XmlAttribute("IntOrder")]
		[Bindable(true)]
		public short IntOrder 
		{
			get { return GetColumnValue<short>(Columns.IntOrder); }
			set { SetColumnValue(Columns.IntOrder, value); }
		}
		  
		[XmlAttribute("SDesc")]
		[Bindable(true)]
		public string SDesc 
		{
			get { return GetColumnValue<string>(Columns.SDesc); }
			set { SetColumnValue(Columns.SDesc, value); }
		}
		  
		[XmlAttribute("DeptType")]
		[Bindable(true)]
		public int? DeptType 
		{
			get { return GetColumnValue<int?>(Columns.DeptType); }
			set { SetColumnValue(Columns.DeptType, value); }
		}
		  
		[XmlAttribute("DeptFee")]
		[Bindable(true)]
		public decimal? DeptFee 
		{
			get { return GetColumnValue<decimal?>(Columns.DeptFee); }
			set { SetColumnValue(Columns.DeptFee, value); }
		}
		  
		[XmlAttribute("LoaiTamUng")]
		[Bindable(true)]
		public string LoaiTamUng 
		{
			get { return GetColumnValue<string>(Columns.LoaiTamUng); }
			set { SetColumnValue(Columns.LoaiTamUng, value); }
		}
		  
		[XmlAttribute("TienTamUng")]
		[Bindable(true)]
		public decimal? TienTamUng 
		{
			get { return GetColumnValue<decimal?>(Columns.TienTamUng); }
			set { SetColumnValue(Columns.TienTamUng, value); }
		}
		  
		[XmlAttribute("KieuKphong")]
		[Bindable(true)]
		public string KieuKphong 
		{
			get { return GetColumnValue<string>(Columns.KieuKphong); }
			set { SetColumnValue(Columns.KieuKphong, value); }
		}
		  
		[XmlAttribute("KhoaCapcuu")]
		[Bindable(true)]
		public int? KhoaCapcuu 
		{
			get { return GetColumnValue<int?>(Columns.KhoaCapcuu); }
			set { SetColumnValue(Columns.KhoaCapcuu, value); }
		}
		  
		[XmlAttribute("LoaiKhoa")]
		[Bindable(true)]
		public string LoaiKhoa 
		{
			get { return GetColumnValue<string>(Columns.LoaiKhoa); }
			set { SetColumnValue(Columns.LoaiKhoa, value); }
		}
		  
		[XmlAttribute("UnitId")]
		[Bindable(true)]
		public int? UnitId 
		{
			get { return GetColumnValue<int?>(Columns.UnitId); }
			set { SetColumnValue(Columns.UnitId, value); }
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
		  
		[XmlAttribute("PhongThien")]
		[Bindable(true)]
		public string PhongThien 
		{
			get { return GetColumnValue<string>(Columns.PhongThien); }
			set { SetColumnValue(Columns.PhongThien, value); }
		}
		  
		[XmlAttribute("MaPhongStt")]
		[Bindable(true)]
		public string MaPhongStt 
		{
			get { return GetColumnValue<string>(Columns.MaPhongStt); }
			set { SetColumnValue(Columns.MaPhongStt, value); }
		}
		  
		[XmlAttribute("HienThi")]
		[Bindable(true)]
		public byte? HienThi 
		{
			get { return GetColumnValue<byte?>(Columns.HienThi); }
			set { SetColumnValue(Columns.HienThi, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varDepartmentCode,string varDepartmentName,short? varParentId,byte varSpeciality,short varIntOrder,string varSDesc,int? varDeptType,decimal? varDeptFee,string varLoaiTamUng,decimal? varTienTamUng,string varKieuKphong,int? varKhoaCapcuu,string varLoaiKhoa,int? varUnitId,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua,string varPhongThien,string varMaPhongStt,byte? varHienThi)
		{
			LDepartment item = new LDepartment();
			
			item.DepartmentCode = varDepartmentCode;
			
			item.DepartmentName = varDepartmentName;
			
			item.ParentId = varParentId;
			
			item.Speciality = varSpeciality;
			
			item.IntOrder = varIntOrder;
			
			item.SDesc = varSDesc;
			
			item.DeptType = varDeptType;
			
			item.DeptFee = varDeptFee;
			
			item.LoaiTamUng = varLoaiTamUng;
			
			item.TienTamUng = varTienTamUng;
			
			item.KieuKphong = varKieuKphong;
			
			item.KhoaCapcuu = varKhoaCapcuu;
			
			item.LoaiKhoa = varLoaiKhoa;
			
			item.UnitId = varUnitId;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiSua = varNguoiSua;
			
			item.NgaySua = varNgaySua;
			
			item.PhongThien = varPhongThien;
			
			item.MaPhongStt = varMaPhongStt;
			
			item.HienThi = varHienThi;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(short varDepartmentId,string varDepartmentCode,string varDepartmentName,short? varParentId,byte varSpeciality,short varIntOrder,string varSDesc,int? varDeptType,decimal? varDeptFee,string varLoaiTamUng,decimal? varTienTamUng,string varKieuKphong,int? varKhoaCapcuu,string varLoaiKhoa,int? varUnitId,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua,string varPhongThien,string varMaPhongStt,byte? varHienThi)
		{
			LDepartment item = new LDepartment();
			
				item.DepartmentId = varDepartmentId;
			
				item.DepartmentCode = varDepartmentCode;
			
				item.DepartmentName = varDepartmentName;
			
				item.ParentId = varParentId;
			
				item.Speciality = varSpeciality;
			
				item.IntOrder = varIntOrder;
			
				item.SDesc = varSDesc;
			
				item.DeptType = varDeptType;
			
				item.DeptFee = varDeptFee;
			
				item.LoaiTamUng = varLoaiTamUng;
			
				item.TienTamUng = varTienTamUng;
			
				item.KieuKphong = varKieuKphong;
			
				item.KhoaCapcuu = varKhoaCapcuu;
			
				item.LoaiKhoa = varLoaiKhoa;
			
				item.UnitId = varUnitId;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiSua = varNguoiSua;
			
				item.NgaySua = varNgaySua;
			
				item.PhongThien = varPhongThien;
			
				item.MaPhongStt = varMaPhongStt;
			
				item.HienThi = varHienThi;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn DepartmentIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn DepartmentCodeColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn DepartmentNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn ParentIdColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SpecialityColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn IntOrderColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn SDescColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn DeptTypeColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn DeptFeeColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiTamUngColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn TienTamUngColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn KieuKphongColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn KhoaCapcuuColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiKhoaColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn UnitIdColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn PhongThienColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn MaPhongSttColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn HienThiColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string DepartmentId = @"Department_ID";
			 public static string DepartmentCode = @"Department_Code";
			 public static string DepartmentName = @"Department_Name";
			 public static string ParentId = @"Parent_ID";
			 public static string Speciality = @"Speciality";
			 public static string IntOrder = @"intOrder";
			 public static string SDesc = @"sDesc";
			 public static string DeptType = @"Dept_Type";
			 public static string DeptFee = @"Dept_Fee";
			 public static string LoaiTamUng = @"LOAI_TAM_UNG";
			 public static string TienTamUng = @"TIEN_TAM_UNG";
			 public static string KieuKphong = @"KIEU_KPHONG";
			 public static string KhoaCapcuu = @"KHOA_CAPCUU";
			 public static string LoaiKhoa = @"LOAI_KHOA";
			 public static string UnitId = @"Unit_ID";
			 public static string NguoiTao = @"NGUOI_TAO";
			 public static string NgayTao = @"NGAY_TAO";
			 public static string NguoiSua = @"NGUOI_SUA";
			 public static string NgaySua = @"NGAY_SUA";
			 public static string PhongThien = @"PHONG_THIEN";
			 public static string MaPhongStt = @"MA_PHONG_STT";
			 public static string HienThi = @"Hien_Thi";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
