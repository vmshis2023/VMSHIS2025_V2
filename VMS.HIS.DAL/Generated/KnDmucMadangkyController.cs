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
    /// Controller class for kn_dmuc_madangky
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class KnDmucMadangkyController
    {
        // Preload our schema..
        KnDmucMadangky thisSchemaLoad = new KnDmucMadangky();
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
        public KnDmucMadangkyCollection FetchAll()
        {
            KnDmucMadangkyCollection coll = new KnDmucMadangkyCollection();
            Query qry = new Query(KnDmucMadangky.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public KnDmucMadangkyCollection FetchByID(object MaDangky)
        {
            KnDmucMadangkyCollection coll = new KnDmucMadangkyCollection().Where("ma_dangky", MaDangky).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public KnDmucMadangkyCollection FetchByQuery(Query qry)
        {
            KnDmucMadangkyCollection coll = new KnDmucMadangkyCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object MaDangky)
        {
            return (KnDmucMadangky.Delete(MaDangky) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object MaDangky)
        {
            return (KnDmucMadangky.Destroy(MaDangky) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string MaDangky,short Nam,byte Loai)
        {
            Query qry = new Query(KnDmucMadangky.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("MaDangky", MaDangky).AND("Nam", Nam).AND("Loai", Loai);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int Stt,string MaDangky,short Nam,byte TrangThai,byte Loai,string UsedBy,DateTime? StartTime,DateTime? EndTime,string UnlockBy,DateTime? UnlockTime)
	    {
		    KnDmucMadangky item = new KnDmucMadangky();
		    
            item.Stt = Stt;
            
            item.MaDangky = MaDangky;
            
            item.Nam = Nam;
            
            item.TrangThai = TrangThai;
            
            item.Loai = Loai;
            
            item.UsedBy = UsedBy;
            
            item.StartTime = StartTime;
            
            item.EndTime = EndTime;
            
            item.UnlockBy = UnlockBy;
            
            item.UnlockTime = UnlockTime;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Stt,string MaDangky,short Nam,byte TrangThai,byte Loai,string UsedBy,DateTime? StartTime,DateTime? EndTime,string UnlockBy,DateTime? UnlockTime)
	    {
		    KnDmucMadangky item = new KnDmucMadangky();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Stt = Stt;
				
			item.MaDangky = MaDangky;
				
			item.Nam = Nam;
				
			item.TrangThai = TrangThai;
				
			item.Loai = Loai;
				
			item.UsedBy = UsedBy;
				
			item.StartTime = StartTime;
				
			item.EndTime = EndTime;
				
			item.UnlockBy = UnlockBy;
				
			item.UnlockTime = UnlockTime;
				
	        item.Save(UserName);
	    }
    }
}
