using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class Authenticator
    {
        // TODO: Implement the Authenticator.Admin property
        public Identity Admin = new Identity
        {
            Email = "admin@ex.ism",
            FacialFeatures = new FacialFeatures { EyeColor = "green", PhiltrumWidth = 0.9M },
            NameAndAddress = new List<string> { "Chanakya", "Mumbai", "India" }
        };

        // TODO: Implement the Authenticator.Developers property
        public IDictionary<string, Identity> Developers = new Dictionary<string, Identity>
        {
            ["Bertrand"] = new Identity
            {
                Email = "bert@ex.ism",
                FacialFeatures = new FacialFeatures { EyeColor = "blue", PhiltrumWidth = 0.8M },
                NameAndAddress = new List<string> { "Bertrand", "Paris", "France" }
            },

            ["Anders"] = new Identity
            {
                Email = "anders@ex.ism",
                FacialFeatures = new FacialFeatures { EyeColor = "brown", PhiltrumWidth = 0.85M },
                NameAndAddress = new List<string> { "Anders", "Redmond", "USA" }
            },
        };
    }

    //**** please do not modify the FacialFeatures class ****
    public class FacialFeatures
    {
        public string EyeColor { get; set; }
        public decimal PhiltrumWidth { get; set; }
    }

    //**** please do not modify the Identity class ****
    public class Identity
    {
        public string Email { get; set; }
        public FacialFeatures FacialFeatures { get; set; }
        public IList<string> NameAndAddress { get; set; }
    }
}
