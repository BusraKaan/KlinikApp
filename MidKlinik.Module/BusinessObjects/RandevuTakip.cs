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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MidKlinik.Module.BusinessObjects
{
    [DefaultClassOptions]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [ImageName("Time")]
    public class RandevuTakip : Event, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public RandevuTakip(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            RandevuTarihi = DateTime.Now;
            RandevuSaati = RandevuSaati + DateTime.Now.TimeOfDay;
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }

        private HastaListesi _Hastalar;
        public HastaListesi Hastalar
        {
            get { return _Hastalar; }
            set
            {
                SetPropertyValue<HastaListesi>(nameof(HastaListesi), ref _Hastalar, value);
                if (!IsLoading)
                {
                    Subject = Hastalar.HastaAdSoyad + " " + Hastalar.TCKimlikNumarası;
                }
            }
        }

        [DevExpress.Xpo.Aggregated, Association]
        public XPCollection<Muayene> Muayeneler
        {
            get { return GetCollection<Muayene>(nameof(Muayeneler)); }
        }

        private DateTime _RandevuTarihi;
        public DateTime RandevuTarihi
        {
            get { return _RandevuTarihi; }
            set
            {
                SetPropertyValue<DateTime>(nameof(RandevuTarihi), ref _RandevuTarihi, value);
                if (!IsLoading)
                {
                    RandevuTarihiOlustur();
                }
            }
        }

        private void MuayeneGuncelle()
        {
            CriteriaOperator criteria = CriteriaOperator.Parse("Randevu = ?", Oid);
            Muayene muayene = objectSpace.FindObject(typeof(Muayene), criteria) as Muayene;
            if (muayene != null)
            {
                muayene.MuayeneTarihi = StartOn;
                muayene.HastaListesi = Hastalar;
                muayene.RandevuSaati = string.Format("{0:00}:{1:00}", RandevuSaati.Hours, RandevuSaati.Minutes) + " - "
                    + string.Format("{0:00}:{1:00}", RandevuBitisSaati.Hours, RandevuBitisSaati.Minutes);
                muayene.Save();
            }
        }

        private void RandevuTarihiOlustur()
        {
            StartOn = RandevuTarihi.Date.Add(RandevuSaati);
            EndOn = RandevuTarihi.Date.Add(RandevuBitisSaati);
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
                    RandevuTarihiOlustur();
                }
            }
        }

        private TimeSpan _RandevuBitisSaati;
        [ModelDefault("EditMaskType", "DateTime")]
        public TimeSpan RandevuBitisSaati
        {
            get { return _RandevuBitisSaati; }
            set
            {
                SetPropertyValue(nameof(RandevuBitisSaati), ref _RandevuBitisSaati, value);
                if (!IsLoading)
                {
                    RandevuTarihiOlustur();
                }
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            MuayeneGuncelle();
        }
        protected override void OnDeleting()
        {
            base.OnDeleting();
            if (StartOn > DateTime.Now)
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Randevu = ?", Oid);
                Muayene muayene = objectSpace.FindObject(typeof(Muayene), criteria) as Muayene;
                if (muayene != null)
                {
                    objectSpace.Delete(muayene);
                    objectSpace.CommitChanges();
                }
            }
            else
            {
                throw new UserFriendlyException("Tarihi geçmiş randevu ve muayene silinemez.");
            }
        }
    }
}