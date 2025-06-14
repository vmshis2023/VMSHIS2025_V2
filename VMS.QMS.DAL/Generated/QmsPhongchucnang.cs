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
namespace VMS.QMS.DAL
{
	/// <summary>
	/// Strongly-typed collection for the QmsPhongchucnang class.
	/// </summary>
    [Serializable]
	public partial class QmsPhongchucnangCollection : ActiveList<QmsPhongchucnang, QmsPhongchucnangCollection>
	{	   
		public QmsPhongchucnangCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>QmsPhongchucnangCollection</returns>
		public QmsPhongchucnangCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                QmsPhongchucnang o = this[i];
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
	/// This is an ActiveRecord class which wraps the qms_phongchucnang table.
	/// </summary>
	[Serializable]
	public partial class QmsPhongchucnang : ActiveRecord<QmsPhongchucnang>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public QmsPhongchucnang()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public QmsPhongchucnang(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public QmsPhongchucnang(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public QmsPhongchucnang(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("qms_phongchucnang", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "id";
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
				
				TableSchema.TableColumn colvarMaKhoakcb = new TableSchema.TableColumn(schema);
				colvarMaKhoakcb.ColumnName = "ma_khoakcb";
				colvarMaKhoakcb.DataType = DbType.String;
				colvarMaKhoakcb.MaxLength = 30;
				colvarMaKhoakcb.AutoIncrement = false;
				colvarMaKhoakcb.IsNullable = false;
				colvarMaKhoakcb.IsPrimaryKey = false;
				colvarMaKhoakcb.IsForeignKey = false;
				colvarMaKhoakcb.IsReadOnly = false;
				colvarMaKhoakcb.DefaultSetting = @"";
				colvarMaKhoakcb.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaKhoakcb);
				
				TableSchema.TableColumn colvarMaPhong = new TableSchema.TableColumn(schema);
				colvarMaPhong.ColumnName = "ma_phong";
				colvarMaPhong.DataType = DbType.String;
				colvarMaPhong.MaxLength = 30;
				colvarMaPhong.AutoIncrement = false;
				colvarMaPhong.IsNullable = false;
				colvarMaPhong.IsPrimaryKey = false;
				colvarMaPhong.IsForeignKey = false;
				colvarMaPhong.IsReadOnly = false;
				colvarMaPhong.DefaultSetting = @"";
				colvarMaPhong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaPhong);
				
				TableSchema.TableColumn colvarMaChucnang = new TableSchema.TableColumn(schema);
				colvarMaChucnang.ColumnName = "ma_chucnang";
				colvarMaChucnang.DataType = DbType.String;
				colvarMaChucnang.MaxLength = 30;
				colvarMaChucnang.AutoIncrement = false;
				colvarMaChucnang.IsNullable = false;
				colvarMaChucnang.IsPrimaryKey = false;
				colvarMaChucnang.IsForeignKey = false;
				colvarMaChucnang.IsReadOnly = false;
				colvarMaChucnang.DefaultSetting = @"";
				colvarMaChucnang.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaChucnang);
				
				TableSchema.TableColumn colvarMaChidinh = new TableSchema.TableColumn(schema);
				colvarMaChidinh.ColumnName = "ma_chidinh";
				colvarMaChidinh.DataType = DbType.String;
				colvarMaChidinh.MaxLength = 11;
				colvarMaChidinh.AutoIncrement = false;
				colvarMaChidinh.IsNullable = false;
				colvarMaChidinh.IsPrimaryKey = false;
				colvarMaChidinh.IsForeignKey = false;
				colvarMaChidinh.IsReadOnly = false;
				colvarMaChidinh.DefaultSetting = @"";
				colvarMaChidinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaChidinh);
				
				TableSchema.TableColumn colvarSoQms = new TableSchema.TableColumn(schema);
				colvarSoQms.ColumnName = "so_qms";
				colvarSoQms.DataType = DbType.Int32;
				colvarSoQms.MaxLength = 0;
				colvarSoQms.AutoIncrement = false;
				colvarSoQms.IsNullable = false;
				colvarSoQms.IsPrimaryKey = false;
				colvarSoQms.IsForeignKey = false;
				colvarSoQms.IsReadOnly = false;
				colvarSoQms.DefaultSetting = @"";
				colvarSoQms.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoQms);
				
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
				
