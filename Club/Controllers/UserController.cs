using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Club.Models;

namespace Club.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet]
        public ActionResult Register()
        {
            //using (var db=new ClubEntities())
            //{
            //    var userList = db.User.ToList();
            //    foreach (var user in userList)
            //    {
            //        user.CreateTime=DateTime.Now.AddDays(-2);
            //    }
            //    db.SaveChanges();
            //}



            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegisterParams userRegisterParams)
        {
            using (var db = new ClubEntities())
            {
                var user = db.User.FirstOrDefault(a => a.Account == userRegisterParams.Account);
                if (user != null)
                {
                    ShowMsg("该用户已存在");
                    return View();
                }

                user = new User();
                user.Account = userRegisterParams.Account;
                user.PassWord = userRegisterParams.PassWord.MD5Encoding(user.Account);
                user.Name = userRegisterParams.Name;
                user.CreateTime = DateTime.Now;
                user.LevelId = 1;
                db.User.Add(user);
                db.SaveChanges();
                ShowMsg("用户注册成功，请登录");
               
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            Session["loginUser"] = null;

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginParams userLoginParams)
        {
            if (string.IsNullOrEmpty(userLoginParams.Account) || string.IsNullOrEmpty(userLoginParams.PassWord))
            {
                ShowMsg("账号密码不能为空");
                return RedirectToAction("Login");
            }

            using (var db = new ClubEntities())
            {

                var pw = userLoginParams.PassWord.MD5Encoding(userLoginParams.Account);

                var user = db.User.FirstOrDefault(a => a.Account == userLoginParams.Account && a.PassWord == pw);
                if (user == null)
                {
                    ShowMsg("用户不存在");
                    return RedirectToAction("Login");
                }
                //设置用户登陆状态
                Session["loginUser"] = user;


                return Redirect("/");
            }

        }
        public ActionResult user()
        {
            var Id = Request["Id"].ToInt();

            using (var db = new ClubEntities())
            {
                var loginUser = (User)Session["loginUser"];
                ViewBag.LoginUser = loginUser;
                var postList = new List<ListPostModel>();
                var list = db.Post.ToList();
                foreach (var item in list)
                {
                    var postModel = new ListPostModel();
                    postModel.Id = item.Id;
                    postModel.Title = item.Title;
                    postModel.UserName = item.User.Name;
                    postModel.CreateTime = item.CreateTime;
                    postModel.ViewCount = item.ViewCount;
                    postModel.Status = item.Status;
                    postModel.UserImage = item.User.Image;
                    postList.Add(postModel);

                }
                return View(postList);
                }
        }

    }
}