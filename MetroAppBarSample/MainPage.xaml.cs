using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MetroAppBarSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            XNamespace p_ns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
            XNamespace xaml_ns = "http://schemas.microsoft.com/winfx/2006/xaml";

            var doc = XDocument.Load(@"Common\StandardStyles.xaml");
            foreach (var style in doc.Descendants(p_ns + "Style"))
            {
                var key = style.Attribute(xaml_ns + "Key");
                if (key != null && key.Value != null)
                {
                    var basedOn = style.Attribute("BasedOn");
                    if (basedOn != null && basedOn.Value == @"{StaticResource AppBarButtonStyle}")
                    {
                        var button = new Button();
                        button.Style = App.Current.Resources[key.Value] as Style;
                        ToolTipService.SetToolTip(button, key.Value);
                        button.Click += (sender, args) =>
                        {
                            string styleName = ToolTipService.GetToolTip(sender as Button) as string;
                            DataPackage clipboardData = new DataPackage();
                            clipboardData.SetText(styleName);
                            Clipboard.SetContent(clipboardData);
                        };
                        AppBarButtonListView.Items.Add(button);
                    }
                }
            }
        }
    }
}
