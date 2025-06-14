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
	/// Strongly-typed collection for the LPreUse class.
	/// </summary>
    [Serializable]
	public partial class LPreUseCollection : ActiveList<LPreUse, LPreUseCollection>
	{	   
		public LPreUseCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LPreUseCollection</returns>
		public LPreUseCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LPreUse o = this[i];
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
	/// This is an ActiveRecord class which wraps the L_Pre_Uses table.
	/// </summary>
	[Serializable]
	public partial class LPreUse : ActiveRecord<LPreUse>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LPreUse()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LPreUse(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LPreUse(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LPreUse(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("L_Pre_Uses", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarUseId = new TableSchema.TableColumn(schema);
				colvarUseId.ColumnName = "Use_Id";
				colvarUseId.DataType = DbType.Int32;
				colvarUseId.MaxLength = 0;
				colvarUseId.AutoIncrement = true;
				colvarUseId.IsNullable = false;
				colvarUseId.IsPrimaryKey = true;
				colvarUseId.IsForeignKey = false;
				colvarUseId.IsReadOnly = false;
				colvarUseId.DefaultSetting = @"";
				colvarUseId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUseId);
				
				TableSchema.TableColumn colvarUseName = new TableSchema.TableColumn(schema);
				colvarUseName.ColumnName = "Use_Name";
				colvarUseName.DataType = DbType.String;
				colvarUseName.MaxLength = 50;
				colvarUseName.AutoIncrement = false;
				colvarUseName.IsNullable = true;
				colvarUseName.IsPrimaryKey = false;
				colvarUseName.IsForeignKey = false;
				colvarUseName.IsReadOnly = false;
				colvarUseName.DefaultSetting = @"";
				colvarUseName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUseName);
				
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
				
				TableSchema.TableColumn colvarSDesc = new TableSchema.TableColumn(schema);
				colvarSDesc.ColumnName = "sDesc";
				colvarSDesc.DataType = DbType.String;
				colvarSDesc.MaxLength = 10;
				colvarSDesc.AutoIncrement = false;
				colvarSDesc.IsNullable = true;
				colvarSDesc.IsPrimaryKey = false;
				colvarSDesc.IsForeignKey = false;
				colvarSDesc.IsReadOnly = false;
				colvarSDesc.DefaultSetting = @"";
				colvarSDesc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSDesc);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("L_Pre_Uses",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("UseId")]
		[Bindable(true)]
		public int UseId 
		{
			get { return GetColumnValue<int>(Columns.UseId); }
			set { SetColumnValue(Columns.UseId, value); }
		}
		  
		[XmlAttribute("UseName")]
		[Bindable(true)]
		public string UseName 
		{
			get { return GetColumnValue<string>(Columns.UseName); }
			set { SetColumnValue(Columns.UseName, value); }
		}
		  
		[XmlAttribute("IntOrder")]
		[Bindable(true)]
		public int? IntOrder 
		{
			get { return GetColumnValue<int?>(Columns.IntOrder); }
			set { SetColumnValue(Columns.IntOrder, value); }
		}
		  
		[XmlAttribute("SDesc")]
		[Bindable(true)]
		public string SDesc 
		{
			get { return GetColumnValue<string>(Columns.SDesc); }
			set { SetColumnValue(Columns.SDesc, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varUseName,int? varIntOrder,string varSDesc)
		{
			LPreUse item = new LPreUse();
			
			item.UseName = varUseName;
			
			item.IntOrder = varIntOrder;
			
			item.SDesc = varSDesc;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varUseId,string varUseName,int? varIntOrder,string varSDesc)
		{
			LPreUse item = new LPreUse();
			
				item.UseId = varUseId;
			
				item.UseName = varUseName;
			
				item.IntOrder = varIntOrder;
			
				item.SDesc = varSDesc;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn UseIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn UseNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IntOrderColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SDescColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string UseId = @"Use_Id";
			 public static string UseName = @"Use_Name";
			 public static string IntOrder = @"IntOrder";
			 public static string SDesc = @"sDesc";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
