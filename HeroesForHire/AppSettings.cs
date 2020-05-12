namespace HeroesForHire
{
    public class AppSettings
    {
        public string Secret { get; set; } 
        public string[] AllowedOrigins { get; set; }
        
        public string CamundaRestApiUri { get; set; }
    }
}