using System;
using System.Globalization;
using System.Net;
using System.Xml;
using Wallet.Core.Models;

namespace Wallet.Services.Helpers
{
    internal class EcbParser
    {
        private readonly XmlDocument _document;
        private readonly string _attributeName = "rate";

        internal EcbParser(XmlDocument document)
        {
            _document = document;
        }

        public decimal FindRate(Currency currency)
        {
            NumberFormatInfo nfi = new NumberFormatInfo() { NumberDecimalSeparator = "." };

            string xpath = string.Format("//*[@currency='{0}']", WebUtility.HtmlEncode(currency.ToString()));
            var node = _document.SelectSingleNode(xpath);
            var rateResult = decimal.Parse(node.Attributes[_attributeName].Value, nfi);

            return Convert.ToDecimal(rateResult);
        }
    }
}
