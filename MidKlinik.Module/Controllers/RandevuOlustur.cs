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
    public partial class RandevuOlustur : ViewController
    {
        public RandevuOlustur()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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

        public IObjectSpace objectSpace;
        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            objectSpace = Application.CreateObjectSpace();
            RandevuTakip randevu = objectSpace.CreateObject<RandevuTakip>();

            e.View = Application.CreateDetailView(objectSpace, randevu);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            RandevuTakip randevu = e.PopupWindow.View.CurrentObject as RandevuTakip;
            if (randevu.StartOn < DateTime.Now)
            {
                throw new UserFriendlyException("Şu anki tarihten önceye randevu oluşturamazsınız.");
            }
            else
            {
                Muayene muayene = objectSpace.CreateObject<Muayene>();
                muayene.MuayeneTarihi = randevu.StartOn;
                muayene.RandevuSaati = string.Format("{0:00}:{1:00}", randevu.RandevuSaati.Hours, randevu.RandevuSaati.Minutes) + " - " + string.Format("{0:00}:{1:00}", randevu.RandevuBitisSaati.Hours, randevu.RandevuBitisSaati.Minutes);
                muayene.HastaListesi = randevu.Hastalar;
                muayene.Randevu = randevu;
                muayene.Aciklama = randevu.Description;
            }
            
        }
        //SatisFisleri satis = e.PopupWindow.View.CurrentObject as SatisFisleri;
        //IList<SatilanUrunler> liste = satis.UrunCollection;
        //IObjectSpace space = Application.CreateObjectSpace(typeof(StokHareket));
        //IObjectSpace stokspace = Application.CreateObjectSpace(typeof(BusinessObjects.StokKart));

        //IList list = ObjectSpace.GetObjects(typeof(BusinessObjects.StokKart));
        //string tabloadi = "SatisFisiOlustur";

        //    foreach (BusinessObjects.SatilanUrunler satir in liste)
        //    {
        //        satir.Stok.Stok_miktar = satir.Stok.Stok_miktar - satir.Adet;
        //        satir.Stok.Save();

        //        EsdegerGoster cariSatisEsdegerTablo = new EsdegerGoster(satir.Stok, tabloadi);
        //cariSatisEsdegerTablo.ShowDialog();
        //    }
    }
}
