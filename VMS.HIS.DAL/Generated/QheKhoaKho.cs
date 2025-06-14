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
	/// Strongly-typed collection for the QheKhoaKho class.
	/// </summary>
    [Serializable]
	public partial class QheKhoaKhoCollection : ActiveList<QheKhoaKho, QheKhoaKhoCollection>
	{	   
		public QheKhoaKhoCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>QheKhoaKhoCollection</returns>
		public QheKhoaKhoCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                QheKhoaKho o = this[i];
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
	/// This is an ActiveRecord class which wraps the qhe_khoa_kho table.
	/// </summary>
	[Serializable]
	public partial class QheKhoaKho : ActiveRecord<QheKhoaKho>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public QheKhoaKho()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public QheKhoaKho(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public QheKhoaKho(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public QheKhoaKho(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("qhe_khoa_kho", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdKhoa = new TableSchema.TableColumn(schema);
				colvarIdKhoa.ColumnName = "id_khoa";
				colvarIdKhoa.DataType = DbType.Int16;
				colvarIdKhoa.MaxLength = 0;
				colvarIdKhoa.AutoIncrement = false;
				colvarIdKhoa.IsNullable = false;
				colvarIdKhoa.IsPrimaryKey = true;
				colvarIdKhoa.IsForeignKey = false;
				colvarIdKhoa.IsReadOnly = false;
				colvarIdKhoa.DefaultSetting = @"";
				colvarIdKhoa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhoa);
				
				TableSchema.TableColumn colvarIdKho = new TableSchema.TableColumn(schema);
				colvarIdKho.ColumnName = "id_kho";
				colvarIdKho.DataType = DbType.Int16;
				colvarIdKho.MaxLength = 0;
				colvarIdKho.AutoIncrement = false;
				colvarIdKho.IsNullable = false;
				colvarIdKho.IsPrimaryKey = true;
				colvarIdKho.IsForeignKey = false;
				colvarIdKho.IsReadOnly = false;
				colvarIdKho.DefaultSetting = @"";
				colvarIdKho.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKho);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("qhe_khoa_kho",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdKhoa")]
		[Bindable(true)]
		public short IdKhoa 
		{
			get { return GetColumnValue<short>(Columns.IdKhoa); }
			set { SetColumnValue(Columns.IdKhoa, value); }
		}
		  
		[XmlAttribute("IdKho")]
		[Bindable(true)]
		public short IdKho 
		{
			get { return GetColumnValue<short>(Columns.IdKho); }
			set { SetColumnValue(Columns.IdKho, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(short varIdKhoa,short varIdKho)
		{
			QheKhoaKho item = new QheKhoaKho();
			
			item.IdKhoa = varIdKhoa;
			
			item.IdKho = varIdKho;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(short varIdKhoa,short varIdKho)
		{
			QheKhoaKho item = new QheKhoaKho();
			
				item.IdKhoa = varIdKhoa;
			
				item.IdKho = varIdKho;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdKhoaColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhoColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdKhoa = @"id_khoa";
			 public static string IdKho = @"id_kho";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
