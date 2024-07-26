using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageApiController : BaseController
    {
        private readonly ILanguage _language;

        public LanguageApiController(ILanguage language)
        {
            _language = language;
        }

        [HttpGet("GetLanguages")]
        public async Task<IActionResult> GetLanguages()
        {
            var response = await _language.GetLanguages();
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("CreateLanguage")]
        public async Task<IActionResult> CreateLanguage(LanguageDto request)
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
