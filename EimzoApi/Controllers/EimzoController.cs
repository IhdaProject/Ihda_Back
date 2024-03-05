using EimzoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServicePkcs7;
using ServiceTsaProxy;
using System;
using System.Threading.Tasks;

namespace EimzoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EimzoController : ControllerBase
    {

        private readonly ILogger<EimzoController> _logger;
        //private ILanguageService _languageService;

        public EimzoController(ILogger<EimzoController> logger)
        //    ILanguageService languageService)
        {
            _logger = logger;
            //_languageService = languageService;
        }

        [HttpPost]
        public async Task<PkcsResponse> GetVerifyPkcs([FromBody] SignaturePost model)
        {
            try
            {
                var pkcs7 = new Pkcs7Client();
                var responsePkcs = await pkcs7.verifyPkcs7Async(model.Signature);
                if (responsePkcs == null)
                    throw new Exception("#222");

                var verifyPkcs = JsonConvert.DeserializeObject<PkcsResponse>(responsePkcs.Body.@return);

                return verifyPkcs;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error VerifyPkcs7: {ex.Message}");
                return null;
            }
        }


        [HttpPost]
        public async Task<TimeStampModel> GetTimeStamp([FromBody] SignaturePost model)
        {
            try
            {
                var timeStampToken = new TimeStampResponse();
                var pkcs7 = new Pkcs7Client();
                var  responsePks= await pkcs7.verifyPkcs7Async(model.Signature);
                var verifyPkcs = JsonConvert.DeserializeObject<PkcsResponse>(responsePks.Body.@return);
                if (verifyPkcs != null&& verifyPkcs.Success)
                {
                    var signatureHex = verifyPkcs.Pkcs7Info.Signers[0].Signature;
                    var tsa = new TsaProxyClient();
                    var responseToken = await tsa.getTimeStampTokenForSignatureAsync(signatureHex);
                    timeStampToken = JsonConvert.DeserializeObject<TimeStampResponse>(responseToken.Body.@return);

                    if (timeStampToken == null || !timeStampToken.Success)
                        throw new Exception("#223" + " : " + timeStampToken?.Reason);
                }
                return  new TimeStampModel { TimeStamp = timeStampToken.TimeStampTokenB64 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error TimeStamp: {ex.Message}");
                return  null;
            }
        }

    }
}
