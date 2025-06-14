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
namespace VietBaIT.HISLink.DataAccessLayer{
    /// <summary>
    /// Strongly-typed collection for the ViewLayChiDinhXetNghiem class.
    /// </summary>
    [Serializable]
    public partial class ViewLayChiDinhXetNghiemCollection : ReadOnlyList<ViewLayChiDinhXetNghiem, ViewLayChiDinhXetNghiemCollection>
    {        
        public ViewLayChiDinhXetNghiemCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the view_LayChiDinhXetNghiem view.
    /// </summary>
    [Serializable]
    public partial class ViewLayChiDinhXetNghiem : ReadOnlyRecord<ViewLayChiDinhXetNghiem>, IReadOnlyRecord
    {
    
	    #region Default Settings
	    protected static void SetSQLProps() 
	    {
		    GetTableSchema();
	    }
	    #endregion
        #region Schema Accessor
	    public static TableSchema.Table Schema
        {
            get
            {
                if (BaseSchema == null)
                {
                    SetSQLProps();
                }
                return BaseSchema;
            }
        }
    	
        private static void GetTableSchema() 
        {
            if(!IsSchemaInitialized)
            {
                //Schema declaration
                TableSchema.Table schema = new TableSchema.Table("view_LayChiDinhXetNghiem", TableType.View, DataService.GetInstance("ORM"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarAssignId = new TableSchema.TableColumn(schema);
                colvarAssignId.ColumnName = "Assign_ID";
                colvarAssignId.DataType = DbType.Int64;
                colvarAssignId.MaxLength = 0;
                colvarAssignId.AutoIncrement = false;
                colvarAssignId.IsNullable = false;
                colvarAssignId.IsPrimaryKey = false;
                colvarAssignId.IsForeignKey = false;
                colvarAssignId.IsReadOnly = false;
                
                schema.Columns.Add(colvarAssignId);
                
                TableSchema.TableColumn colvarAssignCode = new TableSchema.TableColumn(schema);
                colvarAssignCode.ColumnName = "Assign_Code";
                colvarAssignCode.DataType = DbType.AnsiString;
                colvarAssignCode.MaxLength = 50;
                colvarAssignCode.AutoIncrement = false;
                colvarAssignCode.IsNullable = true;
                colvarAssignCode.IsPrimaryKey = false;
                colvarAssignCode.IsForeignKey = false;
                colvarAssignCode.IsReadOnly = false;
                
                schema.Columns.Add(colvarAssignCode);
                
                TableSchema.TableColumn colvarBarcode = new TableSchema.TableColumn(schema);
                colvarBarcode.ColumnName = "Barcode";
                colvarBarcode.DataType = DbType.String;
                colvarBarcode.MaxLength = 20;
                colvarBarcode.AutoIncrement = false;
                colvarBarcode.IsNullable = true;
                colvarBarcode.IsPrimaryKey = false;
                colvarBarcode.IsForeignKey = false;
                colvarBarcode.IsReadOnly = false;
                
                schema.Columns.Add(colvarBarcode);
                
                TableSchema.TableColumn colvarMaChidinh = new TableSchema.TableColumn(schema);
                colvarMaChidinh.ColumnName = "MA_CHIDINH";
                colvarMaChidinh.DataType = DbType.AnsiString;
                colvarMaChidinh.MaxLength = 30;
                colvarMaChidinh.AutoIncrement = false;
                colvarMaChidinh.IsNullable = true;
                colvarMaChidinh.IsPrimaryKey = false;
                colvarMaChidinh.IsForeignKey = false;
                colvarMaChidinh.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaChidinh);
                
                TableSchema.TableColumn colvarMaKhoaThien = new TableSchema.TableColumn(schema);
                colvarMaKhoaThien.ColumnName = "MA_KHOA_THIEN";
                colvarMaKhoaThien.DataType = DbType.String;
                colvarMaKhoaThien.MaxLength = 10;
                colvarMaKhoaThien.AutoIncrement = false;
                colvarMaKhoaThien.IsNullable = true;
                colvarMaKhoaThien.IsPrimaryKey = false;
                colvarMaKhoaThien.IsForeignKey = false;
                colvarMaKhoaThien.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaKhoaThien);
                
                TableSchema.TableColumn colvarExamId = new TableSchema.TableColumn(schema);
                colvarExamId.ColumnName = "Exam_ID";
                colvarExamId.DataType = DbType.Int64;
                colvarExamId.MaxLength = 0;
                colvarExamId.AutoIncrement = false;
                colvarExamId.IsNullable = true;
                colvarExamId.IsPrimaryKey = false;
                colvarExamId.IsForeignKey = false;
                colvarExamId.IsReadOnly = false;
                
                schema.Columns.Add(colvarExamId);
                
                TableSchema.TableColumn colvarDepartmentId = new TableSchema.TableColumn(schema);
                colvarDepartmentId.ColumnName = "Department_ID";
                colvarDepartmentId.DataType = DbType.Int16;
                colvarDepartmentId.MaxLength = 0;
                colvarDepartmentId.AutoIncrement = false;
                colvarDepartmentId.IsNullable = true;
                colvarDepartmentId.IsPrimaryKey = false;
                colvarDepartmentId.IsForeignKey = false;
                colvarDepartmentId.IsReadOnly = false;
                
                schema.Columns.Add(colvarDepartmentId);
                
                TableSchema.TableColumn colvarAssignDetailId = new TableSchema.TableColumn(schema);
                colvarAssignDetailId.ColumnName = "AssignDetail_ID";
                colvarAssignDetailId.DataType = DbType.Int64;
                colvarAssignDetailId.MaxLength = 0;
                colvarAssignDetailId.AutoIncrement = false;
                colvarAssignDetailId.IsNullable = false;
                colvarAssignDetailId.IsPrimaryKey = false;
                colvarAssignDetailId.IsForeignKey = false;
                colvarAssignDetailId.IsReadOnly = false;
                
                schema.Columns.Add(colvarAssignDetailId);
                
                TableSchema.TableColumn colvarServiceId = new TableSchema.TableColumn(schema);
                colvarServiceId.ColumnName = "Service_ID";
                colvarServiceId.DataType = DbType.Int16;
                colvarServiceId.MaxLength = 0;
                colvarServiceId.AutoIncrement = false;
                colvarServiceId.IsNullable = true;
                colvarServiceId.IsPrimaryKey = false;
                colvarServiceId.IsForeignKey = false;
                colvarServiceId.IsReadOnly = false;
                
                schema.Columns.Add(colvarServiceId);
                
                TableSchema.TableColumn colvarServiceDetailId = new TableSchema.TableColumn(schema);
                colvarServiceDetailId.ColumnName = "ServiceDetail_ID";
                colvarServiceDetailId.DataType = DbType.Int32;
                colvarServiceDetailId.MaxLength = 0;
                colvarServiceDetailId.AutoIncrement = false;
                colvarServiceDetailId.IsNullable = false;
                colvarServiceDetailId.IsPrimaryKey = false;
                colvarServiceDetailId.IsForeignKey = false;
                colvarServiceDetailId.IsReadOnly = false;
                
                schema.Columns.Add(colvarServiceDetailId);
                
                TableSchema.TableColumn colvarNoiTru = new TableSchema.TableColumn(schema);
                colvarNoiTru.ColumnName = "Noi_Tru";
                colvarNoiTru.DataType = DbType.Byte;
                colvarNoiTru.MaxLength = 0;
                colvarNoiTru.AutoIncrement = false;
                colvarNoiTru.IsNullable = true;
                colvarNoiTru.IsPrimaryKey = false;
                colvarNoiTru.IsForeignKey = false;
                colvarNoiTru.IsReadOnly = false;
                
                schema.Columns.Add(colvarNoiTru);
                
                TableSchema.TableColumn colvarDiagPerson = new TableSchema.TableColumn(schema);
                colvarDiagPerson.ColumnName = "Diag_Person";
                colvarDiagPerson.DataType = DbType.Int16;
                colvarDiagPerson.MaxLength = 0;
                colvarDiagPerson.AutoIncrement = false;
                colvarDiagPerson.IsNullable = true;
                colvarDiagPerson.IsPrimaryKey = false;
                colvarDiagPerson.IsForeignKey = false;
                colvarDiagPerson.IsReadOnly = false;
                
                schema.Columns.Add(colvarDiagPerson);
                
                TableSchema.TableColumn colvarPaymentStatus = new TableSchema.TableColumn(schema);
                colvarPaymentStatus.ColumnName = "Payment_Status";
                colvarPaymentStatus.DataType = DbType.Byte;
                colvarPaymentStatus.MaxLength = 0;
                colvarPaymentStatus.AutoIncrement = false;
                colvarPaymentStatus.IsNullable = true;
                colvarPaymentStatus.IsPrimaryKey = false;
                colvarPaymentStatus.IsForeignKey = false;
                colvarPaymentStatus.IsReadOnly = false;
                
                schema.Columns.Add(colvarPaymentStatus);
                
                TableSchema.TableColumn colvarAssignDetailStatus = new TableSchema.TableColumn(schema);
                colvarAssignDetailStatus.ColumnName = "AssignDetail_Status";
                colvarAssignDetailStatus.DataType = DbType.Byte;
                colvarAssignDetailStatus.MaxLength = 0;
                colvarAssignDetailStatus.AutoIncrement = false;
                colvarAssignDetailStatus.IsNullable = false;
                colvarAssignDetailStatus.IsPrimaryKey = false;
                colvarAssignDetailStatus.IsForeignKey = false;
                colvarAssignDetailStatus.IsReadOnly = false;
                
                schema.Columns.Add(colvarAssignDetailStatus);
                
                TableSchema.TableColumn colvarInputDate = new TableSchema.TableColumn(schema);
                colvarInputDate.ColumnName = "Input_Date";
                colvarInputDate.DataType = DbType.DateTime;
                colvarInputDate.MaxLength = 0;
                colvarInputDate.AutoIncrement = false;
                colvarInputDate.IsNullable = true;
                colvarInputDate.IsPrimaryKey = false;
                colvarInputDate.IsForeignKey = false;
                colvarInputDate.IsReadOnly = false;
                
                schema.Columns.Add(colvarInputDate);
                
                TableSchema.TableColumn colvarRegDate = new TableSchema.TableColumn(schema);
                colvarRegDate.ColumnName = "Reg_Date";
                colvarRegDate.DataType = DbType.AnsiString;
                colvarRegDate.MaxLength = 10;
                colvarRegDate.AutoIncrement = false;
                colvarRegDate.IsNullable = true;
                colvarRegDate.IsPrimaryKey = false;
                colvarRegDate.IsForeignKey = false;
                colvarRegDate.IsReadOnly = false;
                
                schema.Columns.Add(colvarRegDate);
                
                TableSchema.TableColumn colvarMaKetNoi = new TableSchema.TableColumn(schema);
                colvarMaKetNoi.ColumnName = "Ma_KetNoi";
                colvarMaKetNoi.DataType = DbType.AnsiString;
                colvarMaKetNoi.MaxLength = 50;
                colvarMaKetNoi.AutoIncrement = false;
                colvarMaKetNoi.IsNullable = true;
                colvarMaKetNoi.IsPrimaryKey = false;
                colvarMaKetNoi.IsForeignKey = false;
                colvarMaKetNoi.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaKetNoi);
                
                TableSchema.TableColumn colvarMaChiTietKetNoi = new TableSchema.TableColumn(schema);
                colvarMaChiTietKetNoi.ColumnName = "Ma_ChiTiet_KetNoi";
                colvarMaChiTietKetNoi.DataType = DbType.AnsiString;
                colvarMaChiTietKetNoi.MaxLength = 50;
                colvarMaChiTietKetNoi.AutoIncrement = false;
                colvarMaChiTietKetNoi.IsNullable = true;
                colvarMaChiTietKetNoi.IsPrimaryKey = false;
                colvarMaChiTietKetNoi.IsForeignKey = false;
                colvarMaChiTietKetNoi.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaChiTietKetNoi);
                
