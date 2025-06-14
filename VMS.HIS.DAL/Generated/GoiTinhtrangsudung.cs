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
	/// Strongly-typed collection for the GoiTinhtrangsudung class.
	/// </summary>
    [Serializable]
	public partial class GoiTinhtrangsudungCollection : ActiveList<GoiTinhtrangsudung, GoiTinhtrangsudungCollection>
	{	   
		public GoiTinhtrangsudungCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>GoiTinhtrangsudungCollection</returns>
		public GoiTinhtrangsudungCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                GoiTinhtrangsudung o = this[i];
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
	/// This is an ActiveRecord class which wraps the goi_tinhtrangsudung table.
	/// </summary>
	[Serializable]
	public partial class GoiTinhtrangsudung : ActiveRecord<GoiTinhtrangsudung>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public GoiTinhtrangsudung()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public GoiTinhtrangsudung(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public GoiTinhtrangsudung(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public GoiTinhtrangsudung(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("goi_tinhtrangsudung", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdSudung = new TableSchema.TableColumn(schema);
				colvarIdSudung.ColumnName = "id_sudung";
				colvarIdSudung.DataType = DbType.Int32;
				colvarIdSudung.MaxLength = 0;
				colvarIdSudung.AutoIncrement = true;
				colvarIdSudung.IsNullable = false;
				colvarIdSudung.IsPrimaryKey = true;
				colvarIdSudung.IsForeignKey = false;
				colvarIdSudung.IsReadOnly = false;
				colvarIdSudung.DefaultSetting = @"";
				colvarIdSudung.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdSudung);
				
				TableSchema.TableColumn colvarIdDangky = new TableSchema.TableColumn(schema);
				colvarIdDangky.ColumnName = "id_dangky";
				colvarIdDangky.DataType = DbType.Int32;
				colvarIdDangky.MaxLength = 0;
				colvarIdDangky.AutoIncrement = false;
				colvarIdDangky.IsNullable = false;
				colvarIdDangky.IsPrimaryKey = false;
				colvarIdDangky.IsForeignKey = false;
				colvarIdDangky.IsReadOnly = false;
				colvarIdDangky.DefaultSetting = @"";
				colvarIdDangky.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdDangky);
				
				TableSchema.TableColumn colvarIdGoi = new TableSchema.TableColumn(schema);
				colvarIdGoi.ColumnName = "id_goi";
				colvarIdGoi.DataType = DbType.Int32;
				colvarIdGoi.MaxLength = 0;
				colvarIdGoi.AutoIncrement = false;
				colvarIdGoi.IsNullable = false;
				colvarIdGoi.IsPrimaryKey = false;
				colvarIdGoi.IsForeignKey = false;
				colvarIdGoi.IsReadOnly = false;
				colvarIdGoi.DefaultSetting = @"";
				colvarIdGoi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdGoi);
				
				TableSchema.TableColumn colvarIdDichvu = new TableSchema.TableColumn(schema);
				colvarIdDichvu.ColumnName = "id_dichvu";
				colvarIdDichvu.DataType = DbType.Int32;
				colvarIdDichvu.MaxLength = 0;
				colvarIdDichvu.AutoIncrement = false;
				colvarIdDichvu.IsNullable = false;
				colvarIdDichvu.IsPrimaryKey = false;
				colvarIdDichvu.IsForeignKey = false;
				colvarIdDichvu.IsReadOnly = false;
				colvarIdDichvu.DefaultSetting = @"";
				colvarIdDichvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdDichvu);
				
				TableSchema.TableColumn colvarIdChitietdichvu = new TableSchema.TableColumn(schema);
				colvarIdChitietdichvu.ColumnName = "id_chitietdichvu";
				colvarIdChitietdichvu.DataType = DbType.Int32;
				colvarIdChitietdichvu.MaxLength = 0;
				colvarIdChitietdichvu.AutoIncrement = false;
				colvarIdChitietdichvu.IsNullable = false;
				colvarIdChitietdichvu.IsPrimaryKey = false;
				colvarIdChitietdichvu.IsForeignKey = false;
				colvarIdChitietdichvu.IsReadOnly = false;
				colvarIdChitietdichvu.DefaultSetting = @"";
				colvarIdChitietdichvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChitietdichvu);
				
				TableSchema.TableColumn colvarTenChitietdichvu = new TableSchema.TableColumn(schema);
				colvarTenChitietdichvu.ColumnName = "ten_chitietdichvu";
				colvarTenChitietdichvu.DataType = DbType.String;
				colvarTenChitietdichvu.MaxLength = 200;
				colvarTenChitietdichvu.AutoIncrement = false;
				colvarTenChitietdichvu.IsNullable = false;
				colvarTenChitietdichvu.IsPrimaryKey = false;
				colvarTenChitietdichvu.IsForeignKey = false;
				colvarTenChitietdichvu.IsReadOnly = false;
				colvarTenChitietdichvu.DefaultSetting = @"";
				colvarTenChitietdichvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenChitietdichvu);
				
				TableSchema.TableColumn colvarSoLuong = new TableSchema.TableColumn(schema);
				colvarSoLuong.ColumnName = "so_luong";
				colvarSoLuong.DataType = DbType.Int16;
				colvarSoLuong.MaxLength = 0;
				colvarSoLuong.AutoIncrement = false;
				colvarSoLuong.IsNullable = true;
				colvarSoLuong.IsPrimaryKey = false;
				colvarSoLuong.IsForeignKey = false;
				colvarSoLuong.IsReadOnly = false;
				colvarSoLuong.DefaultSetting = @"";
				colvarSoLuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLuong);
				
				TableSchema.TableColumn colvarLoaiDvu = new TableSchema.TableColumn(schema);
				colvarLoaiDvu.ColumnName = "loai_dvu";
				colvarLoaiDvu.DataType = DbType.Int16;
				colvarLoaiDvu.MaxLength = 0;
				colvarLoaiDvu.AutoIncrement = false;
				colvarLoaiDvu.IsNullable = true;
				colvarLoaiDvu.IsPrimaryKey = false;
				colvarLoaiDvu.IsForeignKey = false;
				colvarLoaiDvu.IsReadOnly = false;
				colvarLoaiDvu.DefaultSetting = @"";
				colvarLoaiDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoaiDvu);
				
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
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "trang_thai";
				colvarTrangThai.DataType = DbType.Byte;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = true;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				
						colvarTrangThai.DefaultSetting = @"((1))";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
				TableSchema.TableColumn colvarSoluongDung = new TableSchema.TableColumn(schema);
				colvarSoluongDung.ColumnName = "soluong_dung";
				colvarSoluongDung.DataType = DbType.Decimal;
				colvarSoluongDung.MaxLength = 0;
				colvarSoluongDung.AutoIncrement = false;
				colvarSoluongDung.IsNullable = true;
				colvarSoluongDung.IsPrimaryKey = false;
				colvarSoluongDung.IsForeignKey = false;
				colvarSoluongDung.IsReadOnly = false;
				colvarSoluongDung.DefaultSetting = @"";
				colvarSoluongDung.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoluongDung);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("goi_tinhtrangsudung",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdSudung")]
		[Bindable(true)]
		public int IdSudung 
		{
			get { return GetColumnValue<int>(Columns.IdSudung); }
			set { SetColumnValue(Columns.IdSudung, value); }
		}
		  
		[XmlAttribute("IdDangky")]
		[Bindable(true)]
		public int IdDangky 
		{
			get { return GetColumnValue<int>(Columns.IdDangky); }
			set { SetColumnValue(Columns.IdDangky, value); }
		}
		  
		[XmlAttribute("IdGoi")]
		[Bindable(true)]
		public int IdGoi 
		{
			get { return GetColumnValue<int>(Columns.IdGoi); }
			set { SetColumnValue(Columns.IdGoi, value); }
		}
		  
		[XmlAttribute("IdDichvu")]
		[Bindable(true)]
		public int IdDichvu 
		{
			get { return GetColumnValue<int>(Columns.IdDichvu); }
			set { SetColumnValue(Columns.IdDichvu, value); }
		}
		  
		[XmlAttribute("IdChitietdichvu")]
		[Bindable(true)]
		public int IdChitietdichvu 
		{
			get { return GetColumnValue<int>(Columns.IdChitietdichvu); }
			set { SetColumnValue(Columns.IdChitietdichvu, value); }
		}
		  
		[XmlAttribute("TenChitietdichvu")]
		[Bindable(true)]
		public string TenChitietdichvu 
		{
			get { return GetColumnValue<string>(Columns.TenChitietdichvu); }
			set { SetColumnValue(Columns.TenChitietdichvu, value); }
		}
		  
		[XmlAttribute("SoLuong")]
		[Bindable(true)]
		public short? SoLuong 
		{
			get { return GetColumnValue<short?>(Columns.SoLuong); }
			set { SetColumnValue(Columns.SoLuong, value); }
		}
		  
		[XmlAttribute("LoaiDvu")]
		[Bindable(true)]
		public short? LoaiDvu 
		{
			get { return GetColumnValue<short?>(Columns.LoaiDvu); }
			set { SetColumnValue(Columns.LoaiDvu, value); }
		}
		  
		[XmlAttribute("DonGia")]
		[Bindable(true)]
		public decimal? DonGia 
		{
			get { return GetColumnValue<decimal?>(Columns.DonGia); }
			set { SetColumnValue(Columns.DonGia, value); }
		}
		  
		[XmlAttribute("MienGiam")]
		[Bindable(true)]
		public decimal? MienGiam 
		{
			get { return GetColumnValue<decimal?>(Columns.MienGiam); }
			set { SetColumnValue(Columns.MienGiam, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public byte? TrangThai 
		{
			get { return GetColumnValue<byte?>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("SoluongDung")]
		[Bindable(true)]
		public decimal? SoluongDung 
		{
			get { return GetColumnValue<decimal?>(Columns.SoluongDung); }
			set { SetColumnValue(Columns.SoluongDung, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varIdDangky,int varIdGoi,int varIdDichvu,int varIdChitietdichvu,string varTenChitietdichvu,short? varSoLuong,short? varLoaiDvu,decimal? varDonGia,decimal? varMienGiam,byte? varTrangThai,decimal? varSoluongDung)
		{
			GoiTinhtrangsudung item = new GoiTinhtrangsudung();
			
			item.IdDangky = varIdDangky;
			
			item.IdGoi = varIdGoi;
			
			item.IdDichvu = varIdDichvu;
			
			item.IdChitietdichvu = varIdChitietdichvu;
			
			item.TenChitietdichvu = varTenChitietdichvu;
			
			item.SoLuong = varSoLuong;
			
			item.LoaiDvu = varLoaiDvu;
			
			item.DonGia = varDonGia;
			
			item.MienGiam = varMienGiam;
			
			item.TrangThai = varTrangThai;
			
			item.SoluongDung = varSoluongDung;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdSudung,int varIdDangky,int varIdGoi,int varIdDichvu,int varIdChitietdichvu,string varTenChitietdichvu,short? varSoLuong,short? varLoaiDvu,decimal? varDonGia,decimal? varMienGiam,byte? varTrangThai,decimal? varSoluongDung)
		{
			GoiTinhtrangsudung item = new GoiTinhtrangsudung();
			
				item.IdSudung = varIdSudung;
			
				item.IdDangky = varIdDangky;
			
				item.IdGoi = varIdGoi;
			
				item.IdDichvu = varIdDichvu;
			
				item.IdChitietdichvu = varIdChitietdichvu;
			
				item.TenChitietdichvu = varTenChitietdichvu;
			
				item.SoLuong = varSoLuong;
			
				item.LoaiDvu = varLoaiDvu;
			
				item.DonGia = varDonGia;
			
				item.MienGiam = varMienGiam;
			
				item.TrangThai = varTrangThai;
			
				item.SoluongDung = varSoluongDung;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdSudungColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdDangkyColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IdGoiColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdDichvuColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn IdChitietdichvuColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn TenChitietdichvuColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLuongColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiDvuColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn DonGiaColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn MienGiamColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn SoluongDungColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdSudung = @"id_sudung";
			 public static string IdDangky = @"id_dangky";
			 public static string IdGoi = @"id_goi";
			 public static string IdDichvu = @"id_dichvu";
			 public static string IdChitietdichvu = @"id_chitietdichvu";
			 public static string TenChitietdichvu = @"ten_chitietdichvu";
			 public static string SoLuong = @"so_luong";
			 public static string LoaiDvu = @"loai_dvu";
			 public static string DonGia = @"don_gia";
			 public static string MienGiam = @"mien_giam";
			 public static string TrangThai = @"trang_thai";
			 public static string SoluongDung = @"soluong_dung";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
