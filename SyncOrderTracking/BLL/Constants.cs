using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncOrderTracking.BLL
{
    public class Constants
    {
        /// <summary>
        /// 添加系统日志的存储过程
        /// 需要参数：
        /// 1. @mainAGid INT
        /// 2. @result NVARCHAR(200)
        /// </summary>
        public readonly static string sp_AddSystemLog = "dbo.sp_AddSystemLog";
        /// <summary>
        /// 更新运单的处理状态
        /// 需要参数：
        /// 1.@intProcessCode INT ,
        /// 2.@mainSynSign VARCHAR(100) ,
        /// 3.@mainAGid INT,--代理编号
        /// 4.@intSpacing INT ,--同步时间间隔
        /// 5.@mainOverTime INT --过期天数默认45天，就是45天之内的我们就去同步运单状态，45天之外的，我们不需要去同步运单的状态
        /// </summary>
        public readonly static string sp_Update_SFI_TrackNum = "dbo.sp_Update_SFI_TrackNum";
        /// <summary>
        /// 获取需要同步的运单信息
        /// 需要参数：
        /// 1. @intProcessCode --处理状态码
        /// 2. @mainSynSign --参数格式为：'1,2'
        /// </summary>
        public readonly static string sp_Get_SFI_TrackNum = "dbo.sp_Get_SFI_TrackNum";
        /// <summary>
        /// 记录同步运单状态的服务日志
        /// 需要参数：
        /// 1. @type CHAR(1) = NULL ,
        /// 2. @MappingID INT = NULL,--运单表记录的id
        /// 3. @result VARCHAR(50) = NULL ,--同步运单成功和失败的记录结果
        /// 4. @error NVARCHAR(1000) = NULL ,--错误信息
        /// 5. @message VARCHAR(200) = NULL OUTPUT --返回数据库执行的结果信息
        /// </summary>
        public readonly static string sp_Logupdate = "dbo.sp_Logupdate";
        /// <summary>
        /// 通过ID更新SFI_TrackNum表
        /// 需要参数：
        /// 1. @int_Mappingid INT
        /// </summary>
        public readonly static string sp_Update_SFI_TrackNumByID = "dbo.sp_Update_SFI_TrackNumByID";
        /// <summary>
        /// 更新运单状态表
        /// 需要参数：
        /// 1. @MappingId INT = NULL ,
        /// 2. @AGid INT = NULL,
        /// 3. @SFInumber VARCHAR(32) = NULL ,
        /// 4. @AGnumber VARCHAR(32) = NULL ,
        /// 5. @dttm DATETIME = NULL,
        /// 6. @Description NVARCHAR(MAX) = NULL ,
        /// 7. @DescriptionLocal NVARCHAR(MAX) = NULL ,
        /// 8. @city NVARCHAR(100) = NULL ,
        /// 9. @code VARCHAR(32) = NULL ,
        /// 10.@message VARCHAR(200) = NULL OUTPUT
        /// </summary>
        public readonly static string sp_RecordUpdate = "dbo.sp_RecordUpdate";
    }
}
