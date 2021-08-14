using System;

namespace CommunityCrimeApp
{
    public sealed class CrimeRecord
    {
        public string Sector { get; set; }
        public string CommunityName { get; set; }
        public string Category { get; set; }
        public int CrimeCount { get; set; }
        public int ResidentCount { get; set; }
        public DateTime Date { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public string ID { get; set; }
        public string CommunityCenterPoint { get; set; }
    }
}
