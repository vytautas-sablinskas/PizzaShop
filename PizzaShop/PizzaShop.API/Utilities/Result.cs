namespace PizzaShop.API.Utilities
{
    public record Result (
        ResultType ResultType,
        string Message
    );
}