using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Entitys.Models
{
    [Keyless, ComplexType]
    public class MultiLanguageField
    {
        /// <summary>
        /// O`zbek (Lotin) tilida
        /// </summary>
        public string uz { get; set; }

        /// <summary>
        /// Rus tilida
        /// </summary>
        public string ru { get; set; }

        /// <summary>
        /// Inglis tilida
        /// </summary>
        [NotMapped]
        public string en { get; set; } = "null";

        public static implicit operator MultiLanguageField(string data) => new MultiLanguageField()
        {
            ru = data,
            uz = data,
            en = data
        };

        public override string? ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}