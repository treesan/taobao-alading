using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using Alading.Taobao;
using System.Collections;
using System.IO;
using Alading.Utils;
using Alading.Entity;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Alading.Forms.Print
{
    public partial class TemplateDesign : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 是否是编辑模版,true为编辑
        /// </summary>
        public  bool isModigy = false;
        private bool isFirst = true;
        private SaveCommandHandler saveHandler = null;
        private LogisticCompanyTemplate template = null;
        private string companyName = string.Empty;

        public TemplateDesign()
        {
            InitializeComponent();
        }
        public TemplateDesign(LogisticCompanyTemplate template)
        {
            InitializeComponent();
            this.template = template;
            isModigy = true;
        }
        public TemplateDesign(string companyName)
        {
            InitializeComponent();
            this.companyName = companyName;
            isModigy = false;
        }

        /// <summary>
        /// 加载界面时触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemplateDesign_Load(object sender, EventArgs e)
        {
            if (template == null)
            {
                template = new LogisticCompanyTemplate();                
            }
            else
            { //模版编辑

                LogisticCompanyEditor.Properties.Value = template.LogisticCompanyName;
                editorRowName.Properties.Value = template.TemplateName;
                editorRowPrinter.Properties.Value = template.DefaultPrinter;
                if (template.Landscape == true)
                {
                    editorRowLanscape.Properties.Value = "横向";
                }
                else
                {
                    editorRowLanscape.Properties.Value = "纵向";
                }
                
                editorRowWidth.Properties.Value = template.PaperWidth;
                editorRowHeight.Properties.Value = template.PaperHeight;
                editorRowTop.Properties.Value = template.TemplateTopOffset;
                editorRowBottom.Properties.Value = template.TemplateBottomOffset;
                editorRowLeft.Properties.Value = template.TemplateLeftOffset;
                editorRowRight.Properties.Value = template.TemplateRightOffset;  
                if (template.TemplateData != null)
                {
                    //1、首先解压缩
                    byte[] decommpressBytes = CompressHelper.Decompress(template.TemplateData);
                    MemoryStream memStream = new MemoryStream(decommpressBytes);
                    xrDesignPanel.OpenReport(memStream);                    
                    memStream.Close();
                }

                //新建模版不可用
                commandBarItemNew.Enabled = false;
            }

            saveHandler = new SaveCommandHandler(xrDesignPanel, template, isModigy);
            try
            {
                ShowLogisticsCompany();
                ShowDeliveryState();
                if (companyName != string.Empty && companyName != "所有物流公司")
                {
                    LogisticCompanyEditor.Properties.Value = companyName;
                }
                //true为模板编辑
                if (isModigy == true)
                {
                    List<string> areaList = template.CoverAreaList.Split(new Char[] { ','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                    for (int index = 0; index < checkedListBoxStateCtrl.Items.Count; index++)
                    {
                        if (areaList.Contains(checkedListBoxStateCtrl.Items[index].Value.ToString()))
                        {
                            checkedListBoxStateCtrl.Items[index].CheckState = System.Windows.Forms.CheckState.Checked;
                        }
                    }
                }
                else
                {
                    //默认全选
                    barEditAllSelect.EditValue = true;
                    for (int index = 0; index < checkedListBoxStateCtrl.Items.Count; index++)
                    {
                        checkedListBoxStateCtrl.Items[index].CheckState = System.Windows.Forms.CheckState.Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,Constants.SYSTEM_PROMPT,MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 展示物流公司
        /// </summary>
        public void ShowLogisticsCompany()
        {
            List<Alading.Entity.LogisticCompany> logisticList = LogisticCompanyService.GetAllLogisticCompany();
            Hashtable logisticCodeHash = new Hashtable();//用于存放物流公司的Code
            int index = 0;


            foreach (Alading.Entity.LogisticCompany logistic in logisticList)
            {
                repositoryItemComboBoxLogistic.Items.Add(logistic.name);
                logisticCodeHash.Add(index, logistic.code);

                index++;//改变索引
            }

            repositoryItemComboBoxLogistic.Tag = logisticCodeHash;//赋值物流公司的Code
        }

        /// <summary>
        /// 展示配送地区，只显示省份
        /// </summary>
        public void ShowDeliveryState()
        { 
            List<Alading.Entity.Area> areaList =AreaService.GetArea(c=>c.parent_id=="1");

            foreach(Alading.Entity.Area area in areaList)
            {
                checkedListBoxStateCtrl.Items.Add(area.name,false);
                checkedListBoxStateCtrl.Tag = area.id;
            }
        }

        /// <summary>
        /// 点击配送地区的全选按钮，tag用于代表选择状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemCheckAllSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (((DevExpress.XtraEditors.CheckEdit)(sender)).Checked == true)//变为选中
            {
                for (int index = 0; index < checkedListBoxStateCtrl.Items.Count; index++)
                {
                    checkedListBoxStateCtrl.Items[index].CheckState = System.Windows.Forms.CheckState.Checked;
                }
            }
            else//变为全不选中
            {
                for (int index = 0; index < checkedListBoxStateCtrl.Items.Count; index++)
                {
                    checkedListBoxStateCtrl.Items[index].CheckState = System.Windows.Forms.CheckState.Unchecked; ;
                }
            }
        }

        #region 模板布局发生改变
        private void xrDesignBarManager_LayoutUpgrade(object sender, DevExpress.Utils.LayoutUpgadeEventArgs e)
        {
            commandBarItemSave.Enabled = true;
        }
        #endregion

        private void commandBarItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //将焦点行取消，要不然数据获取不了
            vGridControl.FocusedRow.Index = -1;
            //1、验证必填信息是否填写完整，命令按钮将先触发该事件
            bool validateOK = true;

            foreach (DevExpress.XtraVerticalGrid.Rows.CategoryRow cRow in vGridControl.Rows)
            {
                foreach (DevExpress.XtraVerticalGrid.Rows.EditorRow row in cRow.ChildRows)
                {
                    if (row.Properties.Value == null || row.Properties.Value.ToString() == string.Empty)
                    {
                        XtraMessageBox.Show(string.Format("请填写{0}", row.Properties.Caption), Constants.SYSTEM_PROMPT);
                        validateOK = false;
                        break;
                    }
                }
                if (validateOK == false)
                {
                    break;
                }
            }

            //保证CommandHandle只加载一次
            if (isFirst)
            {
                //自定义保存按钮事件，取消默认的弹出对话框
                xrDesignPanel.AddCommandHandler(saveHandler);
                isFirst = false;
            }

            //如果验证不通过则，去掉CommandHandler
            if (!validateOK)
            {
                xrDesignPanel.RemoveCommandHandler(saveHandler);
                isFirst = true;
            }
            else
            {
                if (isModigy == false)
                {
                    template.TemplateCode = System.Guid.NewGuid().ToString();
                }   
                template.TemplateName = editorRowName.Properties.Value.ToString();  
                
                MemoryStream memStream = new MemoryStream();
                //存储到内存流中
                xrDesignPanel.Report.SaveLayout(memStream);
                byte[] reportBytes = new byte[memStream.Length];
                //重新设置起点，否则读取会不成功
                memStream.Seek(0, SeekOrigin.Begin);
                memStream.Read(reportBytes, 0, reportBytes.Length);
                //关闭流
                memStream.Close();
                //压缩数据
                byte[] compressBytes = CompressHelper.Compress(reportBytes);

                template.TemplateData = compressBytes;

                string areaList = string.Empty;
                for (int index = 0; index < checkedListBoxStateCtrl.Items.Count; index++)
                {
                    if (checkedListBoxStateCtrl.Items[index].CheckState == System.Windows.Forms.CheckState.Checked)
                    {
                        areaList += checkedListBoxStateCtrl.Items[index]+",";
                    }
                }
                template.CoverAreaList = areaList.Trim(',');

                Hashtable companyTable = repositoryItemComboBoxLogistic.Tag as Hashtable;
                int indexCode = repositoryItemComboBoxLogistic.Items.IndexOf(LogisticCompanyEditor.Properties.Value.ToString());
                string companyCode = companyTable[indexCode].ToString();


                template.LogisticCompanyCode = companyCode;

                template.TemplateLeftOffset =int.Parse(editorRowLeft.Properties.Value.ToString());
                template.TemplateRightOffset = int.Parse(editorRowRight.Properties.Value.ToString());
                template.TemplateTopOffset = int.Parse(editorRowTop.Properties.Value.ToString());
                template.TemplateBottomOffset = int.Parse(editorRowBottom.Properties.Value.ToString());                
                template.DefaultPrinter = editorRowPrinter.Properties.Value.ToString();                
                template.LogisticCompanyName = LogisticCompanyEditor.Properties.Value.ToString();
                template.PaperHeight = int.Parse(editorRowHeight.Properties.Value.ToString());
                template.PaperWidth = int.Parse(editorRowWidth.Properties.Value.ToString());
                //如果页面应横向打印，则为 true；反之，则为 false。默认值由打印机决定。
                if (editorRowLanscape.Properties.Value.ToString() == "横向")
                {
                    template.Landscape = true;
                }
                else
                {
                    template.Landscape = false;
                }
            }
        }

        private void TemplateDesign_FormClosing(object sender, FormClosingEventArgs e)
        {
            //一定要移除CommandHandler，否则会重复执行时间，导致加入多个模板
            xrDesignPanel.RemoveCommandHandler(saveHandler);
        }
    }

    public class SaveCommandHandler : ICommandHandler
    {
        XRDesignPanel panel;
        LogisticCompanyTemplate template;
        bool isModigy = false;

        public SaveCommandHandler(XRDesignPanel panel, LogisticCompanyTemplate template, bool isModigy)
        {
            this.panel = panel;
            this.template = template;
            this.isModigy = isModigy;
        }

        public virtual void HandleCommand(ReportCommand command, object[] args, ref bool handled)
        {
            if (!CanHandleCommand(command)) return;

            // Save a report.
            Save();

            // Set handled to true to avoid the standard saving procedure to be called.
            handled = true;
        }

        public virtual bool CanHandleCommand(ReportCommand command)
        {
            // This handler is used for SaveFile, SaveFileAs and Closing commands.
            return command == ReportCommand.SaveFile ||
                command == ReportCommand.SaveFileAs ||
                command == ReportCommand.Closing;
        }

        //自定义的保存代码
        void Save()
        {
            //1、验证必填信息是否填写完整

            //2、将设计的模板保存到内存流中，并存入数据库            
            //MemoryStream memStream = new MemoryStream();
            ////存储到内存流中
            //panel.Report.SaveLayout(memStream);            
            //byte[] reportBytes = new byte[memStream.Length];
            ////重新设置起点，否则读取会不成功
            //memStream.Seek(0, SeekOrigin.Begin);
            //memStream.Read(reportBytes, 0, reportBytes.Length);
            ////关闭流
            //memStream.Close();
            ////压缩数据
            //byte[] compressBytes = CompressHelper.Compress(reportBytes);

            //3、将模板存储到数据库中
            if (isModigy == false)
            {
                LogisticCompanyService.AddLogisticCompanyTemplate(template);
            }
            else
            {
                LogisticCompanyService.UpdateLogisticCompanyTemplate(template);
            }
            
            // Prevent the "Report has been changed" dialog from being shown.
            panel.ReportState = ReportState.Saved;

        }
    }

}