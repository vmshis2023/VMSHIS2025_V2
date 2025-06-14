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
    /// Controller class for D_Phieu_ThanhLy_CT
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DPhieuThanhLyCtController
    {
        // Preload our schema..
        DPhieuThanhLyCt thisSchemaLoad = new DPhieuThanhLyCt();
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
        public DPhieuThanhLyCtCollection FetchAll()
        {
            DPhieuThanhLyCtCollection coll = new DPhieuThanhLyCtCollection();
            Query qry = new Query(DPhieuThanhLyCt.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DPhieuThanhLyCtCollection FetchByID(object IdThanhLyCt)
        {
            DPhieuThanhLyCtCollection coll = new DPhieuThanhLyCtCollection().Where("Id_ThanhLy_CT", IdThanhLyCt).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DPhieuThanhLyCtCollection FetchByQuery(Query qry)
        {
            DPhieuThanhLyCtCollection coll = new DPhieuThanhLyCtCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdThanhLyCt)
        {
            return (DPhieuThanhLyCt.Delete(IdThanhLyCt) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdThanhLyCt)
        {
            return (DPhieuThanhLyCt.Destroy(IdThanhLyCt) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int IdThanhLy,int IdThuoc,int SoLuong,DateTime NgayHetHan,decimal DonGia,decimal GiaBan)
	    {
		    DPhieuThanhLyCt item = new DPhieuThanhLyCt();
		    
            item.IdThanhLy = IdThanhLy;
            
            item.IdThuoc = IdThuoc;
            
            item.SoLuong = SoLuong;
            
            item.NgayHetHan = NgayHetHan;
            
            item.DonGia = DonGia;
            
            item.GiaBan = GiaBan;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdThanhLyCt,int IdThanhLy,int IdThuoc,int SoLuong,DateTime NgayHetHan,decimal DonGia,decimal GiaBan)
	    {
		    DPhieuThanhLyCt item = new DPhieuThanhLyCt();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdThanhLyCt = IdThanhLyCt;
				
			item.IdThanhLy = IdThanhLy;
				
			item.IdThuoc = IdThuoc;
				
			item.SoLuong = SoLuong;
				
			item.NgayHetHan = NgayHetHan;
				
			item.DonGia = DonGia;
				
			item.GiaBan = GiaBan;
				
	        item.Save(UserName);
	    }
    }
}
