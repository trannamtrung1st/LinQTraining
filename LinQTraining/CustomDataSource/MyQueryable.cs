using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LinQTraining.CustomDataSource
{
    public class MyQueryable : IQueryable<string>
    {
        public List<string> ContainsFilter { get; }
        public List<string> NotEqualsFilter { get; }
        public List<string> EqualsFilter { get; }

        public Type ElementType => typeof(string);

        private Expression _expression;
        public Expression Expression => _expression;

        private IQueryProvider _provider;
        public IQueryProvider Provider => _provider;

        public string QueryText
        {
            get
            {
                var queryText = new StringBuilder("SELECT * FROM MockDataSource").AppendLine();

                if (ContainsFilter.Any())
                {
                    queryText.AppendLine($"CONTAINS:{string.Join(',', ContainsFilter)}");
                }

                if (NotEqualsFilter.Any())
                {
                    queryText.AppendLine($"NOT EQUALS:{string.Join(',', NotEqualsFilter)}");
                }

                if (EqualsFilter.Any())
                {
                    queryText.AppendLine($"EQUALS:{string.Join(',', EqualsFilter)}");
                }

                return queryText.ToString();
            }
        }

        public MyQueryable()
        {
            _provider = new MyQueryProvider(this);
            _expression = Expression.Variable(typeof(IQueryable<string>));
            ContainsFilter = new List<string>();
            NotEqualsFilter = new List<string>();
            EqualsFilter = new List<string>();
        }

        public MyQueryable(MyQueryable from)
        {
            _provider = new MyQueryProvider(this);
            _expression = Expression.Variable(typeof(IQueryable<string>));
            ContainsFilter = from.ContainsFilter.ToList();
            NotEqualsFilter = from.NotEqualsFilter.ToList();
            EqualsFilter = from.EqualsFilter.ToList();
        }

        public IEnumerator<string> GetEnumerator()
        {
            var filteredData = MockDataSource.Query(QueryText.ToString());

            return new List<string>(filteredData).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class MyQueryEnumerator : IEnumerator<string>
    {
        public string Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    class MyQueryProvider : IQueryProvider
    {
        static readonly MethodInfo WhereMethod = typeof(Queryable)
            .GetMethods().FirstOrDefault(m => m.Name == "Where");

        static MyQueryProvider()
        {
        }

        private readonly MyQueryable _query;

        public MyQueryProvider(MyQueryable query)
        {
            _query = query;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return CreateQuery<string>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            if (typeof(string).IsAssignableFrom(typeof(TElement)) == false)
            {
                throw new NotSupportedException();
            }

            switch (expression.NodeType)
            {
                case ExpressionType.Call:
                    {
                        var methodCallExpr = (MethodCallExpression)expression;
                        var baseDef = methodCallExpr.Method.GetBaseDefinition();
                        if (baseDef.HasSameMetadataDefinitionAs(WhereMethod) == false)
                        {
                            throw new NotSupportedException();
                        }

                        var predicate = (LambdaExpression)((UnaryExpression)methodCallExpr.Arguments[1]).Operand;
                        var query = new MyQueryable(_query);

                        switch (predicate.Body.NodeType)
                        {
                            case ExpressionType.Call:
                                {
                                    var body = (MethodCallExpression)predicate.Body;

                                    if (body.Method.Name != "Contains")
                                    {
                                        throw new NotSupportedException();
                                    }

                                    var containsValue = (string)((ConstantExpression)body.Arguments[0]).Value;

                                    query.ContainsFilter.Add(containsValue);

                                    return (IQueryable<TElement>)query;
                                }
                            case ExpressionType.NotEqual:
                                {
                                    var body = (BinaryExpression)predicate.Body;
                                    query.NotEqualsFilter.Add((string)((ConstantExpression)body.Right).Value);
                                    return (IQueryable<TElement>)query;
                                }
                            case ExpressionType.Equal:
                                {
                                    var body = (BinaryExpression)predicate.Body;
                                    query.EqualsFilter.Add((string)((ConstantExpression)body.Right).Value);
                                    return (IQueryable<TElement>)query;
                                }
                        }

                        break;
                    }
            }

            throw new NotSupportedException();
        }

        public object Execute(Expression expression)
        {
            return Execute<string>(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var filteredData = MockDataSource.Query(_query.QueryText);

            return (TResult)(object)filteredData.FirstOrDefault();
        }
    }

    static class MockDataSource
    {
        public static readonly string[] Data = new string[]
        { "A", "AA", "ABC", "ABCD", "ABCDE", "BB", "C", "DD", "E", "FF" };

        // Similar to a SQL database
        public static IEnumerable<string> Query(string queryText)
        {
            IEnumerable<string> data = Data;

            var lines = queryText.Split('\n');

            foreach (var line in lines)
            {
                if (line.StartsWith("CONTAINS"))
                {
                    var contains = line.Split(':')[1].Trim().Split(',');

                    foreach (var value in contains)
                    {
                        data = data.Where(d => d.Contains(value));
                    }
                }
                else if (line.StartsWith("NOT EQUALS"))
                {
                    var notEquals = line.Split(':')[1].Trim().Split(',');

                    foreach (var value in notEquals)
                    {
                        data = data.Where(d => d != value);
                    }
                }
                else if (line.StartsWith("EQUALS"))
                {
                    var equals = line.Split(':')[1].Trim().Split(',');

                    foreach (var value in equals)
                    {
                        data = data.Where(d => d == value);
                    }
                }
            }

            return data;
        }
    }
}