                TableSchema.TableColumn colvarServiceDetailName = new TableSchema.TableColumn(schema);
                colvarServiceDetailName.ColumnName = "ServiceDetail_Name";
                colvarServiceDetailName.DataType = DbType.String;
                colvarServiceDetailName.MaxLength = 300;
                colvarServiceDetailName.AutoIncrement = false;
                colvarServiceDetailName.IsNullable = true;
                colvarServiceDetailName.IsPrimaryKey = false;
                colvarServiceDetailName.IsForeignKey = false;
                colvarServiceDetailName.IsReadOnly = false;
                
                schema.Columns.Add(colvarServiceDetailName);
                
                TableSchema.TableColumn colvarServiceDetailCode = new TableSchema.TableColumn(schema);
                colvarServiceDetailCode.ColumnName = "ServiceDetail_Code";
                colvarServiceDetailCode.DataType = DbType.Int32;
                colvarServiceDetailCode.MaxLength = 0;
                colvarServiceDetailCode.AutoIncrement = false;
                colvarServiceDetailCode.IsNullable = true;
                colvarServiceDetailCode.IsPrimaryKey = false;
                colvarServiceDetailCode.IsForeignKey = false;
                colvarServiceDetailCode.IsReadOnly = false;
                
                schema.Columns.Add(colvarServiceDetailCode);
                
