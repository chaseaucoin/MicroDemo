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
    public class InvoiceItem
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [DataMember]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [DataMember]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets the line total
        /// </summary>
        /// <returns></returns>
        [DataMember]
        public decimal LineTotal
        {
            get
            {
                var total = Quantity * Price;
                return total;
            }
            set { }
        }
    }
}
