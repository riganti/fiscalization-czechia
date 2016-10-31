﻿using System;
using System.Xml.Serialization;

namespace Mews.Eet.Dto.Wsdl
{
    [SerializableAttribute]
    [XmlType(Namespace = "http://fs.mfcr.cz/eet/schema/v3")]
    public class SignatureElementType
    {
        [XmlAttribute(AttributeName = "digest")]
        public SignatureDigestType Digest { get; set; }

        [XmlAttribute(AttributeName = "cipher")]
        public SignatureCipherType Cipher { get; set; }

        [XmlAttribute(AttributeName = "encoding")]
        public SignatureEncodingType Encoding { get; set; }

        [XmlAttribute]
        public string[] Text { get; set; }
    }
}
