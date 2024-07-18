using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguage _language;

        public LanguageController(ILanguage language)
        {
            _language = language;
        }

        [HttpGet("GetLanguages")]
        public async Task<IActionResult> GetLanguages()
        {
            var response = await _language.GetLanguages();
            if (response.Count() > 0)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("CreateLanguage")]
        public async Task<IActionResult> CreateLanguage(LanguagugeDto request)
        {
            var response = await _language.Create(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("DeleteLanguage")]
        public async Task<IActionResult> DeleteLanguage(int Id)
        {
            var response = await _language.Delete(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
