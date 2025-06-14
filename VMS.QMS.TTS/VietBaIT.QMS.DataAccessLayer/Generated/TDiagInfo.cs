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
	/// Strongly-typed collection for the TDiagInfo class.
	/// </summary>
    [Serializable]
	public partial class TDiagInfoCollection : ActiveList<TDiagInfo, TDiagInfoCollection>
	{	   
		public TDiagInfoCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TDiagInfoCollection</returns>
		public TDiagInfoCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TDiagInfo o = this[i];
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
	/// This is an ActiveRecord class which wraps the T_Diag_Info table.
	/// </summary>
	[Serializable]
	public partial class TDiagInfo : ActiveRecord<TDiagInfo>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TDiagInfo()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TDiagInfo(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TDiagInfo(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TDiagInfo(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("T_Diag_Info", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarDiagId = new TableSchema.TableColumn(schema);
				colvarDiagId.ColumnName = "Diag_ID";
				colvarDiagId.DataType = DbType.Int64;
				colvarDiagId.MaxLength = 0;
				colvarDiagId.AutoIncrement = true;
				colvarDiagId.IsNullable = false;
				colvarDiagId.IsPrimaryKey = true;
				colvarDiagId.IsForeignKey = false;
				colvarDiagId.IsReadOnly = false;
				colvarDiagId.DefaultSetting = @"";
				colvarDiagId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDiagId);
				
				TableSchema.TableColumn colvarExamId = new TableSchema.TableColumn(schema);
				colvarExamId.ColumnName = "Exam_ID";
				colvarExamId.DataType = DbType.Int64;
				colvarExamId.MaxLength = 0;
				colvarExamId.AutoIncrement = false;
				colvarExamId.IsNullable = false;
				colvarExamId.IsPrimaryKey = false;
				colvarExamId.IsForeignKey = false;
				colvarExamId.IsReadOnly = false;
				colvarExamId.DefaultSetting = @"";
				colvarExamId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarExamId);
				
				TableSchema.TableColumn colvarPatientDeptId = new TableSchema.TableColumn(schema);
				colvarPatientDeptId.ColumnName = "PatientDept_ID";
				colvarPatientDeptId.DataType = DbType.Int32;
				colvarPatientDeptId.MaxLength = 0;
				colvarPatientDeptId.AutoIncrement = false;
				colvarPatientDeptId.IsNullable = true;
				colvarPatientDeptId.IsPrimaryKey = false;
				colvarPatientDeptId.IsForeignKey = false;
				colvarPatientDeptId.IsReadOnly = false;
				colvarPatientDeptId.DefaultSetting = @"";
				colvarPatientDeptId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPatientDeptId);
				
				TableSchema.TableColumn colvarPatientId = new TableSchema.TableColumn(schema);
				colvarPatientId.ColumnName = "Patient_ID";
				colvarPatientId.DataType = DbType.Int64;
				colvarPatientId.MaxLength = 0;
				colvarPatientId.AutoIncrement = false;
				colvarPatientId.IsNullable = false;
				colvarPatientId.IsPrimaryKey = false;
				colvarPatientId.IsForeignKey = false;
				colvarPatientId.IsReadOnly = false;
				colvarPatientId.DefaultSetting = @"";
				colvarPatientId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPatientId);
				
				TableSchema.TableColumn colvarPatientCode = new TableSchema.TableColumn(schema);
				colvarPatientCode.ColumnName = "Patient_Code";
				colvarPatientCode.DataType = DbType.AnsiString;
				colvarPatientCode.MaxLength = 10;
				colvarPatientCode.AutoIncrement = false;
				colvarPatientCode.IsNullable = false;
				colvarPatientCode.IsPrimaryKey = false;
				colvarPatientCode.IsForeignKey = false;
				colvarPatientCode.IsReadOnly = false;
				colvarPatientCode.DefaultSetting = @"";
				colvarPatientCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPatientCode);
				
				TableSchema.TableColumn colvarSummarizeInfo = new TableSchema.TableColumn(schema);
				colvarSummarizeInfo.ColumnName = "Summarize_Info";
				colvarSummarizeInfo.DataType = DbType.String;
				colvarSummarizeInfo.MaxLength = 200;
				colvarSummarizeInfo.AutoIncrement = false;
				colvarSummarizeInfo.IsNullable = true;
				colvarSummarizeInfo.IsPrimaryKey = false;
				colvarSummarizeInfo.IsForeignKey = false;
				colvarSummarizeInfo.IsReadOnly = false;
				colvarSummarizeInfo.DefaultSetting = @"";
				colvarSummarizeInfo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSummarizeInfo);
				
				TableSchema.TableColumn colvarDiagInfo = new TableSchema.TableColumn(schema);
				colvarDiagInfo.ColumnName = "Diag_Info";
				colvarDiagInfo.DataType = DbType.String;
				colvarDiagInfo.MaxLength = 4000;
				colvarDiagInfo.AutoIncrement = false;
				colvarDiagInfo.IsNullable = true;
				colvarDiagInfo.IsPrimaryKey = false;
				colvarDiagInfo.IsForeignKey = false;
				colvarDiagInfo.IsReadOnly = false;
				colvarDiagInfo.DefaultSetting = @"";
				colvarDiagInfo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDiagInfo);
				
				TableSchema.TableColumn colvarTreatInfo = new TableSchema.TableColumn(schema);
				colvarTreatInfo.ColumnName = "Treat_Info";
				colvarTreatInfo.DataType = DbType.String;
				colvarTreatInfo.MaxLength = 200;
				colvarTreatInfo.AutoIncrement = false;
				colvarTreatInfo.IsNullable = true;
				colvarTreatInfo.IsPrimaryKey = false;
				colvarTreatInfo.IsForeignKey = false;
				colvarTreatInfo.IsReadOnly = false;
				colvarTreatInfo.DefaultSetting = @"";
				colvarTreatInfo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTreatInfo);
				
				TableSchema.TableColumn colvarMainDiseaseId = new TableSchema.TableColumn(schema);
				colvarMainDiseaseId.ColumnName = "MainDisease_ID";
				colvarMainDiseaseId.DataType = DbType.String;
				colvarMainDiseaseId.MaxLength = 300;
				colvarMainDiseaseId.AutoIncrement = false;
				colvarMainDiseaseId.IsNullable = true;
				colvarMainDiseaseId.IsPrimaryKey = false;
				colvarMainDiseaseId.IsForeignKey = false;
				colvarMainDiseaseId.IsReadOnly = false;
				colvarMainDiseaseId.DefaultSetting = @"";
				colvarMainDiseaseId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMainDiseaseId);
				
				TableSchema.TableColumn colvarAuxiDiseaseId = new TableSchema.TableColumn(schema);
				colvarAuxiDiseaseId.ColumnName = "AuxiDisease_ID";
				colvarAuxiDiseaseId.DataType = DbType.String;
				colvarAuxiDiseaseId.MaxLength = 300;
				colvarAuxiDiseaseId.AutoIncrement = false;
				colvarAuxiDiseaseId.IsNullable = true;
				colvarAuxiDiseaseId.IsPrimaryKey = false;
				colvarAuxiDiseaseId.IsForeignKey = false;
				colvarAuxiDiseaseId.IsReadOnly = false;
				colvarAuxiDiseaseId.DefaultSetting = @"";
				colvarAuxiDiseaseId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAuxiDiseaseId);
				
				TableSchema.TableColumn colvarDifferInfo = new TableSchema.TableColumn(schema);
				colvarDifferInfo.ColumnName = "Differ_Info";
				colvarDifferInfo.DataType = DbType.String;
				colvarDifferInfo.MaxLength = 200;
				colvarDifferInfo.AutoIncrement = false;
				colvarDifferInfo.IsNullable = true;
				colvarDifferInfo.IsPrimaryKey = false;
				colvarDifferInfo.IsForeignKey = false;
				colvarDifferInfo.IsReadOnly = false;
				colvarDifferInfo.DefaultSetting = @"";
				colvarDifferInfo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDifferInfo);
				
				TableSchema.TableColumn colvarDoctorId = new TableSchema.TableColumn(schema);
				colvarDoctorId.ColumnName = "Doctor_ID";
				colvarDoctorId.DataType = DbType.Int16;
				colvarDoctorId.MaxLength = 0;
				colvarDoctorId.AutoIncrement = false;
				colvarDoctorId.IsNullable = false;
				colvarDoctorId.IsPrimaryKey = false;
				colvarDoctorId.IsForeignKey = false;
				colvarDoctorId.IsReadOnly = false;
				colvarDoctorId.DefaultSetting = @"";
				colvarDoctorId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDoctorId);
				
				TableSchema.TableColumn colvarDiagDate = new TableSchema.TableColumn(schema);
				colvarDiagDate.ColumnName = "Diag_Date";
				colvarDiagDate.DataType = DbType.DateTime;
				colvarDiagDate.MaxLength = 0;
				colvarDiagDate.AutoIncrement = false;
				colvarDiagDate.IsNullable = false;
				colvarDiagDate.IsPrimaryKey = false;
				colvarDiagDate.IsForeignKey = false;
				colvarDiagDate.IsReadOnly = false;
				
						colvarDiagDate.DefaultSetting = @"(getdate())";
				colvarDiagDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDiagDate);
				
				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "Created_By";
				colvarCreatedBy.DataType = DbType.AnsiString;
				colvarCreatedBy.MaxLength = 50;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = true;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"";
				colvarCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedBy);
				
				TableSchema.TableColumn colvarCreateDate = new TableSchema.TableColumn(schema);
				colvarCreateDate.ColumnName = "Create_Date";
				colvarCreateDate.DataType = DbType.DateTime;
				colvarCreateDate.MaxLength = 0;
				colvarCreateDate.AutoIncrement = false;
				colvarCreateDate.IsNullable = true;
				colvarCreateDate.IsPrimaryKey = false;
				colvarCreateDate.IsForeignKey = false;
				colvarCreateDate.IsReadOnly = false;
				colvarCreateDate.DefaultSetting = @"";
				colvarCreateDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreateDate);
				
				TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(schema);
				colvarModifiedBy.ColumnName = "ModifiedBy";
				colvarModifiedBy.DataType = DbType.AnsiString;
				colvarModifiedBy.MaxLength = 50;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = true;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"";
				colvarModifiedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedBy);
				
				TableSchema.TableColumn colvarModifiedDate = new TableSchema.TableColumn(schema);
				colvarModifiedDate.ColumnName = "ModifiedDate";
				colvarModifiedDate.DataType = DbType.DateTime;
				colvarModifiedDate.MaxLength = 0;
				colvarModifiedDate.AutoIncrement = false;
				colvarModifiedDate.IsNullable = true;
				colvarModifiedDate.IsPrimaryKey = false;
				colvarModifiedDate.IsForeignKey = false;
				colvarModifiedDate.IsReadOnly = false;
				colvarModifiedDate.DefaultSetting = @"";
				colvarModifiedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedDate);
				
				TableSchema.TableColumn colvarKeyCode = new TableSchema.TableColumn(schema);
				colvarKeyCode.ColumnName = "KeyCode";
				colvarKeyCode.DataType = DbType.String;
				colvarKeyCode.MaxLength = 20;
				colvarKeyCode.AutoIncrement = false;
				colvarKeyCode.IsNullable = true;
				colvarKeyCode.IsPrimaryKey = false;
				colvarKeyCode.IsForeignKey = false;
				colvarKeyCode.IsReadOnly = false;
				colvarKeyCode.DefaultSetting = @"";
				colvarKeyCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKeyCode);
				
				TableSchema.TableColumn colvarPhanBietId = new TableSchema.TableColumn(schema);
				colvarPhanBietId.ColumnName = "PhanBiet_ID";
				colvarPhanBietId.DataType = DbType.String;
				colvarPhanBietId.MaxLength = 300;
				colvarPhanBietId.AutoIncrement = false;
				colvarPhanBietId.IsNullable = true;
				colvarPhanBietId.IsPrimaryKey = false;
				colvarPhanBietId.IsForeignKey = false;
				colvarPhanBietId.IsReadOnly = false;
				colvarPhanBietId.DefaultSetting = @"";
				colvarPhanBietId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhanBietId);
				
				TableSchema.TableColumn colvarDepartmentId = new TableSchema.TableColumn(schema);
				colvarDepartmentId.ColumnName = "Department_ID";
				colvarDepartmentId.DataType = DbType.Int32;
				colvarDepartmentId.MaxLength = 0;
				colvarDepartmentId.AutoIncrement = false;
				colvarDepartmentId.IsNullable = true;
				colvarDepartmentId.IsPrimaryKey = false;
				colvarDepartmentId.IsForeignKey = false;
				colvarDepartmentId.IsReadOnly = false;
				colvarDepartmentId.DefaultSetting = @"";
				colvarDepartmentId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDepartmentId);
				
				TableSchema.TableColumn colvarDepartmentName = new TableSchema.TableColumn(schema);
				colvarDepartmentName.ColumnName = "Department_Name";
				colvarDepartmentName.DataType = DbType.String;
				colvarDepartmentName.MaxLength = 50;
				colvarDepartmentName.AutoIncrement = false;
				colvarDepartmentName.IsNullable = true;
				colvarDepartmentName.IsPrimaryKey = false;
				colvarDepartmentName.IsForeignKey = false;
				colvarDepartmentName.IsReadOnly = false;
				colvarDepartmentName.DefaultSetting = @"";
				colvarDepartmentName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDepartmentName);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("T_Diag_Info",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("DiagId")]
		[Bindable(true)]
		public long DiagId 
		{
			get { return GetColumnValue<long>(Columns.DiagId); }
			set { SetColumnValue(Columns.DiagId, value); }
		}
		  
		[XmlAttribute("ExamId")]
		[Bindable(true)]
		public long ExamId 
		{
			get { return GetColumnValue<long>(Columns.ExamId); }
			set { SetColumnValue(Columns.ExamId, value); }
		}
		  
		[XmlAttribute("PatientDeptId")]
		[Bindable(true)]
		public int? PatientDeptId 
		{
			get { return GetColumnValue<int?>(Columns.PatientDeptId); }
			set { SetColumnValue(Columns.PatientDeptId, value); }
		}
		  
		[XmlAttribute("PatientId")]
		[Bindable(true)]
		public long PatientId 
		{
			get { return GetColumnValue<long>(Columns.PatientId); }
			set { SetColumnValue(Columns.PatientId, value); }
		}
		  
		[XmlAttribute("PatientCode")]
		[Bindable(true)]
		public string PatientCode 
		{
			get { return GetColumnValue<string>(Columns.PatientCode); }
			set { SetColumnValue(Columns.PatientCode, value); }
		}
		  
		[XmlAttribute("SummarizeInfo")]
		[Bindable(true)]
		public string SummarizeInfo 
		{
			get { return GetColumnValue<string>(Columns.SummarizeInfo); }
			set { SetColumnValue(Columns.SummarizeInfo, value); }
		}
		  
		[XmlAttribute("DiagInfo")]
		[Bindable(true)]
		public string DiagInfo 
		{
			get { return GetColumnValue<string>(Columns.DiagInfo); }
			set { SetColumnValue(Columns.DiagInfo, value); }
		}
		  
		[XmlAttribute("TreatInfo")]
		[Bindable(true)]
		public string TreatInfo 
		{
			get { return GetColumnValue<string>(Columns.TreatInfo); }
			set { SetColumnValue(Columns.TreatInfo, value); }
		}
		  
		[XmlAttribute("MainDiseaseId")]
		[Bindable(true)]
		public string MainDiseaseId 
		{
			get { return GetColumnValue<string>(Columns.MainDiseaseId); }
			set { SetColumnValue(Columns.MainDiseaseId, value); }
		}
		  
		[XmlAttribute("AuxiDiseaseId")]
		[Bindable(true)]
		public string AuxiDiseaseId 
		{
			get { return GetColumnValue<string>(Columns.AuxiDiseaseId); }
			set { SetColumnValue(Columns.AuxiDiseaseId, value); }
		}
		  
		[XmlAttribute("DifferInfo")]
		[Bindable(true)]
		public string DifferInfo 
		{
			get { return GetColumnValue<string>(Columns.DifferInfo); }
			set { SetColumnValue(Columns.DifferInfo, value); }
		}
		  
		[XmlAttribute("DoctorId")]
		[Bindable(true)]
		public short DoctorId 
		{
			get { return GetColumnValue<short>(Columns.DoctorId); }
			set { SetColumnValue(Columns.DoctorId, value); }
		}
		  
		[XmlAttribute("DiagDate")]
		[Bindable(true)]
		public DateTime DiagDate 
		{
			get { return GetColumnValue<DateTime>(Columns.DiagDate); }
			set { SetColumnValue(Columns.DiagDate, value); }
		}
		  
		[XmlAttribute("CreatedBy")]
		[Bindable(true)]
		public string CreatedBy 
		{
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set { SetColumnValue(Columns.CreatedBy, value); }
		}
		  
		[XmlAttribute("CreateDate")]
		[Bindable(true)]
		public DateTime? CreateDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.CreateDate); }
			set { SetColumnValue(Columns.CreateDate, value); }
		}
		  
		[XmlAttribute("ModifiedBy")]
		[Bindable(true)]
		public string ModifiedBy 
		{
			get { return GetColumnValue<string>(Columns.ModifiedBy); }
			set { SetColumnValue(Columns.ModifiedBy, value); }
		}
		  
		[XmlAttribute("ModifiedDate")]
		[Bindable(true)]
		public DateTime? ModifiedDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.ModifiedDate); }
			set { SetColumnValue(Columns.ModifiedDate, value); }
		}
		  
		[XmlAttribute("KeyCode")]
		[Bindable(true)]
		public string KeyCode 
		{
			get { return GetColumnValue<string>(Columns.KeyCode); }
			set { SetColumnValue(Columns.KeyCode, value); }
		}
		  
		[XmlAttribute("PhanBietId")]
		[Bindable(true)]
		public string PhanBietId 
		{
			get { return GetColumnValue<string>(Columns.PhanBietId); }
			set { SetColumnValue(Columns.PhanBietId, value); }
		}
		  
		[XmlAttribute("DepartmentId")]
		[Bindable(true)]
		public int? DepartmentId 
		{
			get { return GetColumnValue<int?>(Columns.DepartmentId); }
			set { SetColumnValue(Columns.DepartmentId, value); }
		}
		  
		[XmlAttribute("DepartmentName")]
		[Bindable(true)]
		public string DepartmentName 
		{
			get { return GetColumnValue<string>(Columns.DepartmentName); }
			set { SetColumnValue(Columns.DepartmentName, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varExamId,int? varPatientDeptId,long varPatientId,string varPatientCode,string varSummarizeInfo,string varDiagInfo,string varTreatInfo,string varMainDiseaseId,string varAuxiDiseaseId,string varDifferInfo,short varDoctorId,DateTime varDiagDate,string varCreatedBy,DateTime? varCreateDate,string varModifiedBy,DateTime? varModifiedDate,string varKeyCode,string varPhanBietId,int? varDepartmentId,string varDepartmentName)
		{
			TDiagInfo item = new TDiagInfo();
			
			item.ExamId = varExamId;
			
			item.PatientDeptId = varPatientDeptId;
			
			item.PatientId = varPatientId;
			
			item.PatientCode = varPatientCode;
			
			item.SummarizeInfo = varSummarizeInfo;
			
			item.DiagInfo = varDiagInfo;
			
			item.TreatInfo = varTreatInfo;
			
			item.MainDiseaseId = varMainDiseaseId;
			
			item.AuxiDiseaseId = varAuxiDiseaseId;
			
			item.DifferInfo = varDifferInfo;
			
			item.DoctorId = varDoctorId;
			
			item.DiagDate = varDiagDate;
			
			item.CreatedBy = varCreatedBy;
			
			item.CreateDate = varCreateDate;
			
			item.ModifiedBy = varModifiedBy;
			
			item.ModifiedDate = varModifiedDate;
			
			item.KeyCode = varKeyCode;
			
			item.PhanBietId = varPhanBietId;
			
			item.DepartmentId = varDepartmentId;
			
			item.DepartmentName = varDepartmentName;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varDiagId,long varExamId,int? varPatientDeptId,long varPatientId,string varPatientCode,string varSummarizeInfo,string varDiagInfo,string varTreatInfo,string varMainDiseaseId,string varAuxiDiseaseId,string varDifferInfo,short varDoctorId,DateTime varDiagDate,string varCreatedBy,DateTime? varCreateDate,string varModifiedBy,DateTime? varModifiedDate,string varKeyCode,string varPhanBietId,int? varDepartmentId,string varDepartmentName)
		{
			TDiagInfo item = new TDiagInfo();
			
				item.DiagId = varDiagId;
			
				item.ExamId = varExamId;
			
				item.PatientDeptId = varPatientDeptId;
			
				item.PatientId = varPatientId;
			
				item.PatientCode = varPatientCode;
			
				item.SummarizeInfo = varSummarizeInfo;
			
				item.DiagInfo = varDiagInfo;
			
				item.TreatInfo = varTreatInfo;
			
				item.MainDiseaseId = varMainDiseaseId;
			
				item.AuxiDiseaseId = varAuxiDiseaseId;
			
				item.DifferInfo = varDifferInfo;
			
				item.DoctorId = varDoctorId;
			
				item.DiagDate = varDiagDate;
			
				item.CreatedBy = varCreatedBy;
			
				item.CreateDate = varCreateDate;
			
				item.ModifiedBy = varModifiedBy;
			
				item.ModifiedDate = varModifiedDate;
			
				item.KeyCode = varKeyCode;
			
				item.PhanBietId = varPhanBietId;
			
				item.DepartmentId = varDepartmentId;
			
				item.DepartmentName = varDepartmentName;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn DiagIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn ExamIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn PatientDeptIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn PatientIdColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn PatientCodeColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn SummarizeInfoColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn DiagInfoColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn TreatInfoColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn MainDiseaseIdColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn AuxiDiseaseIdColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn DifferInfoColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn DoctorIdColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn DiagDateColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedByColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateDateColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedByColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedDateColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn KeyCodeColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn PhanBietIdColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn DepartmentIdColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn DepartmentNameColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string DiagId = @"Diag_ID";
			 public static string ExamId = @"Exam_ID";
			 public static string PatientDeptId = @"PatientDept_ID";
			 public static string PatientId = @"Patient_ID";
			 public static string PatientCode = @"Patient_Code";
			 public static string SummarizeInfo = @"Summarize_Info";
			 public static string DiagInfo = @"Diag_Info";
			 public static string TreatInfo = @"Treat_Info";
			 public static string MainDiseaseId = @"MainDisease_ID";
			 public static string AuxiDiseaseId = @"AuxiDisease_ID";
			 public static string DifferInfo = @"Differ_Info";
			 public static string DoctorId = @"Doctor_ID";
			 public static string DiagDate = @"Diag_Date";
			 public static string CreatedBy = @"Created_By";
			 public static string CreateDate = @"Create_Date";
			 public static string ModifiedBy = @"ModifiedBy";
			 public static string ModifiedDate = @"ModifiedDate";
			 public static string KeyCode = @"KeyCode";
			 public static string PhanBietId = @"PhanBiet_ID";
			 public static string DepartmentId = @"Department_ID";
			 public static string DepartmentName = @"Department_Name";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
