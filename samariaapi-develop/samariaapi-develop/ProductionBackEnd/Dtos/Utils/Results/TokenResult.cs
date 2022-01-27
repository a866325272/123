using System;

namespace ProductionBackEnd.Dtos.Utils.Results
{
    public class TokenResult
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
