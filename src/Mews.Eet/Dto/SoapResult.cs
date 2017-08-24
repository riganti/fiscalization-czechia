using System.Security;
using System.Xml;
using Mews.Eet.Communication;

namespace Mews.Eet.Dto
{
    public sealed class SoapResult<TIn, TBody>
        where TIn : class, new()
        where TBody : class, new()
    {
        public SoapResult(PostResponse postResponse, SoapInput<TIn> input, XmlElement messageBodyXml, XmlDocument soapXmlDocument)
        {
            PostResponse = postResponse;
            SoapInput = input;
            MessageBodyXml = messageBodyXml;
            SoapXmlDocument = soapXmlDocument;
            XmlManipulator = new XmlManipulator();
            ResponseData = XmlManipulator.Deserialize<TBody>(GetSoapBody(postResponse.Body));
        }

        public PostResponse PostResponse { get; }

        public TBody ResponseData { get; }

        public SoapInput<TIn> SoapInput { get; }

        public XmlElement MessageBodyXml { get; }

        public XmlDocument SoapXmlDocument { get; }

        private XmlManipulator XmlManipulator { get; }

        private XmlElement GetSoapBody(string soapXmlString)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(soapXmlString);

            var soapMessage = SoapMessage.FromSoapXml(xmlDocument);
            if (!soapMessage.VerifySignature())
            {
                throw new SecurityException("The SOAP message signature is not valid.");
            }
            return soapMessage.Body.XmlElement.FirstChild as XmlElement;
        }
    }
}
