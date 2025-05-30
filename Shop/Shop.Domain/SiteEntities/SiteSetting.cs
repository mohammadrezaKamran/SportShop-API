using Common.Domain;
using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SiteEntities
{
    public class SiteSetting : BaseEntity
    {
        private SiteSetting() { }
        public SiteSetting(string key, string value, SiteSettingGroup? group = null, string? description = null)
        {

            NullOrEmptyDomainDataException.CheckString(key, nameof(key));
            NullOrEmptyDomainDataException.CheckString(value, nameof(value));

            Key = key;
            Value = value;
            Description = description;
            Group = group;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }
        public string? Description { get; private set; }
        public SiteSettingGroup? Group { get; private set; }


        public void UpdateValue(string newValue)
        {
            if (newValue == null)
                throw new NullOrEmptyDomainDataException(nameof(newValue));

            Value = newValue;
        }

        public void UpdateDescription(string? newDescription)
        {
            Description = newDescription;
        }

        public void UpdateGroup(SiteSettingGroup? newGroup)
        {
            Group = newGroup;
        }
    }


}
public enum SiteSettingGroup
{
    General = 0,
    Contact = 1,
    SEO = 2,
    SocialMedia = 3,
    Payment = 4,
    Appearance = 5,
    Support = 6,
    Notification = 7,
    Other = 99
}