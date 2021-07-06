
namespace MidKlinik.Module.Controllers
{
    partial class Fatura_irsaliye_olustur
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.FaturaIrsaliyeAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // FaturaIrsaliyeAction
            // 
            this.FaturaIrsaliyeAction.AcceptButtonCaption = null;
            this.FaturaIrsaliyeAction.CancelButtonCaption = null;
            this.FaturaIrsaliyeAction.Caption = "Fatura/İrsaliye Oluştur";
            this.FaturaIrsaliyeAction.ConfirmationMessage = null;
            this.FaturaIrsaliyeAction.Id = "FaturaIrsaliyeAction";
            this.FaturaIrsaliyeAction.ToolTip = null;
            this.FaturaIrsaliyeAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.FaturaIrsaliyeAction_CustomizePopupWindowParams);
            // 
            // Fatura_irsaliye_olustur
            // 
            this.Actions.Add(this.FaturaIrsaliyeAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction FaturaIrsaliyeAction;
    }
}
