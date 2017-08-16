namespace MyWallet.ErrorHandlers
{
    public class WebAPIError
    {
        public string message { get; set; }
        public bool isError { get; set; }
        public string detail { get; set; }        

        public WebAPIError(string message)
        {
            this.message = message;
            isError = true;
        }        
    }
}