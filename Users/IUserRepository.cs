namespace FlashcardXpApi.Users
{
    public interface IUserRepository
    {
        void Insert(User user);

        void Update(User user);

        Task<User?> GetById(int id);
        
        Task<List<User>> GetAll();

        Task<bool> IsEmailUnique(string email);

        void SaveChangesAsync();


    }
}
