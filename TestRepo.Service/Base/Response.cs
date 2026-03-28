namespace TetPee.Service.Base;

public class Response
{
        public class PageResult<T>
        {
            public int TotalItems { get; set; }
            public List<T> Items { get; set; }
        }
}