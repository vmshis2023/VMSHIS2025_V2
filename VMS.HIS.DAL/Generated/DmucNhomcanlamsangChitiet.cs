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
	/// Strongly-typed collection for the DmucNhomcanlamsangChitiet class.
	/// </summary>
    [Serializable]
	public partial class DmucNhomcanlamsangChitietCollection : ActiveList<DmucNhomcanlamsangChitiet, DmucNhomcanlamsangChitietCollection>
	{	   
		public DmucNhomcanlamsangChitietCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DmucNhomcanlamsangChitietCollection</returns>
		public DmucNhomcanlamsangChitietCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DmucNhomcanlamsangChitiet o = this[i];
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
	/// This is an ActiveRecord class which wraps the dmuc_nhomcanlamsang_chitiet table.
	/// </summary>
	[Serializable]
	public partial class DmucNhomcanlamsangChitiet : ActiveRecord<DmucNhomcanlamsangChitiet>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DmucNhomcanlamsangChitiet()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DmucNhomcanlamsangChitiet(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DmucNhomcanlamsangChitiet(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DmucNhomcanlamsangChitiet(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("dmuc_nhomcanlamsang_chitiet", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdChitiet = new TableSchema.TableColumn(schema);
				colvarIdChitiet.ColumnName = "id_chitiet";
				colvarIdChitiet.DataType = DbType.Int32;
				colvarIdChitiet.MaxLength = 0;
				colvarIdChitiet.AutoIncrement = true;
				colvarIdChitiet.IsNullable = false;
				colvarIdChitiet.IsPrimaryKey = true;
				colvarIdChitiet.IsForeignKey = false;
				colvarIdChitiet.IsReadOnly = false;
				colvarIdChitiet.DefaultSetting = @"";
				colvarIdChitiet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChitiet);
				
				TableSchema.TableColumn colvarIdNhom = new TableSchema.TableColumn(schema);
				colvarIdNhom.ColumnName = "id_nhom";
				colvarIdNhom.DataType = DbType.Int16;
				colvarIdNhom.MaxLength = 0;
				colvarIdNhom.AutoIncrement = false;
				colvarIdNhom.IsNullable = false;
				colvarIdNhom.IsPrimaryKey = false;
				colvarIdNhom.IsForeignKey = false;
				colvarIdNhom.IsReadOnly = false;
				colvarIdNhom.DefaultSetting = @"";
				colvarIdNhom.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdNhom);
				
				TableSchema.TableColumn colvarIdDichvu = new TableSchema.TableColumn(schema);
				colvarIdDichvu.ColumnName = "id_dichvu";
				colvarIdDichvu.DataType = DbType.Int32;
				colvarIdDichvu.MaxLength = 0;
				colvarIdDichvu.AutoIncrement = false;
				colvarIdDichvu.IsNullable = false;
				colvarIdDichvu.IsPrimaryKey = false;
				colvarIdDichvu.IsForeignKey = false;
				colvarIdDichvu.IsReadOnly = false;
				colvarIdDichvu.DefaultSetting = @"";
				colvarIdDichvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdDichvu);
				
				TableSchema.TableColumn colvarIdChitietdichvu = new TableSchema.TableColumn(schema);
				colvarIdChitietdichvu.ColumnName = "id_chitietdichvu";
				colvarIdChitietdichvu.DataType = DbType.Int32;
				colvarIdChitietdichvu.MaxLength = 0;
				colvarIdChitietdichvu.AutoIncrement = false;
				colvarIdChitietdichvu.IsNullable = false;
				colvarIdChitietdichvu.IsPrimaryKey = false;
				colvarIdChitietdichvu.IsForeignKey = false;
				colvarIdChitietdichvu.IsReadOnly = false;
				colvarIdChitietdichvu.DefaultSetting = @"";
				colvarIdChitietdichvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChitietdichvu);
				
				TableSchema.TableColumn colvarSoLuong = new TableSchema.TableColumn(schema);
				colvarSoLuong.ColumnName = "so_luong";
				colvarSoLuong.DataType = DbType.Int32;
				colvarSoLuong.MaxLength = 0;
				colvarSoLuong.AutoIncrement = false;
				colvarSoLuong.IsNullable = false;
				colvarSoLuong.IsPrimaryKey = false;
				colvarSoLuong.IsForeignKey = false;
				colvarSoLuong.IsReadOnly = false;
				
						colvarSoLuong.DefaultSetting = @"((1))";
				colvarSoLuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLuong);
				
				TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
				colvarNguoiTao.ColumnName = "nguoi_tao";
				colvarNguoiTao.DataType = DbType.String;
				colvarNguoiTao.MaxLength = 30;
				colvarNguoiTao.AutoIncrement = false;
				colvarNguoiTao.IsNullable = false;
				colvarNguoiTao.IsPrimaryKey = false;
				colvarNguoiTao.IsForeignKey = false;
				colvarNguoiTao.IsReadOnly = false;
				colvarNguoiTao.DefaultSetting = @"";
				colvarNguoiTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiTao);
				
				TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
				colvarNgayTao.ColumnName = "ngay_tao";
				colvarNgayTao.DataType = DbType.DateTime;
				colvarNgayTao.MaxLength = 0;
				colvarNgayTao.AutoIncrement = false;
				colvarNgayTao.IsNullable = false;
				colvarNgayTao.IsPrimaryKey = false;
				colvarNgayTao.IsForeignKey = false;
				colvarNgayTao.IsReadOnly = false;
				colvarNgayTao.DefaultSetting = @"";
				colvarNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTao);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("dmuc_nhomcanlamsang_chitiet",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdChitiet")]
		[Bindable(true)]
		public int IdChitiet 
		{
			get { return GetColumnValue<int>(Columns.IdChitiet); }
			set { SetColumnValue(Columns.IdChitiet, value); }
		}
		  
		[XmlAttribute("IdNhom")]
		[Bindable(true)]
		public short IdNhom 
		{
			get { return GetColumnValue<short>(Columns.IdNhom); }
			set { SetColumnValue(Columns.IdNhom, value); }
		}
		  
		[XmlAttribute("IdDichvu")]
		[Bindable(true)]
		public int IdDichvu 
		{
			get { return GetColumnValue<int>(Columns.IdDichvu); }
			set { SetColumnValue(Columns.IdDichvu, value); }
		}
		  
		[XmlAttribute("IdChitietdichvu")]
		[Bindable(true)]
		public int IdChitietdichvu 
		{
			get { return GetColumnValue<int>(Columns.IdChitietdichvu); }
			set { SetColumnValue(Columns.IdChitietdichvu, value); }
		}
		  
		[XmlAttribute("SoLuong")]
		[Bindable(true)]
		public int SoLuong 
		{
			get { return GetColumnValue<int>(Columns.SoLuong); }
			set { SetColumnValue(Columns.SoLuong, value); }
		}
		  
		[XmlAttribute("NguoiTao")]
		[Bindable(true)]
		public string NguoiTao 
		{
			get { return GetColumnValue<string>(Columns.NguoiTao); }
			set { SetColumnValue(Columns.NguoiTao, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime NgayTao 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(short varIdNhom,int varIdDichvu,int varIdChitietdichvu,int varSoLuong,string varNguoiTao,DateTime varNgayTao)
		{
			DmucNhomcanlamsangChitiet item = new DmucNhomcanlamsangChitiet();
			
			item.IdNhom = varIdNhom;
			
			item.IdDichvu = varIdDichvu;
			
			item.IdChitietdichvu = varIdChitietdichvu;
			
			item.SoLuong = varSoLuong;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgayTao = varNgayTao;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdChitiet,short varIdNhom,int varIdDichvu,int varIdChitietdichvu,int varSoLuong,string varNguoiTao,DateTime varNgayTao)
		{
			DmucNhomcanlamsangChitiet item = new DmucNhomcanlamsangChitiet();
			
				item.IdChitiet = varIdChitiet;
			
				item.IdNhom = varIdNhom;
			
				item.IdDichvu = varIdDichvu;
			
				item.IdChitietdichvu = varIdChitietdichvu;
			
				item.SoLuong = varSoLuong;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgayTao = varNgayTao;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdChitietColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdNhomColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IdDichvuColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdChitietdichvuColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLuongColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdChitiet = @"id_chitiet";
			 public static string IdNhom = @"id_nhom";
			 public static string IdDichvu = @"id_dichvu";
			 public static string IdChitietdichvu = @"id_chitietdichvu";
			 public static string SoLuong = @"so_luong";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string NgayTao = @"ngay_tao";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
