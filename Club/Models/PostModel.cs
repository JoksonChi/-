using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Club.Models
{
    public class ListPostModel
    {
        public int Id { get; set; }
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
        public bool Status { get; set; }

        /// <summary>
        /// 是否精华帖
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// 发帖用户头像
        /// </summary>
        public string UserImage { get; set; }
    }
   
}