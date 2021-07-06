using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MidKlinik.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("Business_Report")]
    public class BelgeSatir : BaseObject, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public BelgeSatir(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }

        [DevExpress.Xpo.Aggregated, Association, DevExpress.Xpo.DisplayName("Stok Bilgileri")]
        public XPCollection<Stok> Stok
        {
            get { return GetCollection<Stok>(nameof(Stok)); }
        }

        private Stok _StokList;
        [DevExpress.Xpo.DisplayName("Ürünler")]
        public Stok StokList
        {
            get { return _StokList; }
            set { SetPropertyValue<Stok>(nameof(StokList), ref _StokList, value);
                if (!IsLoading)
                {
                    KDVorani = StokList.KDVorani.KDVorani;
                    BirimFiyat = StokList.BirimFiyat;
                }
            }
        }

        private BelgeBaslik _BelgeBaslik;
        [Association, DevExpress.Xpo.DisplayName("Belge Başlık")]
        public BelgeBaslik BelgeBaslik
        {
            get { return _BelgeBaslik; }
            set { SetPropertyValue<BelgeBaslik>(nameof(BelgeBaslik), ref _BelgeBaslik, value); }
        }

        private int _Miktar;
        [DevExpress.Xpo.DisplayName("Miktar")]
        public int Miktar
        {
            get { return _Miktar; }
            set
            {
                SetPropertyValue<int>(nameof(Miktar), ref _Miktar, value);
                if (!IsLoading)
                {
                    BelgeBaslikGuncelle();
                }
            }
        }
        
        private double _BirimFiyat;
        [DevExpress.Xpo.DisplayName("Birim Fiyat")]
        public double BirimFiyat
        {
            get { return _BirimFiyat; }
            set
            {
                SetPropertyValue<double>(nameof(BirimFiyat), ref _BirimFiyat, value);
                if (!IsLoading)
                {
                    BelgeBaslikGuncelle();
                }
            }
        }

        private double _KDVorani;
        [DevExpress.Xpo.DisplayName("KDV Oranı %")]
        public double KDVorani
        {
            get { return _KDVorani; }
            set { SetPropertyValue<double>(nameof(KDVorani), ref _KDVorani, value);
                if (!IsLoading)
                {
                    BelgeBaslikGuncelle();
                }
            }
        }

        private double _KDVtutari;
        [DevExpress.Xpo.DisplayName("KDV Tutarı")]
        public double KDVtutari
        {
            get { return _KDVtutari; }
            set { SetPropertyValue<double>(nameof(KDVtutari), ref _KDVtutari, value); }
        }

        private double _KDVharicTutar;
        [DevExpress.Xpo.DisplayName("Mal/Hizmet Tutarı(KDV Hariç)")]
        public double KDVharicTutar
        {
            get { return _KDVharicTutar; }
            set { SetPropertyValue<double>(nameof(KDVharicTutar), ref _KDVharicTutar, value); }
        }

        private void BelgeBaslikGuncelle()
        {
            KDVtutari = (KDVorani / 100) * (Convert.ToDouble(Miktar) * BirimFiyat);
            KDVharicTutar = (Convert.ToDouble(Miktar) * BirimFiyat);
            BelgeBaslik.ToplamFiyat *= 1;
            BelgeBaslik.KDVtoplam *= 1;
            BelgeBaslik.KDVDahiltoplam *= 1;
        }



    }
}