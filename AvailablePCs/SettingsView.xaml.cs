using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace AvailablePCs
{
    public sealed partial class SettingsView : UserControl
    {
        internal static Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        
        //private static Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        private static SettingsView _sv = null;

        public SettingsView()
        {
            this.InitializeComponent();
            _sv = this;
            
            SettingsFlyout.Background = new SolidColorBrush(Color.FromArgb(255, 102, 0, 0));
            CacheToggleSwitch.Background = new SolidColorBrush(Color.FromArgb(255, 102, 0, 0));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleCache(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    localSettings.Values["Cache"] = true;
                }
                else
                {
                    localSettings.Values["Cache"] = false;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static void SetCacheSetting(bool value)
        {
            _sv.CacheToggleSwitch.IsOn = value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool GetCacheSetting()
        {
            return Convert.ToBoolean(localSettings.Values["Cache"]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static void SetHaveCacheSetting(bool value)
        {
            localSettings.Values["IsCache"] = value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool HaveCacheSetting()
        {
            return Convert.ToBoolean(localSettings.Values["IsCache"]);
        }
    }
}
