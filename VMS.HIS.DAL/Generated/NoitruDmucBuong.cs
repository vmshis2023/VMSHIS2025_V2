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
	/// Strongly-typed collection for the NoitruDmucBuong class.
	/// </summary>
    [Serializable]
	public partial class NoitruDmucBuongCollection : ActiveList<NoitruDmucBuong, NoitruDmucBuongCollection>
	{	   
		public NoitruDmucBuongCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>NoitruDmucBuongCollection</returns>
		public NoitruDmucBuongCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                NoitruDmucBuong o = this[i];
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
	/// This is an ActiveRecord class which wraps the noitru_dmuc_buong table.
	/// </summary>
	[Serializable]
	public partial class NoitruDmucBuong : ActiveRecord<NoitruDmucBuong>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public NoitruDmucBuong()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public NoitruDmucBuong(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public NoitruDmucBuong(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public NoitruDmucBuong(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("noitru_dmuc_buong", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdBuong = new TableSchema.TableColumn(schema);
				colvarIdBuong.ColumnName = "id_buong";
				colvarIdBuong.DataType = DbType.Int16;
				colvarIdBuong.MaxLength = 0;
				colvarIdBuong.AutoIncrement = true;
				colvarIdBuong.IsNullable = false;
				colvarIdBuong.IsPrimaryKey = true;
				colvarIdBuong.IsForeignKey = false;
				colvarIdBuong.IsReadOnly = false;
				colvarIdBuong.DefaultSetting = @"";
				colvarIdBuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBuong);
				
				TableSchema.TableColumn colvarMaBuong = new TableSchema.TableColumn(schema);
				colvarMaBuong.ColumnName = "ma_buong";
				colvarMaBuong.DataType = DbType.String;
				colvarMaBuong.MaxLength = 20;
				colvarMaBuong.AutoIncrement = false;
				colvarMaBuong.IsNullable = false;
				colvarMaBuong.IsPrimaryKey = false;
				colvarMaBuong.IsForeignKey = false;
				colvarMaBuong.IsReadOnly = false;
				colvarMaBuong.DefaultSetting = @"";
				colvarMaBuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaBuong);
				
				TableSchema.TableColumn colvarTenBuong = new TableSchema.TableColumn(schema);
				colvarTenBuong.ColumnName = "ten_buong";
				colvarTenBuong.DataType = DbType.String;
				colvarTenBuong.MaxLength = 50;
				colvarTenBuong.AutoIncrement = false;
				colvarTenBuong.IsNullable = false;
				colvarTenBuong.IsPrimaryKey = false;
				colvarTenBuong.IsForeignKey = false;
				colvarTenBuong.IsReadOnly = false;
				colvarTenBuong.DefaultSetting = @"";
				colvarTenBuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenBuong);
				
				TableSchema.TableColumn colvarIdKhoanoitru = new TableSchema.TableColumn(schema);
				colvarIdKhoanoitru.ColumnName = "id_khoanoitru";
				colvarIdKhoanoitru.DataType = DbType.Int16;
				colvarIdKhoanoitru.MaxLength = 0;
				colvarIdKhoanoitru.AutoIncrement = false;
				colvarIdKhoanoitru.IsNullable = false;
				colvarIdKhoanoitru.IsPrimaryKey = false;
				colvarIdKhoanoitru.IsForeignKey = false;
				colvarIdKhoanoitru.IsReadOnly = false;
				colvarIdKhoanoitru.DefaultSetting = @"";
				colvarIdKhoanoitru.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhoanoitru);
				
				TableSchema.TableColumn colvarDonGia = new TableSchema.TableColumn(schema);
				colvarDonGia.ColumnName = "don_gia";
				colvarDonGia.DataType = DbType.Int32;
				colvarDonGia.MaxLength = 0;
				colvarDonGia.AutoIncrement = false;
				colvarDonGia.IsNullable = false;
				colvarDonGia.IsPrimaryKey = false;
				colvarDonGia.IsForeignKey = false;
				colvarDonGia.IsReadOnly = false;
				colvarDonGia.DefaultSetting = @"";
				colvarDonGia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDonGia);
				
				TableSchema.TableColumn colvarMotaThem = new TableSchema.TableColumn(schema);
				colvarMotaThem.ColumnName = "mota_them";
				colvarMotaThem.DataType = DbType.String;
				colvarMotaThem.MaxLength = 255;
				colvarMotaThem.AutoIncrement = false;
				colvarMotaThem.IsNullable = true;
				colvarMotaThem.IsPrimaryKey = false;
				colvarMotaThem.IsForeignKey = false;
				colvarMotaThem.IsReadOnly = false;
				colvarMotaThem.DefaultSetting = @"";
				colvarMotaThem.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMotaThem);
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "trang_thai";
				colvarTrangThai.DataType = DbType.Byte;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = true;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				colvarTrangThai.DefaultSetting = @"";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
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
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("noitru_dmuc_buong",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdBuong")]
		[Bindable(true)]
		public short IdBuong 
		{
			get { return GetColumnValue<short>(Columns.IdBuong); }
			set { SetColumnValue(Columns.IdBuong, value); }
		}
		  
		[XmlAttribute("MaBuong")]
		[Bindable(true)]
		public string MaBuong 
		{
			get { return GetColumnValue<string>(Columns.MaBuong); }
			set { SetColumnValue(Columns.MaBuong, value); }
		}
		  
		[XmlAttribute("TenBuong")]
		[Bindable(true)]
		public string TenBuong 
		{
			get { return GetColumnValue<string>(Columns.TenBuong); }
			set { SetColumnValue(Columns.TenBuong, value); }
		}
		  
		[XmlAttribute("IdKhoanoitru")]
		[Bindable(true)]
		public short IdKhoanoitru 
		{
			get { return GetColumnValue<short>(Columns.IdKhoanoitru); }
			set { SetColumnValue(Columns.IdKhoanoitru, value); }
		}
		  
		[XmlAttribute("DonGia")]
		[Bindable(true)]
		public int DonGia 
		{
			get { return GetColumnValue<int>(Columns.DonGia); }
			set { SetColumnValue(Columns.DonGia, value); }
		}
		  
		[XmlAttribute("MotaThem")]
		[Bindable(true)]
		public string MotaThem 
		{
			get { return GetColumnValue<string>(Columns.MotaThem); }
			set { SetColumnValue(Columns.MotaThem, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public byte? TrangThai 
		{
			get { return GetColumnValue<byte?>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("SttHthi")]
		[Bindable(true)]
		public short? SttHthi 
		{
			get { return GetColumnValue<short?>(Columns.SttHthi); }
			set { SetColumnValue(Columns.SttHthi, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaBuong,string varTenBuong,short varIdKhoanoitru,int varDonGia,string varMotaThem,byte? varTrangThai,short? varSttHthi)
		{
			NoitruDmucBuong item = new NoitruDmucBuong();
			
			item.MaBuong = varMaBuong;
			
			item.TenBuong = varTenBuong;
			
			item.IdKhoanoitru = varIdKhoanoitru;
			
			item.DonGia = varDonGia;
			
			item.MotaThem = varMotaThem;
			
			item.TrangThai = varTrangThai;
			
			item.SttHthi = varSttHthi;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(short varIdBuong,string varMaBuong,string varTenBuong,short varIdKhoanoitru,int varDonGia,string varMotaThem,byte? varTrangThai,short? varSttHthi)
		{
			NoitruDmucBuong item = new NoitruDmucBuong();
			
				item.IdBuong = varIdBuong;
			
				item.MaBuong = varMaBuong;
			
				item.TenBuong = varTenBuong;
			
				item.IdKhoanoitru = varIdKhoanoitru;
			
				item.DonGia = varDonGia;
			
				item.MotaThem = varMotaThem;
			
				item.TrangThai = varTrangThai;
			
				item.SttHthi = varSttHthi;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdBuongColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaBuongColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TenBuongColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhoanoitruColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn DonGiaColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn MotaThemColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn SttHthiColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdBuong = @"id_buong";
			 public static string MaBuong = @"ma_buong";
			 public static string TenBuong = @"ten_buong";
			 public static string IdKhoanoitru = @"id_khoanoitru";
			 public static string DonGia = @"don_gia";
			 public static string MotaThem = @"mota_them";
			 public static string TrangThai = @"trang_thai";
			 public static string SttHthi = @"stt_hthi";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
