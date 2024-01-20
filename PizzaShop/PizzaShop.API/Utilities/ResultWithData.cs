namespace PizzaShop.API.Utilities
{
    public class ResultWithData<T>
    {
        public ResultType ResultType { get; set; }
        public string? Message { get; set; }

        public T? Data { get; set; }
    }
}
