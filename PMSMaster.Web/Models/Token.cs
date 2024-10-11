namespace PMSMaster.Web.Models
{
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
        public double? Target { get; set; }
    }
}
