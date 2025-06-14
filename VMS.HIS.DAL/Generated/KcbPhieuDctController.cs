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
    /// Controller class for kcb_phieu_dct
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class KcbPhieuDctController
    {
        // Preload our schema..
        KcbPhieuDct thisSchemaLoad = new KcbPhieuDct();
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
        public KcbPhieuDctCollection FetchAll()
        {
            KcbPhieuDctCollection coll = new KcbPhieuDctCollection();
            Query qry = new Query(KcbPhieuDct.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbPhieuDctCollection FetchByID(object IdPhieuDct)
        {
            KcbPhieuDctCollection coll = new KcbPhieuDctCollection().Where("id_phieu_dct", IdPhieuDct).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbPhieuDctCollection FetchByQuery(Query qry)
        {
            KcbPhieuDctCollection coll = new KcbPhieuDctCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdPhieuDct)
        {
            return (KcbPhieuDct.Delete(IdPhieuDct) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdPhieuDct)
        {
            return (KcbPhieuDct.Destroy(IdPhieuDct) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaPhieuDct,string MaLuotkham,long IdBenhnhan,decimal TongTien,decimal BhytChitra,decimal BnhanChitra,decimal? NguonkhacChitra,byte KieuThanhtoan,short? IdKhoaThien,string MaKhoaThuchien,string TenKieuthanhtoan,long? IdThe,string MatheBhyt,string NguoiTao,DateTime NgayTao,DateTime? NgaySua,string NguoiSua,string IpMaytao,string TenMaytao,string IpMaysua,string TenMaysua,byte? TrangthaiXml,string MaCoso,byte? TthaiChot,DateTime? NgayChot)
	    {
		    KcbPhieuDct item = new KcbPhieuDct();
		    
            item.MaPhieuDct = MaPhieuDct;
            
            item.MaLuotkham = MaLuotkham;
            
            item.IdBenhnhan = IdBenhnhan;
            
            item.TongTien = TongTien;
            
            item.BhytChitra = BhytChitra;
            
            item.BnhanChitra = BnhanChitra;
            
            item.NguonkhacChitra = NguonkhacChitra;
            
            item.KieuThanhtoan = KieuThanhtoan;
            
            item.IdKhoaThien = IdKhoaThien;
            
            item.MaKhoaThuchien = MaKhoaThuchien;
            
            item.TenKieuthanhtoan = TenKieuthanhtoan;
            
            item.IdThe = IdThe;
            
            item.MatheBhyt = MatheBhyt;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.NgaySua = NgaySua;
            
            item.NguoiSua = NguoiSua;
            
            item.IpMaytao = IpMaytao;
            
            item.TenMaytao = TenMaytao;
            
            item.IpMaysua = IpMaysua;
            
            item.TenMaysua = TenMaysua;
            
            item.TrangthaiXml = TrangthaiXml;
            
            item.MaCoso = MaCoso;
            
            item.TthaiChot = TthaiChot;
            
            item.NgayChot = NgayChot;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IdPhieuDct,string MaPhieuDct,string MaLuotkham,long IdBenhnhan,decimal TongTien,decimal BhytChitra,decimal BnhanChitra,decimal? NguonkhacChitra,byte KieuThanhtoan,short? IdKhoaThien,string MaKhoaThuchien,string TenKieuthanhtoan,long? IdThe,string MatheBhyt,string NguoiTao,DateTime NgayTao,DateTime? NgaySua,string NguoiSua,string IpMaytao,string TenMaytao,string IpMaysua,string TenMaysua,byte? TrangthaiXml,string MaCoso,byte? TthaiChot,DateTime? NgayChot)
	    {
		    KcbPhieuDct item = new KcbPhieuDct();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdPhieuDct = IdPhieuDct;
				
			item.MaPhieuDct = MaPhieuDct;
				
			item.MaLuotkham = MaLuotkham;
				
			item.IdBenhnhan = IdBenhnhan;
				
			item.TongTien = TongTien;
				
			item.BhytChitra = BhytChitra;
				
			item.BnhanChitra = BnhanChitra;
				
			item.NguonkhacChitra = NguonkhacChitra;
				
			item.KieuThanhtoan = KieuThanhtoan;
				
			item.IdKhoaThien = IdKhoaThien;
				
			item.MaKhoaThuchien = MaKhoaThuchien;
				
			item.TenKieuthanhtoan = TenKieuthanhtoan;
				
			item.IdThe = IdThe;
				
			item.MatheBhyt = MatheBhyt;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.NgaySua = NgaySua;
				
			item.NguoiSua = NguoiSua;
				
			item.IpMaytao = IpMaytao;
				
			item.TenMaytao = TenMaytao;
				
			item.IpMaysua = IpMaysua;
				
			item.TenMaysua = TenMaysua;
				
			item.TrangthaiXml = TrangthaiXml;
				
			item.MaCoso = MaCoso;
				
			item.TthaiChot = TthaiChot;
				
			item.NgayChot = NgayChot;
				
	        item.Save(UserName);
	    }
    }
}
