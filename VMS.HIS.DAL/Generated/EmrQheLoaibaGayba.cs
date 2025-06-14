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
	/// Strongly-typed collection for the EmrQheLoaibaGayba class.
	/// </summary>
    [Serializable]
	public partial class EmrQheLoaibaGaybaCollection : ActiveList<EmrQheLoaibaGayba, EmrQheLoaibaGaybaCollection>
	{	   
		public EmrQheLoaibaGaybaCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>EmrQheLoaibaGaybaCollection</returns>
		public EmrQheLoaibaGaybaCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                EmrQheLoaibaGayba o = this[i];
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
	/// This is an ActiveRecord class which wraps the emr_qhe_loaiba_gayba table.
	/// </summary>
	[Serializable]
	public partial class EmrQheLoaibaGayba : ActiveRecord<EmrQheLoaibaGayba>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public EmrQheLoaibaGayba()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public EmrQheLoaibaGayba(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public EmrQheLoaibaGayba(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public EmrQheLoaibaGayba(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("emr_qhe_loaiba_gayba", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "id";
				colvarId.DataType = DbType.Int64;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarLoaiBa = new TableSchema.TableColumn(schema);
				colvarLoaiBa.ColumnName = "loai_ba";
				colvarLoaiBa.DataType = DbType.String;
				colvarLoaiBa.MaxLength = 30;
				colvarLoaiBa.AutoIncrement = false;
				colvarLoaiBa.IsNullable = false;
				colvarLoaiBa.IsPrimaryKey = false;
				colvarLoaiBa.IsForeignKey = false;
				colvarLoaiBa.IsReadOnly = false;
				colvarLoaiBa.DefaultSetting = @"";
				colvarLoaiBa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoaiBa);
				
				TableSchema.TableColumn colvarMaGay = new TableSchema.TableColumn(schema);
				colvarMaGay.ColumnName = "ma_gay";
				colvarMaGay.DataType = DbType.String;
				colvarMaGay.MaxLength = 30;
				colvarMaGay.AutoIncrement = false;
				colvarMaGay.IsNullable = false;
				colvarMaGay.IsPrimaryKey = false;
				colvarMaGay.IsForeignKey = false;
				colvarMaGay.IsReadOnly = false;
				colvarMaGay.DefaultSetting = @"";
				colvarMaGay.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaGay);
				
				TableSchema.TableColumn colvarMaPhieu = new TableSchema.TableColumn(schema);
				colvarMaPhieu.ColumnName = "ma_phieu";
				colvarMaPhieu.DataType = DbType.String;
				colvarMaPhieu.MaxLength = 30;
				colvarMaPhieu.AutoIncrement = false;
				colvarMaPhieu.IsNullable = false;
				colvarMaPhieu.IsPrimaryKey = false;
				colvarMaPhieu.IsForeignKey = false;
				colvarMaPhieu.IsReadOnly = false;
				colvarMaPhieu.DefaultSetting = @"";
				colvarMaPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaPhieu);
				
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
				DataService.Providers["ORM"].AddSchema("emr_qhe_loaiba_gayba",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public long Id 
		{
			get { return GetColumnValue<long>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("LoaiBa")]
		[Bindable(true)]
		public string LoaiBa 
		{
			get { return GetColumnValue<string>(Columns.LoaiBa); }
			set { SetColumnValue(Columns.LoaiBa, value); }
		}
		  
		[XmlAttribute("MaGay")]
		[Bindable(true)]
		public string MaGay 
		{
			get { return GetColumnValue<string>(Columns.MaGay); }
			set { SetColumnValue(Columns.MaGay, value); }
		}
		  
		[XmlAttribute("MaPhieu")]
		[Bindable(true)]
		public string MaPhieu 
		{
			get { return GetColumnValue<string>(Columns.MaPhieu); }
			set { SetColumnValue(Columns.MaPhieu, value); }
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
		public static void Insert(string varLoaiBa,string varMaGay,string varMaPhieu,string varNguoiTao,DateTime varNgayTao)
		{
			EmrQheLoaibaGayba item = new EmrQheLoaibaGayba();
			
			item.LoaiBa = varLoaiBa;
			
			item.MaGay = varMaGay;
			
			item.MaPhieu = varMaPhieu;
			
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
		public static void Update(long varId,string varLoaiBa,string varMaGay,string varMaPhieu,string varNguoiTao,DateTime varNgayTao)
		{
			EmrQheLoaibaGayba item = new EmrQheLoaibaGayba();
			
				item.Id = varId;
			
				item.LoaiBa = varLoaiBa;
			
				item.MaGay = varMaGay;
			
				item.MaPhieu = varMaPhieu;
			
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
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiBaColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn MaGayColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn MaPhieuColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"id";
			 public static string LoaiBa = @"loai_ba";
			 public static string MaGay = @"ma_gay";
			 public static string MaPhieu = @"ma_phieu";
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
