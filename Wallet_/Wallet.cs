namespace ApplicationNo1.Wallet_
{
    public class Wallet : IWallet
    {
        #region Fields

        private double _balance;
        private double _change;
        private Dictionary<int, double> _transactionsHistory;

        #endregion

        #region Properties
        public double Balance { get { return _balance; } }
        public double Change { get { return _change; } }
        public Dictionary<int, double> TransactionsHistory { get { return _transactionsHistory; } }

        #endregion

        #region Constructor
        public Wallet(double cash)
        {
            _balance = cash;
            _change = 0;
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

        public void GivesChange(double cash, double moneyRequired)
        {
            if(!SurpassesWalletLimit(cash))
            {
                if(cash >= moneyRequired)
                {
                    _change = cash - moneyRequired;
                    _balance -= moneyRequired;
                    AddPaymentToDictionary(moneyRequired);
                }
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
            return !SurpassesWalletLimit(cash);
        }

        private bool SurpassesWalletLimit(double cash)
        {
            return cash > _balance;
        }
        #endregion

    }
}
