using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using DPDModel.Models;
using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using DPDModelDPDModel.DAL;
using DPDTrack.Models;
using System.Linq;

namespace DPDTrack.ModelView
{
    [POCOViewModel]
    public class CheckPointServerMgrViewModel
    {

        public CheckPointServerMgrViewModel()
        {
            Logs = new ObservableCollection<SystemLog>();
            
            ServerLogDate = DateTime.Now;
            CBXAgents = GetAgentInfo();
            SelectedAgent = CBXAgents.FirstOrDefault();
        }

        public virtual IEnumerable<CBXAgent> CBXAgents { get; set; }
        /// <summary>
        /// 商业代理名称
        /// </summary>
        public virtual CBXAgent SelectedAgent { get; set; }
        /// <summary>
        /// 日志记录的时间
        /// </summary>
        public virtual DateTime ServerLogDate { get; set; }

        /// <summary>
        /// 服务器的状态信息
        /// </summary>
        public virtual string ServerStatus { get; set; }
        /// <summary>
        /// 用于显示加载提示窗体
        /// </summary>
        public ISplashScreenService SplashScreenService
        {
            get
            {
                return this.GetService<ISplashScreenService>();
            }
        }
        /// <summary>
        /// 用于弹出提示框
        /// </summary>
        public IMessageBoxService MessageBoxService
        {
            get
            {
                return this.GetService<IMessageBoxService>();
            }
        }

        /// <summary>
        /// 服务器运行的日志信息
        /// </summary>
        public virtual ObservableCollection<SystemLog> Logs { get; set; }

        /// <summary>
        /// 获取服务器运行的日志信息
        /// </summary>
        public virtual void GetServerLogs()
        {
            try
            {
                SplashScreenService.ShowSplashScreen();
                string sqlStr = $"SELECT [dttm],[result] FROM [db_SFI].[dbo].[tb_SystemLog] WHERE [AGid]=1 AND (CONVERT(varchar(10),dttm,112) = '{ServerLogDate.ToString("yyyyMMdd")}') ORDER BY int_id DESC ";

                IEnumerable<SystemLog> logs = SQLHelper.GetObject<SystemLog>(sqlStr);
                foreach (SystemLog log in logs)
                {
                    Logs.Add(log);
                }
                SplashScreenService.HideSplashScreen();
            }
            catch (Exception ex)
            {
                SplashScreenService.HideSplashScreen();
                MessageBoxService.ShowMessage($"刷新日志信息出错：{ex.Message}", "提示信息", MessageButton.OK, MessageIcon.Error);
            }
        }

        private IEnumerable<CBXAgent> GetAgentInfo()
        {
            //string sql = "select [int_AGid] as AGid,[vchar_AGname] as Agname from tb_Agent where IsOpen=1 and int_AGtype = 0";
            string sql = "select [int_AGid] as AGid,[vchar_AGname] as Agname from tb_Agent where IsOpen=1";
            return SQLHelper.GetObject<CBXAgent>(sql);
        }

    }
}