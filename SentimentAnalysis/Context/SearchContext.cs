using SentimentAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SentimentAnalysis.Context
{
    public class SearchContext : DbContext
    {
        public DbSet<Search> Searches { get; set; }
    }
}