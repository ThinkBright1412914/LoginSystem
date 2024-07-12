using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;


namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;

        public UserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _adminUserService.GetUsers();
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUsersById(Guid Id)
        {
            var response = await _adminUserService.GetUserById(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDataVM request)
        {
            var response = await _adminUserService.CreateUser(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }


        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserDataVM request)
        {
            var response = await _adminUserService.UpdateUser(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }


        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var response = await _adminUserService.DeleteUser(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }


        [HttpGet("GeneratePdf")]
        public async Task<IActionResult> GeneratePdf(Guid Id)
        {
            var document = new PdfDocument();
            var userInfo = await _adminUserService.GetUserById(Id);
            Random random = new Random();
            int invoiceNo = random.Next(0, 10);
            var currentDate = DateTime.Now;
            string htmlContent = $@"
                <html lang=""en"">
                <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>UserIfno</title>
                <style>
                  body {{
                    font-family: Arial, sans-serif;
                    line-height: 1.6;
                    background-color: #f0f0f0;
                    padding: 20px;
                  }}
                  .invoice-container {{
                    max-width: 600px;
                    margin: 0 auto;
                    background-color: #fff;
                    padding: 20px;
                    box-shadow: 0 0 10px rgba(0,0,0,0.1);
                  }}
                  .invoice-header {{
                    display: flex;
                    align-items: center;
                    margin-bottom: 20px;
                  }}
                  .invoice-header img {{
                    max-width: 100px;
                    height: auto;
                    margin-right: 20px;
                  }}
                  .invoice-header h2 {{
                    margin: 0;
                    font-size: 24px;
                  }}
                  .invoice-details {{
                    margin-bottom: 20px;
                  }}
                  .invoice-details h4 {{
                    margin-top: 0;
                    font-size: 18px;
                  }}
                  .invoice-table {{
                    width: 100%;
                    border-collapse: collapse;
                    margin-bottom: 20px;
                  }}
                  .invoice-table th, .invoice-table td {{
                    padding: 10px;
                    border: 1px solid #ccc;
                    text-align: left;
                  }}
                  .invoice-total {{
                    text-align: right;
                  }}
                </style>
                </head>
                <body>
                  <div class=""invoice-container"">
                    <div class=""invoice-header"">
                      <h2>User Details</h2>
                    </div>
                    <div class=""invoice-details"">
                      <h4>Invoice Details</h4>
                      <p><strong>Invoice Number:</strong> INV-{invoiceNo}</p>
                      <p><strong>Invoice Date:</strong> {currentDate.ToString("yyyy MMMM dd dddd")}</p>
                    </div>
                    <div class=""user-details"">
                      <h4>User Details</h4>
                      <p><strong>Name:</strong> {userInfo.UserName}</p>
                      <p><strong>Email:</strong> {userInfo.Email}</p>
                      <p><strong>Address:</strong> Test</p>
                    </div>
                    <table class=""invoice-table"">
                      <thead>
                        <tr>
                          <th>Description</th>
                          <th>Quantity</th>
                          <th>Unit Price</th>
                          <th>Total</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <td> N/A </td>
                          <td> N/A </td>
                          <td> N/A </td>
                          <td> N/A </td>
                        </tr>               
                      </tbody>
                      <tfoot>
                        <tr>
                          <td colspan=""3"" class=""invoice-total"">Total:</td>
                          <td> N/A </td>
                        </tr>
                      </tfoot>
                    </table>
                    <div class=""invoice-notes"">
                      <h4>Notes</h4>
                      <p>This is a sample.</p>
                    </div>
                  </div>
                </body>
                </html>
                ";
            PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
            byte[] response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            string fileName = $"Test_Invoice_{invoiceNo}.pdf";
            return File(response, "application/pdf", fileName);
        }


    }
}
