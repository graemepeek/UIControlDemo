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
    public partial class LaserControl1 : UserControl
    {
        BAAdsPlcClient _plcClient;
        private bool _laserenable;
        private bool _spotweld;


        public LaserControl1(IBAContainerFacade iocContainer)
        {
            InitializeComponent();
            var adsServer = iocContainer.Resolve<IBAAdsServer>();// get ADSServer
            _plcClient = adsServer.GetAdsClient<IBAAdsPlcClient>("PLC") as BAAdsPlcClient;   // get PLC Client from ADS Server
            // Get inital states of laser enable and spot weld enable
            _laserenable = _plcClient.ReadSymbol<bool>("Global_HMI.bLaserEnable");
            if (_laserenable)
            {
                btnLaserEnable.Background = Brushes.Red;
            }
            else
            {
                btnLaserEnable.Background = Brushes.Green;
            }
            _spotweld = _plcClient.ReadSymbol<bool>("Global_HMI.bSpotWeldEnable");
            if (_spotweld)
            {
                btnSpotWeld.Background = Brushes.Red;
            }
            else
            {
                btnSpotWeld.Background = Brushes.Green;
            }
        }

        private void btnLaserEnable_Click(object sender, RoutedEventArgs e)
        {
            _laserenable = !_laserenable; // Invert laser enable state
            _plcClient.WriteSymbol("Global_HMI.bLaserEnable", _laserenable);
            if (_laserenable)
            {
                btnLaserEnable.Background = Brushes.Red;
            }
            else
            {
                btnLaserEnable.Background = Brushes.Green;
            }

        }

        private void btnSpotWeld_Click(object sender, RoutedEventArgs e)
        {
            _spotweld = !_spotweld; // Invert spotweld enable state
            _plcClient.WriteSymbol("Global_HMI.bSpotWeldEnable", _spotweld);
            if (_spotweld)
            {
                btnSpotWeld.Background = Brushes.Red;
            }
            else
            {
                btnSpotWeld.Background = Brushes.Green;
            }
        }
    }
}

