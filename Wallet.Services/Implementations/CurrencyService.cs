using System;
using System.IO;
using System.Net;
using System.Xml;
using Wallet.Core.Models;
using Wallet.Services.Contracts;
using Wallet.Services.Helpers;

namespace Wallet.Services.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        public string Url { get; private set; }

        public CurrencyService()
        {
            Url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
        }

        public decimal GetRate(Currency currencySource, Currency currencyRecipient)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(GetResponseFromCurrencyApi());

            EcbParser ecbParser = new EcbParser(document);
            decimal sourceRate;
            decimal recipientRate;

            if (currencySource == Currency.EUR)
            {
                sourceRate = 1.0M;
            }
            else
            {
                sourceRate = ecbParser.FindRate(currencySource);
            }

            if (currencyRecipient == Currency.EUR)
            {
                recipientRate = 1.0M;
            }
            else
            {
                recipientRate = ecbParser.FindRate(currencyRecipient);
            }

            return recipientRate / sourceRate;
        }

        private string GetResponseFromCurrencyApi()
        {
            WebRequest request = WebRequest.Create(Url);
            WebResponse response = request.GetResponse();


            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
