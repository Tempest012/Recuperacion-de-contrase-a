using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Recuperacion_de_contraseña.Controllers
{
    public class Acceso : Controller
    {
        private char[] str;

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult StarRecovery()
        {
            Models.ViewModel.RecoveryViewModel model = new Models.ViewModel1.RecoveryViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult StarRecovery(Models.ViewModel.RecoveryViewModel mode)
        {
            if (!ModelState.IsValid)
            {
                return View(mode);
            }
            string token =GetSha256(Guid.NewGuid().ToString());

            using (Models.pruebaEntities db = new Models.pruenaEntities())
            {
                var oUser = db.user.where(db => db.email == Model.Email).First0rDefualt();
                if (oUser !=null)
                {
                    oUser.token_recovery = token;
                    db.SaveChanges();
                }
            }
                return View();
        }

        public ActionResult Recovery()
        {
            return View();
        }
        #region HELPERS
        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        #endregion

    }
}
