using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using LawyersAdda.Models;
using LawyersAdda.Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace LawyersAdda.Controllers
{
    public class ImageController : Controller
    {

        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        public async Task<string> AddImage()
        {
            HttpStatusCode result = new HttpStatusCode();
            var url = ""; var msg = ""; var finalString = "";
            //string lawyerId = Session["LUserId"].ToString();
            string lawyerId = User.Identity.GetUserId();
            if (lawyerId != null && lawyerId.Length != 0)
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

            LawyerImage image = new LawyerImage();
            image.Id = finalString;
            image.ImageUrl = url;
            image.isDisplayPic = false;
            image.LawyerId = lawyerId;
            ApplicationDbContext db = new ApplicationDbContext();
            var l = db.Lawyers.Find(lawyerId);
            l.ImageUrl = url;
            db.LawyerImages.Add(image);
            try
            {
                db.Entry(l).State = EntityState.Modified;
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

        public async Task<string> AddUserImage()
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
            ApplicationUser usr = new ApplicationUser();
            usr = db.Users.Where(t => t.Id == UserId).Single();
            usr.ProfilePicURL = url;
            try
            {
                db.Entry(usr).State = EntityState.Modified;
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