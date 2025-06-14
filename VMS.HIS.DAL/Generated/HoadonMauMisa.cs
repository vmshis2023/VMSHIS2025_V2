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
	/// Strongly-typed collection for the HoadonMauMisa class.
	/// </summary>
    [Serializable]
	public partial class HoadonMauMisaCollection : ActiveList<HoadonMauMisa, HoadonMauMisaCollection>
	{	   
		public HoadonMauMisaCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>HoadonMauMisaCollection</returns>
		public HoadonMauMisaCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                HoadonMauMisa o = this[i];
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
	/// This is an ActiveRecord class which wraps the hoadon_mau_misa table.
	/// </summary>
	[Serializable]
	public partial class HoadonMauMisa : ActiveRecord<HoadonMauMisa>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public HoadonMauMisa()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public HoadonMauMisa(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public HoadonMauMisa(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public HoadonMauMisa(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("hoadon_mau_misa", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "id";
				colvarId.DataType = DbType.Int32;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarIPTemplateID = new TableSchema.TableColumn(schema);
				colvarIPTemplateID.ColumnName = "IPTemplateID";
				colvarIPTemplateID.DataType = DbType.String;
				colvarIPTemplateID.MaxLength = 255;
				colvarIPTemplateID.AutoIncrement = false;
				colvarIPTemplateID.IsNullable = true;
				colvarIPTemplateID.IsPrimaryKey = false;
				colvarIPTemplateID.IsForeignKey = false;
				colvarIPTemplateID.IsReadOnly = false;
				colvarIPTemplateID.DefaultSetting = @"";
				colvarIPTemplateID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIPTemplateID);
				
				TableSchema.TableColumn colvarTemplateName = new TableSchema.TableColumn(schema);
				colvarTemplateName.ColumnName = "TemplateName";
				colvarTemplateName.DataType = DbType.String;
				colvarTemplateName.MaxLength = 255;
				colvarTemplateName.AutoIncrement = false;
				colvarTemplateName.IsNullable = true;
				colvarTemplateName.IsPrimaryKey = false;
				colvarTemplateName.IsForeignKey = false;
				colvarTemplateName.IsReadOnly = false;
				colvarTemplateName.DefaultSetting = @"";
				colvarTemplateName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTemplateName);
				
				TableSchema.TableColumn colvarInvSeries = new TableSchema.TableColumn(schema);
				colvarInvSeries.ColumnName = "InvSeries";
				colvarInvSeries.DataType = DbType.String;
				colvarInvSeries.MaxLength = 255;
				colvarInvSeries.AutoIncrement = false;
				colvarInvSeries.IsNullable = true;
				colvarInvSeries.IsPrimaryKey = false;
				colvarInvSeries.IsForeignKey = false;
				colvarInvSeries.IsReadOnly = false;
				colvarInvSeries.DefaultSetting = @"";
				colvarInvSeries.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInvSeries);
				
				TableSchema.TableColumn colvarIsActive = new TableSchema.TableColumn(schema);
				colvarIsActive.ColumnName = "isActive";
				colvarIsActive.DataType = DbType.Boolean;
				colvarIsActive.MaxLength = 0;
				colvarIsActive.AutoIncrement = false;
				colvarIsActive.IsNullable = true;
				colvarIsActive.IsPrimaryKey = false;
				colvarIsActive.IsForeignKey = false;
				colvarIsActive.IsReadOnly = false;
				colvarIsActive.DefaultSetting = @"";
				colvarIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActive);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("hoadon_mau_misa",schema);
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
		  
		[XmlAttribute("IPTemplateID")]
		[Bindable(true)]
		public string IPTemplateID 
		{
			get { return GetColumnValue<string>(Columns.IPTemplateID); }
			set { SetColumnValue(Columns.IPTemplateID, value); }
		}
		  
		[XmlAttribute("TemplateName")]
		[Bindable(true)]
		public string TemplateName 
		{
			get { return GetColumnValue<string>(Columns.TemplateName); }
			set { SetColumnValue(Columns.TemplateName, value); }
		}
		  
		[XmlAttribute("InvSeries")]
		[Bindable(true)]
		public string InvSeries 
		{
			get { return GetColumnValue<string>(Columns.InvSeries); }
			set { SetColumnValue(Columns.InvSeries, value); }
		}
		  
		[XmlAttribute("IsActive")]
		[Bindable(true)]
		public bool? IsActive 
		{
			get { return GetColumnValue<bool?>(Columns.IsActive); }
			set { SetColumnValue(Columns.IsActive, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varIPTemplateID,string varTemplateName,string varInvSeries,bool? varIsActive)
		{
			HoadonMauMisa item = new HoadonMauMisa();
			
			item.IPTemplateID = varIPTemplateID;
			
			item.TemplateName = varTemplateName;
			
			item.InvSeries = varInvSeries;
			
			item.IsActive = varIsActive;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varIPTemplateID,string varTemplateName,string varInvSeries,bool? varIsActive)
		{
			HoadonMauMisa item = new HoadonMauMisa();
			
				item.Id = varId;
			
				item.IPTemplateID = varIPTemplateID;
			
				item.TemplateName = varTemplateName;
			
				item.InvSeries = varInvSeries;
			
				item.IsActive = varIsActive;
			
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
        
        
        
        public static TableSchema.TableColumn IPTemplateIDColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TemplateNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn InvSeriesColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn IsActiveColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"id";
			 public static string IPTemplateID = @"IPTemplateID";
			 public static string TemplateName = @"TemplateName";
			 public static string InvSeries = @"InvSeries";
			 public static string IsActive = @"isActive";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
