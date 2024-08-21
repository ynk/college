using System;
using System.Collections.Generic;
using System.Text;

namespace Extentie.inputStructuur
{
    public class WrData
    {
        public WrData(int wegsegmentId, string geo, int morfologie, int beginWegknoopId, int eindWegknoopId, int linksStraatNaamId, int rechtsStraatNaamId)
        {
            wegsegmentID = wegsegmentId;
            this.geo = geo;
            this.morfologie = morfologie;
            beginWegknoopID = beginWegknoopId;
            eindWegknoopID = eindWegknoopId;
            this.linksStraatNaamId = linksStraatNaamId;
            this.rechtsStraatNaamId = rechtsStraatNaamId;
        }

        public int wegsegmentID { get; set; }
        public string geo { get; set; }
        public int morfologie { get; set; }
        //oh god
        public int beginWegknoopID { get; set; }
        public int eindWegknoopID { get; set; }
        //
        public int linksStraatNaamId { get; set; }
        public int rechtsStraatNaamId { get; set; }
    }
}
