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
    /// Controller class for qhe_doituong_thuoc
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class QheDoituongThuocController
    {
        // Preload our schema..
        QheDoituongThuoc thisSchemaLoad = new QheDoituongThuoc();
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
        public QheDoituongThuocCollection FetchAll()
        {
            QheDoituongThuocCollection coll = new QheDoituongThuocCollection();
            Query qry = new Query(QheDoituongThuoc.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public QheDoituongThuocCollection FetchByID(object IdQuanhe)
        {
            QheDoituongThuocCollection coll = new QheDoituongThuocCollection().Where("id_quanhe", IdQuanhe).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public QheDoituongThuocCollection FetchByQuery(Query qry)
        {
            QheDoituongThuocCollection coll = new QheDoituongThuocCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdQuanhe)
        {
            return (QheDoituongThuoc.Delete(IdQuanhe) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdQuanhe)
        {
            return (QheDoituongThuoc.Destroy(IdQuanhe) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? IdDoituongKcb,short? IdLoaithuoc,int IdThuoc,decimal TyleGiamgia,string KieuGiamgia,decimal? TyleTt,string MotaThem,decimal DonGia,decimal? PhuthuDungtuyen,int IdLoaidoituongKcb,decimal? PhuthuTraituyen,string MaDoituongKcb,string NguoiTao,DateTime? NgayTao,string NguoiSua,DateTime? NgaySua,string MaKhoaThuchien,string KieuThuocvt,decimal? GiaCu)
	    {
		    QheDoituongThuoc item = new QheDoituongThuoc();
		    
            item.IdDoituongKcb = IdDoituongKcb;
            
            item.IdLoaithuoc = IdLoaithuoc;
            
            item.IdThuoc = IdThuoc;
            
            item.TyleGiamgia = TyleGiamgia;
            
            item.KieuGiamgia = KieuGiamgia;
            
            item.TyleTt = TyleTt;
            
            item.MotaThem = MotaThem;
            
            item.DonGia = DonGia;
            
            item.PhuthuDungtuyen = PhuthuDungtuyen;
            
            item.IdLoaidoituongKcb = IdLoaidoituongKcb;
            
            item.PhuthuTraituyen = PhuthuTraituyen;
            
            item.MaDoituongKcb = MaDoituongKcb;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
            item.MaKhoaThuchien = MaKhoaThuchien;
            
            item.KieuThuocvt = KieuThuocvt;
            
            item.GiaCu = GiaCu;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdQuanhe,int? IdDoituongKcb,short? IdLoaithuoc,int IdThuoc,decimal TyleGiamgia,string KieuGiamgia,decimal? TyleTt,string MotaThem,decimal DonGia,decimal? PhuthuDungtuyen,int IdLoaidoituongKcb,decimal? PhuthuTraituyen,string MaDoituongKcb,string NguoiTao,DateTime? NgayTao,string NguoiSua,DateTime? NgaySua,string MaKhoaThuchien,string KieuThuocvt,decimal? GiaCu)
	    {
		    QheDoituongThuoc item = new QheDoituongThuoc();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdQuanhe = IdQuanhe;
				
			item.IdDoituongKcb = IdDoituongKcb;
				
			item.IdLoaithuoc = IdLoaithuoc;
				
			item.IdThuoc = IdThuoc;
				
			item.TyleGiamgia = TyleGiamgia;
				
			item.KieuGiamgia = KieuGiamgia;
				
			item.TyleTt = TyleTt;
				
			item.MotaThem = MotaThem;
				
			item.DonGia = DonGia;
				
			item.PhuthuDungtuyen = PhuthuDungtuyen;
				
			item.IdLoaidoituongKcb = IdLoaidoituongKcb;
				
			item.PhuthuTraituyen = PhuthuTraituyen;
				
			item.MaDoituongKcb = MaDoituongKcb;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
			item.MaKhoaThuchien = MaKhoaThuchien;
				
			item.KieuThuocvt = KieuThuocvt;
				
			item.GiaCu = GiaCu;
				
	        item.Save(UserName);
	    }
    }
}
