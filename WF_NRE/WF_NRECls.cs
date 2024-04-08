using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using System.Diagnostics;//for debugger :)

using LSEXT;
using LSSERVICEPROVIDERLib;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;


using ADODB;
using Patholab_Common;


namespace WF_NRE
{

    [ComVisible(true)]
    [ProgId("WF_NRE.WF_NRECls")]
    public class WF_NRECls : IWorkflowExtension
    {
        INautilusServiceProvider sp;


        public void Execute(ref LSExtensionParameters Parameters)
        {
            try
            {


                string tableName = Parameters["TABLE_NAME"];

                sp = Parameters["SERVICE_PROVIDER"];
                var rs = Parameters["RECORDS"];



         //       Debugger.Launch();
                //Recordset rs = Parameters["RECORDS"];
                string firstSDG = rs["SDG_ID"].Value.ToString();
                string NAME = rs["NAME"].Value.ToString();
                rs.MoveLast();
                string tableID = rs.Fields["SDG_ID"].Value.ToString();
                string workstationId = Parameters["WORKSTATION_ID"].ToString();

                ContainerFrm frm = new ContainerFrm(NAME);
                NewResultEntry.NewResultEntryCls nre = new NewResultEntry.NewResultEntryCls();
                nre.runByWf(sp, NAME);
                nre.Dock = DockStyle.Fill;
                frm.Controls.Add(nre);
        
                frm.WindowState = FormWindowState.Maximized;//1   MUST
                frm.ShowDialog ( );
         //       frm.WindowState = FormWindowState.Maximized;//2
                frm.BringToFront ( );
                

                //אם עושים ShowDialog זה מקטין את המסך
            }
            catch (Exception ex)
            {
                MessageBox.Show("נכשלה שליחת התוצאה");
                Logger.WriteLogFile(ex);
            }
        }
    }
}
