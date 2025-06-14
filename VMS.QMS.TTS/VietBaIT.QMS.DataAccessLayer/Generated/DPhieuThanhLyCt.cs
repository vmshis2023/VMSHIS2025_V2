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
	/// Strongly-typed collection for the DPhieuThanhLyCt class.
	/// </summary>
    [Serializable]
	public partial class DPhieuThanhLyCtCollection : ActiveList<DPhieuThanhLyCt, DPhieuThanhLyCtCollection>
	{	   
		public DPhieuThanhLyCtCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DPhieuThanhLyCtCollection</returns>
		public DPhieuThanhLyCtCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DPhieuThanhLyCt o = this[i];
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
	/// This is an ActiveRecord class which wraps the D_Phieu_ThanhLy_CT table.
	/// </summary>
	[Serializable]
	public partial class DPhieuThanhLyCt : ActiveRecord<DPhieuThanhLyCt>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DPhieuThanhLyCt()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DPhieuThanhLyCt(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DPhieuThanhLyCt(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DPhieuThanhLyCt(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("D_Phieu_ThanhLy_CT", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdThanhLyCt = new TableSchema.TableColumn(schema);
				colvarIdThanhLyCt.ColumnName = "Id_ThanhLy_CT";
				colvarIdThanhLyCt.DataType = DbType.Int32;
				colvarIdThanhLyCt.MaxLength = 0;
				colvarIdThanhLyCt.AutoIncrement = true;
				colvarIdThanhLyCt.IsNullable = false;
				colvarIdThanhLyCt.IsPrimaryKey = true;
				colvarIdThanhLyCt.IsForeignKey = false;
				colvarIdThanhLyCt.IsReadOnly = false;
				colvarIdThanhLyCt.DefaultSetting = @"";
				colvarIdThanhLyCt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThanhLyCt);
				
				TableSchema.TableColumn colvarIdThanhLy = new TableSchema.TableColumn(schema);
				colvarIdThanhLy.ColumnName = "Id_ThanhLy";
				colvarIdThanhLy.DataType = DbType.Int32;
				colvarIdThanhLy.MaxLength = 0;
				colvarIdThanhLy.AutoIncrement = false;
				colvarIdThanhLy.IsNullable = false;
				colvarIdThanhLy.IsPrimaryKey = false;
				colvarIdThanhLy.IsForeignKey = false;
				colvarIdThanhLy.IsReadOnly = false;
				colvarIdThanhLy.DefaultSetting = @"";
				colvarIdThanhLy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThanhLy);
				
				TableSchema.TableColumn colvarIdThuoc = new TableSchema.TableColumn(schema);
				colvarIdThuoc.ColumnName = "Id_Thuoc";
				colvarIdThuoc.DataType = DbType.Int32;
				colvarIdThuoc.MaxLength = 0;
				colvarIdThuoc.AutoIncrement = false;
				colvarIdThuoc.IsNullable = false;
				colvarIdThuoc.IsPrimaryKey = false;
				colvarIdThuoc.IsForeignKey = false;
				colvarIdThuoc.IsReadOnly = false;
				colvarIdThuoc.DefaultSetting = @"";
				colvarIdThuoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThuoc);
				
				TableSchema.TableColumn colvarSoLuong = new TableSchema.TableColumn(schema);
				colvarSoLuong.ColumnName = "So_Luong";
				colvarSoLuong.DataType = DbType.Int32;
				colvarSoLuong.MaxLength = 0;
				colvarSoLuong.AutoIncrement = false;
				colvarSoLuong.IsNullable = false;
				colvarSoLuong.IsPrimaryKey = false;
				colvarSoLuong.IsForeignKey = false;
				colvarSoLuong.IsReadOnly = false;
				colvarSoLuong.DefaultSetting = @"";
				colvarSoLuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLuong);
				
				TableSchema.TableColumn colvarNgayHetHan = new TableSchema.TableColumn(schema);
				colvarNgayHetHan.ColumnName = "Ngay_Het_Han";
				colvarNgayHetHan.DataType = DbType.DateTime;
				colvarNgayHetHan.MaxLength = 0;
				colvarNgayHetHan.AutoIncrement = false;
				colvarNgayHetHan.IsNullable = false;
				colvarNgayHetHan.IsPrimaryKey = false;
				colvarNgayHetHan.IsForeignKey = false;
				colvarNgayHetHan.IsReadOnly = false;
				colvarNgayHetHan.DefaultSetting = @"";
				colvarNgayHetHan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayHetHan);
				
				TableSchema.TableColumn colvarDonGia = new TableSchema.TableColumn(schema);
				colvarDonGia.ColumnName = "Don_Gia";
				colvarDonGia.DataType = DbType.Decimal;
				colvarDonGia.MaxLength = 0;
				colvarDonGia.AutoIncrement = false;
				colvarDonGia.IsNullable = false;
				colvarDonGia.IsPrimaryKey = false;
				colvarDonGia.IsForeignKey = false;
				colvarDonGia.IsReadOnly = false;
				colvarDonGia.DefaultSetting = @"";
				colvarDonGia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDonGia);
				
				TableSchema.TableColumn colvarGiaBan = new TableSchema.TableColumn(schema);
				colvarGiaBan.ColumnName = "Gia_Ban";
				colvarGiaBan.DataType = DbType.Decimal;
				colvarGiaBan.MaxLength = 0;
				colvarGiaBan.AutoIncrement = false;
				colvarGiaBan.IsNullable = false;
				colvarGiaBan.IsPrimaryKey = false;
				colvarGiaBan.IsForeignKey = false;
				colvarGiaBan.IsReadOnly = false;
				colvarGiaBan.DefaultSetting = @"";
				colvarGiaBan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGiaBan);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("D_Phieu_ThanhLy_CT",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdThanhLyCt")]
		[Bindable(true)]
		public int IdThanhLyCt 
		{
			get { return GetColumnValue<int>(Columns.IdThanhLyCt); }
			set { SetColumnValue(Columns.IdThanhLyCt, value); }
		}
		  
		[XmlAttribute("IdThanhLy")]
		[Bindable(true)]
		public int IdThanhLy 
		{
			get { return GetColumnValue<int>(Columns.IdThanhLy); }
			set { SetColumnValue(Columns.IdThanhLy, value); }
		}
		  
		[XmlAttribute("IdThuoc")]
		[Bindable(true)]
		public int IdThuoc 
		{
			get { return GetColumnValue<int>(Columns.IdThuoc); }
			set { SetColumnValue(Columns.IdThuoc, value); }
		}
		  
		[XmlAttribute("SoLuong")]
		[Bindable(true)]
		public int SoLuong 
		{
			get { return GetColumnValue<int>(Columns.SoLuong); }
			set { SetColumnValue(Columns.SoLuong, value); }
		}
		  
		[XmlAttribute("NgayHetHan")]
		[Bindable(true)]
		public DateTime NgayHetHan 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayHetHan); }
			set { SetColumnValue(Columns.NgayHetHan, value); }
		}
		  
		[XmlAttribute("DonGia")]
		[Bindable(true)]
		public decimal DonGia 
		{
			get { return GetColumnValue<decimal>(Columns.DonGia); }
			set { SetColumnValue(Columns.DonGia, value); }
		}
		  
		[XmlAttribute("GiaBan")]
		[Bindable(true)]
		public decimal GiaBan 
		{
			get { return GetColumnValue<decimal>(Columns.GiaBan); }
			set { SetColumnValue(Columns.GiaBan, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varIdThanhLy,int varIdThuoc,int varSoLuong,DateTime varNgayHetHan,decimal varDonGia,decimal varGiaBan)
		{
			DPhieuThanhLyCt item = new DPhieuThanhLyCt();
			
			item.IdThanhLy = varIdThanhLy;
			
			item.IdThuoc = varIdThuoc;
			
			item.SoLuong = varSoLuong;
			
			item.NgayHetHan = varNgayHetHan;
			
			item.DonGia = varDonGia;
			
			item.GiaBan = varGiaBan;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdThanhLyCt,int varIdThanhLy,int varIdThuoc,int varSoLuong,DateTime varNgayHetHan,decimal varDonGia,decimal varGiaBan)
		{
			DPhieuThanhLyCt item = new DPhieuThanhLyCt();
			
				item.IdThanhLyCt = varIdThanhLyCt;
			
				item.IdThanhLy = varIdThanhLy;
			
				item.IdThuoc = varIdThuoc;
			
				item.SoLuong = varSoLuong;
			
				item.NgayHetHan = varNgayHetHan;
			
				item.DonGia = varDonGia;
			
				item.GiaBan = varGiaBan;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdThanhLyCtColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThanhLyColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThuocColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLuongColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayHetHanColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn DonGiaColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn GiaBanColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdThanhLyCt = @"Id_ThanhLy_CT";
			 public static string IdThanhLy = @"Id_ThanhLy";
			 public static string IdThuoc = @"Id_Thuoc";
			 public static string SoLuong = @"So_Luong";
			 public static string NgayHetHan = @"Ngay_Het_Han";
			 public static string DonGia = @"Don_Gia";
			 public static string GiaBan = @"Gia_Ban";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
