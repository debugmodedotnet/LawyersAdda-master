using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using LawyersAdda.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using LawyersAdda.Entities;
namespace LawyersAdda.Controllers
{
    public class FileController : Controller
    {
        //
        // GET: /File/
        public ActionResult Index()
        {
            return View();
        }
        public async Task<string> AddFile(string id)
        {
            HttpStatusCode result = new HttpStatusCode();
            var url = ""; var msg = ""; var finalString = "";
            //string UserId = Session["UserID"].ToString();
            string UserId = User.Identity.GetUserId();
            if (UserId != null && UserId.Length != 0)
            {
                var httpRequest = HttpContext.Request;

                try
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];

                        //Azure Upload
                        var contentType = postedFile.ContentType;
                        var streamContents = postedFile.InputStream;
                        var blobName = postedFile.FileName;
                        string UserConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", "debugmodelab", "t2yJAWoR8tcL0fcz6TbP/5m3fSmgWS0qIAY2aj8G9k4vbhdzlKsKpmrHJ3AgWP5GsyTkPM8g9lGSyPG2MhNVzQ==");
                        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(UserConnectionString);
                        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                        CloudBlobContainer blobContainer = blobClient.GetContainerReference("masterjee");
                        blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });


                        //Random Name
                        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                        var stringChars = new char[8];
                        var random = new Random();

                        for (int i = 0; i < stringChars.Length; i++)
                            stringChars[i] = chars[random.Next(chars.Length)];

                        finalString = new String(stringChars);
                        //Random Name

                        var blob1 = blobContainer.GetBlockBlobReference(finalString);
                        blob1.Properties.ContentType = contentType;
                        blob1.UploadFromStream(streamContents);

                        url = "https://debugmodelab.blob.core.windows.net/" + blobContainer.Name + "/" + finalString;
                        //Azure Upload

                    }
                    result = HttpStatusCode.Created;
                    if (url != "")
                        msg = "Uploaded Successfully";


                }
                catch (Exception errMsg)
                {
                    result = HttpStatusCode.BadRequest;
                    msg = "Error Uploading file" + errMsg;

                }
            }

            ApplicationDbContext db = new ApplicationDbContext();
            RelatedDocument rd = new RelatedDocument();
            rd.ID = Guid.NewGuid().ToString();
            rd.URL = url;
            rd.DocumentID = id;
            try
            {
                db.RelatedDocuments.Add(rd);
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            ImgReturn a = new ImgReturn();
            a.imgUrl = url;
            a.response = result;
            a.msg = msg;
            return url;
        }
	}
}