                TableSchema.TableColumn colvarServiceName = new TableSchema.TableColumn(schema);
                colvarServiceName.ColumnName = "SERVICE_NAME";
                colvarServiceName.DataType = DbType.String;
                colvarServiceName.MaxLength = 300;
                colvarServiceName.AutoIncrement = false;
                colvarServiceName.IsNullable = false;
                colvarServiceName.IsPrimaryKey = false;
                colvarServiceName.IsForeignKey = false;
                colvarServiceName.IsReadOnly = false;
                
                schema.Columns.Add(colvarServiceName);
                
                TableSchema.TableColumn colvarGroupIntOrder = new TableSchema.TableColumn(schema);
                colvarGroupIntOrder.ColumnName = "GroupIntOrder";
                colvarGroupIntOrder.DataType = DbType.Int16;
                colvarGroupIntOrder.MaxLength = 0;
                colvarGroupIntOrder.AutoIncrement = false;
                colvarGroupIntOrder.IsNullable = false;
                colvarGroupIntOrder.IsPrimaryKey = false;
                colvarGroupIntOrder.IsForeignKey = false;
                colvarGroupIntOrder.IsReadOnly = false;
                
                schema.Columns.Add(colvarGroupIntOrder);
                
                TableSchema.TableColumn colvarIntOrder = new TableSchema.TableColumn(schema);
                colvarIntOrder.ColumnName = "IntOrder";
                colvarIntOrder.DataType = DbType.Int32;
                colvarIntOrder.MaxLength = 0;
                colvarIntOrder.AutoIncrement = false;
                colvarIntOrder.IsNullable = true;
                colvarIntOrder.IsPrimaryKey = false;
                colvarIntOrder.IsForeignKey = false;
                colvarIntOrder.IsReadOnly = false;
                
