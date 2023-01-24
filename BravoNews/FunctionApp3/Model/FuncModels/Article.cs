using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerNewsApp.Model.FuncModels;

namespace TimerNewsApp.Model
{
 public class Article
    {
        public int Id { get; set; }
        public DateTime DateStamp { get; set; }
        public bool Archived { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string FileName { get; set; }
        public string ContentSummary { get; set; }
        public string Headline { get; set; }
    }
}
