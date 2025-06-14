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
    /// Strongly-typed collection for the ViewTAssignDetailTemp class.
    /// </summary>
    [Serializable]
    public partial class ViewTAssignDetailTempCollection : ReadOnlyList<ViewTAssignDetailTemp, ViewTAssignDetailTempCollection>
    {        
        public ViewTAssignDetailTempCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the view_T_Assign_Detail_temp view.
    /// </summary>
    [Serializable]
    public partial class ViewTAssignDetailTemp : ReadOnlyRecord<ViewTAssignDetailTemp>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("view_T_Assign_Detail_temp", TableType.View, DataService.GetInstance("ORM"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
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
                
                TableSchema.TableColumn colvarDiscountPrice = new TableSchema.TableColumn(schema);
                colvarDiscountPrice.ColumnName = "Discount_Price";
                colvarDiscountPrice.DataType = DbType.Currency;
                colvarDiscountPrice.MaxLength = 0;
                colvarDiscountPrice.AutoIncrement = false;
                colvarDiscountPrice.IsNullable = true;
                colvarDiscountPrice.IsPrimaryKey = false;
                colvarDiscountPrice.IsForeignKey = false;
                colvarDiscountPrice.IsReadOnly = false;
                
                schema.Columns.Add(colvarDiscountPrice);
                
                TableSchema.TableColumn colvarSurchargePrice = new TableSchema.TableColumn(schema);
                colvarSurchargePrice.ColumnName = "Surcharge_Price";
                colvarSurchargePrice.DataType = DbType.Currency;
                colvarSurchargePrice.MaxLength = 0;
                colvarSurchargePrice.AutoIncrement = false;
                colvarSurchargePrice.IsNullable = true;
                colvarSurchargePrice.IsPrimaryKey = false;
                colvarSurchargePrice.IsForeignKey = false;
                colvarSurchargePrice.IsReadOnly = false;
                
                schema.Columns.Add(colvarSurchargePrice);
                
                TableSchema.TableColumn colvarUserId = new TableSchema.TableColumn(schema);
                colvarUserId.ColumnName = "User_ID";
                colvarUserId.DataType = DbType.AnsiString;
                colvarUserId.MaxLength = 20;
                colvarUserId.AutoIncrement = false;
                colvarUserId.IsNullable = true;
                colvarUserId.IsPrimaryKey = false;
                colvarUserId.IsForeignKey = false;
                colvarUserId.IsReadOnly = false;
                
                schema.Columns.Add(colvarUserId);
                
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
                
                TableSchema.TableColumn colvarIsPayment = new TableSchema.TableColumn(schema);
                colvarIsPayment.ColumnName = "IsPayment";
                colvarIsPayment.DataType = DbType.Byte;
                colvarIsPayment.MaxLength = 0;
                colvarIsPayment.AutoIncrement = false;
                colvarIsPayment.IsNullable = true;
                colvarIsPayment.IsPrimaryKey = false;
                colvarIsPayment.IsForeignKey = false;
                colvarIsPayment.IsReadOnly = false;
                
                schema.Columns.Add(colvarIsPayment);
                
                TableSchema.TableColumn colvarQuantity = new TableSchema.TableColumn(schema);
                colvarQuantity.ColumnName = "Quantity";
                colvarQuantity.DataType = DbType.Int32;
                colvarQuantity.MaxLength = 0;
                colvarQuantity.AutoIncrement = false;
                colvarQuantity.IsNullable = true;
                colvarQuantity.IsPrimaryKey = false;
                colvarQuantity.IsForeignKey = false;
                colvarQuantity.IsReadOnly = false;
                
                schema.Columns.Add(colvarQuantity);
                
                TableSchema.TableColumn colvarGhiChu = new TableSchema.TableColumn(schema);
                colvarGhiChu.ColumnName = "Ghi_Chu";
                colvarGhiChu.DataType = DbType.String;
                colvarGhiChu.MaxLength = 255;
                colvarGhiChu.AutoIncrement = false;
                colvarGhiChu.IsNullable = true;
                colvarGhiChu.IsPrimaryKey = false;
                colvarGhiChu.IsForeignKey = false;
                colvarGhiChu.IsReadOnly = false;
                
                schema.Columns.Add(colvarGhiChu);
                
                TableSchema.TableColumn colvarGiaBhytCt = new TableSchema.TableColumn(schema);
                colvarGiaBhytCt.ColumnName = "Gia_BHYT_CT";
                colvarGiaBhytCt.DataType = DbType.Decimal;
                colvarGiaBhytCt.MaxLength = 0;
                colvarGiaBhytCt.AutoIncrement = false;
                colvarGiaBhytCt.IsNullable = true;
                colvarGiaBhytCt.IsPrimaryKey = false;
                colvarGiaBhytCt.IsForeignKey = false;
                colvarGiaBhytCt.IsReadOnly = false;
                
                schema.Columns.Add(colvarGiaBhytCt);
                
                TableSchema.TableColumn colvarGiaBnct = new TableSchema.TableColumn(schema);
                colvarGiaBnct.ColumnName = "Gia_BNCT";
                colvarGiaBnct.DataType = DbType.Decimal;
                colvarGiaBnct.MaxLength = 0;
                colvarGiaBnct.AutoIncrement = false;
                colvarGiaBnct.IsNullable = true;
                colvarGiaBnct.IsPrimaryKey = false;
                colvarGiaBnct.IsForeignKey = false;
                colvarGiaBnct.IsReadOnly = false;
                
                schema.Columns.Add(colvarGiaBnct);
                
                TableSchema.TableColumn colvarChoPhepIn = new TableSchema.TableColumn(schema);
                colvarChoPhepIn.ColumnName = "CHO_PHEP_IN";
                colvarChoPhepIn.DataType = DbType.Byte;
                colvarChoPhepIn.MaxLength = 0;
                colvarChoPhepIn.AutoIncrement = false;
                colvarChoPhepIn.IsNullable = true;
                colvarChoPhepIn.IsPrimaryKey = false;
                colvarChoPhepIn.IsForeignKey = false;
                colvarChoPhepIn.IsReadOnly = false;
                
                schema.Columns.Add(colvarChoPhepIn);
                
                TableSchema.TableColumn colvarStt = new TableSchema.TableColumn(schema);
                colvarStt.ColumnName = "STT";
                colvarStt.DataType = DbType.Int16;
                colvarStt.MaxLength = 0;
                colvarStt.AutoIncrement = false;
                colvarStt.IsNullable = true;
                colvarStt.IsPrimaryKey = false;
                colvarStt.IsForeignKey = false;
                colvarStt.IsReadOnly = false;
                
                schema.Columns.Add(colvarStt);
                
                TableSchema.TableColumn colvarIdNoiThien = new TableSchema.TableColumn(schema);
                colvarIdNoiThien.ColumnName = "ID_NOI_THIEN";
                colvarIdNoiThien.DataType = DbType.Int16;
                colvarIdNoiThien.MaxLength = 0;
                colvarIdNoiThien.AutoIncrement = false;
                colvarIdNoiThien.IsNullable = true;
                colvarIdNoiThien.IsPrimaryKey = false;
                colvarIdNoiThien.IsForeignKey = false;
                colvarIdNoiThien.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdNoiThien);
                
                TableSchema.TableColumn colvarTenLoaiDvu = new TableSchema.TableColumn(schema);
                colvarTenLoaiDvu.ColumnName = "TEN_LOAI_DVU";
                colvarTenLoaiDvu.DataType = DbType.String;
                colvarTenLoaiDvu.MaxLength = 300;
                colvarTenLoaiDvu.AutoIncrement = false;
                colvarTenLoaiDvu.IsNullable = true;
                colvarTenLoaiDvu.IsPrimaryKey = false;
                colvarTenLoaiDvu.IsForeignKey = false;
                colvarTenLoaiDvu.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenLoaiDvu);
                
                TableSchema.TableColumn colvarServiceCode = new TableSchema.TableColumn(schema);
                colvarServiceCode.ColumnName = "Service_Code";
                colvarServiceCode.DataType = DbType.AnsiString;
                colvarServiceCode.MaxLength = 20;
                colvarServiceCode.AutoIncrement = false;
                colvarServiceCode.IsNullable = true;
                colvarServiceCode.IsPrimaryKey = false;
                colvarServiceCode.IsForeignKey = false;
                colvarServiceCode.IsReadOnly = false;
                
                schema.Columns.Add(colvarServiceCode);
                
                TableSchema.TableColumn colvarIntOrderGroup = new TableSchema.TableColumn(schema);
                colvarIntOrderGroup.ColumnName = "IntOrderGroup";
                colvarIntOrderGroup.DataType = DbType.Int16;
                colvarIntOrderGroup.MaxLength = 0;
                colvarIntOrderGroup.AutoIncrement = false;
                colvarIntOrderGroup.IsNullable = true;
                colvarIntOrderGroup.IsPrimaryKey = false;
                colvarIntOrderGroup.IsForeignKey = false;
                colvarIntOrderGroup.IsReadOnly = false;
                
                schema.Columns.Add(colvarIntOrderGroup);
                
                TableSchema.TableColumn colvarTenDvu = new TableSchema.TableColumn(schema);
                colvarTenDvu.ColumnName = "TEN_DVU";
                colvarTenDvu.DataType = DbType.String;
                colvarTenDvu.MaxLength = 300;
                colvarTenDvu.AutoIncrement = false;
                colvarTenDvu.IsNullable = true;
                colvarTenDvu.IsPrimaryKey = false;
                colvarTenDvu.IsForeignKey = false;
                colvarTenDvu.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenDvu);
                
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
                
                TableSchema.TableColumn colvarNoiThucHien = new TableSchema.TableColumn(schema);
                colvarNoiThucHien.ColumnName = "NOI_THUC_HIEN";
                colvarNoiThucHien.DataType = DbType.String;
                colvarNoiThucHien.MaxLength = 100;
                colvarNoiThucHien.AutoIncrement = false;
                colvarNoiThucHien.IsNullable = true;
                colvarNoiThucHien.IsPrimaryKey = false;
                colvarNoiThucHien.IsForeignKey = false;
                colvarNoiThucHien.IsReadOnly = false;
                
                schema.Columns.Add(colvarNoiThucHien);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("view_T_Assign_Detail_temp",schema);
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
	    public ViewTAssignDetailTemp()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public ViewTAssignDetailTemp(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public ViewTAssignDetailTemp(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public ViewTAssignDetailTemp(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
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
	      
        [XmlAttribute("DiscountPrice")]
        [Bindable(true)]
        public decimal? DiscountPrice 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("Discount_Price");
		    }
            set 
		    {
			    SetColumnValue("Discount_Price", value);
            }
        }
	      
        [XmlAttribute("SurchargePrice")]
        [Bindable(true)]
        public decimal? SurchargePrice 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("Surcharge_Price");
		    }
            set 
		    {
			    SetColumnValue("Surcharge_Price", value);
            }
        }
	      
        [XmlAttribute("UserId")]
        [Bindable(true)]
        public string UserId 
	    {
		    get
		    {
			    return GetColumnValue<string>("User_ID");
		    }
            set 
		    {
			    SetColumnValue("User_ID", value);
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
	      
        [XmlAttribute("IsPayment")]
        [Bindable(true)]
        public byte? IsPayment 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("IsPayment");
		    }
            set 
		    {
			    SetColumnValue("IsPayment", value);
            }
        }
	      
        [XmlAttribute("Quantity")]
        [Bindable(true)]
        public int? Quantity 
	    {
		    get
		    {
			    return GetColumnValue<int?>("Quantity");
		    }
            set 
		    {
			    SetColumnValue("Quantity", value);
            }
        }
	      
        [XmlAttribute("GhiChu")]
        [Bindable(true)]
        public string GhiChu 
	    {
		    get
		    {
			    return GetColumnValue<string>("Ghi_Chu");
		    }
            set 
		    {
			    SetColumnValue("Ghi_Chu", value);
            }
        }
	      
        [XmlAttribute("GiaBhytCt")]
        [Bindable(true)]
        public decimal? GiaBhytCt 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("Gia_BHYT_CT");
		    }
            set 
		    {
			    SetColumnValue("Gia_BHYT_CT", value);
            }
        }
	      
        [XmlAttribute("GiaBnct")]
        [Bindable(true)]
        public decimal? GiaBnct 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("Gia_BNCT");
		    }
            set 
		    {
			    SetColumnValue("Gia_BNCT", value);
            }
        }
	      
        [XmlAttribute("ChoPhepIn")]
        [Bindable(true)]
        public byte? ChoPhepIn 
	    {
		    get
		    {
			    return GetColumnValue<byte?>("CHO_PHEP_IN");
		    }
            set 
		    {
			    SetColumnValue("CHO_PHEP_IN", value);
            }
        }
	      
        [XmlAttribute("Stt")]
        [Bindable(true)]
        public short? Stt 
	    {
		    get
		    {
			    return GetColumnValue<short?>("STT");
		    }
            set 
		    {
			    SetColumnValue("STT", value);
            }
        }
	      
        [XmlAttribute("IdNoiThien")]
        [Bindable(true)]
        public short? IdNoiThien 
	    {
		    get
		    {
			    return GetColumnValue<short?>("ID_NOI_THIEN");
		    }
            set 
		    {
			    SetColumnValue("ID_NOI_THIEN", value);
            }
        }
	      
        [XmlAttribute("TenLoaiDvu")]
        [Bindable(true)]
        public string TenLoaiDvu 
	    {
		    get
		    {
			    return GetColumnValue<string>("TEN_LOAI_DVU");
		    }
            set 
		    {
			    SetColumnValue("TEN_LOAI_DVU", value);
            }
        }
	      
        [XmlAttribute("ServiceCode")]
        [Bindable(true)]
        public string ServiceCode 
	    {
		    get
		    {
			    return GetColumnValue<string>("Service_Code");
		    }
            set 
		    {
			    SetColumnValue("Service_Code", value);
            }
        }
	      
        [XmlAttribute("IntOrderGroup")]
        [Bindable(true)]
        public short? IntOrderGroup 
	    {
		    get
		    {
			    return GetColumnValue<short?>("IntOrderGroup");
		    }
            set 
		    {
			    SetColumnValue("IntOrderGroup", value);
            }
        }
	      
        [XmlAttribute("TenDvu")]
        [Bindable(true)]
        public string TenDvu 
	    {
		    get
		    {
			    return GetColumnValue<string>("TEN_DVU");
		    }
            set 
		    {
			    SetColumnValue("TEN_DVU", value);
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
	      
        [XmlAttribute("NoiThucHien")]
        [Bindable(true)]
        public string NoiThucHien 
	    {
		    get
		    {
			    return GetColumnValue<string>("NOI_THUC_HIEN");
		    }
            set 
		    {
			    SetColumnValue("NOI_THUC_HIEN", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string AssignDetailId = @"AssignDetail_ID";
            
            public static string ExamId = @"Exam_ID";
            
            public static string AssignId = @"Assign_ID";
            
            public static string ServiceId = @"Service_ID";
            
            public static string ServiceDetailId = @"ServiceDetail_ID";
            
            public static string DiagPerson = @"Diag_Person";
            
            public static string DiscountPrice = @"Discount_Price";
            
            public static string SurchargePrice = @"Surcharge_Price";
            
            public static string UserId = @"User_ID";
            
            public static string InputDate = @"Input_Date";
            
            public static string IsPayment = @"IsPayment";
            
            public static string Quantity = @"Quantity";
            
            public static string GhiChu = @"Ghi_Chu";
            
            public static string GiaBhytCt = @"Gia_BHYT_CT";
            
            public static string GiaBnct = @"Gia_BNCT";
            
            public static string ChoPhepIn = @"CHO_PHEP_IN";
            
            public static string Stt = @"STT";
            
            public static string IdNoiThien = @"ID_NOI_THIEN";
            
            public static string TenLoaiDvu = @"TEN_LOAI_DVU";
            
            public static string ServiceCode = @"Service_Code";
            
            public static string IntOrderGroup = @"IntOrderGroup";
            
            public static string TenDvu = @"TEN_DVU";
            
            public static string IntOrder = @"IntOrder";
            
            public static string NoiThucHien = @"NOI_THUC_HIEN";
            
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
