namespace Judge.App.Controllers
{
    using Infrastructure;
    using Services.Contracts;
    using SimpleMvc.Framework.Contracts;
    using System.Linq;
    using System.Text;

    public class HomeController : BaseController
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            this.ViewModel["userName"] = this.Profile == null? "Guest" : this.Profile.FullName;
            return this.View();
        }
    }
}
