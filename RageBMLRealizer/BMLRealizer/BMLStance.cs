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
    /// Child element of <posture> and <postureShift> behaviors, defines global body posture of the ECA.
    /// Child element of <posture> and <postureShift> behaviors, defines global body posture of the ECA. This global posture may then be modified by one or more <pose> siblings.
    /// </summary>
    public class BMLStance : BMLBehavior
    {
        /// <summary>
        /// Global body posture. Possible values are [SITTING, CROUCHING, STANDING, LYING]
        /// </summary>
        public Type type;

        /// <summary>
        /// Global body posture. Possible values are [SITTING, CROUCHING, STANDING, LYING]
        /// </summary>
        public enum Type
        {
            SITTING,
            CROUCHING,
            STANDING,
            LYING
        }

        /// <summary>
        /// constructor
        /// </summary>
        public BMLStance()
        {

        }

        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);

            type = TryParseAtribute<Type>(reader, "type", Type.STANDING, true);
        }
    }
}
