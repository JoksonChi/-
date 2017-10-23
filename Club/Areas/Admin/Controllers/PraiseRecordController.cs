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
    public class PraiseRecordController : BaseController
    {
        // GET: Admin/PraiseRecord
        public ActionResult Index()
        {
            int pageSize = 10;
            var pageIndex = Request["pageIndex"].ToInt(1);
            var kw = Request["kw"];
            //if (string.IsNullOrEmpty(indexStr))
            //{
            //    indexStr = "1";
            //}
            //var pageIndex = int.Parse(indexStr);
            IPagedList<PraiseRecord> items;

            using (var db = new ClubEntities())
            {
                var list = db.PraiseRecord.Where(a => a.IsAbort == false).Include(a => a.User).Include(a => a.Post);

                if (!string.IsNullOrEmpty(kw))
                {
                    list = list.Where(a => a.User.Name.Contains(kw) || a.Post.Title.Contains(kw));
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
                var user = db.PraiseRecord.FirstOrDefault(a => a.Id == id);
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
        //[HttpGet]
        //public ActionResult Edit()
        //{
        //    var Id = Request["Id"].ToInt();

        //    using (var db = new ClubEntities())
        //    {
        //        var collection = db.Collection.Include(a => a.User).Include(a=>a.Post).FirstOrDefault(a => a.Id == Id);


        //        //var selectItems = new List<SelectListItem>();

        //        var levels = db.Level.ToList();

        //        //foreach (var level in levels)
        //        //{
        //        //    var selectItem = new SelectListItem();
        //        //    selectItem.Text = level.Name;
        //        //    selectItem.Value = level.Id.ToString();
        //        //    if (user != null && (user.LevelId == level.Id))
        //        //    {
        //        //        selectItem.Selected = true;
        //        //    }
        //        //    selectItems.Add(selectItem);
        //        //}

        //        //ViewBag.SeletItems = selectItems;

        //        if (collection == null)
        //            collection = new Collection();
        //        return View(collection);
        //    }

        //}
        //[HttpPost]
        //public ActionResult Save()
        //{
        //    var id = Request["id"].ToInt();
        //    var userId = Request["userId"].ToInt();
        //    var postId = Request["postId"].ToInt();
        //    var createTime = Request["createTime"];


        //    using (var db = new ClubEntities())
        //    {
        //        var collection = db.Collection.FirstOrDefault(a => a.Id == id);

        //        //if (user == null)
        //        //{
        //        //    user = new User();
        //        //    user.Account = account;
        //        //    user.IsAdmin = false;
        //        //    user.PassWord = "000000";
        //        //    db.User.Add(user);
        //        //}

        //        collection.UserId = userId;
        //        collection.PostId = postId;
        //        collection.CreateTime = Convert.ToDateTime(createTime);
        //        db.SaveChanges();
        //        ShowMsg("操作成功");
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}