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
	/// Strongly-typed collection for the TThau class.
	/// </summary>
    [Serializable]
	public partial class TThauCollection : ActiveList<TThau, TThauCollection>
	{	   
		public TThauCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TThauCollection</returns>
		public TThauCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TThau o = this[i];
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
	/// This is an ActiveRecord class which wraps the t_thau table.
	/// </summary>
	[Serializable]
	public partial class TThau : ActiveRecord<TThau>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TThau()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TThau(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TThau(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TThau(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("t_thau", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdThau = new TableSchema.TableColumn(schema);
				colvarIdThau.ColumnName = "id_thau";
				colvarIdThau.DataType = DbType.Int64;
				colvarIdThau.MaxLength = 0;
				colvarIdThau.AutoIncrement = true;
				colvarIdThau.IsNullable = false;
				colvarIdThau.IsPrimaryKey = true;
				colvarIdThau.IsForeignKey = false;
				colvarIdThau.IsReadOnly = false;
				colvarIdThau.DefaultSetting = @"";
				colvarIdThau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThau);
				
				TableSchema.TableColumn colvarLoaiThau = new TableSchema.TableColumn(schema);
				colvarLoaiThau.ColumnName = "loai_thau";
				colvarLoaiThau.DataType = DbType.String;
				colvarLoaiThau.MaxLength = 50;
				colvarLoaiThau.AutoIncrement = false;
				colvarLoaiThau.IsNullable = false;
				colvarLoaiThau.IsPrimaryKey = false;
				colvarLoaiThau.IsForeignKey = false;
				colvarLoaiThau.IsReadOnly = false;
				colvarLoaiThau.DefaultSetting = @"";
				colvarLoaiThau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoaiThau);
				
				TableSchema.TableColumn colvarNhomThau = new TableSchema.TableColumn(schema);
				colvarNhomThau.ColumnName = "nhom_thau";
				colvarNhomThau.DataType = DbType.String;
				colvarNhomThau.MaxLength = 50;
				colvarNhomThau.AutoIncrement = false;
				colvarNhomThau.IsNullable = false;
				colvarNhomThau.IsPrimaryKey = false;
				colvarNhomThau.IsForeignKey = false;
				colvarNhomThau.IsReadOnly = false;
				colvarNhomThau.DefaultSetting = @"";
				colvarNhomThau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNhomThau);
				
				TableSchema.TableColumn colvarGoiThau = new TableSchema.TableColumn(schema);
				colvarGoiThau.ColumnName = "goi_thau";
				colvarGoiThau.DataType = DbType.String;
				colvarGoiThau.MaxLength = 50;
				colvarGoiThau.AutoIncrement = false;
				colvarGoiThau.IsNullable = false;
				colvarGoiThau.IsPrimaryKey = false;
				colvarGoiThau.IsForeignKey = false;
				colvarGoiThau.IsReadOnly = false;
				colvarGoiThau.DefaultSetting = @"";
				colvarGoiThau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGoiThau);
				
				TableSchema.TableColumn colvarSoThau = new TableSchema.TableColumn(schema);
				colvarSoThau.ColumnName = "so_thau";
				colvarSoThau.DataType = DbType.String;
				colvarSoThau.MaxLength = 50;
				colvarSoThau.AutoIncrement = false;
				colvarSoThau.IsNullable = false;
				colvarSoThau.IsPrimaryKey = false;
				colvarSoThau.IsForeignKey = false;
				colvarSoThau.IsReadOnly = false;
				colvarSoThau.DefaultSetting = @"";
				colvarSoThau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoThau);
				
				TableSchema.TableColumn colvarNgaythauTu = new TableSchema.TableColumn(schema);
				colvarNgaythauTu.ColumnName = "ngaythau_tu";
				colvarNgaythauTu.DataType = DbType.DateTime;
				colvarNgaythauTu.MaxLength = 0;
				colvarNgaythauTu.AutoIncrement = false;
				colvarNgaythauTu.IsNullable = false;
				colvarNgaythauTu.IsPrimaryKey = false;
				colvarNgaythauTu.IsForeignKey = false;
				colvarNgaythauTu.IsReadOnly = false;
				
						colvarNgaythauTu.DefaultSetting = @"(getdate())";
				colvarNgaythauTu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgaythauTu);
				
				TableSchema.TableColumn colvarNgaythauDen = new TableSchema.TableColumn(schema);
				colvarNgaythauDen.ColumnName = "ngaythau_den";
				colvarNgaythauDen.DataType = DbType.DateTime;
				colvarNgaythauDen.MaxLength = 0;
				colvarNgaythauDen.AutoIncrement = false;
				colvarNgaythauDen.IsNullable = true;
				colvarNgaythauDen.IsPrimaryKey = false;
				colvarNgaythauDen.IsForeignKey = false;
				colvarNgaythauDen.IsReadOnly = false;
				colvarNgaythauDen.DefaultSetting = @"";
				colvarNgaythauDen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgaythauDen);
				
				TableSchema.TableColumn colvarSoQuyetdinh = new TableSchema.TableColumn(schema);
				colvarSoQuyetdinh.ColumnName = "so_quyetdinh";
				colvarSoQuyetdinh.DataType = DbType.String;
				colvarSoQuyetdinh.MaxLength = 50;
				colvarSoQuyetdinh.AutoIncrement = false;
				colvarSoQuyetdinh.IsNullable = false;
				colvarSoQuyetdinh.IsPrimaryKey = false;
				colvarSoQuyetdinh.IsForeignKey = false;
				colvarSoQuyetdinh.IsReadOnly = false;
				colvarSoQuyetdinh.DefaultSetting = @"";
				colvarSoQuyetdinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoQuyetdinh);
				
				TableSchema.TableColumn colvarNgayqdTu = new TableSchema.TableColumn(schema);
				colvarNgayqdTu.ColumnName = "ngayqd_tu";
				colvarNgayqdTu.DataType = DbType.DateTime;
				colvarNgayqdTu.MaxLength = 0;
				colvarNgayqdTu.AutoIncrement = false;
				colvarNgayqdTu.IsNullable = false;
				colvarNgayqdTu.IsPrimaryKey = false;
				colvarNgayqdTu.IsForeignKey = false;
				colvarNgayqdTu.IsReadOnly = false;
				
						colvarNgayqdTu.DefaultSetting = @"(getdate())";
				colvarNgayqdTu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayqdTu);
				
				TableSchema.TableColumn colvarNgayqdDen = new TableSchema.TableColumn(schema);
				colvarNgayqdDen.ColumnName = "ngayqd_den";
				colvarNgayqdDen.DataType = DbType.DateTime;
				colvarNgayqdDen.MaxLength = 0;
				colvarNgayqdDen.AutoIncrement = false;
				colvarNgayqdDen.IsNullable = true;
				colvarNgayqdDen.IsPrimaryKey = false;
				colvarNgayqdDen.IsForeignKey = false;
				colvarNgayqdDen.IsReadOnly = false;
				colvarNgayqdDen.DefaultSetting = @"";
				colvarNgayqdDen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayqdDen);
				
				TableSchema.TableColumn colvarMaNhacungcap = new TableSchema.TableColumn(schema);
				colvarMaNhacungcap.ColumnName = "ma_nhacungcap";
				colvarMaNhacungcap.DataType = DbType.String;
				colvarMaNhacungcap.MaxLength = 50;
				colvarMaNhacungcap.AutoIncrement = false;
				colvarMaNhacungcap.IsNullable = true;
				colvarMaNhacungcap.IsPrimaryKey = false;
				colvarMaNhacungcap.IsForeignKey = false;
				colvarMaNhacungcap.IsReadOnly = false;
				colvarMaNhacungcap.DefaultSetting = @"";
				colvarMaNhacungcap.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaNhacungcap);
				
				TableSchema.TableColumn colvarGhiChu = new TableSchema.TableColumn(schema);
				colvarGhiChu.ColumnName = "ghi_chu";
				colvarGhiChu.DataType = DbType.String;
				colvarGhiChu.MaxLength = 512;
				colvarGhiChu.AutoIncrement = false;
				colvarGhiChu.IsNullable = true;
				colvarGhiChu.IsPrimaryKey = false;
				colvarGhiChu.IsForeignKey = false;
				colvarGhiChu.IsReadOnly = false;
				colvarGhiChu.DefaultSetting = @"";
				colvarGhiChu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGhiChu);
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "trang_thai";
				colvarTrangThai.DataType = DbType.Byte;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = true;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				
						colvarTrangThai.DefaultSetting = @"((0))";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
				TableSchema.TableColumn colvarTthaiXacnhan = new TableSchema.TableColumn(schema);
				colvarTthaiXacnhan.ColumnName = "tthai_xacnhan";
				colvarTthaiXacnhan.DataType = DbType.Byte;
				colvarTthaiXacnhan.MaxLength = 0;
				colvarTthaiXacnhan.AutoIncrement = false;
				colvarTthaiXacnhan.IsNullable = true;
				colvarTthaiXacnhan.IsPrimaryKey = false;
				colvarTthaiXacnhan.IsForeignKey = false;
				colvarTthaiXacnhan.IsReadOnly = false;
				colvarTthaiXacnhan.DefaultSetting = @"";
				colvarTthaiXacnhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTthaiXacnhan);
				
				TableSchema.TableColumn colvarNguoiXacnhan = new TableSchema.TableColumn(schema);
				colvarNguoiXacnhan.ColumnName = "nguoi_xacnhan";
				colvarNguoiXacnhan.DataType = DbType.String;
				colvarNguoiXacnhan.MaxLength = 50;
				colvarNguoiXacnhan.AutoIncrement = false;
				colvarNguoiXacnhan.IsNullable = true;
				colvarNguoiXacnhan.IsPrimaryKey = false;
				colvarNguoiXacnhan.IsForeignKey = false;
				colvarNguoiXacnhan.IsReadOnly = false;
				colvarNguoiXacnhan.DefaultSetting = @"";
				colvarNguoiXacnhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiXacnhan);
				
				TableSchema.TableColumn colvarNgayXacnhan = new TableSchema.TableColumn(schema);
				colvarNgayXacnhan.ColumnName = "ngay_xacnhan";
				colvarNgayXacnhan.DataType = DbType.DateTime;
				colvarNgayXacnhan.MaxLength = 0;
				colvarNgayXacnhan.AutoIncrement = false;
				colvarNgayXacnhan.IsNullable = true;
				colvarNgayXacnhan.IsPrimaryKey = false;
				colvarNgayXacnhan.IsForeignKey = false;
				colvarNgayXacnhan.IsReadOnly = false;
				colvarNgayXacnhan.DefaultSetting = @"";
				colvarNgayXacnhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayXacnhan);
				
				TableSchema.TableColumn colvarTthaiDieutiet = new TableSchema.TableColumn(schema);
				colvarTthaiDieutiet.ColumnName = "tthai_dieutiet";
				colvarTthaiDieutiet.DataType = DbType.Boolean;
				colvarTthaiDieutiet.MaxLength = 0;
				colvarTthaiDieutiet.AutoIncrement = false;
				colvarTthaiDieutiet.IsNullable = true;
				colvarTthaiDieutiet.IsPrimaryKey = false;
				colvarTthaiDieutiet.IsForeignKey = false;
				colvarTthaiDieutiet.IsReadOnly = false;
				colvarTthaiDieutiet.DefaultSetting = @"";
				colvarTthaiDieutiet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTthaiDieutiet);
				
				TableSchema.TableColumn colvarIdBenhvienDieutiet = new TableSchema.TableColumn(schema);
				colvarIdBenhvienDieutiet.ColumnName = "id_benhvien_dieutiet";
				colvarIdBenhvienDieutiet.DataType = DbType.Int32;
				colvarIdBenhvienDieutiet.MaxLength = 0;
				colvarIdBenhvienDieutiet.AutoIncrement = false;
				colvarIdBenhvienDieutiet.IsNullable = true;
				colvarIdBenhvienDieutiet.IsPrimaryKey = false;
				colvarIdBenhvienDieutiet.IsForeignKey = false;
				colvarIdBenhvienDieutiet.IsReadOnly = false;
				colvarIdBenhvienDieutiet.DefaultSetting = @"";
				colvarIdBenhvienDieutiet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBenhvienDieutiet);
				
				TableSchema.TableColumn colvarSoHdongDieutiet = new TableSchema.TableColumn(schema);
				colvarSoHdongDieutiet.ColumnName = "so_hdong_dieutiet";
				colvarSoHdongDieutiet.DataType = DbType.String;
				colvarSoHdongDieutiet.MaxLength = 50;
				colvarSoHdongDieutiet.AutoIncrement = false;
				colvarSoHdongDieutiet.IsNullable = true;
				colvarSoHdongDieutiet.IsPrimaryKey = false;
				colvarSoHdongDieutiet.IsForeignKey = false;
				colvarSoHdongDieutiet.IsReadOnly = false;
				colvarSoHdongDieutiet.DefaultSetting = @"";
				colvarSoHdongDieutiet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoHdongDieutiet);
				
				TableSchema.TableColumn colvarNgayhdDieutiet = new TableSchema.TableColumn(schema);
				colvarNgayhdDieutiet.ColumnName = "ngayhd_dieutiet";
				colvarNgayhdDieutiet.DataType = DbType.DateTime;
				colvarNgayhdDieutiet.MaxLength = 0;
				colvarNgayhdDieutiet.AutoIncrement = false;
				colvarNgayhdDieutiet.IsNullable = true;
				colvarNgayhdDieutiet.IsPrimaryKey = false;
				colvarNgayhdDieutiet.IsForeignKey = false;
				colvarNgayhdDieutiet.IsReadOnly = false;
				colvarNgayhdDieutiet.DefaultSetting = @"";
				colvarNgayhdDieutiet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayhdDieutiet);
				
				TableSchema.TableColumn colvarNgayktDieutiet = new TableSchema.TableColumn(schema);
				colvarNgayktDieutiet.ColumnName = "ngaykt_dieutiet";
				colvarNgayktDieutiet.DataType = DbType.DateTime;
				colvarNgayktDieutiet.MaxLength = 0;
				colvarNgayktDieutiet.AutoIncrement = false;
				colvarNgayktDieutiet.IsNullable = true;
				colvarNgayktDieutiet.IsPrimaryKey = false;
				colvarNgayktDieutiet.IsForeignKey = false;
				colvarNgayktDieutiet.IsReadOnly = false;
				colvarNgayktDieutiet.DefaultSetting = @"";
				colvarNgayktDieutiet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayktDieutiet);
				
				TableSchema.TableColumn colvarHthucThau = new TableSchema.TableColumn(schema);
				colvarHthucThau.ColumnName = "hthuc_thau";
				colvarHthucThau.DataType = DbType.Int32;
				colvarHthucThau.MaxLength = 0;
				colvarHthucThau.AutoIncrement = false;
				colvarHthucThau.IsNullable = true;
				colvarHthucThau.IsPrimaryKey = false;
				colvarHthucThau.IsForeignKey = false;
				colvarHthucThau.IsReadOnly = false;
				colvarHthucThau.DefaultSetting = @"";
				colvarHthucThau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHthucThau);
				
				TableSchema.TableColumn colvarBophanDung = new TableSchema.TableColumn(schema);
				colvarBophanDung.ColumnName = "bophan_dung";
				colvarBophanDung.DataType = DbType.Int32;
				colvarBophanDung.MaxLength = 0;
				colvarBophanDung.AutoIncrement = false;
				colvarBophanDung.IsNullable = true;
				colvarBophanDung.IsPrimaryKey = false;
				colvarBophanDung.IsForeignKey = false;
				colvarBophanDung.IsReadOnly = false;
				colvarBophanDung.DefaultSetting = @"";
				colvarBophanDung.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBophanDung);
				
				TableSchema.TableColumn colvarKieuThuocvt = new TableSchema.TableColumn(schema);
				colvarKieuThuocvt.ColumnName = "kieu_thuocvt";
				colvarKieuThuocvt.DataType = DbType.String;
				colvarKieuThuocvt.MaxLength = 10;
				colvarKieuThuocvt.AutoIncrement = false;
				colvarKieuThuocvt.IsNullable = false;
				colvarKieuThuocvt.IsPrimaryKey = false;
				colvarKieuThuocvt.IsForeignKey = false;
				colvarKieuThuocvt.IsReadOnly = false;
				colvarKieuThuocvt.DefaultSetting = @"";
				colvarKieuThuocvt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKieuThuocvt);
				
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
				colvarNguoiTao.MaxLength = 50;
				colvarNguoiTao.AutoIncrement = false;
				colvarNguoiTao.IsNullable = true;
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
				colvarNguoiSua.MaxLength = 50;
				colvarNguoiSua.AutoIncrement = false;
				colvarNguoiSua.IsNullable = true;
				colvarNguoiSua.IsPrimaryKey = false;
				colvarNguoiSua.IsForeignKey = false;
				colvarNguoiSua.IsReadOnly = false;
				colvarNguoiSua.DefaultSetting = @"";
				colvarNguoiSua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiSua);
				
				TableSchema.TableColumn colvarLaApthau = new TableSchema.TableColumn(schema);
				colvarLaApthau.ColumnName = "la_apthau";
				colvarLaApthau.DataType = DbType.Byte;
				colvarLaApthau.MaxLength = 0;
				colvarLaApthau.AutoIncrement = false;
				colvarLaApthau.IsNullable = true;
				colvarLaApthau.IsPrimaryKey = false;
				colvarLaApthau.IsForeignKey = false;
				colvarLaApthau.IsReadOnly = false;
				
						colvarLaApthau.DefaultSetting = @"((0))";
				colvarLaApthau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLaApthau);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("t_thau",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdThau")]
		[Bindable(true)]
		public long IdThau 
		{
			get { return GetColumnValue<long>(Columns.IdThau); }
			set { SetColumnValue(Columns.IdThau, value); }
		}
		  
		[XmlAttribute("LoaiThau")]
		[Bindable(true)]
		public string LoaiThau 
		{
			get { return GetColumnValue<string>(Columns.LoaiThau); }
			set { SetColumnValue(Columns.LoaiThau, value); }
		}
		  
		[XmlAttribute("NhomThau")]
		[Bindable(true)]
		public string NhomThau 
		{
			get { return GetColumnValue<string>(Columns.NhomThau); }
			set { SetColumnValue(Columns.NhomThau, value); }
		}
		  
		[XmlAttribute("GoiThau")]
		[Bindable(true)]
		public string GoiThau 
		{
			get { return GetColumnValue<string>(Columns.GoiThau); }
			set { SetColumnValue(Columns.GoiThau, value); }
		}
		  
		[XmlAttribute("SoThau")]
		[Bindable(true)]
		public string SoThau 
		{
			get { return GetColumnValue<string>(Columns.SoThau); }
			set { SetColumnValue(Columns.SoThau, value); }
		}
		  
		[XmlAttribute("NgaythauTu")]
		[Bindable(true)]
		public DateTime NgaythauTu 
		{
			get { return GetColumnValue<DateTime>(Columns.NgaythauTu); }
			set { SetColumnValue(Columns.NgaythauTu, value); }
		}
		  
		[XmlAttribute("NgaythauDen")]
		[Bindable(true)]
		public DateTime? NgaythauDen 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgaythauDen); }
			set { SetColumnValue(Columns.NgaythauDen, value); }
		}
		  
		[XmlAttribute("SoQuyetdinh")]
		[Bindable(true)]
		public string SoQuyetdinh 
		{
			get { return GetColumnValue<string>(Columns.SoQuyetdinh); }
			set { SetColumnValue(Columns.SoQuyetdinh, value); }
		}
		  
		[XmlAttribute("NgayqdTu")]
		[Bindable(true)]
		public DateTime NgayqdTu 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayqdTu); }
			set { SetColumnValue(Columns.NgayqdTu, value); }
		}
		  
		[XmlAttribute("NgayqdDen")]
		[Bindable(true)]
		public DateTime? NgayqdDen 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayqdDen); }
			set { SetColumnValue(Columns.NgayqdDen, value); }
		}
		  
		[XmlAttribute("MaNhacungcap")]
		[Bindable(true)]
		public string MaNhacungcap 
		{
			get { return GetColumnValue<string>(Columns.MaNhacungcap); }
			set { SetColumnValue(Columns.MaNhacungcap, value); }
		}
		  
		[XmlAttribute("GhiChu")]
		[Bindable(true)]
		public string GhiChu 
		{
			get { return GetColumnValue<string>(Columns.GhiChu); }
			set { SetColumnValue(Columns.GhiChu, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public byte? TrangThai 
		{
			get { return GetColumnValue<byte?>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("TthaiXacnhan")]
		[Bindable(true)]
		public byte? TthaiXacnhan 
		{
			get { return GetColumnValue<byte?>(Columns.TthaiXacnhan); }
			set { SetColumnValue(Columns.TthaiXacnhan, value); }
		}
		  
		[XmlAttribute("NguoiXacnhan")]
		[Bindable(true)]
		public string NguoiXacnhan 
		{
			get { return GetColumnValue<string>(Columns.NguoiXacnhan); }
			set { SetColumnValue(Columns.NguoiXacnhan, value); }
		}
		  
		[XmlAttribute("NgayXacnhan")]
		[Bindable(true)]
		public DateTime? NgayXacnhan 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayXacnhan); }
			set { SetColumnValue(Columns.NgayXacnhan, value); }
		}
		  
		[XmlAttribute("TthaiDieutiet")]
		[Bindable(true)]
		public bool? TthaiDieutiet 
		{
			get { return GetColumnValue<bool?>(Columns.TthaiDieutiet); }
			set { SetColumnValue(Columns.TthaiDieutiet, value); }
		}
		  
		[XmlAttribute("IdBenhvienDieutiet")]
		[Bindable(true)]
		public int? IdBenhvienDieutiet 
		{
			get { return GetColumnValue<int?>(Columns.IdBenhvienDieutiet); }
			set { SetColumnValue(Columns.IdBenhvienDieutiet, value); }
		}
		  
		[XmlAttribute("SoHdongDieutiet")]
		[Bindable(true)]
		public string SoHdongDieutiet 
		{
			get { return GetColumnValue<string>(Columns.SoHdongDieutiet); }
			set { SetColumnValue(Columns.SoHdongDieutiet, value); }
		}
		  
		[XmlAttribute("NgayhdDieutiet")]
		[Bindable(true)]
		public DateTime? NgayhdDieutiet 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayhdDieutiet); }
			set { SetColumnValue(Columns.NgayhdDieutiet, value); }
		}
		  
		[XmlAttribute("NgayktDieutiet")]
		[Bindable(true)]
		public DateTime? NgayktDieutiet 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayktDieutiet); }
			set { SetColumnValue(Columns.NgayktDieutiet, value); }
		}
		  
		[XmlAttribute("HthucThau")]
		[Bindable(true)]
		public int? HthucThau 
		{
			get { return GetColumnValue<int?>(Columns.HthucThau); }
			set { SetColumnValue(Columns.HthucThau, value); }
		}
		  
		[XmlAttribute("BophanDung")]
		[Bindable(true)]
		public int? BophanDung 
		{
			get { return GetColumnValue<int?>(Columns.BophanDung); }
			set { SetColumnValue(Columns.BophanDung, value); }
		}
		  
		[XmlAttribute("KieuThuocvt")]
		[Bindable(true)]
		public string KieuThuocvt 
		{
			get { return GetColumnValue<string>(Columns.KieuThuocvt); }
			set { SetColumnValue(Columns.KieuThuocvt, value); }
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
		  
		[XmlAttribute("LaApthau")]
		[Bindable(true)]
		public byte? LaApthau 
		{
			get { return GetColumnValue<byte?>(Columns.LaApthau); }
			set { SetColumnValue(Columns.LaApthau, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varLoaiThau,string varNhomThau,string varGoiThau,string varSoThau,DateTime varNgaythauTu,DateTime? varNgaythauDen,string varSoQuyetdinh,DateTime varNgayqdTu,DateTime? varNgayqdDen,string varMaNhacungcap,string varGhiChu,byte? varTrangThai,byte? varTthaiXacnhan,string varNguoiXacnhan,DateTime? varNgayXacnhan,bool? varTthaiDieutiet,int? varIdBenhvienDieutiet,string varSoHdongDieutiet,DateTime? varNgayhdDieutiet,DateTime? varNgayktDieutiet,int? varHthucThau,int? varBophanDung,string varKieuThuocvt,DateTime? varNgayTao,string varNguoiTao,DateTime? varNgaySua,string varNguoiSua,byte? varLaApthau)
		{
			TThau item = new TThau();
			
			item.LoaiThau = varLoaiThau;
			
			item.NhomThau = varNhomThau;
			
			item.GoiThau = varGoiThau;
			
			item.SoThau = varSoThau;
			
			item.NgaythauTu = varNgaythauTu;
			
			item.NgaythauDen = varNgaythauDen;
			
			item.SoQuyetdinh = varSoQuyetdinh;
			
			item.NgayqdTu = varNgayqdTu;
			
			item.NgayqdDen = varNgayqdDen;
			
			item.MaNhacungcap = varMaNhacungcap;
			
			item.GhiChu = varGhiChu;
			
			item.TrangThai = varTrangThai;
			
			item.TthaiXacnhan = varTthaiXacnhan;
			
			item.NguoiXacnhan = varNguoiXacnhan;
			
			item.NgayXacnhan = varNgayXacnhan;
			
			item.TthaiDieutiet = varTthaiDieutiet;
			
			item.IdBenhvienDieutiet = varIdBenhvienDieutiet;
			
			item.SoHdongDieutiet = varSoHdongDieutiet;
			
			item.NgayhdDieutiet = varNgayhdDieutiet;
			
			item.NgayktDieutiet = varNgayktDieutiet;
			
			item.HthucThau = varHthucThau;
			
			item.BophanDung = varBophanDung;
			
			item.KieuThuocvt = varKieuThuocvt;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgaySua = varNgaySua;
			
			item.NguoiSua = varNguoiSua;
			
			item.LaApthau = varLaApthau;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varIdThau,string varLoaiThau,string varNhomThau,string varGoiThau,string varSoThau,DateTime varNgaythauTu,DateTime? varNgaythauDen,string varSoQuyetdinh,DateTime varNgayqdTu,DateTime? varNgayqdDen,string varMaNhacungcap,string varGhiChu,byte? varTrangThai,byte? varTthaiXacnhan,string varNguoiXacnhan,DateTime? varNgayXacnhan,bool? varTthaiDieutiet,int? varIdBenhvienDieutiet,string varSoHdongDieutiet,DateTime? varNgayhdDieutiet,DateTime? varNgayktDieutiet,int? varHthucThau,int? varBophanDung,string varKieuThuocvt,DateTime? varNgayTao,string varNguoiTao,DateTime? varNgaySua,string varNguoiSua,byte? varLaApthau)
		{
			TThau item = new TThau();
			
				item.IdThau = varIdThau;
			
				item.LoaiThau = varLoaiThau;
			
				item.NhomThau = varNhomThau;
			
				item.GoiThau = varGoiThau;
			
				item.SoThau = varSoThau;
			
				item.NgaythauTu = varNgaythauTu;
			
				item.NgaythauDen = varNgaythauDen;
			
				item.SoQuyetdinh = varSoQuyetdinh;
			
				item.NgayqdTu = varNgayqdTu;
			
				item.NgayqdDen = varNgayqdDen;
			
				item.MaNhacungcap = varMaNhacungcap;
			
				item.GhiChu = varGhiChu;
			
				item.TrangThai = varTrangThai;
			
				item.TthaiXacnhan = varTthaiXacnhan;
			
				item.NguoiXacnhan = varNguoiXacnhan;
			
				item.NgayXacnhan = varNgayXacnhan;
			
				item.TthaiDieutiet = varTthaiDieutiet;
			
				item.IdBenhvienDieutiet = varIdBenhvienDieutiet;
			
				item.SoHdongDieutiet = varSoHdongDieutiet;
			
				item.NgayhdDieutiet = varNgayhdDieutiet;
			
				item.NgayktDieutiet = varNgayktDieutiet;
			
				item.HthucThau = varHthucThau;
			
				item.BophanDung = varBophanDung;
			
				item.KieuThuocvt = varKieuThuocvt;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgaySua = varNgaySua;
			
				item.NguoiSua = varNguoiSua;
			
				item.LaApthau = varLaApthau;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdThauColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiThauColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn NhomThauColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn GoiThauColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SoThauColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaythauTuColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaythauDenColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn SoQuyetdinhColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayqdTuColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayqdDenColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn MaNhacungcapColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn GhiChuColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn TthaiXacnhanColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiXacnhanColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayXacnhanColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn TthaiDieutietColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn IdBenhvienDieutietColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn SoHdongDieutietColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayhdDieutietColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayktDieutietColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn HthucThauColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn BophanDungColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        public static TableSchema.TableColumn KieuThuocvtColumn
        {
            get { return Schema.Columns[23]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[24]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[25]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[26]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[27]; }
        }
        
        
        
        public static TableSchema.TableColumn LaApthauColumn
        {
            get { return Schema.Columns[28]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdThau = @"id_thau";
			 public static string LoaiThau = @"loai_thau";
			 public static string NhomThau = @"nhom_thau";
			 public static string GoiThau = @"goi_thau";
			 public static string SoThau = @"so_thau";
			 public static string NgaythauTu = @"ngaythau_tu";
			 public static string NgaythauDen = @"ngaythau_den";
			 public static string SoQuyetdinh = @"so_quyetdinh";
			 public static string NgayqdTu = @"ngayqd_tu";
			 public static string NgayqdDen = @"ngayqd_den";
			 public static string MaNhacungcap = @"ma_nhacungcap";
			 public static string GhiChu = @"ghi_chu";
			 public static string TrangThai = @"trang_thai";
			 public static string TthaiXacnhan = @"tthai_xacnhan";
			 public static string NguoiXacnhan = @"nguoi_xacnhan";
			 public static string NgayXacnhan = @"ngay_xacnhan";
			 public static string TthaiDieutiet = @"tthai_dieutiet";
			 public static string IdBenhvienDieutiet = @"id_benhvien_dieutiet";
			 public static string SoHdongDieutiet = @"so_hdong_dieutiet";
			 public static string NgayhdDieutiet = @"ngayhd_dieutiet";
			 public static string NgayktDieutiet = @"ngaykt_dieutiet";
			 public static string HthucThau = @"hthuc_thau";
			 public static string BophanDung = @"bophan_dung";
			 public static string KieuThuocvt = @"kieu_thuocvt";
			 public static string NgayTao = @"ngay_tao";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string NgaySua = @"ngay_sua";
			 public static string NguoiSua = @"nguoi_sua";
			 public static string LaApthau = @"la_apthau";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
