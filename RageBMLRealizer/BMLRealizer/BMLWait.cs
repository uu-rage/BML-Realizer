/*
  Copyright 2016 Utrecht University http://www.uu.nl/
   
  This software has been created in the context of the EU-funded RAGE project.
  Realising and Applied Gaming Eco-System (RAGE), Grant agreement No 644187,
  http://rageproject.eu/
 
  The Behavior Markup Language (BML) is a language whose specifications were developed
  in the SAIBA framework. More information here : http://www.mindmakers.org/projects/bml-1-0/wiki
 
  Created by: Christyowidiasmoro, Utrecht University <c.christyowidiasmoro@uu.nl>
 
  For more information, contact Dr. Zerrin YUMAK, Email: z.yumak@uu.nl Web: http://www.zerrinyumak.com/
  https://www.staff.science.uu.nl/~yumak001/UUVHC/index.html
*/

using System.Xml;

namespace BMLRealizer
{
    public class BMLWait : BMLBehavior
    {
        /// <summary>
        /// the duration of the wait in seconds
        /// </summary>
        public float duration;

        /// <summary>
        /// constructor
        /// </summary>
        public BMLWait()
        {

        }

        /// <summary>
        /// parsing the XML
        /// atribute: duration
        /// </summary>
        /// <param name="reader"></param>
        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);

            duration = TryParseAtribute<float>(reader, "duration", 0.0f, true);

            TryParseSyncPoint(reader, "start");

            syncPoints.Add("end", new BMLSyncPoint(this, "end", id + ":start+" + duration));
        }
    }
}
