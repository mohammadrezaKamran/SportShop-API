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
public class SeoDataDto
{
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeyWords { get; set; }
    public bool? IndexPage { get; set; }
    public string? Canonical { get; set; }
    public string? Schema { get; set; }
}