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
	/// Strongly-typed collection for the DynamicFieldUserCustomSize class.
	/// </summary>
    [Serializable]
	public partial class DynamicFieldUserCustomSizeCollection : ActiveList<DynamicFieldUserCustomSize, DynamicFieldUserCustomSizeCollection>
	{	   
		public DynamicFieldUserCustomSizeCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DynamicFieldUserCustomSizeCollection</returns>
		public DynamicFieldUserCustomSizeCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DynamicFieldUserCustomSize o = this[i];
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
	/// This is an ActiveRecord class which wraps the DynamicFieldUserCustomSize table.
	/// </summary>
	[Serializable]
	public partial class DynamicFieldUserCustomSize : ActiveRecord<DynamicFieldUserCustomSize>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DynamicFieldUserCustomSize()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DynamicFieldUserCustomSize(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DynamicFieldUserCustomSize(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DynamicFieldUserCustomSize(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("DynamicFieldUserCustomSize", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "Id";
				colvarId.DataType = DbType.Int32;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = false;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarUid = new TableSchema.TableColumn(schema);
				colvarUid.ColumnName = "UID";
				colvarUid.DataType = DbType.String;
				colvarUid.MaxLength = 30;
				colvarUid.AutoIncrement = false;
				colvarUid.IsNullable = false;
				colvarUid.IsPrimaryKey = true;
				colvarUid.IsForeignKey = false;
				colvarUid.IsReadOnly = false;
				colvarUid.DefaultSetting = @"";
				colvarUid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUid);
				
				TableSchema.TableColumn colvarTxtW = new TableSchema.TableColumn(schema);
				colvarTxtW.ColumnName = "txtW";
				colvarTxtW.DataType = DbType.Int16;
				colvarTxtW.MaxLength = 0;
				colvarTxtW.AutoIncrement = false;
				colvarTxtW.IsNullable = true;
				colvarTxtW.IsPrimaryKey = false;
				colvarTxtW.IsForeignKey = false;
				colvarTxtW.IsReadOnly = false;
				colvarTxtW.DefaultSetting = @"";
				colvarTxtW.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTxtW);
				
				TableSchema.TableColumn colvarTxtH = new TableSchema.TableColumn(schema);
				colvarTxtH.ColumnName = "txtH";
				colvarTxtH.DataType = DbType.Int16;
				colvarTxtH.MaxLength = 0;
				colvarTxtH.AutoIncrement = false;
				colvarTxtH.IsNullable = true;
				colvarTxtH.IsPrimaryKey = false;
				colvarTxtH.IsForeignKey = false;
				colvarTxtH.IsReadOnly = false;
				colvarTxtH.DefaultSetting = @"";
				colvarTxtH.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTxtH);
				
				TableSchema.TableColumn colvarLblW = new TableSchema.TableColumn(schema);
				colvarLblW.ColumnName = "lblW";
				colvarLblW.DataType = DbType.Int16;
				colvarLblW.MaxLength = 0;
				colvarLblW.AutoIncrement = false;
				colvarLblW.IsNullable = true;
				colvarLblW.IsPrimaryKey = false;
				colvarLblW.IsForeignKey = false;
				colvarLblW.IsReadOnly = false;
				colvarLblW.DefaultSetting = @"";
				colvarLblW.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLblW);
				
				TableSchema.TableColumn colvarLblH = new TableSchema.TableColumn(schema);
				colvarLblH.ColumnName = "lblH";
				colvarLblH.DataType = DbType.Int16;
				colvarLblH.MaxLength = 0;
				colvarLblH.AutoIncrement = false;
				colvarLblH.IsNullable = true;
				colvarLblH.IsPrimaryKey = false;
				colvarLblH.IsForeignKey = false;
				colvarLblH.IsReadOnly = false;
				colvarLblH.DefaultSetting = @"";
				colvarLblH.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLblH);
				
				TableSchema.TableColumn colvarW = new TableSchema.TableColumn(schema);
				colvarW.ColumnName = "W";
				colvarW.DataType = DbType.Int16;
				colvarW.MaxLength = 0;
				colvarW.AutoIncrement = false;
				colvarW.IsNullable = true;
				colvarW.IsPrimaryKey = false;
				colvarW.IsForeignKey = false;
				colvarW.IsReadOnly = false;
				colvarW.DefaultSetting = @"";
				colvarW.ForeignKeyTableName = "";
				schema.Columns.Add(colvarW);
				
				TableSchema.TableColumn colvarH = new TableSchema.TableColumn(schema);
				colvarH.ColumnName = "H";
				colvarH.DataType = DbType.Int16;
				colvarH.MaxLength = 0;
				colvarH.AutoIncrement = false;
				colvarH.IsNullable = true;
				colvarH.IsPrimaryKey = false;
				colvarH.IsForeignKey = false;
				colvarH.IsReadOnly = false;
				colvarH.DefaultSetting = @"";
				colvarH.ForeignKeyTableName = "";
				schema.Columns.Add(colvarH);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("DynamicFieldUserCustomSize",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public int Id 
		{
			get { return GetColumnValue<int>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("Uid")]
		[Bindable(true)]
		public string Uid 
		{
			get { return GetColumnValue<string>(Columns.Uid); }
			set { SetColumnValue(Columns.Uid, value); }
		}
		  
		[XmlAttribute("TxtW")]
		[Bindable(true)]
		public short? TxtW 
		{
			get { return GetColumnValue<short?>(Columns.TxtW); }
			set { SetColumnValue(Columns.TxtW, value); }
		}
		  
		[XmlAttribute("TxtH")]
		[Bindable(true)]
		public short? TxtH 
		{
			get { return GetColumnValue<short?>(Columns.TxtH); }
			set { SetColumnValue(Columns.TxtH, value); }
		}
		  
		[XmlAttribute("LblW")]
		[Bindable(true)]
		public short? LblW 
		{
			get { return GetColumnValue<short?>(Columns.LblW); }
			set { SetColumnValue(Columns.LblW, value); }
		}
		  
		[XmlAttribute("LblH")]
		[Bindable(true)]
		public short? LblH 
		{
			get { return GetColumnValue<short?>(Columns.LblH); }
			set { SetColumnValue(Columns.LblH, value); }
		}
		  
		[XmlAttribute("W")]
		[Bindable(true)]
		public short? W 
		{
			get { return GetColumnValue<short?>(Columns.W); }
			set { SetColumnValue(Columns.W, value); }
		}
		  
		[XmlAttribute("H")]
		[Bindable(true)]
		public short? H 
		{
			get { return GetColumnValue<short?>(Columns.H); }
			set { SetColumnValue(Columns.H, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varId,string varUid,short? varTxtW,short? varTxtH,short? varLblW,short? varLblH,short? varW,short? varH)
		{
			DynamicFieldUserCustomSize item = new DynamicFieldUserCustomSize();
			
			item.Id = varId;
			
			item.Uid = varUid;
			
			item.TxtW = varTxtW;
			
			item.TxtH = varTxtH;
			
			item.LblW = varLblW;
			
			item.LblH = varLblH;
			
			item.W = varW;
			
			item.H = varH;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varUid,short? varTxtW,short? varTxtH,short? varLblW,short? varLblH,short? varW,short? varH)
		{
			DynamicFieldUserCustomSize item = new DynamicFieldUserCustomSize();
			
				item.Id = varId;
			
				item.Uid = varUid;
			
				item.TxtW = varTxtW;
			
				item.TxtH = varTxtH;
			
				item.LblW = varLblW;
			
				item.LblH = varLblH;
			
				item.W = varW;
			
				item.H = varH;
			
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
        
        
        
        public static TableSchema.TableColumn UidColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TxtWColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn TxtHColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn LblWColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn LblHColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn WColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn HColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string Uid = @"UID";
			 public static string TxtW = @"txtW";
			 public static string TxtH = @"txtH";
			 public static string LblW = @"lblW";
			 public static string LblH = @"lblH";
			 public static string W = @"W";
			 public static string H = @"H";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
