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
    [ImageName("Tasks")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Dokuman : BaseObject, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Dokuman(Session session)
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

        private FileData file;
        [DevExpress.ExpressApp.DC.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        public FileData File
        {
            get { return file; }
            set
            {
                SetPropertyValue(nameof(File), ref file, value);
                if (!IsLoading)
                {
                    if (HastaListesi != null)
                    {
                        HastaListesi.Save();
                        objectSpace.CommitChanges();
                    }
                    else if (BelgeDokuman != null)
                    {
                        BelgeDokuman.Save();
                        objectSpace.CommitChanges();
                    }
                }
            }
        }

        private HastaListesi _HastaListesi;
        [Association, DevExpress.Xpo.DisplayName("Hasta Bilgisi")]
        public HastaListesi HastaListesi
        {
            get { return _HastaListesi; }
            set { SetPropertyValue<HastaListesi>(nameof(HastaListesi), ref _HastaListesi, value); }
        }

        private BelgeBaslik _BelgeDokuman;
        [Association, DevExpress.Xpo.DisplayName("Fatura Bilgisi")]
        public BelgeBaslik BelgeDokuman
        {
            get { return _BelgeDokuman; }
            set { SetPropertyValue<BelgeBaslik>(nameof(BelgeDokuman), ref _BelgeDokuman, value); }
        }

    }
}