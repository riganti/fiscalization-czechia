﻿using System;
using System.Xml.Serialization;

namespace Mews.Eet.Dto.Wsdl
{
    [Serializable]
    [XmlType(Namespace = "http://fs.mfcr.cz/eet/schema/v3")]
    public class ResponseSuccess
    {
        [XmlAttribute(AttributeName = "fik")]
        public string FiscalCode { get; set; }

        [XmlAttribute(AttributeName = "test")]
        public bool Test { get; set; }

        [XmlIgnore]
        public bool TestSpecified { get; set; }
    }
}
