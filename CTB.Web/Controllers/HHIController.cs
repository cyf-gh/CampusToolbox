using Microsoft.AspNetCore.Mvc;
using CTB.Model.HHI;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CTB.Service;
using CTB.Service.HHI;

namespace CTB.Web.Controllers {
    public class Utils_HHIController : Controller {
        private readonly IHHIService _HHIService;

        public Utils_HHIController( IHHIService HHIService ) {
            _HHIService = HHIService;
        }

        public IActionResult UploadImage( string name ) {
            return View( ( (HHIModel)_HHIService.GetHHIModel() ).GetTaskByName( name ) );
        }

        public IActionResult Index() {
            return View( (HHIModel)_HHIService.GetHHIModel() );
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
            return Ok( "Successfully Uploaded!" );
        }
        public IActionResult Issue() {
            Directory.GetCurrentDirectory();
            return View();
        }
        public async Task<IActionResult> GetFileIssue() {
            return Ok();
        }
    }
}