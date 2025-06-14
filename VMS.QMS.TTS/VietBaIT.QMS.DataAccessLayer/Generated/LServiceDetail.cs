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
	/// Strongly-typed collection for the LServiceDetail class.
	/// </summary>
    [Serializable]
	public partial class LServiceDetailCollection : ActiveList<LServiceDetail, LServiceDetailCollection>
	{	   
		public LServiceDetailCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LServiceDetailCollection</returns>
		public LServiceDetailCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LServiceDetail o = this[i];
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
	/// This is an ActiveRecord class which wraps the L_Service_Detail table.
	/// </summary>
	[Serializable]
	public partial class LServiceDetail : ActiveRecord<LServiceDetail>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LServiceDetail()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LServiceDetail(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LServiceDetail(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LServiceDetail(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("L_Service_Detail", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarServiceDetailId = new TableSchema.TableColumn(schema);
				colvarServiceDetailId.ColumnName = "ServiceDetail_ID";
				colvarServiceDetailId.DataType = DbType.Int32;
				colvarServiceDetailId.MaxLength = 0;
				colvarServiceDetailId.AutoIncrement = true;
				colvarServiceDetailId.IsNullable = false;
				colvarServiceDetailId.IsPrimaryKey = true;
				colvarServiceDetailId.IsForeignKey = false;
				colvarServiceDetailId.IsReadOnly = false;
				colvarServiceDetailId.DefaultSetting = @"";
				colvarServiceDetailId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarServiceDetailId);
				
				TableSchema.TableColumn colvarServiceDetailCode = new TableSchema.TableColumn(schema);
				colvarServiceDetailCode.ColumnName = "ServiceDetail_Code";
				colvarServiceDetailCode.DataType = DbType.AnsiString;
				colvarServiceDetailCode.MaxLength = 20;
				colvarServiceDetailCode.AutoIncrement = false;
				colvarServiceDetailCode.IsNullable = true;
				colvarServiceDetailCode.IsPrimaryKey = false;
				colvarServiceDetailCode.IsForeignKey = false;
				colvarServiceDetailCode.IsReadOnly = false;
				colvarServiceDetailCode.DefaultSetting = @"";
				colvarServiceDetailCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarServiceDetailCode);
				
				TableSchema.TableColumn colvarServiceDetailName = new TableSchema.TableColumn(schema);
				colvarServiceDetailName.ColumnName = "ServiceDetail_Name";
				colvarServiceDetailName.DataType = DbType.String;
				colvarServiceDetailName.MaxLength = 300;
				colvarServiceDetailName.AutoIncrement = false;
				colvarServiceDetailName.IsNullable = false;
				colvarServiceDetailName.IsPrimaryKey = false;
				colvarServiceDetailName.IsForeignKey = false;
				colvarServiceDetailName.IsReadOnly = false;
				colvarServiceDetailName.DefaultSetting = @"";
				colvarServiceDetailName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarServiceDetailName);
				
				TableSchema.TableColumn colvarTestResult = new TableSchema.TableColumn(schema);
				colvarTestResult.ColumnName = "Test_Result";
				colvarTestResult.DataType = DbType.String;
				colvarTestResult.MaxLength = 100;
				colvarTestResult.AutoIncrement = false;
				colvarTestResult.IsNullable = true;
				colvarTestResult.IsPrimaryKey = false;
				colvarTestResult.IsForeignKey = false;
				colvarTestResult.IsReadOnly = false;
				colvarTestResult.DefaultSetting = @"";
				colvarTestResult.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTestResult);
				
				TableSchema.TableColumn colvarServiceId = new TableSchema.TableColumn(schema);
				colvarServiceId.ColumnName = "Service_ID";
				colvarServiceId.DataType = DbType.Int16;
				colvarServiceId.MaxLength = 0;
				colvarServiceId.AutoIncrement = false;
				colvarServiceId.IsNullable = false;
				colvarServiceId.IsPrimaryKey = false;
				colvarServiceId.IsForeignKey = false;
				colvarServiceId.IsReadOnly = false;
				colvarServiceId.DefaultSetting = @"";
				colvarServiceId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarServiceId);
				
				TableSchema.TableColumn colvarMeasureUnit = new TableSchema.TableColumn(schema);
				colvarMeasureUnit.ColumnName = "Measure_Unit";
				colvarMeasureUnit.DataType = DbType.String;
				colvarMeasureUnit.MaxLength = 100;
				colvarMeasureUnit.AutoIncrement = false;
				colvarMeasureUnit.IsNullable = true;
				colvarMeasureUnit.IsPrimaryKey = false;
				colvarMeasureUnit.IsForeignKey = false;
				colvarMeasureUnit.IsReadOnly = false;
				colvarMeasureUnit.DefaultSetting = @"";
				colvarMeasureUnit.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMeasureUnit);
				
				TableSchema.TableColumn colvarMeasureType = new TableSchema.TableColumn(schema);
				colvarMeasureType.ColumnName = "Measure_Type";
				colvarMeasureType.DataType = DbType.Byte;
				colvarMeasureType.MaxLength = 0;
				colvarMeasureType.AutoIncrement = false;
				colvarMeasureType.IsNullable = true;
				colvarMeasureType.IsPrimaryKey = false;
				colvarMeasureType.IsForeignKey = false;
				colvarMeasureType.IsReadOnly = false;
				
						colvarMeasureType.DefaultSetting = @"((0))";
				colvarMeasureType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMeasureType);
				
				TableSchema.TableColumn colvarNormalLevel0 = new TableSchema.TableColumn(schema);
				colvarNormalLevel0.ColumnName = "Normal_Level_0";
				colvarNormalLevel0.DataType = DbType.String;
				colvarNormalLevel0.MaxLength = 100;
				colvarNormalLevel0.AutoIncrement = false;
				colvarNormalLevel0.IsNullable = true;
				colvarNormalLevel0.IsPrimaryKey = false;
				colvarNormalLevel0.IsForeignKey = false;
				colvarNormalLevel0.IsReadOnly = false;
				colvarNormalLevel0.DefaultSetting = @"";
				colvarNormalLevel0.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNormalLevel0);
				
				TableSchema.TableColumn colvarNormalLevel1 = new TableSchema.TableColumn(schema);
				colvarNormalLevel1.ColumnName = "Normal_Level_1";
				colvarNormalLevel1.DataType = DbType.String;
				colvarNormalLevel1.MaxLength = 100;
				colvarNormalLevel1.AutoIncrement = false;
				colvarNormalLevel1.IsNullable = true;
				colvarNormalLevel1.IsPrimaryKey = false;
				colvarNormalLevel1.IsForeignKey = false;
				colvarNormalLevel1.IsReadOnly = false;
				colvarNormalLevel1.DefaultSetting = @"";
				colvarNormalLevel1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNormalLevel1);
				
				TableSchema.TableColumn colvarValid = new TableSchema.TableColumn(schema);
				colvarValid.ColumnName = "Valid";
				colvarValid.DataType = DbType.Byte;
				colvarValid.MaxLength = 0;
				colvarValid.AutoIncrement = false;
				colvarValid.IsNullable = true;
				colvarValid.IsPrimaryKey = false;
				colvarValid.IsForeignKey = false;
				colvarValid.IsReadOnly = false;
				colvarValid.DefaultSetting = @"";
				colvarValid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarValid);
				
				TableSchema.TableColumn colvarPrice = new TableSchema.TableColumn(schema);
				colvarPrice.ColumnName = "Price";
				colvarPrice.DataType = DbType.Decimal;
				colvarPrice.MaxLength = 0;
				colvarPrice.AutoIncrement = false;
				colvarPrice.IsNullable = true;
				colvarPrice.IsPrimaryKey = false;
				colvarPrice.IsForeignKey = false;
				colvarPrice.IsReadOnly = false;
				
						colvarPrice.DefaultSetting = @"((0))";
				colvarPrice.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPrice);
				
				TableSchema.TableColumn colvarSDesc = new TableSchema.TableColumn(schema);
				colvarSDesc.ColumnName = "sDesc";
				colvarSDesc.DataType = DbType.String;
				colvarSDesc.MaxLength = 255;
				colvarSDesc.AutoIncrement = false;
				colvarSDesc.IsNullable = true;
				colvarSDesc.IsPrimaryKey = false;
				colvarSDesc.IsForeignKey = false;
				colvarSDesc.IsReadOnly = false;
				colvarSDesc.DefaultSetting = @"";
				colvarSDesc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSDesc);
				
				TableSchema.TableColumn colvarGroupDetailId = new TableSchema.TableColumn(schema);
				colvarGroupDetailId.ColumnName = "Group_Detail_ID";
				colvarGroupDetailId.DataType = DbType.Int16;
				colvarGroupDetailId.MaxLength = 0;
				colvarGroupDetailId.AutoIncrement = false;
				colvarGroupDetailId.IsNullable = true;
				colvarGroupDetailId.IsPrimaryKey = false;
				colvarGroupDetailId.IsForeignKey = false;
				colvarGroupDetailId.IsReadOnly = false;
				colvarGroupDetailId.DefaultSetting = @"";
				colvarGroupDetailId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGroupDetailId);
				
				TableSchema.TableColumn colvarIntOrder = new TableSchema.TableColumn(schema);
				colvarIntOrder.ColumnName = "IntOrder";
				colvarIntOrder.DataType = DbType.Int32;
				colvarIntOrder.MaxLength = 0;
				colvarIntOrder.AutoIncrement = false;
				colvarIntOrder.IsNullable = true;
				colvarIntOrder.IsPrimaryKey = false;
				colvarIntOrder.IsForeignKey = false;
				colvarIntOrder.IsReadOnly = false;
				colvarIntOrder.DefaultSetting = @"";
				colvarIntOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIntOrder);
				
				TableSchema.TableColumn colvarSendAllId = new TableSchema.TableColumn(schema);
				colvarSendAllId.ColumnName = "Send_All_ID";
				colvarSendAllId.DataType = DbType.Byte;
				colvarSendAllId.MaxLength = 0;
				colvarSendAllId.AutoIncrement = false;
				colvarSendAllId.IsNullable = true;
				colvarSendAllId.IsPrimaryKey = false;
				colvarSendAllId.IsForeignKey = false;
				colvarSendAllId.IsReadOnly = false;
				
						colvarSendAllId.DefaultSetting = @"((0))";
				colvarSendAllId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSendAllId);
				
				TableSchema.TableColumn colvarUnitId = new TableSchema.TableColumn(schema);
				colvarUnitId.ColumnName = "Unit_ID";
				colvarUnitId.DataType = DbType.Int32;
				colvarUnitId.MaxLength = 0;
				colvarUnitId.AutoIncrement = false;
				colvarUnitId.IsNullable = true;
				colvarUnitId.IsPrimaryKey = false;
				colvarUnitId.IsForeignKey = false;
				colvarUnitId.IsReadOnly = false;
				colvarUnitId.DefaultSetting = @"";
				colvarUnitId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUnitId);
				
				TableSchema.TableColumn colvarFormId = new TableSchema.TableColumn(schema);
				colvarFormId.ColumnName = "Form_ID";
				colvarFormId.DataType = DbType.Int32;
				colvarFormId.MaxLength = 0;
				colvarFormId.AutoIncrement = false;
				colvarFormId.IsNullable = true;
				colvarFormId.IsPrimaryKey = false;
				colvarFormId.IsForeignKey = false;
				colvarFormId.IsReadOnly = false;
				colvarFormId.DefaultSetting = @"";
				colvarFormId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFormId);
				
				TableSchema.TableColumn colvarMaChiTietKetNoi = new TableSchema.TableColumn(schema);
				colvarMaChiTietKetNoi.ColumnName = "Ma_ChiTiet_KetNoi";
				colvarMaChiTietKetNoi.DataType = DbType.AnsiString;
				colvarMaChiTietKetNoi.MaxLength = 50;
				colvarMaChiTietKetNoi.AutoIncrement = false;
				colvarMaChiTietKetNoi.IsNullable = true;
				colvarMaChiTietKetNoi.IsPrimaryKey = false;
				colvarMaChiTietKetNoi.IsForeignKey = false;
				colvarMaChiTietKetNoi.IsReadOnly = false;
				colvarMaChiTietKetNoi.DefaultSetting = @"";
				colvarMaChiTietKetNoi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaChiTietKetNoi);
				
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
				
				TableSchema.TableColumn colvarThuocGoi = new TableSchema.TableColumn(schema);
				colvarThuocGoi.ColumnName = "THUOC_GOI";
				colvarThuocGoi.DataType = DbType.Int32;
				colvarThuocGoi.MaxLength = 0;
				colvarThuocGoi.AutoIncrement = false;
				colvarThuocGoi.IsNullable = true;
				colvarThuocGoi.IsPrimaryKey = false;
				colvarThuocGoi.IsForeignKey = false;
				colvarThuocGoi.IsReadOnly = false;
				colvarThuocGoi.DefaultSetting = @"";
				colvarThuocGoi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarThuocGoi);
				
				TableSchema.TableColumn colvarMaKhoaThien = new TableSchema.TableColumn(schema);
				colvarMaKhoaThien.ColumnName = "MA_KHOA_THIEN";
				colvarMaKhoaThien.DataType = DbType.String;
				colvarMaKhoaThien.MaxLength = 10;
				colvarMaKhoaThien.AutoIncrement = false;
				colvarMaKhoaThien.IsNullable = true;
				colvarMaKhoaThien.IsPrimaryKey = false;
				colvarMaKhoaThien.IsForeignKey = false;
				colvarMaKhoaThien.IsReadOnly = false;
				colvarMaKhoaThien.DefaultSetting = @"";
				colvarMaKhoaThien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaKhoaThien);
				
				TableSchema.TableColumn colvarDepartmentId = new TableSchema.TableColumn(schema);
				colvarDepartmentId.ColumnName = "Department_ID";
				colvarDepartmentId.DataType = DbType.Int16;
				colvarDepartmentId.MaxLength = 0;
				colvarDepartmentId.AutoIncrement = false;
				colvarDepartmentId.IsNullable = true;
				colvarDepartmentId.IsPrimaryKey = false;
				colvarDepartmentId.IsForeignKey = false;
				colvarDepartmentId.IsReadOnly = false;
				colvarDepartmentId.DefaultSetting = @"";
				colvarDepartmentId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDepartmentId);
				
				TableSchema.TableColumn colvarTenBhyt = new TableSchema.TableColumn(schema);
				colvarTenBhyt.ColumnName = "TEN_BHYT";
				colvarTenBhyt.DataType = DbType.String;
				colvarTenBhyt.MaxLength = 300;
				colvarTenBhyt.AutoIncrement = false;
				colvarTenBhyt.IsNullable = true;
				colvarTenBhyt.IsPrimaryKey = false;
				colvarTenBhyt.IsForeignKey = false;
				colvarTenBhyt.IsReadOnly = false;
				colvarTenBhyt.DefaultSetting = @"";
				colvarTenBhyt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenBhyt);
				
				TableSchema.TableColumn colvarMaBhyt = new TableSchema.TableColumn(schema);
				colvarMaBhyt.ColumnName = "Ma_BHYT";
				colvarMaBhyt.DataType = DbType.String;
				colvarMaBhyt.MaxLength = 50;
				colvarMaBhyt.AutoIncrement = false;
				colvarMaBhyt.IsNullable = true;
				colvarMaBhyt.IsPrimaryKey = false;
				colvarMaBhyt.IsForeignKey = false;
				colvarMaBhyt.IsReadOnly = false;
				colvarMaBhyt.DefaultSetting = @"";
				colvarMaBhyt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaBhyt);
				
				TableSchema.TableColumn colvarNqTt = new TableSchema.TableColumn(schema);
				colvarNqTt.ColumnName = "NQ_TT";
				colvarNqTt.DataType = DbType.String;
				colvarNqTt.MaxLength = 50;
				colvarNqTt.AutoIncrement = false;
				colvarNqTt.IsNullable = true;
				colvarNqTt.IsPrimaryKey = false;
				colvarNqTt.IsForeignKey = false;
				colvarNqTt.IsReadOnly = false;
				colvarNqTt.DefaultSetting = @"";
				colvarNqTt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNqTt);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("L_Service_Detail",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("ServiceDetailId")]
		[Bindable(true)]
		public int ServiceDetailId 
		{
			get { return GetColumnValue<int>(Columns.ServiceDetailId); }
			set { SetColumnValue(Columns.ServiceDetailId, value); }
		}
		  
		[XmlAttribute("ServiceDetailCode")]
		[Bindable(true)]
		public string ServiceDetailCode 
		{
			get { return GetColumnValue<string>(Columns.ServiceDetailCode); }
			set { SetColumnValue(Columns.ServiceDetailCode, value); }
		}
		  
		[XmlAttribute("ServiceDetailName")]
		[Bindable(true)]
		public string ServiceDetailName 
		{
			get { return GetColumnValue<string>(Columns.ServiceDetailName); }
			set { SetColumnValue(Columns.ServiceDetailName, value); }
		}
		  
		[XmlAttribute("TestResult")]
		[Bindable(true)]
		public string TestResult 
		{
			get { return GetColumnValue<string>(Columns.TestResult); }
			set { SetColumnValue(Columns.TestResult, value); }
		}
		  
		[XmlAttribute("ServiceId")]
		[Bindable(true)]
		public short ServiceId 
		{
			get { return GetColumnValue<short>(Columns.ServiceId); }
			set { SetColumnValue(Columns.ServiceId, value); }
		}
		  
		[XmlAttribute("MeasureUnit")]
		[Bindable(true)]
		public string MeasureUnit 
		{
			get { return GetColumnValue<string>(Columns.MeasureUnit); }
			set { SetColumnValue(Columns.MeasureUnit, value); }
		}
		  
		[XmlAttribute("MeasureType")]
		[Bindable(true)]
		public byte? MeasureType 
		{
			get { return GetColumnValue<byte?>(Columns.MeasureType); }
			set { SetColumnValue(Columns.MeasureType, value); }
		}
		  
		[XmlAttribute("NormalLevel0")]
		[Bindable(true)]
		public string NormalLevel0 
		{
			get { return GetColumnValue<string>(Columns.NormalLevel0); }
			set { SetColumnValue(Columns.NormalLevel0, value); }
		}
		  
		[XmlAttribute("NormalLevel1")]
		[Bindable(true)]
		public string NormalLevel1 
		{
			get { return GetColumnValue<string>(Columns.NormalLevel1); }
			set { SetColumnValue(Columns.NormalLevel1, value); }
		}
		  
		[XmlAttribute("Valid")]
		[Bindable(true)]
		public byte? Valid 
		{
			get { return GetColumnValue<byte?>(Columns.Valid); }
			set { SetColumnValue(Columns.Valid, value); }
		}
		  
		[XmlAttribute("Price")]
		[Bindable(true)]
		public decimal? Price 
		{
			get { return GetColumnValue<decimal?>(Columns.Price); }
			set { SetColumnValue(Columns.Price, value); }
		}
		  
		[XmlAttribute("SDesc")]
		[Bindable(true)]
		public string SDesc 
		{
			get { return GetColumnValue<string>(Columns.SDesc); }
			set { SetColumnValue(Columns.SDesc, value); }
		}
		  
		[XmlAttribute("GroupDetailId")]
		[Bindable(true)]
		public short? GroupDetailId 
		{
			get { return GetColumnValue<short?>(Columns.GroupDetailId); }
			set { SetColumnValue(Columns.GroupDetailId, value); }
		}
		  
		[XmlAttribute("IntOrder")]
		[Bindable(true)]
		public int? IntOrder 
		{
			get { return GetColumnValue<int?>(Columns.IntOrder); }
			set { SetColumnValue(Columns.IntOrder, value); }
		}
		  
		[XmlAttribute("SendAllId")]
		[Bindable(true)]
		public byte? SendAllId 
		{
			get { return GetColumnValue<byte?>(Columns.SendAllId); }
			set { SetColumnValue(Columns.SendAllId, value); }
		}
		  
		[XmlAttribute("UnitId")]
		[Bindable(true)]
		public int? UnitId 
		{
			get { return GetColumnValue<int?>(Columns.UnitId); }
			set { SetColumnValue(Columns.UnitId, value); }
		}
		  
		[XmlAttribute("FormId")]
		[Bindable(true)]
		public int? FormId 
		{
			get { return GetColumnValue<int?>(Columns.FormId); }
			set { SetColumnValue(Columns.FormId, value); }
		}
		  
		[XmlAttribute("MaChiTietKetNoi")]
		[Bindable(true)]
		public string MaChiTietKetNoi 
		{
			get { return GetColumnValue<string>(Columns.MaChiTietKetNoi); }
			set { SetColumnValue(Columns.MaChiTietKetNoi, value); }
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
		  
		[XmlAttribute("ThuocGoi")]
		[Bindable(true)]
		public int? ThuocGoi 
		{
			get { return GetColumnValue<int?>(Columns.ThuocGoi); }
			set { SetColumnValue(Columns.ThuocGoi, value); }
		}
		  
		[XmlAttribute("MaKhoaThien")]
		[Bindable(true)]
		public string MaKhoaThien 
		{
			get { return GetColumnValue<string>(Columns.MaKhoaThien); }
			set { SetColumnValue(Columns.MaKhoaThien, value); }
		}
		  
		[XmlAttribute("DepartmentId")]
		[Bindable(true)]
		public short? DepartmentId 
		{
			get { return GetColumnValue<short?>(Columns.DepartmentId); }
			set { SetColumnValue(Columns.DepartmentId, value); }
		}
		  
		[XmlAttribute("TenBhyt")]
		[Bindable(true)]
		public string TenBhyt 
		{
			get { return GetColumnValue<string>(Columns.TenBhyt); }
			set { SetColumnValue(Columns.TenBhyt, value); }
		}
		  
		[XmlAttribute("MaBhyt")]
		[Bindable(true)]
		public string MaBhyt 
		{
			get { return GetColumnValue<string>(Columns.MaBhyt); }
			set { SetColumnValue(Columns.MaBhyt, value); }
		}
		  
		[XmlAttribute("NqTt")]
		[Bindable(true)]
		public string NqTt 
		{
			get { return GetColumnValue<string>(Columns.NqTt); }
			set { SetColumnValue(Columns.NqTt, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varServiceDetailCode,string varServiceDetailName,string varTestResult,short varServiceId,string varMeasureUnit,byte? varMeasureType,string varNormalLevel0,string varNormalLevel1,byte? varValid,decimal? varPrice,string varSDesc,short? varGroupDetailId,int? varIntOrder,byte? varSendAllId,int? varUnitId,int? varFormId,string varMaChiTietKetNoi,DateTime? varNgayTao,string varNguoiTao,DateTime? varNgaySua,string varNguoiSua,int? varThuocGoi,string varMaKhoaThien,short? varDepartmentId,string varTenBhyt,string varMaBhyt,string varNqTt)
		{
			LServiceDetail item = new LServiceDetail();
			
			item.ServiceDetailCode = varServiceDetailCode;
			
			item.ServiceDetailName = varServiceDetailName;
			
			item.TestResult = varTestResult;
			
			item.ServiceId = varServiceId;
			
			item.MeasureUnit = varMeasureUnit;
			
			item.MeasureType = varMeasureType;
			
			item.NormalLevel0 = varNormalLevel0;
			
			item.NormalLevel1 = varNormalLevel1;
			
			item.Valid = varValid;
			
			item.Price = varPrice;
			
			item.SDesc = varSDesc;
			
			item.GroupDetailId = varGroupDetailId;
			
			item.IntOrder = varIntOrder;
			
			item.SendAllId = varSendAllId;
			
			item.UnitId = varUnitId;
			
			item.FormId = varFormId;
			
			item.MaChiTietKetNoi = varMaChiTietKetNoi;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgaySua = varNgaySua;
			
			item.NguoiSua = varNguoiSua;
			
			item.ThuocGoi = varThuocGoi;
			
			item.MaKhoaThien = varMaKhoaThien;
			
			item.DepartmentId = varDepartmentId;
			
			item.TenBhyt = varTenBhyt;
			
			item.MaBhyt = varMaBhyt;
			
			item.NqTt = varNqTt;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varServiceDetailId,string varServiceDetailCode,string varServiceDetailName,string varTestResult,short varServiceId,string varMeasureUnit,byte? varMeasureType,string varNormalLevel0,string varNormalLevel1,byte? varValid,decimal? varPrice,string varSDesc,short? varGroupDetailId,int? varIntOrder,byte? varSendAllId,int? varUnitId,int? varFormId,string varMaChiTietKetNoi,DateTime? varNgayTao,string varNguoiTao,DateTime? varNgaySua,string varNguoiSua,int? varThuocGoi,string varMaKhoaThien,short? varDepartmentId,string varTenBhyt,string varMaBhyt,string varNqTt)
		{
			LServiceDetail item = new LServiceDetail();
			
				item.ServiceDetailId = varServiceDetailId;
			
				item.ServiceDetailCode = varServiceDetailCode;
			
				item.ServiceDetailName = varServiceDetailName;
			
				item.TestResult = varTestResult;
			
				item.ServiceId = varServiceId;
			
				item.MeasureUnit = varMeasureUnit;
			
				item.MeasureType = varMeasureType;
			
				item.NormalLevel0 = varNormalLevel0;
			
				item.NormalLevel1 = varNormalLevel1;
			
				item.Valid = varValid;
			
				item.Price = varPrice;
			
				item.SDesc = varSDesc;
			
				item.GroupDetailId = varGroupDetailId;
			
				item.IntOrder = varIntOrder;
			
				item.SendAllId = varSendAllId;
			
				item.UnitId = varUnitId;
			
				item.FormId = varFormId;
			
				item.MaChiTietKetNoi = varMaChiTietKetNoi;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgaySua = varNgaySua;
			
				item.NguoiSua = varNguoiSua;
			
				item.ThuocGoi = varThuocGoi;
			
				item.MaKhoaThien = varMaKhoaThien;
			
				item.DepartmentId = varDepartmentId;
			
				item.TenBhyt = varTenBhyt;
			
				item.MaBhyt = varMaBhyt;
			
				item.NqTt = varNqTt;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn ServiceDetailIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn ServiceDetailCodeColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ServiceDetailNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn TestResultColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn ServiceIdColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn MeasureUnitColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn MeasureTypeColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn NormalLevel0Column
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn NormalLevel1Column
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn ValidColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn PriceColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn SDescColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn GroupDetailIdColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn IntOrderColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn SendAllIdColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn UnitIdColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn FormIdColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn MaChiTietKetNoiColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn ThuocGoiColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        public static TableSchema.TableColumn MaKhoaThienColumn
        {
            get { return Schema.Columns[23]; }
        }
        
        
        
        public static TableSchema.TableColumn DepartmentIdColumn
        {
            get { return Schema.Columns[24]; }
        }
        
        
        
        public static TableSchema.TableColumn TenBhytColumn
        {
            get { return Schema.Columns[25]; }
        }
        
        
        
        public static TableSchema.TableColumn MaBhytColumn
        {
            get { return Schema.Columns[26]; }
        }
        
        
        
        public static TableSchema.TableColumn NqTtColumn
        {
            get { return Schema.Columns[27]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string ServiceDetailId = @"ServiceDetail_ID";
			 public static string ServiceDetailCode = @"ServiceDetail_Code";
			 public static string ServiceDetailName = @"ServiceDetail_Name";
			 public static string TestResult = @"Test_Result";
			 public static string ServiceId = @"Service_ID";
			 public static string MeasureUnit = @"Measure_Unit";
			 public static string MeasureType = @"Measure_Type";
			 public static string NormalLevel0 = @"Normal_Level_0";
			 public static string NormalLevel1 = @"Normal_Level_1";
			 public static string Valid = @"Valid";
			 public static string Price = @"Price";
			 public static string SDesc = @"sDesc";
			 public static string GroupDetailId = @"Group_Detail_ID";
			 public static string IntOrder = @"IntOrder";
			 public static string SendAllId = @"Send_All_ID";
			 public static string UnitId = @"Unit_ID";
			 public static string FormId = @"Form_ID";
			 public static string MaChiTietKetNoi = @"Ma_ChiTiet_KetNoi";
			 public static string NgayTao = @"NGAY_TAO";
			 public static string NguoiTao = @"NGUOI_TAO";
			 public static string NgaySua = @"NGAY_SUA";
			 public static string NguoiSua = @"NGUOI_SUA";
			 public static string ThuocGoi = @"THUOC_GOI";
			 public static string MaKhoaThien = @"MA_KHOA_THIEN";
			 public static string DepartmentId = @"Department_ID";
			 public static string TenBhyt = @"TEN_BHYT";
			 public static string MaBhyt = @"Ma_BHYT";
			 public static string NqTt = @"NQ_TT";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
