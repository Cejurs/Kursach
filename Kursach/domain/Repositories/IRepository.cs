namespace Kursach.domain
{
    public interface IRepository
    {
        void Add(string tableName, params string[] parameters);
        void Delete(string tableName, int id);
        void Update(string tableName,object entity);
        object[] GetItemsByDate(string tableName,string date);
    }
}
