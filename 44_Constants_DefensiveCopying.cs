using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code3
{
    public class Authenticator
    {
        private class EyeColor
        {
            const string Blue = "blue";
            const string Green = "green";
            const string Brown = "brown";
            const string Hazel = "hazel";
            const string Grey = "grey";
        }

        public Authenticator(Identity admin)
        {
            this.admin = admin;
        }

        private readonly Identity admin;

        private IDictionary<string, Identity> developers
            = new Dictionary<string, Identity>
                {
                    ["Bertrand"] = new Identity
                    {
                        Email = "bert@ex.ism",
                        EyeColor = "blue"
                    },

                    ["Anders"] = new Identity
                    {
                        Email = "anders@ex.ism",
                        EyeColor = "brown"
                    }
                };

        public Identity Admin
        {
            get { return new Identity(admin); }
        }

        public ReadOnlyDictionary<string, Identity> GetDevelopers()
        {
            return  new ReadOnlyDictionary<string, Identity>(developers);
        }
    }

    public struct Identity
    {
        public string Email { get; set; }

        public string EyeColor { get; set; }

        public Identity(Identity identity)
        {
            Email = identity.Email;
            EyeColor = identity.EyeColor;
        }
    }
}
