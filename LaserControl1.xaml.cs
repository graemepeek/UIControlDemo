using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Beckhoff.App.Ads.Core;
using Beckhoff.App.Ads.Core.Plc;
using Beckhoff.App.Core.Interfaces;

namespace UILaserControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class IPCamControl1 : UserControl
    {
        BAAdsPlcClient _plcClient;
                

        public LaserControl1(IBAContainerFacade iocContainer)
        {
            InitializeComponent();
            var adsServer = iocContainer.Resolve<IBAAdsServer>();// get ADSServer
            _plcClient = adsServer.GetAdsClient<IBAAdsPlcClient>("PLC") as BAAdsPlcClient;   // get PLC Client from ADS Server
          
        }

    }
}
