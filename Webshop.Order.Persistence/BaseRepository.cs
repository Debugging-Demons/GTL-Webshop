namespace Webshop.Order.Persistence;

public class BaseRepository
{
    public BaseRepository(string tableName, DataContext dataContext)
    {
        this.dataContext = dataContext;
        this.TableName = tableName;
    }

    protected string TableName { get; private set; }
    protected DataContext dataContext { get; private set; }
}