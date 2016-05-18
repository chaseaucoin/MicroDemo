using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Contracts.Invoices
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Invoice
    {
        public Invoice()
        {
            Items = new List<InvoiceItem>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        [DataMember]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [DataMember]
        public IEnumerable<InvoiceItem> Items { get; set; }

        /// <summary>
        /// Gets the invoice total.
        /// </summary>
        /// <returns></returns>
        [DataMember]
        public decimal InvoiceTotal
        {
            get
            {
                var total = Items.Sum(item => item.LineTotal);
                return total;
            }

            set { }
        }
    }
}
