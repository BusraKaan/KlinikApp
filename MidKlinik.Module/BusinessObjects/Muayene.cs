using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Utils.CommonDialogs.Internal;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace MidKlinik.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("ShowAllValue")]
    public class Muayene : BaseObject, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).

        public Muayene(Session session)
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

        private DateTime _MuayeneTarihi;
        public DateTime MuayeneTarihi
        {
            get { return _MuayeneTarihi; }
            set { SetPropertyValue<DateTime>(nameof(MuayeneTarihi), ref _MuayeneTarihi, value); }
        }

        private string _RandevuSaati;
        [DevExpress.Xpo.DisplayName("Muayene Saati")]
        public string RandevuSaati
        {
            get { return _RandevuSaati; }
            set { SetPropertyValue<string>(nameof(RandevuSaati), ref _RandevuSaati, value); }
        }

        private MuayeneDurum _Durum;
        [DevExpress.Xpo.DisplayName("Muayene Durum")]
        public MuayeneDurum Durum
        {
            get { return _Durum; }
            set { SetPropertyValue<MuayeneDurum>(nameof(Durum), ref _Durum, value); }
        }

        private string _Sikayeti;
        [DevExpress.Xpo.DisplayName("Şikayeti")]
        public string Sikayeti
        {
            get { return _Sikayeti; }
            set { SetPropertyValue<string>(nameof(Sikayeti), ref _Sikayeti, value); }
        }

        private string _Hikayesi;
        [DevExpress.Xpo.DisplayName("Hikayesi")]
        public string Hikayesi
        {
            get { return _Hikayesi; }
            set { SetPropertyValue<string>(nameof(Hikayesi), ref _Hikayesi, value); }
        }

        private string _Aciklama;
        [DevExpress.Xpo.DisplayName("Açıklama")]
        public string Aciklama
        {
            get { return _Aciklama; }
            set { SetPropertyValue<string>(nameof(Aciklama), ref _Aciklama, value); }
        }

        private string _Bulgular;
        [DevExpress.Xpo.DisplayName("Bulgular")]
        public string Bulgular
        {
            get { return _Bulgular; }
            set { SetPropertyValue<string>(nameof(Bulgular), ref _Bulgular, value); }
        }

        private bool _kayit;
        public bool kayit
        {
            get { return false; }
            set { SetPropertyValue<bool>(nameof(kayit), ref _kayit, value); }
        }

        [DevExpress.Xpo.Aggregated, Association, DevExpress.Xpo.DisplayName("Hastaya Uygulanacak İşlemler")]
        public XPCollection<HastaIslemleri> HastaIslemleri
        {
            get { return GetCollection<HastaIslemleri>(nameof(HastaIslemleri)); }
        }

        [DevExpress.Xpo.Aggregated, Association, DevExpress.Xpo.DisplayName("Kullanılacak Ürünler")]
        public XPCollection<MuayeneStok> MuayeneStokBilgisi
        {
            get { return GetCollection<MuayeneStok>(nameof(MuayeneStokBilgisi)); }
        }

        private HastaListesi _HastaListesi;
        [Association, DevExpress.Xpo.DisplayName("Hasta Bilgisi")]
        public HastaListesi HastaListesi
        {
            get { return _HastaListesi; }
            set { SetPropertyValue<HastaListesi>(nameof(HastaListesi), ref _HastaListesi, value); }
        }

        private RandevuTakip _Randevu;
        [Association, DevExpress.Xpo.DisplayName("Randevu")]
        public RandevuTakip Randevu
        {
            get { return _Randevu; }
            set { SetPropertyValue<RandevuTakip>(nameof(Randevu), ref _Randevu, value); }
        }
        protected override void OnSaving()
        {
            base.OnSaving();
            MuayeneStokKontrol();
            HastaIslemleriKontrol();
        }

        private void HastaIslemleriKontrol()
        {
            foreach (HastaIslemleri item in HastaIslemleri)
            {
                item.ilkSeansTrh = item.ilkSeansTrh + item.RandevuSaati;
                if (item.ilkSeansTrh < DateTime.Now)
                {
                    throw new UserFriendlyException("Şu anki tarihten önceye randevu oluşturamazsınız.");
                }
                else
                {
                    if (item.SeansAraligi != 0 && item.SeansSayisi != 0 && item.ilkSeansTrh != Convert.ToDateTime("1.01.0001 00:00:00") && item.kayitControl == false)
                    {
                        DateTime geciciTarih = item.ilkSeansTrh;
                        for (int i = 0; i < item.SeansSayisi; i++)
                        {
                            Muayene muayene = objectSpace.CreateObject<Muayene>();
                            RandevuTakip randevu = objectSpace.CreateObject<RandevuTakip>();
                            muayene.Randevu = randevu;
                            randevu.Hastalar = item.Muayene.HastaListesi;
                            randevu.StartOn = geciciTarih.Date.Add(item.RandevuSaati);
                            randevu.EndOn = geciciTarih.Date.Add(item.RandevuBitisSaati);
                            randevu.Subject = item.Muayene.HastaListesi.HastaAdSoyad + " " + item.Muayene.HastaListesi.TCKimlikNumarası;
                            randevu.Description = item.Islemler.IslemAdi + " " + (i + 1) + ". seans";
                            muayene.Aciklama = item.Islemler.IslemAdi + " " + (i + 1) + ". seans";
                            muayene.MuayeneTarihi = geciciTarih;
                            muayene.RandevuSaati = string.Format("{0:00}:{1:00}", item.RandevuSaati.Hours, item.RandevuSaati.Minutes) + " - "
                        + string.Format("{0:00}:{1:00}", item.RandevuBitisSaati.Hours, item.RandevuBitisSaati.Minutes);
                            muayene.HastaListesi = item.Muayene.HastaListesi;
                            switch (item.AralikTuru)
                            {
                                case AralikTuruEnum.Gün:
                                    geciciTarih = geciciTarih.AddDays(item.SeansAraligi);
                                    break;
                                case AralikTuruEnum.Hafta:
                                    geciciTarih = geciciTarih.AddDays(item.SeansAraligi * 7);
                                    break;
                                case AralikTuruEnum.Ay:
                                    geciciTarih = geciciTarih.AddDays(item.SeansAraligi * 30);
                                    break;
                                default:
                                    break;
                            }
                            item.kayitControl = true;

                        }
                    }
                }

            }
        }

        private void MuayeneStokKontrol()
        {
            bool stokWarning = false;
            string stokAdi = "";
            foreach (MuayeneStok item in MuayeneStokBilgisi)
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Oid=?", item.StokList.Oid);
                Stok urun = objectSpace.FindObject(typeof(Stok), criteria) as Stok;
                if (urun != null && item.Miktar != 0 && item.kayitControl == false)
                {
                    urun.Miktar -= item.Miktar;
                    urun.Save();
                    item.kayitControl = true;
                    if (urun.Miktar <= 0)
                    {
                        stokWarning = true;
                        stokAdi += "*" + urun.StokAdi + "\n";
                    }
                }
            }
            if (stokWarning == true)
            {
                XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                args.Caption = "Urun Kullanımı";
                args.Text = "Stokta yeterince \n" + stokAdi + "bulunmuyor. Ürünleri temin etmeniz gerekecek!!";
                //args.Buttons = new DialogResult[] { DialogResult.OK };
                XtraMessageBox.Show(args).ToString();
            }
        }

        protected override void OnDeleting()
        {
            base.OnDeleting();

            //char[] ayrac = { ' ', '-' };
            //string[] saatler = RandevuSaati.Split(ayrac);
            //DateTime saat = Convert.ToDateTime(saatler[0]);
            //if ((MuayeneTarihi + saat.TimeOfDay) > DateTime.Now)
            //{
            //    CriteriaOperator criteria = CriteriaOperator.Parse("Oid = ?", Randevu.Oid);
            //    RandevuTakip randevu = objectSpace.FindObject(typeof(RandevuTakip), criteria) as RandevuTakip;
            //    if (randevu != null)
            //    {
            //        objectSpace.Delete(randevu);
            //        objectSpace.CommitChanges();
            //    }
            //}
        }
    }
}