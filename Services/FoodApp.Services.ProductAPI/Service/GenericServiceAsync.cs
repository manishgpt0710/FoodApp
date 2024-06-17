using AutoMapper;
using FoodApp.Services.ProductAPI.Data;

namespace FoodApp.Services.ProductAPI.Service
{
    public interface IGenericServiceAsync<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(long id);
        Task AddAsync(TDto dto);
        Task DeleteAsync(TDto dto);
        Task UpdateAsync(TDto dto);
    }
    public partial class GenericServiceAsync<TEntity, TDto> : IGenericServiceAsync<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public GenericServiceAsync(IUnitOfWork unitOfWork, IMapper mapper) : base()
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            try
            {
                var result = await unitOfWork.Repository<TEntity>().GetAllAsync();

                if (result.Any())
                {
                    return _mapper.Map<IEnumerable<TDto>>(result);
                }
                else
                {
                    throw new EntityNotFoundException($"No {typeof(TDto).Name}s were found");
                }

            }
            catch (EntityNotFoundException ex)
            {
                var message = $"Error retrieving all {typeof(TDto).Name}s";

                throw new EntityNotFoundException(message, ex);
            }
        }

        public async Task<TDto> GetByIdAsync(long id)
        {
            try
            {
                var result = await unitOfWork.Repository<TEntity>().GetByIdAsync(id);

                if (result is null)
                {
                    throw new EntityNotFoundException($"Entity with ID {id} not found.");
                }

                return _mapper.Map<TDto>(result);
            }

            catch (EntityNotFoundException ex)
            {
                var message = $"Error retrieving {typeof(TDto).Name} with Id: {id}";

                throw new EntityNotFoundException(message, ex);
            }
        }

        public async Task AddAsync(TDto dto)
        {
            await unitOfWork.Repository<TEntity>().AddAsync(_mapper.Map<TEntity>(dto));
            await unitOfWork.SaveChangesAsync();

        }

        public async Task DeleteAsync(TDto dto)
        {
            unitOfWork.Repository<TEntity>().DeleteAsync(_mapper.Map<TEntity>(dto));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            unitOfWork.Repository<TEntity>().UpdateAsync(entity);
            await unitOfWork.SaveChangesAsync();
        }
    }

}
