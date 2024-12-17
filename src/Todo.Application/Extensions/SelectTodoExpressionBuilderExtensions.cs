using System.Text;

using Todo.Domain.Entities;

namespace Todo.Application.Extensions;
public static class SelectTodoExpressionBuilderExtensions
{
    private const string BaseQuery = "SELECT * FROM c ";
    public static string GetQueryString(this string result, string? description, ItemStatus? status, uint? pageNumber, uint pageSize)
    {
        var whereClauseCount = 0;
        StringBuilder sb = new StringBuilder();
        pageNumber = pageNumber == null || pageNumber.Value == 0 ? 0 : (pageNumber.Value - 1) * pageSize;

        sb.Append(BaseQuery);
        if (description != null)
        {
            whereClauseCount++;
            sb.Append($"WHERE c.description LIKE \"%{description}%\" ");
        }
        if (status != null)
        {
            sb.Append(whereClauseCount == 0 ? "WHERE " : "AND ");
            sb.Append($" c.status = {(uint)status} ");
        }
        sb.Append($"ORDER BY c.description OFFSET {pageNumber} LIMIT {pageSize}");
        result = sb.ToString();

        return result;

    }
}
