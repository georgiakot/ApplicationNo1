using ApplicationNo1.Trip_;
using ApplicationNo1.User_;

namespace ApplicationNo1.Menu_
{
    public class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu(new UserService(),new TripService());
            menu.Start();
        }
    }
}