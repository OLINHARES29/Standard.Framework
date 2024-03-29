﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace Standard.Framework.Result.Abstractions
{
    public interface IApplicationResult<T> : IActionResult
    {
        T Data { get; set; }
        HttpStatusCode StatusCode { get; set; }
        List<string> Messages { get; }

        void SetStatusCode(HttpStatusCode statusCode);
    }
}
