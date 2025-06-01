using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using VNS.Libs;

namespace VNS.Properties
{
    public class PropertyLib
    {
        public static TrathuocthuaProperties _TrathuocthuaProperties =null;// new TrathuocthuaProperties();
        public static QMSColorProperties _QMSColorProperties = null;// new TrathuocthuaProperties();

        public static QMSTiepdonProperties _QMSTiepdonProperties = null;

        public static ConfigProperties _ConfigProperties = null;// new ConfigProperties();

        public static KSKProperties _KskProperties = null;// new KSKProperties();
        public static DynamicInputProperties _DynamicInputProperties = null;//new DynamicInputProperties();

        public static QuaythuocProperties _QuaythuocProperties = null;// new QuaythuocProperties();
        public static HinhAnhProperties _HinhAnhProperties = null;// new HinhAnhProperties();
        public static BenhAnProperties _BenhAnProperties = null;// new BenhAnProperties();

        public static FTPProperties _FTPProperties = null;//new FTPProperties();
        public static PhieuxuatBNProperty _PhieuxuatBNProperty = null;//new FTPProperties();
        public static NoitruProperties _NoitruProperties = null;// new NoitruProperties();

        public static NhapkhoProperties _NhapkhoProperties = null;// new NhapkhoProperties();

        public static ChuyenkhoProperties _ChuyenkhoProperties = null;// new ChuyenkhoProperties();

        public static KCBProperties _KCBProperties = null;//new KCBProperties();

        public static AppProperties _AppProperties = null;

        public static HISCLSProperties _HISCLSProperties = null;//new HISCLSProperties();

        public static MayInProperties _MayInProperties = null;//new MayInProperties();

        public static ThamKhamProperties _ThamKhamProperties = null;// new ThamKhamProperties();

        public static ThanhtoanProperties _ThanhtoanProperties = null;//new ThanhtoanProperties();

