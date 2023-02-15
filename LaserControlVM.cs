using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beckhoff.App.Ads.Core;
using Beckhoff.App.Ads.Core.Plc;
using Beckhoff.App.Ads.Nc;
using Beckhoff.App.Core.Interfaces;
using GalaSoft.MvvmLight;

namespace UILaserControl
{
    public class LaserControlVM : ViewModelBase
    {

        private string _laserpower;
        private string _laserstartdelay;
        private string _spotweld;
        private string _cycletime;
        private string _counter;
        
        // Create instance of plc and CNC ADS Client
        BAAdsCncClient _cncClient;
        BAAdsPlcClient _plcClient;
        BAAdsNc _ncData;
        //IBAContainerFacade _container;
        // Create notification event list control
        private List<IBAAdsNotification> _notifications;

        /// <summary>
        /// Constructor; class is used as ViewModel for Control
        /// </summary>
        public LaserControlVM()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iocContainer">IoContainer to resolve infrastructure components</param>
        public LaserControlVM(IBAContainerFacade iocContainer) : this()
        {
            
            var adsServer = iocContainer.Resolve<IBAAdsServer>();// get ADSServer
            _notifications = new List<IBAAdsNotification>(); // create instance of notifications list for ADS events
            _plcClient = adsServer.GetAdsClient<IBAAdsPlcClient>("PLC") as BAAdsPlcClient;   // get PLC Client from ADS Server
            _cncClient = adsServer.GetAdsClient<IBAAdsCncClient>("CNC") as BAAdsCncClient;   // get CNC Client from ADS Server
            // create on change notification instances for every tag being read 
            // Can be changed to cyclic, example here --> var notifylaserstartdelay = new OnCyclicDeviceNotification("Global_HMI.nLaserStartDelay",1000, LaserStartDelayVarHandler);
            var notifylaserstartdelay = new OnChangeDeviceNotification("Global_HMI.sLaserStartDelay", LaserStartDelayVarHandler);
            var notifylaserpower = new OnChangeDeviceNotification("Global_HMI.sLaserPower", LaserPowerVarHandler);
            var notifyspotweld = new OnChangeDeviceNotification("Global_HMI.sSpotWeld", SpotWeldVarHandler);
            var notifycycletime = new OnChangeDeviceNotification("Global_HMI.sCycleTime", CycleTimeVarHandler);
            var notifycounter = new OnChangeDeviceNotification("Global_HMI.sCounter", CounterVarHandler);
            // Add to notifications list each on change notification instance
            _notifications.Add(notifylaserstartdelay);
            _notifications.Add(notifylaserpower);
            _notifications.Add(notifyspotweld);
            _notifications.Add(notifycycletime);
            _notifications.Add(notifycounter);

            // Create notications (should add a remove for a form but for UI i'm not adding at this point)
            // So these should in form load and a a remove on a form unload or visability change
            _plcClient.AddPlcDeviceNotification(notifylaserpower);
            _plcClient.AddPlcDeviceNotification(notifylaserstartdelay);
            _plcClient.AddPlcDeviceNotification(notifyspotweld);
            _plcClient.AddPlcDeviceNotification(notifycycletime);
            _plcClient.AddPlcDeviceNotification(notifycounter);

            if (_cncClient != null)
            {
                NcData = _cncClient.AdsCncData;
            }
        }


        private void LaserPowerVarHandler(object sender, BAAdsNotificationItem notification)
        {
            if (notification.Name.Equals("Global_HMI.sLaserPower"))
            {
                if (notification.PlcObject is string)
                {
                    _laserpower = (string)(notification.PlcObject);
                    var b = (string)(notification.PlcObject);
                    LaserPower = b;
                }
            }
        }

        private void LaserStartDelayVarHandler(object sender, BAAdsNotificationItem notification)
        {
           if (notification.Name.Equals("Global_HMI.sLaserStartDelay"))
            {
                if (notification.PlcObject is string)
                {
                    _laserstartdelay = (string)(notification.PlcObject);
                    var b = (string)(notification.PlcObject);
                    LaserStartDelay = b;
                }
            }
        }

        private void SpotWeldVarHandler(object sender, BAAdsNotificationItem notification)
        {
            if (notification.Name.Equals("Global_HMI.sSpotWeld"))
            {
                if (notification.PlcObject is string)
                {
                    _spotweld = (string)(notification.PlcObject);
                    var b = (string)(notification.PlcObject);
                    SpotWeld = b;
                }
            }
        }

        private void CycleTimeVarHandler(object sender, BAAdsNotificationItem notification)
        {
            if (notification.Name.Equals("Global_HMI.sCycleTime"))
            {
                if (notification.PlcObject is string)
                {
                    _cycletime = (string)(notification.PlcObject);
                    var b = (string)(notification.PlcObject);
                    CycleTime = b;
                }
            }
        }

        private void CounterVarHandler(object sender, BAAdsNotificationItem notification)
        {
            if (notification.Name.Equals("Global_HMI.sCounter"))
            {
                if (notification.PlcObject is string)
                {
                    _counter = (string)(notification.PlcObject);
                    var b = (string)(notification.PlcObject);
                    Counter = b;
                }
            }
        }

        public string LaserPower
        {
            get { return _laserpower; }
            set
            {
                _laserpower = value;
                RaisePropertyChanged(() => LaserPower);
            }
        }

        public string LaserStartDelay
        {
            get { return _laserstartdelay; }
            set
            {
                _laserstartdelay = value;
                RaisePropertyChanged(() => LaserStartDelay);
            }
        }

        public string SpotWeld
        {
            get { return _spotweld; }
            set
            {
                _spotweld = value;
                RaisePropertyChanged(() => SpotWeld);
            }
        }

        public string CycleTime
        {
            get { return _cycletime; }
            set
            {
                _cycletime = value;
                RaisePropertyChanged(() => CycleTime);
            }
        }

        public string Counter
        {
            get { return _counter; }
            set
            {
                _counter = value;
                RaisePropertyChanged(() => Counter);
            }
        }

        public BAAdsNc NcData
        {
            get { return _ncData; }
            set
            {
                _ncData = value;
                RaisePropertyChanged(() => NcData);
            }
        }
        

    }
}
