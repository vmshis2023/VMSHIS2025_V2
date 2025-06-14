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
	/// Strongly-typed collection for the LRoom class.
	/// </summary>
    [Serializable]
	public partial class LRoomCollection : ActiveList<LRoom, LRoomCollection>
	{	   
		public LRoomCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>LRoomCollection</returns>
		public LRoomCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                LRoom o = this[i];
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
	/// This is an ActiveRecord class which wraps the L_Rooms table.
	/// </summary>
	[Serializable]
	public partial class LRoom : ActiveRecord<LRoom>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public LRoom()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public LRoom(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public LRoom(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public LRoom(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("L_Rooms", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarRoomId = new TableSchema.TableColumn(schema);
				colvarRoomId.ColumnName = "Room_ID";
				colvarRoomId.DataType = DbType.Int16;
				colvarRoomId.MaxLength = 0;
				colvarRoomId.AutoIncrement = true;
				colvarRoomId.IsNullable = false;
				colvarRoomId.IsPrimaryKey = true;
				colvarRoomId.IsForeignKey = false;
				colvarRoomId.IsReadOnly = false;
				colvarRoomId.DefaultSetting = @"";
				colvarRoomId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRoomId);
				
				TableSchema.TableColumn colvarRoomCode = new TableSchema.TableColumn(schema);
				colvarRoomCode.ColumnName = "Room_Code";
				colvarRoomCode.DataType = DbType.String;
				colvarRoomCode.MaxLength = 20;
				colvarRoomCode.AutoIncrement = false;
				colvarRoomCode.IsNullable = false;
				colvarRoomCode.IsPrimaryKey = false;
				colvarRoomCode.IsForeignKey = false;
				colvarRoomCode.IsReadOnly = false;
				colvarRoomCode.DefaultSetting = @"";
				colvarRoomCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRoomCode);
				
				TableSchema.TableColumn colvarRoomName = new TableSchema.TableColumn(schema);
				colvarRoomName.ColumnName = "Room_Name";
				colvarRoomName.DataType = DbType.String;
				colvarRoomName.MaxLength = 50;
				colvarRoomName.AutoIncrement = false;
				colvarRoomName.IsNullable = false;
				colvarRoomName.IsPrimaryKey = false;
				colvarRoomName.IsForeignKey = false;
				colvarRoomName.IsReadOnly = false;
				colvarRoomName.DefaultSetting = @"";
				colvarRoomName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRoomName);
				
				TableSchema.TableColumn colvarDepartmentId = new TableSchema.TableColumn(schema);
				colvarDepartmentId.ColumnName = "Department_ID";
				colvarDepartmentId.DataType = DbType.Int16;
				colvarDepartmentId.MaxLength = 0;
				colvarDepartmentId.AutoIncrement = false;
				colvarDepartmentId.IsNullable = false;
				colvarDepartmentId.IsPrimaryKey = false;
				colvarDepartmentId.IsForeignKey = false;
				colvarDepartmentId.IsReadOnly = false;
				colvarDepartmentId.DefaultSetting = @"";
				colvarDepartmentId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDepartmentId);
				
				TableSchema.TableColumn colvarRoomFee = new TableSchema.TableColumn(schema);
				colvarRoomFee.ColumnName = "Room_Fee";
				colvarRoomFee.DataType = DbType.Int32;
				colvarRoomFee.MaxLength = 0;
				colvarRoomFee.AutoIncrement = false;
				colvarRoomFee.IsNullable = false;
				colvarRoomFee.IsPrimaryKey = false;
				colvarRoomFee.IsForeignKey = false;
				colvarRoomFee.IsReadOnly = false;
				colvarRoomFee.DefaultSetting = @"";
				colvarRoomFee.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRoomFee);
				
				TableSchema.TableColumn colvarSDesc = new TableSchema.TableColumn(schema);
				colvarSDesc.ColumnName = "sDesc";
				colvarSDesc.DataType = DbType.String;
				colvarSDesc.MaxLength = 255;
				colvarSDesc.AutoIncrement = false;
				colvarSDesc.IsNullable = true;
				colvarSDesc.IsPrimaryKey = false;
				colvarSDesc.IsForeignKey = false;
				colvarSDesc.IsReadOnly = false;
				colvarSDesc.DefaultSetting = @"";
				colvarSDesc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSDesc);
				
				TableSchema.TableColumn colvarHienThi = new TableSchema.TableColumn(schema);
				colvarHienThi.ColumnName = "HIEN_THI";
				colvarHienThi.DataType = DbType.Byte;
				colvarHienThi.MaxLength = 0;
				colvarHienThi.AutoIncrement = false;
				colvarHienThi.IsNullable = true;
				colvarHienThi.IsPrimaryKey = false;
				colvarHienThi.IsForeignKey = false;
				colvarHienThi.IsReadOnly = false;
				colvarHienThi.DefaultSetting = @"";
				colvarHienThi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHienThi);
				
				TableSchema.TableColumn colvarStt = new TableSchema.TableColumn(schema);
				colvarStt.ColumnName = "STT";
				colvarStt.DataType = DbType.Int16;
				colvarStt.MaxLength = 0;
				colvarStt.AutoIncrement = false;
				colvarStt.IsNullable = true;
				colvarStt.IsPrimaryKey = false;
				colvarStt.IsForeignKey = false;
				colvarStt.IsReadOnly = false;
				colvarStt.DefaultSetting = @"";
				colvarStt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStt);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("L_Rooms",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("RoomId")]
		[Bindable(true)]
		public short RoomId 
		{
			get { return GetColumnValue<short>(Columns.RoomId); }
			set { SetColumnValue(Columns.RoomId, value); }
		}
		  
		[XmlAttribute("RoomCode")]
		[Bindable(true)]
		public string RoomCode 
		{
			get { return GetColumnValue<string>(Columns.RoomCode); }
			set { SetColumnValue(Columns.RoomCode, value); }
		}
		  
		[XmlAttribute("RoomName")]
		[Bindable(true)]
		public string RoomName 
		{
			get { return GetColumnValue<string>(Columns.RoomName); }
			set { SetColumnValue(Columns.RoomName, value); }
		}
		  
		[XmlAttribute("DepartmentId")]
		[Bindable(true)]
		public short DepartmentId 
		{
			get { return GetColumnValue<short>(Columns.DepartmentId); }
			set { SetColumnValue(Columns.DepartmentId, value); }
		}
		  
		[XmlAttribute("RoomFee")]
		[Bindable(true)]
		public int RoomFee 
		{
			get { return GetColumnValue<int>(Columns.RoomFee); }
			set { SetColumnValue(Columns.RoomFee, value); }
		}
		  
		[XmlAttribute("SDesc")]
		[Bindable(true)]
		public string SDesc 
		{
			get { return GetColumnValue<string>(Columns.SDesc); }
			set { SetColumnValue(Columns.SDesc, value); }
		}
		  
		[XmlAttribute("HienThi")]
		[Bindable(true)]
		public byte? HienThi 
		{
			get { return GetColumnValue<byte?>(Columns.HienThi); }
			set { SetColumnValue(Columns.HienThi, value); }
		}
		  
		[XmlAttribute("Stt")]
		[Bindable(true)]
		public short? Stt 
		{
			get { return GetColumnValue<short?>(Columns.Stt); }
			set { SetColumnValue(Columns.Stt, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varRoomCode,string varRoomName,short varDepartmentId,int varRoomFee,string varSDesc,byte? varHienThi,short? varStt)
		{
			LRoom item = new LRoom();
			
			item.RoomCode = varRoomCode;
			
			item.RoomName = varRoomName;
			
			item.DepartmentId = varDepartmentId;
			
			item.RoomFee = varRoomFee;
			
			item.SDesc = varSDesc;
			
			item.HienThi = varHienThi;
			
			item.Stt = varStt;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(short varRoomId,string varRoomCode,string varRoomName,short varDepartmentId,int varRoomFee,string varSDesc,byte? varHienThi,short? varStt)
		{
			LRoom item = new LRoom();
			
				item.RoomId = varRoomId;
			
				item.RoomCode = varRoomCode;
			
				item.RoomName = varRoomName;
			
				item.DepartmentId = varDepartmentId;
			
				item.RoomFee = varRoomFee;
			
				item.SDesc = varSDesc;
			
				item.HienThi = varHienThi;
			
				item.Stt = varStt;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn RoomIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn RoomCodeColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn RoomNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn DepartmentIdColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn RoomFeeColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn SDescColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn HienThiColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn SttColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string RoomId = @"Room_ID";
			 public static string RoomCode = @"Room_Code";
			 public static string RoomName = @"Room_Name";
			 public static string DepartmentId = @"Department_ID";
			 public static string RoomFee = @"Room_Fee";
			 public static string SDesc = @"sDesc";
			 public static string HienThi = @"HIEN_THI";
			 public static string Stt = @"STT";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
