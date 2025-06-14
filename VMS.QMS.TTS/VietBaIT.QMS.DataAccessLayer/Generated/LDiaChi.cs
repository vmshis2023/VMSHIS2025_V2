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
	/// Strongly-typed collection for the LDiaChi class.
	/// </summary>
    [Serializable]
	public partial class LDiaChiCollection : ActiveList<LDiaChi, LDiaChiCollection>
	{	   
		public LDiaChiCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LDiaChiCollection</returns>
		public LDiaChiCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LDiaChi o = this[i];
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
	/// This is an ActiveRecord class which wraps the L_Dia_Chi table.
	/// </summary>
	[Serializable]
	public partial class LDiaChi : ActiveRecord<LDiaChi>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LDiaChi()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LDiaChi(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LDiaChi(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LDiaChi(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("L_Dia_Chi", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdDiaChi = new TableSchema.TableColumn(schema);
				colvarIdDiaChi.ColumnName = "Id_DiaChi";
				colvarIdDiaChi.DataType = DbType.Int32;
				colvarIdDiaChi.MaxLength = 0;
				colvarIdDiaChi.AutoIncrement = true;
				colvarIdDiaChi.IsNullable = false;
				colvarIdDiaChi.IsPrimaryKey = true;
				colvarIdDiaChi.IsForeignKey = false;
				colvarIdDiaChi.IsReadOnly = false;
				colvarIdDiaChi.DefaultSetting = @"";
				colvarIdDiaChi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdDiaChi);
				
				TableSchema.TableColumn colvarMaDiaChi = new TableSchema.TableColumn(schema);
				colvarMaDiaChi.ColumnName = "Ma_DiaChi";
				colvarMaDiaChi.DataType = DbType.String;
				colvarMaDiaChi.MaxLength = 500;
				colvarMaDiaChi.AutoIncrement = false;
				colvarMaDiaChi.IsNullable = true;
				colvarMaDiaChi.IsPrimaryKey = false;
				colvarMaDiaChi.IsForeignKey = false;
				colvarMaDiaChi.IsReadOnly = false;
				colvarMaDiaChi.DefaultSetting = @"";
				colvarMaDiaChi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaDiaChi);
				
				TableSchema.TableColumn colvarTenDiaChi = new TableSchema.TableColumn(schema);
				colvarTenDiaChi.ColumnName = "Ten_DiaChi";
				colvarTenDiaChi.DataType = DbType.String;
				colvarTenDiaChi.MaxLength = 500;
				colvarTenDiaChi.AutoIncrement = false;
				colvarTenDiaChi.IsNullable = true;
				colvarTenDiaChi.IsPrimaryKey = false;
				colvarTenDiaChi.IsForeignKey = false;
				colvarTenDiaChi.IsReadOnly = false;
				colvarTenDiaChi.DefaultSetting = @"";
				colvarTenDiaChi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenDiaChi);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("L_Dia_Chi",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdDiaChi")]
		[Bindable(true)]
		public int IdDiaChi 
		{
			get { return GetColumnValue<int>(Columns.IdDiaChi); }
			set { SetColumnValue(Columns.IdDiaChi, value); }
		}
		  
		[XmlAttribute("MaDiaChi")]
		[Bindable(true)]
		public string MaDiaChi 
		{
			get { return GetColumnValue<string>(Columns.MaDiaChi); }
			set { SetColumnValue(Columns.MaDiaChi, value); }
		}
		  
		[XmlAttribute("TenDiaChi")]
		[Bindable(true)]
		public string TenDiaChi 
		{
			get { return GetColumnValue<string>(Columns.TenDiaChi); }
			set { SetColumnValue(Columns.TenDiaChi, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaDiaChi,string varTenDiaChi)
		{
			LDiaChi item = new LDiaChi();
			
			item.MaDiaChi = varMaDiaChi;
			
			item.TenDiaChi = varTenDiaChi;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdDiaChi,string varMaDiaChi,string varTenDiaChi)
		{
			LDiaChi item = new LDiaChi();
			
				item.IdDiaChi = varIdDiaChi;
			
				item.MaDiaChi = varMaDiaChi;
			
				item.TenDiaChi = varTenDiaChi;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdDiaChiColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaDiaChiColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TenDiaChiColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdDiaChi = @"Id_DiaChi";
			 public static string MaDiaChi = @"Ma_DiaChi";
			 public static string TenDiaChi = @"Ten_DiaChi";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
