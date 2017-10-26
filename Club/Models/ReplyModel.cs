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
        public String Contents { get; set; }

        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime ResponseTime { get; set; }
    }
}