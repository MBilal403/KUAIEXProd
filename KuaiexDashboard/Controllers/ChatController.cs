using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using KuaiexDashboard.Filters;
using Newtonsoft.Json;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class ChatController : Controller
    {
        ChatDAL objChatDal = new ChatDAL();
        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerChatMain()
        {
            var status = "error";

            try
            {
                List<CustomerChatMain_Result> list = objChatDal.GetCustomerChatMain();
                status = JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Content(status);
        }
        public ActionResult CustomerChatDetail(Guid UID)
        {
            var status = "error";

            try
            {
                List<CustomerChatDetail_Result> result = objChatDal.GetCustomerChatDetail(UID);
                status = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Content(status);
        }
        public ActionResult CustomerChatTitle(Guid UID)
        {
            var status = "error";

            try
            {
                List<CustomerChatTitle_Result> result = objChatDal.GetCustomerChatTitle(UID);
                status = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Content(status);
        }
        public ActionResult SaveChat(Guid UID, string Message)
        {
            string status = "error";

            try
            {
                Customer_Chat objChat = new Customer_Chat();

                objChat.UID = UID;
                objChat.Message = Message;
                objChat.Message_Type = 1;
                objChat.Created_On = DateTime.Now;
                objChatDal.SaveCustomerChat(objChat.UID, objChat.Message);
                status = "success";
            }
            catch (Exception ex)
            {
                status = "error";
            }
            //}
            return Content(status);
        }
        public ActionResult Chat()
        {
            var status = "error";

            try
            {
                List<Customer_Chat> list = objChatDal.GetAllChats();
                status = JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Content(status);
        }
    }
}