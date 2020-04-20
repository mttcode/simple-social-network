using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loners.DBModel;
using System.Linq.Dynamic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Loners.Controllers
{
    public class HomeController : Controller
    {
        LonersEntities lonersEntities = new LonersEntities();

        public ActionResult Index()
        {   
            return View();
        }


        public ActionResult UserProfile()
        {
            string userName = Session["UserName"].ToString();
            var loginUser = lonersEntities.Users.Where(m => m.UserName == userName).FirstOrDefault();

            return View(loginUser);
        }


        public ActionResult EditProfile()
        {
            Loners.Models.EditModel editModel = new Models.EditModel();
            return View(editModel);
        }


        [HttpPost]
        public ActionResult EditProfile(Loners.Models.EditModel editModel)
        {
            string userName = Session["UserName"].ToString();
            var loginUser = lonersEntities.Users.Where(m => m.UserName == userName).FirstOrDefault();

                DateTime dateOfBirth = new DateTime(editModel.DateYear, editModel.DateMonth, editModel.DateDay);

                loginUser.Age = (int)((DateTime.Today - dateOfBirth).Days / 365.25);
                loginUser.Country = editModel.Country;
                loginUser.AboutMe = editModel.AboutMe;
                loginUser.MBTI = editModel.MBTI;
                loginUser.Biorytmus = editModel.Biorytmus;
                loginUser.InterestsHobbies = editModel.InterestsHobbies;

                lonersEntities.SaveChanges();
            
            return RedirectToAction("UserProfile", "Home");
        }


        [HttpGet]
        public ActionResult Chat()
        {
            var userMessages = from usersChatBoard in lonersEntities.ChatBoards
                               orderby usersChatBoard.DateTimeOfMessage descending
                               select usersChatBoard;

            ViewBag.ChatBoardList = userMessages.ToList();

            var onlineUsers = from user in lonersEntities.Users
                              where user.Status == "Logged In"
                              select user;

            ViewBag.UsersOnlineList = onlineUsers.ToList();

            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Chat(Loners.Models.ChatModel chatModel)
        {
            DBModel.User user = new DBModel.User();

            DBModel.ChatBoard chatBoard = new ChatBoard();
            chatBoard.FromUser = Session["UserName"].ToString();
            chatBoard.Message = chatModel.Message;
            chatBoard.DateTimeOfMessage = DateTime.Now.ToString();

            lonersEntities.ChatBoards.Add(chatBoard);
            await lonersEntities.SaveChangesAsync();

            return RedirectToAction("Chat", "Home");
        }



        public ActionResult Users(int page = 1, string sort = "UserName", string sortdir = "asc", string search = "")
        {
            int pageSize = 15;
            int totalRecord = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            var data = GetUsers(search, sort, sortdir, skip, pageSize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;

            return View(data);
        }



        public List<Loners.DBModel.User> GetUsers(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            using (LonersEntities lonersEntities = new LonersEntities())
            {
                var users = (from user in lonersEntities.Users
                             where
                                    user.UserName.Contains(search) ||
                                    user.Age.ToString().Contains(search) ||
                                    user.Gender.Contains(search) ||
                                    user.Country.Contains(search) ||
                                    user.MBTI.Contains(search) ||
                                    user.Status.ToString().Contains(search)
                             select user
                             );

                totalRecord = users.Count();
                users = users.OrderBy(sort + " " + sortdir);

                if(pageSize > 0)
                {
                    users = users.Skip(skip).Take(pageSize);
                }

                return users.ToList();
            }  
        }
    }
}
