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
    /// <summary>
    /// Temporarily change the posture of the ECA.
    /// Temporarily change the posture of the ECA. After the <posture> behavior has ended, return to the BASE posture.
    /// </summary>
    public class BMLPosture : BMLBehavior
    {
        /// <summary>
        /// constructor
        /// </summary>
        public BMLPosture()
        {

        }

        /// <summary>
        /// parsing xml
        /// atribute: id
        /// sync point: start, ready, relax, end
        /// </summary>
        /// <param name="reader"></param> XmlReader
        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);

            TryParseSyncPoint(reader, "start");
            TryParseSyncPoint(reader, "ready");
            TryParseSyncPoint(reader, "relax");
            TryParseSyncPoint(reader, "end");
        }
    }
}