                schema.Columns.Add(colvarIntOrder);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("view_LayChiDinhXetNghiem",schema);
            }
        }
        #endregion
        
        #region Query Accessor
	    public static Query CreateQuery()
	    {
		    return new Query(Schema);
	    }
	    #endregion
	    
	    #region .ctors
	    public ViewLayChiDinhXetNghiem()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public ViewLayChiDinhXetNghiem(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public ViewLayChiDinhXetNghiem(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public ViewLayChiDinhXetNghiem(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("AssignId")]
        [Bindable(true)]
        public long AssignId 
	    {
		    get
		    {
			    return GetColumnValue<long>("Assign_ID");
		    }
            set 
		    {
			    SetColumnValue("Assign_ID", value);
            }
        }
	      
        [XmlAttribute("AssignCode")]
        [Bindable(true)]
        public string AssignCode 
	    {
		    get
		    {
			    return GetColumnValue<string>("Assign_Code");
		    }
            set 
		    {
			    SetColumnValue("Assign_Code", value);
            }
        }
	      
        [XmlAttribute("Barcode")]
        [Bindable(true)]
        public string Barcode 
	    {
		    get
		    {
			    return GetColumnValue<string>("Barcode");
		    }
            set 
		    {
			    SetColumnValue("Barcode", value);
            }
        }
	      
        [XmlAttribute("MaChidinh")]
        [Bindable(true)]
        public string MaChidinh 
	    {
		    get
		    {
			    return GetColumnValue<string>("MA_CHIDINH");
		    }
            set 
		    {
			    SetColumnValue("MA_CHIDINH", value);
            }
        }
	      
        [XmlAttribute("MaKhoaThien")]
        [Bindable(true)]
        public string MaKhoaThien 
	    {
		    get
		    {
			    return GetColumnValue<string>("MA_KHOA_THIEN");
		    }
            set 
		    {
			    SetColumnValue("MA_KHOA_THIEN", value);
            }
        }
	      
        [XmlAttribute("ExamId")]
        [Bindable(true)]
        public long? ExamId 
	    {
		    get
		    {
			    return GetColumnValue<long?>("Exam_ID");
		    }
            set 
		    {
			    SetColumnValue("Exam_ID", value);
            }
        }
	      
        [XmlAttribute("DepartmentId")]
        [Bindable(true)]
        public short? DepartmentId 
	    {
		    get
		    {
			    return GetColumnValue<short?>("Department_ID");
		    }
            set 
		    {
			    SetColumnValue("Department_ID", value);
            }
        }
	      
        [XmlAttribute("AssignDetailId")]
        [Bindable(true)]
        public long AssignDetailId 
	    {
		    get
		    {
			    return GetColumnValue<long>("AssignDetail_ID");
		    }
            set 
		    {
			    SetColumnValue("AssignDetail_ID", value);
            }
        }
	      
        [XmlAttribute("ServiceId")]
        [Bindable(true)]
        public short? ServiceId 
	    {
		    get
		    {
			    return GetColumnValue<short?>("Service_ID");
		    }
            set 
		    {
			    SetColumnValue("Service_ID", value);
            }
        }
	      
        [XmlAttribute("ServiceDetailId")]
        [Bindable(true)]
        public int ServiceDetailId 
	    {
		    get
		    {
			    return GetColumnValue<int>("ServiceDetail_ID");
		    }
            set 
		    {
			    SetColumnValue("ServiceDetail_ID", value);
            }
        }
	      
        [XmlAttribute("NoiTru")]
        [Bindable(true)]
        public byte? NoiTru 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("Noi_Tru");
		    }
            set 
		    {
			    SetColumnValue("Noi_Tru", value);
            }
        }
	      
