namespace BlazorExpenseTracker.API
{
    public class Properties : IProperties
    {
        private readonly string _cnn;
        public  Properties(string conn) 
        { 
            _cnn= conn;
        }

        public string ConnectiosString { get => _cnn; }
    }
}
