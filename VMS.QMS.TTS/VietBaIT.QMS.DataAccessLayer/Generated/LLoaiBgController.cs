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
    /// Controller class for L_LOAI_BG
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LLoaiBgController
    {
        // Preload our schema..
        LLoaiBg thisSchemaLoad = new LLoaiBg();
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
        public LLoaiBgCollection FetchAll()
        {
            LLoaiBgCollection coll = new LLoaiBgCollection();
            Query qry = new Query(LLoaiBg.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LLoaiBgCollection FetchByID(object IdLoaiBg)
        {
            LLoaiBgCollection coll = new LLoaiBgCollection().Where("ID_LOAI_BG", IdLoaiBg).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LLoaiBgCollection FetchByQuery(Query qry)
        {
            LLoaiBgCollection coll = new LLoaiBgCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdLoaiBg)
        {
            return (LLoaiBg.Delete(IdLoaiBg) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdLoaiBg)
        {
            return (LLoaiBg.Destroy(IdLoaiBg) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaLoaiBg,string TenLoaiBg,short? ThuTu,string KieuThue,decimal? DonGia,string GhiChu,string MaDtuong,byte? HienThi,string TenHienThi,decimal? PhuThuNgoaigoi,decimal? PhuThuTrongGoi)
	    {
		    LLoaiBg item = new LLoaiBg();
		    
            item.MaLoaiBg = MaLoaiBg;
            
            item.TenLoaiBg = TenLoaiBg;
            
            item.ThuTu = ThuTu;
            
            item.KieuThue = KieuThue;
            
            item.DonGia = DonGia;
            
            item.GhiChu = GhiChu;
            
            item.MaDtuong = MaDtuong;
            
            item.HienThi = HienThi;
            
            item.TenHienThi = TenHienThi;
            
            item.PhuThuNgoaigoi = PhuThuNgoaigoi;
            
            item.PhuThuTrongGoi = PhuThuTrongGoi;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdLoaiBg,string MaLoaiBg,string TenLoaiBg,short? ThuTu,string KieuThue,decimal? DonGia,string GhiChu,string MaDtuong,byte? HienThi,string TenHienThi,decimal? PhuThuNgoaigoi,decimal? PhuThuTrongGoi)
	    {
		    LLoaiBg item = new LLoaiBg();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdLoaiBg = IdLoaiBg;
				
			item.MaLoaiBg = MaLoaiBg;
				
			item.TenLoaiBg = TenLoaiBg;
				
			item.ThuTu = ThuTu;
				
			item.KieuThue = KieuThue;
				
			item.DonGia = DonGia;
				
			item.GhiChu = GhiChu;
				
			item.MaDtuong = MaDtuong;
				
			item.HienThi = HienThi;
				
			item.TenHienThi = TenHienThi;
				
			item.PhuThuNgoaigoi = PhuThuNgoaigoi;
				
			item.PhuThuTrongGoi = PhuThuTrongGoi;
				
	        item.Save(UserName);
	    }
    }
}
