using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Alading.Forms.Trade.Controls
{
    public partial class OperateMessages : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable _table = new DataTable();

        public OperateMessages()
        {
            InitializeComponent();
            _table.Columns.Add("operateTime");
            _table.Columns.Add("operateContent");
            _table.Columns.Add("operateMemo");
            _table.Rows.Clear();
        }

        public void AppendNewMessage(string content, string memo)
        {
           DataRow newRow= _table.NewRow();
            newRow["operateTime"] = DateTime.Now;
            newRow["operateContent"] = content;
            newRow["operateMemo"] = memo;
            _table.Rows.Add(newRow);
            gcOperateMesages.DataSource = _table;
            gcOperateMesages.ForceInitialize();
            gvMessages.BestFitColumns();
        }
    }
}
