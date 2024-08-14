using AutoMapper;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Core.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;
        private readonly IMapper mapper;
        private readonly IUserRepository UserRepository;
        public UserService(ILogger<UserService> logger, IMapper mapper, IUserRepository UserRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.UserRepository = UserRepository;
        }
        public async Task<UserListResponseDto> GetAllUser()
        {
            logger.LogInformation("UserService GetAllUser Began");
            try
            {
                var result = await UserRepository.GetAllAsync();
                var dto = mapper.Map<List<UserListDto>>(result);
                logger.LogInformation("UserService GetAllUser Failed");
                return new UserListResponseDto()
                {
                    Data = dto,
                    DataCount = result.Count(),
                    ErrorCode = Data.Enums.ErrorCodes.OK,
                    Status = true,
                    Message = "دریافت شد"
                };
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "UserService GetAllUser Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> CreateUserAsync(CreateUserDto dto)
        {
            logger.LogInformation("UserService CreateUser Began");
            try
            {
                var response = new BaseResponseDto<bool?>();
                var result = await UserRepository.CreateUserAsync(mapper.Map<User>(dto));
                logger.LogInformation("UserService CreateUser Done");
                return new BaseResponseDto<bool?>()
                {
                    ErrorCode = Data.Enums.ErrorCodes.OK,
                    Status = true,
                    Message = "ایجاد شد",
                };
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "UserService CreateUser Failed");
                throw;
            }
        }
    }
}
