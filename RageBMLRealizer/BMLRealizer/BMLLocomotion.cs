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
    /// Move the body of the character from one location to another.
    /// This behavior causes the character to move to the requested target in the manner described.
    /// </summary>
    public class BMLLocomotion : BMLBehavior
    {
        /// <summary>
        /// A reference towards a target instance that represents the end location of the locomotive behavior.
        /// </summary>
        public string target;

        /// <summary>
        /// The general manner of locomotion [WALK, RUN, STRAFE ...] (WALK is the only mandatory element in the set)
        /// </summary>
        public string manner;

        /// <summary>
        /// constructor
        /// </summary>
        public BMLLocomotion()
        {

        }

        /// <summary>
        /// parsing xml
        /// atribute: id, target, manner
        /// sync point: start, end
        /// </summary>
        /// <param name="reader"></param>
        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);

            target = TryParseAtribute<string>(reader, "target", "", true);
            manner = TryParseAtribute<string>(reader, "manner", "WALK", false);

            TryParseSyncPoint(reader, "start");
            TryParseSyncPoint(reader, "end");
        }

    }
}
