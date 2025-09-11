using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Winning.Outp.BLL.PageMenu
{
    public partial class FormKuangfuTreat : Form
    {
        public FormKuangfuTreat()
        {
            InitializeComponent();
        }

        public void  SetUrl(string url)
        {
            this.webBrowser1.Navigate(url);
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {

        }

        //private void Navigate(String address)
        //{
        //    if (String.IsNullOrEmpty(address)) return;
        //    if (address.Equals("about:blank")) return;
        //    if (!address.StartsWith("http://") &&
        //    !address.StartsWith("https://"))
        //    {
        //        address = "http://" + address;
        //    }
        //    try
        //    {
        //        webBrowser1.Navigate(new Uri(address));
        //    }
        //    catch (System.UriFormatException)
        //    {
        //        return;
        //    }
        //}
    }
}
