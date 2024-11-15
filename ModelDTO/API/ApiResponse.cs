using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.API;

public class ApiResponse
{
    public ApiResponse()
    {
        this.ErrorMessages = new List<string>();
    }
    public HttpStatusCode statusCode { get; set; }
    public bool IsSuccess { get; set; }
    public List<string> ErrorMessages { get; set; }
    public Object Result { get; set; }
}
