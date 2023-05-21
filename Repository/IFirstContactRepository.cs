using FirstContactAPI.Model;

namespace FirstContactAPI.Repository
{
    public interface IFirstContactRepository
    {
        Task<IEnumerable<FirstContact>> Get();

        Task<FirstContact> Get(int Id);

        Task<FirstContact> Create(FirstContact firstContact);

        Task Update( FirstContact firstContact);

        Task Delete(int Id);

    }
}
    