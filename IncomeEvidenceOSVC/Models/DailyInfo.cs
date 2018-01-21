using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeEvidenceOSVC.Models
{
    public class DailyInfo
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Int32 Id { get; set; }
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the income.
        /// </summary>
        /// <value>
        /// The income.
        /// </value>
        public Decimal Income { get; set; }
        /// <summary>
        /// Gets or sets the costs.
        /// </summary>
        /// <value>
        /// The costs.
        /// </value>
        public Decimal Costs { get; set; }
        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>
        /// The note.
        /// </value>
        public String Note { get; set; }
        /// <summary>
        /// Gets the document count.
        /// </summary>
        /// <value>
        /// The document count.
        /// </value>
        public Int32 DocumentCount
        {
            get
            {
                if(Documents != null)
                {
                    return Documents.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets or sets the documents.
        /// </summary>
        /// <value>
        /// The documents.
        /// </value>
        public List<Document> Documents { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DailyInfo"/> class.
        /// </summary>
        public DailyInfo()
        {
            Date = DateTime.Today;
            Income = 6000;
            Costs = 1000;
            Note = "Shopping";
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.
        /// </returns>
        public override Boolean Equals(Object obj) => base.Equals(obj);
        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        public override Int32 GetHashCode() => base.GetHashCode();
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override String ToString() => base.ToString();
    }
}
