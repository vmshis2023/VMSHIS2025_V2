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
	/// Strongly-typed collection for the QheThanhtoanTamthu class.
	/// </summary>
    [Serializable]
	public partial class QheThanhtoanTamthuCollection : ActiveList<QheThanhtoanTamthu, QheThanhtoanTamthuCollection>
	{	   
		public QheThanhtoanTamthuCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>QheThanhtoanTamthuCollection</returns>
		public QheThanhtoanTamthuCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                QheThanhtoanTamthu o = this[i];
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
	/// This is an ActiveRecord class which wraps the qhe_thanhtoan_tamthu table.
	/// </summary>
	[Serializable]
	public partial class QheThanhtoanTamthu : ActiveRecord<QheThanhtoanTamthu>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public QheThanhtoanTamthu()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public QheThanhtoanTamthu(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public QheThanhtoanTamthu(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public QheThanhtoanTamthu(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("qhe_thanhtoan_tamthu", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "id";
				colvarId.DataType = DbType.Int64;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarIdThanhtoan = new TableSchema.TableColumn(schema);
				colvarIdThanhtoan.ColumnName = "id_thanhtoan";
				colvarIdThanhtoan.DataType = DbType.Int64;
				colvarIdThanhtoan.MaxLength = 0;
				colvarIdThanhtoan.AutoIncrement = false;
				colvarIdThanhtoan.IsNullable = false;
				colvarIdThanhtoan.IsPrimaryKey = false;
				colvarIdThanhtoan.IsForeignKey = false;
				colvarIdThanhtoan.IsReadOnly = false;
				colvarIdThanhtoan.DefaultSetting = @"";
				colvarIdThanhtoan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThanhtoan);
				
				TableSchema.TableColumn colvarIdTamthu = new TableSchema.TableColumn(schema);
				colvarIdTamthu.ColumnName = "id_tamthu";
				colvarIdTamthu.DataType = DbType.Int64;
				colvarIdTamthu.MaxLength = 0;
				colvarIdTamthu.AutoIncrement = false;
				colvarIdTamthu.IsNullable = false;
				colvarIdTamthu.IsPrimaryKey = false;
				colvarIdTamthu.IsForeignKey = false;
				colvarIdTamthu.IsReadOnly = false;
				colvarIdTamthu.DefaultSetting = @"";
				colvarIdTamthu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdTamthu);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("qhe_thanhtoan_tamthu",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public long Id 
		{
			get { return GetColumnValue<long>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("IdThanhtoan")]
		[Bindable(true)]
		public long IdThanhtoan 
		{
			get { return GetColumnValue<long>(Columns.IdThanhtoan); }
			set { SetColumnValue(Columns.IdThanhtoan, value); }
		}
		  
		[XmlAttribute("IdTamthu")]
		[Bindable(true)]
		public long IdTamthu 
		{
			get { return GetColumnValue<long>(Columns.IdTamthu); }
			set { SetColumnValue(Columns.IdTamthu, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varIdThanhtoan,long varIdTamthu)
		{
			QheThanhtoanTamthu item = new QheThanhtoanTamthu();
			
			item.IdThanhtoan = varIdThanhtoan;
			
			item.IdTamthu = varIdTamthu;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varId,long varIdThanhtoan,long varIdTamthu)
		{
			QheThanhtoanTamthu item = new QheThanhtoanTamthu();
			
				item.Id = varId;
			
				item.IdThanhtoan = varIdThanhtoan;
			
				item.IdTamthu = varIdTamthu;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThanhtoanColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IdTamthuColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"id";
			 public static string IdThanhtoan = @"id_thanhtoan";
			 public static string IdTamthu = @"id_tamthu";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