        [XmlAttribute("DiagPerson")]
        [Bindable(true)]
        public short? DiagPerson 
	    {
		    get
		    {
			    return GetColumnValue<short?>("Diag_Person");
		    }
            set 
		    {
			    SetColumnValue("Diag_Person", value);
            }
        }
	      
        [XmlAttribute("PaymentStatus")]
        [Bindable(true)]
        public byte? PaymentStatus 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("Payment_Status");
		    }
            set 
		    {
			    SetColumnValue("Payment_Status", value);
            }
        }
	      
        [XmlAttribute("AssignDetailStatus")]
        [Bindable(true)]
        public byte AssignDetailStatus 
	    {
		    get
		    {
			    return GetColumnValue<byte>("AssignDetail_Status");
		    }
            set 
		    {
			    SetColumnValue("AssignDetail_Status", value);
            }
        }
	      
        [XmlAttribute("InputDate")]
        [Bindable(true)]
        public DateTime? InputDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("Input_Date");
		    }
            set 
		    {
			    SetColumnValue("Input_Date", value);
            }
        }
	      
        [XmlAttribute("RegDate")]
        [Bindable(true)]
        public string RegDate 
	    {
		    get
		    {
			    return GetColumnValue<string>("Reg_Date");
		    }
            set 
		    {
			    SetColumnValue("Reg_Date", value);
            }
        }
	      
        [XmlAttribute("MaKetNoi")]
        [Bindable(true)]
        public string MaKetNoi 
	    {
		    get
		    {
			    return GetColumnValue<string>("Ma_KetNoi");
		    }
            set 
		    {
			    SetColumnValue("Ma_KetNoi", value);
            }
        }
	      
        [XmlAttribute("MaChiTietKetNoi")]
        [Bindable(true)]
        public string MaChiTietKetNoi 
	    {
		    get
		    {
			    return GetColumnValue<string>("Ma_ChiTiet_KetNoi");
		    }
            set 
		    {
			    SetColumnValue("Ma_ChiTiet_KetNoi", value);
            }
        }
	      
        [XmlAttribute("ServiceDetailName")]
        [Bindable(true)]
        public string ServiceDetailName 
	    {
		    get
		    {
			    return GetColumnValue<string>("ServiceDetail_Name");
		    }
            set 
		    {
			    SetColumnValue("ServiceDetail_Name", value);
            }
        }
	      
        [XmlAttribute("ServiceDetailCode")]
        [Bindable(true)]
        public int? ServiceDetailCode 
	    {
		    get
		    {
			    return GetColumnValue<int?>("ServiceDetail_Code");
		    }
            set 
		    {
			    SetColumnValue("ServiceDetail_Code", value);
            }
        }
	      
        [XmlAttribute("ServiceName")]
        [Bindable(true)]
        public string ServiceName 
	    {
		    get
		    {
			    return GetColumnValue<string>("SERVICE_NAME");
		    }
            set 
		    {
			    SetColumnValue("SERVICE_NAME", value);
            }
        }
	      
        [XmlAttribute("GroupIntOrder")]
        [Bindable(true)]
        public short GroupIntOrder 
	    {
		    get
		    {
			    return GetColumnValue<short>("GroupIntOrder");
		    }
            set 
		    {
			    SetColumnValue("GroupIntOrder", value);
            }
        }
	      
        [XmlAttribute("IntOrder")]
        [Bindable(true)]
        public int? IntOrder 
	    {
		    get
		    {
			    return GetColumnValue<int?>("IntOrder");
		    }
            set 
		    {
			    SetColumnValue("IntOrder", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string AssignId = @"Assign_ID";
            
            public static string AssignCode = @"Assign_Code";
            
            public static string Barcode = @"Barcode";
            
            public static string MaChidinh = @"MA_CHIDINH";
            
            public static string MaKhoaThien = @"MA_KHOA_THIEN";
            
            public static string ExamId = @"Exam_ID";
            
            public static string DepartmentId = @"Department_ID";
            
            public static string AssignDetailId = @"AssignDetail_ID";
            
            public static string ServiceId = @"Service_ID";
            
            public static string ServiceDetailId = @"ServiceDetail_ID";
            
            public static string NoiTru = @"Noi_Tru";
            
            public static string DiagPerson = @"Diag_Person";
            
            public static string PaymentStatus = @"Payment_Status";
            
            public static string AssignDetailStatus = @"AssignDetail_Status";
            
            public static string InputDate = @"Input_Date";
            
            public static string RegDate = @"Reg_Date";
            
            public static string MaKetNoi = @"Ma_KetNoi";
            
            public static string MaChiTietKetNoi = @"Ma_ChiTiet_KetNoi";
            
            public static string ServiceDetailName = @"ServiceDetail_Name";
            
            public static string ServiceDetailCode = @"ServiceDetail_Code";
            
            public static string ServiceName = @"SERVICE_NAME";
            
            public static string GroupIntOrder = @"GroupIntOrder";
            
            public static string IntOrder = @"IntOrder";
            
	    }
	    #endregion
	    
	    
	    #region IAbstractRecord Members
        public new CT GetColumnValue<CT>(string columnName) {
            return base.GetColumnValue<CT>(columnName);
        }
        public object GetColumnValue(string columnName) {
            return base.GetColumnValue<object>(columnName);
        }
        #endregion
	    
    }
}
