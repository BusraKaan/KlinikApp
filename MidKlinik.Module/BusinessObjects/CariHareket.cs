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
    [ImageName("ArrangeGroups")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class CariHareket : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public CariHareket(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            BelgeTarih = DateTime.Now;
        }

        private string _BelgeNo;
        [DevExpress.Xpo.DisplayName("Belge No")]
        public string BelgeNo
        {
            get { return _BelgeNo; }
            set { SetPropertyValue<string>(nameof(BelgeNo), ref _BelgeNo, value); }
        }
        
        private DateTime _BelgeTarih;
        [DevExpress.Xpo.DisplayName("Belge Tarihi")]
        public DateTime BelgeTarih
        {
            get { return _BelgeTarih; }
            set { SetPropertyValue<DateTime>(nameof(BelgeTarih), ref _BelgeTarih, value); }
        }
        
        private double _KDVharicToplam;
        [DevExpress.Xpo.DisplayName("Toplam Fiyat(KDV Hariç)")]
        public double KDVharicToplam
        {
            get { return _KDVharicToplam; }
            set { SetPropertyValue<double>(nameof(KDVharicToplam), ref _KDVharicToplam, value); }
        }
        
        private double _KDVtoplam;
        [DevExpress.Xpo.DisplayName("KDV Toplam")]
        public double KDVtoplam
        {
            get { return _KDVtoplam; }
            set { SetPropertyValue<double>(nameof(KDVtoplam), ref _KDVtoplam, value); }
        }
        
        private double _KDVdahilToplam;
        [DevExpress.Xpo.DisplayName("Toplam Fiyat(KDV Dahil)")]
        public double KDVdahilToplam
        {
            get { return _KDVdahilToplam; }
            set { SetPropertyValue<double>(nameof(KDVdahilToplam), ref _KDVdahilToplam, value); }
        }
        
        private BelgeTurEnum _BelgeTur;
        [DevExpress.Xpo.DisplayName("Belge Türü")]
        public BelgeTurEnum BelgeTur
        {
            get { return _BelgeTur; }
            set { SetPropertyValue<BelgeTurEnum>(nameof(BelgeTur), ref _BelgeTur, value); }
        }
        
        private BelgeTipiEnum _BelgeTip;
        [DevExpress.Xpo.DisplayName("Belge Tipi")]
        public BelgeTipiEnum BelgeTip
        {
            get { return _BelgeTip; }
            set { SetPropertyValue<BelgeTipiEnum>(nameof(BelgeTip), ref _BelgeTip, value); }
        }

        private Cari _CariBilgileri;
        [Association, DevExpress.Xpo.DisplayName("Cari Bilgileri")]
        public Cari CariBilgileri
        {
            get { return _CariBilgileri; }
            set { SetPropertyValue<Cari>(nameof(CariBilgileri), ref _CariBilgileri, value); }
        }
    }
}