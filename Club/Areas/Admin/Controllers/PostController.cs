using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Runtime.Remoting.Channels;
using System.Web.WebPages;
using Webdiyer.WebControls.Mvc;
using Club;

namespace Club.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        /// <summary>
        /// 帖子列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            int pageSize = 5;
            var pageIndex = Request["pageIndex"].ToInt(1);
            var kw = Request["kw"];
            //if (string.IsNullOrEmpty(indexStr))
            //{
            //    indexStr = "1";
            //}

            //var pageIndex = int.Parse(indexStr);
            IPagedList<Post> items;

            using (var db = new ClubEntities())
            {
                var list = db.Post.Where(a => a.IsAbort == false).Include(a => a.User).Include(a => a.Category);

                if (!string.IsNullOrEmpty(kw))
                {
                    list = list.Where(a => a.Title.Contains(kw) || a.User.Name.Contains(kw));
                    ViewBag.KW = kw;
                }
                items = list.OrderByDescending(a => a.Id).ToPagedList(pageIndex: pageIndex, pageSize: pageSize);
            }
            return View(items);
        }


        public ActionResult Delete()
        {
            var idStr = Request["Id"];
            var id = idStr.ToInt();
            if (id == 0)
            {
                TempData["Msg"] = "数据不正确！";

                return RedirectToAction("Index");
            }

            using (var db = new ClubEntities())
            {
                var user = db.Post.FirstOrDefault(a => a.Id == id);
                if (user != null)
                {
                    user.IsAbort = true;
                    //db.User.Remove(user);
                    db.SaveChanges();
                }
            }

            TempData["Msg"] = "删除成功！";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var Id = Request["Id"].ToInt();

            using (var db = new ClubEntities())
            {
                var post = db.Post.Include(a => a.User).Include(a => a.Category).FirstOrDefault(a => a.Id == Id);


                var selectItems = new List<SelectListItem>();
                var selectlistis = new List<SelectListItem>();
                var categorys = db.Category.ToList();
                //帖子类型
                foreach (var category in categorys)
                {
                    var selectItem = new SelectListItem();
                    selectItem.Text = category.Name;
                    selectItem.Value = category.Id.ToString();
                    if (post != null && (post.CategoryId == category.Id))
                    {
                        selectItem.Selected = true;
                    }
                    selectItems.Add(selectItem);
                }

                ViewBag.SeletItems = selectItems;
                //是否审核下拉
                string[,] isfeatured = { { "0", "未审核" }, { "1", "已审核" } };
                for (int i = 0; i < 2; i++)
                {
                    var selectitem = new SelectListItem();
                    selectitem.Text = isfeatured[i, 1];
                    selectitem.Value = isfeatured[i, 0];
                    if (post.IsFeatured == i.ToBit())
                    {
                        selectitem.Selected = true;
                    }
                    selectlistis.Add(selectitem);
                }
                ViewBag.isfeatured = selectlistis;
               
                return View(post);
            }

        }
        [HttpPost]
        public ActionResult Save()
        {
            var id = Request["id"].ToInt();
            int isfeaturedid = Request["isfeaturedid"].ToInt();
            int categoryId = Request["categoryId"].ToInt();
            using (var club = new ClubEntities())
            {
                var post = club.Post.FirstOrDefault(a => a.Id == id);
                post.CategoryId = categoryId;
                post.IsFeatured = isfeaturedid.ToBit();
                club.SaveChanges();
            }
            ShowMsg("操作成功");
            return RedirectToAction("Index");
           
        }
    }
}
