using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalTask.Core.APIUtilities
{
    public interface IActionResultResponseHandler
    {
        IRepositoryResult GetResult(IRepositoryActionResult repositoryActionResult);
    }
}
