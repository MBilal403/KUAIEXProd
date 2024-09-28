using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web;
using Newtonsoft.Json;
using static DataAccessLayer.Helpers.ImageUploadUtility;
using DataAccessLayer.Recources;

namespace DataAccessLayer.Helpers
{
    public class ImageUploadUtility
    {
        public class ApiResponse
        {
            public string ResponseCode { get; set; }
            public string ResponseMessage { get; set; }
            public ResponseData[] ResponseData { get; set; }
            public DateTime ResponseTimeStamp { get; set; }
        }

        public class ResponseData
        {
            public string Civil_Id_Front { get; set; }
            public string Civil_Id_Back { get; set; }
        }
        public static ApiResponse UploadImages(string civilID, HttpPostedFileBase Civil_Id_Front, HttpPostedFileBase Civil_Id_Back)
        {
            try
            {

                var uploadUrl = WebConfigurationManager.AppSettings["ImageUploadUrl"];
                if (string.IsNullOrEmpty(uploadUrl))
                {
                    throw new ArgumentNullException("Upload URL not configured.");
                }

                using (var client = new HttpClient())
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        form.Add(new StringContent(civilID), "Civil_Id");
                        if (Civil_Id_Front != null && Civil_Id_Front.ContentLength > 0)
                        {
                            var imageContent1 = new StreamContent(Civil_Id_Front.InputStream);
                            imageContent1.Headers.ContentType = MediaTypeHeaderValue.Parse(Civil_Id_Front.ContentType);
                            form.Add(imageContent1, "Civil_Id_Front", Civil_Id_Front.FileName);
                        }

                        if (Civil_Id_Back != null && Civil_Id_Back.ContentLength > 0)
                        {
                            var imageContent2 = new StreamContent(Civil_Id_Back.InputStream);
                            imageContent2.Headers.ContentType = MediaTypeHeaderValue.Parse(Civil_Id_Back.ContentType);
                            form.Add(imageContent2, "Civil_Id_Back", Civil_Id_Back.FileName);
                        }

                        var response = client.PostAsync(uploadUrl, form).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var apiResponseContent = response.Content.ReadAsStringAsync().Result;
                            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(apiResponseContent);

                            return apiResponse;
                        }
                        else
                        {
                            var apiResponseContent = response.Content.ReadAsStringAsync().Result;
                            ApiResponse errorResponse = JsonConvert.DeserializeObject<ApiResponse>(apiResponseContent);
                            return errorResponse;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }
    }
}
