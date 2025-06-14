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
    /// Controller class for L_Departments
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LDepartmentController
    {
        // Preload our schema..
        LDepartment thisSchemaLoad = new LDepartment();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public LDepartmentCollection FetchAll()
        {
            LDepartmentCollection coll = new LDepartmentCollection();
            Query qry = new Query(LDepartment.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LDepartmentCollection FetchByID(object DepartmentId)
        {
            LDepartmentCollection coll = new LDepartmentCollection().Where("Department_ID", DepartmentId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LDepartmentCollection FetchByQuery(Query qry)
        {
            LDepartmentCollection coll = new LDepartmentCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object DepartmentId)
        {
            return (LDepartment.Delete(DepartmentId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object DepartmentId)
        {
            return (LDepartment.Destroy(DepartmentId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string DepartmentCode,string DepartmentName,short? ParentId,byte Speciality,short IntOrder,string SDesc,int? DeptType,decimal? DeptFee,string LoaiTamUng,decimal? TienTamUng,string KieuKphong,int? KhoaCapcuu,string LoaiKhoa,int? UnitId,string NguoiTao,DateTime? NgayTao,string NguoiSua,DateTime? NgaySua,string PhongThien,string MaPhongStt,byte? HienThi)
	    {
		    LDepartment item = new LDepartment();
		    
            item.DepartmentCode = DepartmentCode;
            
            item.DepartmentName = DepartmentName;
            
            item.ParentId = ParentId;
            
            item.Speciality = Speciality;
            
            item.IntOrder = IntOrder;
            
            item.SDesc = SDesc;
            
            item.DeptType = DeptType;
            
            item.DeptFee = DeptFee;
            
            item.LoaiTamUng = LoaiTamUng;
            
            item.TienTamUng = TienTamUng;
            
            item.KieuKphong = KieuKphong;
            
            item.KhoaCapcuu = KhoaCapcuu;
            
            item.LoaiKhoa = LoaiKhoa;
            
            item.UnitId = UnitId;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
            item.PhongThien = PhongThien;
            
            item.MaPhongStt = MaPhongStt;
            
            item.HienThi = HienThi;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(short DepartmentId,string DepartmentCode,string DepartmentName,short? ParentId,byte Speciality,short IntOrder,string SDesc,int? DeptType,decimal? DeptFee,string LoaiTamUng,decimal? TienTamUng,string KieuKphong,int? KhoaCapcuu,string LoaiKhoa,int? UnitId,string NguoiTao,DateTime? NgayTao,string NguoiSua,DateTime? NgaySua,string PhongThien,string MaPhongStt,byte? HienThi)
	    {
		    LDepartment item = new LDepartment();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.DepartmentId = DepartmentId;
				
			item.DepartmentCode = DepartmentCode;
				
			item.DepartmentName = DepartmentName;
				
			item.ParentId = ParentId;
				
			item.Speciality = Speciality;
				
			item.IntOrder = IntOrder;
				
			item.SDesc = SDesc;
				
			item.DeptType = DeptType;
				
			item.DeptFee = DeptFee;
				
			item.LoaiTamUng = LoaiTamUng;
				
			item.TienTamUng = TienTamUng;
				
			item.KieuKphong = KieuKphong;
				
			item.KhoaCapcuu = KhoaCapcuu;
				
			item.LoaiKhoa = LoaiKhoa;
				
			item.UnitId = UnitId;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
			item.PhongThien = PhongThien;
				
			item.MaPhongStt = MaPhongStt;
				
			item.HienThi = HienThi;
				
	        item.Save(UserName);
	    }
    }
}
