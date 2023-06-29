namespace ApplicationNo1.Menu_
{
    public class MenuItemActionAttribute : Attribute
    {
        private string? _methodName;

        public MenuItemActionAttribute(string methodName)
        {
            _methodName = methodName;
        }
        public string? MethodName { get { return _methodName; } }
    }
}
