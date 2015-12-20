using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LawyersAdda.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        public string AddImage(object a)
        {
            HttpStatusCode result = new HttpStatusCode();

            var httpRequest = HttpContext.Request;
            var url = ""; var msg = "";
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

                    var finalString = new String(stringChars);
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

            return url;
        }
    }
}