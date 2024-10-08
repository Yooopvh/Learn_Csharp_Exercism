using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public struct Coord : IEquatable<Coord>
    {
        public Coord(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }

        public ushort X { get; }
        public ushort Y { get; }

        public bool Equals(Coord other) => X == other.X && Y == other.Y;
    }

    public struct Plot : IEquatable<Plot>
    {
        public Coord coord1;
        public Coord coord2;
        public Coord coord3;
        public Coord coord4;
        public double longestSide = 0;

        public Plot(Coord coord1, Coord coord2, Coord coord3, Coord coord4)
        {
            this.coord1 = coord1;
            this.coord2 = coord2;
            this.coord3 = coord3;
            this.coord4 = coord4;
            longestSide = Math.Max(
                Math.Max(sideLength(coord1,coord2), sideLength(coord2,coord3)), 
                Math.Max(sideLength(coord3,coord4), sideLength(coord4,coord1))
                );
        }

        private double sideLength(Coord coord1, Coord coord2) => Math.Sqrt(Math.Pow(coord1.X-coord2.X, 2) + Math.Pow(coord1.Y-coord2.Y, 2));

        public bool Equals(Plot other) => coord1.Equals(other.coord1) &&
            coord2.Equals(other.coord2) &&
            coord3.Equals(other.coord3) &&
            coord4.Equals(other.coord4);
    }


    public class ClaimsHandler
    {
        HashSet<Plot> claims = new HashSet<Plot>();

        public void StakeClaim(Plot plot) => claims.Add(plot);

        public bool IsClaimStaked(Plot plot) => claims.Contains(plot);

        public bool IsLastClaim(Plot plot) => plot.Equals(claims.Last());

        public Plot GetClaimWithLongestSide()
        {
            Plot longestSidePlot = new Plot(new Coord(0,0), new Coord(0, 0), new Coord(0, 0), new Coord(0, 0));
            foreach (var claim in claims)
            {
                longestSidePlot = claim.longestSide > longestSidePlot.longestSide ? claim : longestSidePlot;
            }
            return longestSidePlot;
        }
    }
}
