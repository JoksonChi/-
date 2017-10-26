using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Club.Models
{
    public class PraiseModel
    {

        /// <summary>
        /// 所赞帖子的id
        /// </summary>
        public int postid { get; set; }

        /// <summary>
        /// 赞贴用户的id
        /// </summary>
        public int userid { get; set; }

        /// <summary>
        /// 赞贴用户名
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 赞贴用户头像
        /// </summary>
        public string userimage { get; set; }

        /// <summary>
        /// 赞贴时间
        /// </summary>
        public DateTime time { get; set; }
    }
}