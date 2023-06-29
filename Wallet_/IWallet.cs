namespace ApplicationNo1.Wallet_
{ 
    public interface IWallet
    {
        #region Properties
        double Balance { get; }

        double Change { get; }
        Dictionary<int, double> TransactionsHistory { get; }
        #endregion

        #region Methods
        void Payment(double cash);

        void AddPaymentToDictionary(double payment);

        bool ChecksMoneyAvailable(double cash);
        #endregion
    }
}
