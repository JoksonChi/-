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
    public class LevelController : BaseController
    {
        // GET: Admin/Level
        public ActionResult Index()
        {
            int pageSize = 10;
            var pageIndex = Request["pageIndex"].ToInt(1);
            //if (string.IsNullOrEmpty(indexStr))
            //{
            //    indexStr = "1";
            //}

            //var pageIndex = int.Parse(indexStr);


            var list = new List<Level>();
            using (var db = new ClubEntities())
            {
                list = db.Level.Where(a => a.IsAbort == false).OrderByDescending(a => a.Id).ToPagedList(pageIndex: pageIndex, pageSize: pageSize);
            }
            return View(list);
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
                var level = db.Level.FirstOrDefault(a => a.Id == id);
                if (level != null)
                {
                    level.IsAbort = true;
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
                var level = db.Level.FirstOrDefault(a => a.Id == Id);
                //var levels = db.Level.ToList();
                //foreach (var level in levels)
                //{
                //    var selectItem = new SelectListItem();
                //    selectItem.Text = level.Name;
                //    selectItem.Value = level.Id.ToString();
                //    if (user != null && (user.LevelId == level.Id))
                //    {
                //        selectItem.Selected = true;
                //    }
                //    selectItems.Add(selectItem);
                //}

                //ViewBag.SeletItems = selectItems;

                if (level == null)
                    level = new Level();
                return View(level);
            }

        }
        [HttpPost]
        public ActionResult Save()
        {
            var id = Request["id"].ToInt();
            var name = Request["name"];

            using (var db = new ClubEntities())
            {
                var level = db.Level.FirstOrDefault(a => a.Id == id);

                if (level == null)
                {
                    level = new Level();
                    level.IsAbort = false;
                    db.Level.Add(level);
                }

                level.Name = name;
                db.SaveChanges();

                ShowMsg("操作成功");
            }
            return RedirectToAction("Index");
        }
    }
}