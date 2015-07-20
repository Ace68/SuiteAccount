using System.Configuration;

namespace SuiteAccount.Configuration
{
    public class EventStoreSectionHandler : ConfigurationSection
    {
        [ConfigurationProperty("uri", DefaultValue = "127.0.0.1", IsRequired = true)]
        public string Uri
        {
            get { return (string)this["uri"]; }
            set { this["uri"] = value; }
        }

        [ConfigurationProperty("port", DefaultValue = 1113, IsRequired = true)]
        public int Port
        {
            get { return (int)this["port"]; }
            set { this["port"] = value; }
        }

        [ConfigurationProperty("user", DefaultValue = "admin", IsRequired = true)]
        public string User
        {
            get { return (string)this["user"]; }
            set { this["user"] = value; }
        }

        [ConfigurationProperty("password", DefaultValue = "changeit", IsRequired = true)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }
    }

    public class MongoDbSectionHandler : ConfigurationSection
    {
        [ConfigurationProperty("uri", IsRequired = true)]
        public string Uri
        {
            get { return (string)this["uri"]; }
            set { this["uri"] = value; }
        }

        [ConfigurationProperty("port", DefaultValue = 27017, IsRequired = true)]
        public int Port
        {
            get { return (int)this["port"]; }
            set { this["port"] = value; }
        }
    }

    public class SuiteTokenSectionHandler : ConfigurationSection
    {
        [ConfigurationProperty("duration", DefaultValue = 240, IsRequired = true)]
        public int Duration
        {
            get { return (int) this["duration"]; }
            set { this["duration"] = value; }
        }
    }
}
