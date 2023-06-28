namespace ApplicationNo1.Menu_
{
    public class MenuItem
    {
        #region Constructor
        public MenuItem()
        {
            SubItems = new List<MenuItem>();
        }

        #endregion

        #region Properties
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int UserSelection { get; set; }
        public List<MenuItem> SubItems { get; set; }
        public Action? UserSelectionAction { get; set; }

        #endregion

    }
}
