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

namespace MidKlinik.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Product_Group")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class MuayeneStok : BaseObject, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public MuayeneStok(Session session)
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

        [DevExpress.Xpo.Aggregated, Association, DevExpress.Xpo.DisplayName("Kullanılan Ürünler")]
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
                    kayitControl = false;
                }
            }
        }

        private bool _kayitControl;
        public bool kayitControl
        {
            get { return _kayitControl; }
            set { SetPropertyValue<bool>(nameof(kayitControl), ref _kayitControl, value); }
        }

        private int _Miktar;
        [DevExpress.Xpo.DisplayName("Miktar")]
        public int Miktar
        {
            get { return _Miktar; }
            set { SetPropertyValue<int>(nameof(Miktar), ref _Miktar, value); }
        }

        private Muayene _MuayeneBilgileri;
        [Association]
        public Muayene MuayeneBilgileri
        {
            get { return _MuayeneBilgileri; }
            set { SetPropertyValue<Muayene>(nameof(MuayeneBilgileri), ref _MuayeneBilgileri, value); }
        }
        protected override void OnSaving()
        {
            base.OnSaving();
        }
    }
}