using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TimerNewsApp.Model.FuncModels;

namespace TimerNewsApp.Model
{
    public class Like : IdentityUser
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime Date { get; set; }
        public bool Liked { get; set; }
    }
}
