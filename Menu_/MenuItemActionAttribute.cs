namespace ApplicationNo1.Menu_
{
    public class MenuItemActionAttribute : Attribute
    {
        #region Fields
        private string? _methodName;

        #endregion

        #region Constructor
        public MenuItemActionAttribute(string methodName)
        {
            _methodName = methodName;
        }
        #endregion

        #region Properties
        public string? MethodName { get { return _methodName; } }
        #endregion
    }
}
