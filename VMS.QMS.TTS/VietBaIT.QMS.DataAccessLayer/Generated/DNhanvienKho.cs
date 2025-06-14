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
	/// Strongly-typed collection for the DNhanvienKho class.
	/// </summary>
    [Serializable]
	public partial class DNhanvienKhoCollection : ActiveList<DNhanvienKho, DNhanvienKhoCollection>
	{	   
		public DNhanvienKhoCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DNhanvienKhoCollection</returns>
		public DNhanvienKhoCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DNhanvienKho o = this[i];
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
	/// This is an ActiveRecord class which wraps the D_NHANVIEN_KHO table.
	/// </summary>
	[Serializable]
	public partial class DNhanvienKho : ActiveRecord<DNhanvienKho>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DNhanvienKho()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DNhanvienKho(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DNhanvienKho(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DNhanvienKho(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("D_NHANVIEN_KHO", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdNhanVien = new TableSchema.TableColumn(schema);
				colvarIdNhanVien.ColumnName = "ID_NHAN_VIEN";
				colvarIdNhanVien.DataType = DbType.Int16;
				colvarIdNhanVien.MaxLength = 0;
				colvarIdNhanVien.AutoIncrement = false;
				colvarIdNhanVien.IsNullable = false;
				colvarIdNhanVien.IsPrimaryKey = true;
				colvarIdNhanVien.IsForeignKey = false;
				colvarIdNhanVien.IsReadOnly = false;
				colvarIdNhanVien.DefaultSetting = @"";
				colvarIdNhanVien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdNhanVien);
				
				TableSchema.TableColumn colvarIdKho = new TableSchema.TableColumn(schema);
				colvarIdKho.ColumnName = "ID_KHO";
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
				
				TableSchema.TableColumn colvarGhiChu = new TableSchema.TableColumn(schema);
				colvarGhiChu.ColumnName = "GHI_CHU";
				colvarGhiChu.DataType = DbType.String;
				colvarGhiChu.MaxLength = 50;
				colvarGhiChu.AutoIncrement = false;
				colvarGhiChu.IsNullable = true;
				colvarGhiChu.IsPrimaryKey = false;
				colvarGhiChu.IsForeignKey = false;
				colvarGhiChu.IsReadOnly = false;
				colvarGhiChu.DefaultSetting = @"";
				colvarGhiChu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGhiChu);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("D_NHANVIEN_KHO",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdNhanVien")]
		[Bindable(true)]
		public short IdNhanVien 
		{
			get { return GetColumnValue<short>(Columns.IdNhanVien); }
			set { SetColumnValue(Columns.IdNhanVien, value); }
		}
		  
		[XmlAttribute("IdKho")]
		[Bindable(true)]
		public short IdKho 
		{
			get { return GetColumnValue<short>(Columns.IdKho); }
			set { SetColumnValue(Columns.IdKho, value); }
		}
		  
		[XmlAttribute("GhiChu")]
		[Bindable(true)]
		public string GhiChu 
		{
			get { return GetColumnValue<string>(Columns.GhiChu); }
			set { SetColumnValue(Columns.GhiChu, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(short varIdNhanVien,short varIdKho,string varGhiChu)
		{
			DNhanvienKho item = new DNhanvienKho();
			
			item.IdNhanVien = varIdNhanVien;
			
			item.IdKho = varIdKho;
			
			item.GhiChu = varGhiChu;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(short varIdNhanVien,short varIdKho,string varGhiChu)
		{
			DNhanvienKho item = new DNhanvienKho();
			
				item.IdNhanVien = varIdNhanVien;
			
				item.IdKho = varIdKho;
			
				item.GhiChu = varGhiChu;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdNhanVienColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhoColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn GhiChuColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdNhanVien = @"ID_NHAN_VIEN";
			 public static string IdKho = @"ID_KHO";
			 public static string GhiChu = @"GHI_CHU";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
