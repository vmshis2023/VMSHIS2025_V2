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
    /// Controller class for kcb_phieuchuyenvien
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class KcbPhieuchuyenvienController
    {
        // Preload our schema..
        KcbPhieuchuyenvien thisSchemaLoad = new KcbPhieuchuyenvien();
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
        public KcbPhieuchuyenvienCollection FetchAll()
        {
            KcbPhieuchuyenvienCollection coll = new KcbPhieuchuyenvienCollection();
            Query qry = new Query(KcbPhieuchuyenvien.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbPhieuchuyenvienCollection FetchByID(object IdPhieu)
        {
            KcbPhieuchuyenvienCollection coll = new KcbPhieuchuyenvienCollection().Where("id_phieu", IdPhieu).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public KcbPhieuchuyenvienCollection FetchByQuery(Query qry)
        {
            KcbPhieuchuyenvienCollection coll = new KcbPhieuchuyenvienCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdPhieu)
        {
            return (KcbPhieuchuyenvien.Delete(IdPhieu) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdPhieu)
        {
            return (KcbPhieuchuyenvien.Destroy(IdPhieu) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string SoChuyentuyen,long IdBenhnhan,string MaLuotkham,DateTime NgayChuyenvien,short IdBenhvienChuyenden,string DauhieuCls,string KetquaXnCls,string Mabenh,string ChanDoan,string ThuocSudung,string TrangthaiBenhnhan,byte? LydoChuyen,string HuongDieutri,string PhuongtienChuyen,string TenNguoichuyen,int? IdKhoanoitru,int? IdBuong,int? IdGiuong,int? IdRavien,byte NoiTru,short? IdBacsiChuyenvien,DateTime NgayTao,string NguoiTao,string IpMaytao,string NguoiSua,DateTime? NgaySua,byte? TuyenChuyen)
	    {
		    KcbPhieuchuyenvien item = new KcbPhieuchuyenvien();
		    
            item.SoChuyentuyen = SoChuyentuyen;
            
            item.IdBenhnhan = IdBenhnhan;
            
            item.MaLuotkham = MaLuotkham;
            
            item.NgayChuyenvien = NgayChuyenvien;
            
            item.IdBenhvienChuyenden = IdBenhvienChuyenden;
            
            item.DauhieuCls = DauhieuCls;
            
            item.KetquaXnCls = KetquaXnCls;
            
            item.Mabenh = Mabenh;
            
            item.ChanDoan = ChanDoan;
            
            item.ThuocSudung = ThuocSudung;
            
            item.TrangthaiBenhnhan = TrangthaiBenhnhan;
            
            item.LydoChuyen = LydoChuyen;
            
            item.HuongDieutri = HuongDieutri;
            
            item.PhuongtienChuyen = PhuongtienChuyen;
            
            item.TenNguoichuyen = TenNguoichuyen;
            
            item.IdKhoanoitru = IdKhoanoitru;
            
            item.IdBuong = IdBuong;
            
            item.IdGiuong = IdGiuong;
            
            item.IdRavien = IdRavien;
            
            item.NoiTru = NoiTru;
            
            item.IdBacsiChuyenvien = IdBacsiChuyenvien;
            
            item.NgayTao = NgayTao;
            
            item.NguoiTao = NguoiTao;
            
            item.IpMaytao = IpMaytao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
            item.TuyenChuyen = TuyenChuyen;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IdPhieu,string SoChuyentuyen,long IdBenhnhan,string MaLuotkham,DateTime NgayChuyenvien,short IdBenhvienChuyenden,string DauhieuCls,string KetquaXnCls,string Mabenh,string ChanDoan,string ThuocSudung,string TrangthaiBenhnhan,byte? LydoChuyen,string HuongDieutri,string PhuongtienChuyen,string TenNguoichuyen,int? IdKhoanoitru,int? IdBuong,int? IdGiuong,int? IdRavien,byte NoiTru,short? IdBacsiChuyenvien,DateTime NgayTao,string NguoiTao,string IpMaytao,string NguoiSua,DateTime? NgaySua,byte? TuyenChuyen)
	    {
		    KcbPhieuchuyenvien item = new KcbPhieuchuyenvien();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdPhieu = IdPhieu;
				
			item.SoChuyentuyen = SoChuyentuyen;
				
			item.IdBenhnhan = IdBenhnhan;
				
			item.MaLuotkham = MaLuotkham;
				
			item.NgayChuyenvien = NgayChuyenvien;
				
			item.IdBenhvienChuyenden = IdBenhvienChuyenden;
				
			item.DauhieuCls = DauhieuCls;
				
			item.KetquaXnCls = KetquaXnCls;
				
			item.Mabenh = Mabenh;
				
			item.ChanDoan = ChanDoan;
				
			item.ThuocSudung = ThuocSudung;
				
			item.TrangthaiBenhnhan = TrangthaiBenhnhan;
				
			item.LydoChuyen = LydoChuyen;
				
			item.HuongDieutri = HuongDieutri;
				
			item.PhuongtienChuyen = PhuongtienChuyen;
				
			item.TenNguoichuyen = TenNguoichuyen;
				
			item.IdKhoanoitru = IdKhoanoitru;
				
			item.IdBuong = IdBuong;
				
			item.IdGiuong = IdGiuong;
				
			item.IdRavien = IdRavien;
				
			item.NoiTru = NoiTru;
				
			item.IdBacsiChuyenvien = IdBacsiChuyenvien;
				
			item.NgayTao = NgayTao;
				
			item.NguoiTao = NguoiTao;
				
			item.IpMaytao = IpMaytao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
			item.TuyenChuyen = TuyenChuyen;
				
	        item.Save(UserName);
	    }
    }
}
