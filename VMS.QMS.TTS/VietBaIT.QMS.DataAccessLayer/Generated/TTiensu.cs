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
	/// Strongly-typed collection for the TTiensu class.
	/// </summary>
    [Serializable]
	public partial class TTiensuCollection : ActiveList<TTiensu, TTiensuCollection>
	{	   
		public TTiensuCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TTiensuCollection</returns>
		public TTiensuCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TTiensu o = this[i];
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
	/// This is an ActiveRecord class which wraps the T_TIENSU table.
	/// </summary>
	[Serializable]
	public partial class TTiensu : ActiveRecord<TTiensu>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TTiensu()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TTiensu(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TTiensu(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TTiensu(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("T_TIENSU", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarTienSuId = new TableSchema.TableColumn(schema);
				colvarTienSuId.ColumnName = "TienSu_ID";
				colvarTienSuId.DataType = DbType.Int32;
				colvarTienSuId.MaxLength = 0;
				colvarTienSuId.AutoIncrement = false;
				colvarTienSuId.IsNullable = false;
				colvarTienSuId.IsPrimaryKey = true;
				colvarTienSuId.IsForeignKey = false;
				colvarTienSuId.IsReadOnly = false;
				colvarTienSuId.DefaultSetting = @"";
				colvarTienSuId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTienSuId);
				
				TableSchema.TableColumn colvarPatientId = new TableSchema.TableColumn(schema);
				colvarPatientId.ColumnName = "Patient_ID";
				colvarPatientId.DataType = DbType.Int64;
				colvarPatientId.MaxLength = 0;
				colvarPatientId.AutoIncrement = false;
				colvarPatientId.IsNullable = true;
				colvarPatientId.IsPrimaryKey = false;
				colvarPatientId.IsForeignKey = false;
				colvarPatientId.IsReadOnly = false;
				colvarPatientId.DefaultSetting = @"";
				colvarPatientId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPatientId);
				
				TableSchema.TableColumn colvarPara = new TableSchema.TableColumn(schema);
				colvarPara.ColumnName = "Para";
				colvarPara.DataType = DbType.String;
				colvarPara.MaxLength = 100;
				colvarPara.AutoIncrement = false;
				colvarPara.IsNullable = true;
				colvarPara.IsPrimaryKey = false;
				colvarPara.IsForeignKey = false;
				colvarPara.IsReadOnly = false;
				colvarPara.DefaultSetting = @"";
				colvarPara.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPara);
				
				TableSchema.TableColumn colvarSoConTrai = new TableSchema.TableColumn(schema);
				colvarSoConTrai.ColumnName = "So_Con_Trai";
				colvarSoConTrai.DataType = DbType.Int32;
				colvarSoConTrai.MaxLength = 0;
				colvarSoConTrai.AutoIncrement = false;
				colvarSoConTrai.IsNullable = true;
				colvarSoConTrai.IsPrimaryKey = false;
				colvarSoConTrai.IsForeignKey = false;
				colvarSoConTrai.IsReadOnly = false;
				colvarSoConTrai.DefaultSetting = @"";
				colvarSoConTrai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoConTrai);
				
				TableSchema.TableColumn colvarSoConGai = new TableSchema.TableColumn(schema);
				colvarSoConGai.ColumnName = "So_Con_Gai";
				colvarSoConGai.DataType = DbType.Int32;
				colvarSoConGai.MaxLength = 0;
				colvarSoConGai.AutoIncrement = false;
				colvarSoConGai.IsNullable = true;
				colvarSoConGai.IsPrimaryKey = false;
				colvarSoConGai.IsForeignKey = false;
				colvarSoConGai.IsReadOnly = false;
				colvarSoConGai.DefaultSetting = @"";
				colvarSoConGai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoConGai);
				
				TableSchema.TableColumn colvarConNhoNhat = new TableSchema.TableColumn(schema);
				colvarConNhoNhat.ColumnName = "Con_Nho_Nhat";
				colvarConNhoNhat.DataType = DbType.Int32;
				colvarConNhoNhat.MaxLength = 0;
				colvarConNhoNhat.AutoIncrement = false;
				colvarConNhoNhat.IsNullable = true;
				colvarConNhoNhat.IsPrimaryKey = false;
				colvarConNhoNhat.IsForeignKey = false;
				colvarConNhoNhat.IsReadOnly = false;
				colvarConNhoNhat.DefaultSetting = @"";
				colvarConNhoNhat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarConNhoNhat);
				
				TableSchema.TableColumn colvarTaiBien = new TableSchema.TableColumn(schema);
				colvarTaiBien.ColumnName = "Tai_Bien";
				colvarTaiBien.DataType = DbType.String;
				colvarTaiBien.MaxLength = 100;
				colvarTaiBien.AutoIncrement = false;
				colvarTaiBien.IsNullable = true;
				colvarTaiBien.IsPrimaryKey = false;
				colvarTaiBien.IsForeignKey = false;
				colvarTaiBien.IsReadOnly = false;
				colvarTaiBien.DefaultSetting = @"";
				colvarTaiBien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTaiBien);
				
				TableSchema.TableColumn colvarTienSuMoLayThai = new TableSchema.TableColumn(schema);
				colvarTienSuMoLayThai.ColumnName = "TienSu_MoLayThai";
				colvarTienSuMoLayThai.DataType = DbType.String;
				colvarTienSuMoLayThai.MaxLength = 100;
				colvarTienSuMoLayThai.AutoIncrement = false;
				colvarTienSuMoLayThai.IsNullable = true;
				colvarTienSuMoLayThai.IsPrimaryKey = false;
				colvarTienSuMoLayThai.IsForeignKey = false;
				colvarTienSuMoLayThai.IsReadOnly = false;
				colvarTienSuMoLayThai.DefaultSetting = @"";
				colvarTienSuMoLayThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTienSuMoLayThai);
				
				TableSchema.TableColumn colvarSoLanMoLayThai = new TableSchema.TableColumn(schema);
				colvarSoLanMoLayThai.ColumnName = "SoLan_MoLayThai";
				colvarSoLanMoLayThai.DataType = DbType.Int32;
				colvarSoLanMoLayThai.MaxLength = 0;
				colvarSoLanMoLayThai.AutoIncrement = false;
				colvarSoLanMoLayThai.IsNullable = true;
				colvarSoLanMoLayThai.IsPrimaryKey = false;
				colvarSoLanMoLayThai.IsForeignKey = false;
				colvarSoLanMoLayThai.IsReadOnly = false;
				colvarSoLanMoLayThai.DefaultSetting = @"";
				colvarSoLanMoLayThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLanMoLayThai);
				
				TableSchema.TableColumn colvarNamMoLayThai = new TableSchema.TableColumn(schema);
				colvarNamMoLayThai.ColumnName = "Nam_MoLayThai";
				colvarNamMoLayThai.DataType = DbType.Int32;
				colvarNamMoLayThai.MaxLength = 0;
				colvarNamMoLayThai.AutoIncrement = false;
				colvarNamMoLayThai.IsNullable = true;
				colvarNamMoLayThai.IsPrimaryKey = false;
				colvarNamMoLayThai.IsForeignKey = false;
				colvarNamMoLayThai.IsReadOnly = false;
				colvarNamMoLayThai.DefaultSetting = @"";
				colvarNamMoLayThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNamMoLayThai);
				
				TableSchema.TableColumn colvarTienSuPhaThai = new TableSchema.TableColumn(schema);
				colvarTienSuPhaThai.ColumnName = "TienSu_PhaThai";
				colvarTienSuPhaThai.DataType = DbType.String;
				colvarTienSuPhaThai.MaxLength = 100;
				colvarTienSuPhaThai.AutoIncrement = false;
				colvarTienSuPhaThai.IsNullable = true;
				colvarTienSuPhaThai.IsPrimaryKey = false;
				colvarTienSuPhaThai.IsForeignKey = false;
				colvarTienSuPhaThai.IsReadOnly = false;
				colvarTienSuPhaThai.DefaultSetting = @"";
				colvarTienSuPhaThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTienSuPhaThai);
				
				TableSchema.TableColumn colvarSoLanHutThai = new TableSchema.TableColumn(schema);
				colvarSoLanHutThai.ColumnName = "SoLan_HutThai";
				colvarSoLanHutThai.DataType = DbType.Int32;
				colvarSoLanHutThai.MaxLength = 0;
				colvarSoLanHutThai.AutoIncrement = false;
				colvarSoLanHutThai.IsNullable = true;
				colvarSoLanHutThai.IsPrimaryKey = false;
				colvarSoLanHutThai.IsForeignKey = false;
				colvarSoLanHutThai.IsReadOnly = false;
				colvarSoLanHutThai.DefaultSetting = @"";
				colvarSoLanHutThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLanHutThai);
				
				TableSchema.TableColumn colvarSoLanPhaThaiTo = new TableSchema.TableColumn(schema);
				colvarSoLanPhaThaiTo.ColumnName = "SoLan_PhaThaiTo";
				colvarSoLanPhaThaiTo.DataType = DbType.Int32;
				colvarSoLanPhaThaiTo.MaxLength = 0;
				colvarSoLanPhaThaiTo.AutoIncrement = false;
				colvarSoLanPhaThaiTo.IsNullable = true;
				colvarSoLanPhaThaiTo.IsPrimaryKey = false;
				colvarSoLanPhaThaiTo.IsForeignKey = false;
				colvarSoLanPhaThaiTo.IsReadOnly = false;
				colvarSoLanPhaThaiTo.DefaultSetting = @"";
				colvarSoLanPhaThaiTo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLanPhaThaiTo);
				
				TableSchema.TableColumn colvarSoLanSay = new TableSchema.TableColumn(schema);
				colvarSoLanSay.ColumnName = "SoLan_Say";
				colvarSoLanSay.DataType = DbType.Int32;
				colvarSoLanSay.MaxLength = 0;
				colvarSoLanSay.AutoIncrement = false;
				colvarSoLanSay.IsNullable = true;
				colvarSoLanSay.IsPrimaryKey = false;
				colvarSoLanSay.IsForeignKey = false;
				colvarSoLanSay.IsReadOnly = false;
				colvarSoLanSay.DefaultSetting = @"";
				colvarSoLanSay.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLanSay);
				
				TableSchema.TableColumn colvarSoLanThaiChetLuu = new TableSchema.TableColumn(schema);
				colvarSoLanThaiChetLuu.ColumnName = "SoLan_ThaiChetLuu";
				colvarSoLanThaiChetLuu.DataType = DbType.Int32;
				colvarSoLanThaiChetLuu.MaxLength = 0;
				colvarSoLanThaiChetLuu.AutoIncrement = false;
				colvarSoLanThaiChetLuu.IsNullable = true;
				colvarSoLanThaiChetLuu.IsPrimaryKey = false;
				colvarSoLanThaiChetLuu.IsForeignKey = false;
				colvarSoLanThaiChetLuu.IsReadOnly = false;
				colvarSoLanThaiChetLuu.DefaultSetting = @"";
				colvarSoLanThaiChetLuu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLanThaiChetLuu);
				
				TableSchema.TableColumn colvarSoLanPhaThaiTaiDay = new TableSchema.TableColumn(schema);
				colvarSoLanPhaThaiTaiDay.ColumnName = "SoLan_PhaThai_TaiDay";
				colvarSoLanPhaThaiTaiDay.DataType = DbType.Int32;
				colvarSoLanPhaThaiTaiDay.MaxLength = 0;
				colvarSoLanPhaThaiTaiDay.AutoIncrement = false;
				colvarSoLanPhaThaiTaiDay.IsNullable = true;
				colvarSoLanPhaThaiTaiDay.IsPrimaryKey = false;
				colvarSoLanPhaThaiTaiDay.IsForeignKey = false;
				colvarSoLanPhaThaiTaiDay.IsReadOnly = false;
				colvarSoLanPhaThaiTaiDay.DefaultSetting = @"";
				colvarSoLanPhaThaiTaiDay.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLanPhaThaiTaiDay);
				
				TableSchema.TableColumn colvarThoiGianPhaThaiTaiDay = new TableSchema.TableColumn(schema);
				colvarThoiGianPhaThaiTaiDay.ColumnName = "ThoiGian_PhaThai_TaiDay";
				colvarThoiGianPhaThaiTaiDay.DataType = DbType.String;
				colvarThoiGianPhaThaiTaiDay.MaxLength = 100;
				colvarThoiGianPhaThaiTaiDay.AutoIncrement = false;
				colvarThoiGianPhaThaiTaiDay.IsNullable = true;
				colvarThoiGianPhaThaiTaiDay.IsPrimaryKey = false;
				colvarThoiGianPhaThaiTaiDay.IsForeignKey = false;
				colvarThoiGianPhaThaiTaiDay.IsReadOnly = false;
				colvarThoiGianPhaThaiTaiDay.DefaultSetting = @"";
				colvarThoiGianPhaThaiTaiDay.ForeignKeyTableName = "";
				schema.Columns.Add(colvarThoiGianPhaThaiTaiDay);
				
				TableSchema.TableColumn colvarLyDoPhaThaiLanTruoc = new TableSchema.TableColumn(schema);
				colvarLyDoPhaThaiLanTruoc.ColumnName = "LyDo_PhaThai_LanTruoc";
				colvarLyDoPhaThaiLanTruoc.DataType = DbType.String;
				colvarLyDoPhaThaiLanTruoc.MaxLength = 100;
				colvarLyDoPhaThaiLanTruoc.AutoIncrement = false;
				colvarLyDoPhaThaiLanTruoc.IsNullable = true;
				colvarLyDoPhaThaiLanTruoc.IsPrimaryKey = false;
				colvarLyDoPhaThaiLanTruoc.IsForeignKey = false;
				colvarLyDoPhaThaiLanTruoc.IsReadOnly = false;
				colvarLyDoPhaThaiLanTruoc.DefaultSetting = @"";
				colvarLyDoPhaThaiLanTruoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLyDoPhaThaiLanTruoc);
				
				TableSchema.TableColumn colvarLyDoPhaThaiLanNay = new TableSchema.TableColumn(schema);
				colvarLyDoPhaThaiLanNay.ColumnName = "LyDo_PhaThai_LanNay";
				colvarLyDoPhaThaiLanNay.DataType = DbType.String;
				colvarLyDoPhaThaiLanNay.MaxLength = 100;
				colvarLyDoPhaThaiLanNay.AutoIncrement = false;
				colvarLyDoPhaThaiLanNay.IsNullable = true;
				colvarLyDoPhaThaiLanNay.IsPrimaryKey = false;
				colvarLyDoPhaThaiLanNay.IsForeignKey = false;
				colvarLyDoPhaThaiLanNay.IsReadOnly = false;
				colvarLyDoPhaThaiLanNay.DefaultSetting = @"";
				colvarLyDoPhaThaiLanNay.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLyDoPhaThaiLanNay);
				
				TableSchema.TableColumn colvarTienSuNoiNgoai = new TableSchema.TableColumn(schema);
				colvarTienSuNoiNgoai.ColumnName = "TienSu_NoiNgoai";
				colvarTienSuNoiNgoai.DataType = DbType.String;
				colvarTienSuNoiNgoai.MaxLength = 500;
				colvarTienSuNoiNgoai.AutoIncrement = false;
				colvarTienSuNoiNgoai.IsNullable = true;
				colvarTienSuNoiNgoai.IsPrimaryKey = false;
				colvarTienSuNoiNgoai.IsForeignKey = false;
				colvarTienSuNoiNgoai.IsReadOnly = false;
				colvarTienSuNoiNgoai.DefaultSetting = @"";
				colvarTienSuNoiNgoai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTienSuNoiNgoai);
				
				TableSchema.TableColumn colvarTienSuGiaDinh = new TableSchema.TableColumn(schema);
				colvarTienSuGiaDinh.ColumnName = "TienSu_GiaDinh";
				colvarTienSuGiaDinh.DataType = DbType.String;
				colvarTienSuGiaDinh.MaxLength = 500;
				colvarTienSuGiaDinh.AutoIncrement = false;
				colvarTienSuGiaDinh.IsNullable = true;
				colvarTienSuGiaDinh.IsPrimaryKey = false;
				colvarTienSuGiaDinh.IsForeignKey = false;
				colvarTienSuGiaDinh.IsReadOnly = false;
				colvarTienSuGiaDinh.DefaultSetting = @"";
				colvarTienSuGiaDinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTienSuGiaDinh);
				
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
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("T_TIENSU",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("TienSuId")]
		[Bindable(true)]
		public int TienSuId 
		{
			get { return GetColumnValue<int>(Columns.TienSuId); }
			set { SetColumnValue(Columns.TienSuId, value); }
		}
		  
		[XmlAttribute("PatientId")]
		[Bindable(true)]
		public long? PatientId 
		{
			get { return GetColumnValue<long?>(Columns.PatientId); }
			set { SetColumnValue(Columns.PatientId, value); }
		}
		  
		[XmlAttribute("Para")]
		[Bindable(true)]
		public string Para 
		{
			get { return GetColumnValue<string>(Columns.Para); }
			set { SetColumnValue(Columns.Para, value); }
		}
		  
		[XmlAttribute("SoConTrai")]
		[Bindable(true)]
		public int? SoConTrai 
		{
			get { return GetColumnValue<int?>(Columns.SoConTrai); }
			set { SetColumnValue(Columns.SoConTrai, value); }
		}
		  
		[XmlAttribute("SoConGai")]
		[Bindable(true)]
		public int? SoConGai 
		{
			get { return GetColumnValue<int?>(Columns.SoConGai); }
			set { SetColumnValue(Columns.SoConGai, value); }
		}
		  
		[XmlAttribute("ConNhoNhat")]
		[Bindable(true)]
		public int? ConNhoNhat 
		{
			get { return GetColumnValue<int?>(Columns.ConNhoNhat); }
			set { SetColumnValue(Columns.ConNhoNhat, value); }
		}
		  
		[XmlAttribute("TaiBien")]
		[Bindable(true)]
		public string TaiBien 
		{
			get { return GetColumnValue<string>(Columns.TaiBien); }
			set { SetColumnValue(Columns.TaiBien, value); }
		}
		  
		[XmlAttribute("TienSuMoLayThai")]
		[Bindable(true)]
		public string TienSuMoLayThai 
		{
			get { return GetColumnValue<string>(Columns.TienSuMoLayThai); }
			set { SetColumnValue(Columns.TienSuMoLayThai, value); }
		}
		  
		[XmlAttribute("SoLanMoLayThai")]
		[Bindable(true)]
		public int? SoLanMoLayThai 
		{
			get { return GetColumnValue<int?>(Columns.SoLanMoLayThai); }
			set { SetColumnValue(Columns.SoLanMoLayThai, value); }
		}
		  
		[XmlAttribute("NamMoLayThai")]
		[Bindable(true)]
		public int? NamMoLayThai 
		{
			get { return GetColumnValue<int?>(Columns.NamMoLayThai); }
			set { SetColumnValue(Columns.NamMoLayThai, value); }
		}
		  
		[XmlAttribute("TienSuPhaThai")]
		[Bindable(true)]
		public string TienSuPhaThai 
		{
			get { return GetColumnValue<string>(Columns.TienSuPhaThai); }
			set { SetColumnValue(Columns.TienSuPhaThai, value); }
		}
		  
		[XmlAttribute("SoLanHutThai")]
		[Bindable(true)]
		public int? SoLanHutThai 
		{
			get { return GetColumnValue<int?>(Columns.SoLanHutThai); }
			set { SetColumnValue(Columns.SoLanHutThai, value); }
		}
		  
		[XmlAttribute("SoLanPhaThaiTo")]
		[Bindable(true)]
		public int? SoLanPhaThaiTo 
		{
			get { return GetColumnValue<int?>(Columns.SoLanPhaThaiTo); }
			set { SetColumnValue(Columns.SoLanPhaThaiTo, value); }
		}
		  
		[XmlAttribute("SoLanSay")]
		[Bindable(true)]
		public int? SoLanSay 
		{
			get { return GetColumnValue<int?>(Columns.SoLanSay); }
			set { SetColumnValue(Columns.SoLanSay, value); }
		}
		  
		[XmlAttribute("SoLanThaiChetLuu")]
		[Bindable(true)]
		public int? SoLanThaiChetLuu 
		{
			get { return GetColumnValue<int?>(Columns.SoLanThaiChetLuu); }
			set { SetColumnValue(Columns.SoLanThaiChetLuu, value); }
		}
		  
		[XmlAttribute("SoLanPhaThaiTaiDay")]
		[Bindable(true)]
		public int? SoLanPhaThaiTaiDay 
		{
			get { return GetColumnValue<int?>(Columns.SoLanPhaThaiTaiDay); }
			set { SetColumnValue(Columns.SoLanPhaThaiTaiDay, value); }
		}
		  
		[XmlAttribute("ThoiGianPhaThaiTaiDay")]
		[Bindable(true)]
		public string ThoiGianPhaThaiTaiDay 
		{
			get { return GetColumnValue<string>(Columns.ThoiGianPhaThaiTaiDay); }
			set { SetColumnValue(Columns.ThoiGianPhaThaiTaiDay, value); }
		}
		  
		[XmlAttribute("LyDoPhaThaiLanTruoc")]
		[Bindable(true)]
		public string LyDoPhaThaiLanTruoc 
		{
			get { return GetColumnValue<string>(Columns.LyDoPhaThaiLanTruoc); }
			set { SetColumnValue(Columns.LyDoPhaThaiLanTruoc, value); }
		}
		  
		[XmlAttribute("LyDoPhaThaiLanNay")]
		[Bindable(true)]
		public string LyDoPhaThaiLanNay 
		{
			get { return GetColumnValue<string>(Columns.LyDoPhaThaiLanNay); }
			set { SetColumnValue(Columns.LyDoPhaThaiLanNay, value); }
		}
		  
		[XmlAttribute("TienSuNoiNgoai")]
		[Bindable(true)]
		public string TienSuNoiNgoai 
		{
			get { return GetColumnValue<string>(Columns.TienSuNoiNgoai); }
			set { SetColumnValue(Columns.TienSuNoiNgoai, value); }
		}
		  
		[XmlAttribute("TienSuGiaDinh")]
		[Bindable(true)]
		public string TienSuGiaDinh 
		{
			get { return GetColumnValue<string>(Columns.TienSuGiaDinh); }
			set { SetColumnValue(Columns.TienSuGiaDinh, value); }
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
		public static void Insert(int varTienSuId,long? varPatientId,string varPara,int? varSoConTrai,int? varSoConGai,int? varConNhoNhat,string varTaiBien,string varTienSuMoLayThai,int? varSoLanMoLayThai,int? varNamMoLayThai,string varTienSuPhaThai,int? varSoLanHutThai,int? varSoLanPhaThaiTo,int? varSoLanSay,int? varSoLanThaiChetLuu,int? varSoLanPhaThaiTaiDay,string varThoiGianPhaThaiTaiDay,string varLyDoPhaThaiLanTruoc,string varLyDoPhaThaiLanNay,string varTienSuNoiNgoai,string varTienSuGiaDinh,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			TTiensu item = new TTiensu();
			
			item.TienSuId = varTienSuId;
			
			item.PatientId = varPatientId;
			
			item.Para = varPara;
			
			item.SoConTrai = varSoConTrai;
			
			item.SoConGai = varSoConGai;
			
			item.ConNhoNhat = varConNhoNhat;
			
			item.TaiBien = varTaiBien;
			
			item.TienSuMoLayThai = varTienSuMoLayThai;
			
			item.SoLanMoLayThai = varSoLanMoLayThai;
			
			item.NamMoLayThai = varNamMoLayThai;
			
			item.TienSuPhaThai = varTienSuPhaThai;
			
			item.SoLanHutThai = varSoLanHutThai;
			
			item.SoLanPhaThaiTo = varSoLanPhaThaiTo;
			
			item.SoLanSay = varSoLanSay;
			
			item.SoLanThaiChetLuu = varSoLanThaiChetLuu;
			
			item.SoLanPhaThaiTaiDay = varSoLanPhaThaiTaiDay;
			
			item.ThoiGianPhaThaiTaiDay = varThoiGianPhaThaiTaiDay;
			
			item.LyDoPhaThaiLanTruoc = varLyDoPhaThaiLanTruoc;
			
			item.LyDoPhaThaiLanNay = varLyDoPhaThaiLanNay;
			
			item.TienSuNoiNgoai = varTienSuNoiNgoai;
			
			item.TienSuGiaDinh = varTienSuGiaDinh;
			
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
		public static void Update(int varTienSuId,long? varPatientId,string varPara,int? varSoConTrai,int? varSoConGai,int? varConNhoNhat,string varTaiBien,string varTienSuMoLayThai,int? varSoLanMoLayThai,int? varNamMoLayThai,string varTienSuPhaThai,int? varSoLanHutThai,int? varSoLanPhaThaiTo,int? varSoLanSay,int? varSoLanThaiChetLuu,int? varSoLanPhaThaiTaiDay,string varThoiGianPhaThaiTaiDay,string varLyDoPhaThaiLanTruoc,string varLyDoPhaThaiLanNay,string varTienSuNoiNgoai,string varTienSuGiaDinh,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			TTiensu item = new TTiensu();
			
				item.TienSuId = varTienSuId;
			
				item.PatientId = varPatientId;
			
				item.Para = varPara;
			
				item.SoConTrai = varSoConTrai;
			
				item.SoConGai = varSoConGai;
			
				item.ConNhoNhat = varConNhoNhat;
			
				item.TaiBien = varTaiBien;
			
				item.TienSuMoLayThai = varTienSuMoLayThai;
			
				item.SoLanMoLayThai = varSoLanMoLayThai;
			
				item.NamMoLayThai = varNamMoLayThai;
			
				item.TienSuPhaThai = varTienSuPhaThai;
			
				item.SoLanHutThai = varSoLanHutThai;
			
				item.SoLanPhaThaiTo = varSoLanPhaThaiTo;
			
				item.SoLanSay = varSoLanSay;
			
				item.SoLanThaiChetLuu = varSoLanThaiChetLuu;
			
				item.SoLanPhaThaiTaiDay = varSoLanPhaThaiTaiDay;
			
				item.ThoiGianPhaThaiTaiDay = varThoiGianPhaThaiTaiDay;
			
				item.LyDoPhaThaiLanTruoc = varLyDoPhaThaiLanTruoc;
			
				item.LyDoPhaThaiLanNay = varLyDoPhaThaiLanNay;
			
				item.TienSuNoiNgoai = varTienSuNoiNgoai;
			
				item.TienSuGiaDinh = varTienSuGiaDinh;
			
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
        
        
        public static TableSchema.TableColumn TienSuIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn PatientIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ParaColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SoConTraiColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SoConGaiColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn ConNhoNhatColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn TaiBienColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn TienSuMoLayThaiColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLanMoLayThaiColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn NamMoLayThaiColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn TienSuPhaThaiColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLanHutThaiColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLanPhaThaiToColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLanSayColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLanThaiChetLuuColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLanPhaThaiTaiDayColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn ThoiGianPhaThaiTaiDayColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn LyDoPhaThaiLanTruocColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn LyDoPhaThaiLanNayColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn TienSuNoiNgoaiColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn TienSuGiaDinhColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[23]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[24]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string TienSuId = @"TienSu_ID";
			 public static string PatientId = @"Patient_ID";
			 public static string Para = @"Para";
			 public static string SoConTrai = @"So_Con_Trai";
			 public static string SoConGai = @"So_Con_Gai";
			 public static string ConNhoNhat = @"Con_Nho_Nhat";
			 public static string TaiBien = @"Tai_Bien";
			 public static string TienSuMoLayThai = @"TienSu_MoLayThai";
			 public static string SoLanMoLayThai = @"SoLan_MoLayThai";
			 public static string NamMoLayThai = @"Nam_MoLayThai";
			 public static string TienSuPhaThai = @"TienSu_PhaThai";
			 public static string SoLanHutThai = @"SoLan_HutThai";
			 public static string SoLanPhaThaiTo = @"SoLan_PhaThaiTo";
			 public static string SoLanSay = @"SoLan_Say";
			 public static string SoLanThaiChetLuu = @"SoLan_ThaiChetLuu";
			 public static string SoLanPhaThaiTaiDay = @"SoLan_PhaThai_TaiDay";
			 public static string ThoiGianPhaThaiTaiDay = @"ThoiGian_PhaThai_TaiDay";
			 public static string LyDoPhaThaiLanTruoc = @"LyDo_PhaThai_LanTruoc";
			 public static string LyDoPhaThaiLanNay = @"LyDo_PhaThai_LanNay";
			 public static string TienSuNoiNgoai = @"TienSu_NoiNgoai";
			 public static string TienSuGiaDinh = @"TienSu_GiaDinh";
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
