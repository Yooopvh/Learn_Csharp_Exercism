using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code2
{
    public class FacialFeatures : IEquatable<FacialFeatures>
    {
        public string EyeColor { get; }
        public decimal PhiltrumWidth { get; }

        public FacialFeatures(string eyeColor, decimal philtrumWidth)
        {
            EyeColor = eyeColor;
            PhiltrumWidth = philtrumWidth;
        }

        public bool Equals(FacialFeatures? other) => this.EyeColor == other.EyeColor && this.PhiltrumWidth == other.PhiltrumWidth ? true : false;

        public override int GetHashCode()
        {
            return HashCode.Combine(EyeColor, PhiltrumWidth);
        }
    }

    public class Identity : IEquatable<Identity>
    {
        public string Email { get; }
        public FacialFeatures FacialFeatures { get; }

        public Identity(string email, FacialFeatures facialFeatures)
        {
            Email = email;
            FacialFeatures = facialFeatures;
        }

        public bool Equals(Identity? other) => this.Email == other.Email && this.FacialFeatures.Equals(other.FacialFeatures) ? true : false;

        public override int GetHashCode()
        {
            return HashCode.Combine(Email,FacialFeatures);
        }
    }

    public class Authenticator
    {
        private HashSet<Identity> _identitySet = new HashSet<Identity>();
        public static bool AreSameFace(FacialFeatures faceA, FacialFeatures faceB) => faceA.Equals(faceB);

        public bool IsAdmin(Identity identity) => identity.Equals(new Identity("admin@exerc.ism", new FacialFeatures("green", 0.9m)));

        public bool Register(Identity identity) => this._identitySet.Add(identity);

        public bool IsRegistered(Identity identity) => this._identitySet.Contains(identity);

        public static bool AreSameObject(Identity identityA, Identity identityB) => ReferenceEquals(identityA, identityB);
    }
}
