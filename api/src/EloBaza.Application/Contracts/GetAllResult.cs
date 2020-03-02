using System.Collections.Generic;

namespace EloBaza.Application.Contracts
{
    public class GetAllResult<T> where T : class
    {
        public IEnumerable<T> Data { get; private set; }
        public int TotalCount { get; private set; }

        public GetAllResult(IEnumerable<T> data, int totalCount)
        {
            Data = data;
            TotalCount = totalCount;
        }
    }
}
