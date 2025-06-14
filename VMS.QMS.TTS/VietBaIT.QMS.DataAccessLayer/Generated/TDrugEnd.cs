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
	/// Strongly-typed collection for the TDrugEnd class.
	/// </summary>
    [Serializable]
	public partial class TDrugEndCollection : ActiveList<TDrugEnd, TDrugEndCollection>
	{	   
		public TDrugEndCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TDrugEndCollection</returns>
		public TDrugEndCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TDrugEnd o = this[i];
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
	/// This is an ActiveRecord class which wraps the T_DRUG_END table.
	/// </summary>
	[Serializable]
	public partial class TDrugEnd : ActiveRecord<TDrugEnd>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TDrugEnd()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TDrugEnd(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TDrugEnd(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TDrugEnd(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("T_DRUG_END", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarEndId = new TableSchema.TableColumn(schema);
				colvarEndId.ColumnName = "END_ID";
				colvarEndId.DataType = DbType.Int32;
				colvarEndId.MaxLength = 0;
				colvarEndId.AutoIncrement = true;
				colvarEndId.IsNullable = false;
				colvarEndId.IsPrimaryKey = true;
				colvarEndId.IsForeignKey = false;
				colvarEndId.IsReadOnly = false;
				colvarEndId.DefaultSetting = @"";
				colvarEndId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEndId);
				
				TableSchema.TableColumn colvarStockId = new TableSchema.TableColumn(schema);
				colvarStockId.ColumnName = "Stock_ID";
				colvarStockId.DataType = DbType.Int16;
				colvarStockId.MaxLength = 0;
				colvarStockId.AutoIncrement = false;
				colvarStockId.IsNullable = false;
				colvarStockId.IsPrimaryKey = false;
				colvarStockId.IsForeignKey = false;
				colvarStockId.IsReadOnly = false;
				colvarStockId.DefaultSetting = @"";
				colvarStockId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStockId);
				
				TableSchema.TableColumn colvarSignUserId = new TableSchema.TableColumn(schema);
				colvarSignUserId.ColumnName = "SignUser_ID";
				colvarSignUserId.DataType = DbType.Int16;
				colvarSignUserId.MaxLength = 0;
				colvarSignUserId.AutoIncrement = false;
				colvarSignUserId.IsNullable = false;
				colvarSignUserId.IsPrimaryKey = false;
				colvarSignUserId.IsForeignKey = false;
				colvarSignUserId.IsReadOnly = false;
				colvarSignUserId.DefaultSetting = @"";
				colvarSignUserId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSignUserId);
				
				TableSchema.TableColumn colvarInputDate = new TableSchema.TableColumn(schema);
				colvarInputDate.ColumnName = "Input_Date";
				colvarInputDate.DataType = DbType.DateTime;
				colvarInputDate.MaxLength = 0;
				colvarInputDate.AutoIncrement = false;
				colvarInputDate.IsNullable = false;
				colvarInputDate.IsPrimaryKey = false;
				colvarInputDate.IsForeignKey = false;
				colvarInputDate.IsReadOnly = false;
				colvarInputDate.DefaultSetting = @"";
				colvarInputDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInputDate);
				
				TableSchema.TableColumn colvarStatus = new TableSchema.TableColumn(schema);
				colvarStatus.ColumnName = "Status";
				colvarStatus.DataType = DbType.Byte;
				colvarStatus.MaxLength = 0;
				colvarStatus.AutoIncrement = false;
				colvarStatus.IsNullable = false;
				colvarStatus.IsPrimaryKey = false;
				colvarStatus.IsForeignKey = false;
				colvarStatus.IsReadOnly = false;
				colvarStatus.DefaultSetting = @"";
				colvarStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStatus);
				
				TableSchema.TableColumn colvarReasonId = new TableSchema.TableColumn(schema);
				colvarReasonId.ColumnName = "Reason_ID";
				colvarReasonId.DataType = DbType.Int16;
				colvarReasonId.MaxLength = 0;
				colvarReasonId.AutoIncrement = false;
				colvarReasonId.IsNullable = false;
				colvarReasonId.IsPrimaryKey = false;
				colvarReasonId.IsForeignKey = false;
				colvarReasonId.IsReadOnly = false;
				colvarReasonId.DefaultSetting = @"";
				colvarReasonId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReasonId);
				
				TableSchema.TableColumn colvarCreatedby = new TableSchema.TableColumn(schema);
				colvarCreatedby.ColumnName = "Createdby";
				colvarCreatedby.DataType = DbType.String;
				colvarCreatedby.MaxLength = 50;
				colvarCreatedby.AutoIncrement = false;
				colvarCreatedby.IsNullable = false;
				colvarCreatedby.IsPrimaryKey = false;
				colvarCreatedby.IsForeignKey = false;
				colvarCreatedby.IsReadOnly = false;
				colvarCreatedby.DefaultSetting = @"";
				colvarCreatedby.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedby);
				
				TableSchema.TableColumn colvarCreatedDate = new TableSchema.TableColumn(schema);
				colvarCreatedDate.ColumnName = "CreatedDate";
				colvarCreatedDate.DataType = DbType.DateTime;
				colvarCreatedDate.MaxLength = 0;
				colvarCreatedDate.AutoIncrement = false;
				colvarCreatedDate.IsNullable = false;
				colvarCreatedDate.IsPrimaryKey = false;
				colvarCreatedDate.IsForeignKey = false;
				colvarCreatedDate.IsReadOnly = false;
				colvarCreatedDate.DefaultSetting = @"";
				colvarCreatedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedDate);
				
				TableSchema.TableColumn colvarModifiedby = new TableSchema.TableColumn(schema);
				colvarModifiedby.ColumnName = "Modifiedby";
				colvarModifiedby.DataType = DbType.String;
				colvarModifiedby.MaxLength = 50;
				colvarModifiedby.AutoIncrement = false;
				colvarModifiedby.IsNullable = true;
				colvarModifiedby.IsPrimaryKey = false;
				colvarModifiedby.IsForeignKey = false;
				colvarModifiedby.IsReadOnly = false;
				colvarModifiedby.DefaultSetting = @"";
				colvarModifiedby.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedby);
				
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
				
				TableSchema.TableColumn colvarObjectTypeId = new TableSchema.TableColumn(schema);
				colvarObjectTypeId.ColumnName = "ObjectType_ID";
				colvarObjectTypeId.DataType = DbType.Int16;
				colvarObjectTypeId.MaxLength = 0;
				colvarObjectTypeId.AutoIncrement = false;
				colvarObjectTypeId.IsNullable = true;
				colvarObjectTypeId.IsPrimaryKey = false;
				colvarObjectTypeId.IsForeignKey = false;
				colvarObjectTypeId.IsReadOnly = false;
				colvarObjectTypeId.DefaultSetting = @"";
				colvarObjectTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarObjectTypeId);
				
				TableSchema.TableColumn colvarIdKhoa = new TableSchema.TableColumn(schema);
				colvarIdKhoa.ColumnName = "Id_Khoa";
				colvarIdKhoa.DataType = DbType.Int16;
				colvarIdKhoa.MaxLength = 0;
				colvarIdKhoa.AutoIncrement = false;
				colvarIdKhoa.IsNullable = true;
				colvarIdKhoa.IsPrimaryKey = false;
				colvarIdKhoa.IsForeignKey = false;
				colvarIdKhoa.IsReadOnly = false;
				colvarIdKhoa.DefaultSetting = @"";
				colvarIdKhoa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhoa);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("T_DRUG_END",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("EndId")]
		[Bindable(true)]
		public int EndId 
		{
			get { return GetColumnValue<int>(Columns.EndId); }
			set { SetColumnValue(Columns.EndId, value); }
		}
		  
		[XmlAttribute("StockId")]
		[Bindable(true)]
		public short StockId 
		{
			get { return GetColumnValue<short>(Columns.StockId); }
			set { SetColumnValue(Columns.StockId, value); }
		}
		  
		[XmlAttribute("SignUserId")]
		[Bindable(true)]
		public short SignUserId 
		{
			get { return GetColumnValue<short>(Columns.SignUserId); }
			set { SetColumnValue(Columns.SignUserId, value); }
		}
		  
		[XmlAttribute("InputDate")]
		[Bindable(true)]
		public DateTime InputDate 
		{
			get { return GetColumnValue<DateTime>(Columns.InputDate); }
			set { SetColumnValue(Columns.InputDate, value); }
		}
		  
		[XmlAttribute("Status")]
		[Bindable(true)]
		public byte Status 
		{
			get { return GetColumnValue<byte>(Columns.Status); }
			set { SetColumnValue(Columns.Status, value); }
		}
		  
		[XmlAttribute("ReasonId")]
		[Bindable(true)]
		public short ReasonId 
		{
			get { return GetColumnValue<short>(Columns.ReasonId); }
			set { SetColumnValue(Columns.ReasonId, value); }
		}
		  
		[XmlAttribute("Createdby")]
		[Bindable(true)]
		public string Createdby 
		{
			get { return GetColumnValue<string>(Columns.Createdby); }
			set { SetColumnValue(Columns.Createdby, value); }
		}
		  
		[XmlAttribute("CreatedDate")]
		[Bindable(true)]
		public DateTime CreatedDate 
		{
			get { return GetColumnValue<DateTime>(Columns.CreatedDate); }
			set { SetColumnValue(Columns.CreatedDate, value); }
		}
		  
		[XmlAttribute("Modifiedby")]
		[Bindable(true)]
		public string Modifiedby 
		{
			get { return GetColumnValue<string>(Columns.Modifiedby); }
			set { SetColumnValue(Columns.Modifiedby, value); }
		}
		  
		[XmlAttribute("ModifiedDate")]
		[Bindable(true)]
		public DateTime? ModifiedDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.ModifiedDate); }
			set { SetColumnValue(Columns.ModifiedDate, value); }
		}
		  
		[XmlAttribute("ObjectTypeId")]
		[Bindable(true)]
		public short? ObjectTypeId 
		{
			get { return GetColumnValue<short?>(Columns.ObjectTypeId); }
			set { SetColumnValue(Columns.ObjectTypeId, value); }
		}
		  
		[XmlAttribute("IdKhoa")]
		[Bindable(true)]
		public short? IdKhoa 
		{
			get { return GetColumnValue<short?>(Columns.IdKhoa); }
			set { SetColumnValue(Columns.IdKhoa, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(short varStockId,short varSignUserId,DateTime varInputDate,byte varStatus,short varReasonId,string varCreatedby,DateTime varCreatedDate,string varModifiedby,DateTime? varModifiedDate,short? varObjectTypeId,short? varIdKhoa)
		{
			TDrugEnd item = new TDrugEnd();
			
			item.StockId = varStockId;
			
			item.SignUserId = varSignUserId;
			
			item.InputDate = varInputDate;
			
			item.Status = varStatus;
			
			item.ReasonId = varReasonId;
			
			item.Createdby = varCreatedby;
			
			item.CreatedDate = varCreatedDate;
			
			item.Modifiedby = varModifiedby;
			
			item.ModifiedDate = varModifiedDate;
			
			item.ObjectTypeId = varObjectTypeId;
			
			item.IdKhoa = varIdKhoa;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varEndId,short varStockId,short varSignUserId,DateTime varInputDate,byte varStatus,short varReasonId,string varCreatedby,DateTime varCreatedDate,string varModifiedby,DateTime? varModifiedDate,short? varObjectTypeId,short? varIdKhoa)
		{
			TDrugEnd item = new TDrugEnd();
			
				item.EndId = varEndId;
			
				item.StockId = varStockId;
			
				item.SignUserId = varSignUserId;
			
				item.InputDate = varInputDate;
			
				item.Status = varStatus;
			
				item.ReasonId = varReasonId;
			
				item.Createdby = varCreatedby;
			
				item.CreatedDate = varCreatedDate;
			
				item.Modifiedby = varModifiedby;
			
				item.ModifiedDate = varModifiedDate;
			
				item.ObjectTypeId = varObjectTypeId;
			
				item.IdKhoa = varIdKhoa;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn EndIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn StockIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn SignUserIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn InputDateColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn StatusColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn ReasonIdColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedbyColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedDateColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedbyColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedDateColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn ObjectTypeIdColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhoaColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string EndId = @"END_ID";
			 public static string StockId = @"Stock_ID";
			 public static string SignUserId = @"SignUser_ID";
			 public static string InputDate = @"Input_Date";
			 public static string Status = @"Status";
			 public static string ReasonId = @"Reason_ID";
			 public static string Createdby = @"Createdby";
			 public static string CreatedDate = @"CreatedDate";
			 public static string Modifiedby = @"Modifiedby";
			 public static string ModifiedDate = @"ModifiedDate";
			 public static string ObjectTypeId = @"ObjectType_ID";
			 public static string IdKhoa = @"Id_Khoa";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
