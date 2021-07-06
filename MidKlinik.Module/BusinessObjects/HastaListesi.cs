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
    [ImageName("Mrs")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class HastaListesi : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public HastaListesi(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            HastaKayitTarihi = DateTime.Now;
        }
        private string _TCKimlikNumarası;
        public string TCKimlikNumarası
        {
            get { return _TCKimlikNumarası; }
            set { SetPropertyValue<string>(nameof(TCKimlikNumarası), ref _TCKimlikNumarası, value); }
        }

        private string _HastaAd;
        public string HastaAd
        {
            get { return _HastaAd; }
            set { SetPropertyValue<string>(nameof(HastaAd), ref _HastaAd, value); }
        }

        private string _HastaSoyad;
        public string HastaSoyad
        {
            get { return _HastaSoyad; }
            set { SetPropertyValue<string>(nameof(HastaSoyad), ref _HastaSoyad, value); }
        }

        public string HastaAdSoyad
        {
            get { return HastaAd + " " + HastaSoyad; }
            set { SetPropertyValue<string>(nameof(HastaAdSoyad), value); }
        }

        private DateTime _dogumTarihi;
        public DateTime dogumTarihi
        {
            get { return _dogumTarihi; }
            set { SetPropertyValue<DateTime>(nameof(dogumTarihi), ref _dogumTarihi, value); }
        }

        private string _TelefonNo;
        public string TelefonNo
        {
            get { return _TelefonNo; }
            set { SetPropertyValue<string>(nameof(TelefonNo), ref _TelefonNo, value); }
        }

        private CinsiyetEum _cinsiyet;
        [DevExpress.Xpo.DisplayName("Cinsiyet")]
        public CinsiyetEum cinsiyet
        {
            get { return _cinsiyet; }
            set { SetPropertyValue<CinsiyetEum>(nameof(cinsiyet), ref _cinsiyet, value); }
        }

        private KanGrubuEnum _KanGrubu;
        public KanGrubuEnum KanGrubu
        {
            get { return _KanGrubu; }
            set { SetPropertyValue<KanGrubuEnum>(nameof(KanGrubu), ref _KanGrubu, value); }
        }

        private DateTime _HastaKayitTarihi;
        public DateTime HastaKayitTarihi
        {
            get { return _HastaKayitTarihi; }
            set { SetPropertyValue<DateTime>(nameof(HastaKayitTarihi), ref _HastaKayitTarihi, value); }
        }

        [DevExpress.Xpo.Aggregated, Association]
        public XPCollection<Muayene> Muayeneler
        {
            get { return GetCollection<Muayene>(nameof(Muayeneler)); }
        }
        
        [DevExpress.Xpo.Aggregated, Association, DevExpress.Xpo.DisplayName("Hastaya Ait Dökümanlar")]
        public XPCollection<Dokuman> HastaDokumanlari
        {
            get { return GetCollection<Dokuman>(nameof(HastaDokumanlari)); }
        }

    }
    public enum CinsiyetEum
    {
        [ImageName("Mrs")]
        Kadın = 1,
        [ImageName("Mr")]
        Erkek = 2
    }
    public enum KanGrubuEnum
    {
        [ImageName("Actions_Add")]
        [XafDisplayName("0 Rh (+)")]
        SıfırRhPozitif = 1,
        [ImageName("Actions_Remove")]
        [XafDisplayName("0 Rh (-)")]
        SıfırRhNegatif = 2,
        [ImageName("Actions_Add")]
        [XafDisplayName("A Rh (+)")]
        ARhPozitif = 3,
        [ImageName("Actions_Remove")]
        [XafDisplayName("A Rh (-)")]
        ARhNegatif = 4,
        [ImageName("Actions_Add")]
        [XafDisplayName("B Rh (+)")]
        BRhPozitif = 5,
        [ImageName("Actions_Remove")]
        [XafDisplayName("B Rh (-)")]
        BRhNegatif = 6,
        [ImageName("Actions_Add")]
        [XafDisplayName("AB Rh (+)")]
        ABRhPozitif = 7,
        [ImageName("Actions_Remove")]
        [XafDisplayName("AB Rh (-)")]
        ABRhNegatif = 8
    }
}