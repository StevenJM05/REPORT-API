namespace POSREPORT.models
{
    public class Sales
    {
        private int _id;
        private int _customerId;
        private int _paymentId;
        private int _userId;
        private int _podId;
        public int Id { get => _id; set => _id = value; }
        public int CustomerId { get => _customerId; set => _customerId = value; }
        public int PaymentId { get => _paymentId; set => _paymentId = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public int PodId { get => _podId; set => _podId = value; }

        public Sales(int id, int customerId, int paymentId, int userId, int podId)
        {
            _id = id;
            _customerId = customerId;
            _paymentId = paymentId;
            _userId = userId;
            _podId = podId;
        }

       
    }
}
