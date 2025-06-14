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
	/// Strongly-typed collection for the TKetquaChitiet class.
	/// </summary>
    [Serializable]
	public partial class TKetquaChitietCollection : ActiveList<TKetquaChitiet, TKetquaChitietCollection>
	{	   
		public TKetquaChitietCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TKetquaChitietCollection</returns>
		public TKetquaChitietCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TKetquaChitiet o = this[i];
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
	/// This is an ActiveRecord class which wraps the T_KETQUA_CHITIET table.
	/// </summary>
	[Serializable]
	public partial class TKetquaChitiet : ActiveRecord<TKetquaChitiet>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TKetquaChitiet()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TKetquaChitiet(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TKetquaChitiet(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TKetquaChitiet(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("T_KETQUA_CHITIET", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdKqChiTiet = new TableSchema.TableColumn(schema);
				colvarIdKqChiTiet.ColumnName = "ID_KQ_ChiTiet";
				colvarIdKqChiTiet.DataType = DbType.Int64;
				colvarIdKqChiTiet.MaxLength = 0;
				colvarIdKqChiTiet.AutoIncrement = true;
				colvarIdKqChiTiet.IsNullable = false;
				colvarIdKqChiTiet.IsPrimaryKey = true;
				colvarIdKqChiTiet.IsForeignKey = false;
				colvarIdKqChiTiet.IsReadOnly = false;
				colvarIdKqChiTiet.DefaultSetting = @"";
				colvarIdKqChiTiet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKqChiTiet);
				
				TableSchema.TableColumn colvarAssignCode = new TableSchema.TableColumn(schema);
				colvarAssignCode.ColumnName = "Assign_Code";
				colvarAssignCode.DataType = DbType.String;
				colvarAssignCode.MaxLength = 50;
				colvarAssignCode.AutoIncrement = false;
				colvarAssignCode.IsNullable = true;
				colvarAssignCode.IsPrimaryKey = false;
				colvarAssignCode.IsForeignKey = false;
				colvarAssignCode.IsReadOnly = false;
				colvarAssignCode.DefaultSetting = @"";
				colvarAssignCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAssignCode);
				
				TableSchema.TableColumn colvarIdChiDinh = new TableSchema.TableColumn(schema);
				colvarIdChiDinh.ColumnName = "ID_CHI_DINH";
				colvarIdChiDinh.DataType = DbType.Int32;
				colvarIdChiDinh.MaxLength = 0;
				colvarIdChiDinh.AutoIncrement = false;
				colvarIdChiDinh.IsNullable = false;
				colvarIdChiDinh.IsPrimaryKey = false;
				colvarIdChiDinh.IsForeignKey = false;
				colvarIdChiDinh.IsReadOnly = false;
				colvarIdChiDinh.DefaultSetting = @"";
				colvarIdChiDinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChiDinh);
				
				TableSchema.TableColumn colvarBarCode = new TableSchema.TableColumn(schema);
				colvarBarCode.ColumnName = "BarCode";
				colvarBarCode.DataType = DbType.AnsiString;
				colvarBarCode.MaxLength = 50;
				colvarBarCode.AutoIncrement = false;
				colvarBarCode.IsNullable = true;
				colvarBarCode.IsPrimaryKey = false;
				colvarBarCode.IsForeignKey = false;
				colvarBarCode.IsReadOnly = false;
				colvarBarCode.DefaultSetting = @"";
				colvarBarCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBarCode);
				
				TableSchema.TableColumn colvarSoThuTuIn = new TableSchema.TableColumn(schema);
				colvarSoThuTuIn.ColumnName = "So_Thu_Tu_In";
				colvarSoThuTuIn.DataType = DbType.Int32;
				colvarSoThuTuIn.MaxLength = 0;
				colvarSoThuTuIn.AutoIncrement = false;
				colvarSoThuTuIn.IsNullable = true;
				colvarSoThuTuIn.IsPrimaryKey = false;
				colvarSoThuTuIn.IsForeignKey = false;
				colvarSoThuTuIn.IsReadOnly = false;
				colvarSoThuTuIn.DefaultSetting = @"";
				colvarSoThuTuIn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoThuTuIn);
				
				TableSchema.TableColumn colvarKetQua = new TableSchema.TableColumn(schema);
				colvarKetQua.ColumnName = "Ket_Qua";
				colvarKetQua.DataType = DbType.String;
				colvarKetQua.MaxLength = 200;
				colvarKetQua.AutoIncrement = false;
				colvarKetQua.IsNullable = true;
				colvarKetQua.IsPrimaryKey = false;
				colvarKetQua.IsForeignKey = false;
				colvarKetQua.IsReadOnly = false;
				colvarKetQua.DefaultSetting = @"";
				colvarKetQua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKetQua);
				
				TableSchema.TableColumn colvarTbNu = new TableSchema.TableColumn(schema);
				colvarTbNu.ColumnName = "TB_Nu";
				colvarTbNu.DataType = DbType.String;
				colvarTbNu.MaxLength = 200;
				colvarTbNu.AutoIncrement = false;
				colvarTbNu.IsNullable = true;
				colvarTbNu.IsPrimaryKey = false;
				colvarTbNu.IsForeignKey = false;
				colvarTbNu.IsReadOnly = false;
				colvarTbNu.DefaultSetting = @"";
				colvarTbNu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTbNu);
				
				TableSchema.TableColumn colvarTbNam = new TableSchema.TableColumn(schema);
				colvarTbNam.ColumnName = "TB_Nam";
				colvarTbNam.DataType = DbType.String;
				colvarTbNam.MaxLength = 200;
				colvarTbNam.AutoIncrement = false;
				colvarTbNam.IsNullable = true;
				colvarTbNam.IsPrimaryKey = false;
				colvarTbNam.IsForeignKey = false;
				colvarTbNam.IsReadOnly = false;
				colvarTbNam.DefaultSetting = @"";
				colvarTbNam.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTbNam);
				
				TableSchema.TableColumn colvarDonVi = new TableSchema.TableColumn(schema);
				colvarDonVi.ColumnName = "Don_Vi";
				colvarDonVi.DataType = DbType.String;
				colvarDonVi.MaxLength = 50;
				colvarDonVi.AutoIncrement = false;
				colvarDonVi.IsNullable = true;
				colvarDonVi.IsPrimaryKey = false;
				colvarDonVi.IsForeignKey = false;
				colvarDonVi.IsReadOnly = false;
				colvarDonVi.DefaultSetting = @"";
				colvarDonVi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDonVi);
				
				TableSchema.TableColumn colvarTenThongSo = new TableSchema.TableColumn(schema);
				colvarTenThongSo.ColumnName = "Ten_Thong_So";
				colvarTenThongSo.DataType = DbType.String;
				colvarTenThongSo.MaxLength = 50;
				colvarTenThongSo.AutoIncrement = false;
				colvarTenThongSo.IsNullable = true;
				colvarTenThongSo.IsPrimaryKey = false;
				colvarTenThongSo.IsForeignKey = false;
				colvarTenThongSo.IsReadOnly = false;
				colvarTenThongSo.DefaultSetting = @"";
				colvarTenThongSo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenThongSo);
				
				TableSchema.TableColumn colvarTenKq = new TableSchema.TableColumn(schema);
				colvarTenKq.ColumnName = "Ten_KQ";
				colvarTenKq.DataType = DbType.String;
				colvarTenKq.MaxLength = 50;
				colvarTenKq.AutoIncrement = false;
				colvarTenKq.IsNullable = true;
				colvarTenKq.IsPrimaryKey = false;
				colvarTenKq.IsForeignKey = false;
				colvarTenKq.IsReadOnly = false;
				colvarTenKq.DefaultSetting = @"";
				colvarTenKq.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenKq);
				
				TableSchema.TableColumn colvarLoaiKq = new TableSchema.TableColumn(schema);
				colvarLoaiKq.ColumnName = "Loai_KQ";
				colvarLoaiKq.DataType = DbType.Int32;
				colvarLoaiKq.MaxLength = 0;
				colvarLoaiKq.AutoIncrement = false;
				colvarLoaiKq.IsNullable = true;
				colvarLoaiKq.IsPrimaryKey = false;
				colvarLoaiKq.IsForeignKey = false;
				colvarLoaiKq.IsReadOnly = false;
				
						colvarLoaiKq.DefaultSetting = @"((0))";
				colvarLoaiKq.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoaiKq);
				
				TableSchema.TableColumn colvarChoPhepHienThi = new TableSchema.TableColumn(schema);
				colvarChoPhepHienThi.ColumnName = "Cho_Phep_Hien_Thi";
				colvarChoPhepHienThi.DataType = DbType.Int32;
				colvarChoPhepHienThi.MaxLength = 0;
				colvarChoPhepHienThi.AutoIncrement = false;
				colvarChoPhepHienThi.IsNullable = true;
				colvarChoPhepHienThi.IsPrimaryKey = false;
				colvarChoPhepHienThi.IsForeignKey = false;
				colvarChoPhepHienThi.IsReadOnly = false;
				colvarChoPhepHienThi.DefaultSetting = @"";
				colvarChoPhepHienThi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChoPhepHienThi);
				
				TableSchema.TableColumn colvarChoPhepIn = new TableSchema.TableColumn(schema);
				colvarChoPhepIn.ColumnName = "Cho_Phep_In";
				colvarChoPhepIn.DataType = DbType.Int32;
				colvarChoPhepIn.MaxLength = 0;
				colvarChoPhepIn.AutoIncrement = false;
				colvarChoPhepIn.IsNullable = true;
				colvarChoPhepIn.IsPrimaryKey = false;
				colvarChoPhepIn.IsForeignKey = false;
				colvarChoPhepIn.IsReadOnly = false;
				colvarChoPhepIn.DefaultSetting = @"";
				colvarChoPhepIn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChoPhepIn);
				
				TableSchema.TableColumn colvarGhiChu = new TableSchema.TableColumn(schema);
				colvarGhiChu.ColumnName = "Ghi_Chu";
				colvarGhiChu.DataType = DbType.String;
				colvarGhiChu.MaxLength = 100;
				colvarGhiChu.AutoIncrement = false;
				colvarGhiChu.IsNullable = true;
				colvarGhiChu.IsPrimaryKey = false;
				colvarGhiChu.IsForeignKey = false;
				colvarGhiChu.IsReadOnly = false;
				colvarGhiChu.DefaultSetting = @"";
				colvarGhiChu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGhiChu);
				
				TableSchema.TableColumn colvarLoaiXn = new TableSchema.TableColumn(schema);
				colvarLoaiXn.ColumnName = "Loai_XN";
				colvarLoaiXn.DataType = DbType.Int32;
				colvarLoaiXn.MaxLength = 0;
				colvarLoaiXn.AutoIncrement = false;
				colvarLoaiXn.IsNullable = true;
				colvarLoaiXn.IsPrimaryKey = false;
				colvarLoaiXn.IsForeignKey = false;
				colvarLoaiXn.IsReadOnly = false;
				colvarLoaiXn.DefaultSetting = @"";
				colvarLoaiXn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoaiXn);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("T_KETQUA_CHITIET",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdKqChiTiet")]
		[Bindable(true)]
		public long IdKqChiTiet 
		{
			get { return GetColumnValue<long>(Columns.IdKqChiTiet); }
			set { SetColumnValue(Columns.IdKqChiTiet, value); }
		}
		  
		[XmlAttribute("AssignCode")]
		[Bindable(true)]
		public string AssignCode 
		{
			get { return GetColumnValue<string>(Columns.AssignCode); }
			set { SetColumnValue(Columns.AssignCode, value); }
		}
		  
		[XmlAttribute("IdChiDinh")]
		[Bindable(true)]
		public int IdChiDinh 
		{
			get { return GetColumnValue<int>(Columns.IdChiDinh); }
			set { SetColumnValue(Columns.IdChiDinh, value); }
		}
		  
		[XmlAttribute("BarCode")]
		[Bindable(true)]
		public string BarCode 
		{
			get { return GetColumnValue<string>(Columns.BarCode); }
			set { SetColumnValue(Columns.BarCode, value); }
		}
		  
		[XmlAttribute("SoThuTuIn")]
		[Bindable(true)]
		public int? SoThuTuIn 
		{
			get { return GetColumnValue<int?>(Columns.SoThuTuIn); }
			set { SetColumnValue(Columns.SoThuTuIn, value); }
		}
		  
		[XmlAttribute("KetQua")]
		[Bindable(true)]
		public string KetQua 
		{
			get { return GetColumnValue<string>(Columns.KetQua); }
			set { SetColumnValue(Columns.KetQua, value); }
		}
		  
		[XmlAttribute("TbNu")]
		[Bindable(true)]
		public string TbNu 
		{
			get { return GetColumnValue<string>(Columns.TbNu); }
			set { SetColumnValue(Columns.TbNu, value); }
		}
		  
		[XmlAttribute("TbNam")]
		[Bindable(true)]
		public string TbNam 
		{
			get { return GetColumnValue<string>(Columns.TbNam); }
			set { SetColumnValue(Columns.TbNam, value); }
		}
		  
		[XmlAttribute("DonVi")]
		[Bindable(true)]
		public string DonVi 
		{
			get { return GetColumnValue<string>(Columns.DonVi); }
			set { SetColumnValue(Columns.DonVi, value); }
		}
		  
		[XmlAttribute("TenThongSo")]
		[Bindable(true)]
		public string TenThongSo 
		{
			get { return GetColumnValue<string>(Columns.TenThongSo); }
			set { SetColumnValue(Columns.TenThongSo, value); }
		}
		  
		[XmlAttribute("TenKq")]
		[Bindable(true)]
		public string TenKq 
		{
			get { return GetColumnValue<string>(Columns.TenKq); }
			set { SetColumnValue(Columns.TenKq, value); }
		}
		  
		[XmlAttribute("LoaiKq")]
		[Bindable(true)]
		public int? LoaiKq 
		{
			get { return GetColumnValue<int?>(Columns.LoaiKq); }
			set { SetColumnValue(Columns.LoaiKq, value); }
		}
		  
		[XmlAttribute("ChoPhepHienThi")]
		[Bindable(true)]
		public int? ChoPhepHienThi 
		{
			get { return GetColumnValue<int?>(Columns.ChoPhepHienThi); }
			set { SetColumnValue(Columns.ChoPhepHienThi, value); }
		}
		  
		[XmlAttribute("ChoPhepIn")]
		[Bindable(true)]
		public int? ChoPhepIn 
		{
			get { return GetColumnValue<int?>(Columns.ChoPhepIn); }
			set { SetColumnValue(Columns.ChoPhepIn, value); }
		}
		  
		[XmlAttribute("GhiChu")]
		[Bindable(true)]
		public string GhiChu 
		{
			get { return GetColumnValue<string>(Columns.GhiChu); }
			set { SetColumnValue(Columns.GhiChu, value); }
		}
		  
		[XmlAttribute("LoaiXn")]
		[Bindable(true)]
		public int? LoaiXn 
		{
			get { return GetColumnValue<int?>(Columns.LoaiXn); }
			set { SetColumnValue(Columns.LoaiXn, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varAssignCode,int varIdChiDinh,string varBarCode,int? varSoThuTuIn,string varKetQua,string varTbNu,string varTbNam,string varDonVi,string varTenThongSo,string varTenKq,int? varLoaiKq,int? varChoPhepHienThi,int? varChoPhepIn,string varGhiChu,int? varLoaiXn)
		{
			TKetquaChitiet item = new TKetquaChitiet();
			
			item.AssignCode = varAssignCode;
			
			item.IdChiDinh = varIdChiDinh;
			
			item.BarCode = varBarCode;
			
			item.SoThuTuIn = varSoThuTuIn;
			
			item.KetQua = varKetQua;
			
			item.TbNu = varTbNu;
			
			item.TbNam = varTbNam;
			
			item.DonVi = varDonVi;
			
			item.TenThongSo = varTenThongSo;
			
			item.TenKq = varTenKq;
			
			item.LoaiKq = varLoaiKq;
			
			item.ChoPhepHienThi = varChoPhepHienThi;
			
			item.ChoPhepIn = varChoPhepIn;
			
			item.GhiChu = varGhiChu;
			
			item.LoaiXn = varLoaiXn;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varIdKqChiTiet,string varAssignCode,int varIdChiDinh,string varBarCode,int? varSoThuTuIn,string varKetQua,string varTbNu,string varTbNam,string varDonVi,string varTenThongSo,string varTenKq,int? varLoaiKq,int? varChoPhepHienThi,int? varChoPhepIn,string varGhiChu,int? varLoaiXn)
		{
			TKetquaChitiet item = new TKetquaChitiet();
			
				item.IdKqChiTiet = varIdKqChiTiet;
			
				item.AssignCode = varAssignCode;
			
				item.IdChiDinh = varIdChiDinh;
			
				item.BarCode = varBarCode;
			
				item.SoThuTuIn = varSoThuTuIn;
			
				item.KetQua = varKetQua;
			
				item.TbNu = varTbNu;
			
				item.TbNam = varTbNam;
			
				item.DonVi = varDonVi;
			
				item.TenThongSo = varTenThongSo;
			
				item.TenKq = varTenKq;
			
				item.LoaiKq = varLoaiKq;
			
				item.ChoPhepHienThi = varChoPhepHienThi;
			
				item.ChoPhepIn = varChoPhepIn;
			
				item.GhiChu = varGhiChu;
			
				item.LoaiXn = varLoaiXn;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdKqChiTietColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn AssignCodeColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IdChiDinhColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn BarCodeColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SoThuTuInColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn KetQuaColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn TbNuColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn TbNamColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn DonViColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn TenThongSoColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn TenKqColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiKqColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn ChoPhepHienThiColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn ChoPhepInColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn GhiChuColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiXnColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdKqChiTiet = @"ID_KQ_ChiTiet";
			 public static string AssignCode = @"Assign_Code";
			 public static string IdChiDinh = @"ID_CHI_DINH";
			 public static string BarCode = @"BarCode";
			 public static string SoThuTuIn = @"So_Thu_Tu_In";
			 public static string KetQua = @"Ket_Qua";
			 public static string TbNu = @"TB_Nu";
			 public static string TbNam = @"TB_Nam";
			 public static string DonVi = @"Don_Vi";
			 public static string TenThongSo = @"Ten_Thong_So";
			 public static string TenKq = @"Ten_KQ";
			 public static string LoaiKq = @"Loai_KQ";
			 public static string ChoPhepHienThi = @"Cho_Phep_Hien_Thi";
			 public static string ChoPhepIn = @"Cho_Phep_In";
			 public static string GhiChu = @"Ghi_Chu";
			 public static string LoaiXn = @"Loai_XN";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
