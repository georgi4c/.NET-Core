namespace ModPanel.App.Controllers
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

            return this.View();
        }
    }
}
