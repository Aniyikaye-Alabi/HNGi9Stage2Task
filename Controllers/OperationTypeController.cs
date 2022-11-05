using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace HNGi9Stage2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationTypeController : ControllerBase
    {
        [HttpPost]
        [Route("/Result")]
        public IActionResult CalcNums(PostData postData)
        {
            List<MyOutput> myOutput = new List<MyOutput>();


            int operation(string operationType, int x, int y)
            {
                switch (Enum.Parse<opr_type>(operationType))
                {
                    case opr_type.addition:
                    return x + y;
                        break;
                    case opr_type.subtraction:
                        return x - y;
                        break;
                    case opr_type.multiplication:
                        return x * y;
                        break;
                    default:
                        return 0;
                        break;
                }
            }

            myOutput.Add(new MyOutput
            {
                slackUsername = "dProcessor",
                result = operation(postData.operation_type,  postData.x, postData.y),
                operation_type = postData.operation_type
            });

            return Ok(myOutput.FirstOrDefault());

        }
    }

    [Flags]
    public enum opr_type { addition, subtraction, multiplication }

    public class PostData
    {
        public string operation_type { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public class MyOutput
    {
        public string slackUsername { get; set; }
        public int result { get; set; }
        public string operation_type { get; set; }
    }
}
