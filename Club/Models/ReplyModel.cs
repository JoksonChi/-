using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Club.Models
{
    public class ReplyModel
    {
        
        /// <summary>
        /// 帖子id
        /// </summary>
        public int postid { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 发帖用户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 访问次数
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 是否精华帖
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 回复数量
        /// </summary>
        public int Reply { get; set; }

        /// <summary>
        /// 发帖用户头像
        /// </summary>
        public string UserImage { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string details { get; set; }
        /// <summary>
        /// 系统信息
        /// </summary>
        public string sysinfo { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int userid { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 用户身份
        /// </summary>
        public string userlevel { get; set; }

        /// <summary>
        ///用户头像
        /// </summary>
        public string userimage { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public String contents { get; set; }

        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime responseTime { get; set; }
    }
}