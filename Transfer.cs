namespace ReportGoogleBank
{

    public class Transfer
    {
        public int SenderId { get; set; }

        public String SenderName { get; set; }
        public int ReceiverId { get; set; }

        public String ReceiverName { get; set; }

        public float Value { get; set; }
        public string? UserIdRole { get; set; } 

        public void SetUserIdRole(int userId)
        {
            if (SenderId == userId)
            {
                UserIdRole = "sender";
            }
            else if (ReceiverId == userId)
            {
                UserIdRole = "receiver";
            }
        }
    }
}