﻿using LearningSystem.Services.Models;
using System;
using System.Threading.Tasks;

namespace LearningSystem.Services
{
    public interface IUserService
    {
        Task<UserProfileServiceModel> ProfileAsync(string username);
    }
}
