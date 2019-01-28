using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace PracticalTask.Business
{
    public class UserBusiness
    {
        private readonly IMapper _mapper;

        public UserBusiness(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
