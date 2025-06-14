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
    /// Controller class for L_HOADON_CAPPHAT
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LHoadonCapphatController
    {
        // Preload our schema..
        LHoadonCapphat thisSchemaLoad = new LHoadonCapphat();
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
        public LHoadonCapphatCollection FetchAll()
        {
            LHoadonCapphatCollection coll = new LHoadonCapphatCollection();
            Query qry = new Query(LHoadonCapphat.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LHoadonCapphatCollection FetchByID(object HdonId)
        {
            LHoadonCapphatCollection coll = new LHoadonCapphatCollection().Where("HDON_ID", HdonId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LHoadonCapphatCollection FetchByQuery(Query qry)
        {
            LHoadonCapphatCollection coll = new LHoadonCapphatCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object HdonId)
        {
            return (LHoadonCapphat.Delete(HdonId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object HdonId)
        {
            return (LHoadonCapphat.Destroy(HdonId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(decimal HdonMauId,string MaNvien,string MauHdon,string KiHieu,string MaQuyen,string SerieDau,string SerieCuoi,string SerieHtai,DateTime NgayCapphat,short TrangThai)
	    {
		    LHoadonCapphat item = new LHoadonCapphat();
		    
            item.HdonMauId = HdonMauId;
            
            item.MaNvien = MaNvien;
            
            item.MauHdon = MauHdon;
            
            item.KiHieu = KiHieu;
            
            item.MaQuyen = MaQuyen;
            
            item.SerieDau = SerieDau;
            
            item.SerieCuoi = SerieCuoi;
            
            item.SerieHtai = SerieHtai;
            
            item.NgayCapphat = NgayCapphat;
            
            item.TrangThai = TrangThai;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(decimal HdonId,decimal HdonMauId,string MaNvien,string MauHdon,string KiHieu,string MaQuyen,string SerieDau,string SerieCuoi,string SerieHtai,DateTime NgayCapphat,short TrangThai)
	    {
		    LHoadonCapphat item = new LHoadonCapphat();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.HdonId = HdonId;
				
			item.HdonMauId = HdonMauId;
				
			item.MaNvien = MaNvien;
				
			item.MauHdon = MauHdon;
				
			item.KiHieu = KiHieu;
				
			item.MaQuyen = MaQuyen;
				
			item.SerieDau = SerieDau;
				
			item.SerieCuoi = SerieCuoi;
				
			item.SerieHtai = SerieHtai;
				
			item.NgayCapphat = NgayCapphat;
				
			item.TrangThai = TrangThai;
				
	        item.Save(UserName);
	    }
    }
}
