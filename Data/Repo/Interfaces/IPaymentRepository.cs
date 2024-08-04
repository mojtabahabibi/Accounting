using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
    }
}
