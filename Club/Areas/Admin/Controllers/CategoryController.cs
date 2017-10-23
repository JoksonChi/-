using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Webdiyer.WebControls.Mvc;
using Club;

namespace Club.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            int pageSize = 10;
            var pageIndex = Request["pageIndex"].ToInt(1);
            //if (string.IsNullOrEmpty(indexStr))
            //{
            //    indexStr = "1";
            //}

            //var pageIndex = int.Parse(indexStr);


            var list = new List<Category>();
            using (var db = new ClubEntities())
            {
                list = db.Category.Where(a => a.IsAbort == false).OrderByDescending(a => a.Id).ToPagedList(pageIndex: pageIndex, pageSize: pageSize);
            }

            return View(list);
        }

        /// <summary>
        /// 分类管理
        /// </summary>
        /// <returns></returns>


        public ActionResult Delete()
        {
            var idStr = Request["Id"];
            var id = idStr.ToInt();
            if (id == 0)
            {
                ShowMsg("数据不正确!");

                return RedirectToAction("Index");
            }

            using (var db = new ClubEntities())
            {
                var category = db.Category.FirstOrDefault(a => a.Id == id);
                if (category != null)
                {
                    category.IsAbort = true;
                    //db.User.Remove(user);
                    db.SaveChanges();
                }
            }

            ShowMsg("删除成功!");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var Id = Request["Id"].ToInt();

            using (var db = new ClubEntities())
            {
                var category = db.Category.FirstOrDefault(a => a.Id == Id);
                var levels = db.Level.ToList();
         

                if (category == null)
                    category = new Category();
                return View(category);
            }

        }
        [HttpPost]
        public ActionResult Save()
        {
            var id = Request["id"].ToInt();
            var name = Request["name"];
            if (string.IsNullOrEmpty(name))
            {
                ShowMsg("分类名称不能为空！");
                return RedirectToAction("Edit");
            }

            using (var db = new ClubEntities())
            {
                var category = db.Category.FirstOrDefault(a => a.Id == id);

                if (category == null)
                {
                    category = new Category();
                    category.IsAbort = false;
                    db.Category.Add(category);
                }

                category.Name = name;
                db.SaveChanges();

                ShowMsg("操作成功");
            }
            return RedirectToAction("Index");
        }
    }
}