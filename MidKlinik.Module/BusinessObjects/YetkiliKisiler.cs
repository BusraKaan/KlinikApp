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
    [ImageName("BO_Lead")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class YetkiliKisiler : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public YetkiliKisiler(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private string _YetkiliAdSoyad;
        [DevExpress.Xpo.DisplayName("Yetkili Ad Soyad")]
        public string YetkiliAdSoyad
        {
            get { return _YetkiliAdSoyad; }
            set { SetPropertyValue<string>(nameof(YetkiliAdSoyad), ref _YetkiliAdSoyad, value); }
        }
        
        private string _YetkiliTelefon;
        [DevExpress.Xpo.DisplayName("Yetkili Telefon")]
        public string YetkiliTelefon
        {
            get { return _YetkiliTelefon; }
            set { SetPropertyValue<string>(nameof(YetkiliTelefon), ref _YetkiliTelefon, value); }
        }
        
        private string _YetkiliEmail;
        [DevExpress.Xpo.DisplayName("Yetkili Email")]
        public string YetkiliEmail
        {
            get { return _YetkiliEmail; }
            set { SetPropertyValue<string>(nameof(YetkiliEmail), ref _YetkiliEmail, value); }
        }

        private Cari _CariBilgileri;
        [Association]
        public Cari CariBilgileri
        {
            get { return _CariBilgileri; }
            set { SetPropertyValue<Cari>(nameof(CariBilgileri), ref _CariBilgileri, value); }
        }

    }
}