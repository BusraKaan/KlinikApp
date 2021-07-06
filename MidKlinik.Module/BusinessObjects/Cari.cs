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
    [ImageName("Business_Briefcase")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Cari : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Cari(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private string _CariKodu;
        [DevExpress.Xpo.DisplayName("Cari Kodu")]
        public string CariKodu
        {
            get { return _CariKodu; }
            set { SetPropertyValue<string>(nameof(CariKodu), ref _CariKodu, value); }
        }
        
        private string _CariAdi;
        [DevExpress.Xpo.DisplayName("Cari Adı")]
        public string CariAdi
        {
            get { return _CariAdi; }
            set { SetPropertyValue<string>(nameof(CariAdi), ref _CariAdi, value); }
        }
        
        private string _Telefon;
        [DevExpress.Xpo.DisplayName("Telefon")]
        public string Telefon
        {
            get { return _Telefon; }
            set { SetPropertyValue<string>(nameof(Telefon), ref _Telefon, value); }
        }
        
        private string _Email;
        [DevExpress.Xpo.DisplayName("Email")]
        public string Email
        {
            get { return _Email; }
            set { SetPropertyValue<string>(nameof(Email), ref _Email, value); }
        }
        
        private string _Adres;
        [DevExpress.Xpo.DisplayName("Adres")]
        public string Adres
        {
            get { return _Adres; }
            set { SetPropertyValue<string>(nameof(Adres), ref _Adres, value); }
        }

        private string _VergiDairesi;
        [DevExpress.Xpo.DisplayName("Vergi Dairesi")]
        public string VergiDairesi
        {
            get { return _VergiDairesi; }
            set { SetPropertyValue<string>(nameof(VergiDairesi), ref _VergiDairesi, value); }
        }
        
        private string _VergiNumarasi;
        [DevExpress.Xpo.DisplayName("Vergi Numarası")]
        public string VergiNumarasi
        {
            get { return _VergiNumarasi; }
            set { SetPropertyValue<string>(nameof(VergiNumarasi), ref _VergiNumarasi, value); }
        }

        [DevExpress.Xpo.Aggregated, Association, DevExpress.Xpo.DisplayName("Cari Yetkili Bilgileri")]
        public XPCollection<YetkiliKisiler> YetkiliKisiler
        {
            get { return GetCollection<YetkiliKisiler>(nameof(YetkiliKisiler)); }
        }
        [DevExpress.Xpo.Aggregated, Association, DevExpress.Xpo.DisplayName("Cari Hareketleri")]
        public XPCollection<CariHareket> CariHareketleri
        {
            get { return GetCollection<CariHareket>(nameof(CariHareketleri)); }
        }
    }
}