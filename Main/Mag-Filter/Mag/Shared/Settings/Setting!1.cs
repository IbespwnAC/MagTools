namespace Mag.Shared.Settings
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;

    internal class Setting<T>
    {
        public readonly T DefaultValue;
        public readonly string Description;
        private T value;
        public readonly string Xpath;

        public event Action<Setting<T>> Changed;

        public Setting(string xpath, string description = null, T defaultValue = default(T))
        {
            this.Xpath = xpath;
            this.Description = description;
            this.DefaultValue = defaultValue;
            this.LoadValueFromConfig(defaultValue);
        }

        private void LoadValueFromConfig(T defaultValue)
        {
            this.value = SettingsFile.GetSetting<T>(this.Xpath, defaultValue);
        }

        private void StoreValueInConfigFile()
        {
            SettingsFile.PutSetting<T>(this.Xpath, this.value);
        }

        public T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (!object.Equals(this.value, value))
                {
                    this.value = value;
                    this.StoreValueInConfigFile();
                    if (this.Changed != null)
                    {
                        this.Changed((Setting<T>) this);
                    }
                }
            }
        }
    }
}

