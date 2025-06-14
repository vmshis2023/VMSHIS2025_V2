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
    /// Controller class for kcb_bacsicungthuchien
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class KcbBacsicungthuchienController
    {
        // Preload our schema..
        KcbBacsicungthuchien thisSchemaLoad = new KcbBacsicungthuchien();
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
        public KcbBacsicungthuchienCollection FetchAll()
        {
            KcbBacsicungthuchienCollection coll = new KcbBacsicungthuchienCollection();
            Query qry = new Query(KcbBacsicungthuchien.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbBacsicungthuchienCollection FetchByID(object IdChidinhchitiet)
        {
            KcbBacsicungthuchienCollection coll = new KcbBacsicungthuchienCollection().Where("id_chidinhchitiet", IdChidinhchitiet).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbBacsicungthuchienCollection FetchByQuery(Query qry)
        {
            KcbBacsicungthuchienCollection coll = new KcbBacsicungthuchienCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdChidinhchitiet)
        {
            return (KcbBacsicungthuchien.Delete(IdChidinhchitiet) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdChidinhchitiet)
        {
            return (KcbBacsicungthuchien.Destroy(IdChidinhchitiet) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(long IdChidinhchitiet,string MaBacsi)
        {
            Query qry = new Query(KcbBacsicungthuchien.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("IdChidinhchitiet", IdChidinhchitiet).AND("MaBacsi", MaBacsi);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(long IdChidinhchitiet,string MaBacsi,string NoiDung,string NguoiTao,DateTime? NgayTao,string NguoiSua,DateTime NgaySua,bool? Cungthuchien)
	    {
		    KcbBacsicungthuchien item = new KcbBacsicungthuchien();
		    
            item.IdChidinhchitiet = IdChidinhchitiet;
            
            item.MaBacsi = MaBacsi;
            
            item.NoiDung = NoiDung;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
            item.Cungthuchien = Cungthuchien;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IdChidinhchitiet,string MaBacsi,string NoiDung,string NguoiTao,DateTime? NgayTao,string NguoiSua,DateTime NgaySua,bool? Cungthuchien)
	    {
		    KcbBacsicungthuchien item = new KcbBacsicungthuchien();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdChidinhchitiet = IdChidinhchitiet;
				
			item.MaBacsi = MaBacsi;
				
			item.NoiDung = NoiDung;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
			item.Cungthuchien = Cungthuchien;
				
	        item.Save(UserName);
	    }
    }
}
