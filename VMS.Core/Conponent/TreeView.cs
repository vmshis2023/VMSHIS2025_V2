using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CIS.CoreApp.Conponent
{
    public partial class TreeView : Component
    {
        public string AssemBlyName = "";
        public string sDllName = "";
        public string ParameterList = "";
        public TreeView()
        {
            InitializeComponent();
        }

        public TreeView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
