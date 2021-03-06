﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.UI.Properties.Editors;
using WhiskeyEditor.UI.Documents;
using WhiskeyEditor.UI.Documents.Info;


namespace WhiskeyEditor.UI.Documents.ContentFactories
{
    class LevelContentFactory : IDocumentContentFactory
    {

        public DocumentView DocumentView { get; private set; }
        public LevelDocumentInfo Info { get; private set; }


        public LevelContentFactory(DocumentView docView, LevelDocumentInfo info)
        {
            DocumentView = docView;
            Info = info;
        }

        public PropertyEditor generatePropertyContent()
        {
            return new LevelDescriptorPropertyEditor(Info.LevelDescriptor);
        }

        public DocumentTab generateDocumentTab()
        {

            LevelDocument doc = (LevelDocument)DocumentView.getTab(Info.LevelDescriptor.Name);
            if (doc == null)
            {
                doc = new LevelDocument(Info.LevelDescriptor, DocumentView);
            }
            return doc;

            
        }
    }
}
