using Microsoft.AspNetCore.Mvc;
using RadiologyExaminationAPI.Repo;

namespace RadiologyExaminationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private ILogger _logger;
        private IExaminationDbRepo _examinationDbRepo;

        public ExaminationController(ILogger<ExaminationController> logger, IExaminationDbRepo examinationDbRepo)
        {
            _logger = logger;
            _examinationDbRepo = examinationDbRepo;
        }


        [HttpGet("{id}")]
        [HttpGet("{id}/{date?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<string>> Get(string id, string? date = null)
        {
            try
            {
                var examinations = string.IsNullOrEmpty(date) ? 
                    _examinationDbRepo.GetExaminations(id) : 
                    _examinationDbRepo.GetExaminations(id, date);

                return examinations.Any() ? Ok(examinations) :  NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
