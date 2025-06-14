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
	/// Strongly-typed collection for the LQuickSearch class.
	/// </summary>
    [Serializable]
	public partial class LQuickSearchCollection : ActiveList<LQuickSearch, LQuickSearchCollection>
	{	   
		public LQuickSearchCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LQuickSearchCollection</returns>
		public LQuickSearchCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LQuickSearch o = this[i];
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
	/// This is an ActiveRecord class which wraps the L_QuickSearch table.
	/// </summary>
	[Serializable]
	public partial class LQuickSearch : ActiveRecord<LQuickSearch>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LQuickSearch()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LQuickSearch(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LQuickSearch(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LQuickSearch(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("L_QuickSearch", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "ID";
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
				
				TableSchema.TableColumn colvarLoai = new TableSchema.TableColumn(schema);
				colvarLoai.ColumnName = "LOAI";
				colvarLoai.DataType = DbType.String;
				colvarLoai.MaxLength = 5;
				colvarLoai.AutoIncrement = false;
				colvarLoai.IsNullable = false;
				colvarLoai.IsPrimaryKey = false;
				colvarLoai.IsForeignKey = false;
				colvarLoai.IsReadOnly = false;
				colvarLoai.DefaultSetting = @"";
				colvarLoai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoai);
				
				TableSchema.TableColumn colvarShortform = new TableSchema.TableColumn(schema);
				colvarShortform.ColumnName = "SHORTFORM";
				colvarShortform.DataType = DbType.String;
				colvarShortform.MaxLength = 2000;
				colvarShortform.AutoIncrement = false;
				colvarShortform.IsNullable = false;
				colvarShortform.IsPrimaryKey = false;
				colvarShortform.IsForeignKey = false;
				colvarShortform.IsReadOnly = false;
				colvarShortform.DefaultSetting = @"";
				colvarShortform.ForeignKeyTableName = "";
				schema.Columns.Add(colvarShortform);
				
				TableSchema.TableColumn colvarFullform = new TableSchema.TableColumn(schema);
				colvarFullform.ColumnName = "FULLFORM";
				colvarFullform.DataType = DbType.String;
				colvarFullform.MaxLength = 2000;
				colvarFullform.AutoIncrement = false;
				colvarFullform.IsNullable = false;
				colvarFullform.IsPrimaryKey = false;
				colvarFullform.IsForeignKey = false;
				colvarFullform.IsReadOnly = false;
				colvarFullform.DefaultSetting = @"";
				colvarFullform.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFullform);
				
				TableSchema.TableColumn colvarCompareVal = new TableSchema.TableColumn(schema);
				colvarCompareVal.ColumnName = "COMPARE_VAL";
				colvarCompareVal.DataType = DbType.String;
				colvarCompareVal.MaxLength = 2000;
				colvarCompareVal.AutoIncrement = false;
				colvarCompareVal.IsNullable = false;
				colvarCompareVal.IsPrimaryKey = false;
				colvarCompareVal.IsForeignKey = false;
				colvarCompareVal.IsReadOnly = false;
				colvarCompareVal.DefaultSetting = @"";
				colvarCompareVal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCompareVal);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("L_QuickSearch",schema);
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
		  
		[XmlAttribute("Loai")]
		[Bindable(true)]
		public string Loai 
		{
			get { return GetColumnValue<string>(Columns.Loai); }
			set { SetColumnValue(Columns.Loai, value); }
		}
		  
		[XmlAttribute("Shortform")]
		[Bindable(true)]
		public string Shortform 
		{
			get { return GetColumnValue<string>(Columns.Shortform); }
			set { SetColumnValue(Columns.Shortform, value); }
		}
		  
		[XmlAttribute("Fullform")]
		[Bindable(true)]
		public string Fullform 
		{
			get { return GetColumnValue<string>(Columns.Fullform); }
			set { SetColumnValue(Columns.Fullform, value); }
		}
		  
		[XmlAttribute("CompareVal")]
		[Bindable(true)]
		public string CompareVal 
		{
			get { return GetColumnValue<string>(Columns.CompareVal); }
			set { SetColumnValue(Columns.CompareVal, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varLoai,string varShortform,string varFullform,string varCompareVal)
		{
			LQuickSearch item = new LQuickSearch();
			
			item.Loai = varLoai;
			
			item.Shortform = varShortform;
			
			item.Fullform = varFullform;
			
			item.CompareVal = varCompareVal;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varId,string varLoai,string varShortform,string varFullform,string varCompareVal)
		{
			LQuickSearch item = new LQuickSearch();
			
				item.Id = varId;
			
				item.Loai = varLoai;
			
				item.Shortform = varShortform;
			
				item.Fullform = varFullform;
			
				item.CompareVal = varCompareVal;
			
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
        
        
        
        public static TableSchema.TableColumn LoaiColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ShortformColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn FullformColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn CompareValColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string Loai = @"LOAI";
			 public static string Shortform = @"SHORTFORM";
			 public static string Fullform = @"FULLFORM";
			 public static string CompareVal = @"COMPARE_VAL";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
