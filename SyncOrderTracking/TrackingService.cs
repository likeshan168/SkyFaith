using SyncOrderTracking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using systimers = System.Timers;
namespace SyncOrderTracking
{
    /// <summary>
    /// 同步订单状态的服务
    /// </summary>
    public partial class TrackingService : ServiceBase
    {
        private systimers.Timer main_Timer;
        private systimers.Timer Timer_open;
        public TrackingService()
        {
            InitializeComponent();
            main_Timer = new systimers.Timer();
            Timer_open = new systimers.Timer();
        }

        private int myTime;
        private string IsOpen;

        protected override void OnStart(string[] args)
        {
            GetConfig();
            myTime = 0;
            IsOpen = TrackingHelper.GetServerStatus();
            if (IsOpen == "O")
            {
                Thread T = new Thread(TrackingHelper.DoSyncTrackStates);
                T.Start();
            }
            main_Timer.Start();
            Timer_open.Start();
        }

        protected override void OnStop()
        {
            TrackingHelper.AllClear();
            main_Timer.Stop();
        }
        /// <summary>
        /// 根据服务器开启的状态去同步订单的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void main_Timer_Elapsed(object sender, systimers.ElapsedEventArgs e)
        {
            if (IsOpen == "O")
            {
                if (myTime == TrackingHelper.mainTime)
                {
                    Thread T = new Thread(TrackingHelper.DoSyncTrackStates);
                    T.Start();
                    //运行代码
                    myTime = 0;
                }
                myTime += 1;
            }
            else
            {
                myTime = 0;
            }
        }
        /// <summary>
        /// 定时去读取服务器是否开启的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_open_Elapsed(object sender, systimers.ElapsedEventArgs e)
        {
            IsOpen = TrackingHelper.GetServerStatus();
        }
        /// <summary>
        /// 服务配置文件，设置服务器
        /// </summary>
        private void GetConfig()
        {
            try
            {
                string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
                object result = TrackingHelper.LoadFromXml(configFile, typeof(Config));

                if (result != null)
                {
                    Config config = (Config)result;
                    TrackingHelper.mainTime = config.TimeOut;
                    TrackingHelper.mainAGid = config.AGid;
                    TrackingHelper.mainAGname = config.AGname;
                    TrackingHelper.mainOverTime = config.OverTime;

                    for (int i = 1; i <= config.SynSign.Length; i++)
                    {
                        //TrackingHelper.mainSynSign += "'" + config.SynSign.Substring(i, 1) + "',";
                        TrackingHelper.mainSynSign += $"{config.SynSign.Substring(i, 1)},";
                    }
                    TrackingHelper.mainSynSign = TrackingHelper.mainSynSign.TrimEnd(',');
                    TrackingHelper.ProcessName = config.ProcessName;
                }
            }
            catch (Exception ex)
            {
                TrackingHelper.WriteError(ex.Message);
            }
        }
    }
}
