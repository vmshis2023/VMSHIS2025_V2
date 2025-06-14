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
	/// Strongly-typed collection for the TblFiledinhkem class.
	/// </summary>
    [Serializable]
	public partial class TblFiledinhkemCollection : ActiveList<TblFiledinhkem, TblFiledinhkemCollection>
	{	   
		public TblFiledinhkemCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TblFiledinhkemCollection</returns>
		public TblFiledinhkemCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TblFiledinhkem o = this[i];
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
	/// This is an ActiveRecord class which wraps the tbl_filedinhkem table.
	/// </summary>
	[Serializable]
	public partial class TblFiledinhkem : ActiveRecord<TblFiledinhkem>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TblFiledinhkem()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TblFiledinhkem(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TblFiledinhkem(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TblFiledinhkem(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("tbl_filedinhkem", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "id";
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
				
				TableSchema.TableColumn colvarIdChidinh = new TableSchema.TableColumn(schema);
				colvarIdChidinh.ColumnName = "id_chidinh";
				colvarIdChidinh.DataType = DbType.Int64;
				colvarIdChidinh.MaxLength = 0;
				colvarIdChidinh.AutoIncrement = false;
				colvarIdChidinh.IsNullable = false;
				colvarIdChidinh.IsPrimaryKey = false;
				colvarIdChidinh.IsForeignKey = false;
				colvarIdChidinh.IsReadOnly = false;
				colvarIdChidinh.DefaultSetting = @"";
				colvarIdChidinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChidinh);
				
				TableSchema.TableColumn colvarFileData = new TableSchema.TableColumn(schema);
				colvarFileData.ColumnName = "file_data";
				colvarFileData.DataType = DbType.Binary;
				colvarFileData.MaxLength = 2147483647;
				colvarFileData.AutoIncrement = false;
				colvarFileData.IsNullable = false;
				colvarFileData.IsPrimaryKey = false;
				colvarFileData.IsForeignKey = false;
				colvarFileData.IsReadOnly = false;
				colvarFileData.DefaultSetting = @"";
				colvarFileData.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileData);
				
				TableSchema.TableColumn colvarFileName = new TableSchema.TableColumn(schema);
				colvarFileName.ColumnName = "file_name";
				colvarFileName.DataType = DbType.String;
				colvarFileName.MaxLength = 255;
				colvarFileName.AutoIncrement = false;
				colvarFileName.IsNullable = false;
				colvarFileName.IsPrimaryKey = false;
				colvarFileName.IsForeignKey = false;
				colvarFileName.IsReadOnly = false;
				colvarFileName.DefaultSetting = @"";
				colvarFileName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileName);
				
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
				
				TableSchema.TableColumn colvarIdBenhnhan = new TableSchema.TableColumn(schema);
				colvarIdBenhnhan.ColumnName = "id_benhnhan";
				colvarIdBenhnhan.DataType = DbType.Int64;
				colvarIdBenhnhan.MaxLength = 0;
				colvarIdBenhnhan.AutoIncrement = false;
				colvarIdBenhnhan.IsNullable = true;
				colvarIdBenhnhan.IsPrimaryKey = false;
				colvarIdBenhnhan.IsForeignKey = false;
				colvarIdBenhnhan.IsReadOnly = false;
				colvarIdBenhnhan.DefaultSetting = @"";
				colvarIdBenhnhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBenhnhan);
				
				TableSchema.TableColumn colvarMaLuotkham = new TableSchema.TableColumn(schema);
				colvarMaLuotkham.ColumnName = "ma_luotkham";
				colvarMaLuotkham.DataType = DbType.String;
				colvarMaLuotkham.MaxLength = 10;
				colvarMaLuotkham.AutoIncrement = false;
				colvarMaLuotkham.IsNullable = true;
				colvarMaLuotkham.IsPrimaryKey = false;
				colvarMaLuotkham.IsForeignKey = false;
				colvarMaLuotkham.IsReadOnly = false;
				colvarMaLuotkham.DefaultSetting = @"";
				colvarMaLuotkham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaLuotkham);
				
				TableSchema.TableColumn colvarIdCongkham = new TableSchema.TableColumn(schema);
				colvarIdCongkham.ColumnName = "id_congkham";
				colvarIdCongkham.DataType = DbType.Int64;
				colvarIdCongkham.MaxLength = 0;
				colvarIdCongkham.AutoIncrement = false;
				colvarIdCongkham.IsNullable = true;
				colvarIdCongkham.IsPrimaryKey = false;
				colvarIdCongkham.IsForeignKey = false;
				colvarIdCongkham.IsReadOnly = false;
				colvarIdCongkham.DefaultSetting = @"";
				colvarIdCongkham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdCongkham);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("tbl_filedinhkem",schema);
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
		  
		[XmlAttribute("IdChidinh")]
		[Bindable(true)]
		public long IdChidinh 
		{
			get { return GetColumnValue<long>(Columns.IdChidinh); }
			set { SetColumnValue(Columns.IdChidinh, value); }
		}
		  
		[XmlAttribute("FileData")]
		[Bindable(true)]
		public byte[] FileData 
		{
			get { return GetColumnValue<byte[]>(Columns.FileData); }
			set { SetColumnValue(Columns.FileData, value); }
		}
		  
		[XmlAttribute("FileName")]
		[Bindable(true)]
		public string FileName 
		{
			get { return GetColumnValue<string>(Columns.FileName); }
			set { SetColumnValue(Columns.FileName, value); }
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
		  
		[XmlAttribute("IdBenhnhan")]
		[Bindable(true)]
		public long? IdBenhnhan 
		{
			get { return GetColumnValue<long?>(Columns.IdBenhnhan); }
			set { SetColumnValue(Columns.IdBenhnhan, value); }
		}
		  
		[XmlAttribute("MaLuotkham")]
		[Bindable(true)]
		public string MaLuotkham 
		{
			get { return GetColumnValue<string>(Columns.MaLuotkham); }
			set { SetColumnValue(Columns.MaLuotkham, value); }
		}
		  
		[XmlAttribute("IdCongkham")]
		[Bindable(true)]
		public long? IdCongkham 
		{
			get { return GetColumnValue<long?>(Columns.IdCongkham); }
			set { SetColumnValue(Columns.IdCongkham, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varIdChidinh,byte[] varFileData,string varFileName,string varNguoiTao,DateTime varNgayTao,long? varIdBenhnhan,string varMaLuotkham,long? varIdCongkham)
		{
			TblFiledinhkem item = new TblFiledinhkem();
			
			item.IdChidinh = varIdChidinh;
			
			item.FileData = varFileData;
			
			item.FileName = varFileName;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgayTao = varNgayTao;
			
			item.IdBenhnhan = varIdBenhnhan;
			
			item.MaLuotkham = varMaLuotkham;
			
			item.IdCongkham = varIdCongkham;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,long varIdChidinh,byte[] varFileData,string varFileName,string varNguoiTao,DateTime varNgayTao,long? varIdBenhnhan,string varMaLuotkham,long? varIdCongkham)
		{
			TblFiledinhkem item = new TblFiledinhkem();
			
				item.Id = varId;
			
				item.IdChidinh = varIdChidinh;
			
				item.FileData = varFileData;
			
				item.FileName = varFileName;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgayTao = varNgayTao;
			
				item.IdBenhnhan = varIdBenhnhan;
			
				item.MaLuotkham = varMaLuotkham;
			
				item.IdCongkham = varIdCongkham;
			
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
        
        
        
        public static TableSchema.TableColumn IdChidinhColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn FileDataColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn FileNameColumn
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
        
        
        
        public static TableSchema.TableColumn IdBenhnhanColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn MaLuotkhamColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn IdCongkhamColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"id";
			 public static string IdChidinh = @"id_chidinh";
			 public static string FileData = @"file_data";
			 public static string FileName = @"file_name";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string NgayTao = @"ngay_tao";
			 public static string IdBenhnhan = @"id_benhnhan";
			 public static string MaLuotkham = @"ma_luotkham";
			 public static string IdCongkham = @"id_congkham";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
