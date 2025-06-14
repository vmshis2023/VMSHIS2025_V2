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
	/// Strongly-typed collection for the TDeliverDrugDetail class.
	/// </summary>
    [Serializable]
	public partial class TDeliverDrugDetailCollection : ActiveList<TDeliverDrugDetail, TDeliverDrugDetailCollection>
	{	   
		public TDeliverDrugDetailCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TDeliverDrugDetailCollection</returns>
		public TDeliverDrugDetailCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TDeliverDrugDetail o = this[i];
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
	/// This is an ActiveRecord class which wraps the T_DeliverDrug_Detail table.
	/// </summary>
	[Serializable]
	public partial class TDeliverDrugDetail : ActiveRecord<TDeliverDrugDetail>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TDeliverDrugDetail()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TDeliverDrugDetail(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TDeliverDrugDetail(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TDeliverDrugDetail(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("T_DeliverDrug_Detail", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarDetailId = new TableSchema.TableColumn(schema);
				colvarDetailId.ColumnName = "Detail_ID";
				colvarDetailId.DataType = DbType.Int64;
				colvarDetailId.MaxLength = 0;
				colvarDetailId.AutoIncrement = true;
				colvarDetailId.IsNullable = false;
				colvarDetailId.IsPrimaryKey = true;
				colvarDetailId.IsForeignKey = false;
				colvarDetailId.IsReadOnly = false;
				colvarDetailId.DefaultSetting = @"";
				colvarDetailId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDetailId);
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "ID";
				colvarId.DataType = DbType.Int64;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = false;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = false;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarDrugId = new TableSchema.TableColumn(schema);
				colvarDrugId.ColumnName = "Drug_ID";
				colvarDrugId.DataType = DbType.Int32;
				colvarDrugId.MaxLength = 0;
				colvarDrugId.AutoIncrement = false;
				colvarDrugId.IsNullable = false;
				colvarDrugId.IsPrimaryKey = false;
				colvarDrugId.IsForeignKey = false;
				colvarDrugId.IsReadOnly = false;
				colvarDrugId.DefaultSetting = @"";
				colvarDrugId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDrugId);
				
				TableSchema.TableColumn colvarQuantity = new TableSchema.TableColumn(schema);
				colvarQuantity.ColumnName = "Quantity";
				colvarQuantity.DataType = DbType.Int32;
				colvarQuantity.MaxLength = 0;
				colvarQuantity.AutoIncrement = false;
				colvarQuantity.IsNullable = false;
				colvarQuantity.IsPrimaryKey = false;
				colvarQuantity.IsForeignKey = false;
				colvarQuantity.IsReadOnly = false;
				colvarQuantity.DefaultSetting = @"";
				colvarQuantity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarQuantity);
				
				TableSchema.TableColumn colvarHasDelivered = new TableSchema.TableColumn(schema);
				colvarHasDelivered.ColumnName = "hasDelivered";
				colvarHasDelivered.DataType = DbType.Byte;
				colvarHasDelivered.MaxLength = 0;
				colvarHasDelivered.AutoIncrement = false;
				colvarHasDelivered.IsNullable = false;
				colvarHasDelivered.IsPrimaryKey = false;
				colvarHasDelivered.IsForeignKey = false;
				colvarHasDelivered.IsReadOnly = false;
				
						colvarHasDelivered.DefaultSetting = @"((0))";
				colvarHasDelivered.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHasDelivered);
				
				TableSchema.TableColumn colvarStockId = new TableSchema.TableColumn(schema);
				colvarStockId.ColumnName = "stock_id";
				colvarStockId.DataType = DbType.Int16;
				colvarStockId.MaxLength = 0;
				colvarStockId.AutoIncrement = false;
				colvarStockId.IsNullable = false;
				colvarStockId.IsPrimaryKey = false;
				colvarStockId.IsForeignKey = false;
				colvarStockId.IsReadOnly = false;
				
						colvarStockId.DefaultSetting = @"((0))";
				colvarStockId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStockId);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("T_DeliverDrug_Detail",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("DetailId")]
		[Bindable(true)]
		public long DetailId 
		{
			get { return GetColumnValue<long>(Columns.DetailId); }
			set { SetColumnValue(Columns.DetailId, value); }
		}
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public long Id 
		{
			get { return GetColumnValue<long>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("DrugId")]
		[Bindable(true)]
		public int DrugId 
		{
			get { return GetColumnValue<int>(Columns.DrugId); }
			set { SetColumnValue(Columns.DrugId, value); }
		}
		  
		[XmlAttribute("Quantity")]
		[Bindable(true)]
		public int Quantity 
		{
			get { return GetColumnValue<int>(Columns.Quantity); }
			set { SetColumnValue(Columns.Quantity, value); }
		}
		  
		[XmlAttribute("HasDelivered")]
		[Bindable(true)]
		public byte HasDelivered 
		{
			get { return GetColumnValue<byte>(Columns.HasDelivered); }
			set { SetColumnValue(Columns.HasDelivered, value); }
		}
		  
		[XmlAttribute("StockId")]
		[Bindable(true)]
		public short StockId 
		{
			get { return GetColumnValue<short>(Columns.StockId); }
			set { SetColumnValue(Columns.StockId, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varId,int varDrugId,int varQuantity,byte varHasDelivered,short varStockId)
		{
			TDeliverDrugDetail item = new TDeliverDrugDetail();
			
			item.Id = varId;
			
			item.DrugId = varDrugId;
			
			item.Quantity = varQuantity;
			
			item.HasDelivered = varHasDelivered;
			
			item.StockId = varStockId;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varDetailId,long varId,int varDrugId,int varQuantity,byte varHasDelivered,short varStockId)
		{
			TDeliverDrugDetail item = new TDeliverDrugDetail();
			
				item.DetailId = varDetailId;
			
				item.Id = varId;
			
				item.DrugId = varDrugId;
			
				item.Quantity = varQuantity;
			
				item.HasDelivered = varHasDelivered;
			
				item.StockId = varStockId;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn DetailIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn DrugIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn QuantityColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn HasDeliveredColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn StockIdColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string DetailId = @"Detail_ID";
			 public static string Id = @"ID";
			 public static string DrugId = @"Drug_ID";
			 public static string Quantity = @"Quantity";
			 public static string HasDelivered = @"hasDelivered";
			 public static string StockId = @"stock_id";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
