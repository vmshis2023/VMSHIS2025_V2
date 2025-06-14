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
	/// Strongly-typed collection for the NoitruPhieudichtruyen class.
	/// </summary>
    [Serializable]
	public partial class NoitruPhieudichtruyenCollection : ActiveList<NoitruPhieudichtruyen, NoitruPhieudichtruyenCollection>
	{	   
		public NoitruPhieudichtruyenCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>NoitruPhieudichtruyenCollection</returns>
		public NoitruPhieudichtruyenCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                NoitruPhieudichtruyen o = this[i];
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
	/// This is an ActiveRecord class which wraps the noitru_phieudichtruyen table.
	/// </summary>
	[Serializable]
	public partial class NoitruPhieudichtruyen : ActiveRecord<NoitruPhieudichtruyen>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public NoitruPhieudichtruyen()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public NoitruPhieudichtruyen(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public NoitruPhieudichtruyen(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public NoitruPhieudichtruyen(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("noitru_phieudichtruyen", TableType.Table, DataService.GetInstance("ORM"));
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
				colvarMaLuotkham.MaxLength = 10;
				colvarMaLuotkham.AutoIncrement = false;
				colvarMaLuotkham.IsNullable = false;
				colvarMaLuotkham.IsPrimaryKey = false;
				colvarMaLuotkham.IsForeignKey = false;
				colvarMaLuotkham.IsReadOnly = false;
				colvarMaLuotkham.DefaultSetting = @"";
				colvarMaLuotkham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaLuotkham);
				
				TableSchema.TableColumn colvarIdBuonggiuong = new TableSchema.TableColumn(schema);
				colvarIdBuonggiuong.ColumnName = "id_buonggiuong";
				colvarIdBuonggiuong.DataType = DbType.Int64;
				colvarIdBuonggiuong.MaxLength = 0;
				colvarIdBuonggiuong.AutoIncrement = false;
				colvarIdBuonggiuong.IsNullable = true;
				colvarIdBuonggiuong.IsPrimaryKey = false;
				colvarIdBuonggiuong.IsForeignKey = false;
				colvarIdBuonggiuong.IsReadOnly = false;
				colvarIdBuonggiuong.DefaultSetting = @"";
				colvarIdBuonggiuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBuonggiuong);
				
				TableSchema.TableColumn colvarIdKhoadieutri = new TableSchema.TableColumn(schema);
				colvarIdKhoadieutri.ColumnName = "id_khoadieutri";
				colvarIdKhoadieutri.DataType = DbType.Int32;
				colvarIdKhoadieutri.MaxLength = 0;
				colvarIdKhoadieutri.AutoIncrement = false;
				colvarIdKhoadieutri.IsNullable = false;
				colvarIdKhoadieutri.IsPrimaryKey = false;
				colvarIdKhoadieutri.IsForeignKey = false;
				colvarIdKhoadieutri.IsReadOnly = false;
				colvarIdKhoadieutri.DefaultSetting = @"";
				colvarIdKhoadieutri.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhoadieutri);
				
				TableSchema.TableColumn colvarIdBacsichidinh = new TableSchema.TableColumn(schema);
				colvarIdBacsichidinh.ColumnName = "id_bacsichidinh";
				colvarIdBacsichidinh.DataType = DbType.Int32;
				colvarIdBacsichidinh.MaxLength = 0;
				colvarIdBacsichidinh.AutoIncrement = false;
				colvarIdBacsichidinh.IsNullable = false;
				colvarIdBacsichidinh.IsPrimaryKey = false;
				colvarIdBacsichidinh.IsForeignKey = false;
				colvarIdBacsichidinh.IsReadOnly = false;
				colvarIdBacsichidinh.DefaultSetting = @"";
				colvarIdBacsichidinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBacsichidinh);
				
				TableSchema.TableColumn colvarIdYtathuchien = new TableSchema.TableColumn(schema);
				colvarIdYtathuchien.ColumnName = "id_ytathuchien";
				colvarIdYtathuchien.DataType = DbType.Int32;
				colvarIdYtathuchien.MaxLength = 0;
				colvarIdYtathuchien.AutoIncrement = false;
				colvarIdYtathuchien.IsNullable = false;
				colvarIdYtathuchien.IsPrimaryKey = false;
				colvarIdYtathuchien.IsForeignKey = false;
				colvarIdYtathuchien.IsReadOnly = false;
				colvarIdYtathuchien.DefaultSetting = @"";
				colvarIdYtathuchien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdYtathuchien);
				
				TableSchema.TableColumn colvarSoLuong = new TableSchema.TableColumn(schema);
				colvarSoLuong.ColumnName = "so_luong";
				colvarSoLuong.DataType = DbType.Int32;
				colvarSoLuong.MaxLength = 0;
				colvarSoLuong.AutoIncrement = false;
				colvarSoLuong.IsNullable = false;
				colvarSoLuong.IsPrimaryKey = false;
				colvarSoLuong.IsForeignKey = false;
				colvarSoLuong.IsReadOnly = false;
				colvarSoLuong.DefaultSetting = @"";
				colvarSoLuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLuong);
				
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
				
				TableSchema.TableColumn colvarTocDo = new TableSchema.TableColumn(schema);
				colvarTocDo.ColumnName = "toc_do";
				colvarTocDo.DataType = DbType.Int32;
				colvarTocDo.MaxLength = 0;
				colvarTocDo.AutoIncrement = false;
				colvarTocDo.IsNullable = false;
				colvarTocDo.IsPrimaryKey = false;
				colvarTocDo.IsForeignKey = false;
				colvarTocDo.IsReadOnly = false;
				colvarTocDo.DefaultSetting = @"";
				colvarTocDo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTocDo);
				
				TableSchema.TableColumn colvarThoigianBatdau = new TableSchema.TableColumn(schema);
				colvarThoigianBatdau.ColumnName = "thoigian_batdau";
				colvarThoigianBatdau.DataType = DbType.DateTime;
				colvarThoigianBatdau.MaxLength = 0;
				colvarThoigianBatdau.AutoIncrement = false;
				colvarThoigianBatdau.IsNullable = false;
				colvarThoigianBatdau.IsPrimaryKey = false;
				colvarThoigianBatdau.IsForeignKey = false;
				colvarThoigianBatdau.IsReadOnly = false;
				colvarThoigianBatdau.DefaultSetting = @"";
				colvarThoigianBatdau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarThoigianBatdau);
				
				TableSchema.TableColumn colvarThoigianKetthuc = new TableSchema.TableColumn(schema);
				colvarThoigianKetthuc.ColumnName = "thoigian_ketthuc";
				colvarThoigianKetthuc.DataType = DbType.DateTime;
				colvarThoigianKetthuc.MaxLength = 0;
				colvarThoigianKetthuc.AutoIncrement = false;
				colvarThoigianKetthuc.IsNullable = false;
				colvarThoigianKetthuc.IsPrimaryKey = false;
				colvarThoigianKetthuc.IsForeignKey = false;
				colvarThoigianKetthuc.IsReadOnly = false;
				colvarThoigianKetthuc.DefaultSetting = @"";
				colvarThoigianKetthuc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarThoigianKetthuc);
				
				TableSchema.TableColumn colvarNguoiThuchien = new TableSchema.TableColumn(schema);
				colvarNguoiThuchien.ColumnName = "nguoi_thuchien";
				colvarNguoiThuchien.DataType = DbType.String;
				colvarNguoiThuchien.MaxLength = 30;
				colvarNguoiThuchien.AutoIncrement = false;
				colvarNguoiThuchien.IsNullable = false;
				colvarNguoiThuchien.IsPrimaryKey = false;
				colvarNguoiThuchien.IsForeignKey = false;
				colvarNguoiThuchien.IsReadOnly = false;
				colvarNguoiThuchien.DefaultSetting = @"";
				colvarNguoiThuchien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiThuchien);
				
				TableSchema.TableColumn colvarNgayThuchien = new TableSchema.TableColumn(schema);
				colvarNgayThuchien.ColumnName = "ngay_thuchien";
				colvarNgayThuchien.DataType = DbType.DateTime;
				colvarNgayThuchien.MaxLength = 0;
				colvarNgayThuchien.AutoIncrement = false;
				colvarNgayThuchien.IsNullable = false;
				colvarNgayThuchien.IsPrimaryKey = false;
				colvarNgayThuchien.IsForeignKey = false;
				colvarNgayThuchien.IsReadOnly = false;
				colvarNgayThuchien.DefaultSetting = @"";
				colvarNgayThuchien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayThuchien);
				
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
				colvarNguoiTao.MaxLength = 30;
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
				colvarNguoiSua.MaxLength = 30;
				colvarNguoiSua.AutoIncrement = false;
				colvarNguoiSua.IsNullable = true;
				colvarNguoiSua.IsPrimaryKey = false;
				colvarNguoiSua.IsForeignKey = false;
				colvarNguoiSua.IsReadOnly = false;
				colvarNguoiSua.DefaultSetting = @"";
				colvarNguoiSua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiSua);
				
				TableSchema.TableColumn colvarIdChitietdonthuoc = new TableSchema.TableColumn(schema);
				colvarIdChitietdonthuoc.ColumnName = "id_chitietdonthuoc";
				colvarIdChitietdonthuoc.DataType = DbType.Int64;
				colvarIdChitietdonthuoc.MaxLength = 0;
				colvarIdChitietdonthuoc.AutoIncrement = false;
				colvarIdChitietdonthuoc.IsNullable = false;
				colvarIdChitietdonthuoc.IsPrimaryKey = false;
				colvarIdChitietdonthuoc.IsForeignKey = false;
				colvarIdChitietdonthuoc.IsReadOnly = false;
				colvarIdChitietdonthuoc.DefaultSetting = @"";
				colvarIdChitietdonthuoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChitietdonthuoc);
				
				TableSchema.TableColumn colvarTrangthaiIn = new TableSchema.TableColumn(schema);
				colvarTrangthaiIn.ColumnName = "trangthai_in";
				colvarTrangthaiIn.DataType = DbType.Byte;
				colvarTrangthaiIn.MaxLength = 0;
				colvarTrangthaiIn.AutoIncrement = false;
				colvarTrangthaiIn.IsNullable = false;
				colvarTrangthaiIn.IsPrimaryKey = false;
				colvarTrangthaiIn.IsForeignKey = false;
				colvarTrangthaiIn.IsReadOnly = false;
				colvarTrangthaiIn.DefaultSetting = @"";
				colvarTrangthaiIn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangthaiIn);
				
				TableSchema.TableColumn colvarIdThuocKethop = new TableSchema.TableColumn(schema);
				colvarIdThuocKethop.ColumnName = "id_thuoc_kethop";
				colvarIdThuocKethop.DataType = DbType.String;
				colvarIdThuocKethop.MaxLength = 255;
				colvarIdThuocKethop.AutoIncrement = false;
				colvarIdThuocKethop.IsNullable = true;
				colvarIdThuocKethop.IsPrimaryKey = false;
				colvarIdThuocKethop.IsForeignKey = false;
				colvarIdThuocKethop.IsReadOnly = false;
				colvarIdThuocKethop.DefaultSetting = @"";
				colvarIdThuocKethop.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThuocKethop);
				
				TableSchema.TableColumn colvarIdDonthuoc = new TableSchema.TableColumn(schema);
				colvarIdDonthuoc.ColumnName = "id_donthuoc";
				colvarIdDonthuoc.DataType = DbType.Int64;
				colvarIdDonthuoc.MaxLength = 0;
				colvarIdDonthuoc.AutoIncrement = false;
				colvarIdDonthuoc.IsNullable = false;
				colvarIdDonthuoc.IsPrimaryKey = false;
				colvarIdDonthuoc.IsForeignKey = false;
				colvarIdDonthuoc.IsReadOnly = false;
				
						colvarIdDonthuoc.DefaultSetting = @"((0))";
				colvarIdDonthuoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdDonthuoc);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("noitru_phieudichtruyen",schema);
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
		  
		[XmlAttribute("IdBuonggiuong")]
		[Bindable(true)]
		public long? IdBuonggiuong 
		{
			get { return GetColumnValue<long?>(Columns.IdBuonggiuong); }
			set { SetColumnValue(Columns.IdBuonggiuong, value); }
		}
		  
		[XmlAttribute("IdKhoadieutri")]
		[Bindable(true)]
		public int IdKhoadieutri 
		{
			get { return GetColumnValue<int>(Columns.IdKhoadieutri); }
			set { SetColumnValue(Columns.IdKhoadieutri, value); }
		}
		  
		[XmlAttribute("IdBacsichidinh")]
		[Bindable(true)]
		public int IdBacsichidinh 
		{
			get { return GetColumnValue<int>(Columns.IdBacsichidinh); }
			set { SetColumnValue(Columns.IdBacsichidinh, value); }
		}
		  
		[XmlAttribute("IdYtathuchien")]
		[Bindable(true)]
		public int IdYtathuchien 
		{
			get { return GetColumnValue<int>(Columns.IdYtathuchien); }
			set { SetColumnValue(Columns.IdYtathuchien, value); }
		}
		  
		[XmlAttribute("SoLuong")]
		[Bindable(true)]
		public int SoLuong 
		{
			get { return GetColumnValue<int>(Columns.SoLuong); }
			set { SetColumnValue(Columns.SoLuong, value); }
		}
		  
		[XmlAttribute("IdThuoc")]
		[Bindable(true)]
		public int IdThuoc 
		{
			get { return GetColumnValue<int>(Columns.IdThuoc); }
			set { SetColumnValue(Columns.IdThuoc, value); }
		}
		  
		[XmlAttribute("TocDo")]
		[Bindable(true)]
		public int TocDo 
		{
			get { return GetColumnValue<int>(Columns.TocDo); }
			set { SetColumnValue(Columns.TocDo, value); }
		}
		  
		[XmlAttribute("ThoigianBatdau")]
		[Bindable(true)]
		public DateTime ThoigianBatdau 
		{
			get { return GetColumnValue<DateTime>(Columns.ThoigianBatdau); }
			set { SetColumnValue(Columns.ThoigianBatdau, value); }
		}
		  
		[XmlAttribute("ThoigianKetthuc")]
		[Bindable(true)]
		public DateTime ThoigianKetthuc 
		{
			get { return GetColumnValue<DateTime>(Columns.ThoigianKetthuc); }
			set { SetColumnValue(Columns.ThoigianKetthuc, value); }
		}
		  
		[XmlAttribute("NguoiThuchien")]
		[Bindable(true)]
		public string NguoiThuchien 
		{
			get { return GetColumnValue<string>(Columns.NguoiThuchien); }
			set { SetColumnValue(Columns.NguoiThuchien, value); }
		}
		  
		[XmlAttribute("NgayThuchien")]
		[Bindable(true)]
		public DateTime NgayThuchien 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayThuchien); }
			set { SetColumnValue(Columns.NgayThuchien, value); }
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
		  
		[XmlAttribute("IdChitietdonthuoc")]
		[Bindable(true)]
		public long IdChitietdonthuoc 
		{
			get { return GetColumnValue<long>(Columns.IdChitietdonthuoc); }
			set { SetColumnValue(Columns.IdChitietdonthuoc, value); }
		}
		  
		[XmlAttribute("TrangthaiIn")]
		[Bindable(true)]
		public byte TrangthaiIn 
		{
			get { return GetColumnValue<byte>(Columns.TrangthaiIn); }
			set { SetColumnValue(Columns.TrangthaiIn, value); }
		}
		  
		[XmlAttribute("IdThuocKethop")]
		[Bindable(true)]
		public string IdThuocKethop 
		{
			get { return GetColumnValue<string>(Columns.IdThuocKethop); }
			set { SetColumnValue(Columns.IdThuocKethop, value); }
		}
		  
		[XmlAttribute("IdDonthuoc")]
		[Bindable(true)]
		public long IdDonthuoc 
		{
			get { return GetColumnValue<long>(Columns.IdDonthuoc); }
			set { SetColumnValue(Columns.IdDonthuoc, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varIdBenhnhan,string varMaLuotkham,long? varIdBuonggiuong,int varIdKhoadieutri,int varIdBacsichidinh,int varIdYtathuchien,int varSoLuong,int varIdThuoc,int varTocDo,DateTime varThoigianBatdau,DateTime varThoigianKetthuc,string varNguoiThuchien,DateTime varNgayThuchien,DateTime varNgayTao,string varNguoiTao,DateTime? varNgaySua,string varNguoiSua,long varIdChitietdonthuoc,byte varTrangthaiIn,string varIdThuocKethop,long varIdDonthuoc)
		{
			NoitruPhieudichtruyen item = new NoitruPhieudichtruyen();
			
			item.IdBenhnhan = varIdBenhnhan;
			
			item.MaLuotkham = varMaLuotkham;
			
			item.IdBuonggiuong = varIdBuonggiuong;
			
			item.IdKhoadieutri = varIdKhoadieutri;
			
			item.IdBacsichidinh = varIdBacsichidinh;
			
			item.IdYtathuchien = varIdYtathuchien;
			
			item.SoLuong = varSoLuong;
			
			item.IdThuoc = varIdThuoc;
			
			item.TocDo = varTocDo;
			
			item.ThoigianBatdau = varThoigianBatdau;
			
			item.ThoigianKetthuc = varThoigianKetthuc;
			
			item.NguoiThuchien = varNguoiThuchien;
			
			item.NgayThuchien = varNgayThuchien;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgaySua = varNgaySua;
			
			item.NguoiSua = varNguoiSua;
			
			item.IdChitietdonthuoc = varIdChitietdonthuoc;
			
			item.TrangthaiIn = varTrangthaiIn;
			
			item.IdThuocKethop = varIdThuocKethop;
			
			item.IdDonthuoc = varIdDonthuoc;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varIdPhieu,long varIdBenhnhan,string varMaLuotkham,long? varIdBuonggiuong,int varIdKhoadieutri,int varIdBacsichidinh,int varIdYtathuchien,int varSoLuong,int varIdThuoc,int varTocDo,DateTime varThoigianBatdau,DateTime varThoigianKetthuc,string varNguoiThuchien,DateTime varNgayThuchien,DateTime varNgayTao,string varNguoiTao,DateTime? varNgaySua,string varNguoiSua,long varIdChitietdonthuoc,byte varTrangthaiIn,string varIdThuocKethop,long varIdDonthuoc)
		{
			NoitruPhieudichtruyen item = new NoitruPhieudichtruyen();
			
				item.IdPhieu = varIdPhieu;
			
				item.IdBenhnhan = varIdBenhnhan;
			
				item.MaLuotkham = varMaLuotkham;
			
				item.IdBuonggiuong = varIdBuonggiuong;
			
				item.IdKhoadieutri = varIdKhoadieutri;
			
				item.IdBacsichidinh = varIdBacsichidinh;
			
				item.IdYtathuchien = varIdYtathuchien;
			
				item.SoLuong = varSoLuong;
			
				item.IdThuoc = varIdThuoc;
			
				item.TocDo = varTocDo;
			
				item.ThoigianBatdau = varThoigianBatdau;
			
				item.ThoigianKetthuc = varThoigianKetthuc;
			
				item.NguoiThuchien = varNguoiThuchien;
			
				item.NgayThuchien = varNgayThuchien;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgaySua = varNgaySua;
			
				item.NguoiSua = varNguoiSua;
			
				item.IdChitietdonthuoc = varIdChitietdonthuoc;
			
				item.TrangthaiIn = varTrangthaiIn;
			
				item.IdThuocKethop = varIdThuocKethop;
			
				item.IdDonthuoc = varIdDonthuoc;
			
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
        
        
        
        public static TableSchema.TableColumn IdBuonggiuongColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhoadieutriColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn IdBacsichidinhColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn IdYtathuchienColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLuongColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThuocColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn TocDoColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn ThoigianBatdauColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn ThoigianKetthucColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiThuchienColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayThuchienColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn IdChitietdonthuocColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangthaiInColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThuocKethopColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn IdDonthuocColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdPhieu = @"id_phieu";
			 public static string IdBenhnhan = @"id_benhnhan";
			 public static string MaLuotkham = @"ma_luotkham";
			 public static string IdBuonggiuong = @"id_buonggiuong";
			 public static string IdKhoadieutri = @"id_khoadieutri";
			 public static string IdBacsichidinh = @"id_bacsichidinh";
			 public static string IdYtathuchien = @"id_ytathuchien";
			 public static string SoLuong = @"so_luong";
			 public static string IdThuoc = @"id_thuoc";
			 public static string TocDo = @"toc_do";
			 public static string ThoigianBatdau = @"thoigian_batdau";
			 public static string ThoigianKetthuc = @"thoigian_ketthuc";
			 public static string NguoiThuchien = @"nguoi_thuchien";
			 public static string NgayThuchien = @"ngay_thuchien";
			 public static string NgayTao = @"ngay_tao";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string NgaySua = @"ngay_sua";
			 public static string NguoiSua = @"nguoi_sua";
			 public static string IdChitietdonthuoc = @"id_chitietdonthuoc";
			 public static string TrangthaiIn = @"trangthai_in";
			 public static string IdThuocKethop = @"id_thuoc_kethop";
			 public static string IdDonthuoc = @"id_donthuoc";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
