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
    public partial class GelirGiderDateControl : ViewController
    {
        public ParametrizedAction dateFilterAction;
        public GelirGiderDateControl()
        {
            InitializeComponent();
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(GelirGider);
            TargetViewId = "GelirGider_ListView";
            dateFilterAction = new ParametrizedAction(this, "Filtrele", PredefinedCategory.Search, typeof(DateTime));
            dateFilterAction.NullValuePrompt = "Tarih Seçiniz";
            dateFilterAction.Execute += parametrizedAction1_Execute;
        }

        private void parametrizedAction1_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            CriteriaOperator criterion = null;
            if (e.ParameterCurrentValue != null && e.ParameterCurrentValue.ToString() != string.Empty)
            {
                criterion = new BinaryOperator("OdemeTarihi", Convert.ToDateTime(e.ParameterCurrentValue));
            }
            ((ListView)View).CollectionSource.Criteria[dateFilterAction.Id] = criterion;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            CriteriaOperator criterion2 = new BinaryOperator("OdemeTarihi", Convert.ToDateTime(DateTime.Today));
            ((ListView)View).CollectionSource.Criteria[dateFilterAction.Id] = criterion2;
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
