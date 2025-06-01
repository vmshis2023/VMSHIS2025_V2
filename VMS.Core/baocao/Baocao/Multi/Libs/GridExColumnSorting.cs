using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Janus.Windows.GridEX;

namespace VNS.HandyTools
{
    public class GridExColumnSorting
    {
        private static DataTable dtList = new DataTable();
        private static string xmlPath;

        static GridExColumnSorting()
        {
            try
            {
                xmlPath = Utility.ConfigPath+"GridExColumnSorting.xml";
                dtList = SqlUtility.GetDataSet("exec Core_GetGridExSortingConfig").Tables[0];
                //dtList = SqlUtility.GetDataSet("select MA from D_DMUC_CHUNG where LOAI = 'GRIDEXSORTINGCONFIG'").Tables[0];
                dtList.Columns.Add("AssemblyFullName", typeof(string));
                dtList.Columns.Add("GridExName", typeof(string));
                foreach (DataRow dr in dtList.Rows)
                {
                    string[] s = Utility.sDbnull(dr["GRIDEXSORTINGCONFIG"]).Split('|');
                    try
                    {
                        dr["AssemblyFullName"] = s[0];
                        dr["GridExName"] = s[1];
                    }
                    catch (Exception) {}
                    
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static void Save(string assemblyFullName, GridEX gridEx)
        {
            try
            {
                //List<string> lstSetting = new List<string>();
                XmlDocument doc = new XmlDocument();
                try{doc.Load(xmlPath);}catch (Exception){}
                if (doc.ChildNodes.Count <= 0)
                {
                    XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    doc.AppendChild(docNode);
                }
                XmlNode assmbelyNode;
                XmlNodeList lstAssmbelyNode = doc.GetElementsByTagName(assemblyFullName);
                if (lstAssmbelyNode.Count > 0) assmbelyNode = lstAssmbelyNode[0];
                else
                {
                    assmbelyNode = doc.CreateElement(assemblyFullName);
                    doc.AppendChild(assmbelyNode);
                }
                XmlNode gridNode;
                XmlNodeList lstGridNote = doc.GetElementsByTagName(gridEx.Name);
                if (lstGridNote.Count > 0) gridNode = lstGridNote[0];
                else
                
                {
                    gridNode = doc.CreateElement(gridEx.Name);
                    assmbelyNode.AppendChild(gridNode);
                }
                gridNode.RemoveAll();

                foreach (GridEXColumn col in gridEx.RootTable.Columns)
                    if (col.Key != "InvincibleColSortingConfig")
                    {
                        XmlNode columnOrder = doc.CreateElement("ColumnOrder");
                        columnOrder.InnerText = col.Position.ToString();
                        gridNode.AppendChild(columnOrder);

                        //lstSetting.Add(string.Format("{0} {1}",col.Key, col.Index));
                        XmlAttribute columnName = doc.CreateAttribute("ColumnName");
                        columnName.Value = col.Key;
                        columnOrder.Attributes.Append(columnName);
                        //productAttribute = doc.CreateAttribute("ColumnIndex");
                        //productAttribute.Value = col.Index.ToString();
                        //gridNode.Attributes.Append(productAttribute);
                    }
                using (XmlTextWriter writer = new XmlTextWriter(xmlPath, null))
                {
                    writer.Formatting = Formatting.Indented;
                    doc.Save(writer);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            
        }

        public static void SetSortingMoveEvent(string assemblyFullName, Form form)
        {
            try
            {
                string[] arrGridExName = (from dr in dtList.AsEnumerable()
                                          where Convert.ToString(dr.Field<object>("AssemblyFullName")) == assemblyFullName
                                          select Convert.ToString(dr.Field<object>("GridExName"))).ToArray();
                foreach (string sGridExName in arrGridExName)
                {
                    Control[] arrControl = form.Controls.Find(sGridExName, true);
                    if (arrControl.Any()) AddColumnMoveEvent(arrControl[0], assemblyFullName);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public static void PerformGridExColumnSorting(string assemblyFullName, Form form)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                XmlNode assmbelyNode = doc.GetElementsByTagName(assemblyFullName)[0];
                foreach (DataRow dr in dtList.Select(string.Format("AssemblyFullName = '{0}'",assemblyFullName)))
                {
                    foreach (XmlNode node in assmbelyNode.ChildNodes)
                    {
                        if (node.Name == Utility.sDbnull(dr["GridExName"]))
                        {
                            Control[] arrControl = form.Controls.Find(node.Name, true);
                            try
                            {
                                GridEX gridEx = (GridEX) arrControl[0];
                                foreach (XmlNode node1 in node.ChildNodes)
                                {
                                    gridEx.RootTable.Columns[node1.Attributes[0].Value].Position = Utility.Int16Dbnull(node1.InnerText);
                                }
                            }
                            catch (Exception)
                            {
                                
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private static void AddColumnMoveEvent(Control ctrl, string assemblyFullName)
        {
            try
            {
                GridEX gridEx = ((GridEX)ctrl);
                GridEXColumn col = new GridEXColumn();
                col.Key = "InvincibleColSortingConfig";
                col.Visible = false;
                col.Caption = assemblyFullName;
                gridEx.RootTable.Columns.Add(col);
                gridEx.ColumnMoved += EvenGridExColumnMoved;
            }
            catch (Exception ex)
            {
                
            }
            
        }

        private static void EvenGridExColumnMoved(object sender, ColumnActionEventArgs e)
        {
            try
            {
                GridEX gridEx = (GridEX)sender;
                Save(gridEx.RootTable.Columns["InvincibleColSortingConfig"].Caption,gridEx);
            }
            catch (Exception ex)
            {
                
            }
            
        }
    }
}
