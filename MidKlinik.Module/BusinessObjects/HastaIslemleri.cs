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
    [ImageName("BO_StateMachine")]
    public class HastaIslemleri : BaseObject, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public HastaIslemleri(Session session)
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

        private bool _kayitControl;
        public bool kayitControl
        {
            get { return _kayitControl; }
            set { SetPropertyValue<bool>(nameof(kayitControl), ref _kayitControl, value); }
        }

        private UygulananIslemler _Islemler;
        public UygulananIslemler Islemler
        {
            get { return _Islemler; }
            set
            {
                SetPropertyValue<UygulananIslemler>(nameof(UygulananIslemler), ref _Islemler, value);
                if (!IsLoading)
                {
                    AralikTuru = AralikTuruEnum.Gün;
                    kayitControl = false;
                }
            }
        }

        private Muayene _Muayene;
        [Association]
        public Muayene Muayene
        {
            get { return _Muayene; }
            set { SetPropertyValue<Muayene>(nameof(Muayene), ref _Muayene, value); }
        }

        private int _SeansSayisi;
        [DevExpress.Xpo.DisplayName("Seans Sayisi")]
        public int SeansSayisi
        {
            get { return _SeansSayisi; }
            set { SetPropertyValue<int>(nameof(SeansSayisi), ref _SeansSayisi, value); }
        }

        private int _SeansAraligi;
        [DevExpress.Xpo.DisplayName("Seans Aralığı")]
        public int SeansAraligi
        {
            get { return _SeansAraligi; }
            set
            {
                SetPropertyValue<int>(nameof(SeansAraligi), ref _SeansAraligi, value);
            }
        }

        private AralikTuruEnum _AralikTuru;
        [DevExpress.Xpo.DisplayName("Seans Aralık Türü")]
        public AralikTuruEnum AralikTuru
        {
            get { return _AralikTuru; }
            set
            {
                SetPropertyValue<AralikTuruEnum>(nameof(AralikTuru), ref _AralikTuru, value);
            }
        }

        private DateTime _ilkSeansTrh;
        [DevExpress.Xpo.DisplayName("İlk Seans Tarihi")]
        public DateTime ilkSeansTrh
        {
            get { return _ilkSeansTrh; }
            set { SetPropertyValue<DateTime>(nameof(ilkSeansTrh), ref _ilkSeansTrh, value); }
        }

        private TimeSpan _RandevuSaati;
        [ModelDefault("EditMaskType", "DateTime")]
        public TimeSpan RandevuSaati
        {
            get { return _RandevuSaati; }
            set
            {
                SetPropertyValue(nameof(RandevuSaati), ref _RandevuSaati, value);
                if (!IsLoading)
                {
                    RandevuBitisSaati = RandevuSaati.Add(TimeSpan.FromMinutes(30));
                }
            }
        }

        private TimeSpan _RandevuBitisSaati;
        [ModelDefault("EditMaskType", "DateTime")]
        public TimeSpan RandevuBitisSaati
        {
            get { return _RandevuBitisSaati; }
            set { SetPropertyValue(nameof(RandevuBitisSaati), ref _RandevuBitisSaati, value);
                if (!IsLoading)
                {
                    //SeansOlustur();
                }
            }
        }

        private void SeansOlustur()
        {
            if (SeansAraligi != 0 && SeansSayisi != 0 && ilkSeansTrh != Convert.ToDateTime("1.01.0001 00:00:00"))
            {
                DateTime geciciTarih = ilkSeansTrh;
                for (int i = 0; i < SeansSayisi; i++)
                {
                    Muayene muayene = objectSpace.CreateObject<Muayene>();
                    RandevuTakip randevu = objectSpace.CreateObject<RandevuTakip>();
                    muayene.Randevu = randevu;
                    randevu.Hastalar = Muayene.HastaListesi;
                    randevu.StartOn = geciciTarih.Date.Add(RandevuSaati);
                    randevu.EndOn = geciciTarih.Date.Add(RandevuBitisSaati);
                    randevu.Subject = Muayene.HastaListesi.HastaAdSoyad + " " + Muayene.HastaListesi.TCKimlikNumarası;
                    muayene.MuayeneTarihi = geciciTarih;
                    muayene.Randevu.Description = Islemler.IslemAdi + " " + (i+1) + ". seans";
                    muayene.RandevuSaati= string.Format("{0:00}:{1:00}", RandevuSaati.Hours, RandevuSaati.Minutes) + " - "
                    + string.Format("{0:00}:{1:00}", RandevuBitisSaati.Hours, RandevuBitisSaati.Minutes);
                    muayene.HastaListesi = Muayene.HastaListesi;
                    switch (AralikTuru)
                    {
                        case AralikTuruEnum.Gün:
                            geciciTarih = geciciTarih.AddDays(SeansAraligi);
                            break;
                        case AralikTuruEnum.Hafta:
                            geciciTarih = geciciTarih.AddDays(SeansAraligi * 7);
                            break;
                        case AralikTuruEnum.Ay:
                            geciciTarih = geciciTarih.AddDays(SeansAraligi * 30);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    public enum AralikTuruEnum
    {
        [ImageName("DayView")]
        Gün = 0,
        [ImageName("FullWeekView")]
        Hafta = 1,
        [ImageName("MonthView")]
        Ay = 2
    }
}