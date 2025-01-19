﻿using ModelDTO.ApplicationDTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.IValidators
{
    public interface IRetakeTestApplicationValidation
    {
        Task Validate(CreateRetakeTestApplicationRequest request);
    }
}
