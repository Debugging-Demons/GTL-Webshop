namespace Webshop.Order.Persistence;

public class BaseRepository
{
    public BaseRepository(string tableName, IDataContext dataContext)
    {
        this.dataContext = dataContext;
        this.TableName = tableName;
    }

    protected string TableName { get; private set; }
    protected IDataContext dataContext { get; private set; }
}