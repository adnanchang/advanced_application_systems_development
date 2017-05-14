using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SentimentAnalysis.Models
{
    public class SearchResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int id { get; set; }

        [DisplayName("Comment")]
        [Column(Order = 2)]
        public string comment { get; set; }

        [ForeignKey("Search")]
        [Column(Order = 3)]
        public int searchId { get; set; }

        public virtual Search Search { get; set; }
    }
}