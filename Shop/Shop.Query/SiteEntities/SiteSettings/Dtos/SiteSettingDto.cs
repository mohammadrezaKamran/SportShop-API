using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.SiteEntities.SiteSettings.Dtos
{
    public class SiteSettingDto
    {

        public long Id { get; set; }
        public string Key { get; set; } 
        public string Value { get; set; } 
        public string? Description { get; set; }
        public SiteSettingGroup? Group { get; set; }
        public DateTime CreationDate {  get; set; } 

    }
}
