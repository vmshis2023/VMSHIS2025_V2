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
	/// Strongly-typed collection for the NoitruPhieuthuphanungthuoc class.
	/// </summary>
    [Serializable]
	public partial class NoitruPhieuthuphanungthuocCollection : ActiveList<NoitruPhieuthuphanungthuoc, NoitruPhieuthuphanungthuocCollection>
	{	   
		public NoitruPhieuthuphanungthuocCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>NoitruPhieuthuphanungthuocCollection</returns>
		public NoitruPhieuthuphanungthuocCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                NoitruPhieuthuphanungthuoc o = this[i];
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
	/// This is an ActiveRecord class which wraps the noitru_phieuthuphanungthuoc table.
	/// </summary>
	[Serializable]
	public partial class NoitruPhieuthuphanungthuoc : ActiveRecord<NoitruPhieuthuphanungthuoc>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public NoitruPhieuthuphanungthuoc()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public NoitruPhieuthuphanungthuoc(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public NoitruPhieuthuphanungthuoc(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public NoitruPhieuthuphanungthuoc(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("noitru_phieuthuphanungthuoc", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdPhieu = new TableSchema.TableColumn(schema);
				colvarIdPhieu.ColumnName = "id_phieu";
				colvarIdPhieu.DataType = DbType.Int64;
				colvarIdPhieu.MaxLength = 0;
				colvarIdPhieu.AutoIncrement = true;
				colvarIdPhieu.IsNullable = false;
				colvarIdPhieu.IsPrimaryKey = true;
				colvarIdPhieu.IsForeignKey = false;
				colvarIdPhieu.IsReadOnly = false;
				colvarIdPhieu.DefaultSetting = @"";
				colvarIdPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdPhieu);
				
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
				colvarMaLuotkham.MaxLength = 20;
				colvarMaLuotkham.AutoIncrement = false;
				colvarMaLuotkham.IsNullable = false;
				colvarMaLuotkham.IsPrimaryKey = false;
				colvarMaLuotkham.IsForeignKey = false;
				colvarMaLuotkham.IsReadOnly = false;
				colvarMaLuotkham.DefaultSetting = @"";
				colvarMaLuotkham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaLuotkham);
				
				TableSchema.TableColumn colvarIdChitietdonthuoc = new TableSchema.TableColumn(schema);
				colvarIdChitietdonthuoc.ColumnName = "id_chitietdonthuoc";
				colvarIdChitietdonthuoc.DataType = DbType.Int64;
				colvarIdChitietdonthuoc.MaxLength = 0;
				colvarIdChitietdonthuoc.AutoIncrement = false;
				colvarIdChitietdonthuoc.IsNullable = true;
				colvarIdChitietdonthuoc.IsPrimaryKey = false;
				colvarIdChitietdonthuoc.IsForeignKey = false;
				colvarIdChitietdonthuoc.IsReadOnly = false;
				colvarIdChitietdonthuoc.DefaultSetting = @"";
				colvarIdChitietdonthuoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChitietdonthuoc);
				
				TableSchema.TableColumn colvarIdBuongGiuong = new TableSchema.TableColumn(schema);
				colvarIdBuongGiuong.ColumnName = "id_buong_giuong";
				colvarIdBuongGiuong.DataType = DbType.Int64;
				colvarIdBuongGiuong.MaxLength = 0;
				colvarIdBuongGiuong.AutoIncrement = false;
				colvarIdBuongGiuong.IsNullable = true;
				colvarIdBuongGiuong.IsPrimaryKey = false;
				colvarIdBuongGiuong.IsForeignKey = false;
				colvarIdBuongGiuong.IsReadOnly = false;
				colvarIdBuongGiuong.DefaultSetting = @"";
				colvarIdBuongGiuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBuongGiuong);
				
				TableSchema.TableColumn colvarIdKhoanoitru = new TableSchema.TableColumn(schema);
				colvarIdKhoanoitru.ColumnName = "id_khoanoitru";
				colvarIdKhoanoitru.DataType = DbType.Int32;
				colvarIdKhoanoitru.MaxLength = 0;
				colvarIdKhoanoitru.AutoIncrement = false;
				colvarIdKhoanoitru.IsNullable = true;
				colvarIdKhoanoitru.IsPrimaryKey = false;
				colvarIdKhoanoitru.IsForeignKey = false;
				colvarIdKhoanoitru.IsReadOnly = false;
				colvarIdKhoanoitru.DefaultSetting = @"";
				colvarIdKhoanoitru.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhoanoitru);
				
				TableSchema.TableColumn colvarIdBsiChidinh = new TableSchema.TableColumn(schema);
				colvarIdBsiChidinh.ColumnName = "id_bsi_chidinh";
				colvarIdBsiChidinh.DataType = DbType.Int32;
				colvarIdBsiChidinh.MaxLength = 0;
				colvarIdBsiChidinh.AutoIncrement = false;
				colvarIdBsiChidinh.IsNullable = true;
				colvarIdBsiChidinh.IsPrimaryKey = false;
				colvarIdBsiChidinh.IsForeignKey = false;
				colvarIdBsiChidinh.IsReadOnly = false;
				colvarIdBsiChidinh.DefaultSetting = @"";
				colvarIdBsiChidinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBsiChidinh);
				
				TableSchema.TableColumn colvarIdBsiKiemtra = new TableSchema.TableColumn(schema);
				colvarIdBsiKiemtra.ColumnName = "id_bsi_kiemtra";
				colvarIdBsiKiemtra.DataType = DbType.Int32;
				colvarIdBsiKiemtra.MaxLength = 0;
				colvarIdBsiKiemtra.AutoIncrement = false;
				colvarIdBsiKiemtra.IsNullable = true;
				colvarIdBsiKiemtra.IsPrimaryKey = false;
				colvarIdBsiKiemtra.IsForeignKey = false;
				colvarIdBsiKiemtra.IsReadOnly = false;
				colvarIdBsiKiemtra.DefaultSetting = @"";
				colvarIdBsiKiemtra.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBsiKiemtra);
				
				TableSchema.TableColumn colvarNgayKiemtra = new TableSchema.TableColumn(schema);
				colvarNgayKiemtra.ColumnName = "ngay_kiemtra";
				colvarNgayKiemtra.DataType = DbType.DateTime;
				colvarNgayKiemtra.MaxLength = 0;
				colvarNgayKiemtra.AutoIncrement = false;
				colvarNgayKiemtra.IsNullable = true;
				colvarNgayKiemtra.IsPrimaryKey = false;
				colvarNgayKiemtra.IsForeignKey = false;
				colvarNgayKiemtra.IsReadOnly = false;
				colvarNgayKiemtra.DefaultSetting = @"";
				colvarNgayKiemtra.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayKiemtra);
				
				TableSchema.TableColumn colvarIdNvienThu = new TableSchema.TableColumn(schema);
				colvarIdNvienThu.ColumnName = "id_nvien_thu";
				colvarIdNvienThu.DataType = DbType.Int32;
				colvarIdNvienThu.MaxLength = 0;
				colvarIdNvienThu.AutoIncrement = false;
				colvarIdNvienThu.IsNullable = true;
				colvarIdNvienThu.IsPrimaryKey = false;
				colvarIdNvienThu.IsForeignKey = false;
				colvarIdNvienThu.IsReadOnly = false;
				colvarIdNvienThu.DefaultSetting = @"";
				colvarIdNvienThu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdNvienThu);
				
				TableSchema.TableColumn colvarNgayThien = new TableSchema.TableColumn(schema);
				colvarNgayThien.ColumnName = "ngay_thien";
				colvarNgayThien.DataType = DbType.DateTime;
				colvarNgayThien.MaxLength = 0;
				colvarNgayThien.AutoIncrement = false;
				colvarNgayThien.IsNullable = false;
				colvarNgayThien.IsPrimaryKey = false;
				colvarNgayThien.IsForeignKey = false;
				colvarNgayThien.IsReadOnly = false;
				colvarNgayThien.DefaultSetting = @"";
				colvarNgayThien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayThien);
				
				TableSchema.TableColumn colvarMaPhuongphapthu = new TableSchema.TableColumn(schema);
				colvarMaPhuongphapthu.ColumnName = "ma_phuongphapthu";
				colvarMaPhuongphapthu.DataType = DbType.String;
				colvarMaPhuongphapthu.MaxLength = 20;
				colvarMaPhuongphapthu.AutoIncrement = false;
				colvarMaPhuongphapthu.IsNullable = true;
				colvarMaPhuongphapthu.IsPrimaryKey = false;
				colvarMaPhuongphapthu.IsForeignKey = false;
				colvarMaPhuongphapthu.IsReadOnly = false;
				colvarMaPhuongphapthu.DefaultSetting = @"";
				colvarMaPhuongphapthu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaPhuongphapthu);
				
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
				
				TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
				colvarNguoiTao.ColumnName = "nguoi_tao";
				colvarNguoiTao.DataType = DbType.String;
				colvarNguoiTao.MaxLength = 20;
				colvarNguoiTao.AutoIncrement = false;
				colvarNguoiTao.IsNullable = false;
				colvarNguoiTao.IsPrimaryKey = false;
				colvarNguoiTao.IsForeignKey = false;
				colvarNguoiTao.IsReadOnly = false;
				colvarNguoiTao.DefaultSetting = @"";
				colvarNguoiTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiTao);
				
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
				
				TableSchema.TableColumn colvarNguoiSua = new TableSchema.TableColumn(schema);
				colvarNguoiSua.ColumnName = "nguoi_sua";
				colvarNguoiSua.DataType = DbType.String;
				colvarNguoiSua.MaxLength = 20;
				colvarNguoiSua.AutoIncrement = false;
				colvarNguoiSua.IsNullable = true;
				colvarNguoiSua.IsPrimaryKey = false;
				colvarNguoiSua.IsForeignKey = false;
				colvarNguoiSua.IsReadOnly = false;
				colvarNguoiSua.DefaultSetting = @"";
				colvarNguoiSua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiSua);
				
				TableSchema.TableColumn colvarTrangthaiIn = new TableSchema.TableColumn(schema);
				colvarTrangthaiIn.ColumnName = "trangthai_in";
				colvarTrangthaiIn.DataType = DbType.Boolean;
				colvarTrangthaiIn.MaxLength = 0;
				colvarTrangthaiIn.AutoIncrement = false;
				colvarTrangthaiIn.IsNullable = true;
				colvarTrangthaiIn.IsPrimaryKey = false;
				colvarTrangthaiIn.IsForeignKey = false;
				colvarTrangthaiIn.IsReadOnly = false;
				colvarTrangthaiIn.DefaultSetting = @"";
				colvarTrangthaiIn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangthaiIn);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("noitru_phieuthuphanungthuoc",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdPhieu")]
		[Bindable(true)]
		public long IdPhieu 
		{
			get { return GetColumnValue<long>(Columns.IdPhieu); }
			set { SetColumnValue(Columns.IdPhieu, value); }
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
		  
		[XmlAttribute("IdChitietdonthuoc")]
		[Bindable(true)]
		public long? IdChitietdonthuoc 
		{
			get { return GetColumnValue<long?>(Columns.IdChitietdonthuoc); }
			set { SetColumnValue(Columns.IdChitietdonthuoc, value); }
		}
		  
		[XmlAttribute("IdBuongGiuong")]
		[Bindable(true)]
		public long? IdBuongGiuong 
		{
			get { return GetColumnValue<long?>(Columns.IdBuongGiuong); }
			set { SetColumnValue(Columns.IdBuongGiuong, value); }
		}
		  
		[XmlAttribute("IdKhoanoitru")]
		[Bindable(true)]
		public int? IdKhoanoitru 
		{
			get { return GetColumnValue<int?>(Columns.IdKhoanoitru); }
			set { SetColumnValue(Columns.IdKhoanoitru, value); }
		}
		  
		[XmlAttribute("IdBsiChidinh")]
		[Bindable(true)]
		public int? IdBsiChidinh 
		{
			get { return GetColumnValue<int?>(Columns.IdBsiChidinh); }
			set { SetColumnValue(Columns.IdBsiChidinh, value); }
		}
		  
		[XmlAttribute("IdBsiKiemtra")]
		[Bindable(true)]
		public int? IdBsiKiemtra 
		{
			get { return GetColumnValue<int?>(Columns.IdBsiKiemtra); }
			set { SetColumnValue(Columns.IdBsiKiemtra, value); }
		}
		  
		[XmlAttribute("NgayKiemtra")]
		[Bindable(true)]
		public DateTime? NgayKiemtra 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayKiemtra); }
			set { SetColumnValue(Columns.NgayKiemtra, value); }
		}
		  
		[XmlAttribute("IdNvienThu")]
		[Bindable(true)]
		public int? IdNvienThu 
		{
			get { return GetColumnValue<int?>(Columns.IdNvienThu); }
			set { SetColumnValue(Columns.IdNvienThu, value); }
		}
		  
		[XmlAttribute("NgayThien")]
		[Bindable(true)]
		public DateTime NgayThien 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayThien); }
			set { SetColumnValue(Columns.NgayThien, value); }
		}
		  
		[XmlAttribute("MaPhuongphapthu")]
		[Bindable(true)]
		public string MaPhuongphapthu 
		{
			get { return GetColumnValue<string>(Columns.MaPhuongphapthu); }
			set { SetColumnValue(Columns.MaPhuongphapthu, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime NgayTao 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		  
		[XmlAttribute("NguoiTao")]
		[Bindable(true)]
		public string NguoiTao 
		{
			get { return GetColumnValue<string>(Columns.NguoiTao); }
			set { SetColumnValue(Columns.NguoiTao, value); }
		}
		  
		[XmlAttribute("NgaySua")]
		[Bindable(true)]
		public DateTime? NgaySua 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgaySua); }
			set { SetColumnValue(Columns.NgaySua, value); }
		}
		  
		[XmlAttribute("NguoiSua")]
		[Bindable(true)]
		public string NguoiSua 
		{
			get { return GetColumnValue<string>(Columns.NguoiSua); }
			set { SetColumnValue(Columns.NguoiSua, value); }
		}
		  
		[XmlAttribute("TrangthaiIn")]
		[Bindable(true)]
		public bool? TrangthaiIn 
		{
			get { return GetColumnValue<bool?>(Columns.TrangthaiIn); }
			set { SetColumnValue(Columns.TrangthaiIn, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varIdBenhnhan,string varMaLuotkham,long? varIdChitietdonthuoc,long? varIdBuongGiuong,int? varIdKhoanoitru,int? varIdBsiChidinh,int? varIdBsiKiemtra,DateTime? varNgayKiemtra,int? varIdNvienThu,DateTime varNgayThien,string varMaPhuongphapthu,DateTime varNgayTao,string varNguoiTao,DateTime? varNgaySua,string varNguoiSua,bool? varTrangthaiIn)
		{
			NoitruPhieuthuphanungthuoc item = new NoitruPhieuthuphanungthuoc();
			
			item.IdBenhnhan = varIdBenhnhan;
			
			item.MaLuotkham = varMaLuotkham;
			
			item.IdChitietdonthuoc = varIdChitietdonthuoc;
			
			item.IdBuongGiuong = varIdBuongGiuong;
			
			item.IdKhoanoitru = varIdKhoanoitru;
			
			item.IdBsiChidinh = varIdBsiChidinh;
			
			item.IdBsiKiemtra = varIdBsiKiemtra;
			
			item.NgayKiemtra = varNgayKiemtra;
			
			item.IdNvienThu = varIdNvienThu;
			
			item.NgayThien = varNgayThien;
			
			item.MaPhuongphapthu = varMaPhuongphapthu;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgaySua = varNgaySua;
			
			item.NguoiSua = varNguoiSua;
			
			item.TrangthaiIn = varTrangthaiIn;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varIdPhieu,long varIdBenhnhan,string varMaLuotkham,long? varIdChitietdonthuoc,long? varIdBuongGiuong,int? varIdKhoanoitru,int? varIdBsiChidinh,int? varIdBsiKiemtra,DateTime? varNgayKiemtra,int? varIdNvienThu,DateTime varNgayThien,string varMaPhuongphapthu,DateTime varNgayTao,string varNguoiTao,DateTime? varNgaySua,string varNguoiSua,bool? varTrangthaiIn)
		{
			NoitruPhieuthuphanungthuoc item = new NoitruPhieuthuphanungthuoc();
			
				item.IdPhieu = varIdPhieu;
			
				item.IdBenhnhan = varIdBenhnhan;
			
				item.MaLuotkham = varMaLuotkham;
			
				item.IdChitietdonthuoc = varIdChitietdonthuoc;
			
				item.IdBuongGiuong = varIdBuongGiuong;
			
				item.IdKhoanoitru = varIdKhoanoitru;
			
				item.IdBsiChidinh = varIdBsiChidinh;
			
				item.IdBsiKiemtra = varIdBsiKiemtra;
			
				item.NgayKiemtra = varNgayKiemtra;
			
				item.IdNvienThu = varIdNvienThu;
			
				item.NgayThien = varNgayThien;
			
				item.MaPhuongphapthu = varMaPhuongphapthu;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgaySua = varNgaySua;
			
				item.NguoiSua = varNguoiSua;
			
				item.TrangthaiIn = varTrangthaiIn;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdPhieuColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdBenhnhanColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn MaLuotkhamColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdChitietdonthuocColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn IdBuongGiuongColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhoanoitruColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn IdBsiChidinhColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn IdBsiKiemtraColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayKiemtraColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn IdNvienThuColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayThienColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn MaPhuongphapthuColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangthaiInColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdPhieu = @"id_phieu";
			 public static string IdBenhnhan = @"id_benhnhan";
			 public static string MaLuotkham = @"ma_luotkham";
			 public static string IdChitietdonthuoc = @"id_chitietdonthuoc";
			 public static string IdBuongGiuong = @"id_buong_giuong";
			 public static string IdKhoanoitru = @"id_khoanoitru";
			 public static string IdBsiChidinh = @"id_bsi_chidinh";
			 public static string IdBsiKiemtra = @"id_bsi_kiemtra";
			 public static string NgayKiemtra = @"ngay_kiemtra";
			 public static string IdNvienThu = @"id_nvien_thu";
			 public static string NgayThien = @"ngay_thien";
			 public static string MaPhuongphapthu = @"ma_phuongphapthu";
			 public static string NgayTao = @"ngay_tao";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string NgaySua = @"ngay_sua";
			 public static string NguoiSua = @"nguoi_sua";
			 public static string TrangthaiIn = @"trangthai_in";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
