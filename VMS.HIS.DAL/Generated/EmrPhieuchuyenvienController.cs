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
    /// Controller class for emr_phieuchuyenvien
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class EmrPhieuchuyenvienController
    {
        // Preload our schema..
        EmrPhieuchuyenvien thisSchemaLoad = new EmrPhieuchuyenvien();
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
        public EmrPhieuchuyenvienCollection FetchAll()
        {
            EmrPhieuchuyenvienCollection coll = new EmrPhieuchuyenvienCollection();
            Query qry = new Query(EmrPhieuchuyenvien.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public EmrPhieuchuyenvienCollection FetchByID(object IdChuyenvien)
        {
            EmrPhieuchuyenvienCollection coll = new EmrPhieuchuyenvienCollection().Where("id_chuyenvien", IdChuyenvien).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public EmrPhieuchuyenvienCollection FetchByQuery(Query qry)
        {
            EmrPhieuchuyenvienCollection coll = new EmrPhieuchuyenvienCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdChuyenvien)
        {
            return (EmrPhieuchuyenvien.Delete(IdChuyenvien) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdChuyenvien)
        {
            return (EmrPhieuchuyenvien.Destroy(IdChuyenvien) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaLuotkham,long IdBenhnhan,bool? ChuyenvienTuyentren,bool? ChuyenvienTuyenduoi,bool? ChuyenvienKhac,string NoiChuyen,long? IdChuyenvienHis,string NguoiTao,DateTime? NgayTao,string IpMaytao,string NguoiSua,DateTime? NgaySua,string IpMaysua)
	    {
		    EmrPhieuchuyenvien item = new EmrPhieuchuyenvien();
		    
            item.MaLuotkham = MaLuotkham;
            
            item.IdBenhnhan = IdBenhnhan;
            
            item.ChuyenvienTuyentren = ChuyenvienTuyentren;
            
            item.ChuyenvienTuyenduoi = ChuyenvienTuyenduoi;
            
            item.ChuyenvienKhac = ChuyenvienKhac;
            
            item.NoiChuyen = NoiChuyen;
            
            item.IdChuyenvienHis = IdChuyenvienHis;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.IpMaytao = IpMaytao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
            item.IpMaysua = IpMaysua;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IdChuyenvien,string MaLuotkham,long IdBenhnhan,bool? ChuyenvienTuyentren,bool? ChuyenvienTuyenduoi,bool? ChuyenvienKhac,string NoiChuyen,long? IdChuyenvienHis,string NguoiTao,DateTime? NgayTao,string IpMaytao,string NguoiSua,DateTime? NgaySua,string IpMaysua)
	    {
		    EmrPhieuchuyenvien item = new EmrPhieuchuyenvien();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdChuyenvien = IdChuyenvien;
				
			item.MaLuotkham = MaLuotkham;
				
			item.IdBenhnhan = IdBenhnhan;
				
			item.ChuyenvienTuyentren = ChuyenvienTuyentren;
				
			item.ChuyenvienTuyenduoi = ChuyenvienTuyenduoi;
				
			item.ChuyenvienKhac = ChuyenvienKhac;
				
			item.NoiChuyen = NoiChuyen;
				
			item.IdChuyenvienHis = IdChuyenvienHis;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.IpMaytao = IpMaytao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
			item.IpMaysua = IpMaysua;
				
	        item.Save(UserName);
	    }
    }
}
