using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MidKlinik.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("Business_Report")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class BelgeBaslik : BaseObject, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public BelgeBaslik(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            BelgeTarih = DateTime.Now;
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }

        private BelgeTipiEnum _BelgeTipi;
        [DevExpress.Xpo.DisplayName("Belge Tipi")]
        public BelgeTipiEnum BelgeTipi
        {
            get { return _BelgeTipi; }
            set { SetPropertyValue<BelgeTipiEnum>(nameof(BelgeTipi), ref _BelgeTipi, value); }
        }

        private BelgeTurEnum _BelgeTur;
        [DevExpress.Xpo.DisplayName("Belge Türü")]
        public BelgeTurEnum BelgeTur
        {
            get { return _BelgeTur; }
            set { SetPropertyValue<BelgeTurEnum>(nameof(BelgeTur), ref _BelgeTur, value); }
        }

        private Cari _CariBilgiler;
        [DevExpress.Xpo.DisplayName("Cari Adı")]
        public Cari CariBilgiler
        {
            get { return _CariBilgiler; }
            set { SetPropertyValue<Cari>(nameof(Cari), ref _CariBilgiler, value); }
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

        private double _ToplamFiyat = 0;
        [DevExpress.Xpo.DisplayName("Tavsiye Edilen Satış Fiyatı(KDV Hariç)")]
        [PersistentAlias("BelgeSatir.Sum(<BusinessObjects.BelgeSatir>[KDVharicTutar])")]
        [ImmediatePostData]
        public double ToplamFiyat
        {
            get { return Convert.ToDouble(EvaluateAlias("ToplamFiyat")); }
            set { SetPropertyValue<double>(nameof(ToplamFiyat), ref _ToplamFiyat, value); }
        }

        private double _KDVtoplam;
        [DevExpress.Xpo.DisplayName("KDV Toplam")]
        [PersistentAlias("BelgeSatir.Sum(<BusinessObjects.BelgeSatir>[KDVtutari])")]
        [ImmediatePostData]
        public double KDVtoplam
        {
            get { return Convert.ToDouble(EvaluateAlias("KDVtoplam")); }
            set { SetPropertyValue<double>(nameof(KDVtoplam), ref _KDVtoplam, value); }
        }

        private double _KDVDahiltoplam;
        [DevExpress.Xpo.DisplayName("Tavsiye Edilen Satış Fiyatı(KDV Dahil)")]
        [PersistentAlias("BelgeSatir.Sum(<BusinessObjects.BelgeSatir>[KDVtutari]) + BelgeSatir.Sum(<BusinessObjects.BelgeSatir>[KDVharicTutar])")]
        [ImmediatePostData]
        public double KDVDahiltoplam
        {
            get { return Convert.ToDouble(EvaluateAlias("KDVDahiltoplam")); }
            set
            {
                SetPropertyValue<double>(nameof(KDVDahiltoplam), ref _KDVDahiltoplam, value);
                if (!IsLoading)
                {
                    SatisFiyati = KDVDahiltoplam;
                }
            }
        }

        private double _SatisFiyati;
        [DevExpress.Xpo.DisplayName("Satış Fiyatı")]
        [ImmediatePostData]
        public double SatisFiyati
        {
            get { return _SatisFiyati; }
            set { SetPropertyValue<double>(nameof(SatisFiyati), ref _SatisFiyati, value); }
        }

        private bool _islemYapildiMi;
        [DevExpress.Xpo.DisplayName("İşlem Yapıldı mı?")]
        public bool islemYapildiMi
        {
            get { return _islemYapildiMi; }
            set { SetPropertyValue<bool>(nameof(islemYapildiMi), ref _islemYapildiMi, value); }
        }

        [DevExpress.Xpo.Aggregated, Association, DevExpress.Xpo.DisplayName("Ürün Listesi")]
        public XPCollection<BelgeSatir> BelgeSatir
        {
            get { return GetCollection<BelgeSatir>(nameof(BelgeSatir)); }
        }

        [DevExpress.Xpo.Aggregated, Association, DevExpress.Xpo.DisplayName("Belge Döküman Bilgisi")]
        public XPCollection<Dokuman> BelgeDokuman
        {
            get { return GetCollection<Dokuman>(nameof(BelgeDokuman)); }
        }
        
        protected override void OnSaving()
        {
            base.OnSaving();
            if (islemYapildiMi == true)
            {
                throw new UserFriendlyException("Bu belge için işlem zaten yapıldı.\nYeni işlem yapmak için yeni bir belge oluşturunuz.");
            }
            else
            {
                foreach (BelgeSatir item in BelgeSatir)
                {
                    CriteriaOperator criteria2 = CriteriaOperator.Parse("[Oid]=?", item.StokList.Oid);
                    Stok listeStok = objectSpace.FindObject(typeof(Stok), criteria2) as Stok;

                    if (BelgeTipi == BelgeTipiEnum.Alış)
                    {
                        if (listeStok != null)
                        {
                            listeStok.Miktar += item.Miktar;
                        }
                    }
                    else if (BelgeTipi == BelgeTipiEnum.Satış)
                    {
                        if (listeStok != null)
                        {
                            listeStok.Miktar -= item.Miktar;
                        }
                    }
                }

                try
                {
                    objectSpace.CommitChanges();
                }
                catch (Exception)
                {

                }

                if (BelgeTur == BelgeTurEnum.İrsaliye)
                {
                    KDVtoplam = 0;
                    KDVDahiltoplam = 0;
                    ToplamFiyat = 0;
                    SatisFiyati = 0;
                }

                CariHareket cariHareket = objectSpace.CreateObject<CariHareket>();
                cariHareket.CariBilgileri = CariBilgiler;
                cariHareket.BelgeNo = BelgeNo;
                cariHareket.BelgeTarih = BelgeTarih;
                cariHareket.BelgeTip = BelgeTipi;
                cariHareket.BelgeTur = BelgeTur;
                cariHareket.KDVtoplam = KDVtoplam;
                cariHareket.KDVharicToplam = ToplamFiyat;
                cariHareket.KDVdahilToplam = KDVDahiltoplam;

                islemYapildiMi = true;
            }
            

            
        }

    }
    public enum BelgeTurEnum
    {
        [ImageName("Shopping_Package")]
        İrsaliye = 1,
        [ImageName("Shopping_CashVoucher")]
        Fatura = 2
    }
    public enum BelgeTipiEnum
    {
        [ImageName("Actions_Arrow2Down")]
        Alış = 1,
        [ImageName("Actions_Arrow2Up")]
        Satış = 2
    }
}