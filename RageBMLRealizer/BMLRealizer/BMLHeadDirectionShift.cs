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
    /// Orient the head towards a target referenced by the target attribute.
    /// Permanently orient the head in a certain direction.
    /// </summary>
    public class BMLHeadDirectionShift : BMLBehavior
    {
        /// <summary>
        /// target towards which the head is oriented
        /// </summary>
        public string target;

        /// <summary>
        /// constructor
        /// </summary>
        public BMLHeadDirectionShift()
        {

        }

        /// <summary>
        /// parsing the xml
        /// atribute: id, amount, overshoot
        /// sync attribute: start, end
        /// </summary>
        /// <param name="reader"></param> XMLReader
        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);

            target = TryParseAtribute<string>(reader, "target", "", true);

            TryParseSyncPoint(reader, "start");
            TryParseSyncPoint(reader, "end");
        }
    }
}