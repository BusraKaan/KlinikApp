using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MidKlinik.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("ReversSort")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class GelirGider : BaseObject, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public GelirGider(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            OdemeTarihi = DateTime.Today;
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }

        private GelirGiderKategori _Kategori;
        [DevExpress.Xpo.DisplayName("Gelir/Gider Kategori")]
        public GelirGiderKategori Kategori
        {
            get { return _Kategori; }
            set
            {
                SetPropertyValue<GelirGiderKategori>(nameof(Kategori), ref _Kategori, value);
            }
        }

        private decimal _Ucret;
        [DevExpress.Xpo.DisplayName("Ücret")]
        public decimal Ucret
        {
            get { return _Ucret; }
            set { SetPropertyValue<decimal>(nameof(Ucret), ref _Ucret, value); }
        }

        private GelirGiderEnum _ParaHareketTür;
        [DevExpress.Xpo.DisplayName("Hareket Türü")]
        public GelirGiderEnum ParaHareketTür
        {
            get { return _ParaHareketTür; }
            set { SetPropertyValue<GelirGiderEnum>(nameof(ParaHareketTür), ref _ParaHareketTür, value); }
        }

        private OdemeTuruEnum _OdemeTür;
        [DevExpress.Xpo.DisplayName("Ödeme Türü")]
        public OdemeTuruEnum OdemeTür
        {
            get { return _OdemeTür; }
            set { SetPropertyValue<OdemeTuruEnum>(nameof(OdemeTür), ref _OdemeTür, value); }
        }

        private int _TaksitSayisi;
        [DevExpress.Xpo.DisplayName("Taksit Sayısı")]
        public int TaksitSayisi
        {
            get { return _TaksitSayisi; }
            set { SetPropertyValue<int>(nameof(TaksitSayisi), ref _TaksitSayisi, value); }
        }

        private string _Aciklama;
        [DevExpress.Xpo.DisplayName("Açıklama")]
        public string Aciklama
        {
            get { return _Aciklama; }
            set { SetPropertyValue<string>(nameof(Aciklama), ref _Aciklama, value); }
        }

        private DateTime _OdemeTarihi;
        [DevExpress.Xpo.DisplayName("Ödeme Tarihi")]
        public DateTime OdemeTarihi
        {
            get { return _OdemeTarihi; }
            set { SetPropertyValue<DateTime>(nameof(OdemeTarihi), ref _OdemeTarihi, value); }
        }
        //protected override void OnSaved()
        //{
        //    base.OnSaved();
        //    if (OdemeTür == OdemeTuruEnum.KrediKartı)
        //    {
        //        DateTime geciciTarih = OdemeTarihi;
        //        for (int i = 0; i < TaksitSayisi - 1; i++)
        //        {
        //            GelirGider gelirGider = objectSpace.CreateObject<GelirGider>();
        //            gelirGider.Kategori = _Kategori;
        //            gelirGider.OdemeTür = _OdemeTür;
        //            gelirGider.ParaHareketTür = _ParaHareketTür;
        //            gelirGider.Ucret = _Ucret / TaksitSayisi;
        //            gelirGider._OdemeTarihi = geciciTarih.AddDays(30);
        //            gelirGider.TaksitSayisi = _TaksitSayisi;
        //            gelirGider.Aciklama = (i + 2) + ". taksit";
        //        }
        //    }
        //}
    }

    public enum GelirGiderEnum
    {
        [ImageName("Actions_Arrow2Down")]
        Gelir = 0,
        [ImageName("Actions_Arrow2Up")]
        Gider = 1
    }
    public enum OdemeTuruEnum
    {
        [ImageName("Business_Cash")]
        Nakit = 0,
        [ImageName("Business_CreditCard")]
        KrediKartı = 1,
        [ImageName("Business_CreditCard")]
        BankaKartı = 2
    }
}