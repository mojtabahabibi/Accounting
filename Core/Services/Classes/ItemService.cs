using AutoMapper;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;
using FluentValidation;

namespace EcoBar.Accounting.Core.Services.Classes
{
    public class ItemService : IItemService
    {
        private readonly ILogger<ItemService> logger;
        private readonly IMapper mapper;
        private readonly IValidator<BaseItemDto> createValidator;
        private readonly IItemRepository itemRepository;
        public ItemService(ILogger<ItemService> logger, IMapper mapper, IValidator<BaseItemDto> createValidator, IItemRepository itemRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.createValidator = createValidator;
            this.itemRepository = itemRepository;
        }
        public async Task<GetAllItemResponseDto> GetAllItemAsync()
        {
            logger.LogInformation("ItemService GetAllItem Began");
            try
            {
                var acc = await itemRepository.GetAllAsync();
                logger.LogInformation("ItemService GetAllItem Done");
                var data = mapper.Map<List<ItemListDto>>(acc);
                return new GetAllItemResponseDto()
                {
                    Data = data,
                    DataCount = data.Count(),
                    ErrorCode = Data.Enums.ErrorCodes.OK,
                    Status = true,
                    Message = "دریافت شد"
                };
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "ItemService GetAllItem Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> CreateItemAsync(BaseItemDto dto)
        {
            logger.LogInformation("ItemService CreateFinancialYear Began");
            try
            {
                var validation = await createValidator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = Data.Enums.ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var acc = await itemRepository.AddAsync(mapper.Map<Item>(dto));
                    logger.LogInformation("ItemService CreateFinancialYear Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "ایجاد شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "FinancialYearService CreateFinancialYear Failed");
                throw;
            }
        }
    }
}
