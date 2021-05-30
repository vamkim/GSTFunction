using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace GSTFunction
{
    public class GSTFunction
    {
        private readonly IXmlProcessService _service;
        private readonly IConfiguration _configuration;

        public GSTFunction(IConfiguration configuration, IXmlProcessService service)
        {
            _configuration = configuration;
            _service = service;
        }
        [FunctionName("GSTFunction")]
        public async Task<IActionResult> GSTFunctionAsyc(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Start GST Convertion");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var returnValue = await processGMTvalues(requestBody);
                return new OkObjectResult(Newtonsoft.Json.JsonConvert.SerializeObject(returnValue));
            }
            catch (Exception ex)
            {
                var dic = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
                dic.AddModelError("body", $"Invalid body, unable to parse message due to error. ex:{ex.Message}");

                log.LogError($"Invalid body, unable to parse message due to error. ex:{ex.Message}");
                return new BadRequestObjectResult(dic);
            }
        }

        public async Task<Model.expense> processGMTvalues(string requestBody)
        {
            var returnValue = new Model.expense();
            try
            {
                double convertedGSTpercentage;
                double.TryParse(_configuration["GSTpercentage"], out convertedGSTpercentage);
                var extractedXML = _service.extractXMLinText(requestBody);
                returnValue = _service.convertXMLtoData(extractedXML, convertedGSTpercentage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            return returnValue;
        }
    }
}
