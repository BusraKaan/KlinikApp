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
    [ImageName("Shopping_Box")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Stok : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Stok(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private string _StokKodu;
        [DevExpress.Xpo.DisplayName("Stok Kodu")]
        public string StokKodu
        {
            get { return _StokKodu; }
            set { SetPropertyValue<string>(nameof(StokKodu), ref _StokKodu, value); }
        }

        private string _StokAdi;
        [DevExpress.Xpo.DisplayName("Stok Adı")]
        public string StokAdi
        {
            get { return _StokAdi; }
            set { SetPropertyValue<string>(nameof(StokAdi), ref _StokAdi, value); }
        }

        private int _Miktar;
        [DevExpress.Xpo.DisplayName("Stok Miktarı")]
        public int Miktar
        {
            get { return _Miktar; }
            set { SetPropertyValue<int>(nameof(Miktar), ref _Miktar, value); }
        }

        private double _BirimFiyat;
        [DevExpress.Xpo.DisplayName("Birim Fiyatı")]
        public double BirimFiyat
        {
            get { return _BirimFiyat; }
            set { SetPropertyValue<double>(nameof(BirimFiyat), ref _BirimFiyat, value); }
        }

        private ParaBirimiEnum _ParaBirimi;
        [DevExpress.Xpo.DisplayName("Para Birimi")]
        public ParaBirimiEnum ParaBirimi
        {
            get { return _ParaBirimi; }
            set { SetPropertyValue<ParaBirimiEnum>(nameof(ParaBirimi), ref _ParaBirimi, value); }
        }

        private KDVOranlari _KDVorani;
        [DevExpress.Xpo.DisplayName("KDV Oranı %")]
        public KDVOranlari KDVorani
        {
            get { return _KDVorani; }
            set { SetPropertyValue<KDVOranlari>(nameof(KDVorani), ref _KDVorani, value); }
        }

        private string _StokMarka;
        [DevExpress.Xpo.DisplayName("Stok Markası")]
        public string StokMarka
        {
            get { return _StokMarka; }
            set { SetPropertyValue<string>(nameof(StokMarka), ref _StokMarka, value); }
        }

        private BelgeSatir _BelgeSatir;
        [Association]
        public BelgeSatir BelgeSatir
        {
            get { return _BelgeSatir; }
            set { SetPropertyValue<BelgeSatir>(nameof(BelgeSatir), ref _BelgeSatir, value); }
        }

        [DevExpress.Xpo.Aggregated, Association, DevExpress.Xpo.DisplayName("Kullanılan Ürünler")]
        public XPCollection<MuayeneStok> MuayeneStokBilgileri
        {
            get { return GetCollection<MuayeneStok>(nameof(MuayeneStokBilgileri)); }
        }

        
    }
    public enum ParaBirimiEnum
    {
        TürkLirası = 1,
        Dolar = 2,
        Euro = 3
    }
}