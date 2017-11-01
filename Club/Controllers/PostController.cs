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
    public class PostController : BaseController
    {
        // GET: Post

        public ActionResult Index()
        {
            return View();
        }

        [WebAuthFilter(IsNeedLogin = true)]
        public ActionResult NewPost()
        {
            using (var db = new ClubEntities())
            {
                var categorys = db.Category.ToList();
                ViewBag.Categorys = categorys;
            }
            return View();
        }
        [WebAuthFilter(IsNeedLogin = true)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Save()
        {
            var categoryId = Request["categoryId"].ToInt();
            var title = Request["title"];
            var content = Request["content"];
            var loginUser = (User)Session["loginUser"];

            if (categoryId == 0)
            {
                ShowMsg("帖子分类不能为空");
                return RedirectToAction("New");
            }

            if (string.IsNullOrEmpty(title))
            {
                ShowMsg("帖子标题不能为空");
                return RedirectToAction("New");
            }
            if (string.IsNullOrEmpty(content))
            {
                ShowMsg("帖子内容不能为空");
                return RedirectToAction("New");
            }

            using (var db = new ClubEntities())
            {
                var post = new Post();
                post.CategoryId = categoryId;
                post.Title = title;
                post.Details = content;
                post.CreateTime = DateTime.Now;
                post.UserId = loginUser.Id;
                post.IsFeatured = true;
                db.Post.Add(post);
                db.SaveChanges();
                ShowMsg("发布成功");
                return Redirect("/");
            }

        }

        public ActionResult Contents()
        {
            var Id = Request["Id"].ToInt();
            using (var db = new ClubEntities())
            {
                var post = db.Post.Include(a => a.User).FirstOrDefault(a => a.Id == Id);
                var user = db.User.OrderByDescending(a => a.Id).Include(a => a.Level).ToList();
                ViewBag.User = user;
                //查询赞帖子的用户
                var praiserecord = db.PraiseRecord.OrderByDescending(a => a.Id).Include(a => a.User).ToList();
                ViewBag.praiserecord = praiserecord;
                var listpraiserecord = new List<PraiseModel>();
                foreach (var item in praiserecord)
                {
                    var praiserecordmodel = new PraiseModel();
                    praiserecordmodel.postid = item.Post.Id;
                    praiserecordmodel.userid = item.User.Id;
                    praiserecordmodel.username = item.User.Name;
                    praiserecordmodel.userimage = item.User.Image;
                    praiserecordmodel.time = item.CreateTime;
                    listpraiserecord.Add(praiserecordmodel);
                }
                ViewData["praiserecord"] = listpraiserecord;
                //查询收藏帖子的用户
                var collection = db.Collection.OrderByDescending(a => a.Id).Include(a => a.User).ToList();
                ViewBag.collection = collection;
                //查询帖子回复的信息
                var reply = db.AllReply.OrderByDescending(a => a.Id).Include(a => a.User).Where(a => a.Id == Id).ToList();
                var listreply = new List<ReplyModel>();
                foreach (var item in reply)
                {
                    var replyModel = new ReplyModel();
                    replyModel.postid = item.Post.Id;
                    replyModel.userid = item.User.Id;
                    replyModel.username = item.User.Name;
                    replyModel.userlevel = item.User.Level.Name;
                    replyModel.userimage = item.User.Image;
                    replyModel.contents = item.Contents;
                    replyModel.responseTime = item.ResponseTime;
                    listreply.Add(replyModel);
                }
                ViewData["reply"] = listreply;
                return View(post);
            }
        }
    }
}