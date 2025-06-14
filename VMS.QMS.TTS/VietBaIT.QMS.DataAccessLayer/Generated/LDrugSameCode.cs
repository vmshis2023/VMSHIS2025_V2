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
	/// Strongly-typed collection for the LDrugSameCode class.
	/// </summary>
    [Serializable]
	public partial class LDrugSameCodeCollection : ActiveList<LDrugSameCode, LDrugSameCodeCollection>
	{	   
		public LDrugSameCodeCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LDrugSameCodeCollection</returns>
		public LDrugSameCodeCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LDrugSameCode o = this[i];
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
	/// This is an ActiveRecord class which wraps the L_Drug_SameCode table.
	/// </summary>
	[Serializable]
	public partial class LDrugSameCode : ActiveRecord<LDrugSameCode>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LDrugSameCode()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LDrugSameCode(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LDrugSameCode(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LDrugSameCode(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("L_Drug_SameCode", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarDrugId = new TableSchema.TableColumn(schema);
				colvarDrugId.ColumnName = "Drug_ID";
				colvarDrugId.DataType = DbType.Int32;
				colvarDrugId.MaxLength = 0;
				colvarDrugId.AutoIncrement = false;
				colvarDrugId.IsNullable = false;
				colvarDrugId.IsPrimaryKey = true;
				colvarDrugId.IsForeignKey = false;
				colvarDrugId.IsReadOnly = false;
				colvarDrugId.DefaultSetting = @"";
				colvarDrugId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDrugId);
				
				TableSchema.TableColumn colvarSameDrugId = new TableSchema.TableColumn(schema);
				colvarSameDrugId.ColumnName = "SameDrug_ID";
				colvarSameDrugId.DataType = DbType.Int32;
				colvarSameDrugId.MaxLength = 0;
				colvarSameDrugId.AutoIncrement = false;
				colvarSameDrugId.IsNullable = false;
				colvarSameDrugId.IsPrimaryKey = true;
				colvarSameDrugId.IsForeignKey = false;
				colvarSameDrugId.IsReadOnly = false;
				colvarSameDrugId.DefaultSetting = @"";
				colvarSameDrugId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSameDrugId);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("L_Drug_SameCode",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("DrugId")]
		[Bindable(true)]
		public int DrugId 
		{
			get { return GetColumnValue<int>(Columns.DrugId); }
			set { SetColumnValue(Columns.DrugId, value); }
		}
		  
		[XmlAttribute("SameDrugId")]
		[Bindable(true)]
		public int SameDrugId 
		{
			get { return GetColumnValue<int>(Columns.SameDrugId); }
			set { SetColumnValue(Columns.SameDrugId, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varDrugId,int varSameDrugId)
		{
			LDrugSameCode item = new LDrugSameCode();
			
			item.DrugId = varDrugId;
			
			item.SameDrugId = varSameDrugId;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varDrugId,int varSameDrugId)
		{
			LDrugSameCode item = new LDrugSameCode();
			
				item.DrugId = varDrugId;
			
				item.SameDrugId = varSameDrugId;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn DrugIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn SameDrugIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string DrugId = @"Drug_ID";
			 public static string SameDrugId = @"SameDrug_ID";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
