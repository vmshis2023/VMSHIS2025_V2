using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;

using System.Windows.Forms;
using System.Xml.Serialization;
using Leadtools;
using Leadtools.Codecs;
using Leadtools.Dicom;
using Leadtools.ImageProcessing;
using VMS.HIS.Cls.DicomGateWay.DicomVideoCaptureDemo.Common;
using VNS.Libs;
using VB6 = Microsoft.VisualBasic;
namespace VMS.HIS.Cls.DicomGateWay
{
     namespace DicomGateway
    {
        public class Convert2Dicom
        {
            DicomDataSet m_DS;
            DicomIod m_pDICOMIOD;
            bool m_bDataSetInitialized = false;
            bool _modified;
            public DicomClassType m_nClass;
            public DicomDataSetInitializeFlags m_nFlags;
            public bool started = false;
            private string LocalAETitle, RemoteAETitle, RemoteHost;
            int Port, TimeOut;
            public void Init(string LocalAETitle, string RemoteAETitle, string RemoteHost, int Port, int TimeOut, List<string> lstImgPath)
            {
                this.LocalAETitle = LocalAETitle;
                this.RemoteAETitle = RemoteAETitle;
                this.RemoteHost = RemoteHost;
                this.Port = Port;
                this.TimeOut = TimeOut;
                this.lstImgPath = lstImgPath;
                DicomEngine.Startup();
                Support.UnlockNO(false, "", "");
                DicomNet.Startup();
                started = true;
                m_DS = new DicomDataSet();
                m_nClass = DicomClassType.CTImageStorage;// DicomClassType.DXImageStoragePresentation;
                m_nFlags = DicomDataSetInitializeFlags.ExplicitVR | DicomDataSetInitializeFlags.LittleEndian;
            }
            public void ReInit(string LocalAETitle, string RemoteAETitle, string RemoteHost, int Port, int TimeOut, List<string> lstImgPath)
            {
                this.LocalAETitle = LocalAETitle;
                this.RemoteAETitle = RemoteAETitle;
                this.RemoteHost = RemoteHost;
                this.Port = Port;
                this.TimeOut = TimeOut;
                this.lstImgPath = lstImgPath;

            }
            List<string> lstImgPath = new List<string>();
            public List<string> _lstImgPath
            {
                get { return lstImgPath; }
                set { _lstImgPath = value; }
            }
            //private void cmdBrowse_Click(object sender, EventArgs e)
            //{
            //    lstImgPath.Clear();
            //    OpenFileDialog _OpenFileDialog = new OpenFileDialog();
            //    _OpenFileDialog.Multiselect = true;
            //    if (_OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        lstImgPath = _OpenFileDialog.FileNames.ToList<string>();
            //}
            void Reset()
            {
                m_DS.Reset();
                m_pDICOMIOD = null;
                m_bDataSetInitialized = false;

            }
            ClearCanvas.Dicom.Network.Scu.StorageScu _storageScu = null;
            List<ClearCanvas.Dicom.Network.Scu.StorageInstance> _storageList = new List<ClearCanvas.Dicom.Network.Scu.StorageInstance>();
            private void InstanceSent(IAsyncResult ar)
            {
                try
                {
                    ClearCanvas.Dicom.Network.Scu.StorageScu _storageScu = ar.AsyncState as ClearCanvas.Dicom.Network.Scu.StorageScu;
                    if (_storageScu.ResultStatus == ClearCanvas.Dicom.Network.DicomState.Success && _storageScu.FailureDescription.Trim() == "")
                    {
                        return;
                    }
                }
                catch
                {
                }
            }
            public void Send2PACS(List<string> lstInfor, DicomImagePhotometricInterpretationType _pType)
            {
                Reset();
                m_DS.Initialize(m_nClass, m_nFlags);
                CleanDSAndSetDefaultValues(m_nClass, m_DS, false);
                // m_DS.Load(Application.StartupPath + @"\ct.dcm", DicomDataSetLoadFlags.LoadAndClose);
                // Is this IOD in our list ?
                if ((m_pDICOMIOD = GetDSIOD(m_DS)) == null)
                {
                    MessageBox.Show("Could not create a new DICOM file");
                    return;
                }
                //Yes we have a valid dataset
                List<string> lstDcm = new List<string>();

                int idx = 1;
                string SeriesInstanceUID = lstInfor[6] + ".1";
                foreach (string file in lstImgPath)
                {
                    using (RasterCodecs _Codecs = new RasterCodecs())
                    {
                        RasterImage img = _Codecs.Load(file);

                        DicomElement pixelDataElement = m_DS.FindFirstElement(null, DicomTag.PixelData, true);
                        //DicomImageInformation imageInformation = m_DS.GetImageInformation(pixelDataElement, 0);
                        //imageInformation.PhotometricInterpretation,
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.PatientSex, lstInfor[2].ToUpper() == "NAM" ? "M" : "F", true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.AccessionNumber, lstInfor[4], true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.PatientID, lstInfor[0], true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.PatientName, Leadtools.DicomDemos.Utils.Bodau(lstInfor[1]), true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.PatientAge, lstInfor[3], true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.StudyID, lstInfor[5], true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.StudyDescription, "CR Modality - DCMGW", true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.SeriesDescription, "CR Modality - DCMGW", true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.StationName, "CR Modality", true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.InstitutionName, Leadtools.DicomDemos.Utils.Bodau(globalVariables.Branch_Name), true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.NumberOfFrames, "", true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.SamplesPerPixel, "1", true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.StudyInstanceUID, lstInfor[6], true);
                        //SeriesInstanceUID = lstInfor[6] + idx.ToString();
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.SeriesInstanceUID, SeriesInstanceUID, true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.SOPInstanceUID, SeriesInstanceUID + ".1." + idx.ToString(), true);
                        Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.InstanceNumber, idx.ToString(), true);
                        DicomDateValue date1 = new DicomDateValue();
                        date1.Day = DateTime.Now.Day;
                        date1.Month = DateTime.Now.Month;
                        date1.Year = DateTime.Now.Year;
                        DicomElement element = m_DS.FindFirstElement(null, DicomTag.StudyDate, true);
                        if (element != null)
                        {
                            m_DS.SetDateValue(element, date1);
                        }
                        element = m_DS.FindFirstElement(null, DicomTag.SeriesDate, true);
                        if (element != null)
                        {
                            m_DS.SetDateValue(element, date1);
                        }
                        element = m_DS.FindFirstElement(null, DicomTag.ContentDate, true);
                        if (element != null)
                        {
                            m_DS.SetDateValue(element, date1);
                        }
                        //GrayscaleCommand cmdGray = new GrayscaleCommand(16);
                        //cmdGray.Run(img);
                        //Leadtools.DicomDemos.Utils.SetTag(m_DS, DicomTag.StudyDate, DateTime.Now.ToString("yyyyMMdd"), true);
                        m_DS.SetImages(pixelDataElement, img, DicomImageCompressionType.None, _pType,
                            img.BitsPerPixel > 16 ? 16 : img.BitsPerPixel, 2, DicomSetImageFlags.AutoSetVoiLut);
                        string dcmfile = file.Replace(Path.GetExtension(file), ".DCM");
                        m_DS.Save(dcmfile, DicomDataSetSaveFlags.None);
                        lstDcm.Add(dcmfile);
                        idx++;
                    }
                }
                ClearCanvas.Dicom.Samples.SamplesForm _f = new ClearCanvas.Dicom.Samples.SamplesForm(lstDcm, LocalAETitle, RemoteAETitle, RemoteHost, Port,true,true);
                _f.ShowDialog();
                //_storageList.Clear();
                ////Send to PACS
                //foreach (string file2Stored in lstDcm)
                //{
                //    if (File.Exists(file2Stored))
                //    {
                //        _storageList.Add(new ClearCanvas.Dicom.Network.Scu.StorageInstance(file2Stored));
                //    }
                //}
                //_storageScu = new ClearCanvas.Dicom.Network.Scu.StorageScu(LocalAETitle, RemoteAETitle, RemoteHost, Port, TimeOut);
                //_storageScu.AddStorageInstanceList(_storageList);
                //_storageScu.BeginSend(InstanceSent, _storageScu);
            }
            #region Init DicomDataset
            DicomIod GetDSIOD(DicomDataSet pDS)
            {
                if (pDS != null)
                {
                    DicomElement pElement;
                    pElement = pDS.FindFirstElement(null, DicomTag.SOPClassUID, false);
                    if (pElement != null)
                    {
                        string pszText = null;
                        int nClass = -1;

                        pszText = pDS.GetConvertValue(pElement);
                        if (pszText != null)
                        {
                            nClass = GetClassFromUID(pszText);
                            if (nClass != -1)
                            {
                                return GetIODFromMyList(nClass);
                            }

                        }
                    }
                }
                return null;
            }

            DicomIod GetIODFromMyList(int nClass)
            {
                if (nClass == -1)
                    return null;
                return (DicomIodTable.Instance.FindClass((DicomClassType)nClass));
            }

            int GetClassFromUID(string pszUID)
            {
                for (int i = 0; i < Helper.m_DICOMUIDIOD.Length; i++)
                {
                    if (Helper.m_DICOMUIDIOD[i].pszUID == pszUID)
                        return (int)Helper.m_DICOMUIDIOD[i].nClass;
                }
                return -1;
            }
            void CleanDSAndSetDefaultValues
             (
             DicomClassType uClass,
             DicomDataSet pDicomDataSet,
             bool bInsertMissingElements
             )
            {
                CleanDataSet(uClass, pDicomDataSet);
                // Set some default values 
                SetDSDefaultValues(ref pDicomDataSet, bInsertMissingElements);
            }
            void InitDcmDS()
            {
                //DicomDataSet rspDs = new DicomDataSet();

                //rspDs.Initialize(DicomClassType.Undefined, DicomDataSetInitializeType.ExplicitVRLittleEndian);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.PatientName);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.PatientBirthDate);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.PatientBirthTime);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.PatientSex);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.EthnicGroup);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.PatientComments);

                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.StudyDate);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.StudyTime);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.AccessionNumber);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.StudyID);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.PatientID);
                //// Optional Keys
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.StudyDescription);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.ReferringPhysicianName);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.NumberOfStudyRelatedSeries);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.NumberOfStudyRelatedInstances);

                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.Modality);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.SeriesNumber);
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.SeriesDate);
                //// Optional Keys
                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.NumberOfSeriesRelatedInstances);

                //Leadtools.DicomDemos.Utils.InsertKeyElement(rspDs, ds, DicomTag.InstanceNumber);
                //    rspDs.InsertElement(null, false, DicomTag.PatientID, DicomVRType.UN, false, 0);

                //    rspDs.InsertElement(null, false, DicomTag.StudyInstanceUID, DicomVRType.UN, false, 0);

                //    rspDs.InsertElement(null, false, DicomTag.SeriesInstanceUID, DicomVRType.UN, false, 0);

                //rspDs.InsertElement(null, false, DicomTag.SOPInstanceUID, DicomVRType.UN, false, 0);
            }
            // Set some default values from a predefined table(m_DefaultElementValues)
            void SetDSDefaultValues(ref DicomDataSet pDicomDataSet, bool bInsertMissingElements)
            {

                int i;
                DicomTag pTag;
                DicomVRType nVR;
                DicomElement pElement;
                DateTime Time;
                string szValue;


                Time = DateTime.Now;
                // Loop through all the elements in the default value table
                for (i = 0; i < Helper.DefaultElementValues.Length; i++)
                {

                    pTag = DicomTagTable.Instance.Find(Helper.DefaultElementValues[i].nTag);
                    nVR = ((pTag != null) ? pTag.VR : DicomVRType.UN);
                    pElement = null;
                    pElement = pDicomDataSet.FindFirstElement(null, Helper.DefaultElementValues[i].nTag, false);
                    if (pElement == null)
                    {
                        // If the element is missing and the user of 
                        // this functions wants to add it , then add it 
                        if (bInsertMissingElements)
                        {
                            pElement = pDicomDataSet.InsertElement(null,
                                                                      false,
                                                                      Helper.DefaultElementValues[i].nTag,
                                                                      nVR,
                                                                      false,
                                                                      -1);
                        }
                    }
                    if (Helper.DefaultElementValues[i].nTag == DicomTag.DateOfSecondaryCapture)
                    {
                        szValue = string.Format("{0:D2}/{1:D2}/{2:D4}", Time.Month, Time.Day, Time.Year);
                        if (pElement != null)
                        {
                            pDicomDataSet.FreeElementValue(pElement);
                            pDicomDataSet.SetConvertValue(pElement, szValue, 1);
                        }
                    }
                    else
                        if (Helper.DefaultElementValues[i].nTag == DicomTag.TimeOfSecondaryCapture)
                    {
                        szValue = string.Format("{0:D2}:{1:D2}:{2:D4}.0", Time.Hour, Time.Minute, Time.Second);
                        if (pElement != null)
                        {
                            pDicomDataSet.FreeElementValue(pElement);
                            pDicomDataSet.SetConvertValue(pElement, szValue, 1);
                        }
                    }
                    else
                    {
                        if ((pElement != null) && CanUpdateElementValue(pDicomDataSet, pElement))
                        {
                            // Set the value for this element
                            pDicomDataSet.FreeElementValue(pElement);
                            pDicomDataSet.SetConvertValue(pElement, Helper.DefaultElementValues[i].pszValue, 1);
                        }
                    }
                }
                // Set different UIDs for this dataset 
                SetInstanceUIDs(pDicomDataSet);
                //Set instance numbers
                SetInstanceNumbers(pDicomDataSet, 1);
                // Update study date and time
                SetStudyDateAndTime(pDicomDataSet);
                // Set meta header info
                InsertMetaHeader(pDicomDataSet);
            }
            void SetInstanceUIDs(DicomDataSet pDicomDataSet)
            {
                DicomElement pElement;
                string pszInstanceGuid;

                // Set STUDY INSTANCE UID
                //pElement = pDicomDataSet.FindFirstElement(null, DicomTag.PatientName, true);
                //if (pElement == null)
                //{
                //    pElement = pDicomDataSet.InsertElement(null, false, DicomTag.PatientName, DicomVRType.UI, false, 0);
                //}
                //// Set STUDY INSTANCE UID
                //pElement = pDicomDataSet.FindFirstElement(null, DicomTag.PatientID, true);
                //if (pElement == null)
                //{
                //    pElement = pDicomDataSet.InsertElement(null, false, DicomTag.PatientID, DicomVRType.UI, false, 0);
                //}
                //// Set STUDY INSTANCE UID
                //pElement = pDicomDataSet.FindFirstElement(null, DicomTag.PatientSex, true);
                //if (pElement == null)
                //{
                //    pElement = pDicomDataSet.InsertElement(null, false, DicomTag.PatientSex, DicomVRType.UI, false, 0);
                //}
                //pElement = pDicomDataSet.FindFirstElement(null, DicomTag.AccessionNumber, true);
                //if (pElement == null)
                //{
                //    pElement = pDicomDataSet.InsertElement(null, false, DicomTag.AccessionNumber, DicomVRType.UI, false, 0);
                //}
                //pElement = pDicomDataSet.FindFirstElement(null, DicomTag.StudyDate, true);
                //if (pElement == null)
                //{
                //    pElement = pDicomDataSet.InsertElement(null, false, DicomTag.StudyDate, DicomVRType.UI, false, 0);
                //}

                // Set STUDY INSTANCE UID
                //pElement = pDicomDataSet.FindFirstElement(null, DicomTag.StudyInstanceUID, true);
                //if (pElement == null)
                //{
                //    pElement = pDicomDataSet.InsertElement(null, false, DicomTag.StudyInstanceUID, DicomVRType.UI, false, 0);
                //}


                // Set STUDY INSTANCE UID
                pElement = pDicomDataSet.FindFirstElement(null, DicomTag.StudyInstanceUID, false);
                if (pElement == null)
                {
                    pElement = pDicomDataSet.InsertElement(null, false, DicomTag.StudyInstanceUID, DicomVRType.UI, false, 0);
                }
                pszInstanceGuid = WinAPI.GenerateDicomUniqueIdentifier();
                pDicomDataSet.SetConvertValue(pElement, pszInstanceGuid, 1);

                // Set SERIES INSTANCE UID
                pElement = pDicomDataSet.FindFirstElement(null, DicomTag.SeriesInstanceUID, false);
                if (pElement == null)
                {
                    pElement = pDicomDataSet.InsertElement(null, false, DicomTag.SeriesInstanceUID, DicomVRType.UI, false, 0);
                }
                pDicomDataSet.FreeElementValue(pElement);
                pszInstanceGuid = WinAPI.GenerateDicomUniqueIdentifier();
                pDicomDataSet.SetConvertValue(pElement, pszInstanceGuid, 1);

                // Set SOP INSTANCE UID
                pszInstanceGuid = WinAPI.GenerateDicomUniqueIdentifier();
                pElement = pDicomDataSet.FindFirstElement(null, DicomTag.SOPInstanceUID, false);
                if (pElement == null)
                {
                    pElement = pDicomDataSet.InsertElement(null, false, DicomTag.SOPInstanceUID, DicomVRType.UI, false, 0);
                }
                pDicomDataSet.FreeElementValue(pElement);
                pDicomDataSet.SetConvertValue(pElement, pszInstanceGuid, 1);

                // Media Storage SOP Instance UID
                pElement = pDicomDataSet.FindFirstElement(null, DicomTag.MediaStorageSOPInstanceUID, false);
                if (pElement == null)
                {
                    pElement = pDicomDataSet.InsertElement(null, false, DicomTag.MediaStorageSOPInstanceUID, DicomVRType.UI, false, 0);
                }
                pDicomDataSet.FreeElementValue(pElement);
                pDicomDataSet.SetConvertValue(pElement, pszInstanceGuid, 1);
            }

            void SetInstanceNumbers(DicomDataSet pDicomDataSet, int nInstanceNumber)
            {
                DicomElement pElement;
                string szValue;

                szValue = nInstanceNumber.ToString();

                // Series number
                pElement = pDicomDataSet.FindFirstElement(null, DicomTag.SeriesNumber, false);
                if (pElement != null)
                {
                    pDicomDataSet.FreeElementValue(pElement);
                    pDicomDataSet.SetConvertValue(pElement, szValue, 1);
                }

                // Instance number
                pElement = pDicomDataSet.FindFirstElement(null, DicomTag.InstanceNumber, false);
                if (pElement != null)
                {
                    pDicomDataSet.FreeElementValue(pElement);
                    pDicomDataSet.SetConvertValue(pElement, szValue, 1);
                }
                // Study ID
                pElement = pDicomDataSet.FindFirstElement(null, DicomTag.StudyID, false);
                if (pElement != null)
                {
                    pDicomDataSet.FreeElementValue(pElement);
                    pDicomDataSet.SetConvertValue(pElement, szValue, 1);
                }

                szValue = "854125" + nInstanceNumber.ToString();
                // Accession number
                pElement = pDicomDataSet.FindFirstElement(null, DicomTag.AccessionNumber, false);
                if (pElement != null)
                {
                    pDicomDataSet.FreeElementValue(pElement);
                    pDicomDataSet.SetConvertValue(pElement, szValue, 1);
                }
            }

            void SetStudyDateAndTime(DicomDataSet pDicomDataSet)
            {
                DateTime Time;
                string szValue = null;
                DicomElement pElement;

                Time = DateTime.Now;

                // Set study date
                pElement = pDicomDataSet.FindFirstElement(null, DicomTag.StudyDate, true);
                if (pElement != null)
                {
                    szValue = string.Format("%02d/%02d/%04d", Time.Month, Time.Day, Time.Year);
                    pDicomDataSet.FreeElementValue(pElement);
                    pDicomDataSet.SetConvertValue(pElement, szValue, 1);
                }
                // Set content date
                pElement = pDicomDataSet.FindFirstElement(null, DicomTag.ContentDate, false);
                if (pElement != null)
                {
                    pDicomDataSet.FreeElementValue(pElement);
                    pDicomDataSet.SetConvertValue(pElement, szValue, 1);
                }
                // Set study time
                pElement = pDicomDataSet.FindFirstElement(null, DicomTag.StudyTime, true);
                if (pElement != null)
                {
                    szValue = string.Format("%02d:%02d:%04d.0", Time.Hour, Time.Minute, Time.Second);
                    pDicomDataSet.FreeElementValue(pElement);
                    pDicomDataSet.SetConvertValue(pElement, szValue, 1);
                }
                // Set content time
                pElement = pDicomDataSet.FindFirstElement(null, DicomTag.ContentTime, false);
                if (pElement != null)
                {
                    pDicomDataSet.FreeElementValue(pElement);
                    pDicomDataSet.SetConvertValue(pElement, szValue, 1);
                }

            }
            bool CanUpdateElementValue(DicomDataSet pDicomDataSet, DicomElement pElement)
            {
                if ((pElement != null) && (pDicomDataSet.GetConvertValue(pElement) != null))
                {
                    switch (pElement.Tag)
                    {
                        case DicomTag.MediaStorageSOPClassUID:
                        case DicomTag.SOPClassUID:
                        case DicomTag.Modality:
                            return false;
                    }
                }
                return true;
            }

            void InsertMetaHeader(DicomDataSet pDS)
            {
                DicomElement pElement;

                // Add File Meta Information Version
                pElement = pDS.FindFirstElement(null, DicomTag.FileMetaInformationVersion, false);
                if (pElement == null)
                {
                    pElement = pDS.InsertElement(null,
                                                  false,
                                                  DicomTag.FileMetaInformationVersion,
                                                  DicomVRType.OB,
                                                  false,
                                                  0);
                }
                if (pElement != null)
                {
                    byte[] cValue = { 0x00, 0x01 };
                    pDS.SetByteValue(pElement, cValue, 2);
                }
                // Implementation Class UID
                pElement = pDS.FindFirstElement(null, DicomTag.ImplementationClassUID, false);
                if (pElement == null)
                {
                    pElement = pDS.InsertElement(null, false, DicomTag.ImplementationClassUID, DicomVRType.UI, false, 0);
                }
                if (pElement != null)
                {
                    pDS.SetConvertValue(pElement, Helper.LEAD_IMPLEMENTATION_CLASS_UID, 1);
                }

                // Implementation Version Name
                pElement = pDS.FindFirstElement(null, DicomTag.ImplementationVersionName, false);
                if (pElement == null)
                {
                    pElement = pDS.InsertElement(null, false, DicomTag.ImplementationVersionName, DicomVRType.SH, false, 0);
                }
                if (pElement != null)
                {
                    pDS.SetConvertValue(pElement, Helper.LEAD_IMPLEMENTATION_VERSION_NAME, 1);
                }
            }

            void CleanDataSet(DicomClassType uClass, DicomDataSet pDicomDataSet)
            {
                DeleteEmptyElementsType3(uClass, pDicomDataSet);
                DeleteEmptyModulesOptional(uClass, pDicomDataSet);
            }

            void DeleteEmptyModulesOptional(DicomClassType uClass, DicomDataSet pDicomDataSet)
            {
                int nCountModule = DicomIodTable.Instance.GetModuleCount(uClass);
                DicomIod pIOD;

                for (int i = 0; i < nCountModule; i++)
                {
                    pIOD = DicomIodTable.Instance.FindModuleByIndex(uClass, i);
                    if ((pIOD != null) && (pIOD.Usage == DicomIodUsageType.OptionalModule))
                    {
                        DicomModule pModule = pDicomDataSet.FindModule(pIOD.ModuleCode);
                        if ((pModule != null) && IsEmptyModule(pModule, pDicomDataSet))
                            pDicomDataSet.DeleteModule(pIOD.ModuleCode);
                    }
                }
            }

            bool IsEmptyModule(DicomModule pModule, DicomDataSet pDicomDataSet)
            {
                if (pModule == null)
                    return true;

                bool bEmpty = true;
                for (UInt32 i = 0; i < pModule.Elements.Length; i++)
                {
                    if (pModule.Elements[i].Length == 0xFFFFFFFFU)
                        bEmpty = bEmpty && IsEmptySequence(pModule.Elements[i], pDicomDataSet);

                    else if (pModule.Elements[i].Length != 0)
                    {
                        bEmpty = false;
                    }
                }
                return bEmpty;
            }

            // Delete any optional element which has no value
            void DeleteEmptyElementsType3(DicomClassType uClass, DicomDataSet pDicomDataSet)
            {
                DicomElement pElementPrev = null;
                DicomElement pElement;
                DicomIod pIODClass = DicomIodTable.Instance.FindClass(uClass);
                if (pIODClass != null)
                {
                    DicomIod pIOD;

                    pElement = pDicomDataSet.GetFirstElement(null, false, true);
                    pElementPrev = null;
                    while (pElement != null)
                    {
                        pIOD = DicomIodTable.Instance.Find(pIODClass, pElement.Tag, DicomIodType.Element, false);

                        if ((pIOD != null) && (pIOD.Usage == DicomIodUsageType.OptionalElement))
                        {
                            // nLength==0 means (1) Sequence     or (2)Empty Element 

                            // Case 1: Sequence
                            if (pElement.Length == 0xFFFFFFFFU)
                            {
                                bool bEmptySequence = IsEmptySequence(pElement, pDicomDataSet);
                                if (bEmptySequence)
                                {
                                    //if deleting the first element, pElementPrev is NULL
                                    //Therefore we must call GetFirstElement
                                    pDicomDataSet.DeleteElement(pElement);
                                    pElement = pElementPrev;
                                    if (pElement == null)
                                        pElement = pDicomDataSet.GetFirstElement(null, false, true);
                                }
                            }

                            // Case 2: Empty Element
                            else if (pElement.Length == 0)
                            {
                                //if deleting the first element, pElementPrev is NULL
                                //Therefore we must call GetFirstElement
                                pDicomDataSet.DeleteElement(pElement);
                                pElement = pElementPrev;
                                if (pElement == null)
                                    pElement = pDicomDataSet.GetFirstElement(null, false, true);
                            }
                        }

                        pElementPrev = pElement;
                        pElement = pDicomDataSet.GetNextElement(pElement, false, true);
                    }
                }

            }

            bool IsEmptySequence(DicomElement pElementSequence, DicomDataSet pDicomDataSet)
            {
                DicomElement pElementItem;
                DicomElement pElement;
                bool bEmpty;


                bEmpty = true;
                pElementItem = pDicomDataSet.GetChildElement(pElementSequence, true);
                while (pElementItem != null)
                {
                    pElement = pDicomDataSet.GetChildElement(pElementItem, true);
                    while (pElement != null)
                    {
                        // If a sequence, make a recursive call
                        if (pElement.Length == 0xFFFFFFFFU)
                            bEmpty = bEmpty && IsEmptySequence(pElement, pDicomDataSet);

                        else if (pElement.Length != 0)
                        {
                            bEmpty = false;
                        }
                        pElement = pDicomDataSet.GetNextElement(pElement, true, true);
                    }
                    pElementItem = pDicomDataSet.GetNextElement(pElementItem, true, true);
                }
                return bEmpty;
            }
            #endregion
        }
        class WinAPI
        {
            [StructLayout(LayoutKind.Sequential)]
            public class SystemTime
            {
                public ushort Year;
                public ushort Month;
                public ushort DayOfWeek;
                public ushort Day;
                public ushort Hour;
                public ushort Minute;
                public ushort Second;
                public ushort Milliseconds;
            }

            [DllImport("kernel32.dll")]
            public static extern void GetSystemTime([In, Out] SystemTime lpSystemTime);

            [DllImport("kernel32.dll", EntryPoint = "SystemTimeToFileTime", SetLastError = true)]
            public static extern bool SystemTimeToFileTime([In] SystemTime lpSystemTime, ref FILETIME lpFileTime);

            [DllImport("kernel32.dll")]
            public static extern uint GetTickCount();

            public static ushort LOWORD(uint l)
            {
                return (ushort)(l & 0xffff);
            }

            public static ushort HIWORD(uint l)
            {
                return (ushort)((l >> 16) & 0xffff);
            }

            public static string GenerateDicomUniqueIdentifier()
            {
                SystemTime systemTime = new SystemTime();
                FILETIME fileTime = new FILETIME();
                uint Tick;
                uint HighWord;
                Random rand = new Random();
                GetSystemTime(systemTime);
                SystemTimeToFileTime(systemTime, ref fileTime);
                Tick = GetTickCount();
                HighWord = (uint)fileTime.dwHighDateTime + 0x146BF4;

                return string.Format("1.2.840.114257.1.1{0:D010}{1:D05}{2:D05}{3:D05}{4:D05}{5:D05}{6:D05}",
                   fileTime.dwLowDateTime,
                   LOWORD(HighWord),
                   HIWORD(HighWord | 0x10000000),
                   LOWORD((uint)rand.Next()),
                   HIWORD(Tick),
                   LOWORD(Tick),
                   LOWORD((uint)rand.Next()));
            }
        }

    }
    namespace Leadtools.DicomDemos
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public POINT pt;
        }

        public enum WaitReturn
        {
            Complete,
            Timeout,
        }

        /// <summary>
        /// Summary description for Scu.
        /// </summary>
        public class Utils
        {
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool PeekMessage(out MSG lpMsg, HandleRef hWnd,
                                           uint wMsgFilterMin, uint wMsgFilterMax,
                                           uint wRemoveMsg);

            [DllImport("user32.dll")]
            static extern bool TranslateMessage([In] ref MSG lpMsg);
            [DllImport("user32.dll")]
            static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

            const uint PM_REMOVE = 1;

            public static WaitReturn WaitForComplete(double mill, WaitHandle wh)
            {
                TimeSpan goal = new TimeSpan(DateTime.Now.AddMilliseconds(mill).Ticks);
                MSG msg = new MSG();
                HandleRef h = new HandleRef(null, IntPtr.Zero);

                do
                {
                    if (PeekMessage(out msg, h, 0, 0, PM_REMOVE))
                    {
                        TranslateMessage(ref msg);
                        DispatchMessage(ref msg);
                    }

                    if (wh.WaitOne(new TimeSpan(1), false))
                    {
                        return WaitReturn.Complete;
                    }

                    if (goal.CompareTo(new TimeSpan(DateTime.Now.Ticks)) < 0)
                    {
                        return WaitReturn.Timeout;
                    }

                } while (true);
            }
            public static string Bodau(string s)
            {

                int i = 0;
                string CH = null;
                //###################################################
                if (!string.IsNullOrEmpty(s.Trim()))
                {
                    for (i = 1; i <= s.Length; i++)
                    {

                        CH = VB6.Strings.Mid(s, i, 1);
                        switch (CH)
                        {
                            case "â":
                            case "ă":
                            case "ấ":
                            case "ầ":
                            case "ậ":
                            case "ẫ":
                            case "ẩ":
                            case "ắ":
                            case "ằ":
                            case "ẵ":
                            case "ẳ":
                            case "ặ":
                            case "á":
                            case "à":
                            case "ả":
                            case "ã":
                            case "ạ":
                                s = s.Replace(CH, "a");
                                break;
                            case "Â":
                            case "Ă":
                            case "Ấ":
                            case "Ầ":
                            case "Ậ":
                            case "Ẫ":
                            case "Ẩ":
                            case "Ắ":
                            case "Ằ":
                            case "Ẵ":
                            case "Ẳ":
                            case "Ặ":
                            case "Á":
                            case "À":
                            case "Ả":
                            case "Ã":
                            case "Ạ":
                                s = s.Replace(CH, "A");
                                break;
                            case "ó":
                            case "ò":
                            case "ỏ":
                            case "õ":
                            case "ọ":
                            case "ô":
                            case "ố":
                            case "ồ":
                            case "ổ":
                            case "ỗ":
                            case "ộ":
                            case "ơ":
                            case "ớ":
                            case "ờ":
                            case "ợ":
                            case "ở":
                            case "ỡ":
                                s = s.Replace(CH, "o");
                                break;
                            case "Ó":
                            case "Ò":
                            case "Ỏ":
                            case "Õ":
                            case "Ọ":
                            case "Ô":
                            case "Ố":
                            case "Ồ":
                            case "Ổ":
                            case "Ỗ":
                            case "Ộ":
                            case "Ơ":
                            case "Ớ":
                            case "Ờ":
                            case "Ợ":
                            case "Ở":
                            case "Ỡ":
                                s = s.Replace(CH, "O");
                                break;
                            case "ư":
                            case "ứ":
                            case "ừ":
                            case "ự":
                            case "ử":
                            case "ữ":
                            case "ù":
                            case "ú":
                            case "ủ":
                            case "ũ":
                            case "ụ":
                                s = s.Replace(CH, "u");
                                break;
                            case "Ư":
                            case "Ứ":
                            case "Ừ":
                            case "Ự":
                            case "Ử":
                            case "Ữ":
                            case "Ù":
                            case "Ú":
                            case "Ủ":
                            case "Ũ":
                            case "Ụ":
                                s = s.Replace(CH, "U");
                                break;
                            case "ê":
                            case "ế":
                            case "ề":
                            case "ệ":
                            case "ể":
                            case "ễ":
                            case "è":
                            case "é":
                            case "ẻ":
                            case "ẽ":
                            case "ẹ":
                                s = s.Replace(CH, "e");
                                break;
                            case "Ê":
                            case "Ế":
                            case "Ề":
                            case "Ệ":
                            case "Ể":
                            case "Ễ":
                            case "È":
                            case "É":
                            case "Ẻ":
                            case "Ẽ":
                            case "Ẹ":
                                s = s.Replace(CH, "E");
                                break;
                            case "í":
                            case "ì":
                            case "ị":
                            case "ỉ":
                            case "ĩ":
                                s = s.Replace(CH, "i");
                                break;
                            case "Í":
                            case "Ì":
                            case "Ỉ":
                            case "Ĩ":
                            case "Ị":
                                s = s.Replace(CH, "I");
                                break;
                            case "ý":
                            case "ỳ":
                            case "ỵ":
                            case "ỷ":
                            case "ỹ":
                                s = s.Replace(CH, "y");
                                break;
                            case "Ý":
                            case "Ỳ":
                            case "Ỵ":
                            case "Ỷ":
                            case "Ỹ":
                                s = s.Replace(CH, "Y");
                                break;
                            case "đ":
                                s = s.Replace(CH, "d");
                                break;
                            case "Đ":
                                s = s.Replace(CH, "D");
                                break;
                        }
                    }
                }
                return s;
            }
            public static WaitReturn WaitForComplete(double mill, WaitHandle completeHandle, WaitHandle resetTimeoutHandle)
            {
                TimeSpan goal = new TimeSpan(DateTime.Now.AddMilliseconds(mill).Ticks);
                MSG msg = new MSG();
                HandleRef h = new HandleRef(null, IntPtr.Zero);

                WaitHandle[] waitHandles = new WaitHandle[2] { resetTimeoutHandle, completeHandle };

                do
                {
                    if (PeekMessage(out msg, h, 0, 0, PM_REMOVE))
                    {
                        TranslateMessage(ref msg);
                        DispatchMessage(ref msg);
                    }

                    int index = WaitHandle.WaitAny(waitHandles, new TimeSpan(1), false);

                    if (index == WaitHandle.WaitTimeout)
                    {
                        if (goal.CompareTo(new TimeSpan(DateTime.Now.Ticks)) < 0)
                        {
                            return WaitReturn.Timeout;
                        }
                    }

                    else
                    {
                        Debug.Assert(index == 0 || index == 1);
                        AutoResetEvent autoEvent = waitHandles[index] as AutoResetEvent;
                        if (autoEvent == completeHandle)
                        {
                            return WaitReturn.Complete;
                        }
                        else if (autoEvent == resetTimeoutHandle)
                        {
                            Console.WriteLine("Reset Timer");
                            goal = new TimeSpan(DateTime.Now.AddMilliseconds(mill).Ticks);
                        }
                    }



                } while (true);
            }

            public static void EngineStartup()
            {
                DicomEngine.Startup();
            }

            public static void EngineShutdown()
            {
                DicomEngine.Shutdown();
            }

            public static void DicomNetStartup()
            {
                DicomNet.Startup();
            }

            public static void DicomNetShutdown()
            {
                DicomNet.Shutdown();
            }

            /// <summary>
            /// Helper method to get string value from a DICOM dataset.
            /// </summary>
            /// <param name="dcm">The DICOM dataset.</param>
            /// <param name="tag">Dicom tag.</param>
            /// <returns>String value of the specified DICOM tag.</returns>
            public static string GetStringValue(DicomDataSet dcm, long tag, bool tree)
            {
                DicomElement element;

                element = dcm.FindFirstElement(null, tag, tree);
                if (element != null)
                {
                    if (dcm.GetElementValueCount(element) > 0)
                    {
                        return dcm.GetConvertValue(element);
                    }
                }

                return "";
            }

            public static string GetStringValue(DicomDataSet dcm, long tag)
            {
                return GetStringValue(dcm, tag, true);
            }

#if (LTV15_CONFIG)
      public static string GetStringValue(DicomDataSet dcm, DicomTagType tag, bool tree)
      {
         return GetStringValue(dcm, (long)tag, tree);
      }

      public static string GetStringValue(DicomDataSet dcm, DicomTagType tag)
      {
      return GetStringValue(dcm,(long)tag);
      }
#endif

            public static StringCollection GetStringValues(DicomDataSet dcm, long tag)
            {
                DicomElement element;
                StringCollection sc = new StringCollection();

                element = dcm.FindFirstElement(null, tag, true);
                if (element != null)
                {
                    if (dcm.GetElementValueCount(element) > 0)
                    {
                        string s = dcm.GetConvertValue(element);
                        string[] items = s.Split('\\');

                        foreach (string value in items)
                        {
                            sc.Add(value);
                        }
                    }
                }

                return sc;
            }

#if (LTV15_CONFIG)
      public static StringCollection GetStringValues(DicomDataSet dcm, DicomTagType tag)
      {
         return GetStringValues(dcm, (long)tag);
      }
#endif

            public static byte[] GetBinaryValues(DicomDataSet dcm, long tag)
            {
                DicomElement element;

                element = dcm.FindFirstElement(null, tag, true);
                if (element != null)
                {
                    if (element.Length > 0)
                    {
                        return dcm.GetBinaryValue(element, (int)element.Length);
                    }
                }

                return null;
            }


#if (LTV15_CONFIG)
      public static byte[] GetBinaryValues(DicomDataSet dcm, DicomTagType tag)
      {
         return GetBinaryValues(dcm, (long)tag);
      }
#endif

            public static bool IsTagPresent(DicomDataSet dcm, long tag)
            {
                DicomElement element;

                element = dcm.FindFirstElement(null, tag, true);
                return (element != null);
            }

#if (LTV15_CONFIG)
      public static bool IsTagPresent(DicomDataSet dcm, DicomTagType tag)
      {
         return IsTagPresent(dcm, (long)tag);
      }
#endif

            public static bool IsAscii(string value)
            {
                return Encoding.UTF8.GetByteCount(value) == value.Length;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="dcm"></param>
            /// <param name="tag"></param>
            /// <param name="tagValue"></param>
            /// <returns></returns>
            public static DicomExceptionCode SetTag(DicomDataSet dcm, long tag, object tagValue, bool tree)
            {
                DicomExceptionCode ret = DicomExceptionCode.Success;
                DicomElement element;

                if (tagValue == null)
                    return DicomExceptionCode.Parameter;

                element = dcm.FindFirstElement(null, tag, tree);
                if (element == null)
                {
                    element = dcm.InsertElement(null, false, tag, DicomVRType.UN, false, 0);
                }

                if (element == null)
                    return DicomExceptionCode.Parameter;

                try
                {
                    string s = tagValue.ToString();
                    if (IsAscii(s))
                        dcm.SetConvertValue(element, s, 1);
                    else
                        dcm.SetStringValue(element, s, DicomCharacterSetType.UnicodeInUtf8);
                }
                catch (DicomException de)
                {
                    ret = de.Code;
                }

                return ret;
            }

            public static DicomExceptionCode SetTag(DicomDataSet dcm, long tag, object tagValue)
            {
                return SetTag(dcm, tag, tagValue, true);
            }

#if (LTV15_CONFIG)
      public static void SetTag(DicomDataSet dcm, DicomTagType seq,DicomTagType tag, object tagValue)
      {
         SetTag(dcm, (long)seq,(long)tag, tagValue);
      }
#endif

            public static void SetTag(DicomDataSet dcm, long Sequence, long Tag, object TagValue)
            {
                DicomElement seqElement = dcm.FindFirstElement(null, Sequence, true);
                DicomElement seqItem = null;
                DicomElement item = null;

                if (seqElement == null)
                {
                    seqElement = dcm.InsertElement(null, false, Tag, DicomVRType.SQ, true, -1);
                }

                seqItem = dcm.GetChildElement(seqElement, false);
                if (seqItem == null)
                {
#if (LTV15_CONFIG)
              seqItem = dcm.InsertElement(seqElement, true, DicomTagType.SequenceDelimitationItem, DicomVRType.SQ, true, -1);
#else
                    seqItem = dcm.InsertElement(seqElement, true, DicomTag.SequenceDelimitationItem, DicomVRType.SQ, true, -1);
#endif
                }

                item = dcm.GetChildElement(seqItem, true);
                while (item != null)
                {
#if (LTV15_CONFIG)
              if ((long)item.Tag == Tag)
                  break;
#else
                    if (item.Tag == Tag)
                        break;
#endif

                    item = dcm.GetNextElement(item, true, true);
                }

                if (item == null)
                {
                    item = dcm.InsertElement(seqItem, true, Tag, DicomVRType.UN, false, -1);
                }
                dcm.SetConvertValue(item, TagValue.ToString(), 1);
            }


#if (LTV15_CONFIG)
      public static DicomExceptionCode SetTag(DicomDataSet dcm, DicomTagType tag, object tagValue)
      {
         return SetTag(dcm, (long)tag, tagValue);
      }

      public static DicomExceptionCode SetTag(DicomDataSet dcm, DicomTagType tag, object tagValue, bool tree)
      {
          return SetTag(dcm, (long)tag, tagValue, tree);
      }

#endif

            public static DicomExceptionCode SetTag(DicomDataSet dcm, long tag, byte[] tagValue)
            {
                DicomExceptionCode ret = DicomExceptionCode.Success;
                DicomElement element;

                if (tagValue == null)
                    return DicomExceptionCode.Parameter;

                element = dcm.FindFirstElement(null, tag, true);
                if (element == null)
                {
                    element = dcm.InsertElement(null, false, tag, DicomVRType.UN, false, 0);
                }

                dcm.SetBinaryValue(element, tagValue, tagValue.Length);

                return ret;
            }

#if (LTV15_CONFIG)
      public static DicomExceptionCode InsertKeyElement(DicomDataSet dcmRsp, DicomDataSet dcmReq, DicomTagType tag)
      {
         return InsertKeyElement(dcmRsp, dcmReq, (long)tag);
      }
#endif

            public static DicomExceptionCode InsertKeyElement(DicomDataSet dcmRsp, DicomDataSet dcmReq, long tag)
            {
                DicomExceptionCode ret = DicomExceptionCode.Success;
                DicomElement element;

                try
                {
                    element = dcmReq.FindFirstElement(null, tag, true);
                    if (element != null)
                    {
                        dcmRsp.InsertElement(null, false, tag, DicomVRType.UN, false, 0);
                    }
                }
                catch (DicomException de)
                {
                    ret = de.Code;
                }

                return ret;
            }


#if (LTV15_CONFIG)
       public static DicomExceptionCode SetKeyElement(DicomDataSet dcmRsp, DicomTagType tag, object tagValue)
       {
           return SetKeyElement(dcmRsp, (long)tag, tagValue);
       }

       public static DicomExceptionCode SetKeyElement(DicomDataSet dcmRsp, DicomTagType tag, object tagValue, bool tree)
       {
           return SetKeyElement(dcmRsp, (long)tag, tagValue, tree);
       }
#endif

            public static DicomExceptionCode SetKeyElement(DicomDataSet dcmRsp, long tag, object tagValue, bool tree)
            {
                DicomExceptionCode ret = DicomExceptionCode.Success;
                DicomElement element;

                if (tagValue == null)
                    return DicomExceptionCode.Parameter;

                try
                {
                    element = dcmRsp.FindFirstElement(null, tag, tree);
                    if (element != null)
                    {
                        string s = tagValue.ToString();
                        if (IsAscii(s))
                            dcmRsp.SetConvertValue(element, s, 1);
                        else
                            dcmRsp.SetStringValue(element, s, DicomCharacterSetType.UnicodeInUtf8);
                    }
                }
                catch (DicomException de)
                {
                    ret = de.Code;
                }

                return ret;
            }

            public static DicomExceptionCode SetKeyElement(DicomDataSet dcmRsp, long tag, object tagValue)
            {
                return SetKeyElement(dcmRsp, tag, tagValue, true);
            }

            public static UInt16 GetGroup(long tag)
            {
                return ((UInt16)(tag >> 16));
            }

            public static int GetElement(long tag)
            {
                return ((UInt16)(tag & 0xFFFF));
            }


            // Creates a properly formatted Dicom Unique Identifier (VR type of UI) value

            private static String _prevTime;
            private static String _leadRoot = null;
            private static Object _lock = new object();
            private static int _count = 0;
            private const int _maxCount = int.MaxValue;

            // UID is comprised of the following components
            // {LEAD Root}.{ProcessID}.{date}.{time}.{fraction seconds}.{counter}
            // {18 +      1 + 10 +    1 + 8 +1 + 6+ 1 + 7              + 10}
            // Total max length is 63 characters
            public static string GenerateDicomUniqueIdentifier()
            {
                try
                {
                    lock (_lock)
                    {
                        // yyyy     four digit year
                        // MM       month from 01 to 12
                        // dd       01 to 31
                        // HH       hours using a 24-hour clock form 00 to 23
                        // mm       minute 00 to 59
                        // ss       second 00 to 59
                        // fffffff  ten millionths of a second
                        const string dateFormatString = "yyyyMMdd.HHmmss.fffffff";

                        string sUidRet = "";
                        if (_leadRoot == null)
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.Append("1.2.840.114257.1.1");

                            // Process Id
                            sb.AppendFormat(".{0}", (uint)Process.GetCurrentProcess().Id);

                            _leadRoot = sb.ToString();

                            _prevTime = DateTime.UtcNow.ToString(dateFormatString);
                        }

                        StringBuilder uid = new StringBuilder();
                        uid.Append(_leadRoot);

                        String time = DateTime.UtcNow.ToString(dateFormatString);
                        if (time.Equals(_prevTime))
                        {
                            if (_count == _maxCount)
                                throw new Exception("GenerateDicomUniqueIdentifier error -- max count reached.");

                            _count++;
                        }
                        else
                        {
                            _count = 1;
                            _prevTime = time;
                        }

                        uid.AppendFormat(".{0}.{1}", time, _count);

                        sUidRet = uid.ToString();

                        // This should not happen
                        if (sUidRet.Length > 64)
                            sUidRet = sUidRet.Substring(0, 64);

                        return sUidRet;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public static bool IsLocalIPAddress(string hostNameOrAddress)
            {
                if (hostNameOrAddress.ToLower() == Dns.GetHostName().ToLower())
                {
                    return true;
                }
                else
                {
                    IPAddress serviceAddress;

                    if (IPAddress.TryParse(hostNameOrAddress, out serviceAddress))
                    {
                        if (IPAddress.IsLoopback(serviceAddress))
                        {
                            return true;
                        }
                        else
                        {
                            IPAddress[] localAddresses;


                            localAddresses = Dns.GetHostAddresses(Dns.GetHostName());

                            foreach (IPAddress localAddress in localAddresses)
                            {
                                if (localAddress.Equals(serviceAddress))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                return false;
            }


            public static System.Net.IPAddress ResolveIPAddress(string hostNameOrAddress)
            {
                IPAddress[] addresses;
                addresses = Dns.GetHostAddresses(hostNameOrAddress.Trim());
                if (addresses == null || addresses.Length == 0)
                {
                    throw new ArgumentException("Invalid hostNameOrAddress parameter.");
                }
                else
                {
                    foreach (IPAddress address in addresses)
                    {
                        if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ||
                           address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        {
                            return address;
                        }
                    }
                    throw new ArgumentException("Could not resolve a valid host Address. Address must conform to IPv4 or IPv6.");
                }
            }

#if LEADTOOLS_V175_OR_LATER

        // Returns string.empty if valid
        // Otherwise, returns an error message
        public static bool IsValidHostnameOrAddress(string hostNameOrAddress, out string error)
        {
            bool isValid = true;
            error = string.Empty;

            if ((hostNameOrAddress == null) || (string.IsNullOrEmpty(hostNameOrAddress.Trim())))
            {
                error = "Host address must be non-empty";
                return false;
            }
            try
            {
                Utils.ResolveIPAddress(hostNameOrAddress);
            }
            catch (Exception exception)
            {
                error = exception.Message;
                isValid = false;
            }
            return isValid;
        }

        public static bool IsValidApplicationEntity(string aeTitle, out string error)
        {
            error = string.Empty;

            if (aeTitle == null)
            {
                error = "Application Entity must not be empty";
                return false;
            }

            aeTitle = aeTitle.Trim();
            if (string.IsNullOrEmpty(aeTitle))
            {
                error = "Application Entity must not be empty";
                return false;
            }

            if (aeTitle.Length > 16)
            {
                error = "Application Entity must contain 16 characters or less.";
                return false;
            }

            if (aeTitle.Contains("\\"))
            {
                error = "Application Entity must not contain the '\\' character";
                return false;
            }

            return true;
        }
#endif // LEADTOOLS_V175_OR_LATER
        }

        [Serializable]
        public class DicomAE
        {
            public DicomAE()
            {
                _sAE = string.Empty;
                _sIP = string.Empty;
                _port = 0;
                _timeout = 0;
                _useTls = false;
            }

            public DicomAE(string sAE, string sIP, int port, int timeout, bool useTls)
            {
                _sAE = sAE;
                _sIP = sIP;
                _port = port;
                _timeout = timeout;
                _useTls = useTls;
            }

            public string AE
            {
                get
                {
                    return _sAE;
                }

                set
                {
                    _sAE = value;
                }
            }

            public string IPAddress
            {
                get
                {
                    return _sIP;
                }

                set
                {
                    _sIP = value;
                }
            }

            public int Port
            {
                get
                {
                    return _port;
                }

                set
                {
                    _port = value;
                }
            }

            public int Timeout
            {
                get
                {
                    return _timeout;
                }

                set
                {
                    _timeout = value;
                }
            }

            public bool UseTls
            {
                get
                {
                    return _useTls;
                }

                set
                {
                    _useTls = value;
                }
            }



            private string _sAE;
            private string _sIP;
            private int _port;
            private int _timeout;
            private bool _useTls;
        }


        public class DicomDemoSettingsManager
        {
            public static string GlobalPacsConfigFilename
            {
                get
                {
                    return "GlobalPacs.config";
                }
            }

            public static string GlobalPacsConfigFullFileName
            {
                get
                {
                    return Path.Combine(Application.StartupPath, GlobalPacsConfigFilename);
                }
            }

#if LEADTOOLS_V175_OR_LATER
        public static System.Configuration.Configuration GetGlobalPacsConfiguration()
        {
            ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
            configFile.ExeConfigFilename = DicomDemoSettingsManager.GlobalPacsConfigFullFileName;
            configFile.MachineConfigFilename = DicomDemoSettingsManager.GlobalPacsConfigFullFileName;
            System.Configuration.Configuration mappedConfiguration = ConfigurationManager.OpenMappedMachineConfiguration(configFile);
            return mappedConfiguration;

        }

        public static System.Configuration.Configuration GetGlobalPacsAddinsConfiguration(string ServiceDirectory)
        {
            string addInsConfigFile = Path.Combine(ServiceDirectory, @"..\\" + GlobalPacsConfigFilename);
            ExeConfigurationFileMap addInsConfigFileMap = new ExeConfigurationFileMap();
            addInsConfigFileMap.MachineConfigFilename = addInsConfigFile;
            addInsConfigFileMap.ExeConfigFilename = addInsConfigFile;
            System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedMachineConfiguration(addInsConfigFileMap);
            return configuration;
        }
#endif

            public const string StorageDataAccessConfiguration = "storageDataAccessConfiguration175";
            public const string LoggingDataAccessConfiguration = "loggingDataAccessConfiguration175";
            public const string MediaCreationDataAccessConfiguration = "mediaCreationDataAccessConfiguration175";
            public const string UserManagementConfigurationSample = "userManagementConfigurationSample175";
            public const string WorkListDataAccessConfiguration = "workListDataAccessConfiguration175";
            public const string WorkstationDataAccessConfiguration = "workstationDataAccessConfiguration175";
            public const string OptionsConfiguration = "optionsConfiguration175";
            public const string AeManagementConfiguration = "aeManagementConfiguration175";
            public const string AePermissionManagementConfiguration = "aePermissionManagementConfiguration175";
            public const string PermissionManagementConfiguration = "permissionManagementConfiguration175";
            public const string ForwardConfiguration = "forwardConfiguration175";
            public const string DownloadJobsConfiguration = "DownloadJobsConfiguration175";
            public const string PatientRightsConfiguration = "patientRightsConfiguration175";
            public const string ExternalStoreConfiguration = "externalStoreConfiguration175";

            public const string ProductNameStorageServer = "StorageServer";

            public const string ProductNameDemoServer = "DemoServer";

            public const string ProductNameWorkstation = "Workstation";

            public const string ProductNamePatientUpdater = "PatientUpdater";

            public const string ProductNameMedicalViewer = "MedicalViewer";

            public const string ProductDentalApp = "DentalClient";

            public const string ProductNameGateway = "Gateway";

            public static void RunPacsConfigDemo()
            {
                string caption = "Note";
                string message = "Please run the PACSConfigDemo to configure this and other PACS Framework demos.\n\nWould you like to run the PACSConfigDemo now?";

                DialogResult dr = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (DialogResult.Yes == dr)
                {
                    string pacsConfigDemoFileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "CSPacsConfigDemo_Original.exe");

                    if (!File.Exists(pacsConfigDemoFileName))
                    {
                        pacsConfigDemoFileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "CSPacsConfigDemo.exe");
                    }

                    if (File.Exists(pacsConfigDemoFileName))
                    {
                        Process pacConfigProcess = new Process();
                        pacConfigProcess.StartInfo.FileName = pacsConfigDemoFileName;

                        pacConfigProcess.Start();
                        pacConfigProcess.WaitForExit();
                    }
                    else
                    {
                        MessageBox.Show("Could not find the CSPacsConfigDemo.", "Warning", MessageBoxButtons.OK);
                    }
                }
            }

            public static void SaveSettings(string demoName, DicomDemoSettings settings)
            {
                try
                {
                    string filename = GetSettingsFilename(demoName);
                    XmlSerializer xs = new XmlSerializer(typeof(DicomDemoSettings));

                    using (TextWriter xmlTextWriter = new StreamWriter(filename))
                    {
                        xs.Serialize(xmlTextWriter, settings);
                        xmlTextWriter.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static DicomDemoSettings LoadSettings(string demoName)
            {
                XmlSerializer SerializerObj = new XmlSerializer(typeof(DicomDemoSettings));
                string filename = GetSettingsFilename(demoName);

                if (File.Exists(filename))
                {
                    DicomDemoSettings settings;

                    try
                    {
                        // Create a new file stream for reading the XML file
                        using (TextReader ReadFileStream = new StreamReader(filename))
                        {
                            // Load the object saved above by using the Deserialize function
                            settings = (DicomDemoSettings)SerializerObj.Deserialize(ReadFileStream);

                            // Cleanup
                            ReadFileStream.Close();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    SerializerObj = null;

                    return settings;
                }

                return null;
            }


            [DllImport("shfolder.dll", CharSet = CharSet.Auto)]
            private static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, StringBuilder lpszPath);

            private const int CommonDocumentsFolder = 0x2e;

            public static string GetFolderPath()
            {
                StringBuilder lpszPath = new StringBuilder(260);
                // CommonDocuments is the folder than any Vista user (including 'guest') has read/write access
                SHGetFolderPath(IntPtr.Zero, (int)CommonDocumentsFolder, IntPtr.Zero, 0, lpszPath);
                string path = lpszPath.ToString();
                new FileIOPermission(FileIOPermissionAccess.PathDiscovery, path).Demand();
                return path;
            }

            public enum InstallPlatform
            {
                win32 = 0,
                x64 = 1
            };

            public static string GetSettingsFilename(string demo, InstallPlatform platform)
            {
                string commonFolder = GetFolderPath();
                string sPlatform = "32";

                if (platform == InstallPlatform.x64)
                {
                    sPlatform = "64";
                }
                else
                {
                    sPlatform = "32";
                }

                string ext = Path.GetExtension(demo);
                string name = Path.GetFileNameWithoutExtension(demo);

#if (LTV19_CONFIG)
            string settingsFilename = string.Format("{0}\\{1}{2}{3}_19.xml", commonFolder, name, sPlatform, ext);
#elif (LTV18_CONFIG)
         string settingsFilename = string.Format("{0}\\{1}{2}{3}_18.xml", commonFolder, name, sPlatform, ext);
#elif (LTV175_CONFIG)
         string settingsFilename = string.Format("{0}\\{1}{2}{3}_175.xml", commonFolder, name, sPlatform, ext);
#else
                string settingsFilename = string.Format("{0}\\{1}{2}{3}_17.xml", commonFolder, name, sPlatform, ext);
#endif
                return settingsFilename;
            }

            public static string GetSettingsFilename(string demo)
            {
                string commonFolder = GetFolderPath();
                if (Is64Process())
                {
                    return GetSettingsFilename(demo, InstallPlatform.x64);
                }
                else
                {
                    return GetSettingsFilename(demo, InstallPlatform.win32);
                }
            }

            public static bool Is64Process()
            {
                return IntPtr.Size == 8;
            }

            public static string GetClientCertificateFullPath()
            {
                string fileClientCertificate = Application.StartupPath + "\\client.pem";
                if (!File.Exists(fileClientCertificate))
                {
                    fileClientCertificate = string.Empty;
                }
                return fileClientCertificate;
            }

            public static string GetClientCertificatePassword()
            {
                string password = string.Empty;
                string fileClientCertificate = GetClientCertificateFullPath();
                if (!string.IsNullOrEmpty(fileClientCertificate))
                    password = "test";
                return password;
            }
        }

        public enum DicomRetrieveMode
        {
            CMove = 0,
            CGet = 1,
        }

        [Serializable]
        public class DicomDemoSettings : IWorkstationSettings
        {
            public DicomDemoSettings()
            {
                ServerList = new List<DicomAE>();
                FileList = new List<string>();
                ClientAe = new DicomAE();
                _logLowLevel = false;
                _showHelpOnStart = true;
                _isPreconfigured = false;
                _firstRun = true;
                _compression = 0;
                _broadQuery = false;
                _excludeList = new List<long>();
                _storageClassList = new List<string>();
                _dicomRetrieveMode = DicomRetrieveMode.CMove;
            }


            private List<DicomAE> _serverArrayList;
            private List<string> _fileArrayList;
            private DicomAE _clientAe;
            private string _defaultStore;
            private string _defaultImageQuery;
            private string _defaultMwlQuery;
            private string _defaultMpps;
            private string _clientCertificate;
            private string _clientPrivateKey;
            private string _clientPrivateKeyPassword;
            private string _workstationServer;
            private string _highLevelStorageServer;
            private bool _logLowLevel;
            private bool _showHelpOnStart;
            private bool _isPreconfigured;
            private bool _firstRun;
            private int _compression;
            private string _dataPath;
            private bool _broadQuery;
            private List<long> _excludeList;
            private string _temporaryDirectory;
            private string _implementationClass;
            private string _protocolVersion;
            private string _implementationVersionName;
            private List<string> _storageClassList;
            private DicomRetrieveMode _dicomRetrieveMode;

            public DicomAE GetServer(string sa)
            {
                DicomAE ret = null;
                foreach (DicomAE ae in _serverArrayList)
                {
                    if (string.Compare(ae.AE, sa, true) == 0)
                        ret = ae;
                }
                return ret;
            }

            public string GetServerAe(string sa)
            {
                DicomAE ae = GetServer(sa);
                if (ae != null)
                    return ae.AE;
                else
                    return string.Empty;
            }

            public int GetServerPort(string sa)
            {
                DicomAE ae = GetServer(sa);
                if (ae != null)
                    return ae.Port;
                else
                    return 0;
            }

            //[XmlElement("servers")]
            public List<DicomAE> ServerList
            {
                get
                {
                    return _serverArrayList;
                }

                set
                {
                    _serverArrayList = value;
                }
            }

            public List<string> FileList
            {
                get
                {
                    return _fileArrayList;
                }

                set
                {
                    _fileArrayList = value;
                }
            }

            public DicomAE ClientAe
            {
                get { return _clientAe; }
                set { _clientAe = value; }
            }

            public string DefaultStore
            {
                get { return _defaultStore; }
                set { _defaultStore = value; }
            }


            public string DefaultImageQuery
            {
                get { return _defaultImageQuery; }
                set { _defaultImageQuery = value; }
            }


            public string DefaultMwlQuery
            {
                get { return _defaultMwlQuery; }
                set { _defaultMwlQuery = value; }
            }

            public string DefaultMpps
            {
                get { return _defaultMpps; }
                set { _defaultMpps = value; }
            }


            public string ClientCertificate
            {
                get { return _clientCertificate; }
                set { _clientCertificate = value; }
            }

            public string ClientPrivateKey
            {
                get { return _clientPrivateKey; }
                set { _clientPrivateKey = value; }
            }

            public string ClientPrivateKeyPassword
            {
                get { return _clientPrivateKeyPassword; }
                set { _clientPrivateKeyPassword = value; }
            }

            public string WorkstationServer
            {
                get { return _workstationServer; }
                set { _workstationServer = value; }
            }

            public string HighLevelStorageServer
            {
                get { return _highLevelStorageServer; }
                set { _highLevelStorageServer = value; }
            }

            public bool LogLowLevel
            {
                get { return _logLowLevel; }
                set { _logLowLevel = value; }
            }

            public bool ShowHelpOnStart
            {
                get { return _showHelpOnStart; }
                set { _showHelpOnStart = value; }
            }

            public bool IsPreconfigured
            {
                get { return _isPreconfigured; }
                set { _isPreconfigured = value; }
            }

            public int Compression
            {
                get { return _compression; }
                set { _compression = value; }
            }

            public bool FirstRun
            {
                get { return _firstRun; }
                set { _firstRun = value; }
            }

            public string DataPath
            {
                get { return _dataPath; }
                set { _dataPath = value; }
            }

            public bool BroadQuery
            {
                get { return _broadQuery; }
                set { _broadQuery = value; }
            }

            public List<long> ExcludeList
            {
                get { return _excludeList; }
                set { _excludeList = value; }
            }

            public List<string> StorageClassList
            {
                get { return _storageClassList; }
                set { _storageClassList = value; }
            }

            public string TemporaryDirectory
            {
                get { return _temporaryDirectory; }
                set { _temporaryDirectory = value; }
            }

            public string ImplementationClass
            {
                get { return _implementationClass; }
                set { _implementationClass = value; }
            }

            public string ProtocolVersion
            {
                get { return _protocolVersion; }
                set { _protocolVersion = value; }
            }

            public string ImplementationVersionName
            {
                get { return _implementationVersionName; }
                set { _implementationVersionName = value; }
            }

            private bool _ViewerBeforeSend = false;

            public bool ViewerBeforeSend
            {
                get { return _ViewerBeforeSend; }
                set { _ViewerBeforeSend = value; }
            }

            public DicomRetrieveMode DicomImageRetrieveMethod
            {
                get { return _dicomRetrieveMode; }
                set { _dicomRetrieveMode = value; }
            }
        }

        public interface IWorkstationSettings
        {
            List<DicomAE> ServerList
            {
                get;
                set;
            }

            DicomAE ClientAe
            {
                get;
                set;
            }

            string WorkstationServer
            {
                get;
                set;
            }

            string DefaultImageQuery
            {
                get;
                set;
            }

            string DefaultStore
            {
                get;
                set;
            }

            DicomAE GetServer(string serverName);
        }

        public class Templates
        {
            public const string MWLFind = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                 <!--LEAD Technologies, Inc. DICOM XML format-->
                                 <dataset IgnoreBinaryData=""false"" IgnoreAllData=""false"" EncodeBinaryBase64=""true"" EncodeBinaryBinHex=""false"" TagWithCommas=""false"" TrimWhiteSpace=""false"">
                                   <element tag=""00020002"" vr=""UI"" vm=""1"" len=""22"" name=""MediaStorageSOPClassUID"">1.2.840.10008.5.1.4.31</element>
                                   <element tag=""00020010"" vr=""UI"" vm=""1"" len=""20"" name=""TransferSyntaxUID"">1.2.840.10008.1.2.1</element>
                                   <element tag=""00080005"" vr=""CS"" vm=""0"" len=""0"" name=""SpecificCharacterSet"" />
                                   <element tag=""00080050"" vr=""SH"" vm=""0"" len=""0"" name=""AccessionNumber"" />
                                   <element tag=""00080080"" vr=""LO"" vm=""0"" len=""0"" name=""InstitutionName"" />
                                   <element tag=""00080081"" vr=""ST"" vm=""0"" len=""0"" name=""InstitutionAddress"" />
                                   <element tag=""00080090"" vr=""PN"" vm=""0"" len=""0"" name=""ReferringPhysicianName"" />
                                   <!-- <element tag=""00081110"" vr=""SQ"" vm=""0"" len=""-1"" name=""ReferencedStudySequence"" /> -->
                                   <element tag=""00081120"" vr=""SQ"" vm=""0"" len=""-1"" name=""ReferencedPatientSequence"">
                                     <element tag=""FFFEE000"" vr=""OB"" vm=""0"" len=""-1"" name=""Item"">
                                       <element tag=""00081150"" vr=""UI"" vm=""0"" len=""0"" name=""ReferencedSOPClassUID"" />
                                       <element tag=""00081155"" vr=""UI"" vm=""0"" len=""0"" name=""ReferencedSOPInstanceUID"" />
                                     </element>
                                   </element>
                                   <element tag=""00100010"" vr=""PN"" vm=""0"" len=""0"" name=""PatientName"" />
                                   <element tag=""00100020"" vr=""LO"" vm=""0"" len=""0"" name=""PatientID"" />
                                   <element tag=""00100030"" vr=""DA"" vm=""0"" len=""0"" name=""PatientBirthDate"" />
                                   <element tag=""00100040"" vr=""CS"" vm=""0"" len=""0"" name=""PatientSex"" />
                                   <element tag=""00101000"" vr=""LO"" vm=""0"" len=""0"" name=""OtherPatientIDs"" />
                                   <element tag=""00101001"" vr=""PN"" vm=""0"" len=""0"" name=""OtherPatientNames"" />
                                   <element tag=""00101030"" vr=""DS"" vm=""0"" len=""0"" name=""PatientWeight"" />
                                   <element tag=""00101040"" vr=""LO"" vm=""0"" len=""0"" name=""PatientAddress"" />
                                   <element tag=""00102000"" vr=""LO"" vm=""0"" len=""0"" name=""MedicalAlerts"" />
                                   <element tag=""00102110"" vr=""LO"" vm=""0"" len=""0"" name=""Allergies"" />
                                   <element tag=""001021B0"" vr=""LT"" vm=""0"" len=""0"" name=""AdditionalPatientHistory"" />
                                   <element tag=""001021C0"" vr=""US"" vm=""0"" len=""0"" name=""PregnancyStatus"" />
                                   <element tag=""00104000"" vr=""LT"" vm=""0"" len=""0"" name=""PatientComments"" />
                                   <element tag=""0020000D"" vr=""UI"" vm=""0"" len=""0"" name=""StudyInstanceUID"" />
                                   <element tag=""00321032"" vr=""PN"" vm=""0"" len=""0"" name=""RequestingPhysician"" />
                                   <element tag=""00321033"" vr=""LO"" vm=""0"" len=""0"" name=""RequestingService"" />
                                   <element tag=""00321060"" vr=""LO"" vm=""0"" len=""0"" name=""RequestedProcedureDescription"" />
                                   <element tag=""00321064"" vr=""SQ"" vm=""0"" len=""-1"" name=""RequestedProcedureCodeSequence"">
                                     <element tag=""FFFEE000"" vr=""OB"" vm=""0"" len=""-1"" name=""Item"" />
                                   </element>
                                   <element tag=""00380010"" vr=""LO"" vm=""0"" len=""0"" name=""AdmissionID"" />
                                   <element tag=""00380050"" vr=""LO"" vm=""0"" len=""0"" name=""SpecialNeeds"" />
                                   <element tag=""00380300"" vr=""LO"" vm=""0"" len=""0"" name=""CurrentPatientLocation"" />
                                   <element tag=""00380500"" vr=""LO"" vm=""0"" len=""0"" name=""PatientState"" />
                                   <element tag=""00400100"" vr=""SQ"" vm=""0"" len=""-1"" name=""ScheduledProcedureStepSequence"">
                                     <element tag=""FFFEE000"" vr=""OB"" vm=""0"" len=""-1"" name=""Item"">
                                       <element tag=""00080060"" vr=""CS"" vm=""0"" len=""0"" name=""Modality"" />
                                       <element tag=""00321070"" vr=""LO"" vm=""0"" len=""0"" name=""RequestedContrastAgent"" />
                                       <element tag=""00400001"" vr=""AE"" vm=""0"" len=""0"" name=""ScheduledStationAETitle"" />
                                       <element tag=""00400002"" vr=""DA"" vm=""0"" len=""0"" name=""ScheduledProcedureStepStartDate"" />
                                       <element tag=""00400003"" vr=""TM"" vm=""0"" len=""0"" name=""ScheduledProcedureStepStartTime"" />
                                       <element tag=""00400006"" vr=""PN"" vm=""0"" len=""0"" name=""ScheduledPerformingPhysicianName"" />
                                       <element tag=""00400007"" vr=""LO"" vm=""0"" len=""0"" name=""ScheduledProcedureStepDescription"" />
                                       <element tag=""00400008"" vr=""SQ"" vm=""0"" len=""-1"" name=""ScheduledProtocolCodeSequence"">
                                         <element tag=""FFFEE000"" vr=""OB"" vm=""0"" len=""-1"" name=""Item"" />
                                       </element>
                                       <element tag=""00400009"" vr=""SH"" vm=""0"" len=""0"" name=""ScheduledProcedureStepID"" />
                                       <element tag=""00400010"" vr=""SH"" vm=""0"" len=""0"" name=""ScheduledStationName"" />
                                       <element tag=""00400011"" vr=""SH"" vm=""0"" len=""0"" name=""ScheduledProcedureStepLocation"" />
                                       <element tag=""00400012"" vr=""LO"" vm=""0"" len=""0"" name=""PreMedication"" />
                                       <element tag=""00400020"" vr=""CS"" vm=""0"" len=""0"" name=""ScheduledProcedureStepStatus"" />
                                       <element tag=""00400400"" vr=""LT"" vm=""0"" len=""0"" name=""CommentsOnTheScheduledProcedureStep"" />
                                     </element>
                                   </element>
                                   <element tag=""00401001"" vr=""SH"" vm=""0"" len=""0"" name=""RequestedProcedureID"" />
                                   <element tag=""00401002"" vr=""LO"" vm=""0"" len=""0"" name=""ReasonForTheRequestedProcedure"" />
                                   <element tag=""00401003"" vr=""SH"" vm=""0"" len=""0"" name=""RequestedProcedurePriority"" />
                                   <element tag=""00401010"" vr=""PN"" vm=""0"" len=""0"" name=""NamesOfIntendedRecipientsOfResults"" />
                                   <element tag=""00402001"" vr=""LO"" vm=""0"" len=""0"" name=""ReasonForTheImagingServiceRequest"" />
                                   <element tag=""00402016"" vr=""LO"" vm=""0"" len=""0"" name=""PlacerOrderNumberImagingServiceRequest"" />
                                   <element tag=""00402017"" vr=""LO"" vm=""0"" len=""0"" name=""FillerOrderNumberImagingServiceRequest"" />
                                   <element tag=""00402400"" vr=""LT"" vm=""0"" len=""0"" name=""ImagingServiceRequestComments"" />
                                   <element tag=""00403001"" vr=""LO"" vm=""0"" len=""0"" name=""ConfidentialityConstraintOnPatientDataDescription"" />
                                 </dataset>";
        }

        [Serializable]
        public class StorageServerInformation
        {
            public StorageServerInformation()
                : this(null, null, null)
            { }
            public StorageServerInformation(DicomAE server, string serviceName, string machineName)
            {
                DicomServer = server;
                ServiceName = serviceName;
                MachineName = machineName;
            }

            public DicomAE DicomServer { get; set; }
            public string ServiceName { get; set; }
            public string MachineName { get; set; }
        }
    }
    namespace DicomVideoCaptureDemo.Common
    {
        public enum DICOMVID_IMAGE_COMPRESSION
        {
            DICOMVID_IMAGE_COMPRESSION_NONE = 0,
            DICOMVID_IMAGE_COMPRESSION_JPEGLOSSLESS,
            DICOMVID_IMAGE_COMPRESSION_JPEGLOSSY,
            DICOMVID_IMAGE_COMPRESSION_J2KLOSSLESS,
            DICOMVID_IMAGE_COMPRESSION_J2KLOSSY,
            DICOMVID_IMAGE_COMPRESSION_MPEG2,
        };

        public enum DICOM_WRITER_FILTER_TARGET_FORMAT
        {
            DICOM_WRITER_FILTER_TARGET_FORMAT_CUSTOM,//Uncompressed DICOM, JPEG and J2K lossy and lossless
            DICOM_WRITER_FILTER_TARGET_FORMAT_MPEG2//MPEG-2 compressed DICOM
        };

        public struct DICOMSCCLASS
        {
            public long nTag;
            public string pszValue;
            public DICOMSCCLASS(long tag, string zValue)
            {
                nTag = tag;
                pszValue = zValue;
            }
        };

        public struct COMPRESSIONSTRINGPAIR
        {
            public DICOMVID_IMAGE_COMPRESSION ImageCompression;
            public string pszName;

            public COMPRESSIONSTRINGPAIR(DICOMVID_IMAGE_COMPRESSION compression, string name)
            {
                ImageCompression = compression;
                pszName = name;
            }
        };

        public struct MYDICOMUIDIOD
        {
            public DicomClassType nClass;
            public string pszUID;

            public MYDICOMUIDIOD(DicomClassType type, string zUID)
            {
                nClass = type;
                pszUID = zUID;
            }
        };


        public class Helper
        {
            public static string LEAD_IMPLEMENTATION_CLASS_UID = "1.2.840.114257.0.1";
            public static string LEAD_IMPLEMENTATION_VERSION_NAME = "ePACS Demo";

            public static string CANT_FIND_LEAD_DICOM_FILE_WRITER_ERROR = "Could not instantiate the \"ePACS DICOM File Writer\" direct show filter.\nPlease make sure that the  \"ePACS Multimedia Toolkit\" is properly installed on this machine.";
            public static string CANT_INSTANTIATE_CAPTURE_LIBRARY_ERROR = "Could not instantiate the capture library.\nPlease make sure that the \"ePACS Multimedia Toolkit\" is properly installed on this machine.";

            /* The names of the LEAD MPEG-2 Encoders.
               You obtain these with the DirectShow Filter List utility installed with the Multimedia toolkit.
            */
            public static string LEAD_MPEG2_VIDEO_ENCODER = "@device:sw:{33D9A760-90C8-11D0-BD43-00A0C911CE86}\\LEAD MPEG2 Encoder (3.0)";
            public static string LEAD_MPEG_AUDIO_ENCODER = "@device:sw:{33D9A761-90C8-11D0-BD43-00A0C911CE86}\\LEAD MPEG Audio Encoder (2.0)";

            public static int Q_FACTOR_MIN = 2;
            public static int Q_FACTOR_MAX = 255;

            public static MYDICOMUIDIOD[] m_DICOMUIDIOD =
          {
         new MYDICOMUIDIOD( DicomClassType.NMImageStorage                              ,  DicomUidType.NMImageStorage                                 ),
         new MYDICOMUIDIOD( DicomClassType.USMultiFrameImageStorage                    ,  DicomUidType.USMultiframeImageStorage                       ),
         new MYDICOMUIDIOD( DicomClassType.USImageStorage                    ,  DicomUidType.USImageStorage                       ),
         new MYDICOMUIDIOD( DicomClassType.DXImageStoragePresentation                    ,  DicomUidType.DXImageStoragePresentation                   ),
         new MYDICOMUIDIOD( DicomClassType.CTImageStorage                    ,  DicomUidType.CTImageStorage                   ),
         new MYDICOMUIDIOD( DicomClassType.SCImageStorage                              ,  DicomUidType.SCImageStorage                                 ),
         new MYDICOMUIDIOD( DicomClassType.XAImageStorage                              ,  DicomUidType.XAImageStorage                                 ),
         new MYDICOMUIDIOD( DicomClassType.XRFImageStorage                             ,  DicomUidType.XrfImageStorage                                ),
         new MYDICOMUIDIOD( DicomClassType.RTImageStorage                              ,  DicomUidType.RTImageStorage                                 ),
         new MYDICOMUIDIOD( DicomClassType.PETImageStorage                             ,  DicomUidType.PETImageStorage                                ),
         new MYDICOMUIDIOD( DicomClassType.VLEndoscopicImageStorage                    ,  DicomUidType.VLEndoscopicImageStorageClass                  ),
         new MYDICOMUIDIOD( DicomClassType.VLMicroscopicImageStorage                   ,  DicomUidType.VLMicroscopicImageStorageClass                 ),
         new MYDICOMUIDIOD( DicomClassType.VLSlideCoordinatesMicroscopicImageStorage   ,  DicomUidType.VLSlideCoordinatesMicroscopicImageStorageClass ),
         new MYDICOMUIDIOD( DicomClassType.VLPhotographicImageStorage                  ,  DicomUidType.VLPhotographicImageStorageClass                ),
         new MYDICOMUIDIOD( DicomClassType.BasicCardiacEP                              ,  DicomUidType.CardiacElectrophysiologyWaveformStorage        ),
         new MYDICOMUIDIOD( DicomClassType.SCMultiFrameSingleBitImageStorage           ,  DicomUidType.SCMultiFrameSingleBitImageStorage              ),
         new MYDICOMUIDIOD( DicomClassType.SCMultiFrameGrayscaleByteImageStorage       ,  DicomUidType.SCMultiFrameGrayscaleByteImageStorage          ),
         new MYDICOMUIDIOD( DicomClassType.SCMultiFrameGrayscaleWordImageStorage       ,  DicomUidType.SCMultiFrameGrayscaleWordImageStorage          ),
         new MYDICOMUIDIOD( DicomClassType.SCMultiFrameTrueColorImageStorage           ,  DicomUidType.SCMultiFrameTrueColorImageStorage              ),
         new MYDICOMUIDIOD( DicomClassType.VideoEndoscopicImageStorage                 ,  DicomUidType.VideoEndoscopicImageStorage                    ),
         new MYDICOMUIDIOD( DicomClassType.VideoMicroscopicImageStorage                ,  DicomUidType.VideoMicroscopicImageStorage                   ),
         new MYDICOMUIDIOD( DicomClassType.VideoPhotographicImageStorage               ,  DicomUidType.VideoPhotographicImageStorage                  ),

      };

            static DicomClassType[] RemoveList =
          {
         //DicomClassType.NMIMAGESTORAGE,
         //DicomClassType.USMULTIFRAMEIMAGESTORAGE,
         //DicomClassType.SCIMAGESTORAGE,
         //DicomClassType.XAIMAGESTORAGE,
         //DicomClassType.XRFIMAGESTORAGE,
         //DicomClassType.RTIMAGESTORAGE,
         //DicomClassType.PETIMAGESTORAGE,
         //DicomClassType.VLENDOSCOPICIMAGESTORAGE,
         //DicomClassType.VLMICROSCOPICIMAGESTORAGE,
         //DicomClassType.VLSLIDECOORDINATESMICROSCOPICIMAGESTORAGE,
         //DicomClassType.VLPHOTOGRAPHICIMAGESTORAGE,
         //DicomClassType.BASICCARDIACEP
         
         DicomClassType.CRImageStorage,
         DicomClassType.CTImageStorage,
         DicomClassType.MRImageStorage,
         DicomClassType.NMImageStorageRetired,
         DicomClassType.USImageStorage,
         DicomClassType.USImageStorageRetired,
         DicomClassType.USMultiFrameImageStorageRetired,
         DicomClassType.StandaloneOverlayStorage,
         DicomClassType.StandaloneCurveStorage,
         DicomClassType.BasicStudyDescriptor,
         DicomClassType.StandaloneModalityLutStorage,
         DicomClassType.StandaloneVoiLutStorage,
         DicomClassType.XABiplaneImageStorageRetired,
         DicomClassType.RTDoseStorage,
         DicomClassType.RTStructureSetStorage,
         DicomClassType.RTPlanStorage,
         DicomClassType.StandalonePETCurveStorage,
         DicomClassType.StoredPrintStorage,
         DicomClassType.HCGrayscaleImageStorage,
         DicomClassType.HCColorImageStorage,
         //DicomClassType.DXImageStoragePresentation,
        // DicomClassType.DXImageStorageProcessing,
         //DicomClassType.DXMammographyImageStoragePresentation,
         //DicomClassType.DXMammographyImageStorageProcessing,
        // DicomClassType.DXIntraoralImageStoragePresentation,
        // DicomClassType.DXIntraoralImageStorageProcessing,
         DicomClassType.RTBeamsTreatmentRecordStorage,
         DicomClassType.RTBrachyTreatmentRecordStorage,
         DicomClassType.RTTreatmentSummaryRecordStorage,
         DicomClassType.Patient,
         DicomClassType.Visit,
         DicomClassType.Study,
         DicomClassType.StudyComponent,
         DicomClassType.Results,
         DicomClassType.Interpretation,
         DicomClassType.BasicFilmSession,
         DicomClassType.BasicFilmBox,
         DicomClassType.BasicGrayscaleImageBox,
         DicomClassType.BasicColorImageBox,
         DicomClassType.BasicAnnotationBox,
         DicomClassType.PrintJob,
         DicomClassType.Printer,
         DicomClassType.VoiLutBoxRetired,
         DicomClassType.ImageOverlayBoxRetired,
         DicomClassType.StorageCommitmentPushModel,
         DicomClassType.StorageCommitmentPullModel,
         DicomClassType.PrintQueue,
         DicomClassType.ModalityPerformedProcedureStep,
         DicomClassType.PresentationLut,
         DicomClassType.PullPrintRequest,
         DicomClassType.PatientMeta,
         DicomClassType.StudyMeta,
         DicomClassType.ResultsMeta,
         DicomClassType.BasicGrayscalePrintMeta,
         DicomClassType.BasicColorPrintMeta,
         DicomClassType.ReferencedGrayscalePrintMetaRetired,
         DicomClassType.ReferencedColorPrintMetaRetired,
         DicomClassType.PullStoredPrintMeta,
         DicomClassType.PrinterConfiguration,
         DicomClassType.BasicPrintImageOverlayBox,
         DicomClassType.BasicDirectory,
         DicomClassType.PatientRootQueryPatient,
         DicomClassType.PatientRootQueryStudy,
         DicomClassType.PatientRootQuerySeries,
         DicomClassType.PatientRootQueryImage,
         DicomClassType.StudyRootQueryStudy,
         DicomClassType.StudyRootQuerySeries,
         DicomClassType.StudyRootQueryImage,
         DicomClassType.PatientStudyQueryPatient,
         DicomClassType.PatientStudyQueryStudy,
         DicomClassType.BasicTextSR,
         DicomClassType.EnhancedSR,
         DicomClassType.ComprehensiveSR,
         DicomClassType.ModalityWorklist,
         DicomClassType.GrayscaleSoftcopyPresentationState,
         DicomClassType.BasicVoiceAudio,
         DicomClassType.TwelveLeadECG,
         DicomClassType.GeneralECG,
         DicomClassType.AmbulatoryECG,
         DicomClassType.Hemodynamic,
         DicomClassType.EnhancedMRImageStorage,
         DicomClassType.MRSpectroscopyStorage,
         DicomClassType.RawDataStorage,
         //DicomClassType.SCMULTIFRAMESINGLEBITIMAGESTORAGE,
         //DicomClassType.SCMULTIFRAMEGRAYSCALEBYTEIMAGESTORAGE,
         //DicomClassType.SCMULTIFRAMEGRAYSCALEWORDIMAGESTORAGE,
         //DicomClassType.SCMULTIFRAMETRUECOLORIMAGESTORAGE,
         DicomClassType.GeneralPurposeScheduledProcedureStep,
         DicomClassType.GeneralPurposePerformedProcedureStep,
         DicomClassType.GeneralPurposeWorklistManagementMeta,
         DicomClassType.KeyObjectSelectionDocument,
         DicomClassType.MammographyCADSR,
         DicomClassType.ChestCADSR,
         DicomClassType.GeneralPurposeWorklist,
         DicomClassType.Ophthalmic8BitPhotographyImageStorage,
         DicomClassType.Ophthalmic16BitPhotographyImageStorage,
         DicomClassType.StereometricRelationshipStorage,
      };
            public static DICOMSCCLASS[] DefaultElementValues =
          {
            new DICOMSCCLASS( DicomTag.FileMetaInformationVersion,                     "01\\00"),
            new DICOMSCCLASS( DicomTag.MediaStorageSOPClassUID,                        DicomUidType.CTImageStorage ),
            new DICOMSCCLASS( DicomTag.ImplementationClassUID,                         "1.3.6.1.4.1.19291.2.1"),
            new DICOMSCCLASS( DicomTag.ImplementationVersionName,                      "ePACS Writer Filter Version 1.0"),
            new DICOMSCCLASS( DicomTag.SourceApplicationEntityTitle,                   "ePACS Technologies, Inc."),
            
            // Patient
            new DICOMSCCLASS( DicomTag.PatientName,                                      "Anonymous"),
            new DICOMSCCLASS( DicomTag.PatientID,                                        "123-45-6789"),
            new DICOMSCCLASS( DicomTag.PatientBirthDate,                                "11/10/1965"),
            new DICOMSCCLASS( DicomTag.PatientSex,                                       "M"),
            
            // General Study   
            new DICOMSCCLASS( DicomTag.StudyDate,                                        "09/08/2000"),
            new DICOMSCCLASS( DicomTag.StudyTime,                                        "12:00:01.0"),
            new DICOMSCCLASS( DicomTag.ReferringPhysicianName,                          "Physician"),
            new DICOMSCCLASS( DicomTag.StudyID,                                          "1216"),
            new DICOMSCCLASS( DicomTag.StudyDescription,                                          "CR modality"),
            new DICOMSCCLASS( DicomTag.AccessionNumber,                                  "1"),
            
            // General Series
            new DICOMSCCLASS( DicomTag.Modality,                                          "CT"),
            new DICOMSCCLASS( DicomTag.SeriesNumber,                                     "1" ),
            
            // General Image
            new DICOMSCCLASS( DicomTag.InstanceNumber,                                   "1"),
            //   { DicomTag.IMAGESINACQUISITION,                             "30"},
            
            // SC Equipment
            new DICOMSCCLASS( DicomTag.ConversionType,                                   "DV"),
            new DICOMSCCLASS( DicomTag.SecondaryCaptureDeviceManufacturers,             "ePACS Technologies, Inc."),
            new DICOMSCCLASS( DicomTag.SecondaryCaptureDeviceManufacturerModelName,  "" ),
            new DICOMSCCLASS( DicomTag.SecondaryCaptureDeviceSoftwareVersion,         "" ),
            new DICOMSCCLASS( DicomTag.VideoImageFormatAcquired,                       "NTSC"),
            
            // SC Image
            new DICOMSCCLASS( DicomTag.DateOfSecondaryCapture,                         "09/08/2000" ),
            new DICOMSCCLASS( DicomTag.TimeOfSecondaryCapture,                         "12:00:01.0" ),
            
            // SOP Common
            new DICOMSCCLASS( DicomTag.SOPClassUID,                                     DicomUidType.CTImageStorage),
            new DICOMSCCLASS( DicomTag.SpecificCharacterSet,                            "ISO_IR 100")
         };
            public static DICOMSCCLASS[] DXDefaultElementValues =
          {
            new DICOMSCCLASS( DicomTag.FileMetaInformationVersion,                     "01\\00"),
            new DICOMSCCLASS( DicomTag.MediaStorageSOPClassUID,                        DicomUidType.DXImageStoragePresentation ),
            new DICOMSCCLASS( DicomTag.ImplementationClassUID,                         "1.2.840.10008.5.1.4.1.1.1.1"),
            new DICOMSCCLASS( DicomTag.ImplementationVersionName,                      "ePACS Writer Filter Version 1.0"),
            new DICOMSCCLASS( DicomTag.SourceApplicationEntityTitle,                   "ePACS Technologies, Inc."),
            
            // Patient
            new DICOMSCCLASS( DicomTag.PatientName,                                      "Anonymous"),
            new DICOMSCCLASS( DicomTag.PatientID,                                        "123-45-6789"),
            new DICOMSCCLASS( DicomTag.PatientBirthDate,                                "11/10/1965"),
            new DICOMSCCLASS( DicomTag.PatientSex,                                       "M"),
            
            // General Study   
            new DICOMSCCLASS( DicomTag.StudyDate,                                        "09/08/2000"),
            new DICOMSCCLASS( DicomTag.StudyTime,                                        "12:00:01.0"),
            new DICOMSCCLASS( DicomTag.ReferringPhysicianName,                          "Physician"),
            new DICOMSCCLASS( DicomTag.StudyID,                                          "1216"),
            new DICOMSCCLASS( DicomTag.StudyDescription,                                          "CR modality"),
            new DICOMSCCLASS( DicomTag.AccessionNumber,                                  "1"),
            
            // General Series
            new DICOMSCCLASS( DicomTag.Modality,                                          "DX"),
            new DICOMSCCLASS( DicomTag.SeriesNumber,                                     "1" ),
            
            // General Image
            new DICOMSCCLASS( DicomTag.InstanceNumber,                                   "1"),
            //   { DicomTag.IMAGESINACQUISITION,                             "30"},
            
            // SC Equipment
            new DICOMSCCLASS( DicomTag.ConversionType,                                   "DV"),
            new DICOMSCCLASS( DicomTag.SecondaryCaptureDeviceManufacturers,             "ePACS Technologies, Inc."),
            new DICOMSCCLASS( DicomTag.SecondaryCaptureDeviceManufacturerModelName,  "" ),
            new DICOMSCCLASS( DicomTag.SecondaryCaptureDeviceSoftwareVersion,         "" ),
            new DICOMSCCLASS( DicomTag.VideoImageFormatAcquired,                       "NTSC"),
            
            // SC Image
            new DICOMSCCLASS( DicomTag.DateOfSecondaryCapture,                         "09/08/2000" ),
            new DICOMSCCLASS( DicomTag.TimeOfSecondaryCapture,                         "12:00:01.0" ),
            
            // SOP Common
            new DICOMSCCLASS( DicomTag.SOPClassUID,                                     DicomUidType.DXImageStoragePresentation),
            new DICOMSCCLASS( DicomTag.SpecificCharacterSet,                            "ISO_IR 100")
         };

            public static COMPRESSIONSTRINGPAIR[] CompressionStringPair =
          {
         new COMPRESSIONSTRINGPAIR(  DICOMVID_IMAGE_COMPRESSION.DICOMVID_IMAGE_COMPRESSION_NONE,            "Uncompressed"),
         new COMPRESSIONSTRINGPAIR(  DICOMVID_IMAGE_COMPRESSION.DICOMVID_IMAGE_COMPRESSION_JPEGLOSSLESS,    "Lossless JPEG"),
         new COMPRESSIONSTRINGPAIR(  DICOMVID_IMAGE_COMPRESSION.DICOMVID_IMAGE_COMPRESSION_JPEGLOSSY,       "Lossy JPEG"),
         new COMPRESSIONSTRINGPAIR(  DICOMVID_IMAGE_COMPRESSION.DICOMVID_IMAGE_COMPRESSION_J2KLOSSLESS,     "Lossless JPEG 2000"),
         new COMPRESSIONSTRINGPAIR(  DICOMVID_IMAGE_COMPRESSION.DICOMVID_IMAGE_COMPRESSION_J2KLOSSY,        "JPEG 2000"),
         new COMPRESSIONSTRINGPAIR(  DICOMVID_IMAGE_COMPRESSION.DICOMVID_IMAGE_COMPRESSION_MPEG2,           "MPEG2")
      };
        }
    }
}
