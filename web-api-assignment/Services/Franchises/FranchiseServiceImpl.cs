using web_api_assignment.Models.Entities;

namespace web_api_assignment.Services.Franchises
{
    public class FranchiseServiceImpl : IFranchiseService
    {
        private readonly WebApiContext _context;
        public Task AddAsync(Franchise entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Franchise>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Franchise> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Franchise entity)
        {
            throw new NotImplementedException();
        }
    }
}