        public static QMSPrintProperties _QMSPrintProperties = null;//new QMSPrintProperties();
        public static DuocNoitruProperties _DuocNoitruProperties = null;
        public static HisDuocProperties _HisDuocProperties = null;
        public static HISQMSProperties _HISQMSProperties = null;
        public static QheGiaThuocProperties _QheGiaThuocProperties = null;
        public static QheGiaCLSProperties _QheGiaCLSProperties = null;
        public static ThuocProperties _ThuocProperties = null;
        public static XMLProperties _xmlproperties = null;
        public static QMSTiepdonProperties GetQMSTiepdonProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolderQMS))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolderQMS);
                }
                var myProperty = new QMSTiepdonProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolderQMS,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (QMSTiepdonProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new QMSTiepdonProperties();
            }
        }

        public static QMSPrintProperties GetQMSPrintProperties(string folder)
        {
            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var myProperty = new QMSPrintProperties();
                string filePath = string.Format(@"{0}\{1}.xml", folder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (QMSPrintProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new QMSPrintProperties();
            }
        }
        public static QMSPrintProperties GetQMSPrintProperties()
        {
            try
            {
                string folder = globalVariables.m_strPropertiesFolderQMS;
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var myProperty = new QMSPrintProperties();
                string filePath = string.Format(@"{0}\{1}.xml", folder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (QMSPrintProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new QMSPrintProperties();
            }
        }

        public static QMSColorProperties GetQMSColorProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolderQMS))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolderQMS);
                }
                var myProperty = new QMSColorProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolderQMS,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (QMSColorProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new QMSColorProperties();
            }
        }

        public static HinhAnhProperties GetHinhAnhProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new HinhAnhProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (HinhAnhProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new HinhAnhProperties();
            }
        }
        public static KSKProperties GetKSKProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new KSKProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (KSKProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new KSKProperties();
            }
        }
        public static TrathuocthuaProperties GetTrathuocthuaProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new TrathuocthuaProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (TrathuocthuaProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new TrathuocthuaProperties();
            }
        }
        public static ConfigProperties GetConfigProperties(string _path)
        {
            try
            {
                if (!System.IO.Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                var myProperty = new ConfigProperties();
                string filePath = string.Format(@"{0}\{1}.xml", _path, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (ConfigProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {

                return new ConfigProperties();
            }
        }
        public static ConfigProperties GetConfigProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new ConfigProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (ConfigProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new ConfigProperties();
            }
        }

        public static DynamicInputProperties GetDynamicInputProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new DynamicInputProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (DynamicInputProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new DynamicInputProperties();
            }
        }

        public static QuaythuocProperties GetQuaythuocProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new QuaythuocProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (QuaythuocProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new QuaythuocProperties();
            }
        }

        public static BenhAnProperties GetBenhAnProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new BenhAnProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (BenhAnProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new BenhAnProperties();
            }
        }
        public static PhieuxuatBNProperty GetPhieuxuatBNProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new PhieuxuatBNProperty();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (PhieuxuatBNProperty)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new PhieuxuatBNProperty();
            }
        }
        public static FTPProperties GetFTPProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new FTPProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (FTPProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new FTPProperties();
            }
        }

        public static NoitruProperties GetNoitruProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new NoitruProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (NoitruProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new NoitruProperties();
            }
        }

        public static NhapkhoProperties GetNhapkhoProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new NhapkhoProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (NhapkhoProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new NhapkhoProperties();
            }
        }

        public static ChuyenkhoProperties GetChuyenkhoProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new ChuyenkhoProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (ChuyenkhoProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new ChuyenkhoProperties();
            }
        }

        public static KCBProperties GetKCBProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new KCBProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (KCBProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new KCBProperties();
            }
        }

        public static AppProperties GetAppPropertiess()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new AppProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (AppProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new AppProperties();
            }
        }

        public static HISCLSProperties GetHISCLSProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new HISCLSProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (HISCLSProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new HISCLSProperties();
            }
        }

        public static MayInProperties GetMayInProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new MayInProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (MayInProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new MayInProperties();
            }
        }

        public static ThamKhamProperties GetThamKhamProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new ThamKhamProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (ThamKhamProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new ThamKhamProperties();
            }
        }

        public static ThanhtoanProperties GetThanhtoanProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new ThanhtoanProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (ThanhtoanProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new ThanhtoanProperties();
            }
        }

        public static void SavePropertyV1( object _Property)
        {
            if (Utility.DoTrim(globalVariables.m_strPropertiesFolder_V1) != "")
            {
                Utility.CreateFolder(globalVariables.m_strPropertiesFolder_V1);
            }
            try
            {
                using (
                    var myWriter =
                        new StreamWriter(string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder_V1,
                                                       _Property.GetType().Name)))
                {
                    myWriter.AutoFlush = true;
                    var mySerializer = new XmlSerializer(_Property.GetType());
                    mySerializer.Serialize(myWriter, _Property);
                    myWriter.Close();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu cấu hình:\n" + ex.Message);
            }
        }
        public static void SaveProperty(string folder, object _Property)
        {
            if (Utility.DoTrim(folder) != "")
            {
                Utility.CreateFolder(folder);
            }
            try
            {
                using (
                    var myWriter =
                        new StreamWriter(string.Format(@"{0}\{1}.xml", folder,
                                                       _Property.GetType().Name)))
                {
                    myWriter.AutoFlush = true;
                    var mySerializer = new XmlSerializer(_Property.GetType());
                    mySerializer.Serialize(myWriter, _Property);
                    myWriter.Close();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu cấu hình:\n" + ex.Message);
            }
        }

        public static void SaveProperty(object _Property)
        {
          
            if (Utility.DoTrim(globalVariables.m_strPropertiesFolder) != "")
            {
                Utility.CreateFolder(globalVariables.m_strPropertiesFolder);
            }
            try
            {
                using (
                    var myWriter =
                        new StreamWriter(string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                       _Property.GetType().Name)))
                {
                    myWriter.AutoFlush = true;
                    var mySerializer = new XmlSerializer(_Property.GetType());
                    mySerializer.Serialize(myWriter, _Property);
                    myWriter.Close();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu cấu hình:\n" + ex.Message);
            }
        }


        public static DuocNoitruProperties GetDuocNoitruProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new DuocNoitruProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (DuocNoitruProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new DuocNoitruProperties();
            }
        }
        public static HisDuocProperties GetHisDuocProperties(string folder)
        {
            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var myProperty = new HisDuocProperties();
                string filePath = string.Format(@"{0}\{1}.xml", folder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (HisDuocProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new HisDuocProperties();
            }
        }
        public static HisDuocProperties GetHisDuocProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new HisDuocProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (HisDuocProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new HisDuocProperties();
            }
        }


        public static HISQMSProperties GetHISQMSProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolderQMS))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolderQMS);
                }
                var myProperty = new HISQMSProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolderQMS,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (HISQMSProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new HISQMSProperties();
            }
        }


        public static QheGiaThuocProperties GetQheGiaThuocProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new QheGiaThuocProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (QheGiaThuocProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new QheGiaThuocProperties();
            }
        }

        public static QheGiaCLSProperties GetQheGiaCLSProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new QheGiaCLSProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (QheGiaCLSProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new QheGiaCLSProperties();
            }
        }

        public static ThuocProperties GetThuocProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new ThuocProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (ThuocProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new ThuocProperties();
            }
        }

        public static XMLProperties GetXMLProperties()
        {
            try
            {
                if (!Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new XMLProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder,
                                                myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (XMLProperties) mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new XMLProperties();
            }
        }
    }
}