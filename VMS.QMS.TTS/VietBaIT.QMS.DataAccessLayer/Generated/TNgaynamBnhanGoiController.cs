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
    /// Controller class for T_NGAYNAM_BNHAN_GOI
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TNgaynamBnhanGoiController
    {
        // Preload our schema..
        TNgaynamBnhanGoi thisSchemaLoad = new TNgaynamBnhanGoi();
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
        public TNgaynamBnhanGoiCollection FetchAll()
        {
            TNgaynamBnhanGoiCollection coll = new TNgaynamBnhanGoiCollection();
            Query qry = new Query(TNgaynamBnhanGoi.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TNgaynamBnhanGoiCollection FetchByID(object Id)
        {
            TNgaynamBnhanGoiCollection coll = new TNgaynamBnhanGoiCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TNgaynamBnhanGoiCollection FetchByQuery(Query qry)
        {
            TNgaynamBnhanGoiCollection coll = new TNgaynamBnhanGoiCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (TNgaynamBnhanGoi.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (TNgaynamBnhanGoi.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? PatientDeptId,string PatientCode,int? PatientId,int? RoomId,int? BedId,short? UnitId,int? SoLuong,decimal? DonGia,decimal? PhuThu,decimal? Bnct,decimal? Bhct,string NguoiTao,DateTime? NgayTao,int? BedType,int? IsPayment)
	    {
		    TNgaynamBnhanGoi item = new TNgaynamBnhanGoi();
		    
            item.PatientDeptId = PatientDeptId;
            
            item.PatientCode = PatientCode;
            
            item.PatientId = PatientId;
            
            item.RoomId = RoomId;
            
            item.BedId = BedId;
            
            item.UnitId = UnitId;
            
            item.SoLuong = SoLuong;
            
            item.DonGia = DonGia;
            
            item.PhuThu = PhuThu;
            
            item.Bnct = Bnct;
            
            item.Bhct = Bhct;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.BedType = BedType;
            
            item.IsPayment = IsPayment;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,int? PatientDeptId,string PatientCode,int? PatientId,int? RoomId,int? BedId,short? UnitId,int? SoLuong,decimal? DonGia,decimal? PhuThu,decimal? Bnct,decimal? Bhct,string NguoiTao,DateTime? NgayTao,int? BedType,int? IsPayment)
	    {
		    TNgaynamBnhanGoi item = new TNgaynamBnhanGoi();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.PatientDeptId = PatientDeptId;
				
			item.PatientCode = PatientCode;
				
			item.PatientId = PatientId;
				
			item.RoomId = RoomId;
				
			item.BedId = BedId;
				
			item.UnitId = UnitId;
				
			item.SoLuong = SoLuong;
				
			item.DonGia = DonGia;
				
			item.PhuThu = PhuThu;
				
			item.Bnct = Bnct;
				
			item.Bhct = Bhct;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.BedType = BedType;
				
			item.IsPayment = IsPayment;
				
	        item.Save(UserName);
	    }
    }
}
