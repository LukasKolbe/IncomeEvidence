using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeEvidenceOSVC.Models
{

    public class Document
    {
        public String Name { get; set; }
        public String Path { get; set; }
        public Document()
        {

        }
        public Document(String Name,String Path)
        {
            this.Name = Name;
            this.Path = Path;
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
