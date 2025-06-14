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
	/// Strongly-typed collection for the LLoaiXn class.
	/// </summary>
    [Serializable]
	public partial class LLoaiXnCollection : ActiveList<LLoaiXn, LLoaiXnCollection>
	{	   
		public LLoaiXnCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LLoaiXnCollection</returns>
		public LLoaiXnCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LLoaiXn o = this[i];
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
	/// This is an ActiveRecord class which wraps the L_LOAI_XN table.
	/// </summary>
	[Serializable]
	public partial class LLoaiXn : ActiveRecord<LLoaiXn>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LLoaiXn()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LLoaiXn(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LLoaiXn(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LLoaiXn(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("L_LOAI_XN", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdLoaiXn = new TableSchema.TableColumn(schema);
				colvarIdLoaiXn.ColumnName = "ID_Loai_XN";
				colvarIdLoaiXn.DataType = DbType.Int16;
				colvarIdLoaiXn.MaxLength = 0;
				colvarIdLoaiXn.AutoIncrement = true;
				colvarIdLoaiXn.IsNullable = false;
				colvarIdLoaiXn.IsPrimaryKey = true;
				colvarIdLoaiXn.IsForeignKey = false;
				colvarIdLoaiXn.IsReadOnly = false;
				colvarIdLoaiXn.DefaultSetting = @"";
				colvarIdLoaiXn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdLoaiXn);
				
				TableSchema.TableColumn colvarMaXn = new TableSchema.TableColumn(schema);
				colvarMaXn.ColumnName = "Ma_XN";
				colvarMaXn.DataType = DbType.String;
				colvarMaXn.MaxLength = 10;
				colvarMaXn.AutoIncrement = false;
				colvarMaXn.IsNullable = false;
				colvarMaXn.IsPrimaryKey = false;
				colvarMaXn.IsForeignKey = false;
				colvarMaXn.IsReadOnly = false;
				colvarMaXn.DefaultSetting = @"";
				colvarMaXn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaXn);
				
				TableSchema.TableColumn colvarTenLoaiXn = new TableSchema.TableColumn(schema);
				colvarTenLoaiXn.ColumnName = "Ten_Loai_XN";
				colvarTenLoaiXn.DataType = DbType.String;
				colvarTenLoaiXn.MaxLength = 50;
				colvarTenLoaiXn.AutoIncrement = false;
				colvarTenLoaiXn.IsNullable = false;
				colvarTenLoaiXn.IsPrimaryKey = false;
				colvarTenLoaiXn.IsForeignKey = false;
				colvarTenLoaiXn.IsReadOnly = false;
				colvarTenLoaiXn.DefaultSetting = @"";
				colvarTenLoaiXn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenLoaiXn);
				
				TableSchema.TableColumn colvarXnChitiet = new TableSchema.TableColumn(schema);
				colvarXnChitiet.ColumnName = "XN_CHITIET";
				colvarXnChitiet.DataType = DbType.Byte;
				colvarXnChitiet.MaxLength = 0;
				colvarXnChitiet.AutoIncrement = false;
				colvarXnChitiet.IsNullable = true;
				colvarXnChitiet.IsPrimaryKey = false;
				colvarXnChitiet.IsForeignKey = false;
				colvarXnChitiet.IsReadOnly = false;
				colvarXnChitiet.DefaultSetting = @"";
				colvarXnChitiet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarXnChitiet);
				
				TableSchema.TableColumn colvarHienThi = new TableSchema.TableColumn(schema);
				colvarHienThi.ColumnName = "HIEN_THI";
				colvarHienThi.DataType = DbType.Byte;
				colvarHienThi.MaxLength = 0;
				colvarHienThi.AutoIncrement = false;
				colvarHienThi.IsNullable = true;
				colvarHienThi.IsPrimaryKey = false;
				colvarHienThi.IsForeignKey = false;
				colvarHienThi.IsReadOnly = false;
				colvarHienThi.DefaultSetting = @"";
				colvarHienThi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHienThi);
				
				TableSchema.TableColumn colvarMacDinhChon = new TableSchema.TableColumn(schema);
				colvarMacDinhChon.ColumnName = "MAC_DINH_CHON";
				colvarMacDinhChon.DataType = DbType.Byte;
				colvarMacDinhChon.MaxLength = 0;
				colvarMacDinhChon.AutoIncrement = false;
				colvarMacDinhChon.IsNullable = true;
				colvarMacDinhChon.IsPrimaryKey = false;
				colvarMacDinhChon.IsForeignKey = false;
				colvarMacDinhChon.IsReadOnly = false;
				colvarMacDinhChon.DefaultSetting = @"";
				colvarMacDinhChon.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMacDinhChon);
				
				TableSchema.TableColumn colvarSoThuTu = new TableSchema.TableColumn(schema);
				colvarSoThuTu.ColumnName = "So_Thu_Tu";
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
				
				TableSchema.TableColumn colvarMoTa = new TableSchema.TableColumn(schema);
				colvarMoTa.ColumnName = "Mo_Ta";
				colvarMoTa.DataType = DbType.String;
				colvarMoTa.MaxLength = 50;
				colvarMoTa.AutoIncrement = false;
				colvarMoTa.IsNullable = true;
				colvarMoTa.IsPrimaryKey = false;
				colvarMoTa.IsForeignKey = false;
				colvarMoTa.IsReadOnly = false;
				colvarMoTa.DefaultSetting = @"";
				colvarMoTa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMoTa);
				
				TableSchema.TableColumn colvarDonGia = new TableSchema.TableColumn(schema);
				colvarDonGia.ColumnName = "DON_GIA";
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
				
				TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
				colvarNguoiTao.ColumnName = "NGUOI_TAO";
				colvarNguoiTao.DataType = DbType.String;
				colvarNguoiTao.MaxLength = 20;
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
				colvarNguoiSua.MaxLength = 20;
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
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("L_LOAI_XN",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdLoaiXn")]
		[Bindable(true)]
		public short IdLoaiXn 
		{
			get { return GetColumnValue<short>(Columns.IdLoaiXn); }
			set { SetColumnValue(Columns.IdLoaiXn, value); }
		}
		  
		[XmlAttribute("MaXn")]
		[Bindable(true)]
		public string MaXn 
		{
			get { return GetColumnValue<string>(Columns.MaXn); }
			set { SetColumnValue(Columns.MaXn, value); }
		}
		  
		[XmlAttribute("TenLoaiXn")]
		[Bindable(true)]
		public string TenLoaiXn 
		{
			get { return GetColumnValue<string>(Columns.TenLoaiXn); }
			set { SetColumnValue(Columns.TenLoaiXn, value); }
		}
		  
		[XmlAttribute("XnChitiet")]
		[Bindable(true)]
		public byte? XnChitiet 
		{
			get { return GetColumnValue<byte?>(Columns.XnChitiet); }
			set { SetColumnValue(Columns.XnChitiet, value); }
		}
		  
		[XmlAttribute("HienThi")]
		[Bindable(true)]
		public byte? HienThi 
		{
			get { return GetColumnValue<byte?>(Columns.HienThi); }
			set { SetColumnValue(Columns.HienThi, value); }
		}
		  
		[XmlAttribute("MacDinhChon")]
		[Bindable(true)]
		public byte? MacDinhChon 
		{
			get { return GetColumnValue<byte?>(Columns.MacDinhChon); }
			set { SetColumnValue(Columns.MacDinhChon, value); }
		}
		  
		[XmlAttribute("SoThuTu")]
		[Bindable(true)]
		public int? SoThuTu 
		{
			get { return GetColumnValue<int?>(Columns.SoThuTu); }
			set { SetColumnValue(Columns.SoThuTu, value); }
		}
		  
		[XmlAttribute("MoTa")]
		[Bindable(true)]
		public string MoTa 
		{
			get { return GetColumnValue<string>(Columns.MoTa); }
			set { SetColumnValue(Columns.MoTa, value); }
		}
		  
		[XmlAttribute("DonGia")]
		[Bindable(true)]
		public decimal? DonGia 
		{
			get { return GetColumnValue<decimal?>(Columns.DonGia); }
			set { SetColumnValue(Columns.DonGia, value); }
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
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaXn,string varTenLoaiXn,byte? varXnChitiet,byte? varHienThi,byte? varMacDinhChon,int? varSoThuTu,string varMoTa,decimal? varDonGia,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			LLoaiXn item = new LLoaiXn();
			
			item.MaXn = varMaXn;
			
			item.TenLoaiXn = varTenLoaiXn;
			
			item.XnChitiet = varXnChitiet;
			
			item.HienThi = varHienThi;
			
			item.MacDinhChon = varMacDinhChon;
			
			item.SoThuTu = varSoThuTu;
			
			item.MoTa = varMoTa;
			
			item.DonGia = varDonGia;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiSua = varNguoiSua;
			
			item.NgaySua = varNgaySua;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(short varIdLoaiXn,string varMaXn,string varTenLoaiXn,byte? varXnChitiet,byte? varHienThi,byte? varMacDinhChon,int? varSoThuTu,string varMoTa,decimal? varDonGia,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			LLoaiXn item = new LLoaiXn();
			
				item.IdLoaiXn = varIdLoaiXn;
			
				item.MaXn = varMaXn;
			
				item.TenLoaiXn = varTenLoaiXn;
			
				item.XnChitiet = varXnChitiet;
			
				item.HienThi = varHienThi;
			
				item.MacDinhChon = varMacDinhChon;
			
				item.SoThuTu = varSoThuTu;
			
				item.MoTa = varMoTa;
			
				item.DonGia = varDonGia;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiSua = varNguoiSua;
			
				item.NgaySua = varNgaySua;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdLoaiXnColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaXnColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TenLoaiXnColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn XnChitietColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn HienThiColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn MacDinhChonColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn SoThuTuColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn MoTaColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn DonGiaColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdLoaiXn = @"ID_Loai_XN";
			 public static string MaXn = @"Ma_XN";
			 public static string TenLoaiXn = @"Ten_Loai_XN";
			 public static string XnChitiet = @"XN_CHITIET";
			 public static string HienThi = @"HIEN_THI";
			 public static string MacDinhChon = @"MAC_DINH_CHON";
			 public static string SoThuTu = @"So_Thu_Tu";
			 public static string MoTa = @"Mo_Ta";
			 public static string DonGia = @"DON_GIA";
			 public static string NguoiTao = @"NGUOI_TAO";
			 public static string NgayTao = @"NGAY_TAO";
			 public static string NguoiSua = @"NGUOI_SUA";
			 public static string NgaySua = @"NGAY_SUA";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
