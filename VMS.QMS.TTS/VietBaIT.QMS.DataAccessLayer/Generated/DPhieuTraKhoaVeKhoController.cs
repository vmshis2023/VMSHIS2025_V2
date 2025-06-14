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
    /// Controller class for D_PhieuTra_Khoa_VeKho
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DPhieuTraKhoaVeKhoController
    {
        // Preload our schema..
        DPhieuTraKhoaVeKho thisSchemaLoad = new DPhieuTraKhoaVeKho();
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
        public DPhieuTraKhoaVeKhoCollection FetchAll()
        {
            DPhieuTraKhoaVeKhoCollection coll = new DPhieuTraKhoaVeKhoCollection();
            Query qry = new Query(DPhieuTraKhoaVeKho.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DPhieuTraKhoaVeKhoCollection FetchByID(object IdPhieuTraKho)
        {
            DPhieuTraKhoaVeKhoCollection coll = new DPhieuTraKhoaVeKhoCollection().Where("Id_PhieuTra_Kho", IdPhieuTraKho).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DPhieuTraKhoaVeKhoCollection FetchByQuery(Query qry)
        {
            DPhieuTraKhoaVeKhoCollection coll = new DPhieuTraKhoaVeKhoCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdPhieuTraKho)
        {
            return (DPhieuTraKhoaVeKho.Delete(IdPhieuTraKho) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdPhieuTraKho)
        {
            return (DPhieuTraKhoaVeKho.Destroy(IdPhieuTraKho) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(DateTime? NgayTra,short? IdNhanVien,short? IdKhoNhan,short? IdKhoaTra,string NguoiTao,DateTime? NgayTao,string NguoiSua,DateTime? NgaySua,bool? DaTra,string GhiChu,string NguoiXacNhan,DateTime? NgayXacNhan)
	    {
		    DPhieuTraKhoaVeKho item = new DPhieuTraKhoaVeKho();
		    
            item.NgayTra = NgayTra;
            
            item.IdNhanVien = IdNhanVien;
            
            item.IdKhoNhan = IdKhoNhan;
            
            item.IdKhoaTra = IdKhoaTra;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
            item.DaTra = DaTra;
            
            item.GhiChu = GhiChu;
            
            item.NguoiXacNhan = NguoiXacNhan;
            
            item.NgayXacNhan = NgayXacNhan;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdPhieuTraKho,DateTime? NgayTra,short? IdNhanVien,short? IdKhoNhan,short? IdKhoaTra,string NguoiTao,DateTime? NgayTao,string NguoiSua,DateTime? NgaySua,bool? DaTra,string GhiChu,string NguoiXacNhan,DateTime? NgayXacNhan)
	    {
		    DPhieuTraKhoaVeKho item = new DPhieuTraKhoaVeKho();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdPhieuTraKho = IdPhieuTraKho;
				
			item.NgayTra = NgayTra;
				
			item.IdNhanVien = IdNhanVien;
				
			item.IdKhoNhan = IdKhoNhan;
				
			item.IdKhoaTra = IdKhoaTra;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
			item.DaTra = DaTra;
				
			item.GhiChu = GhiChu;
				
			item.NguoiXacNhan = NguoiXacNhan;
				
			item.NgayXacNhan = NgayXacNhan;
				
	        item.Save(UserName);
	    }
    }
}
