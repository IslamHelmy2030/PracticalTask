using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalTask.Business.Dto.Parameter
{
    public class UserParameterDto : IUserParameterDto
    {
        [Required(ErrorMessage = "UserId Is Required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name Is Required", AllowEmptyStrings = false), StringLength(maximumLength: 50, ErrorMessage = "Invalid Name")]
        public string Username { get; set; }
    }
}
