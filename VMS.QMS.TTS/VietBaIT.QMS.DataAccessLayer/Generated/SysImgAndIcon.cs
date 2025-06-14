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
	/// Strongly-typed collection for the SysImgAndIcon class.
	/// </summary>
    [Serializable]
	public partial class SysImgAndIconCollection : ActiveList<SysImgAndIcon, SysImgAndIconCollection>
	{	   
		public SysImgAndIconCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysImgAndIconCollection</returns>
		public SysImgAndIconCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysImgAndIcon o = this[i];
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
	/// This is an ActiveRecord class which wraps the Sys_ImgAndIcon table.
	/// </summary>
	[Serializable]
	public partial class SysImgAndIcon : ActiveRecord<SysImgAndIcon>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public SysImgAndIcon()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysImgAndIcon(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysImgAndIcon(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysImgAndIcon(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("Sys_ImgAndIcon", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "ID";
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
				
				TableSchema.TableColumn colvarSFileName = new TableSchema.TableColumn(schema);
				colvarSFileName.ColumnName = "sFileName";
				colvarSFileName.DataType = DbType.String;
				colvarSFileName.MaxLength = 50;
				colvarSFileName.AutoIncrement = false;
				colvarSFileName.IsNullable = true;
				colvarSFileName.IsPrimaryKey = false;
				colvarSFileName.IsForeignKey = false;
				colvarSFileName.IsReadOnly = false;
				colvarSFileName.DefaultSetting = @"";
				colvarSFileName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSFileName);
				
				TableSchema.TableColumn colvarSFilePath = new TableSchema.TableColumn(schema);
				colvarSFilePath.ColumnName = "sFilePath";
				colvarSFilePath.DataType = DbType.String;
				colvarSFilePath.MaxLength = 250;
				colvarSFilePath.AutoIncrement = false;
				colvarSFilePath.IsNullable = true;
				colvarSFilePath.IsPrimaryKey = false;
				colvarSFilePath.IsForeignKey = false;
				colvarSFilePath.IsReadOnly = false;
				colvarSFilePath.DefaultSetting = @"";
				colvarSFilePath.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSFilePath);
				
				TableSchema.TableColumn colvarData = new TableSchema.TableColumn(schema);
				colvarData.ColumnName = "data";
				colvarData.DataType = DbType.Binary;
				colvarData.MaxLength = 2147483647;
				colvarData.AutoIncrement = false;
				colvarData.IsNullable = false;
				colvarData.IsPrimaryKey = false;
				colvarData.IsForeignKey = false;
				colvarData.IsReadOnly = false;
				colvarData.DefaultSetting = @"";
				colvarData.ForeignKeyTableName = "";
				schema.Columns.Add(colvarData);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("Sys_ImgAndIcon",schema);
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
		  
		[XmlAttribute("SFileName")]
		[Bindable(true)]
		public string SFileName 
		{
			get { return GetColumnValue<string>(Columns.SFileName); }
			set { SetColumnValue(Columns.SFileName, value); }
		}
		  
		[XmlAttribute("SFilePath")]
		[Bindable(true)]
		public string SFilePath 
		{
			get { return GetColumnValue<string>(Columns.SFilePath); }
			set { SetColumnValue(Columns.SFilePath, value); }
		}
		  
		[XmlAttribute("Data")]
		[Bindable(true)]
		public byte[] Data 
		{
			get { return GetColumnValue<byte[]>(Columns.Data); }
			set { SetColumnValue(Columns.Data, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varSFileName,string varSFilePath,byte[] varData)
		{
			SysImgAndIcon item = new SysImgAndIcon();
			
			item.SFileName = varSFileName;
			
			item.SFilePath = varSFilePath;
			
			item.Data = varData;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varSFileName,string varSFilePath,byte[] varData)
		{
			SysImgAndIcon item = new SysImgAndIcon();
			
				item.Id = varId;
			
				item.SFileName = varSFileName;
			
				item.SFilePath = varSFilePath;
			
				item.Data = varData;
			
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
        
        
        
        public static TableSchema.TableColumn SFileNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn SFilePathColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn DataColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string SFileName = @"sFileName";
			 public static string SFilePath = @"sFilePath";
			 public static string Data = @"data";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
