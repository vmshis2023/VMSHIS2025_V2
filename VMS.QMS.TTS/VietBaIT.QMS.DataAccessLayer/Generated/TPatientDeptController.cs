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
    /// Controller class for T_Patient_Dept
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TPatientDeptController
    {
        // Preload our schema..
        TPatientDept thisSchemaLoad = new TPatientDept();
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
        public TPatientDeptCollection FetchAll()
        {
            TPatientDeptCollection coll = new TPatientDeptCollection();
            Query qry = new Query(TPatientDept.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TPatientDeptCollection FetchByID(object PatientDeptId)
        {
            TPatientDeptCollection coll = new TPatientDeptCollection().Where("PatientDept_ID", PatientDeptId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TPatientDeptCollection FetchByQuery(Query qry)
        {
            TPatientDeptCollection coll = new TPatientDeptCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object PatientDeptId)
        {
            return (TPatientDept.Delete(PatientDeptId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object PatientDeptId)
        {
            return (TPatientDept.Destroy(PatientDeptId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(long PatientId,int? FromDepartmentId,short DepartmentId,string PatientCode,short? RoomId,short? BedId,byte? BedType,byte Status,DateTime RegDate,DateTime? EndDate,short? DoctorAssignId,string CreateBy,DateTime CreateDate,string ModifyBy,DateTime? ModifyDate,int? IsCancel,DateTime? PaymentDate,int? PaymentStatus,int? BhytStatus,int? NoiTru,int? Quantity,decimal? Price,int? IsPayment,int? PaymentId,int? IdKhoaRavien,int? TrangThaiRavien,decimal? GiaBhytCt,decimal? GiaBnct,int? IdGoiDvu,int? TrongGoi,int? IdYtaPgiuong,DateTime? NgayPgiuong,string NguoiPgiuong,decimal? SurchargePrice,int? XacNhan,string IpMacSua,string IpMacTao,string IpMaySua,string IpMayTao,string TenHienThi,decimal? GiaBgGoc,string KieuThue,int? PkPatientdeptId,int? Id,int? IdBenhLy,int? IdLoaiBg,decimal? PhuThuNgoaigoi,int? Stt,short? IdKhoaDtriHo)
	    {
		    TPatientDept item = new TPatientDept();
		    
            item.PatientId = PatientId;
            
            item.FromDepartmentId = FromDepartmentId;
            
            item.DepartmentId = DepartmentId;
            
            item.PatientCode = PatientCode;
            
            item.RoomId = RoomId;
            
            item.BedId = BedId;
            
            item.BedType = BedType;
            
            item.Status = Status;
            
            item.RegDate = RegDate;
            
            item.EndDate = EndDate;
            
            item.DoctorAssignId = DoctorAssignId;
            
            item.CreateBy = CreateBy;
            
            item.CreateDate = CreateDate;
            
            item.ModifyBy = ModifyBy;
            
            item.ModifyDate = ModifyDate;
            
            item.IsCancel = IsCancel;
            
            item.PaymentDate = PaymentDate;
            
            item.PaymentStatus = PaymentStatus;
            
            item.BhytStatus = BhytStatus;
            
            item.NoiTru = NoiTru;
            
            item.Quantity = Quantity;
            
            item.Price = Price;
            
            item.IsPayment = IsPayment;
            
            item.PaymentId = PaymentId;
            
            item.IdKhoaRavien = IdKhoaRavien;
            
            item.TrangThaiRavien = TrangThaiRavien;
            
            item.GiaBhytCt = GiaBhytCt;
            
            item.GiaBnct = GiaBnct;
            
            item.IdGoiDvu = IdGoiDvu;
            
            item.TrongGoi = TrongGoi;
            
            item.IdYtaPgiuong = IdYtaPgiuong;
            
            item.NgayPgiuong = NgayPgiuong;
            
            item.NguoiPgiuong = NguoiPgiuong;
            
            item.SurchargePrice = SurchargePrice;
            
            item.XacNhan = XacNhan;
            
            item.IpMacSua = IpMacSua;
            
            item.IpMacTao = IpMacTao;
            
            item.IpMaySua = IpMaySua;
            
            item.IpMayTao = IpMayTao;
            
            item.TenHienThi = TenHienThi;
            
            item.GiaBgGoc = GiaBgGoc;
            
            item.KieuThue = KieuThue;
            
            item.PkPatientdeptId = PkPatientdeptId;
            
            item.Id = Id;
            
            item.IdBenhLy = IdBenhLy;
            
            item.IdLoaiBg = IdLoaiBg;
            
            item.PhuThuNgoaigoi = PhuThuNgoaigoi;
            
            item.Stt = Stt;
            
            item.IdKhoaDtriHo = IdKhoaDtriHo;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long PatientDeptId,long PatientId,int? FromDepartmentId,short DepartmentId,string PatientCode,short? RoomId,short? BedId,byte? BedType,byte Status,DateTime RegDate,DateTime? EndDate,short? DoctorAssignId,string CreateBy,DateTime CreateDate,string ModifyBy,DateTime? ModifyDate,int? IsCancel,DateTime? PaymentDate,int? PaymentStatus,int? BhytStatus,int? NoiTru,int? Quantity,decimal? Price,int? IsPayment,int? PaymentId,int? IdKhoaRavien,int? TrangThaiRavien,decimal? GiaBhytCt,decimal? GiaBnct,int? IdGoiDvu,int? TrongGoi,int? IdYtaPgiuong,DateTime? NgayPgiuong,string NguoiPgiuong,decimal? SurchargePrice,int? XacNhan,string IpMacSua,string IpMacTao,string IpMaySua,string IpMayTao,string TenHienThi,decimal? GiaBgGoc,string KieuThue,int? PkPatientdeptId,int? Id,int? IdBenhLy,int? IdLoaiBg,decimal? PhuThuNgoaigoi,int? Stt,short? IdKhoaDtriHo)
	    {
		    TPatientDept item = new TPatientDept();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.PatientDeptId = PatientDeptId;
				
			item.PatientId = PatientId;
				
			item.FromDepartmentId = FromDepartmentId;
				
			item.DepartmentId = DepartmentId;
				
			item.PatientCode = PatientCode;
				
			item.RoomId = RoomId;
				
			item.BedId = BedId;
				
			item.BedType = BedType;
				
			item.Status = Status;
				
			item.RegDate = RegDate;
				
			item.EndDate = EndDate;
				
			item.DoctorAssignId = DoctorAssignId;
				
			item.CreateBy = CreateBy;
				
			item.CreateDate = CreateDate;
				
			item.ModifyBy = ModifyBy;
				
			item.ModifyDate = ModifyDate;
				
			item.IsCancel = IsCancel;
				
			item.PaymentDate = PaymentDate;
				
			item.PaymentStatus = PaymentStatus;
				
			item.BhytStatus = BhytStatus;
				
			item.NoiTru = NoiTru;
				
			item.Quantity = Quantity;
				
			item.Price = Price;
				
			item.IsPayment = IsPayment;
				
			item.PaymentId = PaymentId;
				
			item.IdKhoaRavien = IdKhoaRavien;
				
			item.TrangThaiRavien = TrangThaiRavien;
				
			item.GiaBhytCt = GiaBhytCt;
				
			item.GiaBnct = GiaBnct;
				
			item.IdGoiDvu = IdGoiDvu;
				
			item.TrongGoi = TrongGoi;
				
			item.IdYtaPgiuong = IdYtaPgiuong;
				
			item.NgayPgiuong = NgayPgiuong;
				
			item.NguoiPgiuong = NguoiPgiuong;
				
			item.SurchargePrice = SurchargePrice;
				
			item.XacNhan = XacNhan;
				
			item.IpMacSua = IpMacSua;
				
			item.IpMacTao = IpMacTao;
				
			item.IpMaySua = IpMaySua;
				
			item.IpMayTao = IpMayTao;
				
			item.TenHienThi = TenHienThi;
				
			item.GiaBgGoc = GiaBgGoc;
				
			item.KieuThue = KieuThue;
				
			item.PkPatientdeptId = PkPatientdeptId;
				
			item.Id = Id;
				
			item.IdBenhLy = IdBenhLy;
				
			item.IdLoaiBg = IdLoaiBg;
				
			item.PhuThuNgoaigoi = PhuThuNgoaigoi;
				
			item.Stt = Stt;
				
			item.IdKhoaDtriHo = IdKhoaDtriHo;
				
	        item.Save(UserName);
	    }
    }
}
