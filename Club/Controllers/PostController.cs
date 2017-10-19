using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                db.Post.Add(post);
                db.SaveChanges();
                ShowMsg("发布成功");
                return Redirect("/");
            }

        }
    }
}