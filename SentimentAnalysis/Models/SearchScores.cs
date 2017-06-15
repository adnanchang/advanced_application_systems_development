using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SentimentAnalysis.Models
{
    public class SearchScores
    {
        [Column(Order = 1)]
        [Key]
        [ForeignKey("SearchResult")]
        public int searchResultId { get; set; }

        [Column(Order = 2)]
        public double score { get; set; }

        public virtual SearchResult SearchResult { get; set; }
    }
}