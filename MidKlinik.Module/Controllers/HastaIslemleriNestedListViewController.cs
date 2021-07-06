using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using MidKlinik.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidKlinik.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class HastaIslemleriNestedListViewController : ViewController
    {
        public HastaIslemleriNestedListViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(Muayene);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ListPropertyEditor listPropertyEditor = ((DetailView)View).FindItem("HastaIslemleri") as ListPropertyEditor;
            if (listPropertyEditor != null)
            {
                listPropertyEditor.ControlCreated += new EventHandler<EventArgs>(listPropertyEditor_ControlCreated);
            }
        }

        private void listPropertyEditor_ControlCreated(object sender, EventArgs e)
        {
            ListPropertyEditor listPropertyEditor = (ListPropertyEditor)sender;
            Frame listViewFrame = listPropertyEditor.Frame;
            ListView nestedListView = listPropertyEditor.ListView;
            AccessParentDetailViewController accessParentDetailViewController = listViewFrame.GetController<AccessParentDetailViewController>();
            nestedListView.CurrentObjectChanged += new EventHandler(nestedListView_Objectchanged);
        }

        private void nestedListView_Objectchanged(object sender, EventArgs e)
        {
            UpdateBool((HastaIslemleri)((ListView)sender).CurrentObject);
        }

        private void UpdateBool(HastaIslemleri currentObject)
        {
            CurrentObject.kayit = true;
        }
        public Muayene CurrentObject
        {
            get { return (Muayene)View.CurrentObject; }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
