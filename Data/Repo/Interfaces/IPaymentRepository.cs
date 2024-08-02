using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IPaymentRepository : IBaseRepo<Payment>
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
    }
}
