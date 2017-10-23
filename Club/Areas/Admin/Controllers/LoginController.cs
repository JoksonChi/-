﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Club.Areas.Admin.Controllers
{

    [AuthFilter(IsNeedLogin = false)]
    public class LoginController : BaseController
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            //重置密码
            //using (var db= new ClubEntities())
            //{
            //    var users = db.User.ToList();
            //    foreach (var item in users)
            //    {
            //        item.PassWord = "000000".MD5Encoding(item.Account);
            //    }
            //    db.SaveChanges();

            //}
            return View();
        }

        [HttpPost]
        public ActionResult Login()
        {
            var account = Request["account"];
            var password = Request["password"];

            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                ShowMsg("账号密码不能为空");
                return RedirectToAction("Index");
            }

            using (var db = new ClubEntities())
            {

                var pw = password.MD5Encoding(account);

                var user = db.User.FirstOrDefault(a => a.Account == account && a.PassWord == pw);
                if (user == null)
                {
                    ShowMsg("用户不存在");
                    return RedirectToAction("Index");
                }
                //设置用户登陆状态
                Session["AdminUser"] = user;

                return Redirect("/admin/home");
            }

        }
    }
}