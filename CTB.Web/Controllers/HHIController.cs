using Microsoft.AspNetCore.Mvc;
using CTB.DomainModel.HappyHandingIn;
using CTB.Factory.HappyHandingIn;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CTB.Web.Controllers {
    public class Utils_HHIController : Controller {
        public IActionResult UploadImage( string name ) {
            HHIModel model = null;
            using( HHIFactory factory = new HHIFactory() ) {
                model = (HHIModel)factory.Create( HHIFactory.CreateBy.DataWebsite, "https://www.cnblogs.com/PROJECT-IDOLPROGRAM/p/10335534.html" );
            }
            return View( model.GetTaskByName( name ) );
        }
        public IActionResult Index() {
            HHIModel model = null;
            using( HHIFactory factory = new HHIFactory() ) {
                model = (HHIModel)factory.Create( HHIFactory.CreateBy.DataWebsite, "https://www.cnblogs.com/PROJECT-IDOLPROGRAM/p/10335534.html" );
            }
            return View( model );
        }
        public IActionResult UploadSuccessfully() {

            return View();
        }
        public async Task<IActionResult> FileSave( [FromServices]IHostingEnvironment env ) {
            var files = Request.Form.Files;
            int imgCount = Convert.ToInt32( Request.Form["image-count"] );
            for( int i = 0; i < imgCount; i++ ) {
                string blobImg = null;
                var blobSrc = Request.Form[i.ToString()];

                var match = Regex.Match( blobSrc.ToString(), "data:image/jpeg;base64,([\\w\\W]*)$" );
                if( match.Success ) {
                    blobImg = match.Groups[1].Value;
                } else {
                    var match2 = Regex.Match( blobSrc.ToString(), "data:image/png;base64,([\\w\\W]*)$" );
                    blobImg = match2.Groups[1].Value;
                }
                if( blobImg == null ) {
                    continue;
                }
                byte[] imgBytes = new byte[0];
                try {
                    imgBytes = Convert.FromBase64String( blobImg );

                    MemoryStream imgMemStream = new MemoryStream( imgBytes );

                    var imgStream = new FileStream( Guid.NewGuid().ToString() + ".png", FileMode.OpenOrCreate );
                    await imgMemStream.CopyToAsync( imgStream );
                    imgStream.Close();
                } catch( Exception ex ) { }
            }
            return Ok("Successfully Uploaded!");
        }
    }
}