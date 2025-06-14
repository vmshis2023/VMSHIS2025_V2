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
    /// Controller class for emr_phieukham_sankhoa
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class EmrPhieukhamSankhoaController
    {
        // Preload our schema..
        EmrPhieukhamSankhoa thisSchemaLoad = new EmrPhieukhamSankhoa();
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
        public EmrPhieukhamSankhoaCollection FetchAll()
        {
            EmrPhieukhamSankhoaCollection coll = new EmrPhieukhamSankhoaCollection();
            Query qry = new Query(EmrPhieukhamSankhoa.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public EmrPhieukhamSankhoaCollection FetchByID(object Id)
        {
            EmrPhieukhamSankhoaCollection coll = new EmrPhieukhamSankhoaCollection().Where("id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public EmrPhieukhamSankhoaCollection FetchByQuery(Query qry)
        {
            EmrPhieukhamSankhoaCollection coll = new EmrPhieukhamSankhoaCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (EmrPhieukhamSankhoa.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (EmrPhieukhamSankhoa.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(long? IdBenhnhan,string MaLuotkham,DateTime NgayKham,string Mach,string NhietDo,string HuyetAp,string NhịpTho,string ChieuCao,string CanNang,string NhomMau,string Bmi,string MotaThem,bool? KhamngoaiBungcoseophauthuatcu,string KhamngoaiHinhdangtucung,string KhamngoaiTuthe,byte? KhamngoaiChieucaotucung,byte? KhamngoaiVongbung,string KhamngoaiConcotucung,byte? KhamngoaiTimthai,string KhamngoaiVu,string KbChisoBishop,string KbAmho,string KbAmdao,string KbTangsinhmon,string KbCotucung,string KbPhanphu,bool? KbTinhtrangoiPhong,bool? KbTinhtrangoiDet,bool? KbTinhtrangoiQuale,DateTime? KbThoigianoivo,bool? KbTinhtrangoivoTunhien,bool? KbTinhtrangoivoBamoi,string KbMausacnuocoi,string KbNuocoinhieuit,string KbNgoi,string KbThe,string KbKieuthe,string KbDuongkinhnhohave,bool? KbDolotCao,bool? KbDolotChuc,bool? KbDolotChat,bool? KbDolotLot,short? IdBacsi,string NguoiTao,DateTime? NgayTao,string NguoiSua,DateTime? NgaySua)
	    {
		    EmrPhieukhamSankhoa item = new EmrPhieukhamSankhoa();
		    
            item.IdBenhnhan = IdBenhnhan;
            
            item.MaLuotkham = MaLuotkham;
            
            item.NgayKham = NgayKham;
            
            item.Mach = Mach;
            
            item.NhietDo = NhietDo;
            
            item.HuyetAp = HuyetAp;
            
            item.NhịpTho = NhịpTho;
            
            item.ChieuCao = ChieuCao;
            
            item.CanNang = CanNang;
            
            item.NhomMau = NhomMau;
            
            item.Bmi = Bmi;
            
            item.MotaThem = MotaThem;
            
            item.KhamngoaiBungcoseophauthuatcu = KhamngoaiBungcoseophauthuatcu;
            
            item.KhamngoaiHinhdangtucung = KhamngoaiHinhdangtucung;
            
            item.KhamngoaiTuthe = KhamngoaiTuthe;
            
            item.KhamngoaiChieucaotucung = KhamngoaiChieucaotucung;
            
            item.KhamngoaiVongbung = KhamngoaiVongbung;
            
            item.KhamngoaiConcotucung = KhamngoaiConcotucung;
            
            item.KhamngoaiTimthai = KhamngoaiTimthai;
            
            item.KhamngoaiVu = KhamngoaiVu;
            
            item.KbChisoBishop = KbChisoBishop;
            
            item.KbAmho = KbAmho;
            
            item.KbAmdao = KbAmdao;
            
            item.KbTangsinhmon = KbTangsinhmon;
            
            item.KbCotucung = KbCotucung;
            
            item.KbPhanphu = KbPhanphu;
            
            item.KbTinhtrangoiPhong = KbTinhtrangoiPhong;
            
            item.KbTinhtrangoiDet = KbTinhtrangoiDet;
            
            item.KbTinhtrangoiQuale = KbTinhtrangoiQuale;
            
            item.KbThoigianoivo = KbThoigianoivo;
            
            item.KbTinhtrangoivoTunhien = KbTinhtrangoivoTunhien;
            
            item.KbTinhtrangoivoBamoi = KbTinhtrangoivoBamoi;
            
            item.KbMausacnuocoi = KbMausacnuocoi;
            
            item.KbNuocoinhieuit = KbNuocoinhieuit;
            
            item.KbNgoi = KbNgoi;
            
            item.KbThe = KbThe;
            
            item.KbKieuthe = KbKieuthe;
            
            item.KbDuongkinhnhohave = KbDuongkinhnhohave;
            
            item.KbDolotCao = KbDolotCao;
            
            item.KbDolotChuc = KbDolotChuc;
            
            item.KbDolotChat = KbDolotChat;
            
            item.KbDolotLot = KbDolotLot;
            
            item.IdBacsi = IdBacsi;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long Id,long? IdBenhnhan,string MaLuotkham,DateTime NgayKham,string Mach,string NhietDo,string HuyetAp,string NhịpTho,string ChieuCao,string CanNang,string NhomMau,string Bmi,string MotaThem,bool? KhamngoaiBungcoseophauthuatcu,string KhamngoaiHinhdangtucung,string KhamngoaiTuthe,byte? KhamngoaiChieucaotucung,byte? KhamngoaiVongbung,string KhamngoaiConcotucung,byte? KhamngoaiTimthai,string KhamngoaiVu,string KbChisoBishop,string KbAmho,string KbAmdao,string KbTangsinhmon,string KbCotucung,string KbPhanphu,bool? KbTinhtrangoiPhong,bool? KbTinhtrangoiDet,bool? KbTinhtrangoiQuale,DateTime? KbThoigianoivo,bool? KbTinhtrangoivoTunhien,bool? KbTinhtrangoivoBamoi,string KbMausacnuocoi,string KbNuocoinhieuit,string KbNgoi,string KbThe,string KbKieuthe,string KbDuongkinhnhohave,bool? KbDolotCao,bool? KbDolotChuc,bool? KbDolotChat,bool? KbDolotLot,short? IdBacsi,string NguoiTao,DateTime? NgayTao,string NguoiSua,DateTime? NgaySua)
	    {
		    EmrPhieukhamSankhoa item = new EmrPhieukhamSankhoa();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.IdBenhnhan = IdBenhnhan;
				
			item.MaLuotkham = MaLuotkham;
				
			item.NgayKham = NgayKham;
				
			item.Mach = Mach;
				
			item.NhietDo = NhietDo;
				
			item.HuyetAp = HuyetAp;
				
			item.NhịpTho = NhịpTho;
				
			item.ChieuCao = ChieuCao;
				
			item.CanNang = CanNang;
				
			item.NhomMau = NhomMau;
				
			item.Bmi = Bmi;
				
			item.MotaThem = MotaThem;
				
			item.KhamngoaiBungcoseophauthuatcu = KhamngoaiBungcoseophauthuatcu;
				
			item.KhamngoaiHinhdangtucung = KhamngoaiHinhdangtucung;
				
			item.KhamngoaiTuthe = KhamngoaiTuthe;
				
			item.KhamngoaiChieucaotucung = KhamngoaiChieucaotucung;
				
			item.KhamngoaiVongbung = KhamngoaiVongbung;
				
			item.KhamngoaiConcotucung = KhamngoaiConcotucung;
				
			item.KhamngoaiTimthai = KhamngoaiTimthai;
				
			item.KhamngoaiVu = KhamngoaiVu;
				
			item.KbChisoBishop = KbChisoBishop;
				
			item.KbAmho = KbAmho;
				
			item.KbAmdao = KbAmdao;
				
			item.KbTangsinhmon = KbTangsinhmon;
				
			item.KbCotucung = KbCotucung;
				
			item.KbPhanphu = KbPhanphu;
				
			item.KbTinhtrangoiPhong = KbTinhtrangoiPhong;
				
			item.KbTinhtrangoiDet = KbTinhtrangoiDet;
				
			item.KbTinhtrangoiQuale = KbTinhtrangoiQuale;
				
			item.KbThoigianoivo = KbThoigianoivo;
				
			item.KbTinhtrangoivoTunhien = KbTinhtrangoivoTunhien;
				
			item.KbTinhtrangoivoBamoi = KbTinhtrangoivoBamoi;
				
			item.KbMausacnuocoi = KbMausacnuocoi;
				
			item.KbNuocoinhieuit = KbNuocoinhieuit;
				
			item.KbNgoi = KbNgoi;
				
			item.KbThe = KbThe;
				
			item.KbKieuthe = KbKieuthe;
				
			item.KbDuongkinhnhohave = KbDuongkinhnhohave;
				
			item.KbDolotCao = KbDolotCao;
				
			item.KbDolotChuc = KbDolotChuc;
				
			item.KbDolotChat = KbDolotChat;
				
			item.KbDolotLot = KbDolotLot;
				
			item.IdBacsi = IdBacsi;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
	        item.Save(UserName);
	    }
    }
}
