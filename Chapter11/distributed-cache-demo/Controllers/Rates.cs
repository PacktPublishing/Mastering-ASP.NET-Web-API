namespace distributed_cache_demo.Controllers
{
    public class Rates
    {
        public double CAD { get; set; }
        public double GBP { get; set; }
        public double INR { get; set; }
    }

    public class RatesRoot
    {
        public string @base { get; set; }
        public string date { get; set; }
        public Rates rates { get; set; }
    }

}