				TableSchema.TableColumn colvarNgayChidinh = new TableSchema.TableColumn(schema);
				colvarNgayChidinh.ColumnName = "ngay_chidinh";
				colvarNgayChidinh.DataType = DbType.DateTime;
				colvarNgayChidinh.MaxLength = 0;
				colvarNgayChidinh.AutoIncrement = false;
				colvarNgayChidinh.IsNullable = false;
				colvarNgayChidinh.IsPrimaryKey = false;
				colvarNgayChidinh.IsForeignKey = false;
				colvarNgayChidinh.IsReadOnly = false;
				colvarNgayChidinh.DefaultSetting = @"";
				colvarNgayChidinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayChidinh);
				
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
				
				TableSchema.TableColumn colvarTenBenhnhan = new TableSchema.TableColumn(schema);
				colvarTenBenhnhan.ColumnName = "ten_benhnhan";
				colvarTenBenhnhan.DataType = DbType.String;
				colvarTenBenhnhan.MaxLength = 100;
				colvarTenBenhnhan.AutoIncrement = false;
				colvarTenBenhnhan.IsNullable = false;
				colvarTenBenhnhan.IsPrimaryKey = false;
				colvarTenBenhnhan.IsForeignKey = false;
				colvarTenBenhnhan.IsReadOnly = false;
				colvarTenBenhnhan.DefaultSetting = @"";
				colvarTenBenhnhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenBenhnhan);
				
				TableSchema.TableColumn colvarNamSinh = new TableSchema.TableColumn(schema);
				colvarNamSinh.ColumnName = "nam_sinh";
				colvarNamSinh.DataType = DbType.Int32;
				colvarNamSinh.MaxLength = 0;
				colvarNamSinh.AutoIncrement = false;
				colvarNamSinh.IsNullable = false;
				colvarNamSinh.IsPrimaryKey = false;
				colvarNamSinh.IsForeignKey = false;
				colvarNamSinh.IsReadOnly = false;
				colvarNamSinh.DefaultSetting = @"";
				colvarNamSinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNamSinh);
				
				TableSchema.TableColumn colvarTuoi = new TableSchema.TableColumn(schema);
				colvarTuoi.ColumnName = "tuoi";
				colvarTuoi.DataType = DbType.Int32;
				colvarTuoi.MaxLength = 0;
				colvarTuoi.AutoIncrement = false;
				colvarTuoi.IsNullable = false;
				colvarTuoi.IsPrimaryKey = false;
				colvarTuoi.IsForeignKey = false;
				colvarTuoi.IsReadOnly = false;
				colvarTuoi.DefaultSetting = @"";
				colvarTuoi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTuoi);
				
				TableSchema.TableColumn colvarGioitinh = new TableSchema.TableColumn(schema);
				colvarGioitinh.ColumnName = "gioitinh";
				colvarGioitinh.DataType = DbType.String;
				colvarGioitinh.MaxLength = 10;
				colvarGioitinh.AutoIncrement = false;
				colvarGioitinh.IsNullable = false;
				colvarGioitinh.IsPrimaryKey = false;
				colvarGioitinh.IsForeignKey = false;
				colvarGioitinh.IsReadOnly = false;
				colvarGioitinh.DefaultSetting = @"";
				colvarGioitinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGioitinh);
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "trang_thai";
				colvarTrangThai.DataType = DbType.Byte;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = false;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				
						colvarTrangThai.DefaultSetting = @"((0))";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
				TableSchema.TableColumn colvarHieuLuc = new TableSchema.TableColumn(schema);
				colvarHieuLuc.ColumnName = "hieu_luc";
				colvarHieuLuc.DataType = DbType.Byte;
				colvarHieuLuc.MaxLength = 0;
				colvarHieuLuc.AutoIncrement = false;
				colvarHieuLuc.IsNullable = false;
				colvarHieuLuc.IsPrimaryKey = false;
				colvarHieuLuc.IsForeignKey = false;
				colvarHieuLuc.IsReadOnly = false;
				
						colvarHieuLuc.DefaultSetting = @"((1))";
				colvarHieuLuc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHieuLuc);
				
				TableSchema.TableColumn colvarDaKham = new TableSchema.TableColumn(schema);
				colvarDaKham.ColumnName = "da_kham";
				colvarDaKham.DataType = DbType.Byte;
				colvarDaKham.MaxLength = 0;
				colvarDaKham.AutoIncrement = false;
				colvarDaKham.IsNullable = true;
				colvarDaKham.IsPrimaryKey = false;
				colvarDaKham.IsForeignKey = false;
				colvarDaKham.IsReadOnly = false;
				
						colvarDaKham.DefaultSetting = @"((0))";
				colvarDaKham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDaKham);
				
				TableSchema.TableColumn colvarUuTien = new TableSchema.TableColumn(schema);
				colvarUuTien.ColumnName = "uu_tien";
				colvarUuTien.DataType = DbType.Byte;
				colvarUuTien.MaxLength = 0;
				colvarUuTien.AutoIncrement = false;
				colvarUuTien.IsNullable = true;
				colvarUuTien.IsPrimaryKey = false;
				colvarUuTien.IsForeignKey = false;
				colvarUuTien.IsReadOnly = false;
				
						colvarUuTien.DefaultSetting = @"((0))";
				colvarUuTien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUuTien);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("qms_phongchucnang",schema);
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
		  
		[XmlAttribute("MaKhoakcb")]
		[Bindable(true)]
		public string MaKhoakcb 
		{
			get { return GetColumnValue<string>(Columns.MaKhoakcb); }
			set { SetColumnValue(Columns.MaKhoakcb, value); }
		}
		  
		[XmlAttribute("MaPhong")]
		[Bindable(true)]
		public string MaPhong 
		{
			get { return GetColumnValue<string>(Columns.MaPhong); }
			set { SetColumnValue(Columns.MaPhong, value); }
		}
		  
		[XmlAttribute("MaChucnang")]
		[Bindable(true)]
		public string MaChucnang 
		{
			get { return GetColumnValue<string>(Columns.MaChucnang); }
			set { SetColumnValue(Columns.MaChucnang, value); }
		}
		  
		[XmlAttribute("MaChidinh")]
		[Bindable(true)]
		public string MaChidinh 
		{
			get { return GetColumnValue<string>(Columns.MaChidinh); }
			set { SetColumnValue(Columns.MaChidinh, value); }
		}
		  
		[XmlAttribute("SoQms")]
		[Bindable(true)]
		public int SoQms 
		{
			get { return GetColumnValue<int>(Columns.SoQms); }
			set { SetColumnValue(Columns.SoQms, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime NgayTao 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		  
		[XmlAttribute("NgayChidinh")]
		[Bindable(true)]
		public DateTime NgayChidinh 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayChidinh); }
			set { SetColumnValue(Columns.NgayChidinh, value); }
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
		  
		[XmlAttribute("TenBenhnhan")]
		[Bindable(true)]
		public string TenBenhnhan 
		{
			get { return GetColumnValue<string>(Columns.TenBenhnhan); }
			set { SetColumnValue(Columns.TenBenhnhan, value); }
		}
		  
		[XmlAttribute("NamSinh")]
		[Bindable(true)]
		public int NamSinh 
		{
			get { return GetColumnValue<int>(Columns.NamSinh); }
			set { SetColumnValue(Columns.NamSinh, value); }
		}
		  
		[XmlAttribute("Tuoi")]
		[Bindable(true)]
		public int Tuoi 
		{
			get { return GetColumnValue<int>(Columns.Tuoi); }
			set { SetColumnValue(Columns.Tuoi, value); }
		}
		  
		[XmlAttribute("Gioitinh")]
		[Bindable(true)]
		public string Gioitinh 
		{
			get { return GetColumnValue<string>(Columns.Gioitinh); }
			set { SetColumnValue(Columns.Gioitinh, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public byte TrangThai 
		{
			get { return GetColumnValue<byte>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("HieuLuc")]
		[Bindable(true)]
		public byte HieuLuc 
		{
			get { return GetColumnValue<byte>(Columns.HieuLuc); }
			set { SetColumnValue(Columns.HieuLuc, value); }
		}
		  
		[XmlAttribute("DaKham")]
		[Bindable(true)]
		public byte? DaKham 
		{
			get { return GetColumnValue<byte?>(Columns.DaKham); }
			set { SetColumnValue(Columns.DaKham, value); }
		}
		  
		[XmlAttribute("UuTien")]
		[Bindable(true)]
		public byte? UuTien 
		{
			get { return GetColumnValue<byte?>(Columns.UuTien); }
			set { SetColumnValue(Columns.UuTien, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaKhoakcb,string varMaPhong,string varMaChucnang,string varMaChidinh,int varSoQms,DateTime varNgayTao,DateTime varNgayChidinh,long varIdBenhnhan,string varMaLuotkham,string varTenBenhnhan,int varNamSinh,int varTuoi,string varGioitinh,byte varTrangThai,byte varHieuLuc,byte? varDaKham,byte? varUuTien)
		{
			QmsPhongchucnang item = new QmsPhongchucnang();
			
			item.MaKhoakcb = varMaKhoakcb;
			
			item.MaPhong = varMaPhong;
			
			item.MaChucnang = varMaChucnang;
			
			item.MaChidinh = varMaChidinh;
			
			item.SoQms = varSoQms;
			
			item.NgayTao = varNgayTao;
			
			item.NgayChidinh = varNgayChidinh;
			
			item.IdBenhnhan = varIdBenhnhan;
			
			item.MaLuotkham = varMaLuotkham;
			
			item.TenBenhnhan = varTenBenhnhan;
			
			item.NamSinh = varNamSinh;
			
			item.Tuoi = varTuoi;
			
			item.Gioitinh = varGioitinh;
			
			item.TrangThai = varTrangThai;
			
			item.HieuLuc = varHieuLuc;
			
			item.DaKham = varDaKham;
			
			item.UuTien = varUuTien;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varId,string varMaKhoakcb,string varMaPhong,string varMaChucnang,string varMaChidinh,int varSoQms,DateTime varNgayTao,DateTime varNgayChidinh,long varIdBenhnhan,string varMaLuotkham,string varTenBenhnhan,int varNamSinh,int varTuoi,string varGioitinh,byte varTrangThai,byte varHieuLuc,byte? varDaKham,byte? varUuTien)
		{
			QmsPhongchucnang item = new QmsPhongchucnang();
			
				item.Id = varId;
			
				item.MaKhoakcb = varMaKhoakcb;
			
				item.MaPhong = varMaPhong;
			
				item.MaChucnang = varMaChucnang;
			
				item.MaChidinh = varMaChidinh;
			
				item.SoQms = varSoQms;
			
				item.NgayTao = varNgayTao;
			
				item.NgayChidinh = varNgayChidinh;
			
				item.IdBenhnhan = varIdBenhnhan;
			
				item.MaLuotkham = varMaLuotkham;
			
				item.TenBenhnhan = varTenBenhnhan;
			
				item.NamSinh = varNamSinh;
			
				item.Tuoi = varTuoi;
			
				item.Gioitinh = varGioitinh;
			
				item.TrangThai = varTrangThai;
			
				item.HieuLuc = varHieuLuc;
			
				item.DaKham = varDaKham;
			
				item.UuTien = varUuTien;
			
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
        
        
        
        public static TableSchema.TableColumn MaKhoakcbColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn MaPhongColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn MaChucnangColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn MaChidinhColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn SoQmsColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayChidinhColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn IdBenhnhanColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn MaLuotkhamColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn TenBenhnhanColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn NamSinhColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn TuoiColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn GioitinhColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn HieuLucColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn DaKhamColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn UuTienColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"id";
			 public static string MaKhoakcb = @"ma_khoakcb";
			 public static string MaPhong = @"ma_phong";
			 public static string MaChucnang = @"ma_chucnang";
			 public static string MaChidinh = @"ma_chidinh";
			 public static string SoQms = @"so_qms";
			 public static string NgayTao = @"ngay_tao";
			 public static string NgayChidinh = @"ngay_chidinh";
			 public static string IdBenhnhan = @"id_benhnhan";
			 public static string MaLuotkham = @"ma_luotkham";
			 public static string TenBenhnhan = @"ten_benhnhan";
			 public static string NamSinh = @"nam_sinh";
			 public static string Tuoi = @"tuoi";
			 public static string Gioitinh = @"gioitinh";
			 public static string TrangThai = @"trang_thai";
			 public static string HieuLuc = @"hieu_luc";
			 public static string DaKham = @"da_kham";
			 public static string UuTien = @"uu_tien";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
