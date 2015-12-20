using System.Net;

namespace LawyersAdda.Controllers
{
    public class ImgReturn
    {
        public string imgUrl { get; set; }
        public HttpStatusCode response { get; set; }
        public string msg { get; set; }
    }
}