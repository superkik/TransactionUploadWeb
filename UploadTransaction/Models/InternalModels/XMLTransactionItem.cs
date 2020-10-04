using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UploadTransaction.Models.InternalModels
{
	[XmlRoot(ElementName= "PaymentDetails")]
	public class XMLPaymentDetails
	{
		[XmlElement(ElementName = "Amount")]
		public string Amount { get; set; }

		[XmlElement(ElementName = "CurrencyCode")]
		public string CurrencyCode { get; set; }
	}

	[XmlRoot(ElementName = "Transaction")]
	public class XMLTransaction
	{
		[XmlElement(ElementName = "TransactionDate")]
		public string TransactionDate { get; set; }

		[XmlElement(ElementName = "PaymentDetails")]
		public XMLPaymentDetails PaymentDetails { get; set; }

		[XmlElement(ElementName = "Status")]
		public string Status { get; set; }

		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
	}

	[XmlRoot(ElementName = "Transactions")]
	public class XMLTransactions
	{
		[XmlElement(ElementName = "Transaction")]
		public List<XMLTransaction> Transaction { get; set; }
	}
}
