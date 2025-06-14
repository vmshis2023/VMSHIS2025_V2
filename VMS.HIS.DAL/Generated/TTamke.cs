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
	/// Strongly-typed collection for the TTamke class.
	/// </summary>
    [Serializable]
	public partial class TTamkeCollection : ActiveList<TTamke, TTamkeCollection>
	{	   
		public TTamkeCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TTamkeCollection</returns>
		public TTamkeCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TTamke o = this[i];
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
	/// This is an ActiveRecord class which wraps the t_tamke table.
	/// </summary>
	[Serializable]
	public partial class TTamke : ActiveRecord<TTamke>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TTamke()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TTamke(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TTamke(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TTamke(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("t_tamke", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "Id";
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
				
				TableSchema.TableColumn colvarIdKho = new TableSchema.TableColumn(schema);
				colvarIdKho.ColumnName = "id_kho";
				colvarIdKho.DataType = DbType.Int16;
				colvarIdKho.MaxLength = 0;
				colvarIdKho.AutoIncrement = false;
				colvarIdKho.IsNullable = false;
				colvarIdKho.IsPrimaryKey = false;
				colvarIdKho.IsForeignKey = false;
				colvarIdKho.IsReadOnly = false;
				colvarIdKho.DefaultSetting = @"";
				colvarIdKho.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKho);
				
				TableSchema.TableColumn colvarIdThuockho = new TableSchema.TableColumn(schema);
				colvarIdThuockho.ColumnName = "id_thuockho";
				colvarIdThuockho.DataType = DbType.Int64;
				colvarIdThuockho.MaxLength = 0;
				colvarIdThuockho.AutoIncrement = false;
				colvarIdThuockho.IsNullable = false;
				colvarIdThuockho.IsPrimaryKey = false;
				colvarIdThuockho.IsForeignKey = false;
				colvarIdThuockho.IsReadOnly = false;
				colvarIdThuockho.DefaultSetting = @"";
				colvarIdThuockho.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThuockho);
				
				TableSchema.TableColumn colvarIdThuoc = new TableSchema.TableColumn(schema);
				colvarIdThuoc.ColumnName = "id_thuoc";
				colvarIdThuoc.DataType = DbType.Int32;
				colvarIdThuoc.MaxLength = 0;
				colvarIdThuoc.AutoIncrement = false;
				colvarIdThuoc.IsNullable = false;
				colvarIdThuoc.IsPrimaryKey = false;
				colvarIdThuoc.IsForeignKey = false;
				colvarIdThuoc.IsReadOnly = false;
				colvarIdThuoc.DefaultSetting = @"";
				colvarIdThuoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThuoc);
				
				TableSchema.TableColumn colvarSoLuong = new TableSchema.TableColumn(schema);
				colvarSoLuong.ColumnName = "so_luong";
				colvarSoLuong.DataType = DbType.Decimal;
				colvarSoLuong.MaxLength = 0;
				colvarSoLuong.AutoIncrement = false;
				colvarSoLuong.IsNullable = false;
				colvarSoLuong.IsPrimaryKey = false;
				colvarSoLuong.IsForeignKey = false;
				colvarSoLuong.IsReadOnly = false;
				colvarSoLuong.DefaultSetting = @"";
				colvarSoLuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLuong);
				
				TableSchema.TableColumn colvarIdPhieuCtiet = new TableSchema.TableColumn(schema);
				colvarIdPhieuCtiet.ColumnName = "id_phieu_ctiet";
				colvarIdPhieuCtiet.DataType = DbType.Int64;
				colvarIdPhieuCtiet.MaxLength = 0;
				colvarIdPhieuCtiet.AutoIncrement = false;
				colvarIdPhieuCtiet.IsNullable = false;
				colvarIdPhieuCtiet.IsPrimaryKey = false;
				colvarIdPhieuCtiet.IsForeignKey = false;
				colvarIdPhieuCtiet.IsReadOnly = false;
				colvarIdPhieuCtiet.DefaultSetting = @"";
				colvarIdPhieuCtiet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdPhieuCtiet);
				
				TableSchema.TableColumn colvarIdPhieu = new TableSchema.TableColumn(schema);
				colvarIdPhieu.ColumnName = "id_phieu";
				colvarIdPhieu.DataType = DbType.Int64;
				colvarIdPhieu.MaxLength = 0;
				colvarIdPhieu.AutoIncrement = false;
				colvarIdPhieu.IsNullable = true;
				colvarIdPhieu.IsPrimaryKey = false;
				colvarIdPhieu.IsForeignKey = false;
				colvarIdPhieu.IsReadOnly = false;
				colvarIdPhieu.DefaultSetting = @"";
				colvarIdPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdPhieu);
				
				TableSchema.TableColumn colvarLoai = new TableSchema.TableColumn(schema);
				colvarLoai.ColumnName = "loai";
				colvarLoai.DataType = DbType.Byte;
				colvarLoai.MaxLength = 0;
				colvarLoai.AutoIncrement = false;
				colvarLoai.IsNullable = false;
				colvarLoai.IsPrimaryKey = false;
				colvarLoai.IsForeignKey = false;
				colvarLoai.IsReadOnly = false;
				colvarLoai.DefaultSetting = @"";
				colvarLoai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoai);
				
				TableSchema.TableColumn colvarGuidKey = new TableSchema.TableColumn(schema);
				colvarGuidKey.ColumnName = "GuidKey";
				colvarGuidKey.DataType = DbType.String;
				colvarGuidKey.MaxLength = 50;
				colvarGuidKey.AutoIncrement = false;
				colvarGuidKey.IsNullable = false;
				colvarGuidKey.IsPrimaryKey = false;
				colvarGuidKey.IsForeignKey = false;
				colvarGuidKey.IsReadOnly = false;
				colvarGuidKey.DefaultSetting = @"";
				colvarGuidKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGuidKey);
				
				TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
				colvarNgayTao.ColumnName = "ngay_tao";
				colvarNgayTao.DataType = DbType.DateTime;
				colvarNgayTao.MaxLength = 0;
				colvarNgayTao.AutoIncrement = false;
				colvarNgayTao.IsNullable = true;
				colvarNgayTao.IsPrimaryKey = false;
				colvarNgayTao.IsForeignKey = false;
				colvarNgayTao.IsReadOnly = false;
				
						colvarNgayTao.DefaultSetting = @"(getdate())";
				colvarNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTao);
				
				TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
				colvarNguoiTao.ColumnName = "nguoi_tao";
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
				
				TableSchema.TableColumn colvarMaLuotkham = new TableSchema.TableColumn(schema);
				colvarMaLuotkham.ColumnName = "ma_luotkham";
				colvarMaLuotkham.DataType = DbType.String;
				colvarMaLuotkham.MaxLength = 10;
				colvarMaLuotkham.AutoIncrement = false;
				colvarMaLuotkham.IsNullable = true;
				colvarMaLuotkham.IsPrimaryKey = false;
				colvarMaLuotkham.IsForeignKey = false;
				colvarMaLuotkham.IsReadOnly = false;
				colvarMaLuotkham.DefaultSetting = @"";
				colvarMaLuotkham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaLuotkham);
				
				TableSchema.TableColumn colvarIdBenhnhan = new TableSchema.TableColumn(schema);
				colvarIdBenhnhan.ColumnName = "id_benhnhan";
				colvarIdBenhnhan.DataType = DbType.Int64;
				colvarIdBenhnhan.MaxLength = 0;
				colvarIdBenhnhan.AutoIncrement = false;
				colvarIdBenhnhan.IsNullable = true;
				colvarIdBenhnhan.IsPrimaryKey = false;
				colvarIdBenhnhan.IsForeignKey = false;
				colvarIdBenhnhan.IsReadOnly = false;
				colvarIdBenhnhan.DefaultSetting = @"";
				colvarIdBenhnhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBenhnhan);
				
				TableSchema.TableColumn colvarNoitru = new TableSchema.TableColumn(schema);
				colvarNoitru.ColumnName = "noitru";
				colvarNoitru.DataType = DbType.Byte;
				colvarNoitru.MaxLength = 0;
				colvarNoitru.AutoIncrement = false;
				colvarNoitru.IsNullable = true;
				colvarNoitru.IsPrimaryKey = false;
				colvarNoitru.IsForeignKey = false;
				colvarNoitru.IsReadOnly = false;
				colvarNoitru.DefaultSetting = @"";
				colvarNoitru.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNoitru);
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "trang_thai";
				colvarTrangThai.DataType = DbType.Boolean;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = true;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				
						colvarTrangThai.DefaultSetting = @"((0))";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
				TableSchema.TableColumn colvarIpMay = new TableSchema.TableColumn(schema);
				colvarIpMay.ColumnName = "ip_may";
				colvarIpMay.DataType = DbType.AnsiString;
				colvarIpMay.MaxLength = 20;
				colvarIpMay.AutoIncrement = false;
				colvarIpMay.IsNullable = true;
				colvarIpMay.IsPrimaryKey = false;
				colvarIpMay.IsForeignKey = false;
				colvarIpMay.IsReadOnly = false;
				colvarIpMay.DefaultSetting = @"";
				colvarIpMay.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIpMay);
				
				TableSchema.TableColumn colvarMotaThem = new TableSchema.TableColumn(schema);
				colvarMotaThem.ColumnName = "mota_them";
				colvarMotaThem.DataType = DbType.String;
				colvarMotaThem.MaxLength = 255;
				colvarMotaThem.AutoIncrement = false;
				colvarMotaThem.IsNullable = true;
				colvarMotaThem.IsPrimaryKey = false;
				colvarMotaThem.IsForeignKey = false;
				colvarMotaThem.IsReadOnly = false;
				colvarMotaThem.DefaultSetting = @"";
				colvarMotaThem.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMotaThem);
				
				TableSchema.TableColumn colvarPrivateGuid = new TableSchema.TableColumn(schema);
				colvarPrivateGuid.ColumnName = "private_guid";
				colvarPrivateGuid.DataType = DbType.String;
				colvarPrivateGuid.MaxLength = 50;
				colvarPrivateGuid.AutoIncrement = false;
				colvarPrivateGuid.IsNullable = true;
				colvarPrivateGuid.IsPrimaryKey = false;
				colvarPrivateGuid.IsForeignKey = false;
				colvarPrivateGuid.IsReadOnly = false;
				colvarPrivateGuid.DefaultSetting = @"";
				colvarPrivateGuid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPrivateGuid);
				
				TableSchema.TableColumn colvarNgayNhaton = new TableSchema.TableColumn(schema);
				colvarNgayNhaton.ColumnName = "ngay_nhaton";
				colvarNgayNhaton.DataType = DbType.DateTime;
				colvarNgayNhaton.MaxLength = 0;
				colvarNgayNhaton.AutoIncrement = false;
				colvarNgayNhaton.IsNullable = true;
				colvarNgayNhaton.IsPrimaryKey = false;
				colvarNgayNhaton.IsForeignKey = false;
				colvarNgayNhaton.IsReadOnly = false;
				colvarNgayNhaton.DefaultSetting = @"";
				colvarNgayNhaton.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayNhaton);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("t_tamke",schema);
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
		  
		[XmlAttribute("IdKho")]
		[Bindable(true)]
		public short IdKho 
		{
			get { return GetColumnValue<short>(Columns.IdKho); }
			set { SetColumnValue(Columns.IdKho, value); }
		}
		  
		[XmlAttribute("IdThuockho")]
		[Bindable(true)]
		public long IdThuockho 
		{
			get { return GetColumnValue<long>(Columns.IdThuockho); }
			set { SetColumnValue(Columns.IdThuockho, value); }
		}
		  
		[XmlAttribute("IdThuoc")]
		[Bindable(true)]
		public int IdThuoc 
		{
			get { return GetColumnValue<int>(Columns.IdThuoc); }
			set { SetColumnValue(Columns.IdThuoc, value); }
		}
		  
		[XmlAttribute("SoLuong")]
		[Bindable(true)]
		public decimal SoLuong 
		{
			get { return GetColumnValue<decimal>(Columns.SoLuong); }
			set { SetColumnValue(Columns.SoLuong, value); }
		}
		  
		[XmlAttribute("IdPhieuCtiet")]
		[Bindable(true)]
		public long IdPhieuCtiet 
		{
			get { return GetColumnValue<long>(Columns.IdPhieuCtiet); }
			set { SetColumnValue(Columns.IdPhieuCtiet, value); }
		}
		  
		[XmlAttribute("IdPhieu")]
		[Bindable(true)]
		public long? IdPhieu 
		{
			get { return GetColumnValue<long?>(Columns.IdPhieu); }
			set { SetColumnValue(Columns.IdPhieu, value); }
		}
		  
		[XmlAttribute("Loai")]
		[Bindable(true)]
		public byte Loai 
		{
			get { return GetColumnValue<byte>(Columns.Loai); }
			set { SetColumnValue(Columns.Loai, value); }
		}
		  
		[XmlAttribute("GuidKey")]
		[Bindable(true)]
		public string GuidKey 
		{
			get { return GetColumnValue<string>(Columns.GuidKey); }
			set { SetColumnValue(Columns.GuidKey, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime? NgayTao 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		  
		[XmlAttribute("NguoiTao")]
		[Bindable(true)]
		public string NguoiTao 
		{
			get { return GetColumnValue<string>(Columns.NguoiTao); }
			set { SetColumnValue(Columns.NguoiTao, value); }
		}
		  
		[XmlAttribute("MaLuotkham")]
		[Bindable(true)]
		public string MaLuotkham 
		{
			get { return GetColumnValue<string>(Columns.MaLuotkham); }
			set { SetColumnValue(Columns.MaLuotkham, value); }
		}
		  
		[XmlAttribute("IdBenhnhan")]
		[Bindable(true)]
		public long? IdBenhnhan 
		{
			get { return GetColumnValue<long?>(Columns.IdBenhnhan); }
			set { SetColumnValue(Columns.IdBenhnhan, value); }
		}
		  
		[XmlAttribute("Noitru")]
		[Bindable(true)]
		public byte? Noitru 
		{
			get { return GetColumnValue<byte?>(Columns.Noitru); }
			set { SetColumnValue(Columns.Noitru, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public bool? TrangThai 
		{
			get { return GetColumnValue<bool?>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("IpMay")]
		[Bindable(true)]
		public string IpMay 
		{
			get { return GetColumnValue<string>(Columns.IpMay); }
			set { SetColumnValue(Columns.IpMay, value); }
		}
		  
		[XmlAttribute("MotaThem")]
		[Bindable(true)]
		public string MotaThem 
		{
			get { return GetColumnValue<string>(Columns.MotaThem); }
			set { SetColumnValue(Columns.MotaThem, value); }
		}
		  
		[XmlAttribute("PrivateGuid")]
		[Bindable(true)]
		public string PrivateGuid 
		{
			get { return GetColumnValue<string>(Columns.PrivateGuid); }
			set { SetColumnValue(Columns.PrivateGuid, value); }
		}
		  
		[XmlAttribute("NgayNhaton")]
		[Bindable(true)]
		public DateTime? NgayNhaton 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayNhaton); }
			set { SetColumnValue(Columns.NgayNhaton, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(short varIdKho,long varIdThuockho,int varIdThuoc,decimal varSoLuong,long varIdPhieuCtiet,long? varIdPhieu,byte varLoai,string varGuidKey,DateTime? varNgayTao,string varNguoiTao,string varMaLuotkham,long? varIdBenhnhan,byte? varNoitru,bool? varTrangThai,string varIpMay,string varMotaThem,string varPrivateGuid,DateTime? varNgayNhaton)
		{
			TTamke item = new TTamke();
			
			item.IdKho = varIdKho;
			
			item.IdThuockho = varIdThuockho;
			
			item.IdThuoc = varIdThuoc;
			
			item.SoLuong = varSoLuong;
			
			item.IdPhieuCtiet = varIdPhieuCtiet;
			
			item.IdPhieu = varIdPhieu;
			
			item.Loai = varLoai;
			
			item.GuidKey = varGuidKey;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiTao = varNguoiTao;
			
			item.MaLuotkham = varMaLuotkham;
			
			item.IdBenhnhan = varIdBenhnhan;
			
			item.Noitru = varNoitru;
			
			item.TrangThai = varTrangThai;
			
			item.IpMay = varIpMay;
			
			item.MotaThem = varMotaThem;
			
			item.PrivateGuid = varPrivateGuid;
			
			item.NgayNhaton = varNgayNhaton;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varId,short varIdKho,long varIdThuockho,int varIdThuoc,decimal varSoLuong,long varIdPhieuCtiet,long? varIdPhieu,byte varLoai,string varGuidKey,DateTime? varNgayTao,string varNguoiTao,string varMaLuotkham,long? varIdBenhnhan,byte? varNoitru,bool? varTrangThai,string varIpMay,string varMotaThem,string varPrivateGuid,DateTime? varNgayNhaton)
		{
			TTamke item = new TTamke();
			
				item.Id = varId;
			
				item.IdKho = varIdKho;
			
				item.IdThuockho = varIdThuockho;
			
				item.IdThuoc = varIdThuoc;
			
				item.SoLuong = varSoLuong;
			
				item.IdPhieuCtiet = varIdPhieuCtiet;
			
				item.IdPhieu = varIdPhieu;
			
				item.Loai = varLoai;
			
				item.GuidKey = varGuidKey;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiTao = varNguoiTao;
			
				item.MaLuotkham = varMaLuotkham;
			
				item.IdBenhnhan = varIdBenhnhan;
			
				item.Noitru = varNoitru;
			
				item.TrangThai = varTrangThai;
			
				item.IpMay = varIpMay;
			
				item.MotaThem = varMotaThem;
			
				item.PrivateGuid = varPrivateGuid;
			
				item.NgayNhaton = varNgayNhaton;
			
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
        
        
        
        public static TableSchema.TableColumn IdKhoColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThuockhoColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThuocColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLuongColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn IdPhieuCtietColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn IdPhieuColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn GuidKeyColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn MaLuotkhamColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn IdBenhnhanColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn NoitruColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn IpMayColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn MotaThemColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn PrivateGuidColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayNhatonColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string IdKho = @"id_kho";
			 public static string IdThuockho = @"id_thuockho";
			 public static string IdThuoc = @"id_thuoc";
			 public static string SoLuong = @"so_luong";
			 public static string IdPhieuCtiet = @"id_phieu_ctiet";
			 public static string IdPhieu = @"id_phieu";
			 public static string Loai = @"loai";
			 public static string GuidKey = @"GuidKey";
			 public static string NgayTao = @"ngay_tao";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string MaLuotkham = @"ma_luotkham";
			 public static string IdBenhnhan = @"id_benhnhan";
			 public static string Noitru = @"noitru";
			 public static string TrangThai = @"trang_thai";
			 public static string IpMay = @"ip_may";
			 public static string MotaThem = @"mota_them";
			 public static string PrivateGuid = @"private_guid";
			 public static string NgayNhaton = @"ngay_nhaton";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
