using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SentimentAnalysis.Models
{
    public class Search
    {
        [Key]
        public int id { get; set; }

        [DisplayName("Keyword")]
        [Required]
        public string keyword { get; set; }

        public DateTime searchDate { get; set; }

        public virtual ICollection<SearchResult> searchResults { get; set; }
    }
}