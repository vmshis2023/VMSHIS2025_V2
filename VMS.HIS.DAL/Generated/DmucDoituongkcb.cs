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
	/// Strongly-typed collection for the DmucDoituongkcb class.
	/// </summary>
    [Serializable]
	public partial class DmucDoituongkcbCollection : ActiveList<DmucDoituongkcb, DmucDoituongkcbCollection>
	{	   
		public DmucDoituongkcbCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DmucDoituongkcbCollection</returns>
		public DmucDoituongkcbCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DmucDoituongkcb o = this[i];
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
	/// This is an ActiveRecord class which wraps the dmuc_doituongkcb table.
	/// </summary>
	[Serializable]
	public partial class DmucDoituongkcb : ActiveRecord<DmucDoituongkcb>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DmucDoituongkcb()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DmucDoituongkcb(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DmucDoituongkcb(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DmucDoituongkcb(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("dmuc_doituongkcb", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdDoituongKcb = new TableSchema.TableColumn(schema);
				colvarIdDoituongKcb.ColumnName = "id_doituong_kcb";
				colvarIdDoituongKcb.DataType = DbType.Int16;
				colvarIdDoituongKcb.MaxLength = 0;
				colvarIdDoituongKcb.AutoIncrement = true;
				colvarIdDoituongKcb.IsNullable = false;
				colvarIdDoituongKcb.IsPrimaryKey = true;
				colvarIdDoituongKcb.IsForeignKey = false;
				colvarIdDoituongKcb.IsReadOnly = false;
				colvarIdDoituongKcb.DefaultSetting = @"";
				colvarIdDoituongKcb.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdDoituongKcb);
				
				TableSchema.TableColumn colvarTenDoituongKcb = new TableSchema.TableColumn(schema);
				colvarTenDoituongKcb.ColumnName = "ten_doituong_kcb";
				colvarTenDoituongKcb.DataType = DbType.String;
				colvarTenDoituongKcb.MaxLength = 100;
				colvarTenDoituongKcb.AutoIncrement = false;
				colvarTenDoituongKcb.IsNullable = false;
				colvarTenDoituongKcb.IsPrimaryKey = false;
				colvarTenDoituongKcb.IsForeignKey = false;
				colvarTenDoituongKcb.IsReadOnly = false;
				colvarTenDoituongKcb.DefaultSetting = @"";
				colvarTenDoituongKcb.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenDoituongKcb);
				
				TableSchema.TableColumn colvarSttHthi = new TableSchema.TableColumn(schema);
				colvarSttHthi.ColumnName = "stt_hthi";
				colvarSttHthi.DataType = DbType.Int16;
				colvarSttHthi.MaxLength = 0;
				colvarSttHthi.AutoIncrement = false;
				colvarSttHthi.IsNullable = false;
				colvarSttHthi.IsPrimaryKey = false;
				colvarSttHthi.IsForeignKey = false;
				colvarSttHthi.IsReadOnly = false;
				colvarSttHthi.DefaultSetting = @"";
				colvarSttHthi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSttHthi);
				
				TableSchema.TableColumn colvarPhantramDungtuyen = new TableSchema.TableColumn(schema);
				colvarPhantramDungtuyen.ColumnName = "phantram_dungtuyen";
				colvarPhantramDungtuyen.DataType = DbType.Decimal;
				colvarPhantramDungtuyen.MaxLength = 0;
				colvarPhantramDungtuyen.AutoIncrement = false;
				colvarPhantramDungtuyen.IsNullable = false;
				colvarPhantramDungtuyen.IsPrimaryKey = false;
				colvarPhantramDungtuyen.IsForeignKey = false;
				colvarPhantramDungtuyen.IsReadOnly = false;
				colvarPhantramDungtuyen.DefaultSetting = @"";
				colvarPhantramDungtuyen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhantramDungtuyen);
				
				TableSchema.TableColumn colvarPhantramTraituyen = new TableSchema.TableColumn(schema);
				colvarPhantramTraituyen.ColumnName = "phantram_traituyen";
				colvarPhantramTraituyen.DataType = DbType.Decimal;
				colvarPhantramTraituyen.MaxLength = 0;
				colvarPhantramTraituyen.AutoIncrement = false;
				colvarPhantramTraituyen.IsNullable = true;
				colvarPhantramTraituyen.IsPrimaryKey = false;
				colvarPhantramTraituyen.IsForeignKey = false;
				colvarPhantramTraituyen.IsReadOnly = false;
				colvarPhantramTraituyen.DefaultSetting = @"";
				colvarPhantramTraituyen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhantramTraituyen);
				
				TableSchema.TableColumn colvarIdLoaidoituongKcb = new TableSchema.TableColumn(schema);
				colvarIdLoaidoituongKcb.ColumnName = "id_loaidoituong_kcb";
				colvarIdLoaidoituongKcb.DataType = DbType.Byte;
				colvarIdLoaidoituongKcb.MaxLength = 0;
				colvarIdLoaidoituongKcb.AutoIncrement = false;
				colvarIdLoaidoituongKcb.IsNullable = false;
				colvarIdLoaidoituongKcb.IsPrimaryKey = false;
				colvarIdLoaidoituongKcb.IsForeignKey = false;
				colvarIdLoaidoituongKcb.IsReadOnly = false;
				
						colvarIdLoaidoituongKcb.DefaultSetting = @"((0))";
				colvarIdLoaidoituongKcb.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdLoaidoituongKcb);
				
				TableSchema.TableColumn colvarMaDoituongKcb = new TableSchema.TableColumn(schema);
				colvarMaDoituongKcb.ColumnName = "ma_doituong_kcb";
				colvarMaDoituongKcb.DataType = DbType.String;
				colvarMaDoituongKcb.MaxLength = 20;
				colvarMaDoituongKcb.AutoIncrement = false;
				colvarMaDoituongKcb.IsNullable = true;
				colvarMaDoituongKcb.IsPrimaryKey = false;
				colvarMaDoituongKcb.IsForeignKey = false;
				colvarMaDoituongKcb.IsReadOnly = false;
				colvarMaDoituongKcb.DefaultSetting = @"";
				colvarMaDoituongKcb.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaDoituongKcb);
				
				TableSchema.TableColumn colvarGiathuocQuanhe = new TableSchema.TableColumn(schema);
				colvarGiathuocQuanhe.ColumnName = "giathuoc_quanhe";
				colvarGiathuocQuanhe.DataType = DbType.Byte;
				colvarGiathuocQuanhe.MaxLength = 0;
				colvarGiathuocQuanhe.AutoIncrement = false;
				colvarGiathuocQuanhe.IsNullable = true;
				colvarGiathuocQuanhe.IsPrimaryKey = false;
				colvarGiathuocQuanhe.IsForeignKey = false;
				colvarGiathuocQuanhe.IsReadOnly = false;
				colvarGiathuocQuanhe.DefaultSetting = @"";
				colvarGiathuocQuanhe.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGiathuocQuanhe);
				
				TableSchema.TableColumn colvarThanhtoanTruockhikham = new TableSchema.TableColumn(schema);
				colvarThanhtoanTruockhikham.ColumnName = "thanhtoan_truockhikham";
				colvarThanhtoanTruockhikham.DataType = DbType.Byte;
				colvarThanhtoanTruockhikham.MaxLength = 0;
				colvarThanhtoanTruockhikham.AutoIncrement = false;
				colvarThanhtoanTruockhikham.IsNullable = true;
				colvarThanhtoanTruockhikham.IsPrimaryKey = false;
				colvarThanhtoanTruockhikham.IsForeignKey = false;
				colvarThanhtoanTruockhikham.IsReadOnly = false;
				colvarThanhtoanTruockhikham.DefaultSetting = @"";
				colvarThanhtoanTruockhikham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarThanhtoanTruockhikham);
				
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
				
				TableSchema.TableColumn colvarTudongThanhtoan = new TableSchema.TableColumn(schema);
				colvarTudongThanhtoan.ColumnName = "tudong_thanhtoan";
				colvarTudongThanhtoan.DataType = DbType.Byte;
				colvarTudongThanhtoan.MaxLength = 0;
				colvarTudongThanhtoan.AutoIncrement = false;
				colvarTudongThanhtoan.IsNullable = true;
				colvarTudongThanhtoan.IsPrimaryKey = false;
				colvarTudongThanhtoan.IsForeignKey = false;
				colvarTudongThanhtoan.IsReadOnly = false;
				colvarTudongThanhtoan.DefaultSetting = @"";
				colvarTudongThanhtoan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTudongThanhtoan);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("dmuc_doituongkcb",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdDoituongKcb")]
		[Bindable(true)]
		public short IdDoituongKcb 
		{
			get { return GetColumnValue<short>(Columns.IdDoituongKcb); }
			set { SetColumnValue(Columns.IdDoituongKcb, value); }
		}
		  
		[XmlAttribute("TenDoituongKcb")]
		[Bindable(true)]
		public string TenDoituongKcb 
		{
			get { return GetColumnValue<string>(Columns.TenDoituongKcb); }
			set { SetColumnValue(Columns.TenDoituongKcb, value); }
		}
		  
		[XmlAttribute("SttHthi")]
		[Bindable(true)]
		public short SttHthi 
		{
			get { return GetColumnValue<short>(Columns.SttHthi); }
			set { SetColumnValue(Columns.SttHthi, value); }
		}
		  
		[XmlAttribute("PhantramDungtuyen")]
		[Bindable(true)]
		public decimal PhantramDungtuyen 
		{
			get { return GetColumnValue<decimal>(Columns.PhantramDungtuyen); }
			set { SetColumnValue(Columns.PhantramDungtuyen, value); }
		}
		  
		[XmlAttribute("PhantramTraituyen")]
		[Bindable(true)]
		public decimal? PhantramTraituyen 
		{
			get { return GetColumnValue<decimal?>(Columns.PhantramTraituyen); }
			set { SetColumnValue(Columns.PhantramTraituyen, value); }
		}
		  
		[XmlAttribute("IdLoaidoituongKcb")]
		[Bindable(true)]
		public byte IdLoaidoituongKcb 
		{
			get { return GetColumnValue<byte>(Columns.IdLoaidoituongKcb); }
			set { SetColumnValue(Columns.IdLoaidoituongKcb, value); }
		}
		  
		[XmlAttribute("MaDoituongKcb")]
		[Bindable(true)]
		public string MaDoituongKcb 
		{
			get { return GetColumnValue<string>(Columns.MaDoituongKcb); }
			set { SetColumnValue(Columns.MaDoituongKcb, value); }
		}
		  
		[XmlAttribute("GiathuocQuanhe")]
		[Bindable(true)]
		public byte? GiathuocQuanhe 
		{
			get { return GetColumnValue<byte?>(Columns.GiathuocQuanhe); }
			set { SetColumnValue(Columns.GiathuocQuanhe, value); }
		}
		  
		[XmlAttribute("ThanhtoanTruockhikham")]
		[Bindable(true)]
		public byte? ThanhtoanTruockhikham 
		{
			get { return GetColumnValue<byte?>(Columns.ThanhtoanTruockhikham); }
			set { SetColumnValue(Columns.ThanhtoanTruockhikham, value); }
		}
		  
		[XmlAttribute("MotaThem")]
		[Bindable(true)]
		public string MotaThem 
		{
			get { return GetColumnValue<string>(Columns.MotaThem); }
			set { SetColumnValue(Columns.MotaThem, value); }
		}
		  
		[XmlAttribute("TudongThanhtoan")]
		[Bindable(true)]
		public byte? TudongThanhtoan 
		{
			get { return GetColumnValue<byte?>(Columns.TudongThanhtoan); }
			set { SetColumnValue(Columns.TudongThanhtoan, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varTenDoituongKcb,short varSttHthi,decimal varPhantramDungtuyen,decimal? varPhantramTraituyen,byte varIdLoaidoituongKcb,string varMaDoituongKcb,byte? varGiathuocQuanhe,byte? varThanhtoanTruockhikham,string varMotaThem,byte? varTudongThanhtoan)
		{
			DmucDoituongkcb item = new DmucDoituongkcb();
			
			item.TenDoituongKcb = varTenDoituongKcb;
			
			item.SttHthi = varSttHthi;
			
			item.PhantramDungtuyen = varPhantramDungtuyen;
			
			item.PhantramTraituyen = varPhantramTraituyen;
			
			item.IdLoaidoituongKcb = varIdLoaidoituongKcb;
			
			item.MaDoituongKcb = varMaDoituongKcb;
			
			item.GiathuocQuanhe = varGiathuocQuanhe;
			
			item.ThanhtoanTruockhikham = varThanhtoanTruockhikham;
			
			item.MotaThem = varMotaThem;
			
			item.TudongThanhtoan = varTudongThanhtoan;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(short varIdDoituongKcb,string varTenDoituongKcb,short varSttHthi,decimal varPhantramDungtuyen,decimal? varPhantramTraituyen,byte varIdLoaidoituongKcb,string varMaDoituongKcb,byte? varGiathuocQuanhe,byte? varThanhtoanTruockhikham,string varMotaThem,byte? varTudongThanhtoan)
		{
			DmucDoituongkcb item = new DmucDoituongkcb();
			
				item.IdDoituongKcb = varIdDoituongKcb;
			
				item.TenDoituongKcb = varTenDoituongKcb;
			
				item.SttHthi = varSttHthi;
			
				item.PhantramDungtuyen = varPhantramDungtuyen;
			
				item.PhantramTraituyen = varPhantramTraituyen;
			
				item.IdLoaidoituongKcb = varIdLoaidoituongKcb;
			
				item.MaDoituongKcb = varMaDoituongKcb;
			
				item.GiathuocQuanhe = varGiathuocQuanhe;
			
				item.ThanhtoanTruockhikham = varThanhtoanTruockhikham;
			
				item.MotaThem = varMotaThem;
			
				item.TudongThanhtoan = varTudongThanhtoan;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdDoituongKcbColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn TenDoituongKcbColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn SttHthiColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn PhantramDungtuyenColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn PhantramTraituyenColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn IdLoaidoituongKcbColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn MaDoituongKcbColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn GiathuocQuanheColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn ThanhtoanTruockhikhamColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn MotaThemColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn TudongThanhtoanColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdDoituongKcb = @"id_doituong_kcb";
			 public static string TenDoituongKcb = @"ten_doituong_kcb";
			 public static string SttHthi = @"stt_hthi";
			 public static string PhantramDungtuyen = @"phantram_dungtuyen";
			 public static string PhantramTraituyen = @"phantram_traituyen";
			 public static string IdLoaidoituongKcb = @"id_loaidoituong_kcb";
			 public static string MaDoituongKcb = @"ma_doituong_kcb";
			 public static string GiathuocQuanhe = @"giathuoc_quanhe";
			 public static string ThanhtoanTruockhikham = @"thanhtoan_truockhikham";
			 public static string MotaThem = @"mota_them";
			 public static string TudongThanhtoan = @"tudong_thanhtoan";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
