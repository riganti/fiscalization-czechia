using System;
using System.Threading.Tasks;
using Mews.Eet.Dto;

namespace Mews.Eet.Communication
{
    public class SoapClient
    {
        public SoapClient(Uri endpointUri, Certificate certificate, TimeSpan httpTimeout, SignAlgorithm signAlgorithm = SignAlgorithm.Sha256, EetLogger logger = null)
        {
            HttpClient = new SoapHttpClient(endpointUri, httpTimeout, logger);
            Certificate = certificate;
            SignAlgorithm = signAlgorithm;
            XmlManipulator = new XmlManipulator();
            Logger = logger;
        }

        private SoapHttpClient HttpClient { get; }

        private Certificate Certificate { get; }

        private SignAlgorithm SignAlgorithm { get; }

        private XmlManipulator XmlManipulator { get; }

        private EetLogger Logger { get; }

        public async Task<SoapResult<TIn, TBody>> SendAsync<TIn, TBody>(SoapInput<TIn> input)
            where TIn : class, new()
            where TBody : class, new()
        {
            var messageInputXmlElement = XmlManipulator.Serialize(input.Message);
            var mesasgeInputXmlString = messageInputXmlElement.OuterXml;
            Logger?.Debug("Created XML document from DTOs.", new { XmlString = mesasgeInputXmlString });

            var soapMessage = new SoapMessage(new SoapMessagePart(messageInputXmlElement));
            var soapXmlDocument = Certificate == null ? soapMessage.GetXmlDocument() : soapMessage.GetSignedXmlDocument(Certificate, SignAlgorithm);

            var xml = soapXmlDocument.OuterXml;
            Logger?.Debug("Created signed XML.", new { SoapString = xml });

            var response = await HttpClient.SendAsync(new PostRequest(xml, input.Operation)).ConfigureAwait(continueOnCapturedContext: false);

            Logger?.Debug("Received RAW response from EET servers.", response);

            return new SoapResult<TIn, TBody>(response, input, messageInputXmlElement, soapXmlDocument);
        }
    }
}
