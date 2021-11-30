using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Repositories;

namespace server.Controllers.Base
{
    [ApiController]
    [Authorize(Policy = "ClaimsRequired")]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }
    }
}