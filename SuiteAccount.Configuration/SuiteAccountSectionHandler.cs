using System.Configuration;

namespace SuiteAccount.Configuration
{
    public class SuiteAccountSectionHandler : ConfigurationSection
    {
        [ConfigurationProperty("eventStoreUri", IsRequired = true)]
        public string EventStoreUri
        {
            get { return (string) this["eventStoreUri"]; }
            set { this["eventStoreUri"] = value; }
        }

        [ConfigurationProperty("eventStorePort", IsRequired = true)]
        public int EventStorePort
        {
            get { return (int)this["eventStorePort"]; }
            set { this["eventStorePort"] = value; }
        }

        [ConfigurationProperty("eventStoreUser", IsRequired = true)]
        public string EventStoreUser
        {
            get { return (string)this["eventStoreUser"]; }
            set { this["eventStoreUser"] = value; }
        }

        [ConfigurationProperty("eventStorePassword", IsRequired = true)]
        public string EventStorePassword
        {
            get { return (string)this["eventStorePassword"]; }
            set { this["eventStorePassword"] = value; }
        }
    }
}
