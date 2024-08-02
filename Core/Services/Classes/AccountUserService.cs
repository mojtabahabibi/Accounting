﻿using AutoMapper;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Core.Services.Classes
{
    public class AccountUserService : IAccountUserService
    {
        private readonly ILogger<AccountUserService> logger;
        private readonly IMapper mapper;
        private readonly IAccountUserRepository accountUserRepository;
        public AccountUserService(ILogger<AccountUserService> logger, IMapper mapper, IAccountUserRepository accountUserRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.accountUserRepository = accountUserRepository;
        }
        public async Task<BaseResponseDto<bool?>> CreateAccountUserAsync(CreateAccountUserDto dto)
        {
            logger.LogInformation("AccountUserService CreateAccountUser Began");
            try
            {
                var response = new BaseResponseDto<bool?>();
                var result = await accountUserRepository.AddAsync(mapper.Map<AccountUser>(dto));
                logger.LogInformation("AccountUserService CreateAccountUser Done");
                return new BaseResponseDto<bool?>()
                {
                    ErrorCode = Data.Enums.ErrorCodes.OK,
                    Status = true,
                    Message = "ایجاد شد",
                };
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountUserService CreateAccountUser Failed");
                throw;
            }
        }
    }
}
