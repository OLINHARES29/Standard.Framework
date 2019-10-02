using Standard.Framework.Result.Enums;
using System.Collections.Generic;

namespace Standard.Framework.Result.Abstractions
{
    public interface IDomainResult<T>
    {
        T Data { get; set; }
        DomainResultType ResultType { get; set; }
        List<string> Messages { get; set; }
    }
}
