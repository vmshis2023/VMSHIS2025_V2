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
    /// Controller class for L_LSU_HCHINH_DONTHUOC_CHITIET
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class LLsuHchinhDonthuocChitietController
    {
        // Preload our schema..
        LLsuHchinhDonthuocChitiet thisSchemaLoad = new LLsuHchinhDonthuocChitiet();
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
        public LLsuHchinhDonthuocChitietCollection FetchAll()
        {
            LLsuHchinhDonthuocChitietCollection coll = new LLsuHchinhDonthuocChitietCollection();
            Query qry = new Query(LLsuHchinhDonthuocChitiet.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public LLsuHchinhDonthuocChitietCollection FetchByID(object IdHchinh)
        {
            LLsuHchinhDonthuocChitietCollection coll = new LLsuHchinhDonthuocChitietCollection().Where("ID_HCHINH", IdHchinh).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public LLsuHchinhDonthuocChitietCollection FetchByQuery(Query qry)
        {
            LLsuHchinhDonthuocChitietCollection coll = new LLsuHchinhDonthuocChitietCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdHchinh)
        {
            return (LLsuHchinhDonthuocChitiet.Delete(IdHchinh) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdHchinh)
        {
            return (LLsuHchinhDonthuocChitiet.Destroy(IdHchinh) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(long IdHchinh,int IdDonthuoc,int IdChitiet)
        {
            Query qry = new Query(LLsuHchinhDonthuocChitiet.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("IdHchinh", IdHchinh).AND("IdDonthuoc", IdDonthuoc).AND("IdChitiet", IdChitiet);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(long IdHchinh,int IdDonthuoc,int IdChitiet,int IdThuoc,int SluongCu,int SluongHchinh)
	    {
		    LLsuHchinhDonthuocChitiet item = new LLsuHchinhDonthuocChitiet();
		    
            item.IdHchinh = IdHchinh;
            
            item.IdDonthuoc = IdDonthuoc;
            
            item.IdChitiet = IdChitiet;
            
            item.IdThuoc = IdThuoc;
            
            item.SluongCu = SluongCu;
            
            item.SluongHchinh = SluongHchinh;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IdHchinh,int IdDonthuoc,int IdChitiet,int IdThuoc,int SluongCu,int SluongHchinh)
	    {
		    LLsuHchinhDonthuocChitiet item = new LLsuHchinhDonthuocChitiet();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdHchinh = IdHchinh;
				
			item.IdDonthuoc = IdDonthuoc;
				
			item.IdChitiet = IdChitiet;
				
			item.IdThuoc = IdThuoc;
				
			item.SluongCu = SluongCu;
				
			item.SluongHchinh = SluongHchinh;
				
	        item.Save(UserName);
	    }
    }
}
