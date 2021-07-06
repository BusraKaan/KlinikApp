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
    public partial class AccessNestedListViewController : ViewController
    {
        public AccessNestedListViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(BelgeBaslik);
            counter = 0;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ListPropertyEditor listPropertyEditor = ((DetailView)View).FindItem("BelgeSatir") as ListPropertyEditor;
            if (listPropertyEditor != null)
            {
                listPropertyEditor.ControlCreated += new EventHandler<EventArgs>(listPropertyEditor_ControlCreated);
            }

            //if (CurrentObject.BelgeTarih == Convert.ToDateTime("1.01.0001 00:00:00"))
            //{
            //    CurrentObject.BelgeTarih = DateTime.Now;
            //}
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
            //UpdateToplam((BelgeSatir)((ListView)sender).CurrentObject);
        }
        public static int counter;
        //private void UpdateToplam(BelgeSatir obj)
        //{
        //    if (CurrentObject.BelgeTur == BelgeTurEnum.Fatura)
        //    {
        //        if (counter != 0)
        //        {
        //            double Toplam = 0;
        //            double kdvToplam = 0;
        //            double KDVdahilToplam = 0;
        //            foreach (var item in CurrentObject.BelgeSatir)
        //            {
        //                //Toplam += item.KDVharicTutar;
        //                kdvToplam += item.KDVtutari;
        //                KDVdahilToplam += item.KDVtutari + item.KDVharicTutar;
        //            }
        //            CurrentObject.KDVtoplam = kdvToplam;
        //            CurrentObject.KDVDahiltoplam = KDVdahilToplam;
        //            //CurrentObject.ToplamFiyat = Toplam;

        //        }
        //        else
        //        {
        //            CurrentObject.KDVtoplam = CurrentObject.KDVtoplam;
        //            CurrentObject.KDVDahiltoplam = CurrentObject.KDVDahiltoplam;
        //            CurrentObject.ToplamFiyat = CurrentObject.ToplamFiyat;

        //            counter++;
        //        }
        //    }
        //}

        public BelgeBaslik CurrentObject
        {
            get { return (BelgeBaslik)View.CurrentObject; }
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
