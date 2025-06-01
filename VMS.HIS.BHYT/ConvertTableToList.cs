using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using VNS.Libs;

namespace VMS.HIS.BHYT
{
    public static class ConvertTableToList
    {
        private static string GetTenKetquaDtri(string sketqua)
        {
            string ten = "";
            switch (sketqua)
            {
                case "1":
                    ten = "Khỏi";
                    break;
                case "2":
                    ten = "Đỡ";
                    break;
                case "3":
                    ten = "Không thay đổi";
                    break;
                case "4":
                    ten = "Nặng hơn";
                    break;
                case "5":
                    ten = "Tử vong";
                    break;
                default:
                    ten = "Khỏi";
                    break;
            }
            return ten;
        }
        private static string GetTenLyDoVaoVien(string slydo)
        {
            string ten = "";
            switch (slydo)
            {
                case "1":
                    ten = "Đúng tuyến";
                    break;
                case "2":
                    ten = "Cấp cứu";
                    break;
                case "3":
                    ten = "Trái tuyến";
                    break;
                case "4":
                    ten = "Thông tuyến";
                    break;
                default:
                    ten = "Đúng tuyến";
                    break;
            }
            return ten;
        }
        private static string GetTenTinhTrang(string stinhTrang)
        {
            string ten = "";
            switch (stinhTrang)
            {
                case "1":
                    ten = "Ra viện";
                    break;
                case "2":
                    ten = "Chuyển viện";
                    break;
                case "3":
                    ten = "Trốn viện";
                    break;
                case "4":
                    ten = "Xin ra viện";
                    break;
                default:
                    ten = "Ra viện";
                    break;
            }
            return ten;
        }

        private static string GetTenBenhVien(string sMacoso)
        {
            try
            {
                if (!string.IsNullOrEmpty(sMacoso))
                {
                    string tenbv = (from bv in globalVariables.gv_dtDmucNoiKCBBD.AsEnumerable()
                                    where bv.Field<string>("mota_them") == sMacoso
                                    select bv.Field<string>("ten_kcbbd")).First<string>();
                    return tenbv;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }

        }
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            var values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public static DataTable dtLichSuKCB(List<LichSuKCB2018> dtLichSuKcb2018)
        {
            DataTable dtLicSuThongTuyen2018 = ToDataTable(dtLichSuKcb2018);
            if (!dtLicSuThongTuyen2018.Columns.Contains("TenCSKCB"))
            {
                dtLicSuThongTuyen2018.Columns.Add("TenCSKCB");
            }
            var result = (from row1 in dtLicSuThongTuyen2018.AsEnumerable()
                          select new
                          {
                              maHoSo = row1.Field<long>("maHoSo"),
                              maCSKCB = row1.Field<string>("maCSKCB"),
                              TenCSKCB = GetTenBenhVien(row1.Field<string>("maCSKCB")),
                              ngayVao = DateTime.ParseExact(row1.Field<string>("ngayVao"), "yyyyMMddHHmm", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy HH:mm"),
                              ngayRa = DateTime.ParseExact(row1.Field<string>("ngayRa"), "yyyyMMddHHmm", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy HH:mm"),
                              tenBenh = row1.Field<string>("tenBenh"),
                              tinhTrang = GetTenTinhTrang(row1.Field<string>("tinhTrang")),
                              kqDieuTri = GetTenKetquaDtri(row1.Field<string>("kqDieuTri")),
                              lydoVV = GetTenLyDoVaoVien(row1.Field<string>("lydoVV"))
                          }).ToList();
            return ToDataTable(result);
        }
    }
}
