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
    /// Controller class for L_TinhHinh_RaVien
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LTinhHinhRaVienController
    {
        // Preload our schema..
        LTinhHinhRaVien thisSchemaLoad = new LTinhHinhRaVien();
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
        public LTinhHinhRaVienCollection FetchAll()
        {
            LTinhHinhRaVienCollection coll = new LTinhHinhRaVienCollection();
            Query qry = new Query(LTinhHinhRaVien.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LTinhHinhRaVienCollection FetchByID(object IdThinhRvien)
        {
            LTinhHinhRaVienCollection coll = new LTinhHinhRaVienCollection().Where("ID_THINH_RVIEN", IdThinhRvien).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LTinhHinhRaVienCollection FetchByQuery(Query qry)
        {
            LTinhHinhRaVienCollection coll = new LTinhHinhRaVienCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdThinhRvien)
        {
            return (LTinhHinhRaVien.Delete(IdThinhRvien) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdThinhRvien)
        {
            return (LTinhHinhRaVien.Destroy(IdThinhRvien) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(short IdThinhRvien,string TenThinhRvien,short? Stt,byte? ChiDoc)
	    {
		    LTinhHinhRaVien item = new LTinhHinhRaVien();
		    
            item.IdThinhRvien = IdThinhRvien;
            
            item.TenThinhRvien = TenThinhRvien;
            
            item.Stt = Stt;
            
            item.ChiDoc = ChiDoc;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(short IdThinhRvien,string TenThinhRvien,short? Stt,byte? ChiDoc)
	    {
		    LTinhHinhRaVien item = new LTinhHinhRaVien();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdThinhRvien = IdThinhRvien;
				
			item.TenThinhRvien = TenThinhRvien;
				
			item.Stt = Stt;
				
			item.ChiDoc = ChiDoc;
				
	        item.Save(UserName);
	    }
    }
}
