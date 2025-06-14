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
	/// Strongly-typed collection for the DmucBenhvien class.
	/// </summary>
    [Serializable]
	public partial class DmucBenhvienCollection : ActiveList<DmucBenhvien, DmucBenhvienCollection>
	{	   
		public DmucBenhvienCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DmucBenhvienCollection</returns>
		public DmucBenhvienCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DmucBenhvien o = this[i];
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
	/// This is an ActiveRecord class which wraps the dmuc_benhvien table.
	/// </summary>
	[Serializable]
	public partial class DmucBenhvien : ActiveRecord<DmucBenhvien>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DmucBenhvien()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DmucBenhvien(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DmucBenhvien(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DmucBenhvien(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("dmuc_benhvien", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdBenhvien = new TableSchema.TableColumn(schema);
				colvarIdBenhvien.ColumnName = "id_benhvien";
				colvarIdBenhvien.DataType = DbType.Int32;
				colvarIdBenhvien.MaxLength = 0;
				colvarIdBenhvien.AutoIncrement = true;
				colvarIdBenhvien.IsNullable = false;
				colvarIdBenhvien.IsPrimaryKey = true;
				colvarIdBenhvien.IsForeignKey = false;
				colvarIdBenhvien.IsReadOnly = false;
				colvarIdBenhvien.DefaultSetting = @"";
				colvarIdBenhvien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBenhvien);
				
				TableSchema.TableColumn colvarMaBenhvien = new TableSchema.TableColumn(schema);
				colvarMaBenhvien.ColumnName = "ma_benhvien";
				colvarMaBenhvien.DataType = DbType.String;
				colvarMaBenhvien.MaxLength = 10;
				colvarMaBenhvien.AutoIncrement = false;
				colvarMaBenhvien.IsNullable = false;
				colvarMaBenhvien.IsPrimaryKey = false;
				colvarMaBenhvien.IsForeignKey = false;
				colvarMaBenhvien.IsReadOnly = false;
				colvarMaBenhvien.DefaultSetting = @"";
				colvarMaBenhvien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaBenhvien);
				
				TableSchema.TableColumn colvarTenBenhvien = new TableSchema.TableColumn(schema);
				colvarTenBenhvien.ColumnName = "ten_benhvien";
				colvarTenBenhvien.DataType = DbType.String;
				colvarTenBenhvien.MaxLength = 200;
				colvarTenBenhvien.AutoIncrement = false;
				colvarTenBenhvien.IsNullable = false;
				colvarTenBenhvien.IsPrimaryKey = false;
				colvarTenBenhvien.IsForeignKey = false;
				colvarTenBenhvien.IsReadOnly = false;
				colvarTenBenhvien.DefaultSetting = @"";
				colvarTenBenhvien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenBenhvien);
				
				TableSchema.TableColumn colvarMaThanhpho = new TableSchema.TableColumn(schema);
				colvarMaThanhpho.ColumnName = "ma_thanhpho";
				colvarMaThanhpho.DataType = DbType.String;
				colvarMaThanhpho.MaxLength = 20;
				colvarMaThanhpho.AutoIncrement = false;
				colvarMaThanhpho.IsNullable = false;
				colvarMaThanhpho.IsPrimaryKey = false;
				colvarMaThanhpho.IsForeignKey = false;
				colvarMaThanhpho.IsReadOnly = false;
				colvarMaThanhpho.DefaultSetting = @"";
				colvarMaThanhpho.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaThanhpho);
				
				TableSchema.TableColumn colvarSttHthi = new TableSchema.TableColumn(schema);
				colvarSttHthi.ColumnName = "stt_hthi";
				colvarSttHthi.DataType = DbType.Int16;
				colvarSttHthi.MaxLength = 0;
				colvarSttHthi.AutoIncrement = false;
				colvarSttHthi.IsNullable = true;
				colvarSttHthi.IsPrimaryKey = false;
				colvarSttHthi.IsForeignKey = false;
				colvarSttHthi.IsReadOnly = false;
				colvarSttHthi.DefaultSetting = @"";
				colvarSttHthi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSttHthi);
				
				TableSchema.TableColumn colvarTuyen = new TableSchema.TableColumn(schema);
				colvarTuyen.ColumnName = "tuyen";
				colvarTuyen.DataType = DbType.AnsiString;
				colvarTuyen.MaxLength = 10;
				colvarTuyen.AutoIncrement = false;
				colvarTuyen.IsNullable = true;
				colvarTuyen.IsPrimaryKey = false;
				colvarTuyen.IsForeignKey = false;
				colvarTuyen.IsReadOnly = false;
				colvarTuyen.DefaultSetting = @"";
				colvarTuyen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTuyen);
				
				TableSchema.TableColumn colvarGhichu = new TableSchema.TableColumn(schema);
				colvarGhichu.ColumnName = "ghichu";
				colvarGhichu.DataType = DbType.String;
				colvarGhichu.MaxLength = 200;
				colvarGhichu.AutoIncrement = false;
				colvarGhichu.IsNullable = true;
				colvarGhichu.IsPrimaryKey = false;
				colvarGhichu.IsForeignKey = false;
				colvarGhichu.IsReadOnly = false;
				colvarGhichu.DefaultSetting = @"";
				colvarGhichu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGhichu);
				
				TableSchema.TableColumn colvarTrangthai = new TableSchema.TableColumn(schema);
				colvarTrangthai.ColumnName = "trangthai";
				colvarTrangthai.DataType = DbType.Boolean;
				colvarTrangthai.MaxLength = 0;
				colvarTrangthai.AutoIncrement = false;
				colvarTrangthai.IsNullable = true;
				colvarTrangthai.IsPrimaryKey = false;
				colvarTrangthai.IsForeignKey = false;
				colvarTrangthai.IsReadOnly = false;
				colvarTrangthai.DefaultSetting = @"";
				colvarTrangthai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangthai);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("dmuc_benhvien",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdBenhvien")]
		[Bindable(true)]
		public int IdBenhvien 
		{
			get { return GetColumnValue<int>(Columns.IdBenhvien); }
			set { SetColumnValue(Columns.IdBenhvien, value); }
		}
		  
		[XmlAttribute("MaBenhvien")]
		[Bindable(true)]
		public string MaBenhvien 
		{
			get { return GetColumnValue<string>(Columns.MaBenhvien); }
			set { SetColumnValue(Columns.MaBenhvien, value); }
		}
		  
		[XmlAttribute("TenBenhvien")]
		[Bindable(true)]
		public string TenBenhvien 
		{
			get { return GetColumnValue<string>(Columns.TenBenhvien); }
			set { SetColumnValue(Columns.TenBenhvien, value); }
		}
		  
		[XmlAttribute("MaThanhpho")]
		[Bindable(true)]
		public string MaThanhpho 
		{
			get { return GetColumnValue<string>(Columns.MaThanhpho); }
			set { SetColumnValue(Columns.MaThanhpho, value); }
		}
		  
		[XmlAttribute("SttHthi")]
		[Bindable(true)]
		public short? SttHthi 
		{
			get { return GetColumnValue<short?>(Columns.SttHthi); }
			set { SetColumnValue(Columns.SttHthi, value); }
		}
		  
		[XmlAttribute("Tuyen")]
		[Bindable(true)]
		public string Tuyen 
		{
			get { return GetColumnValue<string>(Columns.Tuyen); }
			set { SetColumnValue(Columns.Tuyen, value); }
		}
		  
		[XmlAttribute("Ghichu")]
		[Bindable(true)]
		public string Ghichu 
		{
			get { return GetColumnValue<string>(Columns.Ghichu); }
			set { SetColumnValue(Columns.Ghichu, value); }
		}
		  
		[XmlAttribute("Trangthai")]
		[Bindable(true)]
		public bool? Trangthai 
		{
			get { return GetColumnValue<bool?>(Columns.Trangthai); }
			set { SetColumnValue(Columns.Trangthai, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaBenhvien,string varTenBenhvien,string varMaThanhpho,short? varSttHthi,string varTuyen,string varGhichu,bool? varTrangthai)
		{
			DmucBenhvien item = new DmucBenhvien();
			
			item.MaBenhvien = varMaBenhvien;
			
			item.TenBenhvien = varTenBenhvien;
			
			item.MaThanhpho = varMaThanhpho;
			
			item.SttHthi = varSttHthi;
			
			item.Tuyen = varTuyen;
			
			item.Ghichu = varGhichu;
			
			item.Trangthai = varTrangthai;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdBenhvien,string varMaBenhvien,string varTenBenhvien,string varMaThanhpho,short? varSttHthi,string varTuyen,string varGhichu,bool? varTrangthai)
		{
			DmucBenhvien item = new DmucBenhvien();
			
				item.IdBenhvien = varIdBenhvien;
			
				item.MaBenhvien = varMaBenhvien;
			
				item.TenBenhvien = varTenBenhvien;
			
				item.MaThanhpho = varMaThanhpho;
			
				item.SttHthi = varSttHthi;
			
				item.Tuyen = varTuyen;
			
				item.Ghichu = varGhichu;
			
				item.Trangthai = varTrangthai;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdBenhvienColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaBenhvienColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TenBenhvienColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn MaThanhphoColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SttHthiColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn TuyenColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn GhichuColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangthaiColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdBenhvien = @"id_benhvien";
			 public static string MaBenhvien = @"ma_benhvien";
			 public static string TenBenhvien = @"ten_benhvien";
			 public static string MaThanhpho = @"ma_thanhpho";
			 public static string SttHthi = @"stt_hthi";
			 public static string Tuyen = @"tuyen";
			 public static string Ghichu = @"ghichu";
			 public static string Trangthai = @"trangthai";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
