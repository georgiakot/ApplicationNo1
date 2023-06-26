namespace ApplicationNo1.Users
{
    public class Wallet
    {
        #region Fields

        private double _balance;
        private Dictionary<int, double> _transactionsHistory;

        #endregion

        #region Properties
        public double Balance { get { return _balance; } set { _balance = value; } }
        public Dictionary<int, double> TransactionsHistory { get { return _transactionsHistory; } set { _transactionsHistory = value; } }

        #endregion

        #region Constructor
        public Wallet()
        {
            _balance = 0;
            _transactionsHistory = new Dictionary<int, double>();
        }
        #endregion

        #region Methods

        public void Payment(double cash)
        {
            if (!SurpassesWalletLimit(cash))
            {
                _balance -= cash;
                AddPaymentToDictionary(cash);
            }

        }

        public void AddPaymentToDictionary(double payment)
        {
            //Gets transactionNumber
            int transactionNumber = _transactionsHistory.Count + 1;
            //Adds the payment to dictionary
            _transactionsHistory.Add(transactionNumber, payment);
        }
        public bool ChecksMoneyAvailable(double cash)
        {
            //Chris - This can be a single line
            if (SurpassesWalletLimit(cash))
                return false;
            else
                return true;
        }

        private bool SurpassesWalletLimit(double cash)
        {
            return cash > _balance;
        }


        #endregion


    }
}
