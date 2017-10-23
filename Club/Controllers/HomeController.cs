using Club.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace Club.Controllers
{
    public class HomeController : Controller
    {

        [AuthFilter]
        public ActionResult Index()
        {

            #region 数据库的增删改查
            //using (var db = new ClubEntities())
            //{
            //    var level = new Level();
            //    level.Name = "级别1";

            //    db.Level.Add(level);
            //    db.SaveChanges();
            //}


            //using (var db = new ClubEntities())
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        var user = new User();
            //        user.LevelId = 1;
            //        user.Name = "张" + i;
            //        user.Account = "张" + i;
            //        user.PassWord = "000000";
            //        db.User.Add(user);
            //        db.SaveChanges();
            //    }

            //}

            //using (var db= new ClubEntities())
            //{
            //    var user = db.User.FirstOrDefault(a => a.Id == 2);
            //    if (user != null)
            //    {
            //        db.User.Remove(user);
            //        db.SaveChanges();
            //    }

            //}

            //using (var db=new ClubEntities())
            //{

            //    var user = db.User.FirstOrDefault(a => a.Id == 3);
            //    if (user != null)
            //    {
            //        user.Name = "软件153的用户";
            //    }
            //    db.SaveChanges();
            //}

            //using (var db=new ClubEntities())
            //{

            //    var users = db.User.Where(t => t.Id < 10).ToList();

            //    var sb=new StringBuilder();
            //    foreach (var item in users)
            //    {
            //        sb.AppendLine("用户名：" + item.Name+"用户级别："+item.Level.Name);
            //    }
            //    return Content(sb.ToString());

            //}



            //密码加密
            //using (var db = new ClubEntities())
            //{
            //    var users = db.User.ToList();
            //    foreach (var user in users)
            //    {
            //        //var pw = EncryptHelper.MD5Encoding(user.PassWord, user.Account);
            //        user.PassWord = user.PassWord.MD5Encoding(user.Account);
            //    }
            //    db.SaveChanges();
            //}
            // var pw = EncryptHelper.MD5Encoding("000000","zhangchi");
            //return Content("ok");
            #endregion

                var loginUser = (User)Session["loginUser"];
                ViewBag.LoginUser = loginUser;
                int pageSize = 2;
            var pageIndex = Request["pageIndex"].ToInt(1);
            var kw = Request["kw"];
            //if (string.IsNullOrEmpty(indexStr))
            //{
            //    indexStr = "1";
            //}
            //var pageIndex = int.Parse(indexStr);
            IPagedList<Club.Models.ListPostModel> pt;
                var cookies = new HttpCookie("User");
                using (var db = new ClubEntities())
                {
                var postList = new List<ListPostModel>();
                var list = db.Post.OrderByDescending(a => a.Id).Include(a => a.User).Include(a => a.Category).Where(a => a.IsFeatured == true).ToList();
                foreach (var item in list)
                {
                    var postModel = new ListPostModel();
                    var category = db.Category.Where(a => a.Id == item.Id);
                    postModel.Id = item.Id;
                    postModel.Title = item.Title;
                    postModel.UserName = item.User.Name;
                    postModel.CreateTime = item.CreateTime;
                    postModel.ViewCount = item.ViewCount;
                    postModel.Reply = category.Count();
                    if (item.Status==1)
                    {
                        postModel.Status = "【精】";
                    }
                    else
                    {
                        postModel.Status = "";
                    }
                    postModel.UserImage = item.User.Image;

                    postList.Add(postModel);
                       
                 }
                pt = postList.OrderBy(a => a.Id).ToPagedList(pageIndex: pageIndex, pageSize: pageSize);
                return View(pt);
            }
                
        }


        public ActionResult details()
        {

            return View();
        }
    }
}