using System.Collections.Generic;
using DoAn1.Models;

namespace DoAn1.ViewModels
{
    public class HomeViewModel
    {
        public List<NewRelease> NewRelease { get; set; }
        public List<Comming> Comming { get; set; }
        public List<BestSeller> BestSeller { get; set; }
        public List<Winner> Winner { get; set; }
        public List<WhatsNew> WhatsNew { get; set; }
        public List<Author> Author { get; set; }
        public Info Info { get; set; }
    }
}