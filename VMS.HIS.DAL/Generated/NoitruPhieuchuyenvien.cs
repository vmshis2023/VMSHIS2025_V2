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
	/// Strongly-typed collection for the NoitruPhieuchuyenvien class.
	/// </summary>
    [Serializable]
	public partial class NoitruPhieuchuyenvienCollection : ActiveList<NoitruPhieuchuyenvien, NoitruPhieuchuyenvienCollection>
	{	   
		public NoitruPhieuchuyenvienCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>NoitruPhieuchuyenvienCollection</returns>
		public NoitruPhieuchuyenvienCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                NoitruPhieuchuyenvien o = this[i];
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
	/// This is an ActiveRecord class which wraps the noitru_phieuchuyenvien table.
	/// </summary>
	[Serializable]
	public partial class NoitruPhieuchuyenvien : ActiveRecord<NoitruPhieuchuyenvien>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public NoitruPhieuchuyenvien()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public NoitruPhieuchuyenvien(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public NoitruPhieuchuyenvien(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public NoitruPhieuchuyenvien(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("noitru_phieuchuyenvien", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdPhieuchuyenvien = new TableSchema.TableColumn(schema);
				colvarIdPhieuchuyenvien.ColumnName = "id_phieuchuyenvien";
				colvarIdPhieuchuyenvien.DataType = DbType.Int64;
				colvarIdPhieuchuyenvien.MaxLength = 0;
				colvarIdPhieuchuyenvien.AutoIncrement = true;
				colvarIdPhieuchuyenvien.IsNullable = false;
				colvarIdPhieuchuyenvien.IsPrimaryKey = true;
				colvarIdPhieuchuyenvien.IsForeignKey = false;
				colvarIdPhieuchuyenvien.IsReadOnly = false;
				colvarIdPhieuchuyenvien.DefaultSetting = @"";
				colvarIdPhieuchuyenvien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdPhieuchuyenvien);
				
				TableSchema.TableColumn colvarSoChuyenvien = new TableSchema.TableColumn(schema);
				colvarSoChuyenvien.ColumnName = "so_chuyenvien";
				colvarSoChuyenvien.DataType = DbType.String;
				colvarSoChuyenvien.MaxLength = 20;
				colvarSoChuyenvien.AutoIncrement = false;
				colvarSoChuyenvien.IsNullable = true;
				colvarSoChuyenvien.IsPrimaryKey = false;
				colvarSoChuyenvien.IsForeignKey = false;
				colvarSoChuyenvien.IsReadOnly = false;
				colvarSoChuyenvien.DefaultSetting = @"";
				colvarSoChuyenvien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoChuyenvien);
				
				TableSchema.TableColumn colvarMaLuotkham = new TableSchema.TableColumn(schema);
				colvarMaLuotkham.ColumnName = "ma_luotkham";
				colvarMaLuotkham.DataType = DbType.String;
				colvarMaLuotkham.MaxLength = 50;
				colvarMaLuotkham.AutoIncrement = false;
				colvarMaLuotkham.IsNullable = false;
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
				colvarIdBenhnhan.IsNullable = false;
				colvarIdBenhnhan.IsPrimaryKey = false;
				colvarIdBenhnhan.IsForeignKey = false;
				colvarIdBenhnhan.IsReadOnly = false;
				colvarIdBenhnhan.DefaultSetting = @"";
				colvarIdBenhnhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBenhnhan);
				
				TableSchema.TableColumn colvarNgayChuyenvien = new TableSchema.TableColumn(schema);
				colvarNgayChuyenvien.ColumnName = "ngay_chuyenvien";
				colvarNgayChuyenvien.DataType = DbType.DateTime;
				colvarNgayChuyenvien.MaxLength = 0;
				colvarNgayChuyenvien.AutoIncrement = false;
				colvarNgayChuyenvien.IsNullable = false;
				colvarNgayChuyenvien.IsPrimaryKey = false;
				colvarNgayChuyenvien.IsForeignKey = false;
				colvarNgayChuyenvien.IsReadOnly = false;
				
						colvarNgayChuyenvien.DefaultSetting = @"(getdate())";
				colvarNgayChuyenvien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayChuyenvien);
				
				TableSchema.TableColumn colvarMaLydochuyenvien = new TableSchema.TableColumn(schema);
				colvarMaLydochuyenvien.ColumnName = "ma_lydochuyenvien";
				colvarMaLydochuyenvien.DataType = DbType.String;
				colvarMaLydochuyenvien.MaxLength = 30;
				colvarMaLydochuyenvien.AutoIncrement = false;
				colvarMaLydochuyenvien.IsNullable = true;
				colvarMaLydochuyenvien.IsPrimaryKey = false;
				colvarMaLydochuyenvien.IsForeignKey = false;
				colvarMaLydochuyenvien.IsReadOnly = false;
				colvarMaLydochuyenvien.DefaultSetting = @"";
				colvarMaLydochuyenvien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaLydochuyenvien);
				
				TableSchema.TableColumn colvarMaPtienVanchuyen = new TableSchema.TableColumn(schema);
				colvarMaPtienVanchuyen.ColumnName = "ma_ptien_vanchuyen";
				colvarMaPtienVanchuyen.DataType = DbType.String;
				colvarMaPtienVanchuyen.MaxLength = 30;
				colvarMaPtienVanchuyen.AutoIncrement = false;
				colvarMaPtienVanchuyen.IsNullable = true;
				colvarMaPtienVanchuyen.IsPrimaryKey = false;
				colvarMaPtienVanchuyen.IsForeignKey = false;
				colvarMaPtienVanchuyen.IsReadOnly = false;
				colvarMaPtienVanchuyen.DefaultSetting = @"";
				colvarMaPtienVanchuyen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaPtienVanchuyen);
				
				TableSchema.TableColumn colvarIdBacsiChuyenvien = new TableSchema.TableColumn(schema);
				colvarIdBacsiChuyenvien.ColumnName = "id_bacsi_chuyenvien";
				colvarIdBacsiChuyenvien.DataType = DbType.Int32;
				colvarIdBacsiChuyenvien.MaxLength = 0;
				colvarIdBacsiChuyenvien.AutoIncrement = false;
				colvarIdBacsiChuyenvien.IsNullable = true;
				colvarIdBacsiChuyenvien.IsPrimaryKey = false;
				colvarIdBacsiChuyenvien.IsForeignKey = false;
				colvarIdBacsiChuyenvien.IsReadOnly = false;
				colvarIdBacsiChuyenvien.DefaultSetting = @"";
				colvarIdBacsiChuyenvien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBacsiChuyenvien);
				
				TableSchema.TableColumn colvarMaHuongdieutriCv = new TableSchema.TableColumn(schema);
				colvarMaHuongdieutriCv.ColumnName = "ma_huongdieutri_cv";
				colvarMaHuongdieutriCv.DataType = DbType.String;
				colvarMaHuongdieutriCv.MaxLength = 30;
				colvarMaHuongdieutriCv.AutoIncrement = false;
				colvarMaHuongdieutriCv.IsNullable = true;
				colvarMaHuongdieutriCv.IsPrimaryKey = false;
				colvarMaHuongdieutriCv.IsForeignKey = false;
				colvarMaHuongdieutriCv.IsReadOnly = false;
				colvarMaHuongdieutriCv.DefaultSetting = @"";
				colvarMaHuongdieutriCv.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaHuongdieutriCv);
				
				TableSchema.TableColumn colvarMaBvchuyenden = new TableSchema.TableColumn(schema);
				colvarMaBvchuyenden.ColumnName = "ma_bvchuyenden";
				colvarMaBvchuyenden.DataType = DbType.Int16;
				colvarMaBvchuyenden.MaxLength = 0;
				colvarMaBvchuyenden.AutoIncrement = false;
				colvarMaBvchuyenden.IsNullable = true;
				colvarMaBvchuyenden.IsPrimaryKey = false;
				colvarMaBvchuyenden.IsForeignKey = false;
				colvarMaBvchuyenden.IsReadOnly = false;
				colvarMaBvchuyenden.DefaultSetting = @"";
				colvarMaBvchuyenden.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaBvchuyenden);
				
				TableSchema.TableColumn colvarTtrangNguoibenh = new TableSchema.TableColumn(schema);
				colvarTtrangNguoibenh.ColumnName = "ttrang_nguoibenh";
				colvarTtrangNguoibenh.DataType = DbType.String;
				colvarTtrangNguoibenh.MaxLength = 500;
				colvarTtrangNguoibenh.AutoIncrement = false;
				colvarTtrangNguoibenh.IsNullable = true;
				colvarTtrangNguoibenh.IsPrimaryKey = false;
				colvarTtrangNguoibenh.IsForeignKey = false;
				colvarTtrangNguoibenh.IsReadOnly = false;
				colvarTtrangNguoibenh.DefaultSetting = @"";
				colvarTtrangNguoibenh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTtrangNguoibenh);
				
				TableSchema.TableColumn colvarDauhieuCls = new TableSchema.TableColumn(schema);
				colvarDauhieuCls.ColumnName = "dauhieu_cls";
				colvarDauhieuCls.DataType = DbType.String;
				colvarDauhieuCls.MaxLength = 2500;
				colvarDauhieuCls.AutoIncrement = false;
				colvarDauhieuCls.IsNullable = true;
				colvarDauhieuCls.IsPrimaryKey = false;
				colvarDauhieuCls.IsForeignKey = false;
				colvarDauhieuCls.IsReadOnly = false;
				colvarDauhieuCls.DefaultSetting = @"";
				colvarDauhieuCls.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDauhieuCls);
				
				TableSchema.TableColumn colvarXetNghiem = new TableSchema.TableColumn(schema);
				colvarXetNghiem.ColumnName = "xet_nghiem";
				colvarXetNghiem.DataType = DbType.String;
				colvarXetNghiem.MaxLength = 1000;
				colvarXetNghiem.AutoIncrement = false;
				colvarXetNghiem.IsNullable = true;
				colvarXetNghiem.IsPrimaryKey = false;
				colvarXetNghiem.IsForeignKey = false;
				colvarXetNghiem.IsReadOnly = false;
				colvarXetNghiem.DefaultSetting = @"";
				colvarXetNghiem.ForeignKeyTableName = "";
				schema.Columns.Add(colvarXetNghiem);
				
				TableSchema.TableColumn colvarChanDoan = new TableSchema.TableColumn(schema);
				colvarChanDoan.ColumnName = "chan_doan";
				colvarChanDoan.DataType = DbType.String;
				colvarChanDoan.MaxLength = 1024;
				colvarChanDoan.AutoIncrement = false;
				colvarChanDoan.IsNullable = true;
				colvarChanDoan.IsPrimaryKey = false;
				colvarChanDoan.IsForeignKey = false;
				colvarChanDoan.IsReadOnly = false;
				colvarChanDoan.DefaultSetting = @"";
				colvarChanDoan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChanDoan);
				
				TableSchema.TableColumn colvarThuocDadung = new TableSchema.TableColumn(schema);
				colvarThuocDadung.ColumnName = "thuoc_dadung";
				colvarThuocDadung.DataType = DbType.String;
				colvarThuocDadung.MaxLength = 1000;
				colvarThuocDadung.AutoIncrement = false;
				colvarThuocDadung.IsNullable = true;
				colvarThuocDadung.IsPrimaryKey = false;
				colvarThuocDadung.IsForeignKey = false;
				colvarThuocDadung.IsReadOnly = false;
				colvarThuocDadung.DefaultSetting = @"";
				colvarThuocDadung.ForeignKeyTableName = "";
				schema.Columns.Add(colvarThuocDadung);
				
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
				
				TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
				colvarNgayTao.ColumnName = "ngay_tao";
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
				
				TableSchema.TableColumn colvarIpMaytao = new TableSchema.TableColumn(schema);
				colvarIpMaytao.ColumnName = "ip_maytao";
				colvarIpMaytao.DataType = DbType.String;
				colvarIpMaytao.MaxLength = 50;
				colvarIpMaytao.AutoIncrement = false;
				colvarIpMaytao.IsNullable = true;
				colvarIpMaytao.IsPrimaryKey = false;
				colvarIpMaytao.IsForeignKey = false;
				colvarIpMaytao.IsReadOnly = false;
				colvarIpMaytao.DefaultSetting = @"";
				colvarIpMaytao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIpMaytao);
				
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
				
				TableSchema.TableColumn colvarIpMaysua = new TableSchema.TableColumn(schema);
				colvarIpMaysua.ColumnName = "ip_maysua";
				colvarIpMaysua.DataType = DbType.String;
				colvarIpMaysua.MaxLength = 50;
				colvarIpMaysua.AutoIncrement = false;
				colvarIpMaysua.IsNullable = true;
				colvarIpMaysua.IsPrimaryKey = false;
				colvarIpMaysua.IsForeignKey = false;
				colvarIpMaysua.IsReadOnly = false;
				colvarIpMaysua.DefaultSetting = @"";
				colvarIpMaysua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIpMaysua);
				
				TableSchema.TableColumn colvarNoitru = new TableSchema.TableColumn(schema);
				colvarNoitru.ColumnName = "noitru";
				colvarNoitru.DataType = DbType.Boolean;
				colvarNoitru.MaxLength = 0;
				colvarNoitru.AutoIncrement = false;
				colvarNoitru.IsNullable = true;
				colvarNoitru.IsPrimaryKey = false;
				colvarNoitru.IsForeignKey = false;
				colvarNoitru.IsReadOnly = false;
				
						colvarNoitru.DefaultSetting = @"((0))";
				colvarNoitru.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNoitru);
				
				TableSchema.TableColumn colvarMaCsKcb = new TableSchema.TableColumn(schema);
				colvarMaCsKcb.ColumnName = "ma_cs_kcb";
				colvarMaCsKcb.DataType = DbType.String;
				colvarMaCsKcb.MaxLength = 5;
				colvarMaCsKcb.AutoIncrement = false;
				colvarMaCsKcb.IsNullable = true;
				colvarMaCsKcb.IsPrimaryKey = false;
				colvarMaCsKcb.IsForeignKey = false;
				colvarMaCsKcb.IsReadOnly = false;
				colvarMaCsKcb.DefaultSetting = @"";
				colvarMaCsKcb.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaCsKcb);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("noitru_phieuchuyenvien",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdPhieuchuyenvien")]
		[Bindable(true)]
		public long IdPhieuchuyenvien 
		{
			get { return GetColumnValue<long>(Columns.IdPhieuchuyenvien); }
			set { SetColumnValue(Columns.IdPhieuchuyenvien, value); }
		}
		  
		[XmlAttribute("SoChuyenvien")]
		[Bindable(true)]
		public string SoChuyenvien 
		{
			get { return GetColumnValue<string>(Columns.SoChuyenvien); }
			set { SetColumnValue(Columns.SoChuyenvien, value); }
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
		public long IdBenhnhan 
		{
			get { return GetColumnValue<long>(Columns.IdBenhnhan); }
			set { SetColumnValue(Columns.IdBenhnhan, value); }
		}
		  
		[XmlAttribute("NgayChuyenvien")]
		[Bindable(true)]
		public DateTime NgayChuyenvien 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayChuyenvien); }
			set { SetColumnValue(Columns.NgayChuyenvien, value); }
		}
		  
		[XmlAttribute("MaLydochuyenvien")]
		[Bindable(true)]
		public string MaLydochuyenvien 
		{
			get { return GetColumnValue<string>(Columns.MaLydochuyenvien); }
			set { SetColumnValue(Columns.MaLydochuyenvien, value); }
		}
		  
		[XmlAttribute("MaPtienVanchuyen")]
		[Bindable(true)]
		public string MaPtienVanchuyen 
		{
			get { return GetColumnValue<string>(Columns.MaPtienVanchuyen); }
			set { SetColumnValue(Columns.MaPtienVanchuyen, value); }
		}
		  
		[XmlAttribute("IdBacsiChuyenvien")]
		[Bindable(true)]
		public int? IdBacsiChuyenvien 
		{
			get { return GetColumnValue<int?>(Columns.IdBacsiChuyenvien); }
			set { SetColumnValue(Columns.IdBacsiChuyenvien, value); }
		}
		  
		[XmlAttribute("MaHuongdieutriCv")]
		[Bindable(true)]
		public string MaHuongdieutriCv 
		{
			get { return GetColumnValue<string>(Columns.MaHuongdieutriCv); }
			set { SetColumnValue(Columns.MaHuongdieutriCv, value); }
		}
		  
		[XmlAttribute("MaBvchuyenden")]
		[Bindable(true)]
		public short? MaBvchuyenden 
		{
			get { return GetColumnValue<short?>(Columns.MaBvchuyenden); }
			set { SetColumnValue(Columns.MaBvchuyenden, value); }
		}
		  
		[XmlAttribute("TtrangNguoibenh")]
		[Bindable(true)]
		public string TtrangNguoibenh 
		{
			get { return GetColumnValue<string>(Columns.TtrangNguoibenh); }
			set { SetColumnValue(Columns.TtrangNguoibenh, value); }
		}
		  
		[XmlAttribute("DauhieuCls")]
		[Bindable(true)]
		public string DauhieuCls 
		{
			get { return GetColumnValue<string>(Columns.DauhieuCls); }
			set { SetColumnValue(Columns.DauhieuCls, value); }
		}
		  
		[XmlAttribute("XetNghiem")]
		[Bindable(true)]
		public string XetNghiem 
		{
			get { return GetColumnValue<string>(Columns.XetNghiem); }
			set { SetColumnValue(Columns.XetNghiem, value); }
		}
		  
		[XmlAttribute("ChanDoan")]
		[Bindable(true)]
		public string ChanDoan 
		{
			get { return GetColumnValue<string>(Columns.ChanDoan); }
			set { SetColumnValue(Columns.ChanDoan, value); }
		}
		  
		[XmlAttribute("ThuocDadung")]
		[Bindable(true)]
		public string ThuocDadung 
		{
			get { return GetColumnValue<string>(Columns.ThuocDadung); }
			set { SetColumnValue(Columns.ThuocDadung, value); }
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
		  
		[XmlAttribute("IpMaytao")]
		[Bindable(true)]
		public string IpMaytao 
		{
			get { return GetColumnValue<string>(Columns.IpMaytao); }
			set { SetColumnValue(Columns.IpMaytao, value); }
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
		  
		[XmlAttribute("IpMaysua")]
		[Bindable(true)]
		public string IpMaysua 
		{
			get { return GetColumnValue<string>(Columns.IpMaysua); }
			set { SetColumnValue(Columns.IpMaysua, value); }
		}
		  
		[XmlAttribute("Noitru")]
		[Bindable(true)]
		public bool? Noitru 
		{
			get { return GetColumnValue<bool?>(Columns.Noitru); }
			set { SetColumnValue(Columns.Noitru, value); }
		}
		  
		[XmlAttribute("MaCsKcb")]
		[Bindable(true)]
		public string MaCsKcb 
		{
			get { return GetColumnValue<string>(Columns.MaCsKcb); }
			set { SetColumnValue(Columns.MaCsKcb, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varSoChuyenvien,string varMaLuotkham,long varIdBenhnhan,DateTime varNgayChuyenvien,string varMaLydochuyenvien,string varMaPtienVanchuyen,int? varIdBacsiChuyenvien,string varMaHuongdieutriCv,short? varMaBvchuyenden,string varTtrangNguoibenh,string varDauhieuCls,string varXetNghiem,string varChanDoan,string varThuocDadung,string varNguoiTao,DateTime? varNgayTao,string varIpMaytao,string varNguoiSua,DateTime? varNgaySua,string varIpMaysua,bool? varNoitru,string varMaCsKcb)
		{
			NoitruPhieuchuyenvien item = new NoitruPhieuchuyenvien();
			
			item.SoChuyenvien = varSoChuyenvien;
			
			item.MaLuotkham = varMaLuotkham;
			
			item.IdBenhnhan = varIdBenhnhan;
			
			item.NgayChuyenvien = varNgayChuyenvien;
			
			item.MaLydochuyenvien = varMaLydochuyenvien;
			
			item.MaPtienVanchuyen = varMaPtienVanchuyen;
			
			item.IdBacsiChuyenvien = varIdBacsiChuyenvien;
			
			item.MaHuongdieutriCv = varMaHuongdieutriCv;
			
			item.MaBvchuyenden = varMaBvchuyenden;
			
			item.TtrangNguoibenh = varTtrangNguoibenh;
			
			item.DauhieuCls = varDauhieuCls;
			
			item.XetNghiem = varXetNghiem;
			
			item.ChanDoan = varChanDoan;
			
			item.ThuocDadung = varThuocDadung;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgayTao = varNgayTao;
			
			item.IpMaytao = varIpMaytao;
			
			item.NguoiSua = varNguoiSua;
			
			item.NgaySua = varNgaySua;
			
			item.IpMaysua = varIpMaysua;
			
			item.Noitru = varNoitru;
			
			item.MaCsKcb = varMaCsKcb;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varIdPhieuchuyenvien,string varSoChuyenvien,string varMaLuotkham,long varIdBenhnhan,DateTime varNgayChuyenvien,string varMaLydochuyenvien,string varMaPtienVanchuyen,int? varIdBacsiChuyenvien,string varMaHuongdieutriCv,short? varMaBvchuyenden,string varTtrangNguoibenh,string varDauhieuCls,string varXetNghiem,string varChanDoan,string varThuocDadung,string varNguoiTao,DateTime? varNgayTao,string varIpMaytao,string varNguoiSua,DateTime? varNgaySua,string varIpMaysua,bool? varNoitru,string varMaCsKcb)
		{
			NoitruPhieuchuyenvien item = new NoitruPhieuchuyenvien();
			
				item.IdPhieuchuyenvien = varIdPhieuchuyenvien;
			
				item.SoChuyenvien = varSoChuyenvien;
			
				item.MaLuotkham = varMaLuotkham;
			
				item.IdBenhnhan = varIdBenhnhan;
			
				item.NgayChuyenvien = varNgayChuyenvien;
			
				item.MaLydochuyenvien = varMaLydochuyenvien;
			
				item.MaPtienVanchuyen = varMaPtienVanchuyen;
			
				item.IdBacsiChuyenvien = varIdBacsiChuyenvien;
			
				item.MaHuongdieutriCv = varMaHuongdieutriCv;
			
				item.MaBvchuyenden = varMaBvchuyenden;
			
				item.TtrangNguoibenh = varTtrangNguoibenh;
			
				item.DauhieuCls = varDauhieuCls;
			
				item.XetNghiem = varXetNghiem;
			
				item.ChanDoan = varChanDoan;
			
				item.ThuocDadung = varThuocDadung;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgayTao = varNgayTao;
			
				item.IpMaytao = varIpMaytao;
			
				item.NguoiSua = varNguoiSua;
			
				item.NgaySua = varNgaySua;
			
				item.IpMaysua = varIpMaysua;
			
				item.Noitru = varNoitru;
			
				item.MaCsKcb = varMaCsKcb;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdPhieuchuyenvienColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn SoChuyenvienColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn MaLuotkhamColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdBenhnhanColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayChuyenvienColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn MaLydochuyenvienColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn MaPtienVanchuyenColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn IdBacsiChuyenvienColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn MaHuongdieutriCvColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn MaBvchuyendenColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn TtrangNguoibenhColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn DauhieuClsColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn XetNghiemColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn ChanDoanColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn ThuocDadungColumn
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
        
        
        
        public static TableSchema.TableColumn IpMaytaoColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn IpMaysuaColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn NoitruColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn MaCsKcbColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdPhieuchuyenvien = @"id_phieuchuyenvien";
			 public static string SoChuyenvien = @"so_chuyenvien";
			 public static string MaLuotkham = @"ma_luotkham";
			 public static string IdBenhnhan = @"id_benhnhan";
			 public static string NgayChuyenvien = @"ngay_chuyenvien";
			 public static string MaLydochuyenvien = @"ma_lydochuyenvien";
			 public static string MaPtienVanchuyen = @"ma_ptien_vanchuyen";
			 public static string IdBacsiChuyenvien = @"id_bacsi_chuyenvien";
			 public static string MaHuongdieutriCv = @"ma_huongdieutri_cv";
			 public static string MaBvchuyenden = @"ma_bvchuyenden";
			 public static string TtrangNguoibenh = @"ttrang_nguoibenh";
			 public static string DauhieuCls = @"dauhieu_cls";
			 public static string XetNghiem = @"xet_nghiem";
			 public static string ChanDoan = @"chan_doan";
			 public static string ThuocDadung = @"thuoc_dadung";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string NgayTao = @"ngay_tao";
			 public static string IpMaytao = @"ip_maytao";
			 public static string NguoiSua = @"nguoi_sua";
			 public static string NgaySua = @"ngay_sua";
			 public static string IpMaysua = @"ip_maysua";
			 public static string Noitru = @"noitru";
			 public static string MaCsKcb = @"ma_cs_kcb";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
