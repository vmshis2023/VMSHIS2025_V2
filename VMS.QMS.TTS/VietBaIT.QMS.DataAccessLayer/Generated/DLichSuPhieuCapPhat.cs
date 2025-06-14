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
	/// Strongly-typed collection for the DLichSuPhieuCapPhat class.
	/// </summary>
    [Serializable]
	public partial class DLichSuPhieuCapPhatCollection : ActiveList<DLichSuPhieuCapPhat, DLichSuPhieuCapPhatCollection>
	{	   
		public DLichSuPhieuCapPhatCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DLichSuPhieuCapPhatCollection</returns>
		public DLichSuPhieuCapPhatCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DLichSuPhieuCapPhat o = this[i];
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
	/// This is an ActiveRecord class which wraps the D_LichSu_PhieuCapPhat table.
	/// </summary>
	[Serializable]
	public partial class DLichSuPhieuCapPhat : ActiveRecord<DLichSuPhieuCapPhat>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DLichSuPhieuCapPhat()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DLichSuPhieuCapPhat(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DLichSuPhieuCapPhat(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DLichSuPhieuCapPhat(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("D_LichSu_PhieuCapPhat", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "Id";
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
				
				TableSchema.TableColumn colvarIdKhoXuat = new TableSchema.TableColumn(schema);
				colvarIdKhoXuat.ColumnName = "Id_Kho_Xuat";
				colvarIdKhoXuat.DataType = DbType.Int16;
				colvarIdKhoXuat.MaxLength = 0;
				colvarIdKhoXuat.AutoIncrement = false;
				colvarIdKhoXuat.IsNullable = true;
				colvarIdKhoXuat.IsPrimaryKey = false;
				colvarIdKhoXuat.IsForeignKey = false;
				colvarIdKhoXuat.IsReadOnly = false;
				colvarIdKhoXuat.DefaultSetting = @"";
				colvarIdKhoXuat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhoXuat);
				
				TableSchema.TableColumn colvarIdCapphat = new TableSchema.TableColumn(schema);
				colvarIdCapphat.ColumnName = "ID_CAPPHAT";
				colvarIdCapphat.DataType = DbType.Int32;
				colvarIdCapphat.MaxLength = 0;
				colvarIdCapphat.AutoIncrement = false;
				colvarIdCapphat.IsNullable = false;
				colvarIdCapphat.IsPrimaryKey = false;
				colvarIdCapphat.IsForeignKey = false;
				colvarIdCapphat.IsReadOnly = false;
				colvarIdCapphat.DefaultSetting = @"";
				colvarIdCapphat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdCapphat);
				
				TableSchema.TableColumn colvarSoLuong = new TableSchema.TableColumn(schema);
				colvarSoLuong.ColumnName = "SO_LUONG";
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
				colvarNgayHetHan.ColumnName = "NGAY_HET_HAN";
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
				
				TableSchema.TableColumn colvarIdThuoc = new TableSchema.TableColumn(schema);
				colvarIdThuoc.ColumnName = "ID_THUOC";
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
				
				TableSchema.TableColumn colvarDonGia = new TableSchema.TableColumn(schema);
				colvarDonGia.ColumnName = "DON_GIA";
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
				colvarGiaBan.ColumnName = "GIA_BAN";
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
				
				TableSchema.TableColumn colvarVat = new TableSchema.TableColumn(schema);
				colvarVat.ColumnName = "VAT";
				colvarVat.DataType = DbType.Decimal;
				colvarVat.MaxLength = 0;
				colvarVat.AutoIncrement = false;
				colvarVat.IsNullable = false;
				colvarVat.IsPrimaryKey = false;
				colvarVat.IsForeignKey = false;
				colvarVat.IsReadOnly = false;
				colvarVat.DefaultSetting = @"";
				colvarVat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVat);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("D_LichSu_PhieuCapPhat",schema);
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
		  
		[XmlAttribute("IdKhoXuat")]
		[Bindable(true)]
		public short? IdKhoXuat 
		{
			get { return GetColumnValue<short?>(Columns.IdKhoXuat); }
			set { SetColumnValue(Columns.IdKhoXuat, value); }
		}
		  
		[XmlAttribute("IdCapphat")]
		[Bindable(true)]
		public int IdCapphat 
		{
			get { return GetColumnValue<int>(Columns.IdCapphat); }
			set { SetColumnValue(Columns.IdCapphat, value); }
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
		  
		[XmlAttribute("IdThuoc")]
		[Bindable(true)]
		public int IdThuoc 
		{
			get { return GetColumnValue<int>(Columns.IdThuoc); }
			set { SetColumnValue(Columns.IdThuoc, value); }
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
		  
		[XmlAttribute("Vat")]
		[Bindable(true)]
		public decimal Vat 
		{
			get { return GetColumnValue<decimal>(Columns.Vat); }
			set { SetColumnValue(Columns.Vat, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(short? varIdKhoXuat,int varIdCapphat,int varSoLuong,DateTime varNgayHetHan,int varIdThuoc,decimal varDonGia,decimal varGiaBan,decimal varVat)
		{
			DLichSuPhieuCapPhat item = new DLichSuPhieuCapPhat();
			
			item.IdKhoXuat = varIdKhoXuat;
			
			item.IdCapphat = varIdCapphat;
			
			item.SoLuong = varSoLuong;
			
			item.NgayHetHan = varNgayHetHan;
			
			item.IdThuoc = varIdThuoc;
			
			item.DonGia = varDonGia;
			
			item.GiaBan = varGiaBan;
			
			item.Vat = varVat;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,short? varIdKhoXuat,int varIdCapphat,int varSoLuong,DateTime varNgayHetHan,int varIdThuoc,decimal varDonGia,decimal varGiaBan,decimal varVat)
		{
			DLichSuPhieuCapPhat item = new DLichSuPhieuCapPhat();
			
				item.Id = varId;
			
				item.IdKhoXuat = varIdKhoXuat;
			
				item.IdCapphat = varIdCapphat;
			
				item.SoLuong = varSoLuong;
			
				item.NgayHetHan = varNgayHetHan;
			
				item.IdThuoc = varIdThuoc;
			
				item.DonGia = varDonGia;
			
				item.GiaBan = varGiaBan;
			
				item.Vat = varVat;
			
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
        
        
        
        public static TableSchema.TableColumn IdKhoXuatColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IdCapphatColumn
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
        
        
        
        public static TableSchema.TableColumn IdThuocColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn DonGiaColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn GiaBanColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn VatColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string IdKhoXuat = @"Id_Kho_Xuat";
			 public static string IdCapphat = @"ID_CAPPHAT";
			 public static string SoLuong = @"SO_LUONG";
			 public static string NgayHetHan = @"NGAY_HET_HAN";
			 public static string IdThuoc = @"ID_THUOC";
			 public static string DonGia = @"DON_GIA";
			 public static string GiaBan = @"GIA_BAN";
			 public static string Vat = @"VAT";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
