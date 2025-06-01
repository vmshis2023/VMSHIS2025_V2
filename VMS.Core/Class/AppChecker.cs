using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using VietBaIT;
using Microsoft.VisualBasic;
using System.Threading;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;

namespace AppChecker
{
    public class CheckHrk
    {
        static DateTime AppExpiredDate = new DateTime(2014, 3, 4);
        Thread tSaveLastTime = null;
        private string C150181 = "";
        private bool AllowedCheckLst = false;
        bool AutoGenLstFile = false;
        public string pKey = "";
        private string DisplayLanguage = "";
        private Label lblFPDStatus = new Label();
        private ListBox lstFPD = new ListBox();
        static xvect.Encrypt m_ect = new xvect.Encrypt();
        static string expfile = Application.StartupPath + @"\xvexplic.exp";
        static string appexrd = Environment.SystemDirectory + @"\sysappexpd.dll";
        static string appexrd1 = Application.StartupPath + @"\sysappexpd.dll";
        VNS.Libs.AppType.AppEnum.AppMode _AppMode = VNS.Libs.AppType.AppEnum.AppMode.Developer;
        VNS.Libs.AppType.AppEnum.AppName _AppName = VNS.Libs.AppType.AppEnum.AppName.XFILM;
        NLog.Logger log;
        public CheckHrk(NLog.Logger log)
        {
            this.log = log;
            m_ect.UpdateAlgName(m_ect.AlgName);
        }
        public static string getSysFilePath(string value)
        {
            if (value == ("APPEXPIREDDATE" + m_ect.Fam_PWD)) return appexrd;
            return null;
        }
        public static string getAppFilePath(string value)
        {
            if (value == ("APPEXPIREDDATE" + m_ect.Fam_PWD)) return appexrd1;
            return null;
        }
        public static DateTime? getCustomDate(string value)
        {
            if (value == ("APPEXPIREDDATE" + m_ect.Fam_PWD)) return AppExpiredDate;
            return null;
        }
        public CheckHrk(string pKey, VNS.Libs.AppType.AppEnum.AppName _AppName)
        {
            this.pKey = pKey;
            this._AppName = _AppName;

            appexrd = Environment.SystemDirectory + @"\sysappexpd.dll";
            appexrd1 = Application.StartupPath + @"\sysappexpd.dll";

            m_ect.UpdateAlgName(m_ect.AlgName);
        }
        public CheckHrk(string pKey, VNS.Libs.AppType.AppEnum.AppName _AppName, NLog.Logger log)
        {
            this.log = log;

            this.pKey = pKey;
            this._AppName = _AppName;

            appexrd = Environment.SystemDirectory + @"\sysappexpd.dll";
            appexrd1 = Application.StartupPath + @"\sysappexpd.dll";

            m_ect.UpdateAlgName(m_ect.AlgName);
        }
        void ReActivate()
        {
            Utility.Try2DelFile(appexrd);
            Utility.Try2DelFile(appexrd1);
        }
        public static void SaveValue2File(string fileName, string value)
        {
            try
            {
                using (StreamWriter _Writer = new StreamWriter(fileName))
                {
                    _Writer.Write(value);
                    _Writer.Flush();
                    _Writer.Close();
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Thay bằng hàm check dùng tham số hệ thống 
        /// </summary>
        /// <param name="expdate"></param>
        /// <returns></returns>
        public bool isValidExpd_old(ref string expdate)
        {

            log.Trace("Start checking exp----------------------------------------------------------------------------------------------------------");
            frm_RegEprd _RegEprd = new frm_RegEprd(pKey, DisplayLanguage, 0, _AppName);
            try
            {
                m_ect.sPwd = Utility.getAPPName(_AppName) + pKey;
                log.Trace("pwd={0}", m_ect.sPwd);
                if (!File.Exists(expfile))
                {
                    log.Trace("File not found: {0}", expfile);
                    ShowLeadtoolsMessage("File not found: " + expfile + "\nPlease contact the vendor");
                    if (_RegEprd.ShowDialog() != DialogResult.OK)
                        return false;
                    else
                        ReActivate();
                }
                else//Tồn tại file hạn sử dụng
                {
                    string obj = "";
                    using (StreamReader _reader = new StreamReader(expfile))
                    {
                        obj = _reader.ReadLine();
                        if (obj == null)
                        {
                            log.Trace("File content is null from : {0}", expfile);
                            ShowLeadtoolsMessage("File content is not valid: " + expfile + "\nPlease contact the vendor");
                            if (_RegEprd.ShowDialog() != DialogResult.OK)
                                return false;
                            else
                                ReActivate();
                        }
                    }
                    if (obj != null)//File không empty
                    {

                        string _value = m_ect.GiaiMa(obj.Split('|')[0]);
                        log.Trace("file content={0}, _value={1}", obj, _value);
                        //_value = _value.Split('|')[0];
                        if (!Utility.IsNumeric(_value))
                        {
                            log.Trace("File content is not valid from : {0}", expfile);
                            ShowLeadtoolsMessage("File content is not valid: " + expfile + "\nPlease contact the vendor");
                            if (_RegEprd.ShowDialog() != DialogResult.OK)
                                return false;
                            else
                                ReActivate();
                        }
                        expdate = _value;
                        long _exp = Convert.ToInt64(_value);//Có dạng YYYY MM DD                                
                        long _now = Convert.ToInt64(GetYYYYMMDD(DateTime.Now));
                        log.Trace("_exp= {0} , _now={1}", _exp.ToString(), _now.ToString());
                        //Kiểm tra ngày hết hạn so với ngày hiện tại-->Nếu hết hạn thì xóa file hết hạn đi để người dùng phải liên hệ cấp phép lại
                        if (_now >= _exp)
                        {
                            //Tạo 2 file hết hạn để đánh dấu đề phòng người dùng chỉnh đồng hồ
                            if (!File.Exists(appexrd)) SaveValue2File(appexrd, "");
                            if (!File.Exists(appexrd1)) SaveValue2File(appexrd1, "");
                            ShowLeadtoolsMessage("This software has expired.  Contact the provider to order a new version.");
                            _RegEprd.CurrentExp = _exp;
                            _RegEprd.reWrite = false;
                            if (_RegEprd.ShowDialog() != DialogResult.OK)
                                return false;
                            else
                                ReActivate();

                        }
                        else//Nếu đồng hồ máy tính<= ngày hết hạn-->Kiểm tra sự tồn tại của 2 file đảm bảo tránh trường hợp chỉnh đồng hồ máy tính
                        {
                            //Kiểm tra Maxdate
                            if (!isvalidMaxSystemStudyDate(_now))
                            {
                                _RegEprd.reWrite = false;
                                if (_RegEprd.ShowDialog() != DialogResult.OK)
                                    return false;
                                else
                                    ReActivate();
                            }
                            if (File.Exists(appexrd) || File.Exists(appexrd1))
                            {
                                ShowLeadtoolsMessage("This software has expired.  Contact the provider to order a new version.");
                                _RegEprd.CurrentExp = _exp;
                                if (_RegEprd.ShowDialog() != DialogResult.OK)
                                    return false;
                                else
                                    ReActivate();
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowLeadtoolsMessage("Checking expd Exception: \n" + ex.Message);
                return false;
            }
            finally
            {
                log.Trace("Finish checking exp----------------------------------------------------------------------------------------------------------");
            }
        }
        public bool isValidExpd(string key2check, ref string expdate)
        {

            log.Trace("Start checking exp with key={0}---------------------------------------------------------------------------------------------", key2check);
             SysSystemParameter _p = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("LICENSE").ExecuteSingle<SysSystemParameter>();
            try
            {
                m_ect.sPwd = "LICENSE";
                log.Trace("pwd={0}", m_ect.sPwd);
                if (_p != null && _p.SValue != null && _p.SValue.TrimEnd().TrimStart().Length > 0)
                {
                    List<string> lstlicense = new List<string>();
                    string clearLicense=m_ect.GiaiMa(_p.SValue);
                    log.Trace("encrypt license={0}       decrypt license={1}", _p.SValue, clearLicense);
                    lstlicense = clearLicense.Split(';').ToList<string>();
                    foreach (string s in lstlicense)
                    {
                        //Utility.ShowMsg(string.Format("{0}", s));
                        if (s.Split(':')[0] == key2check)
                        {
                            
                            //Check expire date now
                            expdate = m_ect.GiaiMa(s.Split(':')[1]);
                            long _exp = Convert.ToInt64(expdate);//Có dạng YYYY MM DD   
                           // expdate = s.Split(':')[1];// new DateTime(Convert.ToInt64(_exp.ToString().Substring(0, 4)), Convert.ToInt64(_exp.ToString().Substring(4, 2)), Convert.ToInt64(_exp.ToString().Substring(6, 2)));
                            long _now = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));
                            log.Trace("_exp= {0} , _now={1}", _exp.ToString(), _now.ToString());
                            //Kiểm tra ngày hết hạn so với ngày hiện tại-->Nếu hết hạn thì xóa file hết hạn đi để người dùng phải liên hệ cấp phép lại
                            if (_now >= _exp)
                            {
                                ShowLeadtoolsMessage("This software has expired.  Contact the provider to order a new version.");
                                return false;
                            }
                            else
                                return true;
                        }
                    }
                }
                else
                {
                   Utility.ShowMsg("License parameter not found");
                    return false;
                }
               
                return true;
            }
            catch (Exception ex)
            {
                ShowLeadtoolsMessage("Checking expd Exception: \n" + ex.Message);
                return false;
            }
            finally
            {
                log.Trace("Finish checking exp----------------------------------------------------------------------------------------------------------");
            }
        }
        public bool isValidExpd()
        {
            frm_RegEprd _RegEprd = new frm_RegEprd(pKey, DisplayLanguage, 0, _AppName);
            try
            {
                m_ect.sPwd = Utility.getAPPName(_AppName) + pKey;

                if (!File.Exists(expfile))
                {
                    ShowLeadtoolsMessage("File not found: " + expfile + "\nPlease contact the vendor");
                    if (_RegEprd.ShowDialog() != DialogResult.OK)
                        return false;
                    else
                        ReActivate();
                }
                else//Tồn tại file hạn sử dụng
                {
                    string obj = "";
                    using (StreamReader _reader = new StreamReader(expfile))
                    {
                        obj = _reader.ReadLine();
                        if (obj == null)
                        {
                            ShowLeadtoolsMessage("File content is not valid: " + expfile + "\nPlease contact the vendor");
                            if (_RegEprd.ShowDialog() != DialogResult.OK)
                                return false;
                            else
                                ReActivate();
                        }
                    }
                    if (obj != null)//File không empty
                    {
                        string _value = m_ect.GiaiMa(obj.Split('|')[0]);
                        //_value = _value.Split('|')[0];
                        if (!Utility.IsNumeric(_value))
                        {
                            ShowLeadtoolsMessage("File content is invalid: " + expfile + "\nPlease contact the vendor");
                            if (_RegEprd.ShowDialog() != DialogResult.OK)
                                return false;
                            else
                                ReActivate();
                        }

                        long _exp = Convert.ToInt64(_value);//Có dạng YYYY MM DD                                
                        long _now = Convert.ToInt64(GetYYYYMMDD(DateTime.Now));
                        //Kiểm tra ngày hết hạn so với ngày hiện tại-->Nếu hết hạn thì xóa file hết hạn đi để người dùng phải liên hệ cấp phép lại
                        if (_now >= _exp)
                        {
                            //Tạo 2 file hết hạn để đánh dấu đề phòng người dùng chỉnh đồng hồ
                            if (!File.Exists(appexrd)) File.Create(appexrd);
                            if (!File.Exists(appexrd1)) File.Create(appexrd1);
                            ShowLeadtoolsMessage("This software has expired.  Contact the provider to order a new version.");
                            _RegEprd.CurrentExp = _exp;
                            if (_RegEprd.ShowDialog() != DialogResult.OK)
                                return false;
                            else
                                ReActivate();

                        }
                        else//Nếu đồng hồ máy tính<= ngày hết hạn-->Kiểm tra sự tồn tại của 2 file đảm bảo tránh trường hợp chỉnh đồng hồ máy tính
                        {
                            //Kiểm tra Maxdate
                            if (!isvalidMaxSystemStudyDate(_now))
                            {
                                if (_RegEprd.ShowDialog() != DialogResult.OK)
                                    return false;
                                else
                                    ReActivate();
                            }
                            if (File.Exists(appexrd) || File.Exists(appexrd1))
                            {
                                ShowLeadtoolsMessage("This software has expired.  Contact the provider to order a new version.");
                                _RegEprd.CurrentExp = _exp;
                                if (_RegEprd.ShowDialog() != DialogResult.OK)
                                    return false;
                                else
                                    ReActivate();
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowLeadtoolsMessage("Checking expd Exception: \n" + ex.Message);
                return false;
            }
        }
        string maxstudydatefilepath = "ms4chkr.dll";
        /// <summary>
        /// Tự động lưu max studydate vào file và mã hóa. Giá trị này dùng để check sự hợp lệ phía client khi người dùng cố tình chỉnh đồng hồ
        /// </summary>
        /// <param name="studydate"></param>

        bool isvalidMaxSystemStudyDate(long _now)
        {
            try
            {
                xvect.Encrypt _ect = new xvect.Encrypt();
                _ect.UpdateAlgName(_ect.AlgName);
                _ect.sPwd = Utility.getAPPName(_AppName) + "ms4chkr.dll";
                if (!File.Exists(maxstudydatefilepath))
                {
                    ShowLeadtoolsMessage("File ms4chkr.dll not found");
                    return false;
                }
                long oldMax = 0l;
                if (File.Exists(maxstudydatefilepath))
                    oldMax = Utility.Int64Dbnull(_ect.GiaiMa(Utility.GetFirstValueFromFile(maxstudydatefilepath)), 0);
                if (_now < oldMax)//Ngày trên máy client đã bị chỉnh sửa
                {
                    ShowLeadtoolsMessage("The computer clock is modified wrongly. Please set datetime to be greater than(>) " + Utility.translateYYYYMMDD2DDMMYYYY(oldMax.ToString()));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowLeadtoolsMessage("CheckMaxDate().Exception-->" + ex.Message);
                return false;
            }
        }

        public bool isValidExpd4Services(DateTime StudyDate)
        {
            try
            {
                m_ect.sPwd = Utility.getAPPName(_AppName) + pKey;

                if (!File.Exists(expfile))
                {
                    log.Trace(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":File not found: " + expfile + "\nContact the vendor");
                    return false;
                }
                else//Tồn tại file hạn sử dụng
                {
                    string obj = "";
                    using (StreamReader _reader = new StreamReader(expfile))
                    {
                        obj = _reader.ReadLine();
                        if (obj == null)
                        {
                            log.Trace(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":File content is invalid: " + expfile + "\nContact the vendor");
                            return false;
                        }
                    }
                    if (obj != null)//File không empty
                    {
                        string _value = m_ect.GiaiMa(obj.Split('|')[0]);
                        //_value = _value.Split('|')[0];
                        if (!Utility.IsNumeric(_value))
                        {
                            log.Trace(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":File content is invalid: " + expfile + "\nContact the vendor");
                            return false;
                        }
                        long _exp = Convert.ToInt64(_value);//Có dạng YYYY MM DD                                
                        long _now = Convert.ToInt64(GetYYYYMMDD(StudyDate));
                        log.Trace(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "-->NOW=" + _now.ToString() + " - EXP: " + _exp.ToString());
                        //Kiểm tra ngày hết hạn so với ngày hiện tại-->Nếu hết hạn thì xóa file hết hạn đi để người dùng phải liên hệ cấp phép lại
                        if (_now >= _exp)
                        {
                            //Tạo 2 file hết hạn để đánh dấu đề phòng người dùng chỉnh đồng hồ
                            if (!File.Exists(appexrd)) File.Create(appexrd);
                            if (!File.Exists(appexrd1)) File.Create(appexrd1);
                            log.Trace(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":This software has expired.  Contact the provider to order a new version.\n" + _now.ToString() + " vs EXP: " + _exp.ToString());
                            return false;
                        }
                        else
                        {
                            if (File.Exists(appexrd) || File.Exists(appexrd1))
                            {
                                log.Trace(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":This software has expired.  Contact the provider to order a new version.\n" + _now.ToString() + " vs EXP: " + _exp.ToString());
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Trace(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":Checking expd Exception: \n" + ex.Message);
                return false;
            }
        }


        static void try2AlterContent(string filePath, string value)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(value);
                    writer.Flush();
                    writer.Close();
                }
            }
            catch
            {
            }
        }
        void ShowLeadtoolsMessage(string msg)
        {
            MessageBox.Show(
                      null,
                      msg,
                      "Warning",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Stop);
        }
        string m_strlstTimeFile = Application.StartupPath + @"\lstT.lst";





        private string TranslateLastTime(string value)
        {
            try
            {
                if (value.Trim().Length < 14) return "Wrong data";
                return "Date " + value.Substring(6, 2) + " Month " + value.Substring(4, 2) + " Year " + value.Substring(0, 4) + "-" + value.Substring(8, 2) + " hours " + value.Substring(10, 2) + " minutes " + value.Substring(12, 2) + " seconds";
            }
            catch
            {
                return "Wrong data";
            }
        }


        private string mv_RegKey = "";
        private string sTitle = "";
        bool CheckingKeyCompleted = false;


        void Save2File(string fileName, string Value)
        {
            try
            {
                using (StreamWriter _streamW = new StreamWriter(fileName))
                {
                    _streamW.WriteLine(Value);
                    _streamW.Flush();
                    _streamW.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error("LastTime writer Exception: " + ex.Message);
            }
        }


        public static string getRegKeyfromFile()
        {
            try
            {
                string fileName = Application.StartupPath + @"\regKey.dat";
                if (!File.Exists(fileName)) return "";
                using (StreamReader _streamR = new StreamReader(fileName))
                {
                    return _streamR.ReadLine();
                }

            }
            catch
            {
                return "";
            }
        }
        public static void SaveRegKey(string key)
        {
            try
            {
                using (StreamWriter _streamW = new StreamWriter(Application.StartupPath + @"\regKey.dat"))
                {
                    _streamW.WriteLine(key);
                    _streamW.Flush();
                    _streamW.Close();
                }
            }
            catch (Exception ex)
            {
                ShowMsg("SaveRegKey.Exception:\n" + ex.Message);
            }
        }

        public static void SaveExpd(string expd)
        {
            try
            {
                using (StreamWriter _streamW = new StreamWriter(expfile))
                {
                    _streamW.WriteLine(expd);
                    _streamW.Flush();
                    _streamW.Close();
                }
            }
            catch (Exception ex)
            {
                ShowMsg("SaveRegKey.Exception:\n" + ex.Message);
            }
        }
        void Try2DelLastTime()
        {
            try
            {
                File.Delete(m_strlstTimeFile);
            }
            catch
            {
            }
        }

        void Try2DelExpd()
        {
            try
            {
                File.Delete(expfile);
            }
            catch
            {
            }
        }
        void Try2DelRegKey()
        {
            try
            {
                File.Delete(Application.StartupPath + @"\regKey.dat");
            }
            catch
            {
            }
        }
        public void CreateExplsc()
        {
            try
            {
                DateTime _exp = DateTime.Now.AddMonths(1);
                string _strexp = GetYYYYMMDD(new DateTime(2012, 9, 30));

                m_ect.sPwd = pKey;

                string _value = m_ect.Mahoa(_strexp + m_ect.Fam_PWD);
                using (StreamWriter writer = new StreamWriter(expfile))
                {
                    writer.WriteLine(_value);
                    writer.Flush();
                    writer.Close();
                }
            }
            catch
            {
            }
        }
        #region CommonLib
        string GetYYYYMMDDHHMMSS(DateTime dtp)
        {
            return dtp.Year.ToString() + dtp.Month.ToString().PadLeft(2, '0') + dtp.Day.ToString().PadLeft(2, '0') + dtp.Hour.ToString().PadLeft(2, '0') + dtp.Minute.ToString().PadLeft(2, '0') + dtp.Second.ToString().PadLeft(2, '0');
        }
        string GetYYYYMMDD(System.DateTime tDate)
        {
            return tDate.Year.ToString() + Strings.Right("0" + tDate.Month.ToString(), 2) + Strings.Right("0" + tDate.Day.ToString(), 2);
        }
        public static bool AcceptQuestion(string msg, string title, bool YesNoOrOKCancel)
        {
            if (System.Windows.Forms.MessageBox.Show(msg, title, YesNoOrOKCancel == true ? System.Windows.Forms.MessageBoxButtons.YesNo : System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question) == (YesNoOrOKCancel == true ? System.Windows.Forms.DialogResult.Yes : System.Windows.Forms.DialogResult.OK))
                return true;
            else
                return false;
        }

        public static void ShowMsg(string Message)
        {
            System.Windows.Forms.MessageBox.Show(Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowMsg(string Message, string Title)
        {
            System.Windows.Forms.MessageBox.Show(Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowMsg(string Message, string Title, MessageBoxIcon Icon)
        {
            System.Windows.Forms.MessageBox.Show(Message, Title, MessageBoxButtons.OK, Icon);
        }
        #endregion


    }

